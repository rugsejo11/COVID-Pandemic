using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sound[] sounds = null; // List Of Audio Manager Sounds
    private static AudioManager instance; // Audio Manager Instance
    private int lastStep = 0;
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
            s.source.clip = s.GetClip();
            s.source.volume = s.GetVolume();
            s.source.pitch = s.GetPitch();
            s.source.loop = s.GetLoop();
        }
    }

    /// <summary>
    /// Play [name] sound
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.GetName() == name); // Find Sound From Sounds List
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found!");
            return;
        }
        if (!s.source.isPlaying)
            s.source.Play(); // Play Sound
    }

    public void PlayFootstep()
    {
        int newStep = UnityEngine.Random.Range(1, 4);
        if (newStep != lastStep)
        {
            switch (newStep)
            {
                case 1:
                    Play("footStep1");
                    lastStep = newStep;
                    break;
                case 2:
                    Play("footStep2");
                    lastStep = newStep;
                    break;
                case 3:
                    Play("footStep3");
                    lastStep = newStep;
                    break;
                case 4:
                    Play("footStep4");
                    lastStep = newStep;
                    break;

                default:
                    break;
            }
        }
        else PlayFootstep();
    }
    #endregion
}
