using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TowerBluePrint : ScriptableObject //creabile dall'editor, come prefab, non so se può servire o no
{
    public GameObject turretPrefab;
    public float cost; //magari int

    //Ulteriore roba grafica
    /*public qualcosa TurretPreview; //per esempio una piccola preview da mostrare all'utente
      public qualcosaltro EffettoUI; //effetto da mostrare quando si fa l'upgrade
     * .
     * .
     * .
     *  
     * */

    
    //METODI DI UTILITY, per ora mi viene in mente solo
    public bool CanBuild()
    {
        throw new NotImplementedException("TODO:Controllare il wallet dell'utente e rispondere di conseguenza");
    }

}

