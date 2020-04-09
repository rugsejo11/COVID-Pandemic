using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextLocaliserEditWindow : EditorWindow
{
    public static void Open(string key)
    {
        var window = (TextLocaliserEditWindow)ScriptableObject.CreateInstance(typeof(TextLocaliserEditWindow));
        //TextLocaliserEditWindow window = new TextLocaliserEditWindow();
        window.titleContent = new GUIContent("Localiser Window");
        window.ShowUtility();
        window.key = key;
    }

    public string key;
    public string enWord; public string ltWord; public string plWord;

    public void OnGUI()
    {
        key = EditorGUILayout.TextField("Key :", key);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("English:", GUILayout.MaxWidth(50));

        EditorStyles.textArea.wordWrap = true;
        enWord = EditorGUILayout.TextArea(enWord, EditorStyles.textArea, GUILayout.Height(30), GUILayout.Width(100));


        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Lithuanian:", GUILayout.MaxWidth(100));
        ltWord = EditorGUILayout.TextArea(ltWord, EditorStyles.textArea, GUILayout.Height(30), GUILayout.Width(100));


        EditorGUILayout.LabelField("Polish:", GUILayout.MaxWidth(100));
        plWord = EditorGUILayout.TextArea(plWord, EditorStyles.textArea, GUILayout.Height(30), GUILayout.Width(100));

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Add"))
        {
            if (LocalisationSystem.GetLocalisedValue(key) != string.Empty)
            {
                LocalisationSystem.Replace(key, enWord, ltWord, plWord);
            }
            else
            {
                LocalisationSystem.Add(key, enWord, ltWord, plWord);
            }
        }

        minSize = new Vector2(460, 250);
        maxSize = minSize;
    }
}

public class TextLocaliserSearchWindow : EditorWindow
{
    public static void Open()
    {
        var window = (TextLocaliserSearchWindow)ScriptableObject.CreateInstance(typeof(TextLocaliserSearchWindow));
        //TextLocaliserSearchWindow window = new TextLocaliserSearchWindow();
        window.titleContent = new GUIContent("Localisation Search");

        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
        window.ShowAsDropDown(r, new Vector2(500, 300));

    }
    public string value;
    public Vector2 scroll;
    public Dictionary<string, string> dictionary;

    private void OnEnable()
    {
        dictionary = LocalisationSystem.GetDictionaryForEditor();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Search:", EditorStyles.boldLabel);
        value = EditorGUILayout.TextField(value);
        EditorGUILayout.EndHorizontal();

        GetSearchResults();
    }
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
                        LocalisationSystem.Remove(element.Key);
                        AssetDatabase.Refresh();
                        LocalisationSystem.Init();
                        dictionary = LocalisationSystem.GetDictionaryForEditor();
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