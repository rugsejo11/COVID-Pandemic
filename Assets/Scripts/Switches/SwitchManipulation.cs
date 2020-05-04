using UnityEngine;

public class SwitchManipulation : MonoBehaviour
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


    [SerializeField] private HeroInteractive hero; // Game character
    [SerializeField] private Animator animator = null;
    [SerializeField] private ManageMorgueLevel mml = null;

    // PossibleToClick() - Function to check if switch is reachable
    private float distance;
    private float angleView;
    private Vector3 direction;

    #endregion

    private void Start()
    {
        mml = FindObjectOfType<ManageMorgueLevel>(); // Get current socket
        hero = FindObjectOfType<HeroInteractive>(); // Get hero object

    }
    // Update is called once per frame
    private void Update()
    {
        WhichButtonPressed();
    }

    /// <summary>
    /// Function to check if button pressed on switch
    /// </summary>
    void WhichButtonPressed()
    {
        if (ButtonE()) { } // Check if button E was pressed on an switch
        else if (ButtonUp()) { } // Check if button arrow up was pressed on an switch
        else if (ButtonDown()) { } // Check if button arrow down was pressed on an switch
        else if (ButtonLeft()) { } // Check if button arrow left was pressed on an switch
        else if (ButtonRight()) { } // Check if button arrow right was pressed on an switch
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

    #region Button pressed activity

    /// <summary>
    /// Function if Button E pressed to manipulate switch
    /// </summary>
    /// <returns></returns>
    bool ButtonE()
    {
        // If Button E pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.E) && PossibleToClick())
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
    bool ButtonUp()
    {        
        // If button arrow up pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.UpArrow) && PossibleToClick())
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
    bool ButtonDown()
    {        
        // If Button arrow down pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.DownArrow) && PossibleToClick())
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
    bool ButtonLeft()
    {
        // If Button arrow left pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.LeftArrow) && PossibleToClick())
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
    bool ButtonRight()
    {
        // If Button arrow right pressed and object is near enough to be clickable
        if (Input.GetKeyDown(KeyCode.RightArrow) && PossibleToClick())
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

    #region Switches activity

    /// <summary>
    /// Set Hazardous lever animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void HazardousLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManager>().Play("hazardous_lever_moving"); // Play Button Press Audio
                if (!animator.GetBool("HazardousLeverDown"))
                {
                    animator.SetBool("HazardousLeverDown", true);
                    mml.hazardousLeverDone = true;
                    mml.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("HazardousLeverDown", false);
                    LoseHP();
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
                FindObjectOfType<AudioManager>().Play("electricity_noise"); // Play Button Press Audio
                if (!animator.GetBool("ElectricityLeverDown"))
                {
                    animator.SetBool("ElectricityLeverDown", true);
                    mml.electricityLeverDone = true;
                    mml.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("ElectricityLeverDown", false);
                    LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Big button animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void BigButton(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManager>().Play("game_button_clicked"); // Play Button Press Audio
                if (animator.GetBool("BigButtonPressed"))
                {
                    animator.SetBool("BigButtonPressed", false);
                    LoseHP();
                }
                else
                {
                    animator.SetBool("BigButtonPressed", true);
                    LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Small button animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void SmallButton(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManager>().Play("game_button_clicked"); // Play Button Press Audio
                if (!animator.GetBool("SmallButtonPressed"))
                {
                    animator.SetBool("SmallButtonPressed", true);
                    mml.smallButtonDone = true;
                    mml.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("SmallButtonPressed", false);
                    LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Switch animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void Switch(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                FindObjectOfType<AudioManager>().Play("switch_clicked"); // Play Button Press Audio
                if (!animator.GetBool("SwitcherOn"))
                {
                    animator.SetBool("SwitcherOn", true);
                    mml.switcherDone = true;
                    mml.CheckIfStageFinished();
                }
                else
                {
                    animator.SetBool("SwitcherOn", false);
                    LoseHP();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Clown lever animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void ClownLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.UpArrow:
                FindObjectOfType<AudioManager>().Play("lever_pushed"); // Play Button Press Audio
                LoseHP();
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
                FindObjectOfType<AudioManager>().Play("lever_pushed"); // Play Button Press Audio
                if (animator.GetBool("ClownLeverUp"))
                {
                    animator.SetBool("ClownLeverCenter", true);
                    animator.SetBool("ClownLeverUp", false);
                }
                else
                {
                    animator.SetBool("ClownLeverCenter", false);
                    animator.SetBool("ClownLeverDown", true);
                    mml.clownLeverDone = true;
                    mml.CheckIfStageFinished();
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Finish lever animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void FinishLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                LoseHP();
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
                }
                else
                {
                    animator.SetBool("FinishLeverCenter", false);
                    animator.SetBool("FinishLeverRight", true);

                    mml.finishLeverDone = true;
                    mml.CheckIfStageFinished();
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
        FindObjectOfType<AudioManager>().Play("siren_alarm"); // Play Button Press Audio
        hero.LoseHP();
    }

    /// <summary>
    /// Set FinishDetonate object animation and sound
    /// </summary>
    void FinishDetonate()
    {
        animator.SetBool("FinishDetonate", true);
        mml.finishDetonator = true;
        mml.CheckIfStageFinished();
    }

    /// <summary>
    /// Set Elevator button animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void Elevatorbutton(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                FindObjectOfType<AudioManager>().Play("game_button_clicked"); // Play Button Press Audio
                animator.SetBool("ElevatorButtonDown", false);
                animator.SetBool("ElevatorButtonUp", true);
                mml.elevatorButtonDone = true;
                mml.CheckIfStageFinished();
                break;
            case KeyCode.RightArrow:
                FindObjectOfType<AudioManager>().Play("game_button_clicked"); // Play Button Press Audio
                animator.SetBool("ElevatorButtonUp", false);
                animator.SetBool("ElevatorButtonDown", true);
                LoseHP();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set Complex lever animation and sound
    /// </summary>
    /// <param name="keyCode"></param>
    void ComplexLever(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.UpArrow:
                FindObjectOfType<AudioManager>().Play("lever_pushed"); // Play Button Press Audio
                if (animator.GetBool("ComplexLeverDown"))
                {
                    animator.SetBool("ComplexLeverCenter", true);
                    animator.SetBool("ComplexLeverDown", false);
                }
                else
                {
                    animator.SetBool("ComplexLeverCenter", false);
                    animator.SetBool("ComplexLeverUp", true);

                    if (animator.GetBool("ComplexIndicatorLeft") && animator.GetBool("ComplexLeverUp"))
                    {
                        mml.complexLeverDone = true;
                        mml.CheckIfStageFinished();
                    }
                }
                break;
            case KeyCode.DownArrow:
                FindObjectOfType<AudioManager>().Play("lever_pushed"); // Play Button Press Audio
                LoseHP();
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
                FindObjectOfType<AudioManager>().Play("indicator_move"); // Play Button Press Audio
                if (animator.GetBool("ComplexIndicatorRight"))
                {
                    animator.SetBool("ComplexIndicatorCenter", true);
                    animator.SetBool("ComplexIndicatorRight", false);
                }
                else
                {
                    animator.SetBool("ComplexIndicatorCenter", false);
                    animator.SetBool("ComplexIndicatorLeft", true);

                    if (animator.GetBool("ComplexIndicatorLeft") && animator.GetBool("ComplexLeverUp"))
                    {
                        mml.complexLeverDone = true;
                        mml.CheckIfStageFinished();
                    }
                }
                break;
            case KeyCode.RightArrow:
                FindObjectOfType<AudioManager>().Play("indicator_move"); // Play Button Press Audio
                LoseHP();
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

    void LoseHP()
    {
        hero.LoseHP();
    }
}
