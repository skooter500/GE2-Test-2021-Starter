using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public Transform player;
    public int playerDistance = 10;
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
    }

    public override void Exit()
    {
        base.Exit();
    }
}
