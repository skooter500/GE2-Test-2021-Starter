using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tail : MonoBehaviour
{

    float rot;
    public Transform tailTrans;
    public Rigidbody rb;

    bool left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (left)
        {
            rot += 1f*rb.velocity.magnitude;
            if (rot > 35) left = false;
        }
        else
        {
            rot -= 1f * rb.velocity.magnitude;
            if (rot < -35) left = true;
        }



        tailTrans.localRotation = Quaternion.Euler(0,rot,0);
    }
}
