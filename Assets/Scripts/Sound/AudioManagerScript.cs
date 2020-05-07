using UnityEngine;
using System;

public class AudioManagerScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private SoundScript[] sounds = null; // List Of Audio Manager Sounds
    private static AudioManagerScript instance; // Audio Manager Instance
    private int lastStep = 0; // Variable holding last footstep sound played

    #endregion

    #region Functions

    /// <summary>
    /// Function initialize any variables or game state before the game starts
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

        // Run through all the sounds in the sounds array and save them
        foreach (SoundScript s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.GetClip();
            s.source.volume = s.GetVolume();
            s.source.pitch = s.GetPitch();
            s.source.loop = s.GetLoop();
        }
    }

    /// <summary>
    /// Function to play entered sound
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        SoundScript s = Array.Find(sounds, sound => sound.GetName() == name); // Find Sound From Sounds List
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found!");
            return;
        }
        if (!s.source.isPlaying)
            s.source.Play(); // Play Sound
    }

    /// <summary>
    /// Funtion to play footstep sound
    /// </summary>
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
