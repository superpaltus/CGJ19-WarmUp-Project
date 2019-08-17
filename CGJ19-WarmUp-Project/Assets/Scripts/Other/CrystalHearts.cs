using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHearts : MonoBehaviour
{
    public float radius;
    public float speed;
    static float angle = 0.0f;

    public GameObject heartPrefab;
    public GameObject player;

    static public List<GameObject> hearts = new List<GameObject>();


    void Start()
    {
        hearts.Add(Instantiate(heartPrefab, heartPrefab.transform.position, Quaternion.identity, player.transform));
        hearts.Add(Instantiate(heartPrefab, heartPrefab.transform.position, Quaternion.identity, player.transform));
        hearts.Add(Instantiate(heartPrefab, heartPrefab.transform.position, Quaternion.identity, player.transform));
        hearts.Add(Instantiate(heartPrefab, heartPrefab.transform.position, Quaternion.identity, player.transform));
    }
    

    void Update()
    {
        float increment = 0.05f;


        angle += speed * Time.deltaTime;


        foreach (GameObject heart in hearts)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 heartPos = heart.transform.position;
            heart.transform.position = new Vector3(playerPos.x + Mathf.Cos(angle + increment) * radius, playerPos.y + Mathf.Sin(angle + increment) * radius, 0.0f);
            increment += 0.5f;
        }
    }
}
