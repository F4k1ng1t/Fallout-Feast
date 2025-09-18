using NUnit.Framework;
using UnityEngine;

public class TopdownCameraBehaviour : MonoBehaviour
{
    public Transform cameraOrigin;
    public float cameraSensitivity = 1.0f;
    public float scrollSensitivity = 1.0f;
    private Vector3 _lastMousePosition;
    private bool _isDragging = false;

    [Header("Camera Clamps")]
    [SerializeField] float xClampMin = -100f;
    [SerializeField] float xClampMax = 100f;
    [SerializeField] float zClampMin = -100f;
    [SerializeField] float zClampMax = 100f;
    [SerializeField] float yClampMin = 10f;
    [SerializeField] float yClampMax = 100f;


    private float cameraY;

    public void Start()
    {

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
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, xClampMin, xClampMax);
            cameraPosition.z = Mathf.Clamp(cameraPosition.z, zClampMin, zClampMax);
            this.transform.position = cameraPosition;
            _lastMousePosition = Input.mousePosition;
        }
    }
    private float DynamicCameraSensitivity()
    {
        return 0.01f + (cameraSensitivity * (cameraY / 100f));
    }
    private void HandleZoom()
    {
        Vector3 cameraPosition = this.transform.position;
        cameraPosition.y += -Input.mouseScrollDelta.y * scrollSensitivity;
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, yClampMin, yClampMax);
        cameraY = cameraPosition.y;
        this.transform.position = cameraPosition;
    }

}
