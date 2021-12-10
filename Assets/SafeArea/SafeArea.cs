using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var safeArea = Screen.safeArea;
        float safeMarginLeft = safeArea.x / Screen.width;
        float safeMarginRight = (Screen.width-safeArea.xMax) / Screen.width;
        float safeMarginBottom = safeArea.y / Screen.height;
        float safeMarginTop = (Screen.height - safeArea.yMax) / Screen.height;

        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(safeMarginLeft, safeMarginBottom);
        rectTransform.anchorMax = new Vector2(1-safeMarginRight, 1-safeMarginTop);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
