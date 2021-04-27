using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public GameObject targetGameObject = null;

    public Vector3 target = Vector3.zero;

    public Transform hold;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (targetGameObject != null)
            {
                target = targetGameObject.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }

    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);
    }

    public void Update()
    {
        
        if (targetGameObject != null)
        {
            target = new Vector3(targetGameObject.transform.position.x, transform.position.y, targetGameObject.transform.position.z);
        }
    }

    public void GetBall(GameObject ball)
    {
        targetGameObject = ball;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "ball")
        {
            TakeBall();
        }
    }

    void TakeBall()
    {
        targetGameObject.transform.SetParent(hold);
        targetGameObject.transform.position = hold.position;
    }
}