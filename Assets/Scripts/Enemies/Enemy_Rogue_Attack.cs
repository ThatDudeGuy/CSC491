using UnityEngine;

public class Enemy_Rogue_Attack : MonoBehaviour
{
    public GameObject arrow, crossbow;
    public Animator animator;
    public float arrowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        crossbow = GameObject.FindGameObjectWithTag("CrossBow");
        animator.SetBool("inRange", true);
    }

    // public void endAttack(){
    //     GetComponent<Animator>().SetBool("Attack", false);
    // }
    // public void call_damageOn(){
    //     weapon.damageOn();
    // }
    // public void call_damageOff(){
    //     weapon.damageOff();
    // }

    // Referenced as animation events
    public void ShootArrow(){
        animator.SetTrigger("Shoot");
        GameObject copy = Instantiate(arrow, crossbow.transform.position, Quaternion.identity);
        copy.GetComponent<Rigidbody>().AddRelativeForce(0,0,arrowSpeed);
    }

    public void ReloadAnim(){
        animator.SetTrigger("Reload");
    }
}
