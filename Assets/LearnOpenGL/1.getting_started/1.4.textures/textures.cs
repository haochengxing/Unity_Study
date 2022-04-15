using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class textures : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> vertices = new List<Vector3>() {
        new Vector3(0.5f,0.5f,0.0f),
        new Vector3(0.5f,-0.5f,0.0f),
        new Vector3(-0.5f,-0.5f,0.0f),
        new Vector3(-0.5f,0.5f,0.0f),
        };

        List<Vector2> uvs = new List<Vector2>(){
        new Vector2(1f,1f),
        new Vector2(1f,0f),
        new Vector2(0f,0f),
        new Vector2(0f,1f),
        };

        int[] indices = {0,1,3,
        1,2,3};

        GameObject go = new GameObject("textures");
        Mesh mesh = new Mesh();
        Shader shader = Shader.Find("Unlit/textures");
        Material material = new Material(shader);

        int _MainTex = Shader.PropertyToID("_MainTex");

        Texture texture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/LearnOpenGL/1.getting_started/1.4.textures/wall.jpg");

        material.SetTexture(_MainTex, texture);

        mesh.SetVertices(vertices);
        mesh.SetUVs(0, uvs);
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);

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
