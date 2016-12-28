using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Hittable : MonoBehaviour {


    public int HP = 100;
    public static Dictionary<System.Type, float> sensibility = new Dictionary<System.Type, float>();

    public void Proc(List<Effect> effects)
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
        Component previous = this.gameObject.GetComponent(tipo);
        if (previous == null)
        {
            this.gameObject.AddComponent(tipo);
            previous = this.gameObject.GetComponent(tipo);
        }
        float effectiveness = effect.effectiveness * getEffectiveness(tipo);

        EffectScript script = (EffectScript)previous;

        script.RefreshEffect(effect, effectiveness);
       

        yield return null;
    }

    public void setEffectiveness(System.Type tipo, float value)
    {
        sensibility[tipo] = value;
    }

    public float getEffectiveness(System.Type t)
    {
        return (sensibility.ContainsKey(t) ? sensibility[t] : 1.0f);
    }

    public void addToHp(int value)
    {
        HP += value;
        if (HP<=0)
        {
            //animazioncina
            Destroy(this.gameObject);
        }
    }

}
