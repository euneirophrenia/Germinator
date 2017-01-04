using UnityEngine;
using System.Collections;

public class LinearMovement : Movement {

    public GameObject target;

	private Transform other;

	void Start()
	{
		other = target.transform;
	}
	
	// Update is called once per frame
	 void Update () {

        if (target != null)
        {
            this.direction = other.position - this.cachedTransform.position;
            cachedTransform.LookAt(other);
        }
        cachedTransform.Translate(direction.normalized * speed * Time.deltaTime,Space.World);

        
    }
}
