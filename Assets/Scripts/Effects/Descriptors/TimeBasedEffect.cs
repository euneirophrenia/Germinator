using UnityEngine;
using System;
using System.Diagnostics;


public abstract class TimeBasedEffect : Effect
{
    [SerializeField]
    protected int ticks = 0;

    [SerializeField]
    protected float cooldown = 0f;

    [SerializeField]
    protected string scriptName; //il nome dello script corrispondente

    private Type tipo;

    protected TimeBasedEffect()
    {
        this.scriptName = this.GetType().Name + "Script";
        tipo = Type.GetType(this.scriptName);
    }

    public override void Proc(Hittable target, float actual)
    {
        TimeBasedScript previous= (TimeBasedScript)target.FindActive(tipo);
        if (previous == null)
        {
            previous = (TimeBasedScript)target.gameObject.AddComponent(tipo);
            target.Cache(previous);
            
        }
        previous.RefreshEffect(this, actual);
        base.Proc(target, actual);
    }

    public virtual string effectScriptName
    {
        get
        {
            return scriptName;
        }
        set
        {
            scriptName = value;
        }
    }

    public virtual Type EffectType
    {
        get
        {
            return tipo;
        }
    }

    public virtual int Ticks
    {
        get
        {
            return ticks;
        }
        set
        {
            ticks = value;
        }
    }
    public virtual float Cooldown
    {
        get
        {
            return cooldown;
        }
        set
        {
            cooldown = value;
        }
    }

    [Conditional("UNITY_EDITOR")]
    public void OnValidate()
    {
        try
        {
            tipo = Type.GetType(this.scriptName);
        }
        catch (Exception)
        {

        }
    }
}

