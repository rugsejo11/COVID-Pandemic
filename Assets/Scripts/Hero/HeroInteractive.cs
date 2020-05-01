using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInteractive : MonoBehaviour
{
    public Transform GoalPosition; // Level goal position
    public bool objectGrabbed = false; // Variable holding value if hero has object in he's hands

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

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    //[SerializeField] private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

    void Start()
    {
        maxHealth = 3;
    }
    public void LoseHP()
    {
        health -= 1;
        ClampHealth();
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    //public float MaxTotalHealth { get { return maxTotalHealth; } }

    //public void Heal(float health)
    //{
    //    this.health += health;
    //    ClampHealth();
    //}



    //public void AddHealth()
    //{
    //    if (maxHealth < maxTotalHealth)
    //    {
    //        maxHealth += 1;
    //        health = maxHealth;

    //        if (onHealthChangedCallback != null)
    //            onHealthChangedCallback.Invoke();
    //    }
    //}



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
