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

    public override Vector3 Calculate()
    {
        return boid.ArriveForce(targetPosition, slowingDistance);
    }

    public void Update()
    {
        if (targetGameObject != null)
        {
            targetPosition = new Vector3(targetGameObject.transform.position.x, transform.position.y, targetGameObject.transform.position.z);
        }


        if(Vector3.Distance(targetPosition, transform.position) < 4.5f)
        {
            Drop();
            
        }
    }

    void Drop()
    {
        GameObject ball = hold.GetChild(0).gameObject;
        ball.transform.SetParent(null);
        ball.GetComponent<BoxCollider>().enabled = true;
        ball.GetComponent<Rigidbody>().useGravity = true;


        targetGameObject.GetComponent<Player>().Wait();
    }
}