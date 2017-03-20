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
        if (!isLocalPlayer)
        {
            Destroy(this); //if this is not a local player
            return;
        }
        localRigidbody = GetComponent<Rigidbody>();
        CameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);
        mainCamera = Camera.main.transform;
        MoveCamera();
    
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float turnamount = CrossPlatformInputManager.GetAxis("Horizontal");
        float movement = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 deltatranformation = transform.position + transform.forward * movementSpeed * movement * Time.deltaTime;
        localRigidbody.MovePosition(deltatranformation);

        Quaternion deltaRotation = Quaternion.Euler(turnSpeed * new Vector3(0f, turnamount, 0f)*Time.deltaTime);
        localRigidbody.MoveRotation(localRigidbody.rotation * deltaRotation);
        MoveCamera();

	}
    void MoveCamera()
    {
        mainCamera.position = transform.position;
        mainCamera.rotation = transform.rotation;
        mainCamera.Translate(CameraOffset);
        mainCamera.LookAt(transform);
    }
}
