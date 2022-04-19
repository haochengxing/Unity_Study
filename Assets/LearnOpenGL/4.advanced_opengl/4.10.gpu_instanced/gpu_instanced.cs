using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpu_instanced : MonoBehaviour
{

    public GameObject template;
    private List<GameObject> list = new List<GameObject>();

    void Start()
    {
        CreateGoList();
    }

    private void CreateGoList()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject go = Instantiate(template);
            go.transform.localPosition = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));
            list.Add(go);
        }
    }

    private void DestoryGoList()
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject.Destroy(list[i]);
        }
        list.Clear();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 150, 120, 40), "ByMaterial"))
        {
            for (int i = 0; i < list.Count; i++)
            {
                Material mat = list[i].GetComponentInChildren<Renderer>().material;
                mat.SetColor("_Diffuse", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            }
        }
        else if (GUI.Button(new Rect(10, 250, 120, 40), "ByPropertyBlock"))
        {
            for (int i = 0; i < list.Count; i++)
            {
                MaterialPropertyBlock block = new MaterialPropertyBlock();
                list[i].GetComponentInChildren<Renderer>().GetPropertyBlock(block);
                block.SetColor("_Diffuse", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                list[i].GetComponentInChildren<Renderer>().SetPropertyBlock(block);
            }
        }
        else if (GUI.Button(new Rect(10, 350, 120, 40), "Reset"))
        {
            DestoryGoList();
            CreateGoList();
        }
    }

}

