using UnityEngine;
using System.Collections;

public abstract class EffectScript : MonoBehaviour {

    public float effectiveness;

    public virtual void RefreshEffect(Effect e, float actualEffectivenes)
    {
        effectiveness = actualEffectivenes;
    }

    public virtual void Apply() { } 
	public virtual void UnApply() 
	{
		Destroy(this);
	} 

}
