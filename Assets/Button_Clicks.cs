using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Clicks : MonoBehaviour, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler, 
{
    public Button button;
    public GameObject player, menuManager;
    public string value;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        menuManager = GameObject.Find("CanvasManager");
    }
    
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     // print("In the button: "+eventData);
    //     // print(eventData.pointerEnter);
    //     value = eventData.pointerEnter.gameObject.name;
    //     // print(value);
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     // print("Out of the button: "+eventData);
    //     value = "";
    // }

    public void OnPointerClick(PointerEventData eventData)
    {
        // print("Click button: "+eventData);
        // print("Click button value: "+gameObject.name);
        player.GetComponent<Inventory>().UseItem(int.Parse(gameObject.name));
        try{
            if(player.GetComponent<Inventory>().Items[int.Parse(gameObject.name) - 1].GetComponent<Item_Stats>().amount <= 0){
                player.GetComponent<Inventory>().Items.Remove(player.GetComponent<Inventory>().Items[int.Parse(gameObject.name) - 1]);
                menuManager.GetComponent<OpenInventory>().PopulateInventory();
            }
            else{
                menuManager.GetComponent<OpenInventory>().PopulateInventory();
            }
        } catch{}
    }
}
