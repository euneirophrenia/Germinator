using UnityEngine;
using System.Collections;
using System;

[Debuff]
public class SlowScript : TimeBasedAbility {

    //essenzialmente eredita solo per usufruire dei servizi e non doverli duplicare 

    void Start()
    {
		Movement movement=this.gameObject.GetComponent<Movement>();
		if (movement!= null && effectiveness!=0)
			movement.speed /= effectiveness;
    }

    public override void UnApply()
    {
		if (effectiveness!=0)
        	this.gameObject.GetComponent<Movement>().speed *= effectiveness;
        Destroy(this);
    }

}
