﻿/* 
 * Programmer:	Hunter Goodin 
 * Date:		11/5/2017 
 * Project: 	CompSciClubFall2017
 * Description: Creating the Bolt object the player will shoot. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectiles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Bolt(Clone)")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name == "StingerBullet(Clone)")
        {
            Destroy(col.gameObject);
        }


    }
}
