using System;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cameraPoint, mainCamera;
    public Animator animator;
    public float rotationSpeed = 250f, moveSpeed = 5f;
    float cameraRotation;
    int direction_to_face;
    Vector3 rotateTo, movement, player_Y_vector;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // <- HERE is where I have set the game's target framerate. It runs at 60 frames per second.
        rb = GetComponent<Rigidbody>();
        cameraPoint = GameObject.FindGameObjectWithTag("CameraPoint");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        animator = GetComponent<Animator>();
        player_Y_vector = transform.rotation.eulerAngles;
    }

    // Update is called every frame
    private void Update() {
        /*
            From the "START" to "END" below, this handles 8 directional movement and updates
            the way the character is facing depending on which buttons the player is holding
        */

        // START
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)){
            direction_to_face = 4;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)){
            direction_to_face = 5;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }
        else if(Input.GetKey(KeyCode.W)){
            direction_to_face = 0;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }


        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)){
            direction_to_face = 6;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }
        else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)){
            direction_to_face = 7;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }
        else if(Input.GetKey(KeyCode.S)){
            direction_to_face = 1;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }


        if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)){
            direction_to_face = 2;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)){
            direction_to_face = 3;
            updateDirection(direction_to_face);
            animator.SetBool("isWalking", true);
        }
        // END

        // if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)){
        //     player_Y_vector = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        // }

        // if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)){
        //     player_Y_vector = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        // }
        
        /*
            From the "START" to "END" below, this checks if none of the movement buttons are being held,
            if so, then we know the player is no longer moving so we update a couple of booleans
            to stop and play the correct animations based off of the animator.
            Also resetting the movement speed back to its original value in the case where the player
            is sprinting and releases the movement keys to stop moving.

            Pressing shift will allow the player to "sprint". To stop sprinting, the player must release
            any movement buttons. We can change this behaviour if we please.
        */

        // START
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            moveSpeed = 5f;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && animator.GetBool("isWalking")){
            animator.SetBool("isRunning", true);
            moveSpeed = 10f;
        }
        // END
    }

    private void FixedUpdate() {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     rb.AddForce(0f, 300f, 0f);
        // }
        

        if(Input.GetKey(KeyCode.W)){
            cameraDirectionMovement(mainCamera.transform.forward);
        }
        if(Input.GetKey(KeyCode.S)){
            cameraDirectionMovement(-mainCamera.transform.forward);
        }
        if(Input.GetKey(KeyCode.D)){
            cameraDirectionMovement(mainCamera.transform.right);
        }
        if(Input.GetKey(KeyCode.A)){
            cameraDirectionMovement(-mainCamera.transform.right);
        }
    }

    private void updateDirection(int face_direction){
        // Represents the keys being held
        // 0 = W, 
        // 1 = S, 
        // 2 = D, 
        // 3 = A,
        // 4 = W & D,
        // 5 = W & A,
        // 6 = S & D,
        // 7 = S & A
        switch(face_direction){
            case 0:
                calculateEulerAngle(0);
                smoothRotation();
                break;
            case 1:
                calculateEulerAngle(180);
                smoothRotation();
                break;
            case 2:
                calculateEulerAngle(90);
                smoothRotation();
                break;
            case 3:
                calculateEulerAngle(-90);
                smoothRotation();
                break;
            case 4:
                calculateEulerAngle(45);
                smoothRotation();
                break;
            case 5:
                calculateEulerAngle(-45);
                smoothRotation();
                break;
            case 6:
                calculateEulerAngle(135);
                smoothRotation();
                break;
            case 7:
                calculateEulerAngle(-135);
                smoothRotation();
                break;
        }
    }

    // Calculates the correct angle to rotate to based on where the camera is facing
    private void calculateEulerAngle(int angleValue){
        cameraRotation = cameraPoint.transform.rotation.eulerAngles.y;
        rotateTo = transform.rotation.eulerAngles;
        rotateTo.y = cameraRotation + angleValue;
    }

    // Handle movement in a specific camera direction
    private void cameraDirectionMovement(Vector3 direction)
    {
        movement = moveSpeed * Time.deltaTime * direction;
        rb.MovePosition(rb.position + movement);
    }

    private void smoothRotation() {
        float angleDifference = Mathf.DeltaAngle(player_Y_vector.y, rotateTo.y);

        if (Math.Abs(angleDifference) > 0.1f) {  
            // If the difference is significant
            float rotationStep = Math.Sign(angleDifference) * rotationSpeed * Time.deltaTime;

            // Prevent overshooting
            if (Math.Abs(rotationStep) > Math.Abs(angleDifference)) {
                rotationStep = angleDifference;
            }

            player_Y_vector.y += rotationStep;
            transform.rotation = Quaternion.Euler(0f, player_Y_vector.y, 0f);
        }
    }

}
