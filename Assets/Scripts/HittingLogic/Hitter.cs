using UnityEngine;
using System.Collections.Generic;

public abstract class Hitter : MonoBehaviour {

	public List<Effect> effects;

    public List<string> hits;

    protected bool destroy = false;


    void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.GetComponent<Hittable>() == null)
        //    return;
		IEnumerable<Hittable> targets = getTargets(collision);
       
		foreach (Hittable other in targets)
		{
            if (other!= null && hits.Contains(other.tag))
            {
                other.Proc(effects);
                HandleHit(collision); //per cose tipo consentire hit consecutivi  
            }
        }
        if (destroy)
        {
            Destroy(this.gameObject);
        }    

    }

	public abstract IEnumerable<Hittable> getTargets(Collider collision);

    public virtual void HandleHit(Collider coll) //il multihit lo ovverrida
    {
        destroy = true;
    }

}
