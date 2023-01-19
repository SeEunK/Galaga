using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;


    public float spawnRateMin = 4.5f;
    public float spawnRateMax = 5.0f;

    public List<Transform> targetTranfs;
    private float spawnRate = default;
    private float timeAfterSpawn = default;
   
    private GameObject[] enemyPool;
    private int currIndex = 0;

    public GameObject player;

    public List<Transform> startTransfs;
    public int dieCount;
    public int maxCountEnemyType01 = 5;

    public int maxCountEnemyType02 = 3;

    // Start is called before the first frame update
    void Start()
    {
       
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        
        // --> secene object all search 

        int maxenemyCount = maxCountEnemyType01 + maxCountEnemyType02;
        enemyPool = new GameObject[maxenemyCount];

        for (int i = 0; i< maxCountEnemyType01; i++)
        {
            GameObject enemyObject = Instantiate(enemyPrefabs[0]);
           
            enemyPool[i] = enemyObject;
            enemyPool[i].GetComponent<Enemy>().MoveTargetTranf = targetTranfs[i];
            enemyPool[i].GetComponent<Enemy>().targetTranf = player.transform;
            enemyObject.SetActive(false);

        }
        
    } 

    // Update is called once per frame
    void Update()
    {
        if (currIndex >= 5)
        {
            return;
        }

        timeAfterSpawn +=Time.deltaTime;
        if(timeAfterSpawn >= spawnRate)
        {
            
            timeAfterSpawn = 0f;
            enemyPool[currIndex].transform.position = startTransfs[0].position;
            
            enemyPool[currIndex].SetActive(true);
            
            
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            currIndex++;
        
        }
    }
    
}
