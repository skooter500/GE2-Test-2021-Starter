using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody ballRb;

    public float throwForce;
    
    // Start is called before the first frame update
    void Start()
    {
        ballRb = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Throw ball");
            Throw();
        }
            
    }

    public void Throw()
    {
        ballRb.velocity = Vector3.zero;//cancel velocity
        ball.transform.position = this.transform.position;
        ballRb.AddForce(transform.forward*throwForce,ForceMode.Impulse);//add an impulse force to throw

    }
}
