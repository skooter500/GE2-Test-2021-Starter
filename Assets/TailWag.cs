using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWag : MonoBehaviour
{
    public float choseAngle;
    private float angle;
    public float rotationSpeedFactor;
    private float rotationSpeed;
    private Boid dog;
    // Start is called before the first frame update
    void Start()
    {
        dog = transform.parent.GetComponent<Boid>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationSpeed = Mathf.Abs(dog.velocity.z * rotationSpeedFactor);
        Debug.Log(dog.velocity.z + " is Velocity");
        Debug.Log(rotationSpeed + " is Speed");
        float angle = Mathf.PingPong(Time.time * rotationSpeed, choseAngle * 2) - choseAngle;
        transform.localRotation = Quaternion.AngleAxis(angle,Vector3.up);
    }
}
