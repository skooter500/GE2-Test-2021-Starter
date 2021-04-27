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

    public Player player;

    public bool center;

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
            target = new Vector3(targetGameObject.transform.position.x, 0, targetGameObject.transform.position.z);
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
            if(player.CheckDrop()) TakeBall();
        }
    }

    void TakeBall()
    {
        center = true;
        targetGameObject.transform.SetParent(hold,true);
        targetGameObject.transform.position = hold.position;
        targetGameObject.GetComponent<Rigidbody>().useGravity = false;
        targetGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        targetGameObject.GetComponent<BoxCollider>().enabled = false;

        player.Return();

        //targetGameObject.transform.rotation = Quaternion.identity;
        //targetGameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}