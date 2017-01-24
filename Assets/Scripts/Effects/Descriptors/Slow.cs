using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName="Slow", menuName="Effect/Slow")]
public class Slow : TimeBasedEffect
{
	
	public Slow(float effect, int ticks, float cooldown=1)  : base() //dura cooldown*ticks secondi
	{
		init(effect, ticks, cooldown);
	}

	//da usare se si crea l'effetto con ScriptableObject.CreateInstance<Slow>()
	//una new Slow(effect, ticks, cd) funziona ottimamente ma genera un warning molesto
	public void init(float effect, int ticks, float cooldown=1) 
	{
		this.Cooldown = cooldown;
		this.Ticks = ticks;
		this.Effectiveness = effect;
	}
}
