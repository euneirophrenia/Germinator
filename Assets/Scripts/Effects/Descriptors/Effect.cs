using System;
using UnityEngine;

[Serializable]
public abstract class Effect : ScriptableObject //non tutti useranno tutto
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
    
}
