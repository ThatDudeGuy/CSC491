using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationMatchTest : MonoBehaviour
{
    GameObject myCube;
    float rot;
    Vector3 newRot;

    // Start is called before the first frame update
    void Start()
    {
        myCube = GameObject.FindGameObjectWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            rot = myCube.transform.rotation.eulerAngles.y;
            newRot = transform.rotation.eulerAngles;
            newRot.y = rot;
            transform.rotation = Quaternion.Euler(newRot);
        }
    }
}
