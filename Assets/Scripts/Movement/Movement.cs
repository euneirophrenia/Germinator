using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

    public float speed = 100;
 
    protected Vector3 direction;
	protected Transform cachedTransform;

	void Awake()
	{
		cachedTransform = this.transform;
	}

}
