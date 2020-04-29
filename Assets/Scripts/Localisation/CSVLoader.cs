using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVLoader
{
    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char surround = '"';
    private string[] fieldSeperator = { "\",\"" };

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("localisation_translations");
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeId)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeperator);

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

    #region Unity Editor
#if UNITY_EDITOR
    public void Add(string key, string word)
    {
        string append = string.Format("\n\"{0}\",\"{1}\",\"\",\"\"", key, word);
        File.AppendAllText("Assets/Resources/localisation.csv", append);

        UnityEditor.AssetDatabase.Refresh();
    }

    public void Remove(string key)
    {
        string[] lines = csvFile.text.Split(lineSeperator);
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
            File.WriteAllText("Assets/Resources/localisation.csv", replaced);
        }
    }

    public void Edit(string key, string word)
    {
        Remove(key);
        Add(key, word);
    }
#endif
    #endregion
}
