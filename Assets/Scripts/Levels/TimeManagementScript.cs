using UnityEngine;
using TMPro;


public class TimeManagementScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private float secondsLeft = float.MinValue; // Variable holding time left
    private float minutes = 0; // Variable holding rounded timer minutes
    private float seconds = 1; // Variable holding rounded timer seconds
    private float roundedTime; // Variable holding rounded time left
    [SerializeField] private TMP_Text textBox = null; // Text box showing time left

    #endregion

    #region Functions

    /// <summary>
    /// Function is called every frame
    /// </summary>
    void Update()
    {
        secondsLeft -= Time.deltaTime;
        roundedTime = Mathf.Round(secondsLeft);
        UpdateDigitalTimer(roundedTime);
        PlaySoundEffects();
    }

    /// <summary>
    /// Function to update timer shown time
    /// </summary>
    public void UpdateDigitalTimer(float roundedTime)
    {
        if (roundedTime >= 0)
        {
            minutes = Mathf.Floor(roundedTime / 60);
            seconds = roundedTime % 60;
            textBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    /// <summary>
    /// Funtion to play time sound effects
    /// </summary>
    public void PlaySoundEffects()
    {
        if (Mathf.Floor(roundedTime) > 60) // If more than minute time left
        {
            FindObjectOfType<AudioManagerScript>().Play("ticking_slow"); // Play Button Press Audio
        }
        else if (Mathf.Floor(roundedTime) <= 60 && Mathf.Floor(roundedTime) > 0) // If between 60 and 0 seconds time left
        {
            FindObjectOfType<AudioManagerScript>().Play("ticking_fast"); // Play Button Press Audio
        }
        else // If run out of time
        {
            FindObjectOfType<AudioManagerScript>().Play("explosion"); // Play Button Press Audio
            SceneManageScript.RestartScene();
        }
    }

    #endregion
}
