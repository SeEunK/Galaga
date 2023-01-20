using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody enemyRigid;
    private float spawnInterval = 3.0f;
    private float timeAfterSpawn = default;
 
    public Transform targetTranf;

    public int killpoint;
    public bool isKillEnemy= false;
    
    public float enemySpeed = 5.0f;
    public Transform MoveTargetTranf;
    public Transform startTransf;

    public GameObject bulletPrefab;
    private List<GameObject> bulletsPool = new List<GameObject>();
    
    public EnemyGenerator enemyGenerator;

    private GameManager gameManager;


    void Start()
    {
        startTransf = GetComponent<Transform>();
        enemyRigid = GetComponent<Rigidbody>();
        timeAfterSpawn = 0f;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PushBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bulletsPool.Add(bullet.gameObject);
    }
    public GameObject PopBullet()
    {
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
        if (gameManager.gameState != GameManager.GameState.GameOver)
        {
            timeAfterSpawn += Time.deltaTime;

            if (MoveTargetTranf.position != gameObject.transform.position)
            {
                transform.LookAt(targetTranf);
                transform.position = Vector3.MoveTowards(transform.position, MoveTargetTranf.position, enemySpeed * Time.deltaTime);
            }
            else
            {
                transform.LookAt(targetTranf);

                if (timeAfterSpawn >= spawnInterval)
                {
                    timeAfterSpawn = 0f;

                    gameObject.transform.LookAt(targetTranf);

                    GameObject bullet = PopBullet();
                    bullet.transform.position = startTransf.position;
                    bullet.transform.LookAt(targetTranf);

                }

            }
        }
    }
    
    public void Die(){
        gameObject.SetActive(false);
        gameManager.SetScore(killpoint);
        enemyGenerator.PushEnemy(this);
        enemyGenerator.dieCount++;
    }

}
