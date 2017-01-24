using System;
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

    public override void Proc(Hittable target, float actual)
    {
        base.Proc(target, actual);
        target.AddToHp(-(int)actual);
    }
}
