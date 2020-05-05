using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeManagement : MonoBehaviour
{
    [SerializeField] private float secondsLeft = float.MinValue; // Variable holding time left
    private float minutes = float.MinValue; // Variable holding rounded timer minutes
    private float seconds = float.MinValue; // Variable holding rounded timer seconds
    private float roundedTime; // Variable holding rounded time left
    [SerializeField] private TMP_Text textBox = null; // Text box showing time left

    /// <summary>
    /// Function to update every frame.
    /// </summary>
    void Update()
    {
        secondsLeft -= Time.deltaTime;
        roundedTime = Mathf.Round(secondsLeft);
        UpdateDigitalTimer();
        PlaySoundEffects();
    }

    /// <summary>
    /// Function to update timer shown time
    /// </summary>
    void UpdateDigitalTimer()
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
    void PlaySoundEffects()
    {
        if (Mathf.Floor(roundedTime) > 60)
        {
            FindObjectOfType<AudioManager>().Play("ticking_slow"); // Play Button Press Audio
        }
        else if (Mathf.Floor(roundedTime) <= 60 && Mathf.Floor(roundedTime) > 0)
        {
            FindObjectOfType<AudioManager>().Play("ticking_fast"); // Play Button Press Audio
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("explosion"); // Play Button Press Audio
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
