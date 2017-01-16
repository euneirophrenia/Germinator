using UnityEngine;
using System.Diagnostics;

public class FireProjectile : TimeBasedAbility
{
    public GameObject projectile;
    public float radiusOfInfluence;

    public override bool Apply()
    {
        GameObject target = GetTarget();
        if (target!=null)
        {
            GameObject bullet = Instantiate(projectile, this.transform.position,Quaternion.identity);
            bullet.GetComponent<Movement>().CurrentTarget = target;
            return true;
        }
        return false;
    }

    [Conditional("UNITY_EDITOR")]
	void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(transform.position, radiusOfInfluence);
    }

    protected virtual GameObject GetTarget()
    {
        Collider[] gos = Physics.OverlapSphere(this.transform.position, radiusOfInfluence);
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].gameObject.GetComponent<Hittable>()!=null)
            {
                return gos[i].gameObject;
            }
        }

        return null;
    }
}
