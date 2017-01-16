using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Hitter : MonoBehaviour {

	public List<Effect> effects;

    public string[] hits;

    protected bool destroy = false;
	private bool isTriggering=false;

    void OnTriggerEnter(Collider collision)
    {
		if (isTriggering)
			return; 
		isTriggering=true;

		Hittable other = collision.GetComponent<Hittable>();

		if (other==null || !CanHit(other))
			return;

		other.Proc(effects);
          
        HandleHit(other); //per cose tipo consentire hit consecutivi  
        if (destroy)
        {
			Destroy(this.gameObject);
        }  
    }

	protected bool CanHit(Hittable h)
	{
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
