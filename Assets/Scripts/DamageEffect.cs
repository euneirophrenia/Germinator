
using System;

public class DamageEffect : Effect
{ 

    private float effect = 0f;

    public float cooldown
    {
        get
        {
           return 0;
        }

        set {}
    }

    public float effectiveness
    {
        get
        {
            return effect;
        }

        set
        {
            effect = value;
        }
    }

    public string effectScriptName
    {
        get
        {
            return "DamageEffectScript";
        }
    }

    public int ticks
    {
        get
        {
            return 1;
        }

        set { }
    }
}
