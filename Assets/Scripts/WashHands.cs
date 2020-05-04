using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHands : MonoBehaviour
{
    // PossibleToGrabObject()
    private float distance; // Distance between object and hero
    private float angleView; // Angle view between object and hero
    private Vector3 direction; // Direction between object and hero
    [SerializeField] private GameObject washHandsNotification = null;
    [SerializeField] private GameObject washHandsEducationNotification = null;
    private HeroInteractive hero; // Game character

    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroInteractive>(); // Get hero object
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
    }

    /// <summary>
    /// 
    /// </summary>
    void Interaction()
    {
        if (PossibleToGrabObject() && !hero.WereHandsWashed())
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
    }
    bool PossibleToGrabObject()
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
    /// 
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
    /// 
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
    /// 
    /// </summary>
    void PlayWashingHandsSound()
    {
        FindObjectOfType<AudioManager>().Play("WashHands"); // Play Button Press Audio
    }
}