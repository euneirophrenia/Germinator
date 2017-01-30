using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
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


    /// <summary>
    /// Inefficiente come la merda.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public static GameObject[] FindByRegex(this GameObject go, Regex r)
    {
        return (from GameObject g in GameObject.FindObjectsOfType<GameObject>()
                where r.IsMatch(go.name)
                select g).ToArray();
    }
}
