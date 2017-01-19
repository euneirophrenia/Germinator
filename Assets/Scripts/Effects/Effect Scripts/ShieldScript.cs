using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

    private Hittable parent;
    private float previousEffectiveness;

	// Use this for initialization
	public void Start () {
        parent = this.GetComponentInParent<Hittable>();
        previousEffectiveness = parent.GetEffectiveness(typeof(DamageScript));
        parent.SetEffectiveness(typeof(DamageScript), 0);
	}

    public void OnDestroy()
    {
        parent.SetEffectiveness(typeof(DamageScript), previousEffectiveness);
    }
}
