using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Arrive : SteeringBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public float slowingDistance = 15.0f;

    public GameObject targetGameObject = null;
    public Transform hold;

    public bool dropped = false;

    public override Vector3 Calculate()
    {
        return boid.ArriveForce(targetPosition, slowingDistance);
    }

    public void Update()
    {
        if (targetGameObject != null)
        {
            targetPosition = new Vector3(targetGameObject.transform.position.x, 0, targetGameObject.transform.position.z);
        }


        if(Vector3.Distance(targetPosition, transform.position) < 10f)
        {
            Drop();
            
        }
    }

    void Drop()
    {
        dropped = true;
        GameObject ball = hold.GetChild(0).gameObject;
        ball.transform.SetParent(null);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<BoxCollider>().enabled = true;
        ball.GetComponent<Rigidbody>().useGravity = true;


        targetGameObject.GetComponent<Player>().Wait();
    }
}