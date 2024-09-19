using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Vector2 turn;
    public float x_sensitivity = 1f, y_sensitivity = .5f;
    public Vector3 deltaMove;
    public float speed = 1;
    public GameObject point;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // point = GetComponentInParent<Transform>().gameObject;
    }
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * x_sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * y_sensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        point.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        
    }

}
