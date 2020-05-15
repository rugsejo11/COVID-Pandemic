using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR 
[CustomPropertyDrawer(typeof(LocalisedStringScript))]
public class LocalisedStringDrawerScript : PropertyDrawer
{
    #region Variables 

    private bool dropdown; // text dropdown
    private float height; // window height

    #endregion

    #region Localised String Drawer functions 

    /// <summary>
    /// Function to get property height of the localised string drawer window
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (dropdown)
        {
            return height + 25;
        }
        return 20;
    }

    /// <summary>
    /// Function to set user interface for entering, searching, updating localised values
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.width -= 34;
        position.height = 18;

        Rect valueRect = new Rect(position);
        valueRect.x += 15;
        valueRect.width -= 15;

        Rect foldButtonRect = new Rect(position);
        foldButtonRect.width = 15;

        dropdown = EditorGUI.Foldout(foldButtonRect, dropdown, "");

        position.x += 15;
        position.width -= 15;

        SerializedProperty key = property.FindPropertyRelative("key");
        key.stringValue = EditorGUI.TextField(position, key.stringValue);

        position.x += position.width + 2;
        position.width = 17;
        position.height = 17;

        Texture searchIcon = (Texture)Resources.Load("search");
        GUIContent searchContent = new GUIContent(searchIcon);

        if (GUI.Button(position, searchContent))
        {
            TextLocaliserSearchWindow.Open();
        }

        position.x += position.width + 2;
        Texture storeIcon = (Texture)Resources.Load("store");
        GUIContent storeContent = new GUIContent(storeIcon);
        if (GUI.Button(position, storeContent))
        {
            TextLocaliserEditWindow.Open(key.stringValue);

        }
        if (dropdown)
        {
            var value = LocalisationSystemScript.GetLocalisedValue(key.stringValue);
            GUIStyle style = GUI.skin.box;
            height = style.CalcHeight(new GUIContent(value), valueRect.width);

            valueRect.height = height;
            valueRect.y += 21;
            EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
        }

        EditorGUI.EndProperty();
    }

    #endregion
}
#endif