using UnityEngine;

public class States : MonoBehaviour
{
    public bool out_of_range, deathSwitch, angry;
    public int health;
    public Animator animator;
    public LockOn lockOn_system;
    public Rigidbody rb;
    public CapsuleCollider bodyHitBox;
    public Ai_Navigation ai_Navigation;
    public int walkAnim, runAnim;
    public float moveSpeed;
    
    void Start()
    {
        out_of_range = true;
        animator = GetComponent<Animator>();
        health = 15;
        lockOn_system = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LockOn>();
        deathSwitch = animator.GetBool("isDead?");
        rb = GetComponent<Rigidbody>();
        bodyHitBox = GetComponent<CapsuleCollider>();
        ai_Navigation = GetComponent<Ai_Navigation>();
        walkAnim = Random.Range(0,3);
        runAnim = Random.Range(0,3);
        randomAngry();
        animator.SetInteger("WalkingAnim", walkAnim);
        animator.SetInteger("RunningAnim", runAnim);
        moveSpeed = 4f;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)) animator.SetBool("Attack", true);
        if(Input.GetKeyUp(KeyCode.L)) animator.SetBool("Attack", false);
    }

    // The Player_Weapon script that is attached to the weapons object the player is holding
    // calls this function whenever the box colliders collide with an enemy
    public void damageEnemy(int damageValue){
        health -= damageValue;
        if(health <= 0){
            ai_Navigation.patrolling = false;
            ai_Navigation.agent.isStopped = true;
            for (int i = 0; i < animator.parameterCount; i++){
                AnimatorControllerParameter parameter = animator.GetParameter(i);
                if (parameter.type == AnimatorControllerParameterType.Bool){
                    animator.SetBool(parameter.name, false);
                }
            }
            animator.SetBool("isDead?", true);
            animator.SetInteger("DeathAnim", Random.Range(0,3));
            lockOn_system.enemies.Remove(gameObject);
            if(lockOn_system.lock_on_state){
                lockOn_system.getClosestTarget();
            }
            rb.constraints = RigidbodyConstraints.FreezeAll;
            bodyHitBox.enabled = false;
        }
        else return;
    }

    void randomAngry(){
        angry = Random.Range(0,2) == 1;
    }

    public void setMoveSpeed(float speed){
        moveSpeed = speed;
    }

}
