using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/* L'idea qui e' la balestra che penetra
 * 1 hit alla volta
 */
public class MultiHitter : Hitter { 

    public int maxHits = 5;

	public override IEnumerable<Hittable> getTargets(Collider collision)
    {
		Hittable h= collision.gameObject.GetComponent<Hittable>();
        if (h == null)
            yield break;
        yield return h;
    }

    public override void HandleHit(Collider coll)
    {
        if (didHit)
        {
            maxHits--;
            destroy = maxHits == 0;
        }
    }

}
