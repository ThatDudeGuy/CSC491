using UnityEngine;

public class arrowBehaviour : MonoBehaviour
{
    public float lifetime = 3f;
    public bool beginTimer;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
        else if(other.name == "Skeleton_Rogue"){
            return;
        }
        else if(other.name == "lockOn_range_trigger"){
            return;
        }
        else if(other.CompareTag("CrossBow")){
            return;
        }
        else{
            print(other.gameObject.name);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            beginTimer = true;
        }
    }

    private void Update() {
        if(beginTimer){
            lifetime -= Time.deltaTime;
            if(lifetime <= 0) Destroy(gameObject);
        }
    }
}
