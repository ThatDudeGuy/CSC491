using UnityEngine;
using UnityEngine.UI;

enum Item_Type
{
    Potion, Gold
}

public class Item_Stats : MonoBehaviour
{
    public string item_name;
    public Sprite item_image;
    public int amount = 1;
}
