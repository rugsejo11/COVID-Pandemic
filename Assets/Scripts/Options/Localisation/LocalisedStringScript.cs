[System.Serializable]
public struct LocalisedStringScript
{
    public string key; // localised value key

    /// <summary>
    /// Localisated string constructor
    /// </summary>
    /// <param name="key"></param>
    public LocalisedStringScript(string key)
    {
        this.key = key;
    }

    /// <summary>
    /// Function to get localised key value
    /// </summary>
    public string GetValue()
    {
        return LocalisationSystemScript.GetLocalisedValue(key);
    }
}
