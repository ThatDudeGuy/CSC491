using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour
{
    public bool lock_on_state;
    public Collider lock_on_range;
    public GameObject arrowSpawn, arrow, closestEnemy;
    public List<GameObject> enemies = new();
    public int listLength;
    public double shortestPath = 0;
    public Vector3 enemyDirection = new(0,0,0), rotateTo, movement, camera_Y_vector;
    public Camera_Movement mainCamera;
    float nextRaycastTime = 0f;
    float raycastInterval = 0.5f;

    // Below variables are set in the inspector
    public Eye_Sight eye_Sight;
    public Sprite[] lockImages; 
    public Image ui_lock;

    void Start()
    {
        lock_on_range = GetComponent<SphereCollider>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Movement>();
        // arrowSpawn = Resources.Load<GameObject>("Assets/Prefabs/ArrowSpawn.prefab");
    }
    

    private void OnTriggerEnter(Collider other) {
        // This function runs or is "triggered" continuously whenever an enemy stays within the sphere collider attached to the character
        // for more than 1 second I believe
        if(other.CompareTag("Skeleton") && !other.GetComponent<States>().animator.GetBool("isDead?")){
            if(other.GetComponent<States>().out_of_range){
                enemies.Add(other.gameObject);
                closestEnemy = other.gameObject;
                print(enemies.Count);
                other.GetComponent<States>().out_of_range = false;
            }
            else return;
        }
        else return;
    }

    private void OnTriggerExit(Collider other) {
        // This function triggers whenever an enemy leaves the sphere collider
        if(other.CompareTag("Skeleton") && !other.GetComponent<States>().animator.GetBool("isDead?")){
            enemies.Remove(other.gameObject);
            other.GetComponent<States>().out_of_range = true;
            print(enemies.Count);
            if(enemies.Count == 0){
                print("Exiting Lock ON");
                lock_on_state = false;
                Destroy(arrow);
                mainCamera.lockedOn = false;
                ui_lock.sprite = lockImages[0];
            }
        }
        else return;
    }

    void Update()
    {
        // When the player presses "Q", for every enemy within range, we calculate the distance between them and the player
        // we then check for the shortest path and based on that, we know which enemy to look at
        if(Input.GetKeyDown(KeyCode.Q)){
            if(enemies.Count > 0) lock_on_state = !lock_on_state;

            if(lock_on_state){ 
                getClosestTarget();
            }
            else{
                Destroy(arrow);
                mainCamera.lockedOn = false;
                ui_lock.sprite = lockImages[0];
            }
            // this destroys or removes the arrow from the game when the lock_on_state is false         
        }
        if(Input.GetKeyDown(KeyCode.F) && lock_on_state){
            getClosestTarget();
        }
        if(arrow && closestEnemy.GetComponent<States>().angry) arrow.transform.position = new Vector3(closestEnemy.transform.position.x, closestEnemy.transform.localPosition.y + 5.5f, closestEnemy.transform.position.z);
        else if(arrow) arrow.transform.position = new Vector3(closestEnemy.transform.position.x, closestEnemy.transform.localPosition.y + 3.5f, closestEnemy.transform.position.z);
    }

    private void FixedUpdate() {
        if(lock_on_state){
            if (Time.time >= nextRaycastTime && eye_Sight){
                if(eye_Sight.checkForWall(transform.position, closestEnemy.transform.position)){ 
                    lock_on_state = false;
                    mainCamera.lockedOn = false;
                    ui_lock.sprite = lockImages[0];
                    if(arrow) Destroy(arrow);
                }
                nextRaycastTime = Time.time + raycastInterval;
            }
            // Order matters. Subtract the player position from the enemy position to get the correct vector
            // The Player_Movement script references this variable to update the player's rotation
            enemyDirection = closestEnemy.transform.position - transform.position;
        }
    }

    // added this here to get around if eye_sight is non existent
    public bool checkForWall(Vector3 start, Vector3 end){
        Vector3 start_position = start;//new(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z);
        Vector3 end_position = end;//new(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);

        // Calculate the direction from one point to another
        Vector3 directionToPoint = (end_position - start_position).normalized;

        // Calculate the distance between two points
        float distanceToPlayer = Vector3.Distance(start_position, end_position);
        // Debug.DrawRay(new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z), directionToPoint * distanceToPlayer, Color.red);
        // Shoot a ray at a point
        if (Physics.Raycast(start_position, directionToPoint, out RaycastHit hitInfo, distanceToPlayer)) {            
            // Check if the raycast hit a wall
            print(hitInfo.collider.gameObject);
            if (hitInfo.collider.CompareTag("Wall")) {
                Debug.Log("There is a wall in front of the enemy blocking the path to the player.");
                return true;
            }
            else{
                Debug.Log("No wall in front of player.");
                // if(start == transform.position) ai_Navigation.playerFound = true;
                // ai_Navigation.agent.angularSpeed = 360;
                return false;
            }
        }

        return false;
    }
    // Use this function when an enemy dies to re-target the next enemy
    public void getClosestTarget(){
        if(arrow) Destroy(arrow);
        if(enemies.Count == 0){
            print("No more enemies");
            lock_on_state = false;
            mainCamera.lockedOn = false;
            ui_lock.sprite = lockImages[0];
        }
        foreach(var enemy in enemies) {
            Vector3 closestTarget = enemy.transform.position - transform.position;
            if(shortestPath > Math.Sqrt(Math.Pow(closestTarget.x, 2) + Math.Pow(closestTarget.z, 2)) || shortestPath == 0f){
                shortestPath = Math.Sqrt(Math.Pow(closestTarget.x, 2) + Math.Pow(closestTarget.z, 2));
                closestEnemy = enemy;
                enemyDirection = enemy.transform.position - transform.position;
            }
        }
        // if(eye_Sight){
            if(enemies.Count > 0 && checkForWall(transform.position, closestEnemy.transform.position)){
                print("Wall found");
                lock_on_state = false;
                mainCamera.lockedOn = false;
                ui_lock.sprite = lockImages[0];
                shortestPath = 0;
                return;
            }
            else if(enemies.Count > 0){
                print("Enemies found");
                // lock the camera
                mainCamera.lockedOn = true;
                // change the sprite to the locked image
                ui_lock.sprite = lockImages[1];
            }
        // }
        // set it back to 0 to reset
        shortestPath = 0;
        // this spawns an arrow above the head of the closest enemy
        if(enemies.Count != 0){
            arrow = Instantiate(arrowSpawn, closestEnemy.transform.localPosition, Quaternion.identity);
        }
    }
}
