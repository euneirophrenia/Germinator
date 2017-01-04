
using System;

public class SlowEffect : Effect
{
    private float effect = 2f;
	private int tick = 3;

    public float cooldown
    {
        get
        {
            return 1;
        }

        set
        {
            
        }
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
            return "SlowEffectScript";
        }
    }

    public int ticks
    {
        get
        {
            return tick;
        }

        set
        {
			tick=value;
        }
    }
}
