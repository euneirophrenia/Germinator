using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontTouchMe : MonoBehaviour {

	private PoolManager pool;

	void Start()
	{
		pool=PoolManager.SharedInstance();
	}

    void OnTriggerEnter(Collider other)
    {
		pool.ReleaseToPool(other.gameObject);
    }
}
