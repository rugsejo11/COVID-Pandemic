using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroInteractive : MonoBehaviour
{
    public delegate void HPChangeDelegate();
    public HPChangeDelegate onHPChangeCallback;
    public bool objectGrabbed = false; // Variable holding value if hero has object in he's hands
    public bool handsWashed = false; // Variable holding value if hero has washed his hands
    [SerializeField] private float health = float.MinValue; // current health points hero has
    [SerializeField] private float maxHealth = float.MinValue; // max health points hero can have


    /// <summary>
    /// Function to get current health points hero has
    /// </summary>
    /// <returns></returns>
    public float GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Function to get max health points hero can have
    /// </summary>
    /// <returns></returns>
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Function to remove one health point
    /// </summary>
    public void LoseHP()
    {
        health -= 1;
        UpdateHPBar();
        PlayBuzzerSound();
    }

    /// <summary>
    /// Function to update hero's health bar
    /// </summary>
    void UpdateHPBar()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        onHPChangeCallback?.Invoke();

        CheckIfAlive();
    }

    /// <summary>
    /// Function to play buzzer sound, because of wrong hero's action
    /// </summary>
    void PlayBuzzerSound()
    {
        FindObjectOfType<AudioManager>().Play("wrong_buzzer"); // Play Button Press Audio
    }

    /// <summary>
    /// Function to check if hero run out of health points
    /// </summary>
    void CheckIfAlive()
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
