using UnityEngine;

public class UseSwitchScript : MonoBehaviour
{
    //[HideInInspector]
    public bool isHazardousLever = false;
    public bool isFinishLever = false;
    public bool isElectricityLever = false;
    public bool isClownLever = false;
    public bool isComplexLever = false;
    [Space]
    public bool isElevatorButton = false;
    public bool isBigButton = false;
    public bool isSmallButton = false;
    [Space]
    public bool isDetonator = false;
    [Space]
    public bool isSwitch = false;
    [Space]
    public Animator animator;
    public OpenDoorsScript dm;


    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    [HideInInspector]
    private int firstDoors = 1;
    private int secondDoors = 2;


    //[Tooltip("True for rotation like valve (used for ramp/elevator only)")]

    // Update is called once per frame
    private void Update()
    {
        ButtonPressedOnObject();
        CheckIfStageFinished();
    }
    bool ButtonE()
    {
        if (Input.GetKeyDown(KeyCode.E) && NearView())
        {
            if (isDetonator)
            {
                Detonator();
                return true;
            }
            else if (isSmallButton)
            {
                if (animator.GetBool("SmallButtonPressed"))
                    animator.SetBool("SmallButtonPressed", false);
                else
                    animator.SetBool("SmallButtonPressed", true);
                return true;
            }
            else if (isBigButton)
            {
                if (animator.GetBool("BigButtonPressed"))
                    animator.SetBool("BigButtonPressed", false);
                else
                    animator.SetBool("BigButtonPressed", true);

                return true;
            }
            else if (isSwitch)
            {
                if (animator.GetBool("SwitcherOn"))
                    animator.SetBool("SwitcherOn", false);
                else
                    animator.SetBool("SwitcherOn", true);

                return true;
            }

            else if (isElectricityLever)
            {
                if (animator.GetBool("ElectricityLeverDown"))
                    animator.SetBool("ElectricityLeverDown", false);
                else
                    animator.SetBool("ElectricityLeverDown", true);

                return true;
            }
            else if (isHazardousLever)
            {
                if (animator.GetBool("HazardousLeverDown"))
                    animator.SetBool("HazardousLeverDown", false);
                else
                    animator.SetBool("HazardousLeverDown", true);

                return true;
            }
            return false;
        }
        return false;
    }
    bool ButtonUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && NearView())
        {
            if (isComplexLever)
            {
                if (animator.GetBool("ComplexLeverDown"))
                {
                    animator.SetBool("ComplexLeverCenter", true);
                    animator.SetBool("ComplexLeverDown", false);
                    return true;
                }
                else
                {
                    animator.SetBool("ComplexLeverCenter", false);
                    animator.SetBool("ComplexLeverUp", true);
                    return true;
                }
            }
            else if (isClownLever)
            {
                if (animator.GetBool("ClownLeverDown"))
                {
                    animator.SetBool("ClownLeverCenter", true);
                    animator.SetBool("ClownLeverDown", false);
                    return true;
                }
                else
                {
                    animator.SetBool("ClownLeverCenter", false);
                    animator.SetBool("ClownLeverUp", true);
                    return true;
                }
            }
            return false;

        }
        return false;
    }
    bool ButtonDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && NearView())
        {
            if (isComplexLever)
            {
                if (animator.GetBool("ComplexLeverUp"))
                {
                    animator.SetBool("ComplexLeverCenter", true);
                    animator.SetBool("ComplexLeverUp", false);
                    return true;
                }
                else
                {
                    animator.SetBool("ComplexLeverCenter", false);
                    animator.SetBool("ComplexLeverDown", true);
                    return true;
                }
            }
            else if (isClownLever)
            {
                if (animator.GetBool("ClownLeverUp"))
                {
                    animator.SetBool("ClownLeverCenter", true);
                    animator.SetBool("ClownLeverUp", false);
                    return true;
                }
                else
                {
                    animator.SetBool("ClownLeverCenter", false);
                    animator.SetBool("ClownLeverDown", true);
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    bool ButtonLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NearView())
        {
            if (isComplexLever)
            {
                if (animator.GetBool("ComplexIndicatorRight"))
                {
                    animator.SetBool("ComplexIndicatorCenter", true);
                    animator.SetBool("ComplexIndicatorRight", false);
                    return true;
                }
                else
                {
                    animator.SetBool("ComplexIndicatorCenter", false);
                    animator.SetBool("ComplexIndicatorLeft", true);
                    return true;

                }

            }
            else if (isElevatorButton)
            {
                animator.SetBool("ElevatorButtonDown", false);
                animator.SetBool("ElevatorButtonUp", true);
                return true;
            }
            else if (isFinishLever)
            {
                if (animator.GetBool("FinishLeverRight"))
                {
                    animator.SetBool("FinishLeverCenter", true);
                    animator.SetBool("FinishLeverRight", false);
                    return true;
                }
                else
                {
                    animator.SetBool("FinishLeverCenter", false);
                    animator.SetBool("FinishLeverLeft", true);
                    return true;
                }

            }
            return false;
        }
        return false;

    }
    bool ButtonRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && NearView())
        {
            if (isComplexLever)
            {
                if (animator.GetBool("ComplexIndicatorLeft"))
                {
                    animator.SetBool("ComplexIndicatorCenter", true);
                    animator.SetBool("ComplexIndicatorLeft", false);
                    return true;
                }
                else
                {
                    animator.SetBool("ComplexIndicatorCenter", false);
                    animator.SetBool("ComplexIndicatorRight", true);
                    return true;
                }

            }
            else if (isElevatorButton)
            {
                animator.SetBool("ElevatorButtonUp", false);
                animator.SetBool("ElevatorButtonDown", true);
                return true;
            }
            else if (isFinishLever)
            {
                if (animator.GetBool("FinishLeverLeft"))
                {
                    animator.SetBool("FinishLeverCenter", true);
                    animator.SetBool("FinishLeverLeft", false);
                    return true;
                }
                else
                {
                    animator.SetBool("FinishLeverCenter", false);
                    animator.SetBool("FinishLeverRight", true);
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    void ButtonPressedOnObject()
    {
        if (ButtonE()) { }
        else if (ButtonUp()) { }
        else if (ButtonDown()) { }
        else if (ButtonLeft()) { }
        else if (ButtonRight()) { }
    }


    bool NearView() // it is true if you near interactive object
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            return true;
            //if (hit.transform != null)
            //{
            //    //Rigidbody rb;
            //    //if (rb = hit.transform.GetComponent<Rigidbody>())
            //    //{
            //    //    LaunchIntoAir(rb); // Use Force On Rigidbody
            //    //}
            //}

        }
        else return false;


        //distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        //direction = transform.position - Camera.main.transform.position;
        //angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        //if (angleView < 30f && distance < 1.85f) return true;
        //else return false;
    }

    //bool NearView() // it is true if you near interactive object
    //{
    //    distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    //    direction = transform.position - Camera.main.transform.position;
    //    angleView = Vector3.Angle(Camera.main.transform.forward, direction);
    //    if (angleView < 30f && distance < 1.85f) return true;
    //    else return false;
    //}

    /// <summary>
    /// Detonate
    /// </summary>
    void Detonator()
    {
        animator.SetBool("Detonate", true);
    }

    /// <summary>
    /// Function that check if stage is finished
    /// </summary>
    void CheckIfStageFinished()
    {
        if (animator.GetBool("ComplexIndicatorLeft") && animator.GetBool("ComplexLeverUp"))
        {
            if (dm != null)
                dm.OpenDoors(firstDoors);
        }
        if (animator.GetBool("ComplexIndicatorLeft") && animator.GetBool("ComplexLeverUp") && animator.GetBool("SwitcherOn"))
        {
            if (dm != null)
                dm.OpenDoors(secondDoors);
        }
    }
}
