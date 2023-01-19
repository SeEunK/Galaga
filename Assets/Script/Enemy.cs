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

    public int killpoint;
    public bool isKillEnemy= false;
    public Rigidbody enemyRigid;
    private List<GameObject> bulletsPool = new List<GameObject>();
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

        
    }

    public void PushBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bulletsPool.Add(bullet.gameObject);
    }
    public GameObject PopBullet()
    {

        //대기열에 총알이 있는경우
        // 준 총알은 대기열에서 지운다.
        // 대기열에있는 총알은 리턴한다.

        //대기열에 총알이 없는 경우
        //  총알을 새로 만들어 준다.

        if(bulletsPool.Count > 0) {
            GameObject temp = bulletsPool[0];
            bulletsPool.RemoveAt(0);
            temp.SetActive(true);
            return temp;
        }
        else
        {
            GameObject newBullet = CreateBullet();
            newBullet.SetActive(true);
            return newBullet;
        }

    }
    public GameObject CreateBullet()
    {
        GameObject gameObject = Instantiate(bulletPrefab);
        bulletsPool.Add(gameObject);
        gameObject.SetActive(false);
        Bullet bullet = gameObject.GetComponent<Bullet>();
        bullet.shooter = this;

        return gameObject;
    }

 

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn +=Time.deltaTime;

        //다음 스폰 시간이 스폰간격과 같거나 크면 총알을 POOL 에서 INDEX 1부터 꺼내씀.  
        if (timeAfterSpawn >= spawnRate)
        {
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            timeAfterSpawn = 0f;
         
            gameObject.transform.LookAt(targetTranf);
            
            GameObject bullet = PopBullet();
            bullet.transform.position = startTransf.position;
            bullet.transform.LookAt(targetTranf);
            
        }

        //적이 이동 할곳의 포지션에 도달하지않았으면 계속 이동한다.
        if(MoveTargetTranf.position != gameObject.transform.position)
        {
            transform.LookAt(targetTranf);
            transform.position= Vector3.MoveTowards(transform.position,MoveTargetTranf.position,enemySpeed*Time.deltaTime );
        }
        else{
            transform.LookAt(targetTranf);

        }
    }
    
    public void Die(){
        gameObject.SetActive(false);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SetScore(killpoint);
    }

}
