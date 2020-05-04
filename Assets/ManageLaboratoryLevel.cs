using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLaboratoryLevel : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private HeroInteractive hero; // Game character

    // PossibleToClick() - Function to check if switch is reachable
    private float distance;
    private float angleView;
    private Vector3 direction;

    [SerializeField] private SocketScript Socket = null;  // First rack slot
    [SerializeField] private SocketScript Socket2 = null; // Second rack slot
    [SerializeField] private SocketScript Socket3 = null;
    [SerializeField] private SocketScript Socket4 = null;

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

    public void CheckIfStageFinished()
    {
        if (Socket.tube.name != null)
        {
            if (Socket.tube.name == "Green10Cylinder" && hero.WereHandsWashed())
            {
                FindObjectOfType<AudioManager>().Play("level_finished"); // Play Button Press Audio
                Debug.Log("Level finished");
            }
            else
            {
                hero.LoseHP();
            }
        }
        else
        {
            hero.LoseHP();
        }
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
            else
            {
                animator.SetBool("HazardousLeverDown", false);
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
}
