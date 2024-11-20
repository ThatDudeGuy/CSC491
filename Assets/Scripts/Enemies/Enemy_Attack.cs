using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public Enemy_Weapon weapon;
    public States enemy_state;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Enemy_Weapon>();
        enemy_state = GetComponent<States>();
    }

    private void Update() {
        if(enemy_state.angry) weapon.SetDamageMultiplier();
    }

    public void endAttack(){
        GetComponent<Animator>().SetBool("Attack", false);
    }
    public void call_damageOn(){
        weapon.damageOn();
    }
    public void call_damageOff(){
        weapon.damageOff();
    }
}
