using UnityEngine;
using System.Collections;
using System;

public class DamageEffectScript : EffectScript {

    void Start () {
        Hittable h = this.gameObject.GetComponent<Hittable>();
        if (h!=null)
        {
            h.addToHp(-(int)effectiveness);
        }
        Destroy(this); //o magari prima fai vedere qualche animazioncina
	}
	
}
