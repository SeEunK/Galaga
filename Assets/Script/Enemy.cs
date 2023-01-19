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

        //��⿭�� �Ѿ��� �ִ°��
        // �� �Ѿ��� ��⿭���� �����.
        // ��⿭���ִ� �Ѿ��� �����Ѵ�.

        //��⿭�� �Ѿ��� ���� ���
        //  �Ѿ��� ���� ����� �ش�.

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

        //���� ���� �ð��� �������ݰ� ���ų� ũ�� �Ѿ��� POOL ���� INDEX 1���� ������.  
        if (timeAfterSpawn >= spawnRate)
        {
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            timeAfterSpawn = 0f;
         
            gameObject.transform.LookAt(targetTranf);
            
            GameObject bullet = PopBullet();
            bullet.transform.position = startTransf.position;
            bullet.transform.LookAt(targetTranf);
            
        }

        //���� �̵� �Ұ��� �����ǿ� ���������ʾ����� ��� �̵��Ѵ�.
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
