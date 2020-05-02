using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private float secondsLeft = float.MinValue;
    private float minutes = float.MinValue;
    private float seconds = float.MinValue;
    private float roundedTime;
    [SerializeField] private TMP_Text textBox = null;
    [SerializeField] private AudioClip slowTicking = null; // clock slow ticking sound.
    [SerializeField] private AudioClip fastTicking = null; // clock fast ticking sound.
    [SerializeField] private AudioClip explosionSound = null; // clock fast ticking sound.
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            if (!audioSource.isPlaying)
            {
                audioSource.clip = slowTicking;
                audioSource.Play();
            }
        }
        else if (Mathf.Floor(roundedTime) <= 60 && Mathf.Floor(roundedTime) > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = fastTicking;
                audioSource.Play();
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = explosionSound;
                audioSource.Play();
            }
        }
    }
}
