using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class TextLocaliserEditWindow : EditorWindow
{
    /// <summary>
    /// Function to open localisation editing interface
    /// </summary>
    /// <param name="key"></param>
    public static void Open(string key)
    {
        var window = (TextLocaliserEditWindow)ScriptableObject.CreateInstance(typeof(TextLocaliserEditWindow));
        window.titleContent = new GUIContent("Localiser Window");
        window.ShowUtility();
        window.key = key;
    }

    private string key; // localised value key
    private string word; // localised value word

    /// <summary>
    /// Function to set user interface for updating localised values
    /// </summary>
    public void OnGUI()
    {
        key = EditorGUILayout.TextField("Key :", key);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("English:", GUILayout.MaxWidth(50));

        EditorStyles.textArea.wordWrap = true;
        word = EditorGUILayout.TextArea(word, EditorStyles.textArea, GUILayout.Height(30), GUILayout.Width(100));


        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Add"))
        {
            if (LocalisationSystemScript.GetLocalisedValue(key) != string.Empty)
            {
                LocalisationSystemScript.Replace(key, word);
            }
            else
            {
                LocalisationSystemScript.Add(key, word);
            }
        }

        minSize = new Vector2(460, 250);
        maxSize = minSize;
    }
}


public class TextLocaliserSearchWindow : EditorWindow
{
    /// <summary>
    /// Function to set search localisation values interface
    /// </summary>
    public static void Open()
    {
        var window = (TextLocaliserSearchWindow)ScriptableObject.CreateInstance(typeof(TextLocaliserSearchWindow));
        window.titleContent = new GUIContent("Localisation Search");

        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
        window.ShowAsDropDown(r, new Vector2(500, 300));

    }
    public string value;
    public Vector2 scroll;
    public Dictionary<string, string> dictionary;

    /// <summary>
    /// Function to set dictionary on search window enabling
    /// </summary>
    private void OnEnable()
    {
        dictionary = LocalisationSystemScript.GetDictionaryForEditor();
    }

    /// <summary>
    /// Function to set user interface for searching localised values
    /// </summary>
    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Search:", EditorStyles.boldLabel);
        value = EditorGUILayout.TextField(value);
        EditorGUILayout.EndHorizontal();

        GetSearchResults();
    }

    /// <summary>
    /// Function to output localisation search results
    /// </summary>
    private void GetSearchResults()
    {
        if (value == null) { return; }
        EditorGUILayout.BeginVertical();
        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (KeyValuePair<string, string> element in dictionary)
        {
            if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
            {
                EditorGUILayout.BeginHorizontal("box");
                Texture closeIcon = (Texture)Resources.Load("close");

                GUIContent content = new GUIContent(closeIcon);

                if (GUILayout.Button(content, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                {
                    if (EditorUtility.DisplayDialog("Remove key " + element.Key + "?", "This will remove the element from the localisation, are you sure?", "Do it"))
                    {
                        LocalisationSystemScript.Remove(element.Key);
                        AssetDatabase.Refresh();
                        LocalisationSystemScript.Init();
                        dictionary = LocalisationSystemScript.GetDictionaryForEditor();
                    }
                }

                EditorGUILayout.TextField(element.Key);
                EditorGUILayout.LabelField(element.Value);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
#endif