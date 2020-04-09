using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem : MonoBehaviour
{

    public enum Language
    {
        English,
        //French,
        Polish,
        Lithuanian
    }

    public static Language language = Language.English;

    private static Dictionary<string, string> localisedEN;
    //private static Dictionary<string, string> localisedFR;
    private static Dictionary<string, string> localisedLT;
    private static Dictionary<string, string> localisedPL;

    public static bool isInit;

    public static CSVLoader csvLoader;

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
        //localisedFR = csvLoader.GetDictionaryValues("fr");
        localisedLT = csvLoader.GetDictionaryValues("lt");
        localisedPL = csvLoader.GetDictionaryValues("pl");
    }


    public void UpdateLanguageToEnglish()
    {
        language = Language.English;
    }

    public void UpdateLanguageToLithuanian()
    {
        language = Language.Lithuanian;
    }

    public void UpdateLanguageToPolish()
    {
        language = Language.Polish;
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
            //case Language.French:
            //    localisedFR.TryGetValue(key, out value);
            //    break;
            case Language.Lithuanian:
                localisedLT.TryGetValue(key, out value);
                break;
            case Language.Polish:
                localisedPL.TryGetValue(key, out value);
                break;
        }
        return value;
    }


    public static void Add(string key, string ltWord, string enWord, string plWord)
    {
        if (ltWord.Contains("\""))
        {
            ltWord.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key, ltWord, enWord, plWord);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Replace(string key, string ltWord, string enWord, string plWord)
    {
        if (ltWord.Contains("\""))
        {
            ltWord.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Edit(key, ltWord, enWord, plWord);
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
}
