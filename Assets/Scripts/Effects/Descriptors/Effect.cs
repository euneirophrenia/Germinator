using System;
using UnityEngine;

[Serializable]
public abstract class Effect : ScriptableObject
{
	[SerializeField]
	protected float effect =0f; //con  significato diverso a seconda dell'effetto concreto, puo' essere percentuale di slow dichiarata o altro

    public Animation animation; //la parte grafica da mostrare, quel che sarà
    public GameObject source;

    public virtual void Proc(Hittable target, float actual)
    {
       //mostra animazione- effetto visivo
    }

    public virtual float Effectiveness { 
		get {
			return effect;
		} 
		set {
			effect=value;
		}
	}

    public override bool Equals(object other)
    {
        return other is Effect && this.GetType() == other.GetType() && this.effect == ((Effect)other).effect;
       
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
