using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWag : MonoBehaviour
{
    public float choseAngle;
    private float angle;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.PingPong(Time.time * rotationSpeed, choseAngle * 2) - choseAngle;
        transform.localRotation = Quaternion.AngleAxis(angle,Vector3.up);
        Debug.Log(angle);
    }
}
