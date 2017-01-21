using UnityEngine;

public class DamageScript : EffectScript
{
    public void OnEnable()
    {
        target.AddToHp(-(int)effectiveness);
        base.UnApply();
    }
	
}
