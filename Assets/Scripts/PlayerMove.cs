using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float dashSpeed;

    public bool onFloor;
    public bool jumpKeyReleased;
    bool canDash;
   
    int dashCd;

    
    bool isDashing;
    bool hasJumpedOnce;

    
    

    public float npcPosX;
    float myPosX;

    public bool disableMove = false;
    public bool convMove = false;

    Rigidbody2D myBody;

    SpriteRenderer myRenderer;


    //public float test = 3;
    public float rayDis = 1;
    public Transform rayCastOrigin;
    float jumpTimer = 0;
    public float theTimer;
    public bool canJump = true;


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
            speed = 10f;
        }
        else
        {
            speed = 12;
        }

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;            //reset jumpTimer
        }
        Debug.Log("jumpTimer:" + jumpTimer);

    }

    void CheckKeys()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !isDashing)     //left right movement
        {
            myRenderer.flipX = false;
            HandleLRMovement(speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !isDashing)
        {
            myRenderer.flipX = true;
            HandleLRMovement(-speed);

        }

        if (Input.GetKeyUp(KeyCode.Z))   //fall when jump key released
        {
            jumpKeyReleased = true;
            if (myBody.velocity.y > 0)
            {
                myBody.velocity = new Vector3(myBody.velocity.x, 0);
            }
        }


        if (Input.GetKey(KeyCode.Z) && onFloor && jumpKeyReleased && !isDashing)   //jump conditions
        {
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);

            jumpKeyReleased = false;
            hasJumpedOnce = true;

            Debug.Log("firstJump");
        }

        if (Input.GetKey(KeyCode.Z) && hasJumpedOnce && jumpKeyReleased && !isDashing)   //double jump conditions
        {
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);

            jumpKeyReleased = false;
            hasJumpedOnce = false;
        }


        if (onFloor)    //dash conditions when on floor
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
        else     //dash conditions when in the air
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

    void JumpPhysics()    //gravity multiplier
    {
        if (myBody.velocity.y < 0)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1f) * Time.deltaTime;
        }
    }

    void HandleLRMovement(float dir)     //left right movement
    {
        myBody.velocity = new Vector3(dir, myBody.velocity.y);
    }


    IEnumerator Dash(float dir)    //dash codes
    {
        isDashing = true;
        myBody.velocity = new Vector2(dashSpeed * dir, 0f);
        
        float gravity = myBody.gravityScale;
        myBody.gravityScale = 0;
        yield return new WaitForSeconds(0.16f);
        myBody.velocity = new Vector2(0f, 0f);
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

        /*if (collision.gameObject.tag == "floor")    //check if is on floor
        {
            onFloor = true;
            canDash = true;
            hasJumpedOnce = false;

            myBody.velocity = new Vector3(myBody.velocity.x, 0);

        }*/

    }

    private void FixedUpdate()
    {

        RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin.position, Vector2.down, rayDis, 7);
        if (hit.collider)
        {
            Debug.Log(hit.collider.name);
            if ((hit.collider.tag == "floor") && jumpTimer <= 0)
            {
                Debug.Log("floor below, can jump");
                //canJump = true;
                //jumpTimer = theTimer;
                //haveDashed = false;
                //haveSecondJump = false;
                //jumpKeyReleased = true;
                onFloor = true;
                canDash = true;
                hasJumpedOnce = false;

                myBody.velocity = new Vector3(myBody.velocity.x, 0);
            }
            else
            {
                Debug.Log("can't jump");
                onFloor = false;
                //canDash = true;
            }
        }
        else
        {
            Debug.Log("can't jump, no collider detected");
            onFloor = false;
            //canDash = true;
        }

    }

}