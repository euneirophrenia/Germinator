using UnityEngine;
using System.Collections;
using System;

public class SlowEffectScript : TimeBasedAbility {

    //essenzialmente eredita solo per usufruire dei servizi e non doverli duplicare 

    void Start()
    {
        this.gameObject.GetComponent<Movement>().speed *= effectiveness;
    }

    public override void Apply()
    {
       //non serve a meno che non si voglia calcolare dinamicamente uno slow che cambia nel tempo
    }

    public override void UnApply()
    {
        this.gameObject.GetComponent<Movement>().speed /= effectiveness;
        Destroy(this);
    }

}
