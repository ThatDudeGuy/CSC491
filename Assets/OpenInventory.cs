using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenInventory : MonoBehaviour
{
    public GameObject player, inventoryUI;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PopulateInventory(){
        inventoryUI.SetActive(true);
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

        for(int i = 0; i < player.GetComponent<Inventory>().Items.Count; i++) {
            slots[i].GetComponent<Image>().sprite = player.GetComponent<Inventory>().Items[i].GetComponent<Item_Stats>().item_image;
            slots[i].GetComponentInChildren<TextMeshProUGUI>().text = player.GetComponent<Inventory>().Items[i].GetComponent<Item_Stats>().amount.ToString();
        }
        for(int i = player.GetComponent<Inventory>().Items.Count; i < 6; i++) {
            slots[i].GetComponent<Image>().sprite = null;
            slots[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void CloseInventory(){
        inventoryUI.SetActive(false);
    }
}
