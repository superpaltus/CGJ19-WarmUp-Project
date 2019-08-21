using System;
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

    private void Start()
    {
        Player.OnHealthValueChanged += Player_OnHealthValueChanged;
    }

    void Update()
    {
        rotationAmount += Time.deltaTime;

        if (currentHearts.Count > 0)
        {
            MoveHearts();
        }
    }

    private void MoveHearts()
    {
        for (int i = 0; i < currentHearts.Count; i++)
        {
            currentHearts[i].transform.position = new Vector3(Player.instance.transform.position.x + (Mathf.Cos(spacing * i + rotationAmount) * radius), Player.instance.transform.position.y + Mathf.Sin((spacing * i + rotationAmount) * radius) / 1.5f, 0.0f);
        }
    }

    private void Player_OnHealthValueChanged(int newValue)
    {
        if (currentHearts.Count < newValue)
        {
            GameObject tempHeart = Instantiate(crystalHeart, transform.position, Quaternion.identity, transform);
            currentHearts.Add(tempHeart);
            // tempHeart.GetComponent
            spacing = 2 * Mathf.PI / currentHearts.Count;
        }
        else if (currentHearts.Count > newValue)
        {
            Destroy(currentHearts[0]);
            currentHearts.RemoveAt(0);
            spacing = 2 * Mathf.PI / currentHearts.Count;
        }
    }

    private void OnDisable()
    {
        Player.OnHealthValueChanged -= Player_OnHealthValueChanged;
    }
}
