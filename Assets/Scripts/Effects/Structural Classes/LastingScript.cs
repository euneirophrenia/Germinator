using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LastingScript : EffectScript {

    public float duration;

	// Use this for initialization
	void Start () {
	
	}

    public virtual void Update()
    {

        duration -= Time.deltaTime;
        if (duration <= 0)
            this.UnApply();
    }

    public virtual void Refresh(LastingEffect e, float actual, float durationModifier)
    {
        this.duration = e.duration*durationModifier;
        effectiveness = actual;
        this.Apply();
        this.enabled = true;
    }

}
