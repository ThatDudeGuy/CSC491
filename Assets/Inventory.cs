using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Button[] itemSlots;
    private List<Item> items = new List<Item>();

    void Start()
    {
        inventoryPanel.SetActive(false);
        // Initialize item slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            int index = i; // Capture the index
            itemSlots[i].onClick.AddListener(() => ShowItemDetails(index));
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        UpdateInventoryUI();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < items.Count)
            {
                itemSlots[i].GetComponentInChildren<Text>().text = items[i].itemName; // Display item name
            }
            else
            {
                itemSlots[i].GetComponentInChildren<Text>().text = ""; // Clear slot
            }
        }
    }

    private void ShowItemDetails(int index)
    {
        if (index < items.Count)
        {
            Debug.Log($"Item Selected: {items[index].itemName}");
            // Implement further item details logic here
        }
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    // Add more item properties as needed


    
}
