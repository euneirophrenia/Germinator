using UnityEngine;
using System.Collections.Generic;
using Vexe.Runtime.Types;

public abstract class Hitter :BaseBehaviour {

	public List<Effect> effects;

    [PerItem, Tags]
    public string[] hits;

    protected bool destroy = false;
	private bool isTriggering=false;

    void OnTriggerEnter(Collider collision)
    {
		if (isTriggering || !CanHit(collision))
			return; 

		Hittable other = collision.GetComponent<Hittable>();
		if (other==null)
			return;


        isTriggering = true;
        other.Proc(effects);
          
        HandleHit(other); //per cose tipo consentire hit consecutivi  
        if (destroy)
        {
            //Destroy(this.gameObject);
            this.gameObject.Release();
        }  
    }

	protected virtual bool CanHit(Collider h)
	{
        if (h.gameObject.layer != 0)
        {
            return false;
        }

		foreach(string s in hits)
		{
			if (h.CompareTag(s))
				return true;
		}

		return false;
	}

	public void Update()
	{
		isTriggering=false;
	}


    public virtual void HandleHit(Hittable hit) //il multihit lo ovverrida
    {
        destroy = true;
    }

}
