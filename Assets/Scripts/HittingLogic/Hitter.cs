using UnityEngine;
using System.Collections.Generic;

public abstract class Hitter : MonoBehaviour {

    public List<Effect> effects = new List<Effect>();

    public List<string> hits;

    protected bool destroy = false;

    void Start()
    {
        Effect e = new SlowEffect();
        e.effectiveness = 0.5f;
        effects.Add(e);
        
    }

    void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.GetComponent<Hittable>() == null)
        //    return;
        List<GameObject> targets = getTargets(collision);
       
        foreach (GameObject g in targets)
        {
            Hittable other = g.GetComponent<Hittable>();
            if (other != null && hits.Contains(g.tag))
            {
                other.Proc(effects);
                HandleHit(collision); //per cose tipo consentire hit consecutivi  
            }
        }
        if (destroy)
        {
            Destroy(this.gameObject);
        }

        

    }

    public abstract List<GameObject> getTargets(Collider collision);

    public virtual void HandleHit(Collider coll) //il multihit lo ovverrida
    {
        destroy = true;
    }
}
