using UnityEngine;
using System.Collections;

public abstract class EffectScript : MonoBehaviour {

    public float effectiveness;
    protected Hittable target;
    private Hittable[] temp;
    

    public virtual void Awake()
    {
        temp = this.GetComponentsInChildren<Hittable>();
        target = temp[temp.Length - 1];
    }

    public virtual void Apply() { } 
	public virtual void UnApply() 
	{
        //Destroy(this);
        this.enabled = false;
	} 

}
