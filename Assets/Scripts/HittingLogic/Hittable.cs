using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System;
using AsyncCoroutines;

public class Hittable : MonoBehaviour {

    public int HP=100;

    [HideInInspector, SerializeField]
    private int maxHP; 

    public SensibilityDictionary sensibility;

    /*Oltre a farmi comodo per resettare oggetti in pool, sorprendentemente migliora l'efficienza
     * visto che si risparmiano le getComponent per controllare se è presente o no un effetto */
    private List<EffectScript> effectsCache;

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
        //Type tipo = Type.GetType(effect.effectScriptName);
		Type tipo = effect.EffectType;

		float effectiveness = effect.Effectiveness * sensibility[tipo];
		if (effectiveness==0)
			yield break;

        EffectScript previous = this.FindActive(tipo);

		yield return Ninja.JumpToUnity;
        if (previous == null)
        {
            previous = (EffectScript)this.gameObject.AddComponent(tipo);
            this.effectsCache.Add(previous);
        }
        previous.RefreshEffect(effect, effectiveness);
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
        HP += value;
        if (HP<=0)
        {
            //animazioncina
            Destroy(this.gameObject);
            //release anzi che destroy
        }
    }

    private EffectScript FindActive(Type tipo)
    {
        foreach (EffectScript e in this.effectsCache)
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
        this.effectsCache = new List<EffectScript>();
    }

    public void OnEnable()
    {
        this.HP = maxHP;
     
    }

    public void OnDisable()
    {
        if (effectsCache == null)
            return;

       foreach (EffectScript e in this.effectsCache)
        {
            e.UnApply();
        }
    }

    //per popolare correttamente la mappa dall'editor di unity
    [Conditional("UNITY_EDITOR")]
	void OnValidate()
	{
		sensibility.Build();
        maxHP = HP;
    }
    #endregion
}
