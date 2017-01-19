using UnityEngine;

public class DamageScript : EffectScript {

    public override void Start () 
	{
        base.Start();
		target.AddToHp(-(int)effectiveness);
        base.UnApply();
	}

	//Per proccare istanze multiple di danno sullo stesso proiettile 
	//Altrimenti si sarebbe proccata solo la prima
	public override void RefreshEffect(Effect e, float actualEffectiveness)
	{
		effectiveness=actualEffectiveness;
		Start();
	}
	
}
