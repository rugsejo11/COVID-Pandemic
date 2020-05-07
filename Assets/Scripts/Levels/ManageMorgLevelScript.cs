using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMorgLevelScript : MonoBehaviour
{
    #region Variables

    public ManageMorgLevel morgLevel { get; set; }

    [SerializeField] private Animator animator = null; // Animator for switches animations
    private HeroDataScript hero; // Game character

    #endregion

    #region Functions

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

    //Get Set

    public bool HazardousLeverDone { get { return hazardousLeverDone; } set { hazardousLeverDone = value; } }
    public bool FinishLeverDone { get { return finishLeverDone; } set { finishLeverDone = value; } }
    public bool ElectricityLeverDone { get { return electricityLeverDone; } set { electricityLeverDone = value; } }
    public bool ClownLeverDone { get { return clownLeverDone; } set { clownLeverDone = value; } }
    public bool ComplexLeverDone { get { return complexLeverDone; } set { complexLeverDone = value; } }
    public bool ElevatorButtonDone { get { return elevatorButtonDone; } set { elevatorButtonDone = value; } }
    public bool SmallButtonDone { get { return smallButtonDone; } set { smallButtonDone = value; } }
    public bool FinishDetonator { get { return finishDetonator; } set { finishDetonator = value; } }
    public bool SwitcherDone { get { return switcherDone; } set { switcherDone = value; } }

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
    private void OpenDoors(int doorsToOpen)
    {
        switch (doorsToOpen)
        {
            case 1:
                OpenDoors("FirstDoorsOpen");
                PlaySound("first_doors_open", am);
                break;
            case 2:
                OpenDoors("SecondDoorsOpen");
                PlaySound("second_doors_open", am);
                break;
            default:
                Debug.LogError("Doors not found.");
                break;
        }
    }

    /// <summary>
    /// Function to check if first stage is finished
    /// </summary>
    /// <returns></returns>
    public bool FirstStage(bool smallButtonDone, bool hazardousLeverDone, bool switcherDone)
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
    public bool SecondStage(bool clownLeverDone, bool electricityLeverDone, bool elevatorButtonDone)
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
    public bool LastStage(bool finishLeverDone, bool finishDetonator, bool complexLeverDone)
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
        if (currentStage == 1 && FirstStage(smallButtonDone, hazardousLeverDone, switcherDone))
        {
            OpenDoors(currentStage);
            currentStage = 2;
        }
        else if (currentStage == 2 && SecondStage(clownLeverDone, electricityLeverDone, elevatorButtonDone))
        {
            OpenDoors(currentStage);
            currentStage = 3;
        }
        else if (currentStage == 3 && LastStage(finishLeverDone, finishDetonator, complexLeverDone) && WereHeroHandsWashed())
        {
            PlaySound("level_finished", am);
            GoNextScene();
        }
        else if (finishDetonator)
        {
            PlaySound("explosions", am);
            RestartScene();
        }
    }
    public bool WereHeroHandsWashed()
    {
        if (hero.WereHandsWashed())
        {
            return true;
        }
        else { return false; }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart scene
    }

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

    private void OpenDoors(string doors)
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

    public void GoNextScene()
    {
        Debug.Log("Level finished");
        // Go next
    }
}