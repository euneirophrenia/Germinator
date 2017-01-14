using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Diagnostics;

public class Hittable : MonoBehaviour {


    public int HP = 100;
    
	//public Dictionary<System.Type, float> sensibility = new Dictionary<System.Type, float>();
	public SensibilityDictionary sensibility;

	public void Proc(IEnumerable<Effect> effects)
    {
        foreach (Effect e in effects)
        {
            StartCoroutine(Proc(e));
        }
    }
	
	public virtual IEnumerator Proc(Effect effect)
    {
        //Debug.Log("Procced " + effect.effectScriptName + " for " + effect.effectiveness + " on " + this.gameObject.name);

        System.Type tipo = System.Type.GetType(effect.effectScriptName);

		float effectiveness = effect.Effectiveness * sensibility[tipo];
		if (effectiveness==0)
			yield break;

        Component previous = this.gameObject.GetComponent(tipo);
		if (previous == null)
            previous= this.gameObject.AddComponent(tipo);
       


        EffectScript script = (EffectScript)previous;

        script.RefreshEffect(effect, effectiveness);
       

        yield return null;
    }

    public void SetEffectiveness(System.Type tipo, float value)
    {
        sensibility[tipo] = value;
    }

    public float GetEffectiveness(System.Type t)
    {
		return sensibility[t];
    }

    public void AddToHp(int value)
    {
        HP += value;
        if (HP<=0)
        {
            //animazioncina
            Destroy(this.gameObject);
        }
    }
		
	//per popolare correttamente la mappa dall'editor di unity
	[Conditional("UNITY_EDITOR")]
	void OnValidate()
	{
		sensibility.Build();
		/*foreach (System.Type t in sensibility.Keys)
		{
			Debug.Log(t.Name+ " "+ sensibility[t]);
		}*/
	}
}
