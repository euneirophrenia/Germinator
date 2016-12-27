using UnityEngine;
using System.Collections.Generic;

public class WayPointFollow: Movement {

    public List<GameObject> waypoints;

    private GameObject next;
    private int current = 0;
    private float epsilon = 5f;

	// Use this for initialization
    void Start () {
        next = waypoints[0];
	}
	
	// Update is called once per frame
	void Update () {
        
        direction = next.transform.position - this.transform.position;
        direction.y = 0;

        this.transform.Translate(speed * direction.normalized * Time.deltaTime, Space.World);
        transform.LookAt(next.transform);

        if (direction.magnitude<=epsilon)
        {
            if (current==waypoints.Count-1)
            {
                Destroy(this.gameObject);
                //eventuali cose da fare alla distruzione non sono da mettere da qua
                return;
            }
            current = current + 1;
            next = waypoints[current];
        }


    }
}
