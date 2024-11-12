using UnityEngine;

public class Enemy_Rogue_Attack : MonoBehaviour
{
    public GameObject arrow, crossbow, player;
    public Animator animator;
    public float arrowSpeed;
    public bool targetPlayer;
    // Start is called before the first frame update
    void Start()
    {
        crossbow = GameObject.FindGameObjectWithTag("CrossBow");
        player = GameObject.FindGameObjectWithTag("Player");
        animator.SetBool("inRange", true);
    }

    private void Update() {
        if(targetPlayer) transform.LookAt(player.transform.position);
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
        GameObject copy = Instantiate(arrow, crossbow.transform.position, crossbow.transform.rotation);
        copy.GetComponent<Rigidbody>().AddRelativeForce(0,0,arrowSpeed);
    }

    public void ReloadAnim(){
        animator.SetTrigger("Reload");
    }
}
