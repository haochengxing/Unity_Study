using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace quickshadow
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class QuickShadowLauncher : MonoBehaviour
    {
        public Shader shader;
        Camera mCamera;
        void Awake()
        {
            mCamera = this.GetComponent<Camera>();
            mCamera.SetReplacementShader(shader, "");

            Shader.SetGlobalTexture("_ShadowTexture", mCamera.targetTexture);
        }

        void Update()
        {
            Shader.SetGlobalMatrix("_ShadowLauncherMatrix", transform.worldToLocalMatrix);
            Shader.SetGlobalVector("_ShadowLauncherParam", new Vector4(mCamera.orthographicSize,mCamera.nearClipPlane,mCamera.farClipPlane));
        }
    }
}