using UnityEngine;
using System.Collections;

public abstract class TimeBasedAbility : EffectScript {

    public float cooldown;
    public float activeCoolDown = 0;

    public int remainingTicks = int.MaxValue;
   
   
	// Update is called once per frame
	void Update () {
        if (activeCoolDown<=0)
        {
            this.Apply();
            activeCoolDown = cooldown;
            remainingTicks--;
        }
        else
        {
            activeCoolDown -= Time.deltaTime;
        }

        if (remainingTicks <= 0)
            this.UnApply();
          

	}

    public abstract void UnApply();
    public abstract void Apply();

    public override void RefreshEffect(Effect e, float actualEffectiveness)
    {
        effectiveness = actualEffectiveness;
        int attempt = remainingTicks + e.ticks;
        remainingTicks = (attempt > 0 ? attempt : e.ticks);
        cooldown = e.cooldown;
        activeCoolDown = 0;

    }
}
