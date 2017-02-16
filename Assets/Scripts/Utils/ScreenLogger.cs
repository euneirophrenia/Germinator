using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


//FIXME
public class ScreenLogger : MonoBehaviour {

    private static Dictionary<GameObject, string> map = new Dictionary<GameObject, string>();
    private Rect rect;
    private GUIStyle style;
	

	void Start ()
    {
        int w = Screen.width, h = Screen.height;
        style = new GUIStyle();
        rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    }

    public static void Append(GameObject g, string text)
    {
        map[g] = text;
    }

    public static void Remove(GameObject g)
    {
        map.Remove(g);
    }

    void OnGUI()
    {
        StringBuilder text = new StringBuilder();
        foreach (GameObject g in map.Keys)
        {
            text.Append("[");
            text.Append(g.name);
            text.Append("]");
            text.Append(map[g]);
        }
        GUI.Label(rect, text.ToString(), style);
    }
}
