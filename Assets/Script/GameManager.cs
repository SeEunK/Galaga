using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    public GameObject gameOverText;
    public Text recordText;
    public Text scoreText;
    public GameObject player;
    
    public static int playerMaxHeart =3;
    private int score;
    private bool isGameOver;

    public const string SCENE_NAME ="SampleScene";
    public float spawnRateMax = 3.0f;

    public float spawnRate = default;
    
    void Start()
    {
        spawnRate = 0;
        score = 0;
        isGameOver = false;
        GameObject uiObjs_ = GFunc.GetRootObj("Canvas");
        player = GameObject.Find("Player");

        recordText.text = "recordText";
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == true){
            if(Input.GetKeyDown(KeyCode.R)){
                
                SceneManager.LoadScene(SCENE_NAME);
               
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            
           GFunc.QuitThisGame();
        }
        else
        {
            scoreText.text = "Score :" + score;
        
        }
        
       // PlayerController playerController = FindObjectOfType<PlayerController>();
        if(PlayerIsHitable() == false){
            spawnRate +=Time.deltaTime;
            if(spawnRate >=spawnRateMax ){
                player.SetActive(true);
                PlayerSetHitable();
                spawnRate = 0;
            }
        }
       
       
    }

    public void EndGame(){
        isGameOver = true;
        gameOverText.SetActive(true);
        int bestScore = PlayerPrefs.GetInt("BestScore");

        if(score > bestScore){
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        recordText.text = "Best Score: "+(int)bestScore;

    }

    public void SetScore(int addScore){
        score = score+ addScore;
    }

    public bool PlayerIsHitable()
    {
        
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            return playerController.GetHitable();
        }
        else
        {
           return false;
        }
       
    }
    public void PlayerSetHitable()
    {
        
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetHitable(true);
            }
            else
            {
               
            }
        
    }
}
