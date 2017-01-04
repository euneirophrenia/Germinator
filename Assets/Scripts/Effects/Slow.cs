
using System;

public class Slow : Effect
{
	
	public Slow(float effect, int ticks, float cooldown=1) //dura cooldown*ticks secondi
	{
		this.Cooldown = cooldown;
		this.Ticks = ticks;
		this.Effectiveness = effect;

		this.scriptName = "SlowScript";
	}
}
