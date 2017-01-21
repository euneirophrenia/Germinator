using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour {

    public GameObject what;
    public GameObject target;
    public int howMany;
    public float deltaTime;

    private float countdown=0;
    private NavMeshPath cached;
	private PoolManager pool;

    public void Start()
    {
        cached = new NavMeshPath();
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.CalculatePath(target.transform.position, cached);
        Destroy(agent); 

		pool = PoolManager.SharedInstance();
		pool.CreatePool(what, 6, PoolOptions.Static);
    }


	void Update () {

        if (countdown <= 0 && howMany>0)
        {
			GameObject created = pool.GetFromPool(what,this.transform.position);

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
