using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    public float weight = 1.0f;
    public Vector3 force;

    [HideInInspector]
    public Boid boid;

    bool run;


    public void Awake()
    {
        boid = GetComponent<Boid>();
    }

    //public void Exit()
    //{
    //    run = false;
    //}

    //public void Enter()
    //{
    //    run = true;
    //}

    public abstract Vector3 Calculate();
}
