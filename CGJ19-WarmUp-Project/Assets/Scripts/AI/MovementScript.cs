//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class MovementScript : MonoBehaviour
//{
//    public float speed;
//    public LayerMask blockingLayer;

//    private BoxCollider2D boxCollider;
//    private Rigidbody2D rb2D;

    
//    protected virtual void Start()
//    {
//        boxCollider = GetComponent<BoxCollider2D>();
//        rb2D = GetComponent<Rigidbody2D>();
//    }


//    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
//    {
//        Vector2 start = transform.position;
//        Vector2 end = start + new Vector2(xDir, yDir);

//        boxCollider.enabled = false;
//        hit = Physics2D.Linecast(start, end, blockingLayer);
//        boxCollider.enabled = true;

//        if (hit.transform == null)
//        {
//            StartCoroutine(SmoothMovement(end));
//            return true;
//        }
        
//        GameManager.instance.enemiesMoving = false;
//        return false;
//    }


//    protected IEnumerator SmoothMovement(Vector3 end)
//    {
//        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

//        while(sqrRemainingDistance > float.Epsilon)
//        {
//            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, speed * Time.deltaTime);
//            rb2D.MovePosition(newPosition);
//            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
//            yield return null;
//        }
//        GameManager.instance.enemiesMoving = false;
//        Debug.Log("movement done!");
//    }


//    protected virtual void AttemptMove<T>(int xDir, int yDir)
//        where T : Component
//    {
//        RaycastHit2D hit;
//        Move(xDir, yDir, out hit);
//    }
//}
