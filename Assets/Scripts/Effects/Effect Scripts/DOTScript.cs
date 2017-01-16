[Debuff]
public class DOTScript : TimeBasedAbility {

	private Hittable hittable;

	void Start() 
	{
		hittable = this.gameObject.GetComponent<Hittable>(); //solo per efficienza e non dover ricercare a ogni tick
	}

	public override bool Apply()
	{
		hittable.AddToHp(-(int)effectiveness);
        return true;
	}

}
