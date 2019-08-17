using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public Particle[] particles;



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

   
    public GameObject GetParticles(string _name) {
        return Array.Find<Particle>(particles, item => item.identifier == _name).emitter;
    }
}

[Serializable]
public class Particle
{
    public string identifier;
    public GameObject emitter;
}