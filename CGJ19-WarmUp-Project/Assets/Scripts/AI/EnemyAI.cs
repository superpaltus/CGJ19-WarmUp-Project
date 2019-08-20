using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float viewRange;
    public LayerMask blockingLayer;

    Coroutine currentRoutine;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    bool playerInRangePrev = false;
    bool playerInRange = false;

    bool skipMove;
    float waitTime = 1.0f;

    void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        DetectPlayer();
    }


    public bool Move()
    {
        waitTime = Random.Range(2.0f, 5.0f);
        
        RaycastHit2D hit;
        Vector2 start = transform.position;
        Vector2 end;

        boxCollider.enabled = false;

        do
        {
            int xDir = Random.Range(-10, 10);
            int yDir = Random.Range(-10, 10);
            end = start + new Vector2(xDir, yDir);
            hit = Physics2D.Linecast(start, end, blockingLayer);
        }
        while (hit.transform != null);
        
        boxCollider.enabled = true;

        currentRoutine = StartCoroutine(SmoothMovement(end));

        return true;
    }


    IEnumerator SmoothMovement(Vector3 end)
    {
        if (playerInRange)
        {
            float sqrRemainingDistance = (transform.position - player.position).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon && playerInRange)
            {
                Vector3 newPosition = Vector3.MoveTowards(rb2D.position, player.position, speed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(waitTime);

            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon && !playerInRange)
            {
                Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, speed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }
        }

        GameManager.instance.enemiesMoving = false;
        Debug.Log("movement done!");
    }

    void DetectPlayer()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (playerInRange)
        { playerInRangePrev = true; }
        else
        { playerInRangePrev = false; }


        if (dist <= viewRange)
            playerInRange = true;
        else
            playerInRange = false;

        if(!playerInRangePrev && playerInRange)
        {
            StopCoroutine(currentRoutine);
            Move();
        }
        
    }
}
