using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 1.5f;
    public float spawnRateMax = 5.0f;
    public float enemySpeed = 5.0f;

    public Transform targetTranf;
    private float spawnRate = default;
    private float timeAfterSpawn = default;
    
    public bool isKillEnemy= false;
     public Rigidbody enemyRigid;
    private GameObject[] bulletsPool;
    private int currIndex = 0;
    //private Vector3 poolPosition= new Vector3(0, 0, -100);
    
    public Transform MoveTargetTranf;

    public Transform startTransf;
   



    // Start is called before the first frame update
    void Start()
    {
        startTransf = GetComponent<Transform>();
        enemyRigid = GetComponent<Rigidbody>();
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        //targetTranf = FindObjectOfType<PlayerController>().transform;
        // --> secene object all search 

        bulletsPool = new GameObject[10];

        for(int i = 0; i< 10; i++)
        {
            GameObject gameObject = Instantiate(bulletPrefab);
            bulletsPool[i] = gameObject;
            gameObject.SetActive(false);
        }
        //
        
    } 

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn +=Time.deltaTime;
        if(timeAfterSpawn >= spawnRate)
        {
            gameObject.transform.LookAt(targetTranf);
            timeAfterSpawn = 0f;
            bulletsPool[currIndex].transform.position = startTransf.position;
            bulletsPool[currIndex].SetActive(false);
            bulletsPool[currIndex].SetActive(true);
            bulletsPool[currIndex].transform.LookAt(targetTranf);
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            currIndex++;
            if(currIndex>=10){
                currIndex = 0;
            }
        }

        if(MoveTargetTranf.position != gameObject.transform.position)
        {
            gameObject.transform.LookAt(targetTranf);
            transform.position= Vector3.MoveTowards(transform.position,MoveTargetTranf.position,enemySpeed*Time.deltaTime );
        }
        else{
           
        }
    }
    
    public void Die(){
        gameObject.SetActive(false);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SetScore(10);
    }

}
