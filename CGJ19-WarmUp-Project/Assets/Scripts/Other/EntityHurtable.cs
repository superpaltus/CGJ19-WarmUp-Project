using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHurtable : MonoBehaviour
{
    public float flashTimer = 0.5f;


    public int health = 2;
    public int maxHealth = 2;

    public virtual void OnHit()
    {
        flashTimer = 0;
        ChangeHealth(-1);
    }

    public virtual void Update()
    {
        if(flashTimer < 0.5)
        {
            flashTimer += Time.deltaTime;
        }
        if(flashTimer < 0.1)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);
        } else if (flashTimer > 0.1 && flashTimer < 0.2)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
        } else if (flashTimer > 0.2 && flashTimer < 0.3)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);
        }
        else if (flashTimer > 0.3 && flashTimer < 0.4)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
        }

    }
    
    public virtual void ChangeHealth(int _change)
    {
        if (health + _change > 0)
        {
            health--;
        } else if(health + _change > maxHealth)
        {
            health = maxHealth;
        } else
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        Instantiate(ParticleManager.instance.GetParticles("death"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
