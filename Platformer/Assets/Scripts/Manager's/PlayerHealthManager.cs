using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [HideInInspector] public static PlayerHealthManager instance;

    public int health;

    [Header("Player Properties")]
    public int MaxHealth = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        health = MaxHealth;
    }

    public void DamagePlayer(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) //kill player when his health reaches 0
        { 
            health = 0;
            Death();
        }
        Debug.Log("Player Has been Damaged, Current Health: " + health);

    }

    void Death()
    {
        //We will play death animations and we will do stuff like empty players inventory and etc
        GamePlayManager.instance.DestroyPlayer();
        Debug.Log("Player Died");
        Invoke("Respawn",0.5f); //Player will respawn after 0.5 seconds of death;; just doing it here for testing purposes we can add a game over screen instead of respawning and player will respawn via that screen
    }

    void Respawn()
    {
        //here respawn animations and other stuff
        GamePlayManager.instance.SpawnPlayer(CheckPointManager.instance.CurrentCheckPoint);
        health = MaxHealth;
    }



}
