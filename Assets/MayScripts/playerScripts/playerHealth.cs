using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int playerHealthstat;
    public int myHealth;
    public bool respawn = false;
    public bool respawned = false;
    public bool respawnBack = false;
    public float invinsibleTimer = 0;
    public float invinsibleT = 0;
    // Start is called before the first frame update
    void Start()
    {
        myHealth = playerHealthstat;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("timer" + invinsibleTimer);
        if (playerHealthstat <= 0)              //death
        {
            respawn = true;
            respawned = true;
            respawnBack = true;
        }


        if(invinsibleTimer > 0)
        {
            invinsibleTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            if (invinsibleTimer <= 0)
            {
                playerHealthstat--;                                             //-health, cameraShake maybe?
                invinsibleTimer = invinsibleT;
                //Debug.Log("timer" + invinsibleTimer);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            invinsibleTimer = 0;
        }
    }
}
