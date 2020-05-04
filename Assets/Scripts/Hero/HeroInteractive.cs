using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroInteractive : MonoBehaviour
{
    public delegate void HPChangeDelegate();
    public HPChangeDelegate onHPChangeCallback;
    public Transform GoalPosition; // Level goal position
    public bool objectGrabbed = false; // Variable holding value if hero has object in he's hands
    public bool handsWashed = false; // Variable holding value if hero has washed his hands

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
        PlayBuzzerSound();
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHPChangeCallback != null)
            onHPChangeCallback.Invoke();
        CheckHP();
    }

    /// <summary>
    /// 
    /// </summary>
    void PlayBuzzerSound()
    {
        FindObjectOfType<AudioManager>().Play("wrong_buzzer"); // Play Button Press Audio
    }

    /// <summary>
    /// 
    /// </summary>
    void CheckHP()
    {
        if (health == 0)
        {
            MenuControl.GetToMenu();
        }
    }

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

    /// <summary>
    /// Function to wash hero's hands
    /// </summary>
    public void WashHands()
    {
        handsWashed = true;
    }

    /// <summary>
    /// Function to make hero's hands dirty
    /// </summary>
    public void DirtyHands()
    {
        handsWashed = false;
    }

    /// <summary>
    /// Function to get if hero's hands were washed
    /// </summary>
    /// <returns></returns>
    public bool WereHandsWashed()
    {
        return handsWashed;
    }
}
