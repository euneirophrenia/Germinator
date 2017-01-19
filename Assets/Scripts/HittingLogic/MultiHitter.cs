using UnityEngine;

/* L'idea qui e' la balestra che penetra
 * 1 hit alla volta
 */
public class MultiHitter : Hitter { 

    public int maxHits = 5;
    public int hitsLeft;

	public override void HandleHit(Hittable hit)
    {
        hitsLeft--;
        destroy = hitsLeft == 0;
    }

    public void OnEnable()
    {
        hitsLeft = maxHits;
    }

}
