using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHeartManager : MonoBehaviour
{
    public List<GameObject> currentHearts = new List<GameObject>();
    public GameObject crystalHeart;

    public float radius;
    public float rotationAmount;

    public float spacing;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationAmount += Time.deltaTime;

        if(currentHearts.Count < Player.health)
        {
            GameObject tempHeart = Instantiate(crystalHeart, transform.position, Quaternion.identity, transform);
            currentHearts.Add(tempHeart);
           // tempHeart.GetComponent
        } else if(currentHearts.Count > Player.health)
        {
            Destroy(currentHearts[0]);
            currentHearts.RemoveAt(0);
        }
        if(currentHearts.Count > 0)
        {
            spacing = 2 * Mathf.PI / currentHearts.Count;
            for (int i = 0; i < currentHearts.Count; i++)
            {
                currentHearts[i].transform.position = new Vector3(Player.instance.transform.position.x + (Mathf.Cos(spacing * i + rotationAmount) * radius), Player.instance.transform.position.y + Mathf.Sin((spacing * i + rotationAmount) * radius)/1.5f, 0.0f);
            }
        }
        
        
    }
}
