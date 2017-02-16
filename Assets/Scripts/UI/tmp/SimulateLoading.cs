using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateLoading : MonoBehaviour {

    public LoadingBar loadingBar;
    public NavigationNode navigationNode;
    public string navigateTo;

	void Start ()
    {
        List<LoadingItem> items = new List<LoadingItem>();
        items.Add(new LoadingItem("Killing bacterias", 2));
        items.Add(new LoadingItem("Killing humans", 1));
        items.Add(new LoadingItem("Git add", 3));
        items.Add(new LoadingItem("Git commit", 2));
        items.Add(new LoadingItem("Git push", 2));

        loadingBar.Items = items;
        StartCoroutine(sim());
	}
	
	IEnumerator sim()
    {
        yield return new WaitForSeconds(Random.Range(0.8f, 2.2f));
        while( loadingBar.HasNext() )
        {
            loadingBar.Next();
            yield return new WaitForSeconds(Random.Range(0.8f, 2.2f));
        }
        navigationNode.navigateTo(navigateTo);
    }
}
