using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(KeyCode.W)){
        //     rb.AddForce(0f, 0f, 2f);
        // }
    }
    private void FixedUpdate() {
        /*
            In order to have the orientation of 3d movement correct, we need to define
            an absolute North, South, East, and West. When the player presses the "D" key,
            they should rotate/face and move to the East. When the player presses "A" they should
            move to the West, "W" for the north, and "S" for the South

            The camera will determine this for us. Whichever direction the camera is facing, that
            will be defined as North. 

            Our camera will move based off of mouse input. We will rotate about a point, the point
            being the player, in the direction the user slides their mouse. 
        */



        if(Input.GetKey(KeyCode.W)){
            rb.AddForce(0f, 0f, 20f);
        }
        if(Input.GetKey(KeyCode.S)){
            rb.AddForce(0f, 0f, -20f);
        }
        if(Input.GetKey(KeyCode.A)){
            rb.AddForce(20f, 0f, 0f);
        }
        if(Input.GetKey(KeyCode.D)){
            rb.AddForce(-20f, 0f, 0f);
        }
    }
}
