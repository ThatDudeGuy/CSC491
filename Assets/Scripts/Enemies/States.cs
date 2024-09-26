using UnityEngine;

public class States : MonoBehaviour
{
    public bool out_of_range;
    public Animator animator;
    void Start()
    {
        out_of_range = true;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)) animator.SetBool("Attack", true);
        if(Input.GetKeyUp(KeyCode.L)) animator.SetBool("Attack", false);
    }

}
