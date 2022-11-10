using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScript : MonoBehaviour
{
    public Text resolutionText;

    private int width;
    private int height;

    private int newResolution;

    public void NextResolution()
    {
        newResolution++;
        Resolutions();
    }

    public void BackResolutions()
    {
        newResolution--;
        Resolutions();
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Aplicar()
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    private void Resolutions()
    {
        newResolution = Mathf.Clamp(newResolution, 0, 3);
        switch (newResolution)
        {
            case 0://1024 x 576
                width = 1024;
                height = 576;
                break;
            case 1://1280 x 720
                width = 1280;
                height = 720;
                break;
            case 2://1366 x 768
                width = 1366;
                height = 768;
                break;
            case 3://1920 x 1080
                width = 1920;
                height = 1080;
                break;
        }
        resolutionText.text = width.ToString() + " x " + height.ToString();
    }
}