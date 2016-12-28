using UnityEngine;
using System.Collections;

public class LinearMovement : Movement {

    public GameObject target;

	
	// Update is called once per frame
	 void Update () {

        if (target != null)
        {
            this.direction = target.transform.position - this.transform.position;
            transform.LookAt(target.transform);
        }
        this.transform.Translate(direction.normalized * speed * Time.deltaTime,Space.World);

        
    }
}
