using UnityEngine;
using System;
using System.Diagnostics;


public abstract class TickingEffect : Effect
{
    [SerializeField]
    protected int ticks = 0;

    [SerializeField]
    protected float cooldown = 0f;

    [SerializeField]
    protected string scriptName; //il nome dello script corrispondente

    private Type tipo;

    protected TickingEffect()
    {
        this.scriptName = this.GetType().Name + "Script";
        tipo = Type.GetType(this.scriptName);
    }

    public override void Proc(Hittable target, float actual)
    {
        TickingEffectScript previous= (TickingEffectScript)target.FindActive(tipo, actual);
        if (previous == null)
        {
            previous = (TickingEffectScript)target.gameObject.AddComponent(tipo);
            target.Cache(previous);
            
        }
        previous.RefreshEffect(this, actual, target.GetDuration(this.GetType()));
        base.Proc(target, actual);
    }

    public override bool Equals(object other)
    {
        return other is TickingEffect 
            && other.GetType() == this.GetType()
            && this.cooldown == ((TickingEffect)other).cooldown 
            && this.ticks == ((TickingEffect)other).ticks
            && this.effect == ((TickingEffect)other).effect;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
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

