using UnityEngine;
using System.Collections;

public class ParabolicMovement : Movement  {

    public float gravity = 10;
    public GameObject target;

    private float velocityY;
	private Transform other;

	// Use this for initialization
	 void Start () {
        
		other = target.transform;
		direction = other.position - cachedTransform.position;
        direction.y = 0;
        float distance = direction.magnitude;
		velocityY = (0.5f * gravity * distance / speed) - (speed * cachedTransform.position.y / distance); //trust me
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target != null)
        {
			direction = other.position - cachedTransform.position;
            cachedTransform.LookAt(other);
        }
        direction.y = 0;

        float distance = direction.magnitude;
		velocityY = -gravity * Time.deltaTime + (0.5f * gravity * distance / speed) - (speed * cachedTransform.position.y / distance);
        //velocityY -= gravity * Time.deltaTime; piu' naturale ma cade corto, tipicamente, a meno che non sia fermo il bersaglio
        

		cachedTransform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
		cachedTransform.Translate(0, velocityY * Time.deltaTime, 0, Space.World);

        
    }
}
