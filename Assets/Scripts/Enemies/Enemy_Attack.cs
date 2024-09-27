using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public Enemy_Weapon weapon;
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
}
