using UnityEngine;
/*
 * Classe contenete le nostre estensioni per le classi di unity. 
 * 
 */

public static class Extensions
{
    private static PoolManager poolManager = PoolManager.SharedInstance();

    /// <summary>
    /// Rilascia il gameObject al suo pool.
    /// Causa distruzione se il gameObject non appartiene a nessun pool.
    /// </summary>
    public static void Release(this GameObject go)
    {
        poolManager.ReleaseToPool(go);
    }
}
