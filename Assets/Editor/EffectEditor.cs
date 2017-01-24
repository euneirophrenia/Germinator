using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;



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

		if (typeof(TimeBasedEffect).IsAssignableFrom(e.GetType()))
		{
            TimeBasedEffect x = (TimeBasedEffect)e;
			x.Cooldown = EditorGUILayout.FloatField("Cooldown", x.Cooldown);
			x.Ticks = EditorGUILayout.IntField("Ticks", x.Ticks);
			EditorGUILayout.LabelField("L'effetto durerà <b>Ticks*Cooldown (" + x.Ticks * x.Cooldown + ") secondi</b>\n.",tipstyle);
			if (x.Ticks==0)
				EditorGUILayout.HelpBox("Ticks=0 provoca un effetto che dura per sempre.", MessageType.Info);
		}

        EditorGUILayout.Separator();
        EditorUtility.SetDirty(target);
	}

	/*private void UpdateScripts()
	{
		scripts=(from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s=>s.GetTypes())
			where typeof(EffectScript).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface
			select x.Name).ToArray();
	}*/
}
