using UnityEngine;

public class SwitchInteractionScript : MonoBehaviour
{
    #region Variables

    //Switches
    [SerializeField] private bool isHazardousLever = false;
    [SerializeField] private bool isFinishLever = false;
    [SerializeField] private bool isElectricityLever = false;
    [SerializeField] private bool isClownLever = false;
    [SerializeField] private bool isComplexLever = false;
    [Space]
    [SerializeField] private bool isElevatorButton = false;
    [SerializeField] private bool isBigButton = false;
    [SerializeField] private bool isSmallButton = false;
    [Space]
    [SerializeField] private bool isDummyDetonate = false;
    [SerializeField] private bool isFinishDetonate = false;
    [Space]
    [SerializeField] private bool isSwitch = false;
    [Space]


    [SerializeField] private Animator animator = null; // Animator for switches animations
    private HeroDataScript hero; // Game character

    private ManageMorgLevelScript morgLevelScript = null; // Morgue level manager script
    private ManageMorgLevel morgLevel; // Morgue level manager

    // IsObjectInRange()
    private float distance; // Variable holding distance between hero and item
    private float angleView; // Variable holding angle difference between hero camera and item
    private Vector3 direction; // Varialbe holding hero camera direction

    #endregion

    #region Essential functions
    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    private void Start()
    {
        morgLevelScript = FindObjectOfType<ManageMorgLevelScript>(); // Get current socket
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
        morgLevel = morgLevelScript.morgLevel;

    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    private void Update()
    {
        ButtonPressed();
    }

    /// <summary>
    /// Function to check if button pressed on switch
    /// </summary>
    void ButtonPressed()
    {
        if (ButtonEPressed()) { } // Check if button E was pressed on an switch
        else if (ButtonUpPressed()) { } // Check if button arrow up was pressed on an switch
        else if (ButtonDownPressed()) { } // Check if button arrow down was pressed on an switch
        else if (ButtonLeftPressed()) { } // Check if button arrow left was pressed on an switch
        else if (ButtonRightPressed()) { } // Check if button arrow right was pressed on an switch
    }

    /// <summary>
    /// Function to check if switch is reachable
    /// </summary>
    /// <returns></returns>
    bool IsObjectInRange()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);

        if (angleView < 45f && distance < 3f)
            return true;
        else
            return false;
    }

    #endregion

    #region On button pressed activities

    /// <summary>
    /// Function if Button E pressed to manipulate switch
    /// </summary>
    /// <returns></returns>
    bool ButtonEPressed()
    {
        // If Button E pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.E) && IsObjectInRange())
        {
            //Check which switch to manipulate
            if (isDummyDetonate)
            {
                DummyDetonate(); return true;
            }
            if (isFinishDetonate)
            {
                FinishDetonate(); return true;
            }
            else if (isSmallButton)
            {
                SmallButton(KeyCode.E); return true;
            }
            else if (isBigButton)
            {
                BigButton(KeyCode.E); return true;
            }
            else if (isSwitch)
            {
                Switch(KeyCode.E); return true;
            }

            else if (isElectricityLever)
            {
                ElectricityLever(KeyCode.E); return true;
            }
            else if (isHazardousLever)
            {
                HazardousLever(KeyCode.E); return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Function if Button Up pressed to manipulate switch
    /// </summary>
    /// <returns></returns>
    bool ButtonUpPressed()
    {
        // If button arrow up pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsObjectInRange())
        {
            //Check which switch to manipulate
            if (isComplexLever)
            {
                ComplexLever(KeyCode.UpArrow); return true;
            }
            else if (isClownLever)
            {
                ClownLever(KeyCode.UpArrow); return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Function if Button Down pressed to manipulate switch
    /// </summary>
    /// <returns></returns>
    bool ButtonDownPressed()
    {
        // If Button arrow down pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.DownArrow) && IsObjectInRange())
        {
            //Check which switch to manipulate
            if (isComplexLever)
            {
                ComplexLever(KeyCode.DownArrow); return true;
            }
            else if (isClownLever)
            {
                ClownLever(KeyCode.DownArrow); return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Function if Button Left pressed to manipulate switch
    /// </summary>
    /// <returns></returns>
    bool ButtonLeftPressed()
    {
        // If Button arrow left pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.LeftArrow) && IsObjectInRange())
        {
            //Check which switch to manipulate
            if (isComplexLever)
            {
                ComplexLever(KeyCode.LeftArrow); return true;
            }
            else if (isElevatorButton)
            {
                Elevatorbutton(KeyCode.LeftArrow); return true;
            }
            else if (isFinishLever)
            {
                FinishLever(KeyCode.LeftArrow); return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Function if Button Right pressed to manipulate switch
    /// </summary>
    /// <returns></returns>
    bool ButtonRightPressed()
    {
        // If Button arrow right pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.RightArrow) && IsObjectInRange())
        {
            //Check which switch to manipulate
            if (isComplexLever)
            {
                ComplexLever(KeyCode.RightArrow); return true;
            }
            else if (isElevatorButton)
            {
                Elevatorbutton(KeyCode.RightArrow); return true;
            }
            else if (isFinishLever)
            {
                FinishLever(KeyCode.RightArrow); return true;
            }
            return false;
        }
        return false;
    }
    #endregion

    #region Switches activities, animations

    /// <summary>
    /// Set Hazardous lever animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void HazardousLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManagerScript>().Play("hazardous_lever_moving"); // Play Button Press Audio
                if (!animator.GetBool("HazardousLeverDown"))
                {
                    animator.SetBool("HazardousLeverDown", true);
                    morgLevel.SetDone("hazardousLeverDone", true);
                    morgLevel.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("HazardousLeverDown", false);
                    morgLevel.SetDone("hazardousLeverDone", false);
                    hero.LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Electricity lever animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void ElectricityLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManagerScript>().Play("electricity_noise"); // Play Button Press Audio
                if (!animator.GetBool("ElectricityLeverDown"))
                {
                    animator.SetBool("ElectricityLeverDown", true);
                    morgLevel.SetDone("electricityLeverDone", true);
                    morgLevel.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("ElectricityLeverDown", false);
                    morgLevel.SetDone("electricityLeverDone", false);
                    hero.LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Big button animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void BigButton(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManagerScript>().Play("game_button_clicked"); // Play Button Press Audio
                if (animator.GetBool("BigButtonPressed"))
                {
                    animator.SetBool("BigButtonPressed", false);
                    hero.LoseHP();
                }
                else
                {
                    animator.SetBool("BigButtonPressed", true);
                    hero.LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Small button animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void SmallButton(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManagerScript>().Play("game_button_clicked"); // Play Button Press Audio
                if (!animator.GetBool("SmallButtonPressed"))
                {
                    animator.SetBool("SmallButtonPressed", true);
                    morgLevel.SetDone("smallButtonDone", true);
                    morgLevel.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("SmallButtonPressed", false);
                    morgLevel.SetDone("smallButtonDone", false);
                    hero.LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Switch animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void Switch(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManagerScript>().Play("switch_clicked"); // Play Button Press Audio
                if (!animator.GetBool("SwitcherOn"))
                {
                    animator.SetBool("SwitcherOn", true);
                    morgLevel.SetDone("switcherDone", true);
                    morgLevel.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("SwitcherOn", false);
                    morgLevel.SetDone("switcherDone", false);
                    hero.LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Clown lever animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void ClownLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.UpArrow:
                FindObjectOfType<AudioManagerScript>().Play("lever_pushed"); // Play Button Press Audio
                hero.LoseHP();
                morgLevel.SetDone("clownLeverDone", false);
                if (animator.GetBool("ClownLeverDown"))
                {
                    animator.SetBool("ClownLeverCenter", true);
                    animator.SetBool("ClownLeverDown", false);
                }
                else
                {
                    animator.SetBool("ClownLeverCenter", false);
                    animator.SetBool("ClownLeverUp", true);
                }
                break;
            case KeyCode.DownArrow:
                FindObjectOfType<AudioManagerScript>().Play("lever_pushed"); // Play Button Press Audio
                if (animator.GetBool("ClownLeverUp"))
                {
                    animator.SetBool("ClownLeverCenter", true);
                    animator.SetBool("ClownLeverUp", false);
                    morgLevel.SetDone("clownLeverDone", false);
                }
                else
                {
                    animator.SetBool("ClownLeverCenter", false);
                    animator.SetBool("ClownLeverDown", true);
                    morgLevel.SetDone("clownLeverDone", true);
                    morgLevel.CheckIfStageFinished();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Finish lever animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void FinishLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                hero.LoseHP();
                morgLevel.SetDone("finishLeverDone", false);
                if (animator.GetBool("FinishLeverRight"))
                {
                    animator.SetBool("FinishLeverCenter", true);
                    animator.SetBool("FinishLeverRight", false);
                }
                else
                {
                    animator.SetBool("FinishLeverCenter", false);
                    animator.SetBool("FinishLeverLeft", true);
                }
                break;
            case KeyCode.RightArrow:
                if (animator.GetBool("FinishLeverLeft"))
                {
                    animator.SetBool("FinishLeverCenter", true);
                    animator.SetBool("FinishLeverLeft", false);
                    morgLevel.SetDone("finishLeverDone", false);
                }
                else
                {
                    animator.SetBool("FinishLeverCenter", false);
                    animator.SetBool("FinishLeverRight", true);

                    morgLevel.SetDone("finishLeverDone", true);
                    morgLevel.CheckIfStageFinished();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set DummyDetonate object animation and sound
    /// </summary>
    void DummyDetonate()
    {
        animator.SetBool("DummyDetonate", true);
        FindObjectOfType<AudioManagerScript>().Play("siren_alarm"); // Play Button Press Audio
        hero.LoseHP();
    }

    /// <summary>
    /// Set FinishDetonate object animation and sound
    /// </summary>
    void FinishDetonate()
    {
        animator.SetBool("FinishDetonate", true);
        morgLevel.SetDone("finishDetonator", true);
        morgLevel.CheckIfStageFinished();
    }

    /// <summary>
    /// Set Elevator button animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void Elevatorbutton(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                FindObjectOfType<AudioManagerScript>().Play("game_button_clicked"); // Play Button Press Audio
                animator.SetBool("ElevatorButtonDown", false);
                animator.SetBool("ElevatorButtonUp", true);
                morgLevel.SetDone("elevatorButtonDone", true);
                morgLevel.CheckIfStageFinished();
                break;
            case KeyCode.RightArrow:
                FindObjectOfType<AudioManagerScript>().Play("game_button_clicked"); // Play Button Press Audio
                animator.SetBool("ElevatorButtonUp", false);
                animator.SetBool("ElevatorButtonDown", true);
                morgLevel.SetDone("elevatorButtonDone", false);
                hero.LoseHP();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Complex lever animation and sound
    /// </summary>
    /// <param name="keyCode">Keyboard button</param>
    void ComplexLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.UpArrow:
                FindObjectOfType<AudioManagerScript>().Play("lever_pushed"); // Play Button Press Audio
                if (animator.GetBool("ComplexLeverDown"))
                {
                    animator.SetBool("ComplexLeverCenter", true);
                    animator.SetBool("ComplexLeverDown", false);
                    morgLevel.SetDone("complexLeverDone", false);
                }
                else
                {
                    animator.SetBool("ComplexLeverCenter", false);
                    animator.SetBool("ComplexLeverUp", true);

                    if (animator.GetBool("ComplexIndicatorLeft") && animator.GetBool("ComplexLeverUp"))
                    {
                        morgLevel.SetDone("complexLeverDone", true);
                        morgLevel.CheckIfStageFinished();
                    }
                }
                break;
            case KeyCode.DownArrow:
                FindObjectOfType<AudioManagerScript>().Play("lever_pushed"); // Play Button Press Audio
                hero.LoseHP();
                morgLevel.SetDone("complexLeverDone", false);
                if (animator.GetBool("ComplexLeverUp"))
                {
                    animator.SetBool("ComplexLeverCenter", true);
                    animator.SetBool("ComplexLeverUp", false);
                }
                else
                {
                    animator.SetBool("ComplexLeverCenter", false);
                    animator.SetBool("ComplexLeverDown", true);
                }
                break;
            case KeyCode.LeftArrow:
                FindObjectOfType<AudioManagerScript>().Play("indicator_move"); // Play Button Press Audio
                if (animator.GetBool("ComplexIndicatorRight"))
                {
                    animator.SetBool("ComplexIndicatorCenter", true);
                    animator.SetBool("ComplexIndicatorRight", false);
                    morgLevel.SetDone("complexLeverDone", false);
                }
                else
                {
                    animator.SetBool("ComplexIndicatorCenter", false);
                    animator.SetBool("ComplexIndicatorLeft", true);
                    if (animator.GetBool("ComplexIndicatorLeft") && animator.GetBool("ComplexLeverUp"))
                    {
                        morgLevel.SetDone("complexLeverDone", true);
                        morgLevel.CheckIfStageFinished();
                    }
                }
                break;
            case KeyCode.RightArrow:
                FindObjectOfType<AudioManagerScript>().Play("indicator_move"); // Play Button Press Audio
                hero.LoseHP();
                morgLevel.SetDone("complexLeverDone", false);
                if (animator.GetBool("ComplexIndicatorLeft"))
                {
                    animator.SetBool("ComplexIndicatorCenter", true);
                    animator.SetBool("ComplexIndicatorLeft", false);
                }
                else
                {
                    animator.SetBool("ComplexIndicatorCenter", false);
                    animator.SetBool("ComplexIndicatorRight", true);
                }
                break;

            default:
                break;
        }
    }

    #endregion
}