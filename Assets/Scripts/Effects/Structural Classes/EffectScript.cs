using UnityEngine;
using System.Collections;

public abstract class EffectScript : MonoBehaviour {

    public float effectiveness;
    protected Hittable target;

    public virtual void Start()
    {
        Hittable[] temp = this.GetComponentsInChildren<Hittable>();
        target = temp[temp.Length - 1];
        
    }

    public virtual void RefreshEffect(Effect e, float actualEffectivenes)
    {
        effectiveness = actualEffectivenes;
    }

    public virtual void Apply() { } 
	public virtual void UnApply() 
	{
        target.UnApply(this);
		Destroy(this);
	} 

}
