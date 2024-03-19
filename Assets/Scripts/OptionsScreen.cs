using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsScreen : MonoBehaviour
{
    public Toggle fullscreenTog;
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyGraphics()
    {
        Screen.fullScreen = fullscreenTog.isOn;
    }
}
