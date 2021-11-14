using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    bool onFloor;
    bool canJump;

    public float npcPosX;
    float myPosX;

    public bool disableMove = false;
    public bool convMove = false;

    Rigidbody2D myBody;

    SpriteRenderer myRenderer;


    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onFloor && myBody.velocity.y > 0.1)
        {
            onFloor = false;
        }
        if (!disableMove)               //player can only control when notdisableMove
        {
            CheckKeys();
            JumpPhysics();
        }
        if (convMove)
        {
            HandleConvMove();
        }
    }

    void CheckKeys()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myRenderer.flipX = false;
            HandleLRMovement(speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRenderer.flipX = true;
            HandleLRMovement(-speed);

        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            canJump = true;
        }


        if (Input.GetKey(KeyCode.Z) && onFloor && canJump)
        {
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);

            canJump = false;
        }
        
    }

    void JumpPhysics()
    {
        if (myBody.velocity.y < 0)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1f) * Time.deltaTime;
        }
    }

    void HandleLRMovement(float dir)
    {
        myBody.velocity = new Vector3(dir, myBody.velocity.y);
    }


    void HandleConvMove()
    {
        myPosX = transform.position.x;
        if ((myPosX- npcPosX) < 1f)
        {
            HandleLRMovement(speed);
            myRenderer.flipX = false;
            //Debug.Log("move right");
        }if((myPosX - npcPosX) > 1f)
        {
            myRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "floor")
        {
            onFloor = true;
            

            myBody.velocity = new Vector3(myBody.velocity.x, 0);

            

        }

    }

}