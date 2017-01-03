using UnityEngine;
using System.Collections.Generic;

public class SingleTargetHitter : Hitter {

	public override IEnumerable<Hittable> getTargets(Collider collision)
    {
		yield return collision.gameObject.GetComponent<Hittable>();
        
    }

}
