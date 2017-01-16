using UnityEngine;
using System.Collections.Generic;

public class WayPointFollow: Movement {

    public GameObject next;
	private Transform other;

    void Start () {
		if (next!=null)
		{
			other = next.transform;
	        direction = other.position - cachedTransform.position;
	        direction = direction.normalized;
		}
		else {
			Destroy(this.gameObject);
			//obiettivo raggiunto
		}
    }
	

	void Update () {
        
        cachedTransform.Translate(speed * direction * Time.deltaTime, Space.World);
        cachedTransform.LookAt(other.transform);
    }

	/*void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag(waypointtag) && collision.gameObject == waypoints[current])
		{
            current = int.Parse(collision.name);
			if (current==waypoints.Length)
			{
				Destroy(this.gameObject);
				//Raggiunto l'obiettivo
				return;
			}
			other = waypoints[current].transform;
            direction = other.position - cachedTransform.position;
            direction = direction.normalized;
        }
	}*/

	public bool isObjectiveReached()
	{
		return next==null;
	}

	public override GameObject CurrentTarget {
		get {
			return next;
		}
		set { 
			next= value;
			Start();
		}
	}
}
