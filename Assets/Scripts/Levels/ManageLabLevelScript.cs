using UnityEngine;

public class ManageLabLevelScript : MonoBehaviour
{
    #region Variables

    public ManageLabLevel labLevel;
    [SerializeField] private Animator animator = null; // animator object to use animation on lever
    private HeroDataScript hero; // Game character

    //Tube racks
    [SerializeField] private TubesRackScript eastRack = null;  // First rack slot
    [SerializeField] private TubesRackScript southRack = null;  // First rack slot

    #endregion

    #region Manage lab level script functions

    void Awake()
    {

        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
        hero.DirtyHands(); // set hero's hands to dirty

        labLevel = new ManageLabLevel();
        labLevel.hero = hero;
        labLevel.hero.DirtyHands();
        labLevel.eastRack = eastRack;
        labLevel.southRack = southRack;
        labLevel.animator = animator;
        labLevel.transform = transform;
    }


    /// <summary>
    /// Function is called every frame
    /// </summary>
    void Update()
    {
        if (labLevel.ButtonPressedOnLever())
        {
            labLevel.CheckIfStageFinished();
        }
    }
}
public class ManageLabLevel
{
    // IsObjectInRange()
    private float distance; // Variable holding distance between hero and item
    private float angleView; // Variable holding angle difference between hero camera and item
    private Vector3 direction; // Varialbe holding hero camera direction

    public HeroDataScript hero { get; set; }
    public TubesRackScript eastRack { get; set; }
    public TubesRackScript southRack { get; set; }
    public Animator animator { get; set; }
    public Transform transform { get; set; }



    private AudioManagerScript am = Object.FindObjectOfType<AudioManagerScript>();



    /// <summary>
    /// Function to check if button e pressed on finish lever
    /// </summary>
    public bool ButtonPressedOnLever()
    {
        distance = GetDistance(transform, Camera.main);
        direction = GetDirection();
        angleView = GetAngleView(direction);

        if (Input.GetKeyDown(KeyCode.E) && IsOjectInRange(distance, angleView))
        {
            am.Play("hazardous_lever_moving"); // Play Button Press Audio
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
    bool IsOjectInRange(float distance, float angleView)
    {
        if (angleView < 45f && distance < 3f)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public float GetDistance(Transform transform, Camera main)
    {
        distance = Vector3.Distance(transform.position, main.transform.position);

        return distance;
    }

    public Vector3 GetDirection()
    {
        direction = transform.position - Camera.main.transform.position;

        return direction;

    }

    public float GetAngleView(Vector3 direction)
    {
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);

        return angleView;

    }

    /// <summary>
    /// Function to check if south rack is finished
    /// </summary>
    /// <returns></returns>
    bool SouthRack(TubesRackScript southRack)
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
    bool EastRack(TubesRackScript eastRack)
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
        if (hero.WereHandsWashed() && SouthRack(southRack) && EastRack(eastRack))
        {
            am.Play("level_finished"); // Play Button Press Audio
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
