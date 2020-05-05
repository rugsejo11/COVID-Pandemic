using UnityEngine;

public class ManageLaboratoryLevel : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private HeroInteractive hero; // Game character

    // PossibleToClick() - Function to check if switch is reachable
    private float distance;
    private float angleView;
    private Vector3 direction;

    //Racks
    [SerializeField] private TubesRackScript eastRack = null;  // First rack slot
    [SerializeField] private TubesRackScript southRack = null;  // First rack slot

    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroInteractive>(); // Get hero object
        hero.DirtyHands();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonE();
    }

    void ButtonE()
    {
        if (Input.GetKeyDown(KeyCode.E) && PossibleToClick())
        {
            FindObjectOfType<AudioManager>().Play("hazardous_lever_moving"); // Play Button Press Audio
            if (!animator.GetBool("HazardousLeverDown"))
            {
                animator.SetBool("HazardousLeverDown", true);
            }
            CheckIfStageFinished();
        }
    }

    /// <summary>
    /// Function to check if switch is reachable
    /// </summary>
    /// <returns></returns>
    bool PossibleToClick()
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
            if (southRack.UsedSockets() == 2)
            {
                if (southRack.GetSocket(2).isDesired() && southRack.GetSocket(5).isDesired())
                {
                    return true;
                }
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
            if (eastRack.UsedSockets() == 3)
            {
                if (eastRack.GetSocket(1).isDesired() && eastRack.GetSocket(3).isDesired() && eastRack.GetSocket(6).isDesired())
                {
                    return true;
                }
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
            FindObjectOfType<AudioManager>().Play("level_finished"); // Play Button Press Audio
            Debug.Log("Level finished");
        }
        else
        {
            animator.SetBool("HazardousLeverDown", false);
            hero.LoseHP();
        }
    }
}
