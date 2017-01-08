using UnityEngine;
using System.Collections;

public class PredictingLinearMovement : Movement {

    public GameObject target;

  	private Transform other;
    private Vector3 otherPreviousPosition;
    private Vector3 otherVelocity;
    private Vector3 drift;

	void Start()
	{
		other = target.transform;
        otherPreviousPosition = other.position;
        this.direction = cachedTransform.position - other.position;
        float timeBeforeHit = direction.magnitude / speed;
        this.drift = -direction/timeBeforeHit;
	}
	
	 void Update () {

        if (target != null)
        {
            otherVelocity = (other.position - otherPreviousPosition)/Time.deltaTime;
            otherPreviousPosition = other.position;
            this.direction = otherVelocity;
            this.direction += drift;

            cachedTransform.rotation = Quaternion.LookRotation(direction);
        }
        cachedTransform.Translate(direction * Time.deltaTime,Space.World);

        
    }
}
