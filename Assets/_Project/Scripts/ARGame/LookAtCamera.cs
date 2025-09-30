using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // Make the canvas face the camera
        transform.LookAt(transform.position + cam.forward);
    }
}
