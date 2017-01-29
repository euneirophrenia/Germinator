using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DBManager 
{
    private static DBManager instance = null;

    public static DBManager GetInstance()
    {
        if (instance == null)
            instance = new DBManager();

        return instance;
    }

    public T LoadAsset<T>(string assetPath) where T : class
    {
        return Resources.Load(assetPath, typeof(T)) as T;
    }
}
