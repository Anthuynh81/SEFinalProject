using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public TextMeshProUGUI output;

    public void fullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void resolution(int val)
    {
        if (Screen.fullScreen == false)
        {
            switch (val)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, false);
                    break;
                case 1:
                    Screen.SetResolution(1280, 720, false);
                    break;
                case 2:
                    Screen.SetResolution(1024, 576, false);
                    break;
                case 3:
                    Screen.SetResolution(768, 432, false);
                    break;
            }
        }

    }
}