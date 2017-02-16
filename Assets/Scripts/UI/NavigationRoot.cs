using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationRoot : MonoBehaviour {

    private Dictionary<int, List<NavigationNode>> nodes;
    void Start ()
    {
        nodes = new Dictionary<int, List<NavigationNode>>();
        lookForNodes( ref nodes, gameObject, 0);
        foreach( int index in new List<int>(nodes.Keys))
        {
            foreach( NavigationNode node in nodes[index])
            {
                node.Init();
            }
        }
	}

    private void lookForNodes(ref Dictionary<int, List<NavigationNode>> result, GameObject gameObject, int depth)
    {
        Debug.Log("Depth: " + depth);
        foreach( Transform t in gameObject.transform )
        {
            NavigationNode node = t.gameObject.GetComponent<NavigationNode>();
            if ( node != null )
            {
                if( !result.ContainsKey(depth) ) { result[depth] = new List<NavigationNode>(); }
                result[depth].Add(node);
                Debug.Log("Added NavigationNode: " + node.gameObject.name + "\tDepth: " + depth);
            }
            else
            {
                lookForNodes(ref result, gameObject, depth + 1);
            }
        }
        
    }
}
