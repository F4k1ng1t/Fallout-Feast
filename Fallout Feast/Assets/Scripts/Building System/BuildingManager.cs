using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UIElements;

public enum PlacementState
{
    None,
    Placing,
    Positioning
}

public class BuildingManager : MonoBehaviour
{
    public List<GameObject> structurePieceList = new List<GameObject>();
    public Dictionary<int, KeyCode> inputPiecePairs = new Dictionary<int, KeyCode>();
    GameObject currentPiece;
    private PlacementState currentPlacementState = PlacementState.None;
    private int currentPlacementIndex = 0;
    public float floorOffset = 0.5f;

    Vector3 locationBuffer = Vector3.zero;

    private void Start()
    {
        foreach(GameObject structurePiece in structurePieceList)
        {
            if (structurePiece.GetComponent<SelectableObject>() == null)
            {
                Debug.LogError("One of the structure pieces in the structurePieceList is null. Please check the SelectableObject script on " + gameObject.name);
            }
        }

        //Instantiate Input
        for (int i = 0; i < structurePieceList.Count; i++)
        {
            inputPiecePairs.Add(i, KeyCode.Alpha1 + i);
        }
    }
    private void Update()
    {
        
        switch(currentPlacementState)
        {
            case PlacementState.None:
                foreach(KeyValuePair<int, KeyCode> pair in inputPiecePairs)
                {
                    if (Input.GetKeyDown(pair.Value))
                    {
                        currentPlacementIndex = pair.Key;
                        currentPlacementState = PlacementState.Placing;
                    }
                }
                

                if (Input.GetKeyDown(KeyCode.F) && SelectableObject.currentlySelected != null)
                {
                    Destroy(SelectableObject.currentlySelected.gameObject);
                }
                break;
            case PlacementState.Placing:
                InstantiateStructurePiecePreview(structurePieceList[currentPlacementIndex]);
                currentPlacementState = PlacementState.Positioning;
                break;
            case PlacementState.Positioning:
                DetermineStructurePieceLocation(currentPiece);
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    RotatePiece(currentPiece, false);
                }
                if(Input.GetKeyDown(KeyCode.E))
                {
                    RotatePiece(currentPiece, true);
                }
                break;
        }
    }
    void InstantiateStructurePiecePreview(GameObject structurePiece)
    {
        currentPiece = Instantiate(structurePiece, Vector3.zero, Quaternion.identity);
    }
    void DetermineStructurePieceLocation(GameObject structurePiece)
    {
        //PlaceStructurePiece(structurePiece);
        structurePiece.transform.position = MousetoGround();
        if (Input.GetMouseButtonDown(0))
        {
            currentPlacementState = PlacementState.None;
        }
    }
    void RotatePiece(GameObject structurePiece, bool right)
    {
        if (right)
            structurePiece.transform.Rotate(0, 90, 0);
        else
            structurePiece.transform.Rotate(0, -90, 0);
    }
    Vector3 MousetoGround()
    {
        if (Input.mousePosition == null)
        {
            Debug.LogError("Error, cursor not found.");
            return new Vector3(0, floorOffset, 0);
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.GetComponent<SelectableObject>() == null && hit.collider.CompareTag("ground"))
        {
            Vector3 hitPointModified = hit.point;
            hitPointModified.y = floorOffset;
            
            hitPointModified = RoundToGrid(hitPointModified);
            locationBuffer = hitPointModified;
            return hitPointModified;
        }
        else if(hit.collider.GetComponent<SelectableObject>() != null)
        {

        }
            Debug.LogError("Raycast Failed!");
        
        return locationBuffer;
    }
    Vector3 RoundToGrid(Vector3 input)
    {
        Vector3 output = new Vector3(((int)(input.x / 10) * 10), input.y, ((int)(input.z / 10) * 10));
        Debug.Log(output);
        return output;
    }
}
