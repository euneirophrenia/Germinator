using UnityEngine;
using System.Diagnostics;

public class AOEHitter : Hitter {

    public float radius=100;

	public override void HandleHit(Hittable hit)
	{
        Collider[] others= Physics.OverlapSphere(hit.transform.position, radius, CustomLayerEnum.Enemy.ToMask());
        
		foreach (Collider other in others)
		{
            UnityEngine.Debug.Log(other.transform.name);
            if (CanHit(other))
            {
                Hittable h = other.GetComponent<Hittable>();
                if (h != null && h != hit)
                {
                    h.Proc(effects);
                }
            }
		}
		destroy=true;
	}

    [Conditional("UNITY_EDITOR")]
	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 1, 0, 0.75F);
		Gizmos.DrawSphere (transform.position, radius);
	}

}
