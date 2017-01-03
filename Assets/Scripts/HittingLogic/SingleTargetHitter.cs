﻿using UnityEngine;
using System.Collections.Generic;

public class SingleTargetHitter : Hitter {

	public override IEnumerable<GameObject> getTargets(Collider collision)
    {
        List<GameObject> res =new List<GameObject>();
        res.Add(collision.gameObject);
        return res;
    }

}
