using UnityEngine;

public class ManageLabLevelScript : MonoBehaviour
{
    #region Variables

    private ManageLabLevel labLevel;
    [SerializeField] private Animator animator = null; // animator object to use animation on lever
    private HeroDataScript hero; // Game character

    //Tube racks
    [SerializeField] private TubesRack eastRack = null;  // First rack slot
    [SerializeField] private TubesRack southRack = null;  // First rack slot

    #endregion

    #region Monobehaviour functions

    void Awake()
    {

        hero = FindObjectOfType<HeroDataScript>(); // Get hero object

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
        if (labLevel.ButtonPressedOnLever(transform, Camera.main))
        {
            labLevel.CheckIfStageFinished();
        }
    }
    #endregion
}

public class ManageLabLevel
{
    #region Variables

    public HeroDataScript hero { get; set; }
    public TubesRack eastRack { get; set; }
    public TubesRack southRack { get; set; }
    public Animator animator { get; set; }
    public Transform transform { get; set; }

    private AudioManagerScript am = Object.FindObjectOfType<AudioManagerScript>();
    private ObjectDistanceScript objectDistance = new ObjectDistanceScript();

    #endregion

    #region Functions

    /// <summary>
    /// Function to check if button e pressed on finish lever
    /// </summary>
    public bool ButtonPressedOnLever(Transform transform, Camera main)
    {
        if (Input.GetKeyDown(KeyCode.E) && objectDistance.IsObjectInRange(transform.position, main.transform))
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
    /// Function to check if south rack is finished
    /// </summary>
    /// <returns></returns>
    public bool SouthRackFinished(TubesRack southRack)
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
    public bool EastRackFinished(TubesRack eastRack)
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
        if (hero.WereHandsWashed() && SouthRackFinished(southRack) && EastRackFinished(eastRack))
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
