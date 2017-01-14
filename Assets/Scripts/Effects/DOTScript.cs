using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Debuff]
public class DOTScript : TimeBasedAbility {

	private Hittable hittable;

	void Start() 
	{
		hittable = this.gameObject.GetComponent<Hittable>(); //solo per efficienza e non dover ricercare a ogni tick
	}

	public override void Apply()
	{
		hittable.AddToHp(-(int)effectiveness);
	}

}
