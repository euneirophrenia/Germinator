using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName="KnockBack", menuName="Effect/KnockBack")]
public class KnockBack : LastingEffect
{ 
	public KnockBack(float value, float duration) : base()
	{
		init(value, duration);
	}

	public void init(float value, float duration)
	{
		this.Effectiveness = value;
        this.duration = duration;
	}
}
