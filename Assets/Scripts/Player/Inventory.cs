using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Items;
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)){
            foreach(GameObject item in Items){
                print(item.name);
            }
        }
    }
}
