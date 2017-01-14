using System.Diagnostics;
using UnityEngine;

[ExecuteInEditMode]
public class NamePathNodes : MonoBehaviour {

	[Conditional("UNITY_EDITOR")]
	//per evitare di distruggere i nomi di cose che non c'entrano
	//funziona solo se si chiama Path (ignore case) il gameobject padre
	void Start()
	{
		if (string.Compare(this.gameObject.name, "path", true)!=0)  
			DestroyImmediate(this);
		
	}

	[Conditional("UNITY_EDITOR")]
	void Update () 
	{
		if (!Application.isPlaying)
		{
			for (int i=0; i<transform.childCount; i++)
			{
				transform.GetChild(i).name=i.ToString();
			}
		}
	}
}
