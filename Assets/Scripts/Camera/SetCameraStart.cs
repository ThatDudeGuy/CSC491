using UnityEngine;

public class SetCameraStart : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
    }

    private void FixedUpdate() {
        // need a Y threshold. So if the player Y is about 2-3 units higher than the camera, follow the player
        // otherwise, don't move
        if(player.transform.position.y > transform.position.y){}
        transform.position = player.transform.position;
    }
}
