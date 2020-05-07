using UnityEngine;

public class ManageLabLevelScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private Animator animator = null; // animator object to use animation on lever
    private HeroDataScript hero; // Game character

    // IsObjectInRange()
    private float distance; // Variable holding distance between hero and item
    private float angleView; // Variable holding angle difference between hero camera and item
    private Vector3 direction; // Varialbe holding hero camera direction

    //Tube racks
    [SerializeField] private TubesRackScript eastRack = null;  // First rack slot
    [SerializeField] private TubesRackScript southRack = null;  // First rack slot

    #endregion

    #region Manage lab level script functions

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    void Start()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
        hero.DirtyHands(); // set hero's hands to dirty
    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    void Update()
    {
        if (ButtonPressedOnLever())
        {
            CheckIfStageFinished();
        }
    }

    /// <summary>
    /// Function to check if button e pressed on finish lever
    /// </summary>
    private bool ButtonPressedOnLever()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsOjectInRange())
        {
            FindObjectOfType<AudioManagerScript>().Play("hazardous_lever_moving"); // Play Button Press Audio
            if (!animator.GetBool("HazardousLeverDown"))
            {
                animator.SetBool("HazardousLeverDown", true);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Function to check if switch is reachable
    /// </summary>
    /// <returns></returns>
    bool IsOjectInRange()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);

        if (angleView < 45f && distance < 3f)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Function to check if south rack is finished
    /// </summary>
    /// <returns></returns>
    bool SouthRack()
    {
        if (!southRack.GetSocket(2).IsSocketEmpty() && !southRack.GetSocket(5).IsSocketEmpty())
        {
            if (southRack.GetSocket(2).isDesired() && southRack.GetSocket(5).isDesired() && southRack.UsedSockets() == 2)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Function to check if east rack is finished
    /// </summary>
    /// <returns></returns>
    bool EastRack()
    {
        if (!eastRack.GetSocket(1).IsSocketEmpty() && !eastRack.GetSocket(3).IsSocketEmpty() && !eastRack.GetSocket(6).IsSocketEmpty())
        {
            if (eastRack.GetSocket(1).isDesired() && eastRack.GetSocket(3).isDesired() && eastRack.GetSocket(6).isDesired() && eastRack.UsedSockets() == 3)
            {
                return true;
            }

        }
        return false;
    }

    /// <summary>
    /// Function to check if stage is finished
    /// </summary>
    public void CheckIfStageFinished()
    {
        if (hero.WereHandsWashed() && SouthRack() && EastRack())
        {
            FindObjectOfType<AudioManagerScript>().Play("level_finished"); // Play Button Press Audio
            Debug.Log("Level finished");
        }
        else
        {
            animator.SetBool("HazardousLeverDown", false);
            hero.LoseHP();
        }
    }

    #endregion
}
