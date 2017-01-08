using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/* L'idea qui e' la balestra che penetra
 * 1 hit alla volta
 */
public class MultiHitter : Hitter { 

    public int maxHits = 5;

	public override void HandleHit(Hittable hit)
    {
        maxHits--;
        destroy = maxHits == 0;
    }

}
