using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabLightmapData : MonoBehaviour
{
    [System.Serializable]
    struct RendererInfo
    {
        public Renderer renderer;
        public int lightmapIndex;
        public Vector4 lightmapOffsetScale;
    }

    [SerializeField]
    RendererInfo[] m_RendererInfo;
    [SerializeField]
    Texture2D[] m_Lightmaps;

    void Awake()
    {
        if (m_RendererInfo == null || m_RendererInfo.Length == 0)
            return;

        /*var lightmaps = LightmapSettings.lightmaps;
        var combinedLightmaps = new LightmapData[lightmaps.Length + m_Lightmaps.Length];

        lightmaps.CopyTo(combinedLightmaps, 0);
        for (int i = 0; i < m_Lightmaps.Length; i++)
        {
            combinedLightmaps[i + lightmaps.Length] = new LightmapData();
            combinedLightmaps[i + lightmaps.Length].lightmapColor = m_Lightmaps[i];
        }

        ApplyRendererInfo(m_RendererInfo, lightmaps.Length);
        LightmapSettings.lightmaps = combinedLightmaps;*/

        var lightmaps = new LightmapData[m_Lightmaps.Length];
        for (int i = 0; i < m_Lightmaps.Length; i++)
        {
            lightmaps[i] = new LightmapData();
            lightmaps[i].lightmapColor = m_Lightmaps[i];
        }

        ApplyRendererInfo(m_RendererInfo, 0);
        LightmapSettings.lightmaps = lightmaps;
    }


    static void ApplyRendererInfo(RendererInfo[] infos, int lightmapOffsetIndex)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            var info = infos[i];
            info.renderer.lightmapIndex = info.lightmapIndex + lightmapOffsetIndex;
            info.renderer.lightmapScaleOffset = info.lightmapOffsetScale;
        }
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Assets/Bake Prefab Lightmaps")]
    static void GenerateLightmapInfo()
    {
        if (UnityEditor.Lightmapping.giWorkflowMode != UnityEditor.Lightmapping.GIWorkflowMode.OnDemand)
        {
            Debug.LogError("ExtractLightmapData requires that you have baked you lightmaps and Auto mode is disabled.");
            return;
        }
        UnityEditor.Lightmapping.Bake();

        PrefabLightmapData[] prefabs = FindObjectsOfType<PrefabLightmapData>();

        foreach (var instance in prefabs)
        {
            var gameObject = instance.gameObject;
            var rendererInfos = new List<RendererInfo>();
            var lightmaps = new List<Texture2D>();

            GenerateLightmapInfo(gameObject, rendererInfos, lightmaps);

            instance.m_RendererInfo = rendererInfos.ToArray();
            instance.m_Lightmaps = lightmaps.ToArray();

            var targetPrefab = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(instance.gameObject) as GameObject;
            if (targetPrefab != null)
            {
                GameObject root = UnityEditor.PrefabUtility.GetOutermostPrefabInstanceRoot(instance.gameObject);

                if (root != null)
                {
                    GameObject rootPrefab = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(instance.gameObject);
                    string rootPath = UnityEditor.AssetDatabase.GetAssetPath(rootPrefab);
                    UnityEditor.PrefabUtility.UnpackPrefabInstanceAndReturnNewOutermostRoots(root, UnityEditor.PrefabUnpackMode.OutermostRoot);
                    try
                    {
                        UnityEditor.PrefabUtility.ApplyPrefabInstance(instance.gameObject, UnityEditor.InteractionMode.AutomatedAction);
                    }
                    catch { }
                    finally
                    {
                        UnityEditor.PrefabUtility.SaveAsPrefabAssetAndConnect(root, rootPath, UnityEditor.InteractionMode.AutomatedAction);
                    }
                }
                else
                {
                    UnityEditor.PrefabUtility.ApplyPrefabInstance(instance.gameObject, UnityEditor.InteractionMode.AutomatedAction);
                }
            }
        }
    }

    static void GenerateLightmapInfo(GameObject root, List<RendererInfo> rendererInfos, List<Texture2D> lightmaps)
    {
        var renderers = root.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.lightmapIndex != -1)
            {
                RendererInfo info = new RendererInfo();
                info.renderer = renderer;
                info.lightmapOffsetScale = renderer.lightmapScaleOffset;

                Texture2D lightmap = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapColor;

                info.lightmapIndex = lightmaps.IndexOf(lightmap);
                if (info.lightmapIndex == -1)
                {
                    info.lightmapIndex = lightmaps.Count;
                    lightmaps.Add(lightmap);
                }

                rendererInfos.Add(info);
            }
        }
    }
#endif

}