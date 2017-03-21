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
        gunBarrel = transform.FindChild("GunBarrel"); //get the barrel transform
    }
   
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)  // if it is not a localplayer leave 
            return;
	     if(CrossPlatformInputManager.GetButtonDown("Jump")||CrossPlatformInputManager.GetButton("Fire")) // gets the buttons named "fire" and "jump"
        {
            CmdSpawnShell();
        }
	}
    [Command] // command is called by the network server
    void CmdSpawnShell()
    {
        GameObject instance = Instantiate(shellPrefab, gunBarrel.position, gunBarrel.rotation) as GameObject; // instantiates the shell prefab at the gunbarrel's position
        instance.GetComponent<Rigidbody>().AddForce(gunBarrel.forward * power); // adds force to the bullet after instantiating 
        NetworkServer.Spawn(instance); // spawns the shell in the network so that those can be instantiated in all the games
    }
}
