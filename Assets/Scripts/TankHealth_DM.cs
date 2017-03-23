using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class TankHealth_DM : NetworkBehaviour {
    public int maxHealth = 3;

    Text informationText;
    int health;
    private void Start()
    {
        health = maxHealth;
        if (isServer)
            DeathMatchManager.AddTank(this);
    }
    public void TakeDamage(int amount)
    {
        if (!isServer || health <= 0)
            return;
        health = health-amount;
        if (health <= 0)
        {
            health = 0;
            RpcDied();
            if (DeathMatchManager.RemoveTankAndCheckWinner(this))
            {
                TankHealth_DM tank = DeathMatchManager.GetWinner();
                tank.RpcWon();
                Invoke("BackToLobby", 3f);
            }
        }
        return;

    }
    [ClientRpc]
    void RpcDied()
    {
        GetComponent<TankColor>().Hidetank();
        if (isLocalPlayer)
        {
            informationText = GameObject.FindObjectOfType<Text>();
            informationText.text = "GameOver";
            GetComponent<TankController>().enabled = false;
            GetComponent<TankShooting_net>().enabled = false;
        }
    }
    [ClientRpc]
    public void RpcWon()
    {
        if(isLocalPlayer)
        {
            informationText = GameObject.FindObjectOfType<Text>();
            informationText.text = "YOU WON";
        }
    }
    void BackToLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();

    }
}
