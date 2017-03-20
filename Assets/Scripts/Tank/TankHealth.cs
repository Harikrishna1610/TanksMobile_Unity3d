using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TankHealth : NetworkBehaviour
{
    public int m_StartingHealth = 3;
    /*public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
   // public GameObject m_ExplosionPrefab;
    
    /*
    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    private bool m_Dead;            


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
    */
    Text informationText;
    int health;
    GameObject tank;
    void start()
    {
        health = m_StartingHealth;
    }

    public void TakeDamage(int amount)
    {
        //Debug.Log("Atleast Entering here");
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        if (!isServer || health <= 0)
        {
            //  Debug.Log("leaving from here");
            return;
        }
        health -= amount;
        //Debug.Log(health);
        if (health <= 0)
        {
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

   /* private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
    }*/
    void BackToLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
} 