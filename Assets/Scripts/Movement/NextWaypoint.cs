using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaypoint : MonoBehaviour {

	public GameObject next;


	void OnTriggerEnter(Collider collision)
	{
		WayPointFollow wp = collision.GetComponent<WayPointFollow>();
		if (wp!=null)
		{
			wp.CurrentTarget=next;
		}
	}
}
