using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Options : MonoBehaviour
{
    #region Variables
    public AudioMixer audioMixer; // Music Audio Mixer
    public TMP_Dropdown resolutionDropdown; // Dropdown To Select Game Resolution
    public TMP_Dropdown graphicsDropdown; // Dropdown To Select Between Fancy And Fast Graphics
    public Toggle FullscreenToggle; // Toggle To Select Fullscreen Or Windowed Game
    public Slider VolumeSilder; // Audio Volume Selector

    private Resolution[] screenResolutions; // Array Containing All Resolutions
    private Resolution selectedResolution; // Variable Containing Selected Resolution
    #endregion

    #region On Start
    private void Start()
    {
        screenResolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray(); // Fill Array screenResolutions With All Available Monitor Resolutions Only Unique
        PrepareResolutionDropdown();
        SetSavedOptions();
        PrepareGraphicsDropdown();
    }
    #endregion

    #region Preparation Functions
    public void PrepareResolutionDropdown()
    {
        resolutionDropdown.ClearOptions(); // Empty Dropdown Values

        List<string> resolutionsList = new List<string>(); // Create resolutionsList List For resolutionDropdown

        int currentResolutionIndex = 0;
        for (int i = 0; i < screenResolutions.Length; i++)
        {
            string res = screenResolutions[i].width + " x " + screenResolutions[i].height;

            if (!resolutionsList.Contains(res))
                resolutionsList.Add(res);

            // Get Current Resolution Index
            if (screenResolutions[i].width == Screen.width &&
                screenResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i; // Variable That Has Index Of Current Resolution
            }
        }

        resolutionDropdown.AddOptions(resolutionsList); // Add Resolutions List To Dropdown
        resolutionDropdown.value = currentResolutionIndex; // Select Current Resolution In Dropdown
        resolutionDropdown.RefreshShownValue(); // Refresh Dropdown Shown Value
    }
    public void PrepareGraphicsDropdown()
    {
        graphicsDropdown.ClearOptions(); // Empty Dropdown Values

        string[] qualitySettings = QualitySettings.names;
        List<string> graphicsList = new List<string>(); // Create graphicsList List For graphicsDropdown

        int qualityIndex = 0;
        for (int i = 0; i < qualitySettings.Length; i++)
        {
            string settingsName = LocalisationSystem.GetLocalisedValue(qualitySettings[i]);
            if (!graphicsList.Contains(settingsName))
                graphicsList.Add(settingsName);
        }
        graphicsDropdown.AddOptions(graphicsList); // Add Graphics List To Dropdown
        graphicsDropdown.value = PlayerPrefs.GetInt("qualityIndex", qualityIndex); // Select Current Graphic In Dropdown
        graphicsDropdown.RefreshShownValue(); // Refresh Dropdown Shown Value
    }
    public void SetSavedOptions()
    {
        int qualityIndex = int.MinValue; // Variable Containing Selected Quality Index
        int isFullscreen = int.MinValue; // Variable Containing Selected Game Mode Fullscreen Or Windowed
        int resolutionIndex = int.MinValue; // Variable Containing Selected Game Resolution

        //Volume
        VolumeSilder.value = PlayerPrefs.GetFloat("volume", 10f);

        //Graphics
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityIndex", qualityIndex));
        graphicsDropdown.value = PlayerPrefs.GetInt("qualityIndex", qualityIndex);
        graphicsDropdown.RefreshShownValue(); // Refresh Dropdown Shown Value

        //Fullscreen
        isFullscreen = PlayerPrefs.GetInt("isFullscreen", isFullscreen);
        if (isFullscreen == 1)
        {
            Screen.fullScreen = true;
            FullscreenToggle.isOn = true;
        }
        else
        {
            Screen.fullScreen = false;
            FullscreenToggle.isOn = false;
        }

        //Resolution
        resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", resolutionIndex);
        resolutionDropdown.RefreshShownValue();
        Resolution selectedResolution = screenResolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
    #endregion

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

        // If Set Fullscreen = True, Then Save 1, Else Save 0
        if (isFullscreen)
        {
            PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else
            PlayerPrefs.SetInt("isFullscreen", 0);
    }
    /// <summary>
    /// Set Game selectedResolution
    /// </summary>
    /// <param name="resIndex"> int param declaring which selectedResolution to set</param>
    public void SetResolution(int resIndex)
    {
        if (screenResolutions != null)
        {
            // Get selectedResolution from resolutions list
            selectedResolution = screenResolutions[resIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
            PlayerPrefs.SetInt("resolutionIndex", resIndex);
        }

    }
    #endregion
}
