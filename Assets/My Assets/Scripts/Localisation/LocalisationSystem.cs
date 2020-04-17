using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalisationSystem : MonoBehaviour
{
    #region Variables
    private static Dictionary<string, string> localisedEN; // English Dictionary Values
    private static Dictionary<string, string> localisedLT; // Lithuanian Dictionary Values
    private static Dictionary<string, string> localisedPL; // Polish Dictionary Values

    public static bool isInit; // Is Inisitalisation Process Done

    public static CSVLoader csvLoader; // CSV Loader For Loading CSV Data File
    #endregion

    #region Functions

    /// <summary>
    /// Initialise Localisation System
    /// </summary>
    public static void Init()
    {
        csvLoader = new CSVLoader(); // Create New CSVLoader
        csvLoader.LoadCSV(); //Load CSVLoader

        UpdateDictionaries(); // Update Localisation System Dictionary Values

        isInit = true; // Initialisation Process Done
    }

    /// <summary>
    /// Update Localisation System Dictionary Values
    /// </summary>
    public static void UpdateDictionaries()
    {
        localisedEN = csvLoader.GetDictionaryValues("en"); // English Dictionary Values
        localisedLT = csvLoader.GetDictionaryValues("lt"); // Lithuanian Dictionary Values
        localisedPL = csvLoader.GetDictionaryValues("pl"); // Polish Dictionary Values
    }

    /// <summary>
    /// Set UI To English
    /// </summary>
    public void UpdateLanguageToEnglish()
    {
        PlayerPrefs.SetString("Language", "English"); // Save Language Player Preference To English
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload Active Scene
    }

    /// <summary>
    /// Set UI To Lithuanian
    /// </summary>
    public void UpdateLanguageToLithuanian()
    {
        PlayerPrefs.SetString("Language", "Lithuanian"); // Save Language Player Preference To Lithuanian
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload Active Scene
    }

    /// <summary>
    /// SET UI TO POLISH
    /// </summary>
    public void UpdateLanguageToPolish()
    {
        PlayerPrefs.SetString("Language", "Polish"); // Save Language Player Preference To Polish
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload Active Scene
    }

    /// <summary>
    /// Unity Editor Dictionary Localised To English
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetDictionaryForEditor()
    {
        if (!isInit) { Init(); }
        return localisedEN;
    }

    /// <summary>
    /// Get Localised Value Of Key
    /// </summary>
    /// <param name="key">Localisation key</param>
    /// <returns></returns>
    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); } // Initialise If Not
        string value = key;
        string languageString = string.Empty;
        languageString = PlayerPrefs.GetString("Language", languageString); // Get Player Set Language
        switch (languageString)
        {
            case "English":
                localisedEN.TryGetValue(key, out value);
                break;
            case "Lithuanian":
                localisedLT.TryGetValue(key, out value);
                break;
            case "Polish":
                localisedPL.TryGetValue(key, out value);
                break;
        }
        return value;
    }
    #endregion

    #region UNITY EDITOR
#if UNITY_EDITOR

    /// <summary>
    /// Add Word To Localisation List
    /// </summary>
    /// <param name="key"></param>
    /// <param name="word"></param>
    public static void Add(string key, string word)
    {
        if (word.Contains("\""))
        {
            word.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key, word);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Replace(string key, string word)
    {
        if (word.Contains("\""))
        {
            word.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Edit(key, word);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }
#endif
    #endregion
}
