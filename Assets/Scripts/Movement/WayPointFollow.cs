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
	}
	

	void Update () {
        
		direction = other.position - cachedTransform.position;
        direction.y = 0;
	
        cachedTransform.Translate(speed * direction.normalized * Time.deltaTime, Space.World);
        cachedTransform.LookAt(other.transform);
    }

	void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag(waypointtag))
		{
			if (current==waypoints.Length-1)
			{
				Destroy(this.gameObject);
				//eventuali cose da fare alla distruzione non sono da mettere da qua
				return;
			}
			current = current + 1;
			next = waypoints[current];
			other = next.transform;
		}
	}
}
