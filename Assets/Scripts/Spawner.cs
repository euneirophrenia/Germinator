using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject target;

    private float cooldown = 0;
    private float activeCoolDown = 0;
    private Group group = null;

    private int enemiesLeft = 0;
    private NavMeshPath cached;

    void Start()
    {
        cached = new NavMeshPath();
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.CalculatePath(target.transform.position, cached);
        Destroy(agent);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (activeCoolDown <= 0)
        {
            this.Spawn();
            activeCoolDown = cooldown;
            enemiesLeft--;
            return;
        }
        else
        {
            activeCoolDown -= Time.deltaTime;
        }

        if (enemiesLeft <= 0)
            this.NextGroup();
        
    }

    private void Spawn()
    {
        //Trick per gestire TimeBeforeGroupSpawn
        this.cooldown = group.TimeBetweenEnemies;

        var enemyToSpawn = PoolManager.SharedInstance().GetFromPool(group.Enemy, this.transform.position);

        NavMeshAgent agent = enemyToSpawn.GetComponent<NavMeshAgent>();
        if (cached.status != NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.SetPath(cached);
        }
        
    }

    private void NextGroup()
    {
        LevelManager.GetInstance().CurrentGroupSpawnEndHandler();
        this.enabled = false;
    }

    public void InitSpawner(Group group)
    {
        this.group = group;

        //Trick per gestire TimeBeforeGroupSpawn
        this.cooldown = group.TimeBeforeGroupSpawn;
        this.activeCoolDown = this.cooldown;
        this.enemiesLeft = group.Count;

        this.enabled = true;
    }
    
}
