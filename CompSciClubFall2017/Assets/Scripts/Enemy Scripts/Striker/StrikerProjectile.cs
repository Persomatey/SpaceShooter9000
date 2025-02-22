﻿/*
 * 
 * Author: Spencer Wilson
 * Date Created: 11/24/2017 @ 9:44 pm
 * Date Modified: 12/10/2017 @ 9:27 pm
 * Project: CompSciClubFall2017
 * File: StrikerProjectile.cs 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerProjectile : MonoBehaviour
{
    private GameObject strikerProjGameObj;
    private GameObject playerGameObj;
    private Vector3 newPos;
    private Vector3 finalPos;
    public float speed = 10f; // Private float variable named speed that holds the StrikerProjectile's speed.
    private const float lifetime = 5f; // Private float variable named lifetime that holds the projectile's lifetime.
    public int strikerDamage = 5; 

    //private Rigidbody strikerPRb;
    // Use this for initialization
    void Start()
    {
        playerGameObj = GameObject.Find("Player"); // Assigns the gameObject named Player to teh playerGameObj reference variable.
        strikerProjGameObj = gameObject;
        Destroy(gameObject, 2f); // Destorys the current gameObject after five seconds.
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        StrikerProjMovement();
    }

    private void StrikerProjMovement()
    {
        transform.position = Vector3.MoveTowards(strikerProjGameObj.transform.position, playerGameObj.transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().TakeDamage(strikerDamage);
        }
        else if(col.gameObject.name == "Bolt(Clone)")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
