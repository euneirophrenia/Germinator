[Debuff]
public class DOTScript : TickingEffectScript {

	public override void Apply()
	{
		target.AddToHp(-(int)effectiveness);
	}

}
