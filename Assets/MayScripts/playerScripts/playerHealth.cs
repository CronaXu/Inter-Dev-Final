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
    // Start is called before the first frame update
    void Start()
    {
        myHealth = playerHealthstat;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealthstat <= 0)
        {
            respawn = true;
            respawned = true;
            respawnBack = true;
        }
    }
}
