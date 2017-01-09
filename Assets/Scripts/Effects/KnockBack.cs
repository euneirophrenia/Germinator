using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName="KnockBack", menuName="Effect/KnockBack")]
public class KnockBack : Effect
{ 
	public KnockBack(float value) : base()
	{
		init(value);
	}

	public void init(float value)
	{
		this.Effectiveness = value;
	}
}
