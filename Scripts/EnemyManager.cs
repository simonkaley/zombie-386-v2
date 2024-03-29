using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitPool))]
public class EnemyManager : MonoBehaviour
{
    private KillTracker killTracker;

    [SerializeField]
    int maxEnemiesSpawned = 100, spawnsPerSecondMin = 1, spawnsPerSecondMax = 5, spawnsPerSecond=1, spawnsPerSecondSubtract=0, difficultySpikes=20;
    [SerializeField]
    public bool usePooling = false;
    [SerializeField]
    GameObject enemyPrefab;
    public UnitPool pool {get; protected set;}
    float timer = 0;
    int curSpawned = 0;//Should increment up to maxEnemiesSpawned
    // Start is called before the first frame update
    void Start()
    {
        //pool = GetComponent<UnitPool>();
        spawnsPerSecond=Random.Range(spawnsPerSecondMin,spawnsPerSecondMax);

        killTracker = FindObjectOfType<KillTracker>();
    }

    GameObject SpawnEnemy()
    {
        GameObject go;
        if(usePooling)
        {
            go = pool.pool.Get();
        }
        else
        {
            go = Instantiate(enemyPrefab, transform);
        }
        //Todo: how could we alter this so enemies always spawn around the player?
        Vector3 currentPosition = transform.position;
        if (go != null)
        {
            go.transform.position = new Vector3(currentPosition.x,currentPosition.y);//new Vector3(Random.Range(-8, 8), Random.Range(-8, 8));
        }
        return go;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(curSpawned < maxEnemiesSpawned && timer > spawnsPerSecond)
        {
            SpawnEnemy();
            timer = 0;
            spawnsPerSecond=Random.Range(spawnsPerSecondMin,spawnsPerSecondMax-spawnsPerSecondSubtract);
        }
        if (spawnsPerSecondSubtract<spawnsPerSecondMax-1)
        {
            spawnsPerSecondSubtract=(killTracker.getKills()/difficultySpikes);
        }
        
    }
}
