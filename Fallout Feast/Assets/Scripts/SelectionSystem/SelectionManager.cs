using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit) || hit.collider.GetComponent<SelectableObject>() == null)
            {
                SelectableObject.Deselect();
            }
        }
    }
}
