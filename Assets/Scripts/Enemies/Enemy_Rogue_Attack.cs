using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Rogue_Attack : MonoBehaviour
{
    public GameObject arrow, crossbow, player;
    public Animator animator;
    public float arrowSpeed;
    public bool player_found;
    // Start is called before the first frame update
    void Start()
    {
        crossbow = GameObject.FindGameObjectWithTag("CrossBow");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.O)){
            animator.SetBool("inRange", !animator.GetBool("inRange"));
        }

        if(animator.GetBool("inRange") && !animator.GetBool("isDead?")) transform.LookAt(player.transform.position);

    }

    // Referenced as animation events
    public void ShootArrow(){
        animator.SetTrigger("Shoot");
        GameObject copy = Instantiate(arrow, crossbow.transform.position, transform.rotation);
        copy.GetComponent<Rigidbody>().AddRelativeForce(0,0,arrowSpeed);
    }

    public void ReloadAnim(){
        animator.SetTrigger("Reload");
    }

    public void RestartAnim(){
        animator.SetTrigger("Shoot");
    }
}
