﻿/*
 * 
 * Authors: Spencer Wilson, Andrew Ramirez, Hunter Goodin
 * Date Created: 10/8/2017 @ 5:29 pm
 * Date Modified: 11/20/2017 @ 10:06 pm by Hunter Goodin 
 * Project: CompSciClubFall2017
 * File: Player.cs
 * Description: File that houses all of the code for the player's health, lives, movement, and abilities.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


    /* Code regarding the player's moving mechanics & defining the player  */ 
    public float speed; // Declared a floating point variable named speed that will house the user's speed.
    private Rigidbody player; // Creating a Rigidbody object named player.
    private bool isEnemy;
    public Boundary boundary; 

    /* Code regarding player's health and lives */
    // private int currentLives;
    private const int maxLives = 5; // Max amount of lives that the player can have
    private const int maxHealth = 100; // Max amount of player health
    public Text healthText; // Reference to health text
    // public Text liveText; // Reference to live text
    // public Text gameOverText; // Reference to GameOver text
    public int playerHealth = 100; // We will use this for the player's health 
    private float damRate = 0.5f;             // Spawn Rate. Pretty self explanitory 
    private float nextDam;            // Used to see when the function should spawn the next prefab 

    /* Code regarding player's shooting mechanics */ 
    public GameObject shot;         // We will use this to reference the player's projectile -- in-engine, we will populate this with our ""Bolt" prefab. 
    public Transform shotSpawn;     // We will use this to know where to spawn the shot. The location will be set to where the "ShotSpawn" GameObject is. We will do this by populating the Player with the ShotSpawn child object in-engine. 
    public float fireRate;          // The player's fire rate -- this will probably kept as 0 but... just in case. 
    private float nextFire;         // Will be used for the next time the player can fire a shot.

    // Variables related to player audio
    //  public AudioClip player_shoot_1;
    public AudioSource playerAudio;  //Assigns audio linked to player abject to playerAudio
   


    public void Start () // Start method initializes any variables/objects/rigidbodies that need to be used within the script.
    {
        player = GetComponent<Rigidbody>(); // Getting the Rigidbody component of the GameObject player is attached to. 
        isEnemy = false; // Sets the player as not an enemy
        //currentLives = 2; // Set initial player live count
        Health playerHealth = this.GetComponent<Health>();
        playerHealth.hp = 100; // Set initial player health
        setHealthandLiveText(); // Update the health and live count text component
        //  danger = AudioClip.Create("alert_low_health", 1, 1, 1, true);
        playerAudio = GetComponent<AudioSource>();

        //Debug.Log("Current Lives: " + currentLives);
        Debug.Log("Current Health " + playerHealth.hp);
    }
	
	// Update is called once per frame
	public void Update ()
    {
        isAlive(); // Function checks if the player is alive or not.
        playerWeapons();
	}

    public void FixedUpdate() // FixedUpdate is called on a regular timeline. Use FixedUpdate for physics based functions that need to be executed.
    {
        playerMovement(); // Calls on the playerMovement() function.
    }

    void OnDestroy()
    {
        // Check that the player is dead
        Health playerHealth = this.GetComponent<Health>();
        if (playerHealth != null && playerHealth.hp <= 0)
        {
            // Game Over.
            // gameOverText.text = "Game Over"; // Show game over text
            // todo: allow the player to restart? or go back to the game menu
        }
    }
    // Display Health and Lives
    public void setHealthandLiveText()
    {
        Health playerHealth = this.GetComponent<Health>();
        NewMethod(playerHealth);
        // liveText.text = "Lives: " + currentLives;
    }

    private void NewMethod(Health playerHealth)
    {
        healthText.text = "Ship Health: " + playerHealth.hp.ToString();
    }

    public void TakeDamage(int damage) // This function is called whenever an enemy bullet enters the player's collider.
    {
        playerHealth = playerHealth - damage;
        playerAudio.Play();

        setHealthandLiveText(); 
    }

    // This function displays the damage being dealt 

    void OnCollisionEnter(Collision col)
    {
        bool damagePlayer = false;
        int dam;

        if (col.gameObject.name == "StingerBullet(Clone)")
        {
            StingerBullet enemy = col.gameObject.GetComponent<StingerBullet>();
            if (enemy != null)
            { damagePlayer = true; }
            dam = 5;
        }
        else if (col.gameObject.name == "ChomperLazer(Clone)")
        {
            ChomperBullet enemy = col.gameObject.GetComponent<ChomperBullet>();
            if (enemy != null)
            { damagePlayer = true; }
            dam = 10;
        }
        else if (col.gameObject.name == "CrabberExplosion(Clone)")
        {
            CrabberExplosion enemy = col.gameObject.GetComponent<CrabberExplosion>();
            if (enemy != null)
            { damagePlayer = true; }
            dam = 10;
        }
        else if (col.gameObject.name == "StrikerProjectile(Clone)")
        {
            StrikerProjectile enemy = col.gameObject.GetComponent<StrikerProjectile>();
            if (enemy != null)
            { damagePlayer = true; }
            dam = 5;
        }
        else if (col.gameObject.name == "AsteroidUp(Clone)")
        {
            AsteroidUp enemy = col.gameObject.GetComponent<AsteroidUp>();
            if (enemy != null)
            { damagePlayer = true; }
            dam = 15;
        }
        else if (col.gameObject.name == "AsteroidDown(Clone)")
        {
            AsteroidDown enemy = col.gameObject.GetComponent<AsteroidDown>();
            if (enemy != null)
            { damagePlayer = true; }
            dam = 15;
        }
        else
        {  dam = 0; }

        // Setting the Health and Life Text
        if (damagePlayer)
        {
            Health playerHealth = this.GetComponent<Health>();
       
            // Damage the player if their health is not 0
            if (playerHealth.hp > 0 && dam == 1 && Time.time > nextDam)
            {
                playerHealth.Damage(dam, this.isEnemy); // Damage the player by x amount

                Debug.Log("Current Health: " + playerHealth.hp);
                setHealthandLiveText();
                nextDam = Time.time + damRate;
            }
            else if (playerHealth.hp > 0 && dam == 5)
            {
                playerHealth.Damage(dam, this.isEnemy); // Damage the player by x amount

                Debug.Log("Current Health: " + playerHealth.hp);
                setHealthandLiveText();
            }
            else if (playerHealth.hp > 0 && dam == 10 && Time.time > nextDam)
            {
                playerHealth.Damage(dam, this.isEnemy); // Damage the player by x amount

                Debug.Log("Current Health: " + playerHealth.hp);
                setHealthandLiveText();
                nextDam = Time.time + damRate;
            }
            else if (playerHealth.hp > 0 && dam == 15 && Time.time > nextDam)
            {
                playerHealth.Damage(dam, this.isEnemy); // Damage the player by x amount

                Debug.Log("Current Health: " + playerHealth.hp);
                setHealthandLiveText();
                nextDam = Time.time + damRate;
            }
        }
        if (col.gameObject.name == "StingerBullet(Clone)")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name == "Bolt(Clone)")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name == "StrikerProjectile(Clone)")
        {
            Destroy(col.gameObject);
        }
    }

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, yMin, yMax;    // initializes these values 
    }

    private void playerMovement()
    {
        if(Input.GetKey("w")) // When w is pressed, move the player up.
        {
            player.transform.Translate(transform.up * Time.deltaTime * speed);
            //Debug.Log("Player is moving up.");
        }
        if (Input.GetKey("a")) // When a is pressed, move the player to the left.
        {
            player.transform.Translate(-transform.right * Time.deltaTime * speed);
            //Debug.Log("Player is moving right.");
        }
        if (Input.GetKey("s")) // When s is pressed, move the player down.
        {
            player.transform.Translate(-transform.up * Time.deltaTime * speed);
            //Debug.Log("Player is moving down.");
        }
        if(Input.GetKey("d")) // When d is pressed, move the player to the right.
        {
            player.transform.Translate(transform.right * Time.deltaTime * speed);
            //Debug.Log("Player is moving to the left.");
        }
        //player.position = new Vector3(transform.position.x, transform.position.y, 0f);
        player.position = new Vector3
        (
            Mathf.Clamp (player.position.x, boundary.xMin, boundary.xMax),  // The player can't move beyond these values on the X axis 
            Mathf.Clamp (player.position.y, boundary.yMin, boundary.yMax),  // The player can't move beyond these values on the Y axis
            0.0f                                                            // The player can't move beyond 0.0 on the Z axis 
        ); 
    }

    private void isAlive()
    {
        if(playerHealth > 0)
        {
            Debug.Log("Player is alive");
        }
        else if(playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            // Time.timeScale = 0;             // Commented out to test the Game Over Scene 
            Application.LoadLevel(0);
        }
    }

    /* Hunter Goodin was here */
    private void playerWeapons()
    {
        if(Input.GetKeyDown("space"))
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // Creates a new instance of the Shot prefab everytime this is called. 
        }
    }

}


