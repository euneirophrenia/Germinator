using UnityEngine;
using System.Collections.Generic;

public class WayPointFollow: Movement {

    public GameObject[] waypoints;

	private int current = 0;
	private Transform other;
	private static readonly string waypointtag = "WayPoint";

    void Start () {
		other = waypoints[current].transform;
        direction = other.position - cachedTransform.position;
        direction = direction.normalized;
    }
	

	void Update () {
        
        cachedTransform.Translate(speed * direction * Time.deltaTime, Space.World);
        cachedTransform.LookAt(other.transform);
    }

	void OnTriggerEnter(Collider collision)
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
	}

	public bool isObjectiveReached()
	{
		return current==waypoints.Length;
	}

	public override GameObject CurrentTarget {
		get {
			return waypoints[current];
		}
		set { 
			waypoints[current] = value;
			Start();
		}
	}
}
