using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveX, moveY;
    public float lastMoveX, lastMoveY;
    public bool moving;
    public Vector2 nextMove;

    public float speed;


    Animator playerAnim;
    Rigidbody2D playerRigidbody;

    


    void Start()
    {

        if (GetComponent<Animator>() != null)
        {
            playerAnim = GetComponent<Animator>();
            playerAnim.SetFloat("LastMoveX", 0);
            playerAnim.SetFloat("LastMoveY", -1);
        } else
        {
            Debug.Log("Player missing animator!");
        }

        if(GetComponent<Rigidbody2D>() != null)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        } else
        {
            Debug.Log("Player missing Rigidbody2D!");
        }


    }

    void FixedUpdate()
    {
        RigidMove();
    }


    void Update()
    {
        if(moving)
        {
            lastMoveX = moveX;
            lastMoveY = moveY;
        }
        
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        playerAnim.SetBool("Moving", moving);

        playerAnim.SetFloat("LastMoveX", lastMoveX);
        playerAnim.SetFloat("LastMoveY", lastMoveY);


    }

    void RigidMove()
    {
        nextMove = new Vector2(moveX, moveY);
        nextMove = nextMove.normalized;

        playerRigidbody.AddForce(nextMove * speed * 7.5f, ForceMode2D.Impulse);
    }

}
