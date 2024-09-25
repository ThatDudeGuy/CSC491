using UnityEngine;

public class arrowBehaviour : MonoBehaviour
{
    public GameObject cameraPoint;
    void Start()
    {
        cameraPoint = GameObject.FindGameObjectWithTag("CameraPoint");
    }

    private void FixedUpdate() {
        transform.rotation = cameraPoint.transform.rotation;
    }
}
