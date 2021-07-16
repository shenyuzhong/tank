using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{

    // Use this for initialization

    public Transform player;
    public GameObject bulletPrefab;
    public Vector3 gunDirection;
    public float range;
    public float angleRange;
    public float speed;
    public float interval;
    float elapsedTime = 0;

    Vector3 shootingDirection;

    void Start()
    {
        shootingDirection = transform.TransformDirection(gunDirection);
        elapsedTime = interval;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.position + new Vector3(0, 2, 0);

        float distance = Vector3.Distance(transform.position, targetPosition);
        Vector3 targetDirection = targetPosition - transform.position;

        elapsedTime += Time.deltaTime;

        if (distance < range && Vector3.Angle(shootingDirection, targetDirection) < angleRange && elapsedTime > interval)
        {
            // GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.FromToRotation(Vector3.forward, targetDirection)) as GameObject;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(targetDirection)) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(targetDirection * speed);

            elapsedTime = 0;
            GetComponent<AudioSource>().Play();
        }
    }
}
