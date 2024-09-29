using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public int damageValue; 
    bool canDamageEnemy;
    public BoxCollider hitbox;
    
    // Below variables set in the inspector
    public Animator animator; // the player's animator
    void Start()
    {
        damageValue = 5;
        hitbox = GetComponent<BoxCollider>();
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other){
        // if(other.CompareTag("Skeleton") && canDamageEnemy){
        //     // enemy_health.damageEnemy(damageValue);
        // } 
        if(other.CompareTag("Skeleton") && gameObject.GetComponent<BoxCollider>() != null && canDamageEnemy){
            print("Damage enemy");
        }
        else return;
    }

    // Below functions are referenced in the player attacking animations as animation events
    public void damageOn(){
        canDamageEnemy = true;
        hitbox.enabled = true;
    }
    public void damageOff(){
        canDamageEnemy = false;
        hitbox.enabled = false;
    }

    // bool isAttacking(){
    //     if(animator.GetBool("isAttacking") || animator.GetBool("attackChain") || animator.GetBool("thrustAttack")) return true;
    //     else return false;
    // }
}
