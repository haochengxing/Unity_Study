using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class coordinate_systems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Vector3[] v3s =
        {
            //8个不同的顶点
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,0,1),
            new Vector3(0,0,1),
            new Vector3(0,1,0),
            new Vector3(1,1,0),
            new Vector3(1,1,1),
            new Vector3(0,1,1)
        };

        List<Vector3> v3ss = new List<Vector3>() {
            //需要将8个不同的顶点排序成24个顶点，总共6个面：每个面都要顺时针排，
            v3s[0], v3s[1], v3s[2], v3s[3],
            v3s[4], v3s[7], v3s[6], v3s[5],
            v3s[0], v3s[4], v3s[5], v3s[1],
            v3s[1], v3s[5], v3s[6], v3s[2],
            v3s[2], v3s[6], v3s[7], v3s[3],
            v3s[3], v3s[7], v3s[4], v3s[0],
        };

        int[] iss =
        {
          //12个三角形序列
          0,1,2,
          0,2,3,
          4,5,6,
          4,6,7,
          8,9,10,
          8,10,11,
          12,13,14,
          12,14,15,
          16,17,18,
          16,18,19,
          20,21,22,
          20,22,23
        };

        Vector3[] normals =
        {
            //6种法线，要和顶点信息一一对应
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,

            Vector3.down,
            Vector3.down,
            Vector3.down,
            Vector3.down,

            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,

            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.right,

            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,

            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.left,

        };

        List<Vector2> uvs = new List<Vector2>(){
            //每个面获取不同的贴图，24个uv顶点信息，下面的坐标都是在uv上计算的
            new Vector2(0,1),
            new Vector2(0.4f,1),
            new Vector2(0.4f,0.6f),
            new Vector2(0,0.6f),

            new Vector2(0.2f,1),
            new Vector2(0.6f,1),
            new Vector2(0.6f,0.6f),
            new Vector2(0.2f,0.6f),

            new Vector2(0.4f,1),
            new Vector2(0.8f,1),
            new Vector2(0.8f,0.6f),
            new Vector2(0.4f,0.6f),


            new Vector2(0.6f,1),
            new Vector2(1,1),
            new Vector2(1f,0.6f),
            new Vector2(0.6f,0.6f),

            new Vector2(0,0.6f),
            new Vector2(0.4f,0.6f),
            new Vector2(0.4f,0.2f),
            new Vector2(0,0.2f),

            new Vector2(0.2f,0.6f),
            new Vector2(0.6f,0.6f),
            new Vector2(0.6f,0.2f),
            new Vector2(0.2f,0.2f)
        };

        

        GameObject go = new GameObject("coordinate_systems");
        Mesh mesh = new Mesh();
        Shader shader = Shader.Find("Unlit/coordinate_systems");
        Material material = new Material(shader);

        int _MainTex = Shader.PropertyToID("_MainTex");

        Texture texture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/LearnOpenGL/1.getting_started/1.6.coordinate_systems/wall.jpg");

        material.SetTexture(_MainTex, texture);

        mesh.SetVertices(v3ss);
        mesh.SetTriangles(iss,0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        

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
