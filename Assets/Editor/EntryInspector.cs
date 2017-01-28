using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;


//fa si' che il tipo nelle efficacie di hittable si possa scegliere solo tra valori leciti
//è tutto codice che gira solo nell'editor, e non a run-time, quindi relax se pure è poco efficiente


[CustomPropertyDrawer(typeof(SensibilityDictionary.Entry))]
public class EntryInspector : PropertyDrawer 
{
	private int _choice=0;

	private string[] choices = (from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s=>s.GetTypes())
			                         where typeof(Effect).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface
		                                 select x.Name).ToArray();

	public int choiceIndex {
		set {
			_choice= value>0? value : 0;
		}
		get{
			return _choice;
		}

	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		UpdateChoices();
        EditorGUI.BeginProperty (position, label, property);

		// Disegna il nome del campo
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		choiceIndex = Array.IndexOf(choices, property.FindPropertyRelative("type").stringValue);
	

		Rect typeRect = new Rect(position.x, position.y, 100, position.height);
		Rect valueRect = new Rect (position.x+105, position.y, 30, position.height);

		choiceIndex = EditorGUI.Popup(typeRect, choiceIndex, choices);
		property.FindPropertyRelative("type").stringValue=choices[choiceIndex];
       
        EditorGUI.PropertyField (valueRect, property.FindPropertyRelative ("effectiveness"), GUIContent.none);
        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty ();
        
    }

	private void UpdateChoices()
	{
        choices = (from x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                   where typeof(Effect).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface
                   select x.Name).ToArray();
	}

}
