using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRidgidbody;
    public float speed = 8f;
    
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 1.0f;

    private float spawnRate = default;
    private float timeAfterSpawn = default;
    private GameObject[] bulletsPool;
    private int currIndex = 0;
 

    public Transform startTransf;

    void Start()
    {
        playerRidgidbody = GetComponent<Rigidbody>();
        // --> It is not necessary to set the rigibody of the gameObj directly in the editor.
        
        bulletsPool = new GameObject[10];

        for(int i = 0; i< 10; i++)
        {
            GameObject gameObject = Instantiate(bulletPrefab);
            bulletsPool[i] = gameObject;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput*speed;
        float zSpeed = zInput*speed;

        
        Vector3 playerVelocity = new Vector3(xSpeed, 0f, zSpeed);
        playerRidgidbody.velocity = playerVelocity;

      
         

         if(Input.GetKeyDown(KeyCode.Return)){
          
            bulletsPool[currIndex].transform.LookAt(transform.forward);
            
            bulletsPool[currIndex].transform.position = startTransf.position;
            bulletsPool[currIndex].SetActive(false);
            bulletsPool[currIndex].SetActive(true);
            bulletsPool[currIndex].transform.LookAt(transform.forward);
           
            currIndex++;
            if(currIndex>=10){
                currIndex = 0;
            }
         }
    }


    public void Die(){
        gameObject.SetActive(false);
       // GameManager gameManager = FindObjectOfType<GameManager>();
       // gameManager.EndGame();
    }
}
