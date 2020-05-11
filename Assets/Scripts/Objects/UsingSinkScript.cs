using UnityEngine;
public class UsingSinkScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject washHandsNotification = null;
    [SerializeField] private GameObject washHandsEducationNotification = null;

    private HeroDataScript hero; // Game character
    [SerializeField] private bool isWorking = true; // Is sink is working
    [SerializeField] private bool waterValveOn = true; // Is sink's water valve is on
    private ObjectDistanceScript objectDistance; // Object Distance
    private AudioManagerScript am; // Audio manager
    private NotificationsScript notifications; // Notifications

    #endregion

    #region Monobehaviour functions

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    void Start()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
        objectDistance = new ObjectDistanceScript();
        am = FindObjectOfType<AudioManagerScript>();
        notifications = new NotificationsScript();
    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    void Update()
    {
        InteractWithHero(transform, Camera.main);
    }

    #endregion

    #region Interaction with Sink Functions

    /// <summary>
    /// Function to interact with sink
    /// </summary>
    void InteractWithHero(Transform transform, Camera main)
    {
        if (objectDistance.IsObjectInRange(transform.position, main.transform) && !hero.WereHandsWashed())
        {
            if (waterValveOn)
            {
                notifications.ShowNotification(true, washHandsNotification);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    hero.WashHands(); // Set that hero has an object in he's hands
                    notifications.ShowNotification(false, washHandsNotification);
                    PlayWashingHandsSound();

                    StartCoroutine(notifications.Wait(2, true, washHandsEducationNotification));
                    StartCoroutine(notifications.Wait(5, false, washHandsEducationNotification));
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
        else
        {
            notifications.ShowNotification(false, washHandsNotification);
        }
    }

    /// <summary>
    /// Function to play washing hands sound
    /// </summary>
    void PlayWashingHandsSound()
    {
        am.Play("WashHands"); // Play Button Press Audio
    }

    /// <summary>
    /// Function to play valve opening sound
    /// </summary>
    void PlayTurnWaterOnSound()
    {
        //am.Play("SinkWaterOn"); // Play Button Press Audio
    }

    #endregion
}