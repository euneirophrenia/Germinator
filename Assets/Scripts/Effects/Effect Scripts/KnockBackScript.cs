using UnityEngine;
using UnityEngine.AI;

[Debuff]
public class KnockBackScript : TimeBasedAbility {

	private new Rigidbody rigidbody;
	private Vector3 force;
    private NavMeshAgent agent;

	void Start () 
	{
		rigidbody = this.GetComponent<Rigidbody>();
        agent=this.GetComponent<NavMeshAgent>();
        agent.Stop();
        force = -effectiveness * agent.velocity.normalized;
		this.rigidbody.AddForce(force, ForceMode.Impulse);
	}

	public override void UnApply()
	{
		this.rigidbody.AddForce(-force, ForceMode.Impulse);
        agent.Resume();
		Destroy(this);
	}
		
}