using UnityEngine;

public class Attack_System : MonoBehaviour
{
    public Animator animator;
    public int attackPhase = 0, counter = 0;
    public bool canContinue = true;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(canContinue && Input.GetMouseButtonDown(0)){
            print("Mouse Left");
            attackChain();
        }
        if(canContinue && Input.GetMouseButton(0)){
            print("Mouse Left");
            attackChain();
        }

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
                firstAttackOff();
                secondAttackOff();
                return;
        }
        counter = 0;
        attackPhase++;
    }

    // There are different ways in Unity to acheive this result
    // but once the counter is equal to the framerate * 2, which is 60 fps in this case,
    // we know that 2 seconds have elapsed, the player can no longer continue the attack chain,
    // and the the animations and chain reset
    void checkAttackChain(){
        counter++;
        if(counter >= Application.targetFrameRate * 2 && attackPhase >= 1){
            counter = 0;
            attackPhase = 0;
            firstAttackOff();
            secondAttackOff();
        }
    }

    public void firstAttackOff(){
        animator.SetBool("isAttacking", false);
    }
    public void secondAttackOff(){
        animator.SetBool("attackChain", false);
    }

    // Below 2 functions are used as animation events on attack animations
    public void thirdAttackOff(){
        animator.SetBool("thrustAttack", false);
    }
    public void toggle_canContinue(){
        canContinue = !canContinue;
    }

    // This function is an animation event on the thrust attack
    // This will lunge the player foward and return them to their original position
    // public void lunge(){
        
    // }
}
