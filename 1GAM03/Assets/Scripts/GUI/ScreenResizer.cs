using UnityEngine;
using System.Collections;

public class ScreenResizer : MonoBehaviour {
    private bool isFullScreen;

    void Start()
    {
        isFullScreen = Screen.fullScreen;
    }

    void OnGUI()
    {
        if (isFullScreen != Screen.fullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
            isFullScreen = Screen.fullScreen;
        }
    }
}
