using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;



[CustomEditor(typeof(Effect), true)] //true cosi che sia usato nelle sottoclassi!
public class EffectEditor : Editor 
{
	private Effect e;
	private GUIStyle tipstyle;
	private Assembly referenced;
	private string[] scripts;

	private GUIContent effect;

	void OnEnable()
	{
		e = target as Effect;
		effect = new GUIContent("Effetto", "Tanto maggiore, tanto più forte");

	}
		

	public override void OnInspectorGUI()
	{
		tipstyle = new GUIStyle(EditorStyles.helpBox);
		tipstyle.richText=true;
		tipstyle.fontSize=10;


		e.Effectiveness = EditorGUILayout.FloatField(effect, e.Effectiveness);
		EditorGUILayout.Separator();

		if (typeof(TickingEffect).IsAssignableFrom(e.GetType()))
		{
            TickingEffect x = (TickingEffect)e;
			x.Cooldown = EditorGUILayout.FloatField("Cooldown", x.Cooldown);
			x.Ticks = EditorGUILayout.IntField("Ticks", x.Ticks);
			EditorGUILayout.LabelField("L'effetto durerà <b>Ticks*Cooldown (" + x.Ticks * x.Cooldown + ") secondi</b>\n.",tipstyle);
			if (x.Ticks==0)
				EditorGUILayout.HelpBox("Ticks=0 provoca un effetto che dura per sempre.", MessageType.Info);
            x.effectScriptName = x.GetType().Name + "Script";
            if (!Validate(x.effectScriptName))
            {
                EditorGUILayout.HelpBox("Script non trovato, assicurarsi che il nome rispetti la convenzione <NomeEffetto>Script.", MessageType.Error);
            }
		}
        if (typeof(LastingEffect).IsAssignableFrom(e.GetType()))
        {
            LastingEffect x = (LastingEffect)e;
            x.duration = EditorGUILayout.FloatField("Duration", x.duration);
            x.scriptName = x.GetType().Name + "Script";
            if (!Validate(x.scriptName))
            {
                EditorGUILayout.HelpBox("Script non trovato, assicurarsi che il nome rispetti la convenzione <NomeEffetto>Script.", MessageType.Error);
            }
        }

        EditorGUILayout.Separator();
        EditorUtility.SetDirty(target);
	}

    private bool Validate(string type)
    {
        return (from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
         where typeof(EffectScript).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface
         select x.Name).Contains(type);
    }

	/*private void UpdateScripts()
	{
		scripts=(from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s=>s.GetTypes())
			where typeof(EffectScript).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface
			select x.Name).ToArray();
	}*/
}
