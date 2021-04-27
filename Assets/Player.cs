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

    bool haveBall = true;

    public Seek seek;
    public Arrive arrive;
    public FollowPath followPath;

    GameObject Go;

    public bool returned;

    //public State[] states;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (haveBall)
        {
            if (Input.GetKey(KeyCode.Space) && !overHeat)
            {
                power += 0.5f * Time.deltaTime;
                if (power > 3)
                {
                    haveBall = false;
                    if (power > 1) Throw(power);
                    power = 1;
                    overHeat = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                haveBall = false;
                overHeat = false;
                if (power > 1) Throw(power);
                power = 1;
            }



            image.fillAmount = (power - 1) / 2;

        }

        if (Go == null || !returned) return;
        if(Vector3.Distance(Go.transform.position, transform.position) < 5f)
        {
            Destroy(Go);
            returned = false;
            haveBall = true;
        }


    }

    IEnumerator AntiSoftLock()
    {
        yield return new WaitForSeconds(10);

        Wait();

    }




    void Throw(float pow)
    {
        haveBall = false;
        Go = Instantiate(ball, transform.position, transform.rotation);
        Go.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force*pow, ForceMode.Impulse);
        StartCoroutine(AntiSoftLock());

        //if(stateMachine.currentState == null)
        //{
        //    stateMachine.ChangeState(states[0]);
        //}
        seek.GetBall(Go);

    }

    public void Return()
    {
        seek.enabled = false;
        arrive.enabled = true;

        
    }

    public void Wait()
    {
        StopAllCoroutines();
        returned = true;
        seek.enabled = true;
        arrive.enabled = false;
        seek.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //seek.GetBall(gameObject);
    }
}
