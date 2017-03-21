using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class TankController : NetworkBehaviour {
    [Header("Movement Variables")]
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField]
    float turnSpeed = 45.0f;
    [Header("CameraControls")]
    [SerializeField]
    float cameraDistance = 10f;
    [SerializeField]
    float cameraHeight = 12f;
    Rigidbody localRigidbody;
    Transform mainCamera;
    Vector3 CameraOffset;
    // Use this for initialization
    void Start () {
       if (!isLocalPlayer)   // This is to give control only to the local player
        {
            Destroy(this); //if this is not a local playerdelete this script
            return;
        }
        localRigidbody = GetComponent<Rigidbody>();
        CameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);
        mainCamera = Camera.main.transform;
        MoveCamera();
    
	
	}
	
	// FixedUpdate is called once per fixedframe
	void FixedUpdate () {
        float turnamount = CrossPlatformInputManager.GetAxis("Horizontal");// get the amount of rotation
        float movement = CrossPlatformInputManager.GetAxis("Vertical");// get the amount of rotation 
        Vector3 deltatranformation = transform.position + transform.forward * movementSpeed * movement * Time.deltaTime; // it calculates the amount of movement and creates new object with vector3
        localRigidbody.MovePosition(deltatranformation); // applies the movement that is in the position to the tank's rigidbody

        Quaternion deltaRotation = Quaternion.Euler(turnSpeed * new Vector3(0f, turnamount, 0f)*Time.deltaTime); // calculates the rotation using taking turnamount as argument
        localRigidbody.MoveRotation(localRigidbody.rotation * deltaRotation); // applies rotation to rigibody
        MoveCamera();

	}
    void MoveCamera() // movecamera along with the tank
    {
        mainCamera.position = transform.position; 
        mainCamera.rotation = transform.rotation;
        mainCamera.Translate(CameraOffset);
        mainCamera.LookAt(transform); // camera looks at the transform of the tank
    }
}
