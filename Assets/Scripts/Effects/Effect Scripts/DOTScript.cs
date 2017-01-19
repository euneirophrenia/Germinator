[Debuff]
public class DOTScript : TimeBasedEffect {

	public override void Apply()
	{
		target.AddToHp(-(int)effectiveness);
	}

}
