using System;
using System.Diagnostics;
using UnityEngine;

public abstract class LastingEffect : Effect
{
    public float duration;

    public string scriptName; //il nome dello script corrispondente

    private Type tipo;

    protected LastingEffect()
    {
        this.scriptName = this.GetType().Name + "Script";
        tipo = Type.GetType(this.scriptName);
    }

    public override void Proc(Hittable target, float actual)
    {
        LastingScript previous = (LastingScript)target.FindActive(tipo, actual);
        if (previous == null)
        {
            previous = (LastingScript)target.gameObject.AddComponent(tipo);
            target.Cache(previous);

        }
        previous.Refresh(this, actual, target.GetDuration(this.GetType()));
        base.Proc(target, actual);
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
