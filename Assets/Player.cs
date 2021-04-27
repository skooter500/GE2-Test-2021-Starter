using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{


    float power = 1;

    public GameObject ball;
    public float force;

    bool overHeat;

    public Image image;

    //public StateMachine stateMachine;


    public Seek seek;
    public Arrive arrive;
    public FollowPath followPath;


    //public State[] states;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)&&!overHeat)
        {
            power += 0.5f * Time.deltaTime;
            if(power > 3)
            {
                if (power > 1) Throw(power);
                power = 1;
                overHeat = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            overHeat = false;
            if (power > 1) Throw(power);
            power = 1;
        }


        image.fillAmount = (power - 1) / 2;
    }




    void Throw(float pow)
    {
        GameObject Go = Instantiate(ball, transform.position, transform.rotation);
        Go.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force*pow, ForceMode.Impulse);

        //if(stateMachine.currentState == null)
        //{
        //    stateMachine.ChangeState(states[0]);
        //}
        seek.GetBall(Go);

    }
}
