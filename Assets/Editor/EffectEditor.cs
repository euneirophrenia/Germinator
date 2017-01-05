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

	private GUIContent effect;

	void OnEnable()
	{
		e = target as Effect;
		referenced = Assembly.GetAssembly(typeof(TimeBasedAbility));
		e.effectScriptName = e.GetType().Name+"Script";

		effect = new GUIContent("Effetto", "Tanto maggiore, tanto più forte");

	}
		

	public override void OnInspectorGUI()
	{
		tipstyle = new GUIStyle(EditorStyles.helpBox);
		tipstyle.richText=true;
		tipstyle.fontSize=10;


		e.Effectiveness = EditorGUILayout.FloatField(effect, e.Effectiveness);
		EditorGUILayout.Separator();

		if (!String.IsNullOrEmpty(e.effectScriptName) && typeof(TimeBasedAbility).IsAssignableFrom(referenced.GetType(e.effectScriptName)))
		{
			e.Cooldown = EditorGUILayout.FloatField("Cooldown", e.Cooldown);
			e.Ticks = EditorGUILayout.IntField("Ticks", e.Ticks);
			EditorGUILayout.LabelField("L'effetto durerà <b>Ticks*Cooldown secondi</b>\n" +
				"<b>Ticks=0</b> vuol dire che l'effetto dura <i>per sempre</i>\n", tipstyle);
		}
		else
			EditorGUILayout.LabelField("<i>Per questo effetto si possono ignorare <b>Ticks</b> e <b>Cooldown</b></i>", tipstyle);

		EditorGUILayout.Separator();
	
		e.effectScriptName = EditorGUILayout.TextField("Script Name", e.effectScriptName);
		if (isBadName(e.effectScriptName))
		{
			EditorGUILayout.HelpBox("E' possibile che per questo effetto tu non abbia ancora creato lo script o lo abbia chiamato in altro modo." +
				"Comunque, ricordati di settare opportunamente il nome dello script corrispondente qui sotto.", MessageType.Warning);
		}
		EditorGUILayout.LabelField("<i>Da modificare solo se non si è rispettata la convenzione</i>",tipstyle);
	}

	private bool isBadName(string s)
	{
		return String.IsNullOrEmpty(s) || referenced.GetType(s) == null || 
			!typeof(EffectScript).IsAssignableFrom(referenced.GetType(e.effectScriptName));
	}
}
