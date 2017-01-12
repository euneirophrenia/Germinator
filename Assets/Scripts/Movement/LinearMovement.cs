using UnityEngine;

public class LinearMovement : Movement
{

    public GameObject target;

    private Transform other;

    void Start()
    {
        other = target.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
			this.direction = (other.position - this.cachedTransform.position).normalized;
            cachedTransform.LookAt(other);
        }
        cachedTransform.Translate(direction * speed * Time.deltaTime, Space.World);

    }

	public override GameObject CurrentTarget {
		get {
			return target;
		}
		set {
			if (target==null)
				target = value;
			else 
			{
				target=value;
				Start();
			}
		}
	}
}