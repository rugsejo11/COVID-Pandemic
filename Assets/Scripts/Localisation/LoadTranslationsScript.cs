using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoadTranslationsScript
{
    #region Variables

    private TextAsset translationsFile; // translations file
    private char lineSeperator = '\n'; // line seperator
    private char surround = '"'; // word surrounded by this char
    private string[] fieldSeperator = { "\",\"" }; // field seperated by this string

    #endregion

    #region Load translations functions
    /// <summary>
    /// Function to load translations file
    /// </summary>
    public void LoadTranslationsFile()
    {
        translationsFile = Resources.Load<TextAsset>("localisation_translations");
    }


    /// <summary>
    /// Function to work with translation file and get translated value
    /// </summary>
    /// <param name="attributeId">asked language to translate to id</param>
    /// <returns></returns>
    public Dictionary<string, string> GetDictionaryValues(string attributeId)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = translationsFile.text.Split(lineSeperator);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(fieldSeperator, StringSplitOptions.None);

        for (int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(attributeId))
            {
                attributeIndex = i;
                break;
            }
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] words = line.Split(',');
            for (int j = 0; j < words.Length; j++)
            {
                words[j] = words[j].TrimStart(' ', surround).TrimEnd(surround, '\n', '\r');
            }
            if (words.Length > attributeIndex)
            {
                var key = words[0];

                if (dictionary.ContainsKey(key)) { continue; }

                var value = words[attributeIndex];

                dictionary.Add(key, value);
            }

        }
        return dictionary;
    }
    #endregion

    #region Unity editor functions
#if UNITY_EDITOR
    /// <summary>
    /// Add new translation to translations file
    /// </summary>
    /// <param name="key"></param>
    /// <param name="word"></param>
    public void Add(string key, string word)
    {
        string append = string.Format("\n\"{0}\",\"{1}\",\"\",\"\"", key, word);
        File.AppendAllText("Assets/Resources/localisation_translations.csv", append);

        UnityEditor.AssetDatabase.Refresh();
    }

    /// <summary>
    /// Remove translation from to translations file
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key)
    {
        string[] lines = translationsFile.text.Split(lineSeperator);
        string[] keys = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            keys[i] = line.Split(fieldSeperator, StringSplitOptions.None)[0];
        }

        int index = -1;
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].Contains(key))
            {
                index = i;
                break;
            }
        }

        if (index > -1)
        {
            string[] newLines;
            newLines = lines.Where(w => w != lines[index]).ToArray();

            string replaced = string.Join(lineSeperator.ToString(), newLines);
            File.WriteAllText("Assets/Resources/localisation_translations.csv", replaced);
        }
    }

    /// <summary>
    /// Edit translation at translations file
    /// </summary>
    /// <param name="key"></param>
    /// <param name="word"></param>
    public void Edit(string key, string word)
    {
        Remove(key);
        Add(key, word);
    }
#endif
    #endregion
}
