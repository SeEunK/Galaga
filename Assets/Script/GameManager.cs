using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static PlayerController;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GamePlay, GameOver 
    } // 

    public GameState gameState;
    public GameObject gameOverText;
    public Text recordText;
    public Text scoreText;
    public GameObject player;

    public List<Image> hearts;
    public static int playerMaxHeart =3;
    private int score;
    private bool isGameOver;

    public const string SCENE_NAME ="SampleScene";
    public float spawnRateMax = 3.0f;

    public float spawnRate = default;

    
    
    void Start()
    {
        gameState = GameState.GamePlay;
        spawnRate = 0;
        score = 0;
        isGameOver = false;
       
        player = GameObject.Find("Player");

        recordText.text = "recordText";
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == true){
            if(Input.GetKeyDown(KeyCode.R)){
                
                SceneManager.LoadScene(SCENE_NAME);
                gameState = GameState.GamePlay;

            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            
           QuitThisGame();
        }
        else
        {
            scoreText.text = "Score :" + score;
        
        }
        
        PlayerController playerController = player.GetComponent<PlayerController>();
        if(playerController.playerState == PlayerController.PlayerState.Respawn)
        {
            spawnRate += Time.deltaTime;
            int twinkle = (int) (spawnRate / 0.2f);
            if(twinkle%2 == 0)
            {
                playerController.gameObject.SetActive(false);

            }
            else
            {
                playerController.gameObject.SetActive(true);
            }

            if(spawnRate >= spawnRateMax)
            {
                spawnRate = 0;

                playerController.Respawn();
            }
        }
        
       
       
    }

    public void SetHeartCount(int heartCount)
    {
        for (int i = 0; i < playerMaxHeart; i++)
        {
            if (i < heartCount)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    public void EndGame(){
        gameState = GameState.GameOver;
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
    public void QuitThisGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }

}
