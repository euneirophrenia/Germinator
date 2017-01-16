using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour {

    public GameObject what;
    public GameObject target;
    public int howMany;
    public float deltaTime;

    private float countdown=0;


	void Update () {

        if (countdown <= 0 && howMany>0)
        {
            GameObject created =Instantiate(what, this.transform.position, Quaternion.identity);
            created.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            countdown = deltaTime;
            howMany--;
            return;
        }
        countdown -= Time.deltaTime;
	}
}
