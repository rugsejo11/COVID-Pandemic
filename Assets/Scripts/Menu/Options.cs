using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Options : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioMixer audioMixer = null; // Audio Mixer
    [SerializeField] private TMP_Dropdown resolutionDropdown = null; // Dropdown To Select Game Resolution
    [SerializeField] private TMP_Dropdown graphicsDropdown = null; // Dropdown To Select Between Fancy And Fast Graphics
    [SerializeField] private Toggle FullscreenToggle = null; // Toggle To Select Fullscreen Or Windowed Game
    [SerializeField] private TMP_Text ToggleText = null; // Fullscreen Toggle Text
    [SerializeField] private Slider VolumeSilder = null; // Audio Volume Selector

    private Resolution[] screenResolutions; // Array Containing All Resolutions
    private Resolution selectedResolution; // Variable Containing Selected Resolution
    #endregion

    #region On Start
    private void Start()
    {
        screenResolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray(); // All Available Monitor Resolutions With 60 Refresh Rate
        PrepareResolutionDropdown(); // Prepare Resolution Dropdown
        PrepareGraphicsDropdown(); // Prepare Graphics Dropdown
        SetSavedOptions(); // Get Player Set Saved Options And Set Them
    }
    #endregion

    #region Preparation Functions
    /// <summary>
    /// Function To Prepare Resolutions Dropdown List
    /// </summary>
    public void PrepareResolutionDropdown()
    {
        if (screenResolutions.Length > 0) // If Not Mobile
        {
            int currentResolutionIndex = 0; // Index Of Current Resolution
            resolutionDropdown.ClearOptions(); // Empty Dropdown Values
            List<string> resolutionsList = new List<string>(); // List For TMP_Dropdown

            for (int i = 0; i < screenResolutions.Length; i++)
            {
                string res = screenResolutions[i].width + " x " + screenResolutions[i].height; // resolution string

                if (!resolutionsList.Contains(res))
                    resolutionsList.Add(res); // Creating List Of Resolutions 

                // Get Current Resolution Index
                if (screenResolutions[i].width == Screen.width && screenResolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i; // Variable That Has Index Of Current Resolution
                }
            }

            resolutionDropdown.AddOptions(resolutionsList); // Add Resolutions List To Dropdown
            resolutionDropdown.value = currentResolutionIndex; // Select Current Resolution In Dropdown
            resolutionDropdown.RefreshShownValue(); // Refresh Dropdown Shown Value
        }
    }
    /// <summary>
    /// Function To Prepare Graphics Dropdown List
    /// </summary>
    public void PrepareGraphicsDropdown()
    {
        graphicsDropdown.ClearOptions(); // Empty Dropdown Values
        string[] qualitySettings = QualitySettings.names; // Array Of All Graphics
        List<string> graphicsList = new List<string>(); // List For TMP_Dropdown
        int currentQualityIndex = 0; // Index Of Current Graphics

        for (int i = 0; i < qualitySettings.Length; i++)
        {
            string settingsName = LocalisationSystem.GetLocalisedValue(qualitySettings[i]); // Localised Graphics Name

            if (!graphicsList.Contains(settingsName))
                graphicsList.Add(settingsName); // Creating List Of Graphics
        }
        graphicsDropdown.AddOptions(graphicsList); // Add Graphics List To Dropdown
        graphicsDropdown.value = PlayerPrefs.GetInt("qualityIndex", currentQualityIndex); // Select Current Graphic In Dropdown
        graphicsDropdown.RefreshShownValue(); // Refresh Dropdown Shown Value
    }
    /// <summary>
    /// Function To Save Last Saved Options
    /// </summary>
    public void SetSavedOptions()
    {
        int qualityIndex = int.MinValue; // Index Of Selected Quality
        int isFullscreen = int.MinValue; // Selected Game Mode (Fullscreen Or Windowed)
        int resolutionIndex = int.MinValue; // Index Of Selected Game Resolution

        //Volume
        VolumeSilder.value = PlayerPrefs.GetFloat("volume", 1f); // Set VolumeSlider To Last Saved Volume Value
        audioMixer.SetFloat("volume", Mathf.Log10(VolumeSilder.value) * 20); // Set AudioMixer Value To Last Saved Value

        //Graphics
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityIndex", qualityIndex)); // Set QualityLevel To Last Saved Value
        graphicsDropdown.value = PlayerPrefs.GetInt("qualityIndex", qualityIndex); // Set Dropdown Value To Last Saved Value
        graphicsDropdown.RefreshShownValue(); // Refresh Dropdown Shown Value

        if (screenResolutions.Length > 0) // If Not Mobile
        {
            //Fullscreen
            isFullscreen = PlayerPrefs.GetInt("isFullscreen", isFullscreen); // Get Game Mode Value
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
            resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", resolutionIndex); // Set Resolution Index To Last Saved Index
            resolutionDropdown.RefreshShownValue(); // Refresh Dropdown Value
            Resolution selectedResolution = screenResolutions[resolutionDropdown.value]; // Get Last Saved Resolution
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen); // Set Resolution To Last Saved Value
        }
        else // For Mobile
        {
            resolutionDropdown.gameObject.SetActive(false);
            FullscreenToggle.gameObject.SetActive(false);
            Destroy(ToggleText);
        }
    }
    #endregion

    #region Settings Get Set Functions
    /// <summary>
    /// Set Game Volume
    /// </summary>
    /// <param name="volume">Game volume</param>
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("volume", Mathf.Log(volume) * 20);
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
