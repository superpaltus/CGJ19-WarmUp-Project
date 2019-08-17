using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Texture2D cursorTexture;
    public Vector2 hotspot;


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
    }

}
