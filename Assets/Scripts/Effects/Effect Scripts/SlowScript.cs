using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[Debuff]
public class SlowScript : LastingScript {

    private NavMeshAgent agent;

    public override void Awake()
    {
		agent=this.gameObject.GetComponent<NavMeshAgent>();
        effectiveness = 1;
    }

    public override void Refresh(LastingEffect e, float actualEffectiveness, float duration)
    {
        if (this.isActiveAndEnabled)
            agent.speed *= effectiveness;
        agent.speed /= actualEffectiveness;
        base.Refresh(e, actualEffectiveness, duration);
    }

    public override void UnApply()
    {
		agent.speed *= effectiveness;
        base.UnApply();
    }

}
