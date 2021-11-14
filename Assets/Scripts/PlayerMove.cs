using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float dashSpeed;

    bool onFloor;
    bool canJump;
    bool canDash;
   
    int dashCd;

    
    bool isDashing;

    
    

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
        if (dashCd > 0)
        {
            dashCd--;
        }
        if (!onFloor)
        {
            speed = 10.5f;
        }
        else
        {
            speed = 12;
        }
        
    }

    void CheckKeys()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !isDashing)
        {
            myRenderer.flipX = false;
            HandleLRMovement(speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !isDashing)
        {
            myRenderer.flipX = true;
            HandleLRMovement(-speed);

        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            canJump = true;
            if (myBody.velocity.y > 0)
            {
                myBody.velocity = new Vector3(myBody.velocity.x, 0);
            }
        }


        if (Input.GetKey(KeyCode.Z) && onFloor && canJump && !isDashing)
        {
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);

            canJump = false;
        }

        if (onFloor)
        {
            if (Input.GetKeyDown(KeyCode.C) && dashCd == 0)
            {
                if (!myRenderer.flipX)
                {
                    StartCoroutine(Dash(1));

                }
                else
                {
                    StartCoroutine(Dash(-1));
                }
                dashCd = 15;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C) && canDash)
            {
                if (!myRenderer.flipX)
                {
                    StartCoroutine(Dash(1));

                }
                else
                {
                    StartCoroutine(Dash(-1));
                }
                canDash = false;
            }
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


    IEnumerator Dash(float dir)
    {
        isDashing = true;
        myBody.velocity = new Vector2(dashSpeed * dir, 0f);
        
        float gravity = myBody.gravityScale;
        myBody.gravityScale = 0;
        yield return new WaitForSeconds(0.16f);
        isDashing = false;
        myBody.gravityScale = gravity;
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
            canDash = true;

            myBody.velocity = new Vector3(myBody.velocity.x, 0);

            

        }

    }

}