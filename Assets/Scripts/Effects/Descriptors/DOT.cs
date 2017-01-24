using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName="DoT", menuName="Effect/DoT")]
public class DOT : TimeBasedEffect {

	public DOT(float effect, int ticks, float cooldown=1)  : base() //dura cooldown*ticks secondi
	{
		init(effect, ticks, cooldown);
	}


	public void init(float effect, int ticks, float cooldown=1) 
	{
		this.Cooldown = cooldown;
		this.Ticks = ticks;
		this.Effectiveness = effect;
	}
}
