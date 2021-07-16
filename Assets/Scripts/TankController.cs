using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour
{

    public float speed = 8;
    public GameObject shellPrefab;
    public AudioClip hitSound;

    public Transform leftFrontWheel, rightFrontWheel, leftBackWheel, rightBackWheel;
    public Transform leftSteering, rightSteering;
    public Transform turret, cannon;
    public Transform shellPosition;

    public float steerSpeed = 100;
    public float turretSpeed = 100;
    public float cannonSpeed = 50;
    float backWheelRotationSpeed, frontWheelRotatonSpeed;

    float backWheelRadius = 1.23f;
    float frontBackRatio = 1.5f;
    float moveSteerRatio = 0.01f;

    float fireRate = 0.25f, nextFire = 0;
    float cannonAngle = 0;
    float steerAngle = 0;
    float turnAngle = 0;

    new Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        backWheelRotationSpeed = speed / backWheelRadius / 3.1415f * 180;
        frontWheelRotatonSpeed = backWheelRotationSpeed * frontBackRatio;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // The following cannot be used before the Euler angles cannot be easily predicted.
        //transform.Rotate(frontSteering.transform.localEulerAngles * moveSteerRatio);

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
            Debug.Log("Tank: " + verticalInput);


        if (verticalInput != 0)
        {
            turnAngle = steerAngle * moveSteerRatio * Mathf.Sign(verticalInput);
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(0, turnAngle, 0));
        }
       
        // rigidbody.MovePosition(rigidbody.position + transform.TransformVector(new Vector3(0, 0, verticalInput * speed * Time.deltaTime)));

        rigidbody.MovePosition(rigidbody.position + transform.forward * verticalInput * speed * Time.deltaTime);

        #region Transform based
        //if (verticalInput > 0)
        //{
        //    //transform.localRotation = Quaternion.Slerp(transform.localRotation,
        //    //    transform.localRotation * leftSteering.transform.localRotation, moveSteerRatio);
        //    turnAngle = steerRotationSpeed * Time.deltaTime * moveSteerRatio * steerAngle;
        //    rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(0, turnAngle, 0));
        //}
        //else if (verticalInput < 0)
        //{
        //    //transform.localRotation = Quaternion.Slerp(transform.localRotation,
        //    //    transform.localRotation * Quaternion.Inverse(leftSteering.transform.localRotation), moveSteerRatio);
        //    turnAngle = -steerRotationSpeed * Time.deltaTime * moveSteerRatio * steerAngle;
        //    rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(0, turnAngle, 0));
        //}

        //transform.Translate(0, 0, verticalInput * speed * Time.deltaTime);
        #endregion

        // Wheels rotation
        leftFrontWheel.Rotate(verticalInput * frontWheelRotatonSpeed * Time.fixedDeltaTime, 0, 0);
        rightFrontWheel.Rotate(verticalInput * frontWheelRotatonSpeed * Time.fixedDeltaTime, 0, 0);
        leftBackWheel.Rotate(verticalInput * backWheelRotationSpeed * Time.fixedDeltaTime, 0, 0);
        rightBackWheel.Rotate(verticalInput * backWheelRotationSpeed * Time.fixedDeltaTime, 0, 0);

        // Steering
        SteeringTransform(leftSteering);
        SteeringTransform(rightSteering);

        // Turret
        turret.transform.Rotate(0, Input.GetAxis("Turret") * turretSpeed * Time.fixedDeltaTime, 0);

        // Cannon
        cannonAngle += Input.GetAxis("Canon") * cannonSpeed * Time.fixedDeltaTime;
        cannonAngle = Mathf.Clamp(cannonAngle, -90, 0);
        cannon.localEulerAngles = new Vector3(cannonAngle, 0, 0);

        if (Input.GetButton("Jump") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject shell = Instantiate(shellPrefab, shellPosition.position, shellPosition.rotation) as GameObject;
            shell.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * 2000);
            GetComponent<AudioSource>().Play();
        }
    }

    void SteeringTransform(Transform transform)
    {
        steerAngle += Input.GetAxis("Horizontal") * steerSpeed * Time.fixedDeltaTime;
        steerAngle = Mathf.Clamp(steerAngle, -45, 45);
        transform.localEulerAngles = new Vector3(0, steerAngle, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }
    }
}
