using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;



public class NavigationPoint : MonoBehaviour {

    public string ID;
    public AnimatorController defaultAnimator;

    private List<GameObject> animatedChilds;
    private enum Trigger { TransitionIN, TransitionOUT };

    public void Init()
    {
        animatedChilds = new List<GameObject>();
        initializeChildPoints();
        
    }

    private void initializeChildPoints()
    {
        foreach (NavigationLink childScript in gameObject.GetComponentsInChildren<NavigationLink>())
        {
            animatedChilds.Add(childScript.gameObject);
            if (childScript.Animator == null)
            {
                childScript.Animator = defaultAnimator;
                //Debug.Log("Attached animator to: " + childScript.gameObject.name);
            }
            else
            {
                Debug.Log("Skipped! Child already has an animator set!");
            }
        }
        
    }

    public void FadeOut( bool playTransition)
    {   
        if(playTransition)
        {
            makeTransition(Trigger.TransitionOUT);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void FadeIn( bool playTransition)
    {
        gameObject.SetActive(true);

        if (playTransition)
        {
            makeTransition(Trigger.TransitionIN);
        }
    }

    private void makeTransition(Trigger trigger)
    {
        float animDuration = 0f;
        AnimatorClipInfo[] clipInfo;
        foreach (GameObject child in animatedChilds)
        {
            Animator animator = child.GetComponent<Animator>();
            animator.SetTrigger(trigger.ToString());
            if (animDuration == 0f)
            {
                clipInfo = animator.GetCurrentAnimatorClipInfo(0);
                animDuration = clipInfo.Length == 0 ? 0f : clipInfo[0].clip.length;
            }
            if (trigger.Equals(Trigger.TransitionOUT))
            {
                StartCoroutine(timedHide(animDuration));
            }
        }
        
    }

    private IEnumerator timedHide(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
