using UnityEngine;

public class Ground_Check : MonoBehaviour
{
    public GameObject ground;
    public bool isGrounded;
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Ground")) isGrounded = false;
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
