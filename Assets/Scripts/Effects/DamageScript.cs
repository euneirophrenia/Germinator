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
	
}
