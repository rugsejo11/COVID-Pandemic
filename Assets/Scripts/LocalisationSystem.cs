using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalisationSystem : MonoBehaviour
{

    public enum Language
    {
        English,
        Polish,
        Lithuanian
    }

    private static Language language; // Selected Language Object
    private string lang; // Selected Language In String
    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedLT;
    private static Dictionary<string, string> localisedPL;

    public static bool isInit;

    public static CSVLoader csvLoader;

    private void Start()
    {
        lang = PlayerPrefs.GetString("Language", lang);
        switch (lang)
        {
            case "English":
                language = Language.English;
                break;
            case "Lithuanian":
                language = Language.Lithuanian;
                break;
            case "Polish":
                language = Language.Polish;
                break;
        }
    }
    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionaries();

        isInit = true;
    }

    public static void UpdateDictionaries()
    {
        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedLT = csvLoader.GetDictionaryValues("lt");
        localisedPL = csvLoader.GetDictionaryValues("pl");
    }


    public void UpdateLanguageToEnglish()
    {
        language = Language.English;
        PlayerPrefs.SetString("Language", "English");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLanguageToLithuanian()
    {
        language = Language.Lithuanian;
        PlayerPrefs.SetString("Language", "Lithuanian");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLanguageToPolish()
    {
        language = Language.Polish;
        PlayerPrefs.SetString("Language", "Polish");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static Dictionary<string, string> GetDictionaryForEditor()
    {
        if (!isInit) { Init(); }
        return localisedEN;
    }
    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }
        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Lithuanian:
                localisedLT.TryGetValue(key, out value);
                break;
            case Language.Polish:
                localisedPL.TryGetValue(key, out value);
                break;
        }
        return value;
    }
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
