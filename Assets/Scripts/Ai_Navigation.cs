using UnityEngine;
using UnityEngine.AI;

public class Ai_Navigation : MonoBehaviour
{
    public Transform player, endPath, startPath;
    public Animator animator;
    public States enemy;
    public Enemy_Attack enemy_Attack;
    public NavMeshAgent agent;
    public Vector3 destination, pathPoint;
    public Rigidbody rb;
    public bool playerFound, patrolling, giant;
    public int counter;
    private const int Big_Guy_Id = -1372625422;
    public float stop_distance = 0;
    // private NavMeshBuildSettings settings;
    // private int[] agent_Ids;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();       
        rb = GetComponent<Rigidbody>();
        enemy_Attack = GetComponent<Enemy_Attack>();
        startPath = GameObject.FindGameObjectWithTag("Start").transform;
        endPath = GameObject.FindGameObjectWithTag("End").transform;
        if(endPath) agent.destination = endPath.position;
        patrolling = true;
        // print("ID: "+agent.agentTypeID);
        // for (int i = 0; i < NavMesh.GetSettingsCount(); i++){
        //     settings = NavMesh.GetSettingsByIndex(i);
        //     print(settings);
        // }
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
            // if(giant) stop_distance = 3f;
            // else if(patrolling) stop_distance = 0;
            agent.stoppingDistance = stop_distance;
            if(agent.remainingDistance <= agent.stoppingDistance){
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                counter++;
                if (counter >= Application.targetFrameRate * 1.5){
                    changeDestination(agent.destination);
                    counter = 0;
                    agent.isStopped = false;
                }
                
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
            if(giant) {
                stop_distance = 3f;
                agent.agentTypeID = Big_Guy_Id;
            }
            else stop_distance = 2f;
            agent.destination = player.position;
            agent.stoppingDistance = stop_distance;
            transform.LookAt(new Vector3(player.position.x, 0f, player.position.z));
            if(agent.remainingDistance <= agent.stoppingDistance){
                // rb.isKinematic = false;
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                // animator.SetBool("Attack", true);
                // agent.speed = 3f;
                enemy.continuousAttack();
            }
            else if(agent.remainingDistance > agent.stoppingDistance + 1 && !animator.GetBool("Attack")){
                agent.isStopped = false;
                enemy_Attack.call_damageOff();
                animator.SetBool("Attack", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", true);
                agent.speed = 7f;
            }
            else {
                if(!animator.GetBool("Attack")){
                // rb.isKinematic = true;
                    agent.isStopped = false;
                    animator.SetBool("Attack", false);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isWalking", true);
                    agent.speed = 5f;
                }
            }
        }
        else return;
    }
}
