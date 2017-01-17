using UnityEngine;
using System.Diagnostics;

public abstract class TowerAbility : MonoBehaviour {

	public float radius;
	public float timeBetweenActivations;

	private float activeCoolDown=0;


	public abstract bool Do();

	public virtual void Update () {

		if (activeCoolDown<=0 && Do())
		{
			activeCoolDown=timeBetweenActivations;
		}
		else
		{
			activeCoolDown -= Time.deltaTime;
		}
			
		
	}

	[Conditional("UNITY_EDITOR")]
	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 1, 0, 0.55F);
		Gizmos.DrawSphere(transform.position, radius);
	}


}
