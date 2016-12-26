using UnityEngine;
using System.Collections.Generic;

public class AOEHitter : Hitter {

    public float radius=10;


    public override List<GameObject> getTargets(Collision collision)
    {
        List<GameObject> res = new List<GameObject>();
        foreach (Collider c in Physics.OverlapSphere(collision.transform.position, radius))
        {
            res.Add(c.gameObject);
        }

        return res;
    }


}
