using UnityEngine;

public class DamageScript : EffectScript {

    void Start () 
	{
		Hittable[] h = this.GetComponentsInChildren<Hittable>();
		if (h.Length!=0)
			h[h.Length-1].AddToHp(-(int)effectiveness);
		
        Destroy(this); 
	}

	//Per proccare istanze multiple di danno sullo stesso proiettile 
	//Altrimenti si sarebbe proccata solo la prima
	public override void RefreshEffect(Effect e, float actualEffectiveness)
	{
		effectiveness=actualEffectiveness;
		Start();
	}
	
}
