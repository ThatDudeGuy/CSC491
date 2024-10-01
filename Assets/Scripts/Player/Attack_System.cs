using UnityEngine;

public class Attack_System : MonoBehaviour
{
    public Animator animator;
    public Player_Movement player_Movement;
    public Player_Weapon[] weapons;
    public int attackPhase = 0, counter = 0;
    public bool canContinue = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        player_Movement = GetComponent<Player_Movement>();
        weapons = GetComponentsInChildren<Player_Weapon>();
    }

    void Update()
    {
        
        if(canContinue && Input.GetMouseButton(1)){
            // print("Mouse Right");
            toggleBlocking(true);
            player_Movement.setMoveSpeed(0f);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else if(canContinue && Input.GetMouseButtonDown(0)){
            print("Mouse Left");
            attackChain();
        }
        
        if(Input.GetMouseButtonUp(1)){
            toggleBlocking(false);
            player_Movement.setMoveSpeed(5f);
        }

        // if(canContinue && Input.GetMouseButton(0)){
        //     print("Mouse Left");
        //     attackChain();
        // }

        if(attackPhase > 0) checkAttackChain();
    }

    void attackChain(){
        switch(attackPhase){
            case 0:
                animator.SetBool("isAttacking", true);
                break;
            case 1:
                animator.SetBool("attackChain", true);
                break;
            case >= 2:
                animator.SetBool("thrustAttack", true);
                attackPhase = 0;
                counter = 0;
                return;
        }
        if(!animator.GetBool("thrustAttack")) attackPhase++;
        counter = 0;
        
    }

    // There are different ways in Unity to acheive this result
    // but once the counter is equal to the framerate * 2, which is 60 fps in this case,
    // we know that 2 seconds have elapsed, the player can no longer continue the attack chain,
    // and the the animations and chain reset
    void checkAttackChain(){
        counter++;
        if(counter >= Application.targetFrameRate + Application.targetFrameRate/4 && attackPhase >= 1){
            counter = 0;
            attackPhase = 0;
            // firstAttackOff();
            // secondAttackOff();
            allAttacksOff();
        }
    }

    public void allAttacksOff(){
        animator.SetBool("isAttacking", false);
        animator.SetBool("attackChain", false);
        animator.SetBool("thrustAttack", false);
    }
    
    // Below functions are used as animation events on attack animations
    public void firstAttackOff(){
        animator.SetBool("isAttacking", false);
    }
    public void secondAttackOff(){
        animator.SetBool("attackChain", false);
    }

    public void thirdAttackOff(){
        // firstAttackOff();
        // secondAttackOff();
        animator.SetBool("thrustAttack", false);
    }
    public void toggle_canContinue(){
        canContinue = !canContinue;
        player_Movement.canMove = canContinue;
        if(player_Movement.animator.GetBool("isRunning")){
            player_Movement.moveSpeed = canContinue ? 10f : 0f;
        }
        else player_Movement.moveSpeed = canContinue ? 5f : 0f;
    }
    public void call_damageOn(){
        foreach(Player_Weapon weapon in weapons){
            weapon.damageOn();
        } 
    }
    public void call_damageOff(){
        foreach(Player_Weapon weapon in weapons){
            weapon.damageOff();
        } 
    }

    void toggleBlocking(bool toggle){
        animator.SetBool("isBlocking", toggle);
    }


    // This function is an animation event on the thrust attack
    // This will lunge the player foward and return them to their original position
    // public void lunge(){
        
    // }
}
