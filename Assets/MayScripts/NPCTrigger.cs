using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    bool listen = false;
    bool listenH = false;
    public GameObject listenObject;
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
        if (listen)
        {
            Debug.Log("listen");
            GameObject newListener = Instantiate(listenObject, transform.position, transform.rotation);
            newListener.transform.SetParent(gameObject.transform);
            //newBall.transform.localPosition = new Vector3(dir * 1f, -0.1f); ///local position relative to player
        }
    }
}
