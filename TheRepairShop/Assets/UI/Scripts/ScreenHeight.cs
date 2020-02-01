using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHeight : MonoBehaviour
{

    
    void Start()
    {
        //Script here will determine the height of the window by pixels and position the popup in the lower third or quarter.
        Debug.Log("Screen Height : " + Screen.height / 4);
    }

    void Update()
    {
        
    }
}
