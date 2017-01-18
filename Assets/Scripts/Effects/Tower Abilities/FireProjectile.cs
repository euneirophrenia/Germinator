using UnityEngine;

public class FireProjectile : TowerAbility
{
    public GameObject projectile;
	private GameObject bullet;

    private Collider[] gos;

    private PrefabPool bulletPool;
    private Transform cached;

    public void Start()
    {
        bulletPool = new PrefabPool(projectile, 3, dynamic : false); 
        cached = this.transform;
    }

    public override bool Do()
    {
        GameObject target = GetTarget();
        if (target!=null)
        {
            //bullet = Instantiate(projectile, this.transform.position,Quaternion.identity);
            bullet = bulletPool.Get();
            bullet.transform.position = cached.position;
            bullet.GetComponent<Movement>().CurrentTarget = target;
            return true;
        }
        return false;
	}

    protected virtual GameObject GetTarget()
    {
        gos = Physics.OverlapSphere(this.transform.position, radius);
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
