using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    private static GameManager instance;
    private LevelManager levelManager;

	// Use this for initialization
	void Start () 
    {
        instance = this;

        levelManager = LevelManager.GetInstance();
        levelManager.InitLevelManager("Level_0");
        levelManager.StartSpawning();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public static GameManager GetInstance()
    {
        return instance;
    }
}
