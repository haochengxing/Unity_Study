using System.IO;
using UnityEditor;
using UnityEngine;

public class MyEditorLut : EditorWindow
{
    [MenuItem("MyTools/GeneraterLut")]
    static void AddWindow()
    {
        Rect wr = new Rect(0, 0, 300, 300);
        MyEditorLut window = (MyEditorLut)EditorWindow.GetWindowWithRect(typeof(MyEditorLut), wr, true, "GeneraterLut");
        window.Show();
    }

    public Shader lutShader;
    private Texture texture;

    private void OnGUI()
    {
        lutShader = EditorGUILayout.ObjectField("Shader", lutShader, typeof(Shader), true) as Shader;
        if (GUILayout.Button("生成LUT贴图", GUILayout.Width(290)))
        {
            if (lutShader == null)
            {
                this.ShowNotification(new GUIContent("Shader 不能为空!"));
            }
            else
            {
                Export();
            }
        }
        if (texture != null)
        {
            Rect rect = new Rect(10, 40, texture.width * 4, texture.height * 4);
            GUI.DrawTexture(rect, texture);
        }
    }

    void Export()
    {
        Material mat = new Material(lutShader);
        RenderTexture rt = RenderTexture.GetTemporary(64, 64, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(null, rt, mat);

        Texture2D lutTex = new Texture2D(64, 64, TextureFormat.RGBAFloat, true, true);
        Graphics.SetRenderTarget(rt);
        lutTex.ReadPixels(new Rect(0, 0, 64, 64), 0, 0);
        lutTex.Apply();
        texture = lutTex;

        byte[] bytes = lutTex.EncodeToEXR(Texture2D.EXRFlags.OutputAsFloat);
        File.WriteAllBytes(Application.dataPath + "/LearnOpenGL/6.pbr/ut.exr", bytes);

        RenderTexture.ReleaseTemporary(rt);
        Graphics.SetRenderTarget(null);
        AssetDatabase.Refresh();
    }
}

