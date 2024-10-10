using System;
using UnityEngine;
using UnityEngine.UI;

public class Health_Tracking : MonoBehaviour
{
    public int health = 100;
    public Animator animator;
    public Slider staminaBar, healthBar;
    public Player_Movement player;

    // Need to splite Save & Load into separate file and class
    public void SaveGame()
    {
        string data = JsonUtility.ToJson(this, true);
        string directory = Application.persistentDataPath + "/SaveData";
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }
        System.IO.File.WriteAllText(directory +"/saveFile.json", data);
        Debug.Log("Saved Game!");
    }

    public void LoadGame(){
        try{
            string path = Application.persistentDataPath +"/SaveData/saveFile.json";
            if (System.IO.File.Exists(path))
            {
                string jsonData = System.IO.File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(jsonData, this);
                print("Loading game = "+jsonData);
                // GameData data = JsonUtility.FromJson<GameData>(json);

                // Debug.Log("Player Level: " + data.playerLevel);
            }
            else
            {
                Debug.Log("Save file not found!");
            }
        }
        catch(Exception e) {
            Debug.LogError("Save File Corruption: "+e);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player_Movement>();
        health = 100;
        LoadGame();
        healthBar.value = health;
        // staminaBar = 
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private void FixedUpdate(){
        if(animator.GetBool("isRunning") && !animator.GetBool("isAttacking") && !animator.GetBool("attackChain") && !animator.GetBool("thrustAttack")) staminaBar.value -= 0.5f;
        else if(!animator.GetBool("isRunning") && staminaBar.value < 100 || player.moveSpeed == 0) staminaBar.value += 0.2f;
        if(staminaBar.value <= 0){
            animator.SetBool("isRunning", false);
            player.moveSpeed = 5f;
        }
    }

    public void damagePlayer(int damageAmount){
        health -= damageAmount;
        healthBar.value = health;
    }

    public void regenHealth(int regenAmount){
        health += regenAmount;
        if(health > 100) health = 100;
        healthBar.value = health;
    }
}
