using UnityEngine;
using System.Collections;

public class LinearMovement : Movement {

    public GameObject target;

	
	// Update is called once per frame
	 void Update () {
        //Commento commitato con visual studio
       
        this.direction = target.transform.position - this.transform.position;
        
        this.transform.Translate(direction.normalized * speed * Time.deltaTime,Space.World);

        transform.LookAt(target.transform);
    }
}
