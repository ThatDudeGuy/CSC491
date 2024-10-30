using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]  // This attribute allows Unity to serialize the class and create a JSON based off the public variables it contains
public class EnemyData{
    public string enemyName;
    public Vector3 position;
    public bool playerFound;

    public EnemyData(string name, Vector3 pos, bool found){
        enemyName = name;
        position = pos;
        playerFound = found;
    }
}

[Serializable]
public class PlayerData{
    public Vector3 position;
    public int health;

    public PlayerData(Vector3 pos, int hp){
        position = pos;
        health = hp;
    }
}

[Serializable]
public class ItemData{
    public string itemName;
    public bool isPickedUp;

    public ItemData(string name, bool pickedUp){
        itemName = name;
        isPickedUp = pickedUp;
    }
}

[Serializable]
public class GameData{
    // private const string POSITION = "Position", PLAYER_FOUND = "Player_Found"; 
    public List<EnemyData> enemy_positions;
    public List<PlayerData> playerData;
    public List<ItemData> itemData;
    // public List<InventoryData> inventoryData;

    public GameData(){
        // enemy_positions = new Dictionary<string, Dictionary<string, object>>();
        enemy_positions = new List<EnemyData>();
        playerData = new List<PlayerData>();
        itemData = new List<ItemData>(); 
    }

    // Input = The player game object
    public void collectPlayerData(GameObject player){
        playerData.Add(new PlayerData(player.transform.position, player.GetComponent<Health_Tracking>().health));
    }
    // public void collectInventoryData(string itemType, int itemAmount){
    //     this.itemType = itemType;
    //     this.itemAmount = itemAmount;
    //     // create key value from here -> dictionary[itemType].Add(itemAmount)
    // }

    // Input = an array of all enemies in the scene as long as it wasn't summoned by the mage
    public void collectEnemyData(GameObject[] enemies){
        foreach(GameObject enemy in enemies){
            if(!enemy.name.Contains("Clone") && !enemy.name.Contains("Mage")){ // REMOVE THE MAGE CHECK WHEN Ai_Navigation IS ADDED AND COMPLETE FOR THE MAGE
                enemy_positions.Add(new EnemyData(enemy.name, enemy.transform.position, enemy.GetComponent<Ai_Navigation>().playerFound));
            }
        }
        
    }

    // Input = an array of all items in the scene
    public void collectItemData(){
        GameObject[] items = GameObject.FindGameObjectsWithTag("Potion");
        foreach(GameObject item in items){
            Debug.Log(item.name);
            itemData.Add(new ItemData(item.name, item.GetComponent<Item_Behaviour>().isPickedUp));
        }
        Debug.Log("Item Data list = "+itemData);
    }

    public void loadPlayerData(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerData[0].position;
        player.GetComponent<Health_Tracking>().health = playerData[0].health;
    }
    public void loadEnemyData(){
        foreach(EnemyData enemyDataEntry in enemy_positions){
            GameObject enemy = GameObject.Find(enemyDataEntry.enemyName);
            enemy.GetComponent<Ai_Navigation>().playerFound = enemyDataEntry.playerFound;
            enemy.transform.position = enemyDataEntry.position;
        }
    }
    public void loadItemData(){
        foreach(ItemData itemDataEntry in itemData){
            GameObject item = GameObject.Find(itemDataEntry.itemName);
            item.GetComponent<Item_Behaviour>().isPickedUp = itemDataEntry.isPickedUp;
        }
    }
}



public class SaveData : MonoBehaviour
{
    // private Dictionary<int, GameData> data = new Dictionary<int, GameData>();
    private GameData gameData = new();
    // private static string directory = Application.persistentDataPath + "/SaveData";
    // private static string filePath = directory + "/SaveFile00";

    // void Start()
    // {
    //     LoadGame();
    // }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.RightShift)){ 
    //         gameData.collectEnemyData(GameObject.FindGameObjectsWithTag("Skeleton"));
    //         gameData.collectPlayerData(GameObject.FindGameObjectWithTag("Player"));
    //         gameData.collectItemData();
    //         SaveGame();
    //     }
    //     // if(Input.GetKeyDown(KeyCode.O)) foreach(var obj in gameData.itemData) print("item data index = "+obj); 
    // }

    public void SaveGame(){
        string directory = Application.persistentDataPath + "/SaveData";
        string filePath = directory + "/SaveFile00.json";
        string data = JsonUtility.ToJson(gameData, true);
        print(data);
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }
        System.IO.File.WriteAllText(filePath, data);
        Debug.Log("Saved Enemy Data!");
    }

    public void LoadGame(){
        try{
            string directory = Application.persistentDataPath + "/SaveData";
            string filePath = directory + "/SaveFile00.json";
            // string path = Application.persistentDataPath +"/SaveData/enemy_data.json";
            if (System.IO.File.Exists(filePath))
            {
                string jsonData = System.IO.File.ReadAllText(filePath);
                // JsonUtility.FromJsonOverwrite(jsonData, this);
                GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
                loadedData.loadPlayerData();
                loadedData.loadEnemyData();
                loadedData.loadItemData();
                print("Loading game = "+jsonData);
                // GameData data = JsonUtility.FromJson<GameData>(json);

                // Debug.Log("Player Level: " + data.playerLevel);
            }
            else
            {
                Debug.Log("Save file not found!");
            }
        }
        catch(Exception e) {
            Debug.LogError("Save File Corruption: "+e);
        }
    }
}


/*
    Player
        - health
        - position
    Enemies
        - position
        - bool if they are patrolling or chasing the player
    Items
        - bool if they were picked up (if they were picked up, do not render/spawn them)
    Inventory
        - this should be a dictionary with the Slot as the Key and 2 values being the type and amount of the item

*/


// public void collectEnemyData(GameObject[] enemies){
    //     Dictionary<string, object> tempDict;
    //     foreach(GameObject enemy in enemies){
    //         if(!enemy.name.Contains("Mage") && !enemy.name.Contains("Clone")){
    //             tempDict = new Dictionary<string, object>{
    //                 { POSITION, enemy.transform.position }
    //             };
    //             enemy_positions.Add(enemy.name, tempDict);

    //             enemy_positions[enemy.name].Add(PLAYER_FOUND, enemy.GetComponent<Ai_Navigation>().playerFound);
    //         }
    //     }
    //     foreach(KeyValuePair<string, Dictionary<string, object>> kvp in enemy_positions) {
    //         Debug.Log("Enemy Data:: Key = "+kvp.Key+"/ Position = "+(Vector3)kvp.Value[POSITION]+"/ PlayerFound = "+kvp.Value[PLAYER_FOUND]);
    //     }
        
    // }