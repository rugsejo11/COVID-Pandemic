using UnityEngine;
using TMPro;

public class TimeManagementScript : MonoBehaviour
{

    private TimeManagement tm;

    [Range(0, 1800)]
    [SerializeField] private float secondsLeft = 0; // Variable holding time left
    [SerializeField] private TMP_Text textBox = null; // Text box showing time left
    private void Awake()
    {
        tm = new TimeManagement();
        tm.SecondsLeft = secondsLeft;
        tm.TextBox = textBox;
    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    private void Update()
    {
        tm.SecondsLeft -= Time.deltaTime;
        tm.RoundedSecondsLeft = Mathf.Round(tm.SecondsLeft);
        tm.TimerString = tm.ReturnTimerString(tm.RoundedSecondsLeft);
        tm.UpdateDigitalTimer(tm.TimerString);

        tm.TimerSound = tm.ReturnTimerSound(tm.RoundedSecondsLeft);
        tm.PlaySound(tm.TimerSound);
    }

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
    private float timerMinutes { get; set; }
    private float timerSeconds { get; set; }

    private AudioManagerScript am = UnityEngine.Object.FindObjectOfType<AudioManagerScript>();

    // Get Set methods
    public float RoundedSecondsLeft { get { return roundedSecondsLeft; } set { roundedSecondsLeft = value; } } 
    public TMP_Text TextBox { get { return textBox; } set { textBox = value; } }
    public float SecondsLeft { get { return secondsLeft; } set { secondsLeft = value; } }
    public string TimerString { get { return timerString; } set { timerString = value; } }
    public string TimerSound { get { return timerSound; } set { timerSound = value; } }


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
