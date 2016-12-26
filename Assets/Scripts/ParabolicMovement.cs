using UnityEngine;
using System.Collections;

public class ParabolicMovement : Movement  {

    public float gravity = 10;
    public GameObject target;

    private float velocityY;

	// Use this for initialization
	 void Start () {
        
        direction = target.transform.position - this.transform.position;
        direction.y = 0;
        float distance = direction.magnitude;
        velocityY = (0.5f * gravity * distance / speed) - (speed * this.transform.position.y / distance); //trust me
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        direction = target.transform.position - this.transform.position;
        direction.y = 0;

        float distance = direction.magnitude;
        velocityY = -gravity * Time.deltaTime + (0.5f * gravity * distance / speed) - (speed * this.transform.position.y / distance);
        //velocityY -= gravity * Time.deltaTime; piu' naturale ma cade corto, tipicamente, a meno che non sia fermo il bersaglio
        

        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        this.transform.Translate(0, velocityY * Time.deltaTime, 0, Space.World);

        transform.LookAt(target.transform);


    }
}
