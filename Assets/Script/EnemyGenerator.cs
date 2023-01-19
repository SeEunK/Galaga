using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 1.0f;

    public Transform targetTranf;
    private float spawnRate = default;
    private float timeAfterSpawn = default;
   
    private GameObject[] enemyPool;
    private int currIndex = 0;
    

    public Transform startTransf;
   

    // Start is called before the first frame update
    void Start()
    {
        startTransf = gameObject.transform;
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        
        // --> secene object all search 

        enemyPool = new GameObject[10];

        for(int i = 0; i< 10; i++)
        {
            GameObject enemyObject = Instantiate(enemyPrefab);
            enemyPool[i] = enemyObject;
            enemyPool[i].GetComponent<Enemy>().MoveTargetTranf = targetTranf;
            enemyObject.SetActive(false);
        }
        
    } 

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn +=Time.deltaTime;
        if(timeAfterSpawn >= spawnRate)
        {
            enemyPool[currIndex].transform.LookAt(targetTranf);
            timeAfterSpawn = 0f;
            enemyPool[currIndex].transform.position = startTransf.position;
            enemyPool[currIndex].SetActive(false);
            enemyPool[currIndex].SetActive(true);
            enemyPool[currIndex].transform.LookAt(targetTranf);
            
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            currIndex++;
            if(currIndex>=10){
                currIndex = 0;
            }
        }
    }
    
}
