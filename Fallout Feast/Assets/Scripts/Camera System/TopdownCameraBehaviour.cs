using NUnit.Framework;
using UnityEngine;

public class TopdownCameraBehaviour : MonoBehaviour
{
    public Transform cameraOrigin;
    public float cameraSensitivity = 1.0f;
    public float scrollSensitivity = 1.0f;
    private Vector3 _lastMousePosition;
    private bool _isDragging = false;
    private Camera _camera;
    private float cameraY;

    public void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    public void Update()
    {
        HandleCameraDrag();
        HandleZoom();
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
            Vector3 cameraPosition = this.transform.position;
            Vector3 mouseDelta = Input.mousePosition - _lastMousePosition;
            Vector3 move = new Vector3(-mouseDelta.x, 0 , -mouseDelta.y) * DynamicCameraSensitivity();
            
            cameraPosition += move;
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, -100,100);
            cameraPosition.z = Mathf.Clamp(cameraPosition.z, -100,100);
            this.transform.position = cameraPosition;
            _lastMousePosition = Input.mousePosition;
        }
    }
    private float DynamicCameraSensitivity()
    {
        float dynamicCameraSensitivity = 0.01f + (cameraSensitivity * (cameraY / 100f));
        Debug.Log(dynamicCameraSensitivity);
        return dynamicCameraSensitivity;
    }
    private void HandleZoom()
    {
        Vector3 cameraPosition = this.transform.position;
        cameraPosition.y += -Input.mouseScrollDelta.y * scrollSensitivity;
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, 10f, 100f);
        cameraY = cameraPosition.y;
        this.transform.position = cameraPosition;
    }

}
