using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    #region Variables

    // Character movement characteristics
    [Tooltip("Character movement speed")]
    [SerializeField] private float movementSpeed = 3f; // Variable holding value how quick character will go
    [Tooltip("Character runing speed")]
    [SerializeField] private float runSpeed = 5f; // Variable holding value how quick character will run
    [Tooltip("Character jumping force")]
    [SerializeField] private float jumpingForce = 300f; // Variable holding value how high character will jump
    [Tooltip("Mouse sensitivity")]
    [SerializeField] private float mouseSens = 70f; // Variable holding value sensitivity of the mouse
    private float currentMovementSpeed = 0; // Variable holding current movement speed of the character
    private bool onGround = true; // Variable holding value if player is on the ground
    private bool inAir = false; // Variable holding value if player is in the air

    // Hero and hero's camera
    private Rigidbody characterBody = null; // Character body
    private Transform Cam; // Hero's camera
    private float camYRotation; // Camera up or down rotation

    //Sound
    [SerializeField] private float stepsToMakeNoise = 10; // Variable holding value how many steps character has to make to make sound
    private float stepsMade = 0; // Variable holding value how many steps character has made
    private float nextNoiseAtStep = 0; // Variable holding value at which step to make noise

    #endregion

    #region Start, Update, FixedUpdate functions

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    private void Start()
    {
        characterBody = GetComponent<Rigidbody>();
        Cam = Camera.main.GetComponent<Transform>();
        nextNoiseAtStep = stepsMade / 2f;
        SetCursorData();

    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    private void Update()
    {
        CameraRotation(); // Function to rotate hero's camera according to mouse movement 
        Jump(); // Function to let character jump if space button pressed and he is on the grouund
    }

    /// <summary>
    /// Function is called every fixed frame-rate frame
    /// </summary>
    private void FixedUpdate()
    {
        Run(); // Function to let character run if Left or Right Shift being hold
        CharacterMove(); // Function to move character according to the keyboard buttons pressed 
    }

    #endregion

    #region Camera, mouse, character movement functions
    /// <summary>
    /// Function to set cursor data
    /// </summary>
    private void SetCursorData()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor on the center of the game screen
        Cursor.visible = false; // make cursor invisible
    }

    /// <summary>
    /// Function to move hero according to mouse movement 
    /// </summary>
    private void CameraRotation()
    {
        float xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens; // Get Mouse X axis movement
        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens; // Get Mouse Y axis movement
        transform.Rotate(Vector3.up * xmouse);
        camYRotation -= ymouse;
        camYRotation = Mathf.Clamp(camYRotation, -85f, 60f);
        Cam.localRotation = Quaternion.Euler(camYRotation, 0, 0);
    }

    /// <summary>
    /// Function to let character jump if space button pressed and he is on the ground
    /// </summary>
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && inAir == false)
        {
            characterBody.AddForce(transform.up * jumpingForce);
            PlayJumpSound();
            onGround = false;
        }

    }

    /// <summary>
    /// Function to know when character lands
    /// </summary>
    private void Land()
    {
        PlayLandingSound();
        inAir = false;
        nextNoiseAtStep = stepsMade + 1f;
    }

    /// <summary>
    /// Function to let character run if Left or Right Shift being hold
    /// </summary>
    private void Run()
    {
        if (Input.GetButton("Run"))
            currentMovementSpeed = runSpeed;
        else
            currentMovementSpeed = movementSpeed;
    }

    /// <summary>
    /// Function to move character according to the keyboard buttons pressed 
    /// </summary>
    private void CharacterMove()
    {
        Vector3 verticalVector; // Vector holding character vertical movement
        Vector3 horizontalVector; // Vector holding character horizontal movement
        Vector3 jumpVector; // Vector holding character jump movement
        Vector3 moveVector; // Vector holding vertical, horizontal and jump movement vectors

        verticalVector = transform.forward * currentMovementSpeed * Input.GetAxis("Vertical"); // Vertical character movement vector
        horizontalVector = transform.right * currentMovementSpeed * Input.GetAxis("Horizontal"); // horizontal character movement vector
        jumpVector = transform.up * characterBody.velocity.y; // jump character movement vector

        moveVector = verticalVector + horizontalVector + jumpVector; // character movement vector

        characterBody.velocity = moveVector; // set character to move

        if (verticalVector.magnitude > 0 || horizontalVector.magnitude > 0)
            ProgressStepCycle();
    }

    #endregion

    #region Sound

    /// <summary>
    /// Function to keep track of steps character made to know when to play walking sound
    /// </summary>
    private void ProgressStepCycle()
    {
        stepsMade += (characterBody.velocity.magnitude + (currentMovementSpeed * 3)) *
                 Time.fixedDeltaTime; // Get number of steps made according to the current movement speed and hero velocity

        if (!(stepsMade > nextNoiseAtStep))
        {
            return;
        }

        // If required number steps to make sound is reached, get next value when to make noise and make footstep noise
        nextNoiseAtStep = stepsMade + stepsToMakeNoise;

        PlayFootStepAudio();
    }

    /// <summary>
    /// Function to play jump sound
    /// </summary>
    private void PlayJumpSound()
    {
        FindObjectOfType<AudioManagerScript>().Play("Jump"); // Play Button Press Audio
    }

    /// <summary>
    /// Function to play landing sound
    /// </summary>
    private void PlayLandingSound()
    {
        FindObjectOfType<AudioManagerScript>().Play("Land"); // Play Button Press Audio
    }

    /// <summary>
    /// Function to play footstep sound
    /// </summary>
    private void PlayFootStepAudio()
    {
        FindObjectOfType<AudioManagerScript>().PlayFootstep(); // Play Button Press Audio
    }

    #endregion

    #region on trigger with ground

    /// <summary>
    /// Function on trigger with ground set that hero is on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (!onGround && inAir)
        {
            if (other.CompareTag("Ground"))
            {
                Land();
                onGround = true;
            }
            else
                Land();
        }
        inAir = false;
    }

    /// <summary>
    /// Function on trigger leaving the ground set that hero is not on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        inAir = true;
    }

    #endregion
}