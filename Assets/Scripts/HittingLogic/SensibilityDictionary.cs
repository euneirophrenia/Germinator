using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//Questa classe è un dizionario che si può modificare da unity
//Unity a default non mostra dizionari a meno di scriversi dei CustomEditor
//Strada che pure ho tentato ma con scarso successo, nel senso che
//si sarebbe potuto ottenere qualcosa per classi concrete, non generiche
//allora ho deciso che tanto valeva semplificare e far così.

[Serializable]
public class SensibilityDictionary 
{
	[Serializable]
	public struct Entry
	{
		public string type;
		public float effectiveness;

		public Entry(string t, float v)
		{
			type=t;
			effectiveness=v;
		}

	}

	public float defaultValue=1f;
		
	public Entry[] Effectivenesses ;


	public void Build()
	{
		map.Clear();
		if (Effectivenesses == null || Effectivenesses.Count() == 0) return; 
		foreach (Entry e in Effectivenesses)
		{
			try{
				map[Type.GetType(e.type)]=e.effectiveness;
			}
			catch (Exception) {
				//Debug.LogError("No such type: "+e.type);
			}
		}


	}

	private Dictionary<Type, float> map;



	public float this[Type key] {
		
		get {
			return map.ContainsKey(key)? map[key] : defaultValue;
		}

		set {
			map[key]=value;
		}
	}

	public IEnumerable<Type> Keys {
		get {
			return map.Keys;
		}
	}

	public SensibilityDictionary()
	{
		this.map=new Dictionary<Type, float>();
	}
}
