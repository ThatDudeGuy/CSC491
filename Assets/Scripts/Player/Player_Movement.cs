using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public LockOn lockOn;
    public Rigidbody rb;
    public GameObject cameraPoint, mainCamera, stepRayLower, stepRayUpper;
    public Animator animator;
    public float cameraRotation, moveSpeed = 5f, stepHeight = 0.65f, stepSmooth = 10f;
    int direction_to_face;
    Vector3 rotateTo, movement, player_Y_vector;
    public bool isJumping;
    public Ground_Check ground_Check;
    // public CharacterController controller;

    void Start()
    {
        Application.targetFrameRate = 60; // <- HERE is where I have set the game's target framerate. It runs at 60 frames per second.
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        cameraPoint = GameObject.FindGameObjectWithTag("CameraPoint");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        animator = GetComponent<Animator>();
        player_Y_vector = transform.rotation.eulerAngles;
        isJumping = false;
        lockOn = GetComponent<LockOn>();
        stepHeight = 0.65f;
        stepSmooth = 10f;
        ground_Check = GetComponent<Ground_Check>();
        // stepRayUpper.transform.position = new Vector3(0f, stepHeight, 0.6f);
        // controller = GetComponent<CharacterController>();
    }

    // private void Awake() {
    //     stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    // print(new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z));
    // }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            isJumping = true;
        }

        handlePlayerInput();
        handleSprinting();
    }

    private void LateUpdate(){
        // Ensures a bug won't occur that constantly spins the Rigidbody in the Y direction
        // or knocks the player over
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    private void FixedUpdate() {
        if(lockOn.lock_on_state) followEnemy();

        if(isJumping && ground_Check.isGrounded == true){
            rb.AddForce(0f, 300f, 0f);
            // if(transform.position.y > 1.5f){print("Threshold reached");}//rb.mass = 5;}
            isJumping = false;
        }
        // if(transform.position.y > 1.5f){rb.AddForce(0f, -100f, 0f);}
        
        Vector3 movementDirection = Vector3.zero;
        
        if(Input.GetKey(KeyCode.W)){
            movementDirection += mainCamera.transform.forward;
        }
        if(Input.GetKey(KeyCode.S)){
            movementDirection += -mainCamera.transform.forward;
        }
        if(Input.GetKey(KeyCode.D)){
            movementDirection += mainCamera.transform.right;
        }
        if(Input.GetKey(KeyCode.A)){
            movementDirection += -mainCamera.transform.right;
        }

        if (movementDirection != Vector3.zero) Movement(movementDirection);

        // stepClimb();
    }

    private void handlePlayerInput(){
        /*
            This handles 8 directional movement and updates
            the way the character is facing depends on which buttons the player is holding
        */

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

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)){
            player_Y_vector = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)){
            player_Y_vector = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }

    private void handleSprinting() {
        /*
            This checks if none of the movement buttons are being held,
            if so, then we know the player is no longer moving so we update a couple of booleans
            to stop and play the correct animations based off of the animator.
            Also resetting the movement speed back to its original value in the case where the player
            is sprinting and releases the movement keys to stop moving.

            Pressing shift will allow the player to "sprint". To stop sprinting, the player must release
            any movement buttons. We can change this behaviour if we please.
        */

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            moveSpeed = 5f;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && animator.GetBool("isWalking")){
            animator.SetBool("isRunning", true);
            moveSpeed = 10f;
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
        if(!lockOn.lock_on_state){
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
    }

    // This will calculate the correct angle at which to face when locked on to an enemy
    private void followEnemy(){
        transform.rotation = Quaternion.LookRotation(lockOn.enemyDirection);
    }

    // Calculates the correct angle to rotate to based on where the camera is facing
    private void calculateEulerAngle(int angleValue){
        cameraRotation = cameraPoint.transform.rotation.eulerAngles.y;
        rotateTo = transform.rotation.eulerAngles;
        rotateTo.y = cameraRotation + angleValue;
    }

    // Handle movement in a specific camera direction
    private void Movement(Vector3 direction){
        // keep the Y value at 0 to eliminate the "bouncing" effect and prevents the player from
        // moving towards to the camera when rotated in a certain direction 
        direction.y = 0f;
        // Without normalization, diagonal movement vectors have a magnitude of ~1.41 (square root of 2), while straight movement vectors have a magnitude of 1, resulting in faster diagonal movement.
        // normalizing the vector helps keep the direction at which it travels uniform
        direction.Normalize();
        movement = moveSpeed * Time.deltaTime * direction;
        rb.MovePosition(rb.position + movement);
        
        // Vector3 movement = moveSpeed * Time.deltaTime * direction;
        // controller.Move(movement);
    }

    private void smoothRotation(){
        // Calculate the difference in angles (shortest path)
        float angleDifference = Mathf.DeltaAngle(player_Y_vector.y, rotateTo.y);
        
        // Define the speed of rotation (degrees per second)
        float rotationSpeed = 200f; // Adjust this value to control the turning speed

        // If the difference is small, we can consider the rotation done
        if (Mathf.Abs(angleDifference) > 0.1f){
            // Calculate how much to rotate this frame, based on rotation speed and frame rate
            // Mathf.Sign() returns a 1 or -1 depending on the value that is passed into it
            // if < 0 return -1 else return 1
            float rotationStep = Mathf.Sign(angleDifference) * rotationSpeed * Time.deltaTime;

            // Prevent overshooting the target angle by clamping the step
            if (Mathf.Abs(rotationStep) > Mathf.Abs(angleDifference)){
                rotationStep = angleDifference; // Just finish the rotation
            }

            // Apply the rotation step to the current Y angle
            player_Y_vector.y += rotationStep;

            // Update the character's rotation
            transform.rotation = Quaternion.Euler(0f, player_Y_vector.y, 0f);
        }
    }

    void stepClimb(){
        RaycastHit hitLower;
        if(Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f)){
            RaycastHit hitUpper;
            if(!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f)){
                rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }
    }
}
