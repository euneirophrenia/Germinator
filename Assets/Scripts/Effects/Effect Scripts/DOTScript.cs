[Debuff]
public class DOTScript : TimeBasedScript {

	public override void Apply()
	{
		target.AddToHp(-(int)effectiveness);
	}

}
