using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject ballPrefab;

    public int force;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrowBall());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator ThrowBall()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
                ball.GetComponent<Rigidbody>().velocity = Vector3.forward * force;
                yield return new WaitForSeconds(2);
            }

            yield return null;
        }
    }
}
