using UnityEngine;

public class States : MonoBehaviour
{
    public bool out_of_range, deathSwitch;
    public int health;
    public Animator animator;
    public LockOn lockOn_system;
    void Start()
    {
        out_of_range = true;
        animator = GetComponent<Animator>();
        health = 15;
        lockOn_system = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LockOn>();
        deathSwitch = animator.GetBool("isDead?");
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
            animator.SetBool("isDead?", true);
            animator.SetInteger("DeathAnim", Random.Range(0,3));
            lockOn_system.enemies.Remove(gameObject);
            if(lockOn_system.lock_on_state){
                lockOn_system.getClosestTarget();
            }
        }
        else return;
    }



}
