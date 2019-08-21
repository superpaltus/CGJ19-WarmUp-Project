using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

#region Event
    public delegate void HealthEditHandler(int newValue);
    public static event HealthEditHandler OnHealthValueChanged;

    private void NotifyAboutHealthValueChanged()
    {
        if (OnHealthValueChanged != null)
            OnHealthValueChanged(health);
    }
#endregion

    public GameObject projectileHeart;

    float moveX, moveY;
    float lastMoveX, lastMoveY;
    bool moving;
    Vector2 nextMove;

    public static Player instance;

    public static int health = 5;


    public bool isDead = false;


    public float speed;
    public float timeUntilDash;


    Animator playerAnim;
    Rigidbody2D playerRigidbody;

    
    // ====IMPORTANT FUNCTIONS====

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Debug.Log("Multiple instances of player?");
        }

        for (int i = 0; i < health; i++)
        {
            NotifyAboutHealthValueChanged();
        }

        CheckForComponents();
    }

    void FixedUpdate()
    {
        RigidMove();
    }

    void Update()
    {
        CheckForAttack();
        CheckDebugControls();
        CheckForDash();
        UpdateMovement();
    }



    // ====OTHER FUNCTIONS====




    public void ChangeHealth(int _change)
    {
        if (health + _change > 0)
        {
            health--;
        } else
        {
            isDead = true;
        }
        NotifyAboutHealthValueChanged();
    }

    void CheckDebugControls()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            health++;
            NotifyAboutHealthValueChanged();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            health--;
            NotifyAboutHealthValueChanged();
        }
    }


    void CheckForComponents()
    {
        if (GetComponent<Animator>() != null)
        {
            playerAnim = GetComponent<Animator>();
            playerAnim.SetFloat("LastMoveX", 0);
            playerAnim.SetFloat("LastMoveY", -1);
        }
        else
        {
            Debug.Log("Player missing animator!");
        }

        if (GetComponent<Rigidbody2D>() != null)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.Log("Player missing Rigidbody2D!");
        }
    }

    public void Dash()
    {
        Debug.Log("Dashing!");
        Vector2 dashDir = new Vector2(lastMoveX, lastMoveY);
        dashDir = dashDir.normalized;

        

        playerRigidbody.AddForce(dashDir * speed * 150, ForceMode2D.Impulse);
        Instantiate(ParticleManager.instance.GetParticles("dash"), transform.position, Quaternion.identity);
        timeUntilDash = 1;
    }

    void CheckForAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 shootDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            shootDir = shootDir.normalized;

            ShootHeart(shootDir);
        }
    }

    void ShootHeart(Vector2 _shootDir)
    {
        GameObject heart = Instantiate(projectileHeart, transform.position, Quaternion.identity);
        heart.GetComponent<ProjectileHeart>().targetDir = _shootDir;
        ChangeHealth(-1);
    }
    
    void CheckForDash()
    {
        if (Input.GetButtonDown("Dash") && timeUntilDash <= 0)
        {
            Dash();
        }

        if(timeUntilDash > 0)
        {
            timeUntilDash -= Time.deltaTime;
        } else if(timeUntilDash < 0)
        {
            timeUntilDash = 0;
        }

    }

    // CALLED EVERY UPDATE TO CHECK FOR MOVEMENT
    void UpdateMovement()
    {
        if (moving)
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

        playerRigidbody.AddForce(nextMove * speed * 15f, ForceMode2D.Impulse);
    }

}
