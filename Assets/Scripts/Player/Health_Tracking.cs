using UnityEngine;
using UnityEngine.UI;

public class Health_Tracking : MonoBehaviour
{
    public int health;
    public Animator animator;
    public Slider staminaBar;
    public Player_Movement player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player_Movement>();
        // staminaBar = 
    }

    private void FixedUpdate(){
        if(animator.GetBool("isRunning")) staminaBar.value -= 0.5f;
        else if(!animator.GetBool("isRunning") && staminaBar.value < 100) staminaBar.value += 0.2f;
        if(staminaBar.value <= 0){
            animator.SetBool("isRunning", false);
            player.moveSpeed = 5f;
        }
    }
}
