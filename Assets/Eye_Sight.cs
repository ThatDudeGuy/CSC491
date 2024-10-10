using UnityEngine;

public class Eye_Sight : MonoBehaviour
{
    public GameObject eye_ray_cast, player, enemy;
    public Ai_Navigation ai_Navigation;
    float nextRaycastTime = 0f;
    float raycastInterval = 0.2f;

    private void Start() {
        ai_Navigation = enemy.GetComponent<Ai_Navigation>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if (Time.time >= nextRaycastTime){
                checkForWall(enemy.transform.position, player.transform.position);
                nextRaycastTime = Time.time + raycastInterval;
            }
        }
    }

    // private void OnTriggerExit(Collider other) {
    //     if(other.gameObject.CompareTag("Player")){
    //         print("I DON'T see you");
    //     }
    //     else return;
    // }

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
                if(start == transform.position) ai_Navigation.playerFound = true;
                // ai_Navigation.agent.angularSpeed = 360;
                return false;
            }
        }

        return false;
    }

}
