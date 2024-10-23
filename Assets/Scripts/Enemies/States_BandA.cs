using UnityEngine;


public class Stats_BandA: MonoBehaviour
{
    // Reference to spawn an arrow
    public GameObject bowPrefab;   // The bow prefab
    public GameObject arrowPrefab; // The arrow prefab
    public Transform spawnPoint;   // The point where the bow and arrow will spawn
    


    // Reference to shoot an arrow 
    //public GameObject arrowPrefab; // The arrow prefab
    public Transform shootPoint;   // The point from which the arrow will be shot
    public float shootForce = 20f; // The force with which the arrow is shot
    public Animator crossbow_uncommon;



    // States.cs reference
    public bool out_of_range, deathSwitch, angry;
    public int health;
    public Animator animator;
    public LockOn lockOn_system;
    public Rigidbody rb;
    public CapsuleCollider bodyHitBox;
    public int walkAnim, runAnim;
    public float moveSpeed;


/////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        out_of_range = true;
        animator = GetComponent<Animator>();
        health = 15;
        lockOn_system = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LockOn>();
        deathSwitch = animator.GetBool("isDead?");
        rb = GetComponent<Rigidbody>();
        bodyHitBox = GetComponent<CapsuleCollider>();
        if(name != "Skeleton_Rogue"){
            walkAnim = Random.Range(0,3);
            runAnim = Random.Range(0,3);
            randomAngry();
            animator.SetInteger("WalkingAnim", walkAnim);
            animator.SetInteger("RunningAnim", runAnim);
        }
        moveSpeed = 4f;
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////



    // Spawn a bow and arrow
    void SpawnBowAndArrow()
    {
        // Instantiate the bow
        GameObject bow = Instantiate(bowPrefab, spawnPoint.position, spawnPoint.rotation);
        
        // Instantiate an arrow at the bow's shoot point
        Transform shootPoint = bow.transform.Find("SpawnPoint"); // Assuming you have a child GameObject named "ShootPoint"
        if (shootPoint != null)
        {
            //Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
            Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
                spawnPoint.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("SpawnPoint not found!"); // was shootpoint
        }


    }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////

 

    void Update() 
    {
           
        if (Input.GetKeyDown(KeyCode.K)) animator.SetBool("Attack", true); // Change to any key you want
        {
            SpawnBowAndArrow();
        } 
        if(Input.GetKeyUp(KeyCode.K)) animator.SetBool("Attack", false);


        if (Input.GetButtonDown("Fire1")) // Assuming left mouse button or "Fire1" is set in Input
        {
            ShootArrow();
        }
    }



    // Shoot an arrow
    void ShootArrow()
    {
        // Instantiate the arrow
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        
        // Get the Rigidbody component to apply force
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        rb.AddForce(shootPoint.up * shootForce, ForceMode2D.Impulse); // Adjust based on orientation

        rb.gravityScale = 0; // Set to 0 to prevent arrow from falling

    }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



  public void damageEnemy(int damageValue){
        health -= damageValue;
        if(health <= 0){
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






