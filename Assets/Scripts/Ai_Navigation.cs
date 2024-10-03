using UnityEngine;
using UnityEngine.AI;

public class Ai_Navigation : MonoBehaviour
{
    public Transform player, endPath, startPath;
    public Animator animator;
    public States enemy;
    public NavMeshAgent agent;
    public Vector3 destination, pathPoint;
    public Rigidbody rb;
    public bool playerFound, patrolling;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();       
        rb = GetComponent<Rigidbody>();
        agent.destination = endPath.position;
        patrolling = true;
    }

    // private void OnCollisionEnter(Collision other) {
    //     if(other.gameObject.CompareTag("Player")) touchedPlayer = true;
    //     else return;
    // }

    // Update is called once per frame
    void Update()
    {
        if(!playerFound) patrol();
        else chasePlayer();

    }

    void changeDestination(Vector3 point){
        if(point.x == startPath.position.x && point.z == startPath.position.z) agent.destination = endPath.position;
        else if(point.x == endPath.position.x && point.z == endPath.position.z) agent.destination = startPath.position;
    }

    void patrol(){
        // Patrolling gets set to false either when the enemy dies in States.cs
        // and when the player is within Eye_Sight range
        if(patrolling){ //!animator.GetBool("isDead?"
            if(agent.remainingDistance <= agent.stoppingDistance){
                changeDestination(agent.destination);
            }
            else{
                animator.SetBool("Attack", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                agent.speed = 3f;
            }
        }
        else return;
    }

    void chasePlayer(){
        if(!animator.GetBool("isDead?")){
            agent.destination = player.position;
            transform.LookAt(player.position);
            if(agent.remainingDistance <= agent.stoppingDistance){
                // rb.isKinematic = false;
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("Attack", true);
            }
            else if(agent.remainingDistance > agent.stoppingDistance){
                agent.isStopped = false;
                animator.SetBool("Attack", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", true);
                agent.speed = 8.5f;
            }
            // else {
            //     // rb.isKinematic = true;
            //     agent.isStopped = false;
            //     animator.SetBool("Attack", false);
            //     animator.SetBool("isRunning", false);
            //     animator.SetBool("isWalking", true);
            //     agent.speed = 5.25f;
            // }
        }
        else return;
    }
}
