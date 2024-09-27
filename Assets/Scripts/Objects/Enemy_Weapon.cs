using UnityEngine;

public class Enemy_Weapon : MonoBehaviour
{
    // BoxCollider hitbox;
    Animator masterAnimator;
    Health_Tracking player_health;
    public int damageValue; 
    bool canDamagePlayer;

    void Start()
    {
        // hitbox = GetComponent<BoxCollider>();
        Transform currentTransform = transform; // Starting from the child object
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Tracking>();
        while (currentTransform.parent != null){
            currentTransform = currentTransform.parent;
        }
        // Now `currentTransform` is the master parent
        masterAnimator = currentTransform.GetComponent<Animator>();

        if(name.Contains("Blade")) damageValue = 10;
        canDamagePlayer = false;
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && canDamagePlayer){
            player_health.damagePlayer(damageValue);
        } 
        else return;
    }

    public void damageOn(){
        canDamagePlayer = true;
    }
    public void damageOff(){
        canDamagePlayer = false;
    }

}
