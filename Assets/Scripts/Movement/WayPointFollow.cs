using UnityEngine;
using System.Collections.Generic;

public class WayPointFollow: Movement {

    public GameObject[] waypoints;

    private GameObject next;
	private int current = 0;
	private Transform other;
	private static string waypointtag = "WayPoint";

    void Start () {
        next = waypoints[0];
		other = next.transform;
        direction = other.position - cachedTransform.position;
        direction = direction.normalized;
    }
	

	void Update () {
        
        cachedTransform.Translate(speed * direction * Time.deltaTime, Space.World);
        cachedTransform.LookAt(other.transform);
    }

	void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag(waypointtag))
		{
            current = int.Parse(collision.name);
			if (current==waypoints.Length)
			{
				Destroy(this.gameObject);
				return;
			}
			next = waypoints[current];
			other = next.transform;
            direction = other.position - cachedTransform.position;
            direction = direction.normalized;
        }
	}
}
