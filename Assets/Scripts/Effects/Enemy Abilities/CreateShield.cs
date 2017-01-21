using UnityEngine;

public class CreateShield : Ability {

    public int maxHP;
    public float radius;
    public GameObject shieldPrefab;
   
    private GameObject shieldInstance;
    private Vector3 scale;

    public override bool Do()
    {
        if (shieldInstance != null)
            return false;

        shieldInstance = Instantiate(shieldPrefab, this.transform);
        shieldInstance.transform.localPosition = Vector3.zero;
        shieldInstance.transform.localScale = scale;
        shieldInstance.GetComponent<Hittable>().HP = maxHP;
        return true;
    }

    public void Start()
    {
        scale = new Vector3(radius, radius, radius);
        this.activeCoolDown = timeBetweenActivations;
    }

}
