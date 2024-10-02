using System.Collections.Generic;
using UnityEngine;

public class Eye_Sight : MonoBehaviour
{
    public GameObject eye_ray_cast, player, enemy;
    float nextRaycastTime = 0f;
    float raycastInterval = 0.2f;
    // public List<GameObject> objectsSeen;
    // public GameObject player;
    // void Start()
    // {
        
    // }

    // private void OnTriggerEnter(Collider other) {
    //     // objectsSeen.Add(other.gameObject);
    //     // foreach(var obj in objectsSeen) {
    //     //     print(obj);
    //     // }
    //     if(other.gameObject.CompareTag("Player")){
    //         print("I see you");
    //         checkForWall();
    //         // objectsSeen.Add(other.gameObject);
    //     }
    //     else return;
    // }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if (Time.time >= nextRaycastTime){
                checkForWall();
                nextRaycastTime = Time.time + raycastInterval;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        // objectsSeen.Remove(other.gameObject);
        // foreach(var obj in objectsSeen) {
        //     print(obj);
        // }
        if(other.gameObject.CompareTag("Player")){
            print("I DON'T see you");
            // objectsSeen.Remove(other.gameObject);
        }
        else return;
    }

    void checkForWall(){
        Vector3 enemyPosition = enemy.transform.position;
        Vector3 playerPosition = player.transform.position;

        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = (playerPosition - enemyPosition).normalized;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);

        // Perform the raycast
        if (Physics.Raycast(enemyPosition, directionToPlayer, out RaycastHit hitInfo, distanceToPlayer)) {
            // Check if the raycast hit something that is not the player
            if (hitInfo.collider.CompareTag("Wall")) {
                Debug.Log("There is a wall in front of the enemy blocking the path to the player.");
            }
            else{
                Debug.Log("No wall in front of player.");
            }
        }
    }

}
