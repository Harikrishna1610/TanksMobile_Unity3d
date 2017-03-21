using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TankHealth : NetworkBehaviour
{
    public int m_StartingHealth = 3;
    Text informationText;
    int health;
    void Start()
    {
        health = m_StartingHealth;
    }

    public void TakeDamage(int amount)
    {
        
        if (!isServer || health <= 0)
        {
           
            return; 
        }
        health -= amount;
     
        if (health <= 0)
        {
            
            health = 0;
            RpcDied();
            Invoke("BackToLobby", 3f);
            return;
        }
    }
    [ClientRpc]
    void RpcDied()
    {
        informationText = GameObject.FindObjectOfType<Text>();
        if (isLocalPlayer)
        {
            informationText.text = "Game Over";
        }
        else
            informationText.text = "You won";
    }
    void BackToLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
} 