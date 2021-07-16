using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Camera thirdPersonCamera;
    public Camera overheadCamera;

    enum CameraType { ThirdPerson, Overhead };

    CameraType currentCamera = CameraType.ThirdPerson;

	// Use this for initialization
	void Start () {
        //overheadCamera.enabled = true;
        //thirdPersonCamera.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (currentCamera)
            {
                case CameraType.ThirdPerson:
                    currentCamera = CameraType.Overhead;
                    thirdPersonCamera.enabled = false;
                    overheadCamera.enabled = true;
                    break;
                case CameraType.Overhead:
                    currentCamera = CameraType.ThirdPerson;
                    thirdPersonCamera.enabled = true;
                    overheadCamera.enabled = false;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene("Terrain");        
	}
}
