using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour {

    public TowerBluePrint blueprint;
    private TowerBluePrint[] upgrades;


	void OnMouseDown()
    {
        /*if (upgrades==null) 
                this.upgrades = upgrader.GetUpgrades(this.blueprint); //e l'upgrader si decide se è una classe statica o GameObject condiviso
            Show una preview degli upgrades mediante bottoni UI

        ---- i bottoni UI alla pressione, controlleranno il CanBuild() del blueprint corrispondente, e se sì chiameranno
        Build(blueprintSelezionato);
        */
    }

    public void Build(TowerBluePrint blueprint)
    {
        //TODO: logica di sostituzione dei pezzi dell'upgrade
    }
}
