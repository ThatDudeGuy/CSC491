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
    public Vector3 enemyDirection = new(0,0,0);
    // Below variables are set in the inspector
    public Sprite[] lockImages; 
    public Image ui_lock;

    void Start()
    {
        lock_on_range = GetComponent<SphereCollider>();
    }
    

    private void OnTriggerStay(Collider other) {
        // This function runs or is "triggered" whenever an enemy stays within the sphere collider attached to the character
        // for more than 1 second I believe
        if(other.CompareTag("Skeleton")){
            if(other.GetComponent<States>().out_of_range){
                enemies.Add(other.gameObject);
                print(enemies.Count);
                other.GetComponent<States>().out_of_range = false;
            }
        }
        else return;
    }

    private void OnTriggerExit(Collider other) {
        // This function triggers whenever an enemy leaves the sphere collider
        if(other.CompareTag("Skeleton")){
            enemies.Remove(other.gameObject);
            other.GetComponent<States>().out_of_range = true;
            print(enemies.Count);
            if(enemies.Count == 0){ 
                lock_on_state = false;
                Destroy(arrow);
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
                ui_lock.sprite = lockImages[1];
            }
            else{
                Destroy(arrow);
                ui_lock.sprite = lockImages[0];
            }
            // this destroys or removes the arrow from the game when the lock_on_state is false         
        }
        if(Input.GetKeyDown(KeyCode.F) && lock_on_state){
            getClosestTarget();
        }
    }

    private void FixedUpdate() {
        if(lock_on_state){
            // Order matters. Subtract the player position from the enemy position to get the correct vector
            // This is here as the Player_Movement script references this variable to update the player's rotation
            enemyDirection = closestEnemy.transform.position - transform.position;
        }
    }

    private void getClosestTarget(){
        if(arrow) Destroy(arrow);
        foreach(var enemy in enemies) {
            Vector3 closestTarget = enemy.transform.position - transform.position;
            if(shortestPath > Math.Sqrt(Math.Pow(closestTarget.x, 2) + Math.Pow(closestTarget.z, 2)) || shortestPath == 0f){
                shortestPath = Math.Sqrt(Math.Pow(closestTarget.x, 2) + Math.Pow(closestTarget.z, 2));
                closestEnemy = enemy;
                enemyDirection = enemy.transform.position - transform.position;
            }
        }
        //set it back to 0 to reset
        shortestPath = 0;
        // this spawns an arrow above the head of the closest enemy
        arrow = Instantiate(arrowSpawn, new Vector3(closestEnemy.transform.localPosition.x,closestEnemy.transform.localPosition.y + 3.5f ,closestEnemy.transform.localPosition.z), Quaternion.identity);
    }
}
