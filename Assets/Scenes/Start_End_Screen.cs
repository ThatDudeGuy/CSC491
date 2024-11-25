using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Start_End_Screen : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Enemy_1");
    }

    public void OpenInfo(){
        SceneManager.LoadScene("Info");
    }

    public void Restart(){
        SceneManager.LoadScene("StartScreen");
    }
}

