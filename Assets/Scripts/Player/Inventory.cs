using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Items;
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)){
            print("ITEMS: ");
            foreach(GameObject item in Items){
                print(item.GetComponent<Item_Stats>().item_name+" : "+item.GetComponent<Item_Stats>().amount);
            }
        }
    }

    public void Add(GameObject item){
        if(Items.Count == 0){
            Items.Add(item);
            return;
        }
        for(int i = 0; i < Items.Count; i++) {
            print(item.GetComponent<Item_Stats>().item_name+" : "+Items[i].GetComponent<Item_Stats>().item_name);
            if(item.GetComponent<Item_Stats>().item_name == Items[i].GetComponent<Item_Stats>().item_name){
                Items[i].GetComponent<Item_Stats>().amount += item.GetComponent<Item_Stats>().amount;
                return;
            }
        }

        Items.Add(item);
    }

    public void UseItem(int index){
        if(index > Items.Count) return;

        Debug.Log("Used Item: "+Items[index - 1].GetComponent<Item_Stats>().item_name);
        if(Items[index - 1].GetComponent<Item_Stats>().item_name.Contains("Health")){
            Items[index - 1].GetComponent<Item_Stats>().amount -= 1;
            GetComponent<Health_Tracking>().regenHealth(Items[index - 1].GetComponent<Item_Behaviour>().healthRegen);
        }
    }
}
