using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroInteractive : MonoBehaviour
{
    public Transform GoalPosition; // Level goal position
    public bool objectGrabbed = false; // Variable holding value if hero has object in he's hands
    private AudioSource audioSource = null;
    [SerializeField] private AudioClip wrongBuzzer = null;  // buzzer sound played when pressed wrong button.

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    ///
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;



    #region Sigleton
    private static HeroInteractive instance;
    public static HeroInteractive Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HeroInteractive>();
            return instance;
        }
    }
    #endregion

    [SerializeField] private float health = float.MinValue;
    [SerializeField] private float maxHealth = float.MinValue;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

    public void LoseHP()
    {
        health -= 1;
        ClampHealth();
        if (wrongBuzzer != null)
        {
            audioSource.clip = wrongBuzzer;
            audioSource.Play();
        }
        if(health == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }


    ///


    /// <summary>
    /// Function to fill hero's hands 
    /// </summary>
    public void GrabObject()
    {
        objectGrabbed = true;

    }

    /// <summary>
    /// Function to empty hero's hands
    /// </summary>
    public void DropObject()
    {
        objectGrabbed = false;
    }

    /// <summary>
    /// Function to check if hero's hands are empty
    /// </summary>
    /// <returns></returns>
    public bool IsObjectGrabbed()
    {
        return objectGrabbed;
    }
}
