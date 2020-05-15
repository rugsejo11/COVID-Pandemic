using UnityEngine;

public class HeroData : MonoBehaviour
{
    #region Variables

    public delegate void HPChangeDelegate();
    private HPChangeDelegate onHPChangeCallback; // Delegate for updating hearts UI
    private bool objectGrabbed = false; // Variable holding value if hero has object in he's hands
    [SerializeField] private bool handsWashed = false; // Variable holding value if hero has washed his hands
    [SerializeField] private int health = 1; // current health points hero has
    [SerializeField] private int maxHealth = 1; // max health points hero can have
    private float timeAfterTakingFromRack; // Variable holding time since taking item from the rack, because item can only be placed after 2 seconds after been taken out

    #endregion

    #region Functions

    #region Hero HP and Washing Hands Functions

    /// <summary>
    /// Function to set delegate updating hearts UI
    /// </summary>
    /// <param name="function"></param>
    public void SetDelegate(HPChangeDelegate function)
    {
        onHPChangeCallback += function;
    }

    /// <summary>
    /// Function to get current health points hero has
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
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
    private void UpdateHPBar()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        onHPChangeCallback?.Invoke();

        CheckIfAlive();
    }

    /// <summary>
    /// Function to check if hero run out of health points
    /// </summary>
    private void CheckIfAlive()
    {
        if (health == 0)
        {
            SceneManageScript.RestartScene();
        }
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

    #endregion

    #region Grabbing Item Functions

    /// <summary>
    /// Function to get variable timeAfterTakingFromRack data
    /// </summary>
    /// <returns></returns>
    public float GetTimeAfterTakingFromRack()
    {
        return timeAfterTakingFromRack;
    }
    /// <summary>
    /// Function to set variable timeAfterTakingFromRack value
    /// </summary>
    /// <param name="time"></param>
    public void SetTimeAfterTakingFromRack(float time)
    {
        timeAfterTakingFromRack = time;
    }
    /// <summary>
    /// Function to add time to timeAfterTakingFromRack variable
    /// </summary>
    /// <param name="time"></param>
    public void AddTimeAfterTakingFromRack(float time)
    {
        timeAfterTakingFromRack += time;
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

    #endregion

    /// <summary>
    /// Function to play buzzer sound, because of wrong hero's action
    /// </summary>
    private void PlayBuzzerSound()
    {
        FindObjectOfType<AudioManagerScript>().Play("wrong_buzzer"); // Play Button Press Audio
    }
    #endregion
}
