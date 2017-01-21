using UnityEngine;
using UnityEngine.AI;

[Debuff]
public class KnockBackScript : TimeBasedEffect {

	private new Rigidbody rigidbody;
	private Vector3 force;
    private NavMeshAgent agent;

	public override void Awake () 
	{
		rigidbody = this.GetComponent<Rigidbody>();
        agent=this.GetComponent<NavMeshAgent>();
        
	}

    public override void RefreshEffect(Effect e, float actualEffectiveness)
    {
        base.RefreshEffect(e, actualEffectiveness);
        agent.Stop();
        force = -effectiveness * agent.velocity.normalized;
        this.rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public override void UnApply()
	{
		this.rigidbody.AddForce(-force, ForceMode.Impulse);
        if (agent.isActiveAndEnabled)
            agent.Resume();
        base.UnApply();
	}
		
}