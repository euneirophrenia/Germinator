﻿using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName="Damage", menuName="Effect/Damage")]
public class Damage : Effect
{ 
	public Damage(float value) : base()
	{
		init(value);
	}

	public void init(float value)
	{
		this.Effectiveness = value;
	}
}