﻿using UnityEngine;
using UnityEngine.Networking;
public class ShellScript_net : NetworkBehaviour {
    [SerializeField]
    float shellLifeTime = 2f;
    [SerializeField]
    bool canKill = true;
    bool isLive = true;
    float age;
    public ParticleSystem explosionParticles;  // the shell explosion particles
    MeshRenderer shellRenderer;      //shell model renderer
    // Use this for initialization
    void Start () {
        explosionParticles = GetComponentInChildren<ParticleSystem>();
        shellRenderer = GetComponent<MeshRenderer>();
       
	}
	
	//server callback is used to make the methods to run on the server 
    [ServerCallback]
	void Update () {
        age += Time.deltaTime;
        if(age>shellLifeTime)
        {
            NetworkServer.Destroy(gameObject);
        }
	
	}
    void OnCollisionEnter(Collision other)
    {
        if(!isLive)
        {
            return;
        }
        isLive = false;
        shellRenderer.enabled = false;
        explosionParticles.Play(true);
        if (!isServer)
            return;
        if (!canKill || other.gameObject.tag != "Player")
        {
            return;
        }
        //if(other.gameObject.tag == "Ground")
        //        {

        //      }
        //NetworkServer.Destroy(gameObject);
        TankHealth health = other.gameObject.GetComponent<TankHealth>();
        if (health != null)
        {
            health.TakeDamage(1);
            Debug.Log("took damage");
        }
    }
}
