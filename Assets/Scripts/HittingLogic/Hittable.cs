using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using AsyncCoroutines;

public class Hittable : MonoBehaviour {

    public int HP=100;

    [HideInInspector, SerializeField]
    private int maxHP; 

    [SerializeField]
    public SensibilityDictionary sensibility;

    public SensibilityDictionary durations;

    /*Oltre a farmi comodo per resettare oggetti in pool, sorprendentemente migliora l'efficienza
     * visto che si risparmiano le getComponent per controllare se è presente o no un effetto */
    private List<EffectScript> effectsCache;


    #region PROC LOGIC
    public void Proc(IEnumerable<Effect> effects)
    {
        foreach (Effect e in effects)
        {
            //this.StartCoroutineAsync(Proc(e));
            Proc(e);
        }
    }
	
    public virtual  void /*IEnumerator*/ Proc(Effect effect)
    {
        float effectiveness = effect.Effectiveness * sensibility[effect.GetType()];
        if (effectiveness == 0)
            return;//yield break;

        //yield return Ninja.JumpToUnity;
        effect.Proc(this, effectiveness);

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

    public void SetDuration(Type tipo, float value)
    {
        durations[tipo] = value;
    }

    public float GetDuration(Type t)
    {
        return durations[t];
    }

    public void AddToHp(int value)
    {
        HP += value;
        if (HP<=0)
        {
            //animazioncina
            this.gameObject.Release();
        }
    }

    public void Cache(EffectScript effect)
    {
        this.effectsCache.Add(effect);
    }

    public EffectScript FindActive(Type tipo, float actual)
    {
        foreach (EffectScript e in this.effectsCache)
        {
            if (tipo.IsAssignableFrom(e.GetType()) && e.effectiveness == actual)
                return e;
        }
        return null;
    }


    #endregion


    #region UNITY CALLBACKS

    public void Awake()
    {
        sensibility.Build();
        durations.Build();
    }

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
            if (e.isActiveAndEnabled)
                e.UnApply();
        }
    }

    //per popolare correttamente la mappa dall'editor di unity
	void OnValidate()
	{
        sensibility.Build();
        durations.Build();
        this.maxHP = HP;
    }
    #endregion
}
