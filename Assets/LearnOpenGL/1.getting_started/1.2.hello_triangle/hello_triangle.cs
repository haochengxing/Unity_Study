using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello_triangle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*float[] vertices = {0.5f,0.5f,0.0f,
        0.5f,-0.5f,0.0f,
        -0.5f,-0.5f,0.0f,
        -0.5f,0.5f,0.0f};*/

        List<Vector3> vertices = new List<Vector3>() { 
        new Vector3(0.5f,0.5f,0.0f),
        new Vector3(0.5f,-0.5f,0.0f),
        new Vector3(-0.5f,-0.5f,0.0f),
        new Vector3(-0.5f,0.5f,0.0f),
        };

        int[] indices = {0,1,3,
        1,2,3};

        GameObject go = new GameObject("hello_triangle");
        Mesh mesh = new Mesh();
        Shader shader = Shader.Find("Unlit/hello_triangle");
        Material material = new Material(shader);

        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, MeshTopology.Triangles,0);

        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;

        MeshFilter filter = go.AddComponent<MeshFilter>();
        filter.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
