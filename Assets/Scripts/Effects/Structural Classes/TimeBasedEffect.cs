using UnityEngine;
using System.Collections;

public abstract class TimeBasedEffect : EffectScript {

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

    public override void RefreshEffect(Effect e, float actualEffectiveness)
    {
        effectiveness = actualEffectiveness;
        int attempt = remainingTicks + e.Ticks;
        remainingTicks = (attempt > 0 ? attempt : e.Ticks);
        cooldown = e.Cooldown;
        activeCoolDown = 0; //nuovo tick al refresh, mi pare sensato

    }
}
