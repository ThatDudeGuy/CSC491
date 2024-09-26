using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
