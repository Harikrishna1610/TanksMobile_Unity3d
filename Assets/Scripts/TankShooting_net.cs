using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class TankShooting_net : NetworkBehaviour {

    // Use this for initialization
    [SerializeField]
    float power = 800f;
    [SerializeField]
    GameObject shellPrefab;
    [SerializeField]
    Transform gunBarrel;
    void reset()
    {
        gunBarrel = transform.FindChild("GunBarrel");
    }
   
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
	     if(CrossPlatformInputManager.GetButtonDown("Jump")||CrossPlatformInputManager.GetButton("Fire"))
        {
            CmdSpawnShell();
        }
	}
    [Command]
    void CmdSpawnShell()
    {
        GameObject instance = Instantiate(shellPrefab, gunBarrel.position, gunBarrel.rotation) as GameObject;
        instance.GetComponent<Rigidbody>().AddForce(gunBarrel.forward * power);
        NetworkServer.Spawn(instance);
    }
}
