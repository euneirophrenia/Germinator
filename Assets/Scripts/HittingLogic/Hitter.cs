using UnityEngine;
using System.Collections.Generic;
//using System.Runtime.CompilerServices;

public abstract class Hitter : MonoBehaviour {

	public List<Effect> effects;

    public List<string> hits;

    protected bool destroy = false;
	private bool isTriggering=false;


	//TODO trova una soluzione non temporanea
	//[MethodImpl(MethodImplOptions.Synchronized)]
    void OnTriggerStay(Collider collision)
    {
		if (isTriggering)
			return; 
		isTriggering=true;

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

	public void Update()
	{
		isTriggering=false;
	}

	public abstract IEnumerable<Hittable> getTargets(Collider collision);

    public virtual void HandleHit(Collider coll) //il multihit lo ovverrida
    {
        destroy = true;
    }

}
