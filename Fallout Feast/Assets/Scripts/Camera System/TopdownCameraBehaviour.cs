using NUnit.Framework;
using UnityEngine;

public class TopdownCameraBehaviour : MonoBehaviour
{
    public Vector3 boundingBoxParam1;
    public Vector3 boundingBoxParam2;
    public Vector3 defaultCameraOrigin;
    public float cameraSensitivity = 1.0f;
    private Vector3 _lastMousePosition;
    private bool _isDragging = false;
    private Camera _camera;

    public void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    public void Update()
    {
        HandleCameraDrag();
    }
    private void HandleCameraDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _lastMousePosition = Input.mousePosition;
            _isDragging = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - _lastMousePosition;
            Vector3 move = new Vector3(-mouseDelta.y, 0, mouseDelta.x) * cameraSensitivity; // Adjust sensitivity as needed

            transform.position += move;
            _lastMousePosition = Input.mousePosition;
        }
    }
    public void MoveCamera()
    {

    }
}
