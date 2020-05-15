using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageLabLevelScript : MonoBehaviour
{
    #region Variables

    private ManageLabLevel labLevel;
    private HeroData hero; // Game character

    [SerializeField] private Animator animator = null;  // First rack slot

    [SerializeField] private TubesRackScript southRack = null;  // First rack slot
    [SerializeField] private TubesRackScript eastRack = null;  // First rack slot

    #endregion

    #region Monobehaviour functions

    void Awake()
    {

        hero = FindObjectOfType<HeroData>(); // Get hero object

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

    public HeroData hero { get; set; }
    public TubesRackScript eastRack { get; set; }
    public TubesRackScript southRack { get; set; }
    public Animator animator { get; set; }
    public Transform transform { get; set; }

    private AudioManagerScript am = Object.FindObjectOfType<AudioManagerScript>();
    private ObjectDistanceScript objectDistance = new ObjectDistanceScript();
    private MonoBehaviour mb = Object.FindObjectOfType<MonoBehaviour>();
    private NotificationsScript notifications = new NotificationsScript();

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
    public bool IsSouthRackFinished(TubesRack southRack)
    {
        //for(int i = 1; i<=6; i++)
        //{
        //    Debug.Log(southRack.GetSocket(i));

        //}
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
    public bool IsEastRackFinished(TubesRack eastRack)
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
        if (IsSouthRackFinished(southRack.tubes) && IsEastRackFinished(eastRack.tubes) && hero.WereHandsWashed())
        {
            am.Play("level_finished"); // Play Button Press Audio
            mb.StartCoroutine(notifications.StageStatusChange(1, true, SceneManager.GetActiveScene().buildIndex));
        }
        else
        {
            animator.SetBool("HazardousLeverDown", false);
            hero.LoseHP();
        }
    }

    #endregion
}
