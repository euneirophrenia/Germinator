using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour {

    public TowerBlueprint blueprint;
    private TowerBlueprint[] upgrades;


	void OnMouseDown()
    {
        /*if (upgrades==null) 
                this.upgrades = upgrader.GetUpgrades(this.blueprint); //e l'upgrader si decide se è una classe statica o GameObject condiviso
            Show una preview degli upgrades mediante bottoni UI

        ---- i bottoni UI alla pressione, controlleranno il CanBuild() del blueprint corrispondente, e se sì chiameranno
        Build(blueprintSelezionato);
        */
    }

    public void Build(TowerBlueprint blueprint)
    {
		Destroy(this.gameObject);

		//TODO: save params

		Instantiate(blueprint.turretPrefab, this.transform.position, this.transform.rotation);

		//TODO: set params
	}
}
