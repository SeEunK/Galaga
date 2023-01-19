using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static partial class GFunc 
{
  public static void QuitThisGame()
  {
          #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else 
            Application.Quit();
            #endif

  }

  public static void KFunc(this GameObject obj)
  {
    Debug.Log("make func");

  }
  public static void LoadScene (string sceneName_){
    SceneManager.LoadScene(sceneName_);
  }
}
