using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class LevelManager
{
    private Level level;
    private int WaveIndex = 0;
    private int TimeIndex = 0;
    private GameObject[] spawners;
    private int spawnersCallbackNumber = 0;
    private Group[] groupsToSpawn;


    private static LevelManager instance = null;

    public static LevelManager GetInstance()
    {
        if (instance == null)
            instance = new LevelManager();

        return instance;
    }

	private LevelManager() 
    {
        
	}

    public void InitLevelManager(string levelAssetName)
    {
        //TODO generare file di settings o path di un asset hardcoded contenente le settings?
        var assetFolder = "Levels";

        level = DBManager.GetInstance().LoadAsset<Level>(assetFolder + "/" + levelAssetName);

        var usedPrefabs = (from wave in level.Waves
                           from gr in wave.Groups
                           select gr.Enemy).Distinct().ToList();

        foreach (var pref in usedPrefabs)
        {
            PoolManager.SharedInstance().CreatePool(pref, 0, PoolOptions.StackBased);
        }

        spawners = GameObject.FindGameObjectsWithTag("Spawner").OrderBy(g => g.name).ToArray();

        //TODO capire come gestire ------------
        handleProperties(level.Properties);
        handleModifiers(level.Modifiers);
        //------------------------------------

        NextGroupsToSpawn();
    }

    public void StartSpawning()
    {
        if (level == null)
            throw new Exception("Level configuration not loaded!");

        SetGroupsInSpawners();
    }

    public void CurrentGroupSpawnEndHandler()
    {
        spawnersCallbackNumber--;

        if (spawnersCallbackNumber <= 0)
        {
            TimeIndex++;
            NextGroupsToSpawn();
            SetGroupsInSpawners();
        }
    }

    public void SetGroupsInSpawners()
    {
        if (groupsToSpawn.Length <= 0)
        {
            //TODO chiamare GUI per avvertire che la wave è finita
            Debug.Log("Wave End!");
            return;
        }

        for (int i = 0; i < spawners.Length; i++)
		{
            //N.B. Supportato solo un gruppo per TimeIndex sullo stesso Spawner
            var group = groupsToSpawn.Where(g => g.Spawner == spawners[i].name).FirstOrDefault();

            if (group != null)
            {
                spawners[i].GetComponent<Spawner>().InitSpawner(group);
            }
            else
            {
                spawners[i].GetComponent<Spawner>().enabled = false;
                spawnersCallbackNumber--;
            }
		}
    }

    public void NextWave()
    {
        WaveIndex++;
        TimeIndex = 0;
        NextGroupsToSpawn();
        SetGroupsInSpawners();
    }

    public void NextGroupsToSpawn()
    {
        spawnersCallbackNumber = spawners.Length;
        this.groupsToSpawn = level.Waves[WaveIndex].Groups.Where(g => g.TimeIndex == TimeIndex).ToArray();
    }

    public void handleProperties(GenericProperty[] properties)
    {
        //TODO da implementare
    }

    public void handleModifiers(Modifier[] modifiers)
    {
        //TODO da implementare
    }

}
