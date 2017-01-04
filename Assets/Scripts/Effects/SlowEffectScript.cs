using UnityEngine;
using System.Collections;
using System;

[Debuff]
public class SlowEffectScript : TimeBasedAbility {

    //essenzialmente eredita solo per usufruire dei servizi e non doverli duplicare 

    void Start()
    {
		Movement movement=this.gameObject.GetComponent<Movement>();
		if (movement!= null)
			movement.speed /= effectiveness;
    }

    public override void UnApply()
    {
        this.gameObject.GetComponent<Movement>().speed *= effectiveness;
        Destroy(this);
    }

}
