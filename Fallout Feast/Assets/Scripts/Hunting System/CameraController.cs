using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Look Sensitivity")]
    public float sensX;
    public float sensY;
    [Header("Camera Clamping")]
    public float minY;
    public float maxY;

    float rotX = 0f;
    float rotY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // get the mouse movement inputs
        rotX += Input.GetAxis("Mouse X") * sensX;
        rotY += Input.GetAxis("Mouse Y") * sensY;
        // clamp the vertical rotation
        rotY = Mathf.Clamp(rotY, minY, maxY);
        // rotate the camera vertically
        transform.localRotation = Quaternion.Euler(-rotY, 0, 0);
        // rotate the player horizontally
        transform.parent.rotation = Quaternion.Euler(transform.rotation.x, rotX, 0);
    }
}
