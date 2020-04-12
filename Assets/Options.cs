using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;
    public Toggle FullscreenToggle;
    public Slider VolumeSilder;

    Resolution[] resolutions;

    public int qualityIndex; public int isFullscreen; public int resolutionIndex;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if (!options.Contains(option))
                options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        #region Get And Set Previously Entered Options

        //Volume
        VolumeSilder.value = PlayerPrefs.GetFloat("volume", 10f);

        //Graphics
        graphicsDropdown.value = PlayerPrefs.GetInt("qualityIndex", qualityIndex);

        //Resolution
        resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", resolutionIndex);
        resolutionDropdown.RefreshShownValue();
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        //Fullscreen
        isFullscreen = PlayerPrefs.GetInt("isFullscreen", isFullscreen);
        if (isFullscreen == 1)
        {
            FullscreenToggle.isOn = true;
        }
        else
        {
            FullscreenToggle.isOn = false;
        }

        #endregion
    }

    #region Settings Get Set Functions
    /// <summary>
    /// Set Game Volume
    /// </summary>
    /// <param name="volume">Game volume</param>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    /// <summary>
    /// Set Game Quality between High, Low, Medium, etc
    /// </summary>
    /// <param name="qualityIndex">int param declaring selected quality</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }
    /// <summary>
    /// Set Game Fullscreen or windowed
    /// </summary>
    /// <param name="isFullscreen">bool if fullscreen or not</param>
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        // if fullscreen 1, else 0
        if (isFullscreen)
        {
            PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isFullscreen", 0);
        }
    }
    /// <summary>
    /// Set Game resolution
    /// </summary>
    /// <param name="resolutionIndex"> int param declaring which resolution to set</param>
    public void SetResolution(int resolutionIndex)
    {
        // Get resolution from resolutions list
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }
    #endregion
}
