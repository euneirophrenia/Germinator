using UnityEngine;

public class ShieldScript : MonoBehaviour {

    private GameObject parent;

    // Use this for initialization
    public void Start()
    {
        parent = this.transform.parent.gameObject;
        parent.layer = CustomLayerEnum.Shielded;
    }

    public void OnDestroy()
    {
        parent.layer = CustomLayerEnum.Enemy;
    }
}
