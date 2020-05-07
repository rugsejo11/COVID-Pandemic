using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMorgLevelScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private Animator animator = null; // Animator for switches animations
    private HeroDataScript hero; // Game character
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

    #endregion

    #region Functions

    /// <summary>
    /// Function to set lever state as done or not done
    /// </summary>
    /// <param name="LeverName">Lever to set done name</param>
    /// <param name="isDone">bool is lever done or not</param>
    public void SetDone(string LeverName, bool isDone)
    {
        switch (LeverName)
        {
            case "complexLeverDone":
                complexLeverDone = isDone;
                break;
            case "switcherDone":
                switcherDone = isDone;
                break;
            case "clownLeverDone":
                clownLeverDone = isDone;
                break;
            case "elevatorButtonDone":
                elevatorButtonDone = isDone;
                break;
            case "hazardousLeverDone":
                hazardousLeverDone = isDone;
                break;
            case "electricityLeverDone":
                electricityLeverDone = isDone;
                break;
            case "smallButtonDone":
                smallButtonDone = isDone;
                break;
            case "finishLeverDone":
                finishLeverDone = isDone;
                break;
            case "finishDetonator":
                finishDetonator = isDone;
                break;

            default:
                Debug.LogError("Switch " + LeverName + " not found!");
                break;
        }
    }

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    private void Start()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
    }

    /// <summary>
    /// Function to open doors because stage is finished
    /// </summary>
    /// <param name="doorsToOpen">Doors to open number</param>
    private void OpenDoors(int doorsToOpen)
    {
        if (animator != null)
        {
            switch (doorsToOpen)
            {
                case 1:
                    animator.SetBool("FirstDoorsOpen", true);
                    FindObjectOfType<AudioManagerScript>().Play("first_doors_open"); // Play Button Press Audio
                    break;
                case 2:
                    FindObjectOfType<AudioManagerScript>().Play("second_doors_open"); // Play Button Press Audio
                    animator.SetBool("SecondDoorsOpen", true);
                    break;
                default:
                    Debug.LogError("Doors not found.");
                    break;
            }
        }
        else
        {
            Debug.LogError("Animator not found!");
        }
    }

    /// <summary>
    /// Function to check if first stage is finished
    /// </summary>
    /// <returns></returns>
    private bool FirstStage()
    {
        if (smallButtonDone && hazardousLeverDone && switcherDone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Function to check if second stage is finished
    /// </summary>
    /// <returns></returns>
    private bool SecondStage()
    {
        if (clownLeverDone && electricityLeverDone && elevatorButtonDone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Function to check if third stage is finished
    /// </summary>
    /// <returns></returns>
    private bool LastStage()
    {
        if (finishLeverDone && finishDetonator && complexLeverDone && hero.WereHandsWashed())
        {
            return true;
        }
        else if (finishDetonator)
        {
            FindObjectOfType<AudioManagerScript>().Play("explosion"); // Play explosion sound effect
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart scene
        }
        return false;
    }

    /// <summary>
    /// Function to check if morgue stage is finished
    /// </summary>
    public void CheckIfStageFinished()
    {
        if (currentStage == 1 && FirstStage())
        {
            OpenDoors(currentStage);
            currentStage = 2;
        }
        else if (currentStage == 2 && FirstStage() && SecondStage())
        {
            OpenDoors(currentStage);
            currentStage = 3;
        }
        else if (currentStage == 3 && FirstStage() && SecondStage() && LastStage())
        {
            FindObjectOfType<AudioManagerScript>().Play("level_finished"); // Play Button Press Audio
            Debug.Log("Level finished");
        }
    }

    #endregion
}
