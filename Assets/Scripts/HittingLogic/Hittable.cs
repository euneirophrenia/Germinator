using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System;
using AsyncCoroutines;

public class Hittable : MonoBehaviour {

    public int maxHP = 100;
    public int currentHP;
    public SensibilityDictionary sensibility;

    /*Oltre a farmi comodo per resettare oggetti in pool, sorprendentemente migliora l'efficienza
     * visto che si risparmiano le getComponent per controllare se è presente o no un effetto */
    private List<EffectScript> currentlyProcced;

    #region PROC LOGIC
    public void Proc(IEnumerable<Effect> effects)
    {
        foreach (Effect e in effects)
        {
            this.StartCoroutineAsync(Proc(e));
        }
    }
	
	
    public virtual IEnumerator Proc(Effect effect)
    {   
        Type tipo = Type.GetType(effect.effectScriptName);

		float effectiveness = effect.Effectiveness * sensibility[tipo];
		if (effectiveness==0)
			yield break;

        EffectScript previous = this.FindActive(tipo);

		yield return Ninja.JumpToUnity;
        if (previous == null)
        {
            previous = (EffectScript)this.gameObject.AddComponent(tipo);
            this.currentlyProcced.Add(previous);
        }
        previous.RefreshEffect(effect, effectiveness);

        yield return null;
    }


    public void UnApply(EffectScript ef)
    {
        this.currentlyProcced.Remove(ef);
    }
    #endregion

    #region UTILITY
    public void SetEffectiveness(Type tipo, float value)
    {
        sensibility[tipo] = value;
    }

    public float GetEffectiveness(Type t)
    {
		return sensibility[t];
    }

    public void AddToHp(int value)
    {
        currentHP += value;
        if (currentHP<=0)
        {
            //animazioncina
            Destroy(this.gameObject);
            //release anzi che destroy
        }
    }

    private EffectScript FindActive(Type tipo)
    {
        foreach (EffectScript e in this.currentlyProcced)
        {
            if (tipo.IsAssignableFrom(e.GetType()))
                return e;
        }
        return null;
    }

    #endregion


    #region UNITY CALLBACKS

    public void Start()
    {
        this.currentlyProcced = new List<EffectScript>();
    }

    public void OnEnable()
    {
        this.currentHP = maxHP;
    }

    public void OnDisable()
    {
        if (currentlyProcced == null)
            return;

       foreach (EffectScript e in this.currentlyProcced)
        {
            e.UnApply();
        }
        currentlyProcced.Clear();
    }

    //per popolare correttamente la mappa dall'editor di unity
    [Conditional("UNITY_EDITOR")]
	void OnValidate()
	{
		sensibility.Build();
	}
    #endregion
}
