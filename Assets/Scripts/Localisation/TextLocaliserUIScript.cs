using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class TextLocaliserUIScript : MonoBehaviour
{
    TextMeshProUGUI textField; // textfield to show localised string value
    public LocalisedStringScript localisedString; // localised string key

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        textField.text = localisedString.GetValue();
    }
}
