using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;

public enum PoolOptions
{
    /// <summary>
    /// Il pool nascerà con il numero specificato di oggetti, e non andrà oltre a quel numero.
    /// Se automanaged, nasce come array vuoto (tutti i valori null)
    /// E' il più efficiente.
    /// </summary>
    Static,

    /// <summary>
    /// Il pool nascerà con il numero specificato di oggetti, ma all'occorrenza si espanderà oltre.
    /// Se automanaged, nasce come lista vuota.
    /// E' il meno efficiente.
    /// </summary>
    Dynamic
}

public class PoolManager
{

    private static PoolManager instance;

    private Dictionary<GameObject, PrefabPool> pools;
    private Dictionary<int, PrefabPool> releaseMap;

    public static PoolManager SharedInstance()
    {
        if (instance == null)
            instance = new PoolManager();

        return instance;
    }

    private PoolManager()
    {
        this.pools = new Dictionary<GameObject, PrefabPool>();
        this.releaseMap = new Dictionary<int, PrefabPool>();
    }

    public GameObject GetFromPool(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            //TODO:
            //controlla impostazioni mediante asset o altra struttura dati
            //crea il pool con le impostazioni giuste

            CreatePool(prefab, 3); //test, massimo 3 oggetti, a default static e automanaged
        }

        return pools[prefab].Get();
    }

    public void CreatePool(GameObject prefab, int initialSize, PoolOptions opt=PoolOptions.Static, bool autoManaged=true)
    {
        pools[prefab] = PrefabPool.CreatePool(prefab, initialSize, this,  opt, autoManaged);

    }

    private void SetPoolForId(int id, PrefabPool pool)
    {
        this.releaseMap[id] = pool;
    }


    public void ReleaseToPool(GameObject go)
    {
        releaseMap[go.GetInstanceID()].Release(go);
    }

    #region Prefab Pool
    private abstract class PrefabPool
    {
        protected GameObject prefab;
        protected IList<GameObject> pool;

        protected PoolManager manager;


        public static PrefabPool CreatePool(GameObject prefab, int initialSize, PoolManager manager, PoolOptions opt, bool automanaged=true)
        {
            switch (opt)
            {
                case PoolOptions.Dynamic:
                    return new DynamicPool(prefab, initialSize, manager, automanaged);
                case PoolOptions.Static:
                    return new StaticPool(prefab, initialSize, manager, automanaged);
                default:
                    return null;
            }
        }


        public PrefabPool(GameObject prefab, int initialSize, PoolManager manager)
        {
            this.manager = manager;
            this.prefab = prefab;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public abstract GameObject Get();

        public virtual void Release(GameObject g)
        {
            if (pool.Contains(g))       //se ci fosse un modo per marcare gli oggetti e sapere one-shot se sono nel pool o meno sarebbe bello
                g.SetActive(false);     //se nel pool
            else
                GameObject.Destroy(g);  //se non nel pool (es: creato quando il pool era tutto usato e il pool è statico

        }

        public void Clear()
        {
            pool.Clear();
        }

    }

    private class StaticPool : PrefabPool
    {
        private int actualSize;
        private int maxSize;

        public StaticPool(GameObject prefab, int initialSize, PoolManager manager, bool autoManaged=true) : base(prefab, initialSize, manager)
        {
            this.pool = new GameObject[initialSize];
            maxSize = initialSize;
            if (!autoManaged)
            {
                for (int i = 0; i < initialSize; i++)
                {
                    pool[i] = GameObject.Instantiate(prefab);
                    pool[i].SetActive(false);
                    manager.SetPoolForId(pool[i].GetInstanceID(), this);
                }

                actualSize = initialSize;
            }
            else
                actualSize = 0;
        }

        public override GameObject Get()
        {
            foreach (GameObject g in pool)
            {
                if (g && !g.activeInHierarchy)
                {
                    g.SetActive(true);
                    return g;
                }
            }

            GameObject created = GameObject.Instantiate(prefab);
            if (actualSize<maxSize)
            {
                pool[actualSize] = created;
                actualSize++;
                manager.SetPoolForId(created.GetInstanceID(), this);
            }

            return created;
        }
    }

    private class DynamicPool : PrefabPool
    {

        public DynamicPool(GameObject prefab, int initialSize, PoolManager manager, bool autoManaged = true) : base(prefab, initialSize, manager)
        {
            this.pool = new List<GameObject>();
            
            if (!autoManaged)
            {
                for (int i=0; i<initialSize; i++)
                {
                    GameObject created = GameObject.Instantiate(prefab);
                    created.SetActive(false);
                    manager.SetPoolForId(created.GetInstanceID(), this);
                    pool.Add(created);
                }
            }
        }

        public override GameObject Get()
        {
            foreach (GameObject g in pool)
            {
                if (!g.activeInHierarchy)
                {
                    g.SetActive(true);
                    return g;
                }
            }

            GameObject created = GameObject.Instantiate(prefab);
            pool.Add(created);
            manager.SetPoolForId(created.GetInstanceID(), this);

            return created;
        }
    }
    #endregion
}