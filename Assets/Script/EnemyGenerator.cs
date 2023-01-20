using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;


    public float spawnRateMin = 4.5f;
    public float spawnRateMax = 5.0f;

    public List<Transform> moveTargetTranfs;
    private float spawnRate = default;
    private float timeAfterSpawn = default;
   
 
    private List<GameObject> enemyPool = new List<GameObject>();
    
    public GameObject player;

    public List<Transform> startTransfs;
    public int dieCount;
    public int maxCountEnemyType01 = 5;



    // Start is called before the first frame update
    void Start()
    {
        dieCount = 0;
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        // init enemy create
        for (int i = 0; i < maxCountEnemyType01; i++)
        {
            GameObject enemy = InitEnemy();
            enemy.GetComponent<Enemy>().MoveTargetTranf = moveTargetTranfs[i];

            enemy.GetComponent<Enemy>().targetTranf = player.transform;

            // enemy start position set
            enemy.transform.position = startTransfs[0].position;
        }

    } 

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn +=Time.deltaTime;

        if(timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            if (dieCount > 0)
            {
                int enemyIndex = enemyPool.Count;

                GameObject enemy = PopEnemy();
                

                // enemy start position set
                enemy.transform.position = startTransfs[0].position;

                --dieCount;
            }
            
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    public void PushEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPool.Add(enemy.gameObject);
    }
    public GameObject PopEnemy()
    {
        if(enemyPool.Count> 0)
        {
            GameObject temp = enemyPool[0];
            enemyPool.RemoveAt(0);
            temp.SetActive(true);
            return temp;
        }
        else
        { // enemy pool count = 0 , new enemuy
            GameObject newEnemy = CreateEnemy();
            newEnemy.SetActive(true);
            return newEnemy;
        }
    }
    public GameObject InitEnemy()
    {
        GameObject newEnemy = CreateEnemy();
        newEnemy.SetActive(true);
        return newEnemy;
    }

    public GameObject CreateEnemy()
    {
        GameObject enemyObject = Instantiate(enemyPrefabs[0]);
        enemyPool.Add(enemyObject);
        enemyObject.SetActive(false);

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.enemyGenerator = this;
        return enemyObject;

    }


}
