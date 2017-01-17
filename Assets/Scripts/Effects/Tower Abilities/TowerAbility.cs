using UnityEngine;
using System.Diagnostics;

public abstract class TowerAbility : Ability {

	public float radius;

	[Conditional("UNITY_EDITOR")]
	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 1, 0, 0.55F);
		Gizmos.DrawSphere(transform.position, radius);
	}


}
