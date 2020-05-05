using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string name = string.Empty; // Audio Clip Name
    [SerializeField] private AudioClip clip = null; // Audio Clip
    [Range(0f, 1f)]
    [SerializeField] private float volume = 1; // Audio Clip Volume
    [Range(.1f, 3f)]
    [SerializeField] private float pitch = 1; // Audio Clip Pitch
    [SerializeField] private bool loop = false; // Bool To Loop
    public AudioSource source = null; // Audio Source

    /// <summary>
    /// Function to return name
    public string GetName()
    {
        return name;
    }

    /// <summary>
    /// Function to return audio clip
    /// </summary>
    /// <returns></returns>
    public AudioClip GetClip()
    {
        return clip;
    }

    /// <summary>
    /// Function to return volume
    /// </summary>
    /// <returns></returns>
    public float GetVolume()
    {
        return volume;
    }

    /// <summary>
    /// Function to return pitch
    /// </summary>
    /// <returns></returns>
    public float GetPitch()
    {
        return pitch;
    }

    /// <summary>
    /// Function to return loop
    /// </summary>
    /// <returns></returns>
    public bool GetLoop()
    {
        return loop;
    }
}
