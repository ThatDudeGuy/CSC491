using UnityEngine;

public class arrowBehaviour : MonoBehaviour
{
    public float lifetime = 3f;
    public bool beginTimer;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.GetComponent<Health_Tracking>().damagePlayer(10);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Floor") || other.CompareTag("Ground") || other.CompareTag("Bricks")){
            print(other.gameObject.name);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            beginTimer = true;
        }
        else return;
    }

    private void Update() {
        if(beginTimer){
            lifetime -= Time.deltaTime;
            if(lifetime <= 0) Destroy(gameObject);
        }
    }
}
