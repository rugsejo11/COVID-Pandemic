using UnityEngine;

public class PropsManipulation : MonoBehaviour
{
    //[HideInInspector]
    public bool isDetonator = false; // E
    public bool isLever = false;
    public bool isButton = false;
    public bool isHLever = false;
    public bool isLeverPower = false;
    public bool isEButton = false;
    public bool isBButton = false; // E

    public bool isWheel = false; // L R
    public bool isSwitcher = false; // E

    public Animator animator;
    public DoorsManipulation dm;


    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    //[Tooltip("True for rotation like valve (used for ramp/elevator only)")]

    // Update is called once per frame
    private void Update()
    {
        ButtonPressedOnObject();
        CheckIfLevelFinished();
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

            else if (isBButton)
            {
                if (animator.GetBool("BigButtonPressed"))
                    animator.SetBool("BigButtonPressed", false);
                else
                    animator.SetBool("BigButtonPressed", true);

                return true;
            }
            else if (isSwitcher)
            {
                if (animator.GetBool("SwitcherOn"))
                    animator.SetBool("SwitcherOn", false);
                else
                    animator.SetBool("SwitcherOn", true);

                return true;
            }
            else if (isLever)
            {
                if (isLeverPower)
                {
                    if (animator.GetBool("LeverPowerDown"))
                        animator.SetBool("LeverPowerDown", false);
                    else
                        animator.SetBool("LeverPowerDown", true);

                    return true;
                }
                else
                {
                    if (animator.GetBool("HLeverDown"))
                        animator.SetBool("HLeverDown", false);
                    else
                        animator.SetBool("HLeverDown", true);

                    return true;
                }
            }
            return false;
        }
        return false;
    }
    bool ButtonUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && NearView())
        {
            if (isButton)
            {
                if (animator.GetBool("LeverDown"))
                {
                    animator.SetBool("LeverCenter", true);
                    animator.SetBool("LeverDown", false);
                    return true;
                }
                else
                {
                    animator.SetBool("LeverCenter", false);
                    animator.SetBool("LeverUp", true);
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
            if (isButton)
            {
                if (animator.GetBool("LeverUp"))
                {
                    animator.SetBool("LeverCenter", true);
                    animator.SetBool("LeverUp", false);
                    return true;
                }
                else
                {
                    animator.SetBool("LeverCenter", false);
                    animator.SetBool("LeverDown", true);
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
            if (isButton)
            {
                if (animator.GetBool("IndicatorRight"))
                {
                    animator.SetBool("IndicatorCenter", true);
                    animator.SetBool("IndicatorRight", false);
                    return true;
                }
                else
                {
                    animator.SetBool("IndicatorCenter", false);
                    animator.SetBool("IndicatorLeft", true);
                    return true;

                }

            }
            else if(isEButton)
            {
                    animator.SetBool("ButtonSelectedDown", false);
                    animator.SetBool("ButtonSelectedUp", true);
                    return true;
            }
            else if (isLever)
            {
                if (animator.GetBool("LeverRight"))
                {
                    animator.SetBool("LeverCenter", true);
                    animator.SetBool("LeverRight", false);
                    return true;
                }
                else
                {
                    animator.SetBool("LeverCenter", false);
                    animator.SetBool("LeverLeft", true);
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
            if (isButton)
            {

                if (animator.GetBool("IndicatorLeft"))
                {
                    animator.SetBool("IndicatorCenter", true);
                    animator.SetBool("IndicatorLeft", false);
                    return true;
                }
                else
                {
                    animator.SetBool("IndicatorCenter", false);
                    animator.SetBool("IndicatorRight", true);
                    return true;
                }

            }
            else if (isEButton)
            {
                    animator.SetBool("ButtonSelectedUp", false);
                    animator.SetBool("ButtonSelectedDown", true);
                    return true;
            }
            else if (isLever)
            {
                if (animator.GetBool("LeverLeft"))
                {
                    animator.SetBool("LeverCenter", true);
                    animator.SetBool("LeverLeft", false);
                    return true;
                }
                else
                {
                    animator.SetBool("LeverCenter", false);
                    animator.SetBool("LeverRight", true);
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
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (angleView < 30f && distance < 1.85f) return true;
        else return false;
    }
    void Detonator()
    {
        animator.SetBool("Detonate", true);
    }
    void CheckIfLevelFinished()
    {
        if (animator.GetBool("IndicatorLeft") && animator.GetBool("LeverUp"))
            dm.OpenDoors();
    }
}
