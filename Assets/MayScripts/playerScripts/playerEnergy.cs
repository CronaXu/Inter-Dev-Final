using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEnergy : MonoBehaviour
{
    public float energy = 1;
    public float enToLife = 0.25f;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (energy >= enToLife)
        {
            energyToLife();
        }
    }

    void energyToLife()
    {
        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (timer < 1)
            {
                Debug.Log("charged Shot");
            }
            if (timer > 1)
            {
                Debug.Log("get Health");
            }
            timer = 0;

        }

    }
}
