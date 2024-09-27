using System;
using UnityEngine;

public class Item_Behaviour : MonoBehaviour
{
    public int healthRegen;
    public Health_Tracking playerHealth;
    public CapsuleCollider playerCapsuleCollider;
    private string[] tokens;

    void Start()
    {
        tokens = name.Split("_");
        if(tokens[0] == "health" && tokens[1] == "medium"){
            healthRegen = 40;
        } 
        else if(tokens[0] == "health" && tokens[1] == "small"){healthRegen = 20;}  
        // tag = "hello";
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Tracking>();
        playerCapsuleCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
    }


    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && other == playerCapsuleCollider && Input.GetKeyDown(KeyCode.E)){
            if(tokens[0] == "health"){
                Destroy(gameObject); 
                playerHealth.regenHealth(healthRegen);
            }
        }
        else return;
    }
}
