using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Texture2D cursorTexture;
    public Vector2 hotspot;

    //Enemy things
    private List<EnemyAI> enemies;
    public bool enemiesMoving = false;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(hotspot.x, hotspot.y), CursorMode.ForceSoftware);
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        enemies = new List<EnemyAI>();
        enemies.Clear();
    }

    void Update()
    {
        if(!enemiesMoving)
            StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(EnemyAI script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Move();
            Debug.Log("MoveEnemy();!");
            yield return new WaitForSeconds(0.1f);
        }

    }

}
