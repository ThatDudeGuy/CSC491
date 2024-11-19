using UnityEngine;

public class Item_Behaviour : MonoBehaviour
{
    public int healthRegen;
    public Health_Tracking playerHealth;
    public CapsuleCollider playerCapsuleCollider;
    public bool isPickedUp;
    public Inventory inventory;
    public GameObject menuManager;

    void Start()
    {
        if(gameObject.name.Contains("small")){
            healthRegen = 20;
        } 
        else if(gameObject.name.Contains("medium")){
            healthRegen = 40;
        }  
        // tag = "hello";
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Tracking>();
        playerCapsuleCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            menuManager = GameObject.Find("CanvasManager");
            menuManager.GetComponent<PauseMenu>().interact.SetActive(true);
        }
        else return;
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            menuManager = GameObject.Find("CanvasManager");
            menuManager.GetComponent<PauseMenu>().interact.SetActive(false);
        }
        else return;
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && other == playerCapsuleCollider && Input.GetKeyDown(KeyCode.E)){
            menuManager = GameObject.Find("CanvasManager");
            menuManager.GetComponent<PauseMenu>().interact.SetActive(false);
            inventory.Add(gameObject);
            gameObject.SetActive(false);
            isPickedUp = true;
        }
        else return;
    }
}
