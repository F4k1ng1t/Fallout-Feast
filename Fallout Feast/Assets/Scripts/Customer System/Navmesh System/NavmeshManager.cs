using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavmeshManager : MonoBehaviour
{
    NavMeshSurface surface;

    public void Start()
    {
        surface = this.GetComponent<NavMeshSurface>();
    }
    IEnumerator Generate()
    {
        surface.BuildNavMesh();
        yield return null;
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Generate Navmesh"))
        {
            StartCoroutine(Generate());
        }
    }
}
