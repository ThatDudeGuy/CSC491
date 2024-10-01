using UnityEngine;

public class Enemy_Weapon : MonoBehaviour
{
    Health_Tracking player_health;
    public int damageValue; 
    bool canDamagePlayer;

    void Start()
    {
        Transform currentTransform = transform; // Starting from the child object
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Tracking>();
        while (currentTransform.parent != null){
            currentTransform = currentTransform.parent;
        }

        if(name.Contains("Blade")) damageValue = 10;
        canDamagePlayer = false;
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && canDamagePlayer){
            if(!player_health.animator.GetBool("isBlocking")) player_health.damagePlayer(damageValue);
            else if(player_health.animator.GetBool("isBlocking")) player_health.damagePlayer(damageValue/2);
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
