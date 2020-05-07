using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalisationSystemScript : MonoBehaviour
{
    #region Variables

    private static Dictionary<string, string> localisedEN; // English Dictionary Values
    private static Dictionary<string, string> localisedLT; // Lithuanian Dictionary Values
    private static Dictionary<string, string> localisedPL; // Polish Dictionary Values

    public static bool isInit; // Is Inisitalisation Process Done

    public static LoadTranslationsScript translationsFile; // CSV Loader For Loading CSV Data File

    #endregion

    #region Localisation system functions

    /// <summary>
    /// Function to initialise localisation system
    /// </summary>
    public static void Init()
    {
        translationsFile = new LoadTranslationsScript(); // Create New LoadTranslationsScript
        translationsFile.LoadTranslationsFile(); //Load LoadTranslationsScript

        UpdateDictionaries(); // Update Localisation System Dictionary Values

        isInit = true; // Initialisation Process Done
    }

    /// <summary>
    /// Function to update localisation system dictionary values
    /// </summary>
    public static void UpdateDictionaries()
    {
        localisedEN = translationsFile.GetDictionaryValues("en"); // English Dictionary Values
        localisedLT = translationsFile.GetDictionaryValues("lt"); // Lithuanian Dictionary Values
        localisedPL = translationsFile.GetDictionaryValues("pl"); // Polish Dictionary Values
    }

    /// <summary>
    /// Function to set UI To English language
    /// </summary>
    public void UpdateLanguageToEnglish()
    {
        PlayerPrefs.SetString("Language", "English"); // Save Language Player Preference To English
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload Active Scene
    }

    /// <summary>
    /// Function to set UI To Lithuanian language
    /// </summary>
    public void UpdateLanguageToLithuanian()
    {
        PlayerPrefs.SetString("Language", "Lithuanian"); // Save Language Player Preference To Lithuanian
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload Active Scene
    }

    /// <summary>
    /// Function to set UI To Polish language
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
    /// Function to get localised value of the key
    /// </summary>
    /// <param name="key">Localisation key</param>
    /// <returns></returns>
    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); } // Initialise if not initialised before
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
            default:
                languageString = "English";
                PlayerPrefs.SetString("Language", "English"); // Save Language Player Preference To English
                localisedEN.TryGetValue(key, out value);
                break;
        }
        return value;
    }
    #endregion

    #region Unity editor functions
#if UNITY_EDITOR

    /// <summary>
    /// Function to add a word to localisation list
    /// </summary>
    /// <param name="key"></param>
    /// <param name="word"></param>
    public static void Add(string key, string word)
    {
        if (word.Contains("\""))
        {
            word.Replace('"', '\"');
        }

        if (translationsFile == null)
        {
            translationsFile = new LoadTranslationsScript();
        }

        translationsFile.LoadTranslationsFile();
        translationsFile.Add(key, word);
        translationsFile.LoadTranslationsFile();

        UpdateDictionaries();
    }

    /// <summary>
    /// Function to replace a word on a localisation list
    /// </summary>
    /// <param name="key"></param>
    /// <param name="word"></param>
    public static void Replace(string key, string word)
    {
        if (word.Contains("\""))
        {
            word.Replace('"', '\"');
        }

        if (translationsFile == null)
        {
            translationsFile = new LoadTranslationsScript();
        }

        translationsFile.LoadTranslationsFile();
        translationsFile.Edit(key, word);
        translationsFile.LoadTranslationsFile();

        UpdateDictionaries();
    }

    /// <summary>
    /// Function to remove a word on a localisation list
    /// </summary>
    /// <param name="key"></param>
    public static void Remove(string key)
    {
        if (translationsFile == null)
        {
            translationsFile = new LoadTranslationsScript();
        }

        translationsFile.LoadTranslationsFile();
        translationsFile.Remove(key);
        translationsFile.LoadTranslationsFile();

        UpdateDictionaries();
    }
#endif
    #endregion
}
