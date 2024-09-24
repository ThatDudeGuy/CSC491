using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    // Player State: LockOn State
    public bool lock_on_state;
    public Collider lock_on_range;
    public GameObject arrowSpawn, arrow;
    public List<GameObject> enemies = new();
    public int listLength;
    // Start is called before the first frame update
    void Start()
    {
        lock_on_range = GetComponent<SphereCollider>();
        // print(lock_on_range);
    }
    

    private void OnTriggerStay(Collider other) {
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
        if(other.CompareTag("Skeleton")){
            enemies.Remove(other.gameObject);
            other.GetComponent<States>().out_of_range = true;
            print(enemies.Count);
            if(enemies.Count == 0){ 
                lock_on_state = false;
                Destroy(arrow);
            }
        }
        else return;
    }

    void Update()
    {
        // print(lock_on_range);
        if(Input.GetKeyDown(KeyCode.Q)){
            if(enemies.Count > 0) lock_on_state = !lock_on_state;
            if(lock_on_state){
                arrow = Instantiate(arrowSpawn, new Vector3(0f,enemies[0].transform.localPosition.y + 3.5f ,0f), Quaternion.identity);
            }
            else{
                Destroy(arrow);
            }
            // print()
        }
    }
}
