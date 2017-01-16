using System;
using UnityEngine;
using UnityEngine.AI;

//ignorare per ora questa classe

public class Mover : Movement {

    public GameObject target;
    private NavMeshAgent agent;

    public override GameObject CurrentTarget
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
            agent.SetDestination(target.transform.position);
        }
    }

    public override Vector3 Direction
    {
        get
        {
            return agent.velocity.normalized;
        }
    }


    // Use this for initialization
    void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
	}

    
}
