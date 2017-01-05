using UnityEngine;
using System.Collections;
using System;

public class DamageScript : EffectScript {

    void Start () 
	{
		Hittable h = this.gameObject.GetComponent<Hittable>();
		if (h!=null)
			h.addToHp(-(int)effectiveness);
		
        Destroy(this); 
	}

	//Per proccare istanze multiple di danno sullo stesso proiettile 
	//Altrimenti si sarebbe proccata solo la prima
	public override void RefreshEffect(Effect e, float actualEffectiveness)
	{
		effectiveness=actualEffectiveness;
		Start();
	}
	
}
