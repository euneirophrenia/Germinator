using UnityEngine;
using System.Collections;

public class PredictingLinearMovement : Movement {

    public GameObject target;

  	private Transform other;
    private Vector3 otherPreviousPosition;
    private Vector3 drift;

	void Start()
	{
		other = target.transform;
        otherPreviousPosition = other.position;
		this.direction =  other.position- cachedTransform.position;
        float timeBeforeHit = direction.magnitude / speed;
		this.drift = direction/timeBeforeHit;
		//cachedTransform.rotation = Quaternion.LookRotation(direction);
	}
	
	 void Update () {

        if (target != null)
        {
			this.direction = (other.position - otherPreviousPosition)/Time.deltaTime  + drift;
            otherPreviousPosition = other.position;
            
        }
        cachedTransform.Translate(direction * Time.deltaTime,Space.World);
    }

	void FixedUpdate()
	{
		cachedTransform.rotation = Quaternion.Lerp(cachedTransform.rotation, Quaternion.LookRotation(direction), 0.3f);
	}
}
