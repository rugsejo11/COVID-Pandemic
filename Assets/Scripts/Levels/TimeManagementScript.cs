using UnityEngine;
using TMPro;

public class TimeManagementScript : MonoBehaviour
{
    #region Variables
    private TimeManagement tm; // Initializing TimeManagement class
    private AudioManagerScript am;

    [Range(0, 1800)]
    [SerializeField] private float secondsLeft = 0; // Variable holding time left
    [SerializeField] private TMP_Text textBox = null; // Text box showing time left

    #endregion

    #region Monobehaviour Functions

    /// <summary>
    /// Function initialize any variables or game state before the game starts
    /// </summary>
    private void Awake()
    {
        am = FindObjectOfType<AudioManagerScript>();

        tm = new TimeManagement();
        tm.am = am;
        tm.SetSecondsLeft(secondsLeft);
        tm.SetTextBox(textBox);
    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    private void Update()
    {
        tm.MinusSecondsLeft(Time.deltaTime);
        tm.SetRoundedSecondsLeft(Mathf.Round(tm.GetSecondsLeft()));
        tm.SetTimerString(tm.ReturnTimerString(tm.GetRoundedSecondsLeft()));
        tm.UpdateDigitalTimer(tm.GetTimerString());

        tm.SetTimerSound(tm.ReturnTimerSound(tm.GetRoundedSecondsLeft()));
        tm.PlaySound(tm.GetTimerSound());
    }

    #endregion
}
public class TimeManagement
{
    #region Variables

    //Variables 
    private float roundedSecondsLeft;
    private TMP_Text textBox;
    private float secondsLeft;
    private string timerString;
    private string timerSound;
    private float timerMinutes;
    private float timerSeconds;

    #endregion

    #region Get Set Function
    public AudioManagerScript am { get; set; }

    // Get Set methods
    public float GetRoundedSecondsLeft() { return roundedSecondsLeft; }
    public void SetRoundedSecondsLeft(float roundedSeconds) { roundedSecondsLeft = roundedSeconds; }
    public TMP_Text SetTextBox(TMP_Text tBox) { return textBox = tBox; }
    public float GetSecondsLeft() { return secondsLeft; }
    public void SetSecondsLeft(float seconds) { secondsLeft = seconds; }
    public void MinusSecondsLeft(float seconds) { secondsLeft -= seconds; }
    public string GetTimerString() { return timerString; }
    public void SetTimerString(string timerStr) { timerString = timerStr; }
    public string GetTimerSound() { return timerSound; }
    public void SetTimerSound(string timerSoun) { timerSound = timerSoun; }

    #endregion

    #region Functions

    /// <summary>
    /// Function to return timer time string
    /// </summary>
    public string ReturnTimerString(float roundedSecondsLeft)
    {
        if (roundedSecondsLeft >= 0)
        {
            timerMinutes = Mathf.Floor(roundedSecondsLeft / 60);
            timerSeconds = roundedSecondsLeft % 60;
            timerString = string.Format("{0:00}:{1:00}", timerMinutes, timerSeconds);
            return timerString;
        }
        else
        {
            timerString = string.Format("Error");
            return timerString;
        }
    }

    /// <summary>
    /// Function to update timer shown time
    /// </summary>
    /// <param name="timerString"></param>
    public void UpdateDigitalTimer(string timerString)
    {
        textBox.text = timerString;
    }

    /// <summary>
    /// Funtion to play time sound effects
    /// </summary>
    public string ReturnTimerSound(float roundedSecondsLeft)
    {
        if (Mathf.Floor(roundedSecondsLeft) > 60) // If more than minute time left
        {
            timerSound = "ticking_slow";
            return timerSound;
        }
        else if (Mathf.Floor(roundedSecondsLeft) <= 60 && Mathf.Floor(roundedSecondsLeft) > 0) // If between 60 and 0 timerSeconds time left
        {
            timerSound = "ticking_fast";
            return timerSound;
        }
        else if (Mathf.Floor(roundedSecondsLeft) == 0) // If run out of time
        {
            timerSound = "explosion";
            return timerSound;
        }
        else
        {
            timerSound = "error";
            return timerSound;
        }
    }

    /// <summary>
    /// Function to play sound effect according to the timer time left
    /// </summary>
    /// <param name="soundEffectName"></param>
    public void PlaySound(string soundEffectName)
    {
        switch (soundEffectName)
        {
            case "ticking_slow":
                am.Play("ticking_slow"); // Play Button Press Audio
                break;
            case "ticking_fast":
                am.Play("ticking_fast"); // Play Button Press Audio
                break;
            case "explosion":
                am.Play("explosion"); // Play Button Press Audio
                RestartLevel();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Function to restart level
    /// </summary>
    private void RestartLevel()
    {
        SceneManageScript.RestartScene();
    }

    #endregion
}
