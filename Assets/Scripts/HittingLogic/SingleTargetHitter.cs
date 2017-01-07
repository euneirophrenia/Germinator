using UnityEngine;
using System.Collections.Generic;

public class SingleTargetHitter : Hitter {

	public override IEnumerable<Hittable> getTargets(Collider collision)
    {
        Hittable h = collision.gameObject.GetComponent<Hittable>();
        if (h == null)
            yield break;
        yield return h;

    }

}
