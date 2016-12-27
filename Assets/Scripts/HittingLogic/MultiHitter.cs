using UnityEngine;
using System.Collections.Generic;

/* L'idea qui e' la balestra che penetra
 * 1 hit alla volta
 */
public class MultiHitter : Hitter { 

    public int maxHits = 5;

    public override List<GameObject> getTargets(Collider collision)
    {
        List<GameObject> res = new List<GameObject>();
        res.Add(collision.gameObject);
        return res;
    }

    public override void HandleHit(Collider coll)
    {
        maxHits--;
        destroy = maxHits == 0;
    }

}
