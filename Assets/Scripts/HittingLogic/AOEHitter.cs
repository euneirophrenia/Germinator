using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class AOEHitter : Hitter {

    public float radius=100;


	public override IEnumerable<Hittable> getTargets(Collider collision)
    {
		return from x in Physics.OverlapSphere(collision.transform.position, radius) 
				where x.gameObject.GetComponent<Hittable>()!=null 
			select x.gameObject.GetComponent<Hittable>();
    }

	[Conditional("UNITY_EDITOR")]
	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 1, 0, 0.75F);
		Gizmos.DrawSphere (transform.position, radius);
	}

}
