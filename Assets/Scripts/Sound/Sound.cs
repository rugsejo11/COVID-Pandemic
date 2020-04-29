using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name; // Audio Clip Name
    public AudioClip clip; // Audio Clip

    [Range(0f,1f)]
    public float volume; // Audio Clip Volume
    [Range(.1f,3f)]
    public float pitch; // Audio Clip Pitch
    public bool loop; // Bool To Loop

    [HideInInspector]
    public AudioSource source; // Audio Source
}
