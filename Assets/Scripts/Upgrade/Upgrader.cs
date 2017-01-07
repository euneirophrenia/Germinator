using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Upgrader
{ 
    //Se mi viene in mente qualche cosa di condiviso tra l'eager e il lazy trasformo questa interfaccia in classe astratta
    TowerBluePrint[] GetUpgrades(TowerBluePrint tower);


}
