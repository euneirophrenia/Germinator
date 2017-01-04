
using System;

public class Damage : Effect
{ 
	public Damage(float value)
	{
		this.Effectiveness = value;
		this.scriptName= "DamageScript";
	}
}
