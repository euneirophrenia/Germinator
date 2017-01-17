using UnityEngine.AI;

[Debuff]
public class SlowScript : TimeBasedEffect {

    //essenzialmente eredita solo per usufruire dei servizi e non doverli duplicare 
    private NavMeshAgent agent;

    void Start()
    {
		agent=this.gameObject.GetComponent<NavMeshAgent>();
		if (agent!= null)
			agent.speed /= effectiveness;
    }

    public override void UnApply()
    {
		agent.speed *= effectiveness;
        Destroy(this);
    }

}
