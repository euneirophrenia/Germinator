using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class NavigationNode : MonoBehaviour {

    private Dictionary<string, GameObject> UIChilds;
    private Stack<string> history;
    private NavigationPoint currentNavigationPoint;


    public NavigationPoint defaultNavigationPoint;    
    public bool inTransition = true, outTransition = false;

    public void Init()
    {
        currentNavigationPoint = defaultNavigationPoint;
        history = new Stack<string>();
        UIChilds = new Dictionary<string, GameObject>();

        LookForNavigationPoints();
        
        defaultNavigationPoint.gameObject.SetActive(true);
        defaultNavigationPoint.GetComponent<NavigationPoint>().FadeIn(inTransition);
	}

    private void LookForNavigationPoints()
    {
        foreach (NavigationPoint script in gameObject.GetComponentsInChildren<NavigationPoint>(true))
        {
            if (script != null)
            {
                UIChilds[script.ID] = script.gameObject;
                //Debug.Log("Name: " + script.Name + " GameObject: " + child.gameObject.name);
                script.gameObject.SetActive(false);
                script.Init();
            }
        }
    }

    public void navigateTo(string navigationPointID)
    {
        
        switch ( navigationPointID )
        {
            case "BACK":
                switchNavigationPoint(history.Pop());
                break;
            case "HOME":
                switchNavigationPoint(defaultNavigationPoint.GetComponent<NavigationPoint>().ID);
                break;
            default:
                switchNavigationPoint(navigationPointID);
                break;
        }
        
    }
    
    private void switchNavigationPoint(string navigationPointID)
    {
        try
        {
            GameObject newNavigationPoint = UIChilds[navigationPointID];
            if (newNavigationPoint == currentNavigationPoint && newNavigationPoint != null) { return; }
            history.Push(currentNavigationPoint.GetComponent<NavigationPoint>().ID);
            Debug.Log("Node: " + gameObject.name + "\tHistory: " + String.Join(", ", history.ToArray()));
            currentNavigationPoint.GetComponent<NavigationPoint>().FadeOut(outTransition);
            currentNavigationPoint = newNavigationPoint.GetComponent<NavigationPoint>();
            currentNavigationPoint.GetComponent<NavigationPoint>().FadeIn(inTransition);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
   
    //TODO eliminare, temporanea
    public void quitGame()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor 
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

}
