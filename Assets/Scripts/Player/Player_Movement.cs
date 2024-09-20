using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cameraPoint, mainCamera;
    float cameraRotation, moveSpeed = 0;
    int direction_to_face;
    Vector3 rotateTo, cameraDirection;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        cameraPoint = GameObject.FindGameObjectWithTag("CameraPoint");
    }

    // Update is called once per frame
    // void Update()
    // {
        // if(Input.GetKey(KeyCode.W)){
        //     rb.AddForce(0f, 0f, 2f);
        // }
    // }
    private void Update() {
        /*
            In order to have the orientation of 3d movement correct, we need to define
            an absolute North, South, East, and West. When the player presses the "D" key,
            they should rotate/face and move to the East. When the player presses "A" they should
            move to the West, "W" for the north, and "S" for the South

            The camera will determine this for us. Whichever face_direction the camera is facing, that
            will be defined as North. 

            Our camera will move based off of mouse input. We will rotate about a point, the point
            being the player, in the face_direction the user slides their mouse. 
        */

        /*
            There is a point in space that if we keep track of, we will know which way the camera
            is facing. 

            Or potentially use the value of the Empty game objects Y rotation value
        */



        if(Input.GetKey(KeyCode.W)){
            /*
                Depending on the value of the rotation, will determine in which face_direction
                we add force to the player.
            */
            direction_to_face = 0;
            updateDirection(direction_to_face);
            
            // rb.AddForce(0f, 0f, 20f);
            

            // cameraRotation = cameraPoint.transform.rotation.eulerAngles.y;
            // rotateTo = transform.rotation.eulerAngles;
            // rotateTo.y = cameraRotation;
            // transform.rotation = Quaternion.Euler(rotateTo);
        }
        if(Input.GetKey(KeyCode.S)){
            direction_to_face = 1;
            updateDirection(direction_to_face);
            // rb.AddForce(0f, 0f, -20f);
        }
        if(Input.GetKey(KeyCode.D)){
            direction_to_face = 2;
            updateDirection(direction_to_face);
            // rb.AddForce(-20f, 0f, 0f);
        }
        if(Input.GetKey(KeyCode.A)){
            direction_to_face = 3;
            updateDirection(direction_to_face);
            // rb.AddForce(20f, 0f, 0f);
        }
    }

    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(0f, 300f, 0f);
        }
        if(Input.GetKey(KeyCode.W)){
            cameraDirection = mainCamera.transform.forward;
            cameraDirection.y = 0; // Keep movement horizontal

            // Calculate the movement vector
            Vector3 movement = 5f * Time.deltaTime * cameraDirection;

            // Apply the movement to the Rigidbody
            rb.MovePosition(rb.position + movement);
        }
        if(Input.GetKey(KeyCode.S)){
            cameraDirection = -mainCamera.transform.forward;
            cameraDirection.y = 0; // Keep movement horizontal

            // Calculate the movement vector
            Vector3 movement = 5f * Time.deltaTime * cameraDirection;

            // Apply the movement to the Rigidbody
            rb.MovePosition(rb.position + movement);
        }
        if(Input.GetKey(KeyCode.D)){
            cameraDirection = mainCamera.transform.forward;
            cameraDirection.y = 0; // Keep movement horizontal

            // Calculate the movement vector
            Vector3 movement = 5f * Time.deltaTime * cameraDirection;

            // Apply the movement to the Rigidbody
            rb.MovePosition(rb.position + movement);
        }
        if(Input.GetKey(KeyCode.A)){
            cameraDirection = -mainCamera.transform.forward;
            cameraDirection.y = 0; // Keep movement horizontal

            // Calculate the movement vector
            Vector3 movement = 5f * Time.deltaTime * cameraDirection;

            // Apply the movement to the Rigidbody
            rb.MovePosition(rb.position + movement);
        }
    }



    private void updateDirection(int face_direction){
        // 0 = W, 
        // 1 = S, 
        // 2 = D, 
        // 3 = A
        switch(face_direction){
            case 0:
                cameraRotation = cameraPoint.transform.rotation.eulerAngles.y;
                rotateTo = transform.rotation.eulerAngles;
                rotateTo.y = cameraRotation;
                transform.rotation = Quaternion.Euler(rotateTo);
                break;
            case 1:
                cameraRotation = cameraPoint.transform.rotation.eulerAngles.y;
                rotateTo = transform.rotation.eulerAngles;
                rotateTo.y = cameraRotation + 180;
                transform.rotation = Quaternion.Euler(rotateTo);
                break;
            case 2:
                cameraRotation = cameraPoint.transform.rotation.eulerAngles.y;
                rotateTo = transform.rotation.eulerAngles;
                rotateTo.y = cameraRotation + 90;
                transform.rotation = Quaternion.Euler(rotateTo);
                break;
            case 3:
                cameraRotation = cameraPoint.transform.rotation.eulerAngles.y;
                rotateTo = transform.rotation.eulerAngles;
                rotateTo.y = cameraRotation - 90;
                transform.rotation = Quaternion.Euler(rotateTo);
                break;
        }
    }
}
