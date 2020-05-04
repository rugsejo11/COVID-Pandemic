using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeManagement : MonoBehaviour
{
    [SerializeField] private float secondsLeft = float.MinValue;
    private float minutes = float.MinValue;
    private float seconds = float.MinValue;
    private float roundedTime;
    [SerializeField] private TMP_Text textBox = null;

    // Update is called once per frame
    void Update()
    {
        secondsLeft -= Time.deltaTime;
        roundedTime = Mathf.Round(secondsLeft);
        UpdateDigitalTimer();
        PlaySoundEffects();
    }

    void UpdateDigitalTimer()
    {
        if (roundedTime >= 0)
        {
            minutes = Mathf.Floor(roundedTime / 60);
            seconds = roundedTime % 60;
            textBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
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
