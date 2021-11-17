using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkManager : MonoBehaviour
{
    float SaveX;
    float SaveY;
    //int SaveSparkle;
    //int SaveBullet;
    bool check = false;
    //GameObject[] itemList;
    //GameObject items;
    public List<GameObject> itemList;
    GameObject collObject;
    GameObject newChecker;
    bool triggerH = false;
    public bool checkered = false;
    public GameObject checkObject;
    // Start is called before the first frame update
    void Start()
    {
        //SaveBullet = FindObjectOfType<playerShoot>().bulletCount;
        //SaveSparkle = 0;
        SaveX = transform.position.x;
        SaveY = transform.position.y;

        itemList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            if (!triggerH)
            {
                Invoke("sit", 1);
                triggerH = true;
            }

        }

        /*if (collObject.GetComponent<dialogueTrigger>().ConvEnter == true)
        {
            SaveX = transform.position.x;
            SaveY = transform.position.y;
            //SaveSparkle = FindObjectOfType<playerInteract>().starCount;
            //SaveBullet = FindObjectOfType<playerShoot>().bulletCount;
            Debug.Log("progress saved");
            check = false;
            itemList.Clear();
        }*/


        /*if (GetComponent<restart>().respawn == true)
        {
            transform.position = new Vector3(SaveX + 2, SaveY);
            //FindObjectOfType<playerInteract>().starCount = SaveSparkle;
            //FindObjectOfType<playerShoot>().bulletCount = SaveBullet;
            foreach (GameObject item in itemList)
            {
                item.SetActive(true);
                if (item.tag == "enemy")
                {
                    item.GetComponent<enemyBehavior>().added = false;
                    item.GetComponent<enemyBehavior>().enemyHealth = item.GetComponent<enemyBehavior>().myHealth;
                    if (item.name == "movingEnemy")
                    {
                        item.GetComponent<enemyBehavior>().enemyHealth = 1;
                    }
                }
            }
            GetComponent<restart>().respawn = false;
        }*/


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkpoint")
        {
            Debug.Log("checkpoint");
            check = true;
            collObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkpoint")
        {
            check = false;
            triggerH = false;
            checkered = false;
            Destroy(newChecker);
        }
    }

    private void sit()
    {
        if (check)
        {
            Debug.Log("checked");
            Debug.Log(collObject.transform.position);
            newChecker = Instantiate(checkObject, collObject.transform.position, collObject.transform.rotation);
            //newChecker.transform.SetParent(gameObject.transform);
            newChecker.transform.localPosition = new Vector3(collObject.transform.position.x, collObject.transform.position.y+1.65f); ///local position relative to player
            checkered = true;
        }
    }

    private void checkerKey()
    {
        if (checkered)
        {
            if (Input.GetKey(KeyCode.Space))
            {

            }
        }
    }



}
