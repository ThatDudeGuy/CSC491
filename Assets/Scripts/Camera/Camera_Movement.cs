using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Vector2 turn;
    public float x_sensitivity = 1f, y_sensitivity = .5f;
    public GameObject point;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * x_sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * y_sensitivity;
        // if(point.transform.localRotation.x * 100 < -14.8f){// || point.transform.rotation.eulerAngles.x > 24.5f) {
        //     print(point.transform.localRotation.x * 100);
        // }
        point.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        
    }

}
