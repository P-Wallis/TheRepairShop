using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHeight : MonoBehaviour
{

    public RectTransform InOut;
    float quarterScreenHeight;
    
    void Start()
    {
        quarterScreenHeight = Screen.height / 4;
        Debug.Log("Screen Height : " + Screen.height / 4);
        InOut.offsetMin = new Vector2(0, quarterScreenHeight);
    }

    void Update()
    {
        
    }
}
