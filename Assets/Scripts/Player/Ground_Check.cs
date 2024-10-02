using UnityEngine;

public class Ground_Check : MonoBehaviour
{
    public GameObject ground;
    public bool isGrounded;
    public Animator player;
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")){ 
            isGrounded = true;
            player.SetBool("isGrounded",isGrounded);
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Ground")){ 
            isGrounded = false;
            player.SetBool("isGrounded",isGrounded);
        }
        else if(other.gameObject.CompareTag("Skeleton")){
            Rigidbody skelRb = other.rigidbody;
            skelRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else return;
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Skeleton")){
            Rigidbody skelRb = other.rigidbody;
            skelRb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else return;
    }

}
