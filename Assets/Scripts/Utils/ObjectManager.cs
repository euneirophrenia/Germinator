using System.Collections.Generic;
using UnityEngine;

//Questo è solo un passacarte, si può direttamente usare PrefabPool
public class ObjectManager
{

    private static ObjectManager instance;

    private Dictionary<string, PrefabPool> pools;

    public static ObjectManager SharedInstance()
    {
        if (instance == null)
            instance = new ObjectManager();

        return instance;
    }

    private ObjectManager()
    {
        this.pools = new Dictionary<string, PrefabPool>();
    }

    public GameObject GetFromPool(string name)
    {
        return pools[name].Get();
    }

    public void CreatePool(string name, GameObject prefab, int initialSize, bool dynamic=true)
    {
        pools[name] = new PrefabPool(prefab, initialSize, dynamic);
    }

    public void Clear(string name)
    {
        pools[name].Clear();
    }

    public void ReleaseToPool(string name, GameObject go)
    {
        pools[name].Release(go);
    }

}


public class PrefabPool
{
    public GameObject prefab;
    public IList<GameObject> pool;
    public readonly bool dynamic;

    /// <summary>
    /// Crea un pool di GameObject da prefab, inattivi nella scena, ma pronti.
    /// </summary>
    /// <param name="prefab">Il prefab di cui creare un pool.</param>
    /// <param name="initialSize">La dimensione iniziale del pool.</param>
    /// <param name="dynamic">Se false il pool non si espande, ma è molto più efficiente se la dimensione è giusta.</param>
    public PrefabPool(GameObject prefab, int initialSize, bool dynamic=true)
    {
        this.prefab = prefab;
        this.dynamic = dynamic;
        if (dynamic)
            pool = new List<GameObject>();
        else 
            pool = new GameObject[initialSize];

        for (int i = 0; i < initialSize; i++)
        {
            pool[i]=(GameObject.Instantiate(prefab));
            pool[i].SetActive(false);
        }
    }

    public GameObject Get()
    {
        foreach(GameObject g in pool)
        {
            if (!g.activeInHierarchy)
            {
                g.SetActive(true);
                return g;
            }
        }

        GameObject created = GameObject.Instantiate(prefab);

        if (dynamic) {
            pool.Add(created);
        }
        return created;
    }

    public void Release(GameObject g)
    {
        g.SetActive(false);
   
    }

    public void Clear()
    {
        pool.Clear();
    }
}
