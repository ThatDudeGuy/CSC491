using UnityEngine;
using UnityEngine.UI;

public class Health_Tracking : MonoBehaviour
{
    public int health;
    public Animator animator;
    public Slider staminaBar, healthBar;
    public Player_Movement player;
    public DeathofMainPlayerLogic mp;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player_Movement>();
        health = 100;
        mp = GetComponent<DeathofMainPlayerLogic>();
        // staminaBar = 
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z)) damagePlayer(100);
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
        if (health <= 0)
        {
            {
                mp.Die(); // Was characterController
                animator.SetBool("isDead",true);
            }
        }
    }

    public void regenHealth(int regenAmount){
        health += regenAmount;
        if(health > 100) health = 100;
        healthBar.value = health;
    }
}
