using UnityEngine;

public class SetCameraStart : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
    }

    private void FixedUpdate() {
        transform.position = player.transform.position;
    }
}
