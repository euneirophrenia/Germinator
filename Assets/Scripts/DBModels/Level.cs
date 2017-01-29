using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vexe.Runtime.Types;

[Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "Add Level")]
public class Level : BaseScriptableObject
{
    public Wave[] Waves;

    /// <summary>
    /// Multiplier validi per tutto il livello
    /// </summary>
    public Modifier[] Modifiers;

    /// <summary>
    /// Proprietà generali del livello (es Gold iniziali)
    /// </summary>
    public GenericProperty[] Properties;
}

[Serializable]
public class Modifier 
{
    /// <summary>
    /// Nome della classe da istanziare
    /// </summary>
    public string ModifierType;

    public float Effectiveness;
}

[Serializable]
public class GenericProperty
{
    public string Name;

    public string Value;
	/* il gestore deve castare nella set al tipo giusto*/
}

[Serializable]
public class Wave
{
    public Group[] Groups;
}

[Serializable]
public class Group
{
    /// <summary>
    /// Indice di tempo in cui spawnare il gruppo
    /// </summary>
    public int TimeIndex;

    /// <summary>
    /// Nome dello spawner da cui spawnare il gruppo
    /// </summary>
    public string Spawner;

    /// <summary>
    /// Prefab del nemico da spawnare
    /// </summary>
    public GameObject Enemy;

    /// <summary>
    /// Numero di secondi tra un Enemy e l'altro all'interno dello stesso Group
    /// </summary>
    public float TimeBetweenEnemies;

    /// <summary>
    /// Numero di Enemy all'interno del Group
    /// </summary>
    public int Count;

    /// <summary>
    /// Numero di secondi di offset atteso prima di iniziare lo spawn del Group
    /// </summary>
    public float TimeBeforeGroupSpawn;

    
}

//Livelli realizzati come scene "prefabbricate". 
//Contengono un oggetto "Level" con uno script attaccato che si occupa di recuperare la struttura LevelWaves relativa al livello corrente.
//La struttura viene memorizzata, ma vengono calcolati anche due ipotetici Stack "EnemiesToSpawn" e "TimeToWait", con i dati relativi alla prima wave.
//Lo script si occupa nell'update di fare pop di "TimeToWait" e aspettare quel tempo. Fatto questo fa pop di "EnemiesToSpawn" e spawna il nemico indicato.
//Terminati gli stack (con attenzione alla congrua lunghezza tra i due) si rimane in attesa che la GUI o il timer invochi il metodo "NextWave", = scatti l’evento NextWave
//il quale riempie gli stack con i dati relativi alla nuova wave
