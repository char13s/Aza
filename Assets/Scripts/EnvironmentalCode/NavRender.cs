using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class NavRender : MonoBehaviour
{
    [SerializeField] private Material material;
    // Start is called before the first frame update
    void Start()
    {
        ShowNavi();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ShowNavi()
    {

        NavMeshTriangulation nav = NavMesh.CalculateTriangulation();
        Mesh mesh = new Mesh();
        mesh.vertices = nav.vertices;
        mesh.triangles = nav.indices;
        mesh.RecalculateNormals();
        
        GetComponent<MeshRenderer>().material = material;
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
