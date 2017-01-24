using UnityEngine;
using System.Collections.Generic;

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
            //Destroy(this.gameObject);
            this.gameObject.Release();
        }  
    }

	protected virtual bool CanHit(Hittable h)
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
