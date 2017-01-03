using UnityEngine;
using System.Collections.Generic;

/* L'idea qui e' la balestra che penetra
 * 1 hit alla volta
 */
public class MultiHitter : Hitter { 

    public int maxHits = 5;

	public override IEnumerable<Hittable> getTargets(Collider collision)
    {
		yield return collision.gameObject.GetComponent<Hittable>();
    }

    public override void HandleHit(Collider coll)
    {
        maxHits--;
        destroy = maxHits == 0;
    }

}
