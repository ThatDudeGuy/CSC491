using UnityEngine;
using UnityEngine.AI;

public class Ai_Navigation : MonoBehaviour
{
    public Transform player, endPath, startPath;
    public Animator animator;
    public States enemy;
    NavMeshAgent agent;
    public bool touchedPlayer, onPath;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<States>();
        onPath = true;
        agent.destination = endPath.position;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")) touchedPlayer = true;
        else return;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && !animator.GetBool("isDead?") && !onPath){
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("Attack", true);
        }
        else if(agent.remainingDistance > agent.stoppingDistance * 2 && !onPath){
            animator.SetBool("Attack", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", true);
            agent.speed = 6f;
            // enemy.setMoveSpeed(8f);
        }
        else {
            if(agent.remainingDistance <= agent.stoppingDistance){
                agent.destination = startPath.position;
            // animator.SetBool("Attack", false);
            // animator.SetBool("isRunning", false);
            // animator.SetBool("isWalking", true);
            // agent.speed = 4.5f;
            }
            else{
                animator.SetBool("Attack", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                agent.speed = 4.5f;
            }
            // enemy.setMoveSpeed(4f);           
        }
        // if(!animator.GetBool("isDead?")){
        //     if(touchedPlayer) agent.destination = new Vector3(0,transform.position.y,0);
        //     else agent.destination = player.position;
        // }
        // else{
        //     agent.isStopped = true;
        // }
    }
}
