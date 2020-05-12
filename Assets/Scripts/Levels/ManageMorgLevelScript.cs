using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMorgLevelScript : MonoBehaviour
{
    #region Variables

    public ManageMorgLevel morgLevel { get; set; } // Initialize ManageMorgLevel Class

    [SerializeField] private Animator animator = null; // Animator for switches animations
    private HeroDataScript hero; // Game character

    #endregion

    #region Monobehaviour Functions

    /// <summary>
    /// Function to initialize any variables or game state before the game starts
    /// </summary>
    void Awake()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object

        morgLevel = new ManageMorgLevel();
        morgLevel.animator = animator;
        morgLevel.hero = hero;
    }

    #endregion
}

public class ManageMorgLevel
{
    #region Variables

    public HeroDataScript hero { get; set; }
    public Animator animator { get; set; }

    private int currentStage = 1; // Variable holding current stage of the level 
    //Variables holding values if switches are done
    //_//Levers
    private bool hazardousLeverDone = false;
    private bool finishLeverDone = false;
    private bool electricityLeverDone = false;
    private bool clownLeverDone = false;
    private bool complexLeverDone = false;
    //_//Buttons
    private bool elevatorButtonDone = false;
    private bool smallButtonDone = false;
    //_//Detonators
    private bool finishDetonator = false;
    //_//Switchers
    private bool switcherDone = false;

    private AudioManagerScript am = Object.FindObjectOfType<AudioManagerScript>();
    private NotificationsScript notifications = new NotificationsScript();
    private MonoBehaviour mb = Object.FindObjectOfType<MonoBehaviour>();

    #endregion

    #region Functions

    /// <summary>
    /// Function to set lever state as done or not done
    /// </summary>
    /// <param name="LeverName">Lever to set done name</param>
    /// <param name="isDone">bool is lever done or not</param>
    public string SetDone(string LeverName, bool isDone)
    {
        switch (LeverName)
        {
            case "complexLeverDone":
                complexLeverDone = isDone;
                return string.Format("complexLeverDone " + isDone);
            case "switcherDone":
                switcherDone = isDone;
                return string.Format("switcherDone " + isDone);
            case "clownLeverDone":
                clownLeverDone = isDone;
                return string.Format("clownLeverDone " + isDone);
            case "elevatorButtonDone":
                elevatorButtonDone = isDone;
                return string.Format("elevatorButtonDone " + isDone);
            case "hazardousLeverDone":
                hazardousLeverDone = isDone;
                return string.Format("hazardousLeverDone " + isDone);
            case "electricityLeverDone":
                electricityLeverDone = isDone;
                return string.Format("electricityLeverDone " + isDone);
            case "smallButtonDone":
                smallButtonDone = isDone;
                return string.Format("smallButtonDone " + isDone);
            case "finishLeverDone":
                finishLeverDone = isDone;
                return string.Format("finishLeverDone " + isDone);
            case "finishDetonator":
                finishDetonator = isDone;
                return string.Format("finishDetonator " + isDone);

            default:
                return string.Format("Error! Lever name or isDone entered wrong!");
        }
    }

    /// <summary>
    /// Function to open doors because stage is finished
    /// </summary>
    /// <param name="doorsToOpen">Doors to open number</param>
    public int OpenDoors(int doorsToOpen)
    {
        int doorsOpened;
        switch (doorsToOpen)
        {
            case 1:
                if (Application.isPlaying)
                {
                    OpenDoorsAnimation("FirstDoorsOpen");
                    PlaySound("first_doors_open", am);
                }
                doorsOpened = doorsToOpen;
                return doorsOpened;
            case 2:
                if (Application.isPlaying)
                {
                    OpenDoorsAnimation("SecondDoorsOpen");
                    PlaySound("second_doors_open", am);
                }
                doorsOpened = doorsToOpen;
                return doorsOpened;
            default:
                if (Application.isPlaying)
                {
                    Debug.LogError("Doors not found.");
                }
                doorsOpened = 0;
                return doorsOpened;
        }
    }

    /// <summary>
    /// Function to check if first stage is finished
    /// </summary>
    /// <returns></returns>
    public bool IsFirstStageFinished(bool smallButtonDone, bool hazardousLeverDone, bool switcherDone)
    {
        if (smallButtonDone && hazardousLeverDone && switcherDone)
        {
            return true;
        }
        else
        {
            currentStage = 1;
            return false;
        }
    }

    /// <summary>
    /// Function to check if second stage is finished
    /// </summary>
    /// <returns></returns>
    public bool IsSecondStageFinished(bool clownLeverDone, bool electricityLeverDone, bool elevatorButtonDone)
    {
        if (clownLeverDone && electricityLeverDone && elevatorButtonDone)
        {
            return true;
        }
        else
        {
            currentStage = 2;
            return false;
        }
    }

    /// <summary>
    /// Function to check if third stage is finished
    /// </summary>
    /// <returns></returns>
    public bool IsLastStageFinished(bool finishLeverDone, bool finishDetonator, bool complexLeverDone)
    {
        if (finishLeverDone && finishDetonator && complexLeverDone)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Function to check if morgue stage is finished
    /// </summary>
    public void CheckIfStageFinished()
    {
        if (currentStage == 1 && IsFirstStageFinished(smallButtonDone, hazardousLeverDone, switcherDone))
        {
            OpenDoors(currentStage);
            currentStage = 2;
        }
        else if (currentStage == 2 && IsSecondStageFinished(clownLeverDone, electricityLeverDone, elevatorButtonDone))
        {
            OpenDoors(currentStage);
            currentStage = 3;
        }
        else if (currentStage == 3 && IsLastStageFinished(finishLeverDone, finishDetonator, complexLeverDone) && hero.WereHandsWashed())
        {
            PlaySound("level_finished", am);
            mb.StartCoroutine(notifications.StageStatusChange(1, true, SceneManager.GetActiveScene().buildIndex));

        }
        else if (finishDetonator)
        {
            PlaySound("explosions", am);
            mb.StartCoroutine(notifications.StageStatusChange(1, false, SceneManager.GetActiveScene().buildIndex));
        }
    }

    /// <summary>
    /// Function to play sound effects
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="am"></param>
    /// <returns></returns>
    public string PlaySound(string soundName, AudioManagerScript am)
    {
        string playedSong;
        switch (soundName)
        {
            case "explosions":
                if (Application.isPlaying)
                {
                    am.Play("explosion"); // Play explosion sound effect
                }
                playedSong = "explosion";
                return playedSong;
            case "level_finished":
                if (Application.isPlaying)
                {
                    am.Play("level_finished"); // Play Button Press Audio
                }
                playedSong = "level_finished";
                return playedSong;
            case "first_doors_open":
                if (Application.isPlaying)
                {
                    am.Play("first_doors_open"); // Play Button Press Audio
                }
                playedSong = "first_doors_open";
                return playedSong;
            case "second_doors_open":
                if (Application.isPlaying)
                {
                    am.Play("second_doors_open"); // Play Button Press Audio
                }
                playedSong = "second_doors_open";
                return playedSong;
            default:
                playedSong = "Sound not found!";
                return playedSong;
        }
    }

    /// <summary>
    /// Function to use animations for opening doors
    /// </summary>
    /// <param name="doors"></param>
    private void OpenDoorsAnimation(string doors)
    {
        if (animator != null)
        {
            switch (doors)
            {
                case "FirstDoorsOpen":
                    animator.SetBool("FirstDoorsOpen", true);
                    break;
                case "SecondDoorsOpen":
                    animator.SetBool("SecondDoorsOpen", true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.LogError("Animator not found!");
        }
    }    
   
    #endregion
}