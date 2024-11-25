using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgroEnemies : MonoBehaviour
{
    public List<GameObject> Rogues, Warriors, Mages;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            foreach(GameObject rogue in Rogues){
                rogue.GetComponent<Enemy_Rogue_Attack>().animator.SetBool("inRange", true);
            }
            foreach(GameObject warrior in Warriors){
                warrior.GetComponent<Ai_Navigation>().playerFound = true;
            }
            foreach(GameObject mage in Mages){
                mage.GetComponent<Mage_Attacks>().begin_spell = true;
            }
        }
    }

    private void Update() {
        if(Rogues.Count == 0 && Warriors.Count == 0 && Mages.Count == 0){ 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("ToBeContinued");
        }
    }
}
