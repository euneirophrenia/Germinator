using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour {

    public GameObject what;
    public GameObject target;
    public int howMany;
    public float deltaTime;

    private float countdown=0;
    private NavMeshPath cached;

    public void Start()
    {
        cached = new NavMeshPath();
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.CalculatePath(target.transform.position, cached);
        Destroy(agent); 
    }


	void Update () {

        if (countdown <= 0 && howMany>0)
        {
            GameObject created =Instantiate(what, this.transform.position, Quaternion.identity);
            NavMeshAgent agent = created.GetComponent<NavMeshAgent>();
            if (cached.status != NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(target.transform.position);
            }
            else
            {
                agent.SetPath(cached);
            }
            countdown = deltaTime;
            howMany--;
            return;
        }
        countdown -= Time.deltaTime;
	}
}
