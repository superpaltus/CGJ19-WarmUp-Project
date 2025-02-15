﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPSOnFinish : MonoBehaviour
{
 
    private ParticleSystem ps;


    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

}
