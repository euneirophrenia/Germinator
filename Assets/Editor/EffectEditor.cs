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
			EditorGUILayout.LabelField("L'effetto durerà <b>Ticks*Cooldown secondi</b>\n",tipstyle);
			if (e.Ticks==0)
				EditorGUILayout.HelpBox("Ticks=0 provoca un effetto che dura per sempre.", MessageType.Info);
		}
		else if (!isBadName(e.effectScriptName))
			EditorGUILayout.LabelField("Per questo effetto si possono ignorare <b>Ticks</b> e <b>Cooldown</b>", tipstyle);
		else
			EditorGUILayout.LabelField("Non so dirti quali parametri occorrano perché non trovo lo script.", tipstyle);

		EditorGUILayout.Separator();
	
		e.effectScriptName = EditorGUILayout.TextField("Script Name", e.effectScriptName);
		if (isBadName(e.effectScriptName))
		{
			EditorGUILayout.HelpBox("E' possibile che per questo effetto tu non abbia ancora creato lo script o lo abbia chiamato in altro modo." +
				"\nComunque, ricordati di settare opportunamente il nome dello script corrispondente.\n" +
				"Se si è rispettata la convenzione (<NomeEffetto>+\"Script\"), il nome dovrebbe comparire automaticamente.", MessageType.Warning);
			UpdateScripts();

			EditorGUILayout.Separator();
			string message = "<b>Effetti attualmente implementati</b>\n";
			foreach (string s in scripts)
			{
				message+="\n\t<i>"+s+"</i>\n";
			}
			EditorGUILayout.LabelField(message, tipstyle);
		}
	}

	private bool isBadName(string s)
	{
		return String.IsNullOrEmpty(s) || referenced.GetType(s) == null || 
			!typeof(EffectScript).IsAssignableFrom(referenced.GetType(e.effectScriptName));
	}

	private void UpdateScripts()
	{
		scripts=(from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s=>s.GetTypes())
			where typeof(EffectScript).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface
			select x.Name).ToArray();
	}
}
