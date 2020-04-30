using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public Sound[] sounds; // List Of Audio Manager Sounds
    public static AudioManager instance; // Audio Manager Instance
    #endregion

    #region Functions
    /// <summary>
    /// On Start Do
    /// </summary>
    private void Awake()
    {
        // Keep Playing Sound While Switching Scenes
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// Play [name] sound
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find Sound From Sounds List
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found!");
            return;
        }
        if(!s.source.isPlaying)
        s.source.Play(); // Play Sound
    }

    /// <summary>
    /// Play Button Pressed EFX
    /// </summary>
    public void ButtonPressed()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "buttonPress"); // Find ButtonPress Sound From Sounds List
        if (s == null)
        {
            Debug.LogWarning("Sound buttonPress not found!");
            return;
        }
        if (s.source == null)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // If Component Not Found Create It
        }
        s.source.volume = PlayerPrefs.GetFloat("volume", 1f);
        s.source.Play(); // Play Sound
    }
    #endregion
}
