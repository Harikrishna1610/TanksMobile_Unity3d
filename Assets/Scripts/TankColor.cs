using UnityEngine;
using UnityEngine.Networking;
public class TankColor : NetworkBehaviour
{
    [SyncVar]
    public Color color;
    MeshRenderer[] rends;
	// Use this for initialization
	void Start () {
        //gets all the meshrenderers of the tank
        rends = GetComponentsInChildren<MeshRenderer>();
        for(int i = 0;i<rends.Length;i++)
        {
            rends[i].material.color = color;
        }
		
	}
	
	// Update is called once per frame
	public void Hidetank()
    {
        for (int i = 0; i < rends.Length; i++)
            rends[i].material.color = Color.clear;
    }
}
