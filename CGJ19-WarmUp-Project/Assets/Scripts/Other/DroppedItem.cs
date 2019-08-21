using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    Transform player;

    float speed = 20;
    float incrementingSpeed;
    float floor;
    private Rigidbody2D rigidBody;
    bool dropped = false;
    

    void Start()
    {
        player = GameManager.instance.GetComponent<GameManager>().player.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.sleepMode = RigidbodySleepMode2D.NeverSleep;
        floor = transform.position.y - 1f;
    }

    void Update()
    {
        if(transform.position.y < floor && dropped == false)
        {
            rigidBody.velocity = new Vector2(0f, 0f);
            rigidBody.gravityScale = 0f;
            dropped = true;
            Debug.Log("Item Dropped");
        }

        float dist = Vector3.Distance(player.position, transform.position);
        if (dist <= 5f)
        {
            incrementingSpeed += speed * Time.deltaTime;
            MoveTowardPlayer(dist, incrementingSpeed);
        }

        if (dist < 0.5f)
        {
            Debug.Log("Player Collide");
            OnDeath();
        }
    }

    void MoveTowardPlayer(float dist, float speed)
    {
        float sqrRemainingDistance = (transform.position - player.transform.position).sqrMagnitude;
        Vector3 newPosition = Vector3.MoveTowards(rigidBody.position, player.transform.position, speed * Time.deltaTime);
        rigidBody.MovePosition(newPosition);
    }

    void OnDeath()
    {
        player.GetComponent<Player>().ChangeHealth(1);
        Destroy(gameObject);
    }
}
