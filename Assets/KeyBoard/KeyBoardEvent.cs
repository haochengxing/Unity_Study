using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScreenKeyboard = UnityEngine.TouchScreenKeyboard;

public class KeyBoardEvent : MonoBehaviour
{
    public Text text;
    public InputField input;
    public Button button;

    /// <summary>
    /// 是否打开键盘
    /// </summary>
    public bool focus => input.isFocused;


    /// <summary>
    /// 缓存安卓的高度
    /// </summary>
    private static int height = -1;


    // Start is called before the first frame update
    void Start()
    {


        button.onClick.AddListener(onClick);

        input.shouldHideMobileInput = true;

        input.onEndEdit.AddListener(OnReturn);
    }

    // Update is called once per frame
    void Update()
    {
        if (focus)
        {
            int keyboardHeight = GetKeyboardHeight();

            Debug.Log(keyboardHeight);

            text.text = "keyboardHeight : " + keyboardHeight;

            if (keyboardHeight >= Screen.height * 0.9)
            {
                return;
            }


            int h = keyboardHeight * Screen.currentResolution.height / Screen.height;

            Debug.Log(h);

            text.text = "keyboardHeight : " + keyboardHeight+" h : "+h;
        }

        if (input.touchScreenKeyboard!=null && input.touchScreenKeyboard.status== TouchScreenKeyboard.Status.Done)
        {
            onClick();
        }
    }

    void onClick()
    {
        text.text = input.text;
    }

    void OnReturn(string msg)
    {
        onClick();
    }

#if UNITY_EDITOR
    public static int GetKeyboardHeight()
    {
        return 0;
    }
#elif UNITY_ANDROID
    // 获取手机键盘高度
    public static int GetKeyboardHeight()
    {
        using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var unityPlayer = unityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
            var view = unityPlayer.Call<AndroidJavaObject>("getView");
            var dialog = unityPlayer.Get<AndroidJavaObject>("mSoftInputDialog");

            if (view == null || dialog == null)
                return 0;

            var decorHeight = 0;

            if (true) //includeInput
            {
                var decorView = dialog.Call<AndroidJavaObject>("getWindow").Call<AndroidJavaObject>("getDecorView");

                if (decorView != null)
                    decorHeight = decorView.Call<int>("getHeight");
            }

            using (var rect = new AndroidJavaObject("android.graphics.Rect"))
            {
                view.Call("getWindowVisibleDisplayFrame", rect);

                var get_height = rect.Call<int>("height");
                if (get_height < Display.main.systemHeight)
                {
                    height = get_height;
                }

                return Display.main.systemHeight - height + decorHeight;
            }
        }
    }
#elif UNITY_IOS
    // 获取手机键盘高度
    public static int GetKeyboardHeight()
    {
        return (int)TouchScreenKeyboard.area.height * Display.main.systemHeight / Screen.height;
    }
#else
    public static int GetKeyboardHeight()
    {
        return 0;
    }
#endif
}
