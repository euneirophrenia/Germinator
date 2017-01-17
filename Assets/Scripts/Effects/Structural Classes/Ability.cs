using UnityEngine;

public abstract class Ability : MonoBehaviour {

	public float timeBetweenActivations;
	private float activeCoolDown=0;


	/// <summary>
	/// Metodo che applica davvero l'abiltà
	/// </summary>
	/// <returns>Ritorna true se è stata applicata </returns>
	public abstract bool Do(); 

	public virtual void Update () 
	{

		if (activeCoolDown<=0 && Do())
		{
			activeCoolDown=timeBetweenActivations;
		}
		else
		{
			activeCoolDown -= Time.deltaTime;
		}


	}

}
