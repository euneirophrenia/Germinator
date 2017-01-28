using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName="Slow", menuName="Effect/Slow")]
public class Slow : LastingEffect
{
	
	public Slow(float effect, float duration)  : base() //dura cooldown*ticks secondi
	{
		init(effect, duration);
	}

	public void init(float effect, float duration) 
	{
        this.duration = duration;
		this.Effectiveness = effect;
	}
}
