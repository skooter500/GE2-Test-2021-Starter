using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public Transform player;
    public int playerDistance = 10;
    
    public bool isCarryingBall = false;
 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new ArriveToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class ArriveToPlayer : State
{
    private DogController dog;
    public override void Enter()
    {
        dog = owner.GetComponent<DogController>();
        owner.GetComponent<Arrive>().enabled = true;
        
    }

    public override void Think()
    {
        Vector3 toPlayer = owner.transform.position - dog.player.position;
        toPlayer.Normalize();
        Vector3 targetPos = new Vector3(dog.player.position.x + toPlayer.x * dog.playerDistance,0,dog.player.position.z + toPlayer.z * dog.playerDistance);
        owner.GetComponent<Arrive>().targetPosition = targetPos;
        if (Vector3.Distance(owner.transform.position, targetPos) < 1.0f)
        {
            if (dog.isCarryingBall)
            {
                dog.isCarryingBall = false;
                GameObject ball = dog.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
                ball.transform.parent = null;
                ball.GetComponent<Rigidbody>().useGravity = true;
            }
            owner.ChangeState(new LookAtPlayer());
        }
        if (GameObject.FindWithTag("ball"))
        {
            owner.ChangeState(new FetchBall());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Boid>().velocity = Vector3.zero;
        owner.GetComponent<Arrive>().enabled = false;

    }
}

class LookAtPlayer : State
{
    private DogController dog;
    private GameObject dogHead;
    public override void Enter()
    {
        dog = owner.GetComponent<DogController>();
        dogHead = dog.gameObject.transform.Find("head").gameObject;
    }

    public override void Think()
    {
        int rotationSpeed = 2;
        Vector3 toPlayer = dog.player.position - dogHead.transform.position;
        Quaternion lookAtPlayer = Quaternion.LookRotation(toPlayer);
        dogHead.transform.rotation =
            Quaternion.Slerp(dogHead.transform.rotation, lookAtPlayer, rotationSpeed * Time.deltaTime);
        if (Vector3.Distance(owner.transform.position, dog.player.position) > 15.0f)
        {
            owner.ChangeState(new ArriveToPlayer());
        }

        if (GameObject.FindWithTag("ball"))
        {
            owner.ChangeState(new FetchBall());
        }
    }

    public override void Exit()
    {
       
    }
}

class FetchBall : State
{
    private DogController dog;
    private GameObject ball;
    private GameObject dogAttachParent;
    public override void Enter()
    {
        owner.GetComponent<AudioSource>().Play();
        dog = owner.GetComponent<DogController>();
        owner.GetComponent<Arrive>().enabled = true;
        owner.GetComponent<Arrive>().targetGameObject = GameObject.FindWithTag("ball");
        dogAttachParent = dog.gameObject.transform.Find("dog").gameObject;
        ball = owner.GetComponent<Arrive>().targetGameObject;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, ball.transform.position) < 2f)
        {
            Transform ballAttach = dogAttachParent.transform.GetChild(0);
            ball.GetComponent<Rigidbody>().useGravity = false; 
            ball.transform.parent = ballAttach;
            ball.transform.localPosition = Vector3.zero;
            ball.gameObject.tag = "Untagged";
            
        }
        if(ball.tag == "Untagged")
            owner.ChangeState(new ArriveToPlayer());
    }

    public override void Exit()
    {
        dog.isCarryingBall = true;
        owner.GetComponent<Arrive>().targetGameObject = null;
        owner.GetComponent<Boid>().velocity = Vector3.zero;
    }
}

