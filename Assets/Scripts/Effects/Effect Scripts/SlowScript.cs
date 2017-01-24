using UnityEngine;
using UnityEngine.AI;

[Debuff]
public class SlowScript : TimeBasedScript {

    private NavMeshAgent agent;

    public override void Awake()
    {
		agent=this.gameObject.GetComponent<NavMeshAgent>();
        effectiveness = 1;
    }

    public override void OnDisable()
    {
        effectiveness = 1;
    }

    public override void RefreshEffect(TimeBasedEffect e, float actualEffectiveness)
    {
        if (actualEffectiveness > effectiveness)
        {
            agent.speed *= effectiveness;
            agent.speed /= actualEffectiveness;
        }
        base.RefreshEffect(e, actualEffectiveness);

    }

    public override void UnApply()
    {
		agent.speed *= effectiveness;
        base.UnApply();
    }

}
