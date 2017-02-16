using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image barImage;
    public Text barText;

    private List<LoadingItem> items;
    private int index;

	void Start ()
    {
        resetBar();
    }

    private void resetBar()
    {
        index = 0;
        barImage.fillAmount = 0;
        barText.text = "";
        if( items == null)
        {
            items = new List<LoadingItem>();
        }
    }   

    private void calculateWeights()
    {
        float sumWeight = items.Sum(item => item.weight);
        
        foreach( LoadingItem item in items)
        {
            item.weight = item.weight / sumWeight; // sumWeight : 1 = item.weight : X
        }
    }

    public void Next()
    {
        if( HasNext() ) //Per evitare index errors
        {
            barText.text = items[index].text + " ...";
            barImage.fillAmount += items[index].weight;
            index++;
        }
    }

    public bool HasNext()
    {
        if (barImage != null && items != null && index < items.Count)
        {
            return true;
        }
        return false;
    }

    public List<LoadingItem> Items
    {
        get { return this.items; }
        set
        {
            resetBar();
            this.items = value;
            calculateWeights();
        }
    }
}

public class LoadingItem
{
    public string text;
    public float weight;

    public LoadingItem(string text, float weight)
    {
        this.text = text;
        this.weight = weight;
    }
}
