using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vexe.Runtime.Types;

//Non riesco ad utilizzare le properties per serializzare

[Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "Add Level")]
public class Level : BaseScriptableObject
{
    public List<Wave> Waves = new List<Wave>();

    /// <summary>
    /// Multiplier validi per tutto il livello
    /// </summary>
    [SerializeField]
    public List<Modifier> Modifiers = new List<Modifier>();

    /// <summary>
    /// Proprietà generali del livello (es Gold iniziali)
    /// </summary>
    [SerializeField]
    public List<GenericProperty> Properties = new List<GenericProperty>();
}

[Serializable]
public class Modifier 
{
    /// <summary>
    /// Nome della classe da istanziare
    /// </summary>
    [SerializeField]
    public string ModifierType;

    [SerializeField]
    public float Effectiveness;
}

[Serializable]
public class GenericProperty
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public string Value;
	/* il gestore deve castare nella set al tipo giusto*/
}

[Serializable]
public class Wave
{
    [SerializeField]
    public List<Group> Groups = new List<Group>();
}

[Serializable]
public class Group
{
    /// <summary>
    /// Prefab/Reference al nemico da spawnare
    /// </summary>
    [SerializeField]
    public GameObject Enemy;

    /// <summary>
    /// Numero di secondi tra un Enemy e l'altro all'interno dello stesso Group
    /// </summary>
    [SerializeField]
    public int TimeBetweenEnemies;

    /// <summary>
    /// Numero di Enemy all'interno del Group
    /// </summary>
    [SerializeField]
    public int Count;

    /// <summary>
    /// Numero di secondi di offset atteso prima di iniziare lo spawn del Group
    /// </summary>
    [SerializeField]
    public int TimeOffset;
}

//public class Enemy
//{
//    public string Name { get; set; }
//}


//Livelli realizzati come scene "prefabbricate". 
//Contengono un oggetto "Level" con uno script attaccato che si occupa di recuperare la struttura LevelWaves relativa al livello corrente.
//La struttura viene memorizzata, ma vengono calcolati anche due ipotetici Stack "EnemiesToSpawn" e "TimeToWait", con i dati relativi alla prima wave.
//Lo script si occupa nell'update di fare pop di "TimeToWait" e aspettare quel tempo. Fatto questo fa pop di "EnemiesToSpawn" e spawna il nemico indicato.
//Terminati gli stack (con attenzione alla congrua lunghezza tra i due) si rimane in attesa che la GUI o il timer invochi il metodo "NextWave", = scatti l’evento NextWave
//il quale riempie gli stack con i dati relativi alla nuova wave
