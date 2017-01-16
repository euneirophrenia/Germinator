using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreator : MonoBehaviour {

	public Color errorColor;

	private GameObject current;
	private Transform cachedTransform;
	private Vector3 position;
	private RaycastHit cachedHit;
	private Renderer rend;
	private Color original;

	void Update()
	{
		if (!current)
			return;

		bool isAbove = !Physics.BoxCast(cachedTransform.position, cachedTransform.localScale*0.5f, Vector3.down);
		
		if(Input.GetMouseButton(0))
		{
			float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			position=Input.mousePosition;
			position.z = distance_to_screen;
			cachedTransform.position = Camera.main.ScreenToWorldPoint(position);
		}

		if(Input.GetMouseButtonUp(0) && isAbove)
		{
				Activate();
				current = null;
		}

		if (!isAbove)
		{
			rend.material.color = errorColor;
		}
		else
			rend.material.color=original;
	}

	public void CreateAndDrag(GameObject obj)
	{
		current = Instantiate(obj,Input.mousePosition,Quaternion.identity);
		cachedTransform = current.transform;
		rend = current.GetComponent<Renderer>();
		original = rend.material.color;
	}

	private void Activate()
	{
		Physics.Raycast(this.cachedTransform.position, Vector3.down, out cachedHit, float.MaxValue, Physics.IgnoreRaycastLayer);
		cachedTransform.position = cachedHit.point;
		Debug.Log("Piazzata correttamente");
	}

}
