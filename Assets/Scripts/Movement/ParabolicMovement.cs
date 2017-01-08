using UnityEngine;
using System.Collections;

public class ParabolicMovement : Movement  {

	public GameObject target;

	public float gravity = 10;
	private float velocityY;
    private float distance;
	private Transform other;
    private Vector3 otherPreviousPosition;
    private Vector3 otherVelocity;
    private Vector3 drift;

    private float timeBeforeHit;

    void Start () {

        other = target.transform;
        otherPreviousPosition = other.position;
        this.direction = cachedTransform.position - other.position;
        distance = direction.magnitude;
        timeBeforeHit = distance / speed;
        this.drift = -direction / timeBeforeHit;

        velocityY = (other.position.y - cachedTransform.position.y)/timeBeforeHit + (0.5f*gravity*timeBeforeHit);
    }

	// Update is called once per frame
	void Update ()
	{
        if (target != null)
        {
            otherVelocity = (other.position - otherPreviousPosition) / Time.deltaTime;
            otherPreviousPosition = other.position;
            this.direction = otherVelocity;
            this.direction += drift;
            velocityY -= gravity * Time.deltaTime;
            direction.y = velocityY;
            cachedTransform.rotation = Quaternion.LookRotation(direction);
        }
        
        cachedTransform.Translate(direction * Time.deltaTime, Space.World);

	}
}