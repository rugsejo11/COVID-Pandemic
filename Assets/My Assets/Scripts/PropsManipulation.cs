using UnityEngine;

public class PropsManipulation : MonoBehaviour
{
    [HideInInspector]
    public bool isDetonator = false; // E
    public bool isLeve  r = false;
    public bool isButton = false;
    public bool isWheel = false; // L R
    public bool isSwitcherOO = false; // E
    public bool isLeverUD = false; // E

    public bool isButtonSB = false; // E
    public bool isButtonSS = false; // E
    public bool isButtonUDLR = false; // U D L R
    public bool isButtonLR = false; // L R
    public bool isLeverSUD = false; // E
    public bool isLeverLS = false; // E
    public bool isLeverVBLR = false; // E
    public Animator animator;
    public DoorsManipulation dm;


    // NearView()
    float distance;
    float angleView;
    Vector3 direction;


    //[Tooltip("True for rotation like valve (used for ramp/elevator only)")]


    // Start is called before the first frame update
    private void Start()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && NearView())
        {
            if (isDetonator)
            {
                animator.SetTrigger("Detonate");
                //animator.SetBool("Detonate", true);
            }
            else if (isButtonSB)
            {
                //animator.SetBool("Detonate", true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && NearView())
        {
            if (isButton)
            {
                if (animator.GetBool("MoveDown"))
                {
                    animator.SetBool("MoveCenter", true);
                    animator.SetBool("MoveDown", false);
                }
                else if (animator.GetBool("MoveUp"))
                {

                }
                else
                {
                    animator.SetBool("MoveCenter", false);
                    animator.SetBool("MoveUp", true);
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && NearView())
        {
            if (isButton)
            {
                if (animator.GetBool("MoveUp"))
                {
                    animator.SetBool("MoveCenter", true);
                    animator.SetBool("MoveUp", false);
                }
                else if (animator.GetBool("MoveDown"))
                {

                }
                else
                {
                    animator.SetBool("MoveCenter", false);
                    animator.SetBool("MoveDown", true);


                }


            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && NearView())
        {
            if (isButton)
            {
                if (animator.GetBool("MoveRight"))
                {
                    animator.SetBool("MoveICenter", true);
                    animator.SetBool("MoveRight", false);
                }
                else if (animator.GetBool("MoveLeft"))
                {

                }
                else
                {
                    animator.SetBool("MoveICenter", false);
                    animator.SetBool("MoveLeft", true);
                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && NearView())
        {
            if (isButton)
            {

                if (animator.GetBool("MoveLeft"))
                {
                    animator.SetBool("MoveICenter", true);
                    animator.SetBool("MoveLeft", false);
                }
                else if (animator.GetBool("MoveRight"))
                {

                }
                else
                {
                    animator.SetBool("MoveICenter", false);
                    animator.SetBool("MoveRight", true);
                }

            }
        }
        if (animator.GetBool("MoveLeft")&& animator.GetBool("MoveUp"))
            dm.OpenDoors();
    }
    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (angleView < 45f && distance < 2f) return true;
        else return false;
    }

    void OpenDoor()
    {

    }
}
