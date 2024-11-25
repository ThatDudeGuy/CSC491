using System.Collections.Generic;
using UnityEngine;

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
}
