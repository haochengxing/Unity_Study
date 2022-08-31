using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class BuildEditor
{

    private static string GameName = PlayerSettings.productName + "_" + DateTime.Now.ToString("yyyyMMddHHmm");

    private static string OutPutPath = System.IO.Directory.GetCurrentDirectory() + "/BuildTools/OutPut/";
    private static string ApkOutputPath = System.IO.Directory.GetCurrentDirectory() + "/../XLua_{0}/XLua_{1}.apk";
    private static string ApkPath = string.Format("{0}{1}.apk", OutPutPath, GameName);
    private static string AndroidProjectPath = string.Format("{0}{1}_AndroidProject", OutPutPath, GameName);


    [MenuItem("Tools/打包/打包APK")]
    public static void BuildAPK()
    {
        string buildVersion = "1.0.0";
        int versionCode = 1;
        bool isDevelopment = false;
        int BUILD_NUMBER = 0;

        Debug.Log("------------- 接收命令行参数 -------------");
        List<string> commondList = new List<string>();
        foreach (string arg in System.Environment.GetCommandLineArgs())
        {
            Debug.Log("命令行传递过来参数：" + arg);
            commondList.Add(arg);
        }
        try
        {
            Debug.Log("命令行传递过来参数数量：" + commondList.Count);
            buildVersion = commondList[commondList.Count - 4];
            versionCode = int.Parse(commondList[commondList.Count - 3]);
            isDevelopment = bool.Parse(commondList[commondList.Count - 2]);
            BUILD_NUMBER = int.Parse(commondList[commondList.Count - 1]);
        }
        catch (Exception)
        {

        }

        Debug.Log("------------- 更新资源 -------------");
        //OneKeyRefreshSource.Create();

        Debug.Log("------------- 开始 BuildAPK -------------");
        PlayerSettings.bundleVersion = buildVersion;
        PlayerSettings.Android.bundleVersionCode = versionCode;
        BuildOptions buildOption = BuildOptions.None;
        if (isDevelopment)
            buildOption |= BuildOptions.Development;
        else
            buildOption &= BuildOptions.Development;
        //PlayerSettings.Android.keystorePass = "00000000";       // 密钥密码
        //PlayerSettings.Android.keyaliasName = "test.keystore";    // 密钥别名
        //PlayerSettings.Android.keyaliasPass = "00000000";
        ApkPath = string.Format(ApkOutputPath, BUILD_NUMBER, BUILD_NUMBER);

        string path = Path.GetDirectoryName(ApkPath);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, ApkPath, BuildTarget.Android, buildOption);
        Debug.Log("------------- 结束 BuildAPK -------------");
        Debug.Log("Build目录：" + ApkPath);
        //Application.OpenURL(OutPutPath);
    }
}