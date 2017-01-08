using UnityEngine;
using System.Collections.Generic;

public abstract class Hitter : MonoBehaviour {

	public List<Effect> effects;

    public List<string> hits;

    protected bool destroy = false;
	private bool isTriggering=false;

    void OnTriggerEnter(Collider collision)
    {
		if (isTriggering)
			return; 
		isTriggering=true;

		Hittable other = collision.GetComponent<Hittable>();

		if (other==null || !hits.Contains(other.tag))
			return;

		other.Proc(effects);
          
        HandleHit(other); //per cose tipo consentire hit consecutivi  

        if (destroy)
        {
			Destroy(this.gameObject);
        }  
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
