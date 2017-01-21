using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;

public enum PoolOptions
{
    /// <summary>
    /// Il pool nascerà con il numero specificato di oggetti, e non andrà oltre a quel numero.
    /// Se automanaged, nasce come array vuoto (tutti i valori null).
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
    private PrefabPool releasePool;
	private static readonly Vector3 zero = new Vector3(0,0,0);


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

	/// <summary>
	/// Gets the instance of the prefab from pool.
	/// </summary>
	/// <returns>The Instance from pool.</returns>
	/// <param name="prefab">Prefab.</param>
	/// <param name="activate">If set to <c>true</c> the object is returned active.</param>
    /// <param name="position">The position to which spawn the object.</param>
    /// <param name="rotation">The rotation when spawned.</param>
	public GameObject GetFromPool(GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), bool activate=true)
    {
        if (!pools.ContainsKey(prefab))
        {
            //TODO:
            //controlla impostazioni mediante asset o altra struttura dati
            //crea il pool con le impostazioni giuste

            CreatePool(prefab, 3); //test, massimo 3 oggetti, a default static e automanaged
        }

        return pools[prefab].Get(position, rotation, activate);
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
        if (releaseMap.TryGetValue(go.GetInstanceID(), out releasePool))
            releasePool.Release(go);
        else
            GameObject.Destroy(go);
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
			

		public abstract GameObject Get(Vector3 position, Quaternion rotation, bool activate);

        public virtual void Release(GameObject g)
        {
           g.SetActive(false);     
        }

        public void Clear()
        {
            pool.Clear();
        }

    }

	#region Static
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
			{
                actualSize = 0;
			}
        }

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override GameObject Get(Vector3 position, Quaternion rotation, bool activate)
		{
            foreach (GameObject g in pool)
            {
                if (g && !g.activeInHierarchy)
                {
					g.transform.position=position;
					g.transform.rotation = rotation;
                    g.SetActive(activate);
                    return g;
                }
            } 

			GameObject created = GameObject.Instantiate(prefab);
			created.transform.position=position;
			created.transform.rotation=rotation;
			created.SetActive(activate);
			if (actualSize<maxSize)
			{
				pool[actualSize] = created;
				actualSize++;
				manager.SetPoolForId(created.GetInstanceID(), this);
			}

			return created;
        }
			
		public override void Release (GameObject g)
		{
			base.Release (g);
		}
    }
	#endregion

	#region Dynamic
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

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override GameObject Get(Vector3 position, Quaternion rotation, bool activate)
        {
            foreach (GameObject g in pool)
            {
                if (!g.activeInHierarchy)
                {
					g.transform.position=position;
					g.transform.rotation=rotation;
                    g.SetActive(activate);
                    return g;
                }
            }

            GameObject created = GameObject.Instantiate(prefab);
			created.transform.position=position;
			created.transform.rotation=rotation;
			created.SetActive(activate);
            pool.Add(created);
            manager.SetPoolForId(created.GetInstanceID(), this);

            return created;
        }
    }
	#endregion
    #endregion
}