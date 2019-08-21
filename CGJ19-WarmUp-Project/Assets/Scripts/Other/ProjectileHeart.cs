using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHeart : MonoBehaviour
{

    public Vector2 targetDir;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.AddForce(targetDir * 20, ForceMode2D.Impulse);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<EntityHurtable>() != null)
        {
            collision.transform.GetComponent<EntityHurtable>().OnHit();
        }
        Destroy(gameObject);
    }

}
