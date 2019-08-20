using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public GameObject player;
    public EnemyAI enemy;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {

        }
    }
}
