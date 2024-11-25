using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Start_End_Screen : MonoBehaviour
{
    public SceneRef Scenes;
    public void StartGame(){
        SceneManager.LoadScene(Scenes.level_one.name);
    }

    public void OpenInfo(){
        SceneManager.LoadScene(Scenes.infoScreen.name);
    }

    public void Restart(){
        SceneManager.LoadScene(Scenes.start_screen.name);
    }
}

[Serializable]
public class SceneRef
{
    public UnityEngine.Object start_screen, level_one, infoScreen;
}
