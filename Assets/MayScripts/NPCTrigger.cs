using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    bool listen = false;
    bool listenH = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!listenH && listen)
        {
            Invoke("listenNPC", 2);
            listenH = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            listen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            listen = false;
            listenH = false;
        }
    }

    private void listenNPC()
    {
        Debug.Log("listen");
        
    }
}
