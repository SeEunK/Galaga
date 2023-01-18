using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 1.0f;

    public Transform targetTranf;
    private float spawnRate = default;
    private float timeAfterSpawn = default;

    
    private GameObject[] bulletsPool;
    private int currIndex = 0;
    private Vector3 poolPosition= new Vector3(0, 0, -100);

    public Transform startTransf;
   



    // Start is called before the first frame update
    void Start()
    {
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
    }
    
    public void Die(){
        gameObject.SetActive(false);
       
    }
}
