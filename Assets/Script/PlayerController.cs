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
    private List<GameObject> bulletsPool = new List<GameObject>();
    
    public int playerHeart;
    public int playerMaxHeart = 3;

    public Transform startTransf;

    void Start()
    {
        playerState = PlayerState.Live;
        playerHeart = playerMaxHeart;
        
        playerRidgidbody = GetComponent<Rigidbody>();
                
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput*speed;
        float zSpeed = zInput*speed;

        
        Vector3 playerVelocity = new Vector3(xSpeed, 0f, zSpeed);
        playerRidgidbody.velocity = playerVelocity;


         if(Input.GetKeyDown(KeyCode.Space)){

            GameObject playerBullet = PopBullet();
            playerBullet.transform.position = startTransf.position;

           
         }

    }

    public void PushBullet(PlayerBullet playerbullet)
    {
        playerbullet.gameObject.SetActive(false);

        bulletsPool.Add(playerbullet.gameObject);
    }
    public GameObject PopBullet()
    {
        if (bulletsPool.Count > 0)
        {
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
        PlayerBullet playerbullet = gameObject.GetComponent<PlayerBullet>();
        playerbullet.playerController = this;

        return gameObject;
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