using UnityEngine;
using UnityEngine.AI;

public class Ai_Navigation : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    NavMeshAgent agent;
    public bool touchedPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")) touchedPlayer = true;
        else return;
    }

    // Update is called once per frame
    void Update()
    {
        if(!animator.GetBool("isDead?")){
            if(touchedPlayer) agent.destination = new Vector3(0,transform.position.y,0);
            else agent.destination = player.position;
        }
        else{
            agent.isStopped = true;
        }
    }
}
