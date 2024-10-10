using UnityEngine;

public class States : MonoBehaviour
{
    public bool out_of_range, deathSwitch, angry, begin_cleanup;
    public int health, walkAnim, runAnim, castCycleCount, cycleLimit = 3, counter;
    public Animator animator;
    public LockOn lockOn_system;
    public Rigidbody rb;
    public CapsuleCollider bodyHitBox;
    public Ai_Navigation ai_Navigation;
    public float nextAttackTime = 0f, attackInterval = 1.5f, timeElapsed, duration = 1f;
    public GameObject sightRange;
    
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
        if(name.Contains("Mage")) print("Skip");//animator.SetTrigger("isCasting");
        else if(name != "Skeleton_Rogue"){
            walkAnim = Random.Range(0,3);
            runAnim = Random.Range(0,3);
            // randomAngry();
            animator.SetInteger("WalkingAnim", walkAnim);
            animator.SetInteger("RunningAnim", runAnim);
        }
        
    }

    private void Awake() {
        if(name != "Skeleton_Rogue" && !name.Contains("Mage")){
            animator.SetBool("Spawn", false);
        }
    }

    private void Update() {
        if(begin_cleanup) destroy_self();
    }


    // The Player_Weapon script that is attached to the weapons object the player is holding
    // calls this function whenever the box colliders collide with an enemy
    public void damageEnemy(int damageValue){
        health -= damageValue;
        if(health <= 0){
            if(ai_Navigation){
                ai_Navigation.patrolling = false;
                ai_Navigation.agent.isStopped = true;
            }
            for (int i = 0; i < animator.parameterCount; i++){
                AnimatorControllerParameter parameter = animator.GetParameter(i);
                if (parameter.type == AnimatorControllerParameterType.Bool){
                    if(parameter.name == "isCasting") continue;
                    animator.SetBool(parameter.name, false);

                }
            }
            animator.SetBool("isDead?", true); 
            animator.SetInteger("DeathAnim", Random.Range(0,3));
            if(sightRange) Destroy(sightRange);
            lockOn_system.enemies.Remove(gameObject);
            if(lockOn_system.lock_on_state){
                lockOn_system.getClosestTarget();
            }
            rb.constraints = RigidbodyConstraints.FreezeAll;
            bodyHitBox.enabled = false;
        }
        else return;
    }

    private void destroy_self(){
        if(counter >= Application.targetFrameRate * 3){
            if (timeElapsed < duration){
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
            }
            else Destroy(gameObject);
            counter = Application.targetFrameRate * 3;
        }
        counter++;
    }

    // This function will make the enemy attack, wait a second, and
    // then attack again
    public void continuousAttack(){
        // animator.SetBool("Attack", true);
        if (Time.time >= nextAttackTime){
            nextAttackTime = Time.time + attackInterval;
            animator.SetBool("Attack", true);
        }
    }

    // private void randomAngry(){
    //     angry = Random.Range(0,2) == 1;
    // }

    public void begin_Destruction(){
        begin_cleanup = !begin_cleanup;
    }

}
