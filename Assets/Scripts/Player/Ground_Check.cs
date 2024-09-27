using System.Collections;
using System.Collections.Generic;
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
    }

}
