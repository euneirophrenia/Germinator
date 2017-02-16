using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class NavigationLink : MonoBehaviour {


    public NavigationNode navigationNode;
    public string navigationPointID;
    public AnimatorController animator;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(onClick);
        if (animator != null)
        {
            Animator = animator;
        }


    }

    void onClick()
    {
        navigationNode.navigateTo(navigationPointID);
    }

    public AnimatorController Animator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
            if( gameObject.GetComponent<Animator>() == null)
            {
                gameObject.AddComponent<Animator>();
            }
            gameObject.GetComponent<Animator>().runtimeAnimatorController = value;
        }
    }

}
