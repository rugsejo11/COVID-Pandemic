using System.Collections;
using UnityEngine;
public class UsingSinkScript : MonoBehaviour
{
    // SinkIsCloseEnough()
    private float distance; // Variable holding distance between hero and sink
    private float angleView; // Variable holding angle difference between hero camera and isinktem
    private Vector3 direction; // Variable holding hero camera direction

    [SerializeField] private GameObject washHandsNotification = null;
    [SerializeField] private GameObject washHandsEducationNotification = null;

    private HeroDataScript hero; // Game character
    [SerializeField] private bool isWorking = true; // Is sink is working
    [SerializeField] private bool waterValveOn = true; // Is sink's water valve is on

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    void Start()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    void Update()
    {
        Interaction();
    }

    /// <summary>
    /// Function to interact with sink
    /// </summary>
    void Interaction()
    {
        if (SinkIsCloseEnough() && !hero.WereHandsWashed())
        {
            if (waterValveOn)
            {
                ShowNotification(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    hero.WashHands(); // Set that hero has an object in he's hands
                    ShowNotification(false);
                    PlayWashingHandsSound();
                    StartCoroutine(ShowNotificationEducation());
                }
            }
            else if (!waterValveOn)
            {
                if (Input.GetKeyDown(KeyCode.L) && isWorking)
                {
                    waterValveOn = true;
                    PlayTurnWaterOnSound();
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    hero.LoseHP();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    hero.LoseHP();
                }
            }
        }
    }

    /// <summary>
    /// Function to get if sink is close enought to wash hands
    /// </summary>
    /// <returns></returns>
    bool SinkIsCloseEnough()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position); // Distance between object and hero
        direction = transform.position - Camera.main.transform.position; // Direction between object and hero
        angleView = Vector3.Angle(Camera.main.transform.forward, direction); // Angle view between object and hero

        if (distance < 2.5f && angleView < 45f) // If distance and angle view is in range, return that it is possible to grab this object
        {
            return true;
        }
        else
        {
            ShowNotification(false);
            return false;
        }
    }

    /// <summary>
    /// Function to show hands washing notification
    /// </summary>
    /// <param name="enabled"></param>
    void ShowNotification(bool enabled)
    {
        if (enabled)
        {
            washHandsNotification.SetActive(true);
        }
        else
            washHandsNotification.SetActive(false);
    }

    /// <summary>
    /// Function to show washing hands education for x seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowNotificationEducation()
    {
        yield return new WaitForSeconds(2);
        //Print the time of when the function is first called.
        washHandsEducationNotification.SetActive(true);


        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        washHandsEducationNotification.SetActive(false);
    }

    /// <summary>
    /// Function to play washing hands sound
    /// </summary>
    void PlayWashingHandsSound()
    {
        FindObjectOfType<AudioManagerScript>().Play("WashHands"); // Play Button Press Audio
    }

    /// <summary>
    /// Function to play valve opening sound
    /// </summary>
    void PlayTurnWaterOnSound()
    {
        //FindObjectOfType<AudioManager>().Play("SinkWaterOn"); // Play Button Press Audio
    }
}