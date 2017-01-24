using UnityEngine;
using System.Collections;

public abstract class TimeBasedScript : EffectScript {

    public float cooldown;
    public float activeCoolDown = 0;

    public int remainingTicks = int.MaxValue;
   
	// Update is called once per frame
	public virtual void Update () {

        if (activeCoolDown<=0)
        {
			this.Apply();
            activeCoolDown = cooldown;
            remainingTicks--;
			return;
        }
        else
        {
            activeCoolDown -= Time.deltaTime;
        } 
		if (remainingTicks <= 0)
			this.UnApply();

	}

    public virtual void OnDisable()
    {
        effectiveness = 0;
    }

    public virtual void RefreshEffect(TimeBasedEffect e, float actualEffectiveness)
    {
        if (actualEffectiveness > effectiveness)
            effectiveness = actualEffectiveness;
        remainingTicks = (e.Ticks <= 0 ? int.MaxValue : e.Ticks);
        cooldown = e.Cooldown;
        activeCoolDown = 0; //nuovo tick al refresh, mi pare sensato
        this.enabled = true;

    }
}
