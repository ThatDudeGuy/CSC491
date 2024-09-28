using UnityEngine;

public class Attack_System : MonoBehaviour
{
    public Animator animator;
    public Player_Movement player_Movement;
    public int attackPhase = 0, counter = 0;
    public bool canContinue = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        player_Movement = GetComponent<Player_Movement>();
    }

    void Update()
    {
        if(canContinue && Input.GetMouseButtonDown(0)){
            print("Mouse Left");
            attackChain();
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
                attackPhase++;
                break;
            case 1:
                if(animator.GetBool("isAttacking")) animator.SetBool("attackChain", true);
                attackPhase++;
                break;
            case >= 2:
                if(animator.GetBool("attackChain")) animator.SetBool("thrustAttack", true);
                attackPhase = 0;
                counter = 0;
                return;
        }
        counter = 0;
        
    }

    // There are different ways in Unity to acheive this result
    // but once the counter is equal to the framerate * 2, which is 60 fps in this case,
    // we know that 2 seconds have elapsed, the player can no longer continue the attack chain,
    // and the the animations and chain reset
    void checkAttackChain(){
        counter++;
        if(counter >= Application.targetFrameRate * 2 - Application.targetFrameRate/2 && attackPhase >= 1){
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
    public void firstAttackOff(){
        animator.SetBool("isAttacking", false);
    }
    public void secondAttackOff(){
        animator.SetBool("attackChain", false);
    }

    // Below 2 functions are used as animation events on attack animations
    public void thirdAttackOff(){
        firstAttackOff();
        secondAttackOff();
        animator.SetBool("thrustAttack", false);
    }
    public void toggle_canContinue(){
        canContinue = !canContinue;
        player_Movement.canMove = canContinue;
        player_Movement.moveSpeed = canContinue ? 5f : 0f;
        print(canContinue);
    }

    // This function is an animation event on the thrust attack
    // This will lunge the player foward and return them to their original position
    // public void lunge(){
        
    // }
}
