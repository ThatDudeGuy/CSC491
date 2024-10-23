using UnityEngine;

public class Bow_And_Arrow : MonoBehaviour
{
    public Enemy_Weapon weapon;
    public GameObject arrow;


    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Enemy_Weapon>();
    }

    public void call_damageOn(){
        weapon.damageOn();
    }
    public void call_damageOff(){
        weapon.damageOff();
    }

    
    public void spawnArrow(){
        Instantiate(arrow, weapon.transform);
            
    } 
}

// function to spawn an arrow
// the arrow needs to travel at a certain speed, 
// write a script for arrow and when it hits something
// 
