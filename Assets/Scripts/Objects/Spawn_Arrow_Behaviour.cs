using UnityEngine;

public class Spawn_Arrow_Behaviour : MonoBehaviour
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
