using UnityEngine;
using System.Collections;
using System;

[Debuff]
public class KnockBackScript : TimeBasedAbility {

	private new Rigidbody rigidbody;
	private Vector3 force;

	void Start () 
	{
		rigidbody = this.GetComponent<Rigidbody>();
		force = -effectiveness * this.GetComponent<Movement>().Direction;
		this.rigidbody.AddForce(force, ForceMode.Impulse);
	}

	public override void UnApply()
	{
		this.rigidbody.AddForce(-force, ForceMode.Impulse);
		Destroy(this);
	}
		
}