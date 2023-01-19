using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die, Live, Respawn
    } // live -> die -> respawn

    public PlayerState playerState;
    public Rigidbody playerRidgidbody;
    public float speed = 8f;
    
    public GameObject bulletPrefab;
    
    private GameObject[] bulletsPool;
    private int currIndex = 0;
    
    public int playerHeart;
    public int playerMaxHeart = 3;

    public Transform startTransf;

    void Start()
    {
        playerState = PlayerState.Live;
        playerHeart = playerMaxHeart;
      
        
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
          
            
            bulletsPool[currIndex].transform.position = startTransf.position;

            bulletsPool[currIndex].SetActive(true);
           
            currIndex++;
            if(currIndex>=10){
                currIndex = 0;
            }
         }

    }




    public void Die()
    {

        GameManager gameManager = FindObjectOfType<GameManager>();
        playerState = PlayerState.Die;
        gameObject.SetActive(false);
        --playerHeart;
        gameManager.SetHeartCount(playerHeart);


        if (playerHeart <= 0)
        {
            gameManager.EndGame();
        }
        else
        {
            playerState = PlayerState.Respawn;
        }

    }

    public void Respawn()
    {
        playerState = PlayerState.Live;
        gameObject.SetActive(true);
    }
}
