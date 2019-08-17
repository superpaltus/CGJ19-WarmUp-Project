using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHearts : MonoBehaviour
{
    public float radius;
    float angle = 0.0f;

    public GameObject heartPrefab;
    public GameObject player;
    static GameObject heart;

    List<GameObject> hearts;

    void Start()
    {

        heart = Instantiate(heartPrefab, heartPrefab.transform.position, Quaternion.identity, player.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += 2.0f * Time.deltaTime;

        Vector3 playerPos = player.transform.position;
        Vector3 heartPos = heart.transform.position;
        heart.transform.position = new Vector3(playerPos.x + Mathf.Cos(angle) * radius, playerPos.y + Mathf.Sin(angle) * radius, 0.0f);
    }
}
