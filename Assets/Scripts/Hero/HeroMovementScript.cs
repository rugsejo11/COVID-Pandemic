using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    #region Variables

    // Character movement characteristics
    [Tooltip("Character movement speed")]
    [Range(1, 5)]
    [SerializeField] private float movementSpeed = 3f; // Variable holding value how quick character will go
    [Tooltip("Character runing speed")]
    [Range(2, 10)]
    [SerializeField] private float runSpeed = 5f; // Variable holding value how quick character will run
    [Tooltip("Character jumping force")]
    [Range(100, 600)]
    [SerializeField] private float jumpingForce = 300f; // Variable holding value how high character will jump
    [Tooltip("Mouse sensitivity")]
    [Range(50, 500)]
    [SerializeField] private float mouseSens = 70f; // Variable holding value sensitivity of the mouse

    //Sound
    [SerializeField] private float stepsToMakeNoise = 10; // Variable holding value how many steps character has to make to make sound

    // Hero and hero's camera
    private Rigidbody characterBody = null; // Character body
    private Transform Cam; // Hero's camera
    private HeroMovement heroMovement; // Initializing hero movement class
    private AudioManagerScript am; // Initializing audiomanager

    #endregion

    #region Monobehaviour Functions

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    private void Start()
    {
        characterBody = GetComponent<Rigidbody>();
        Cam = Camera.main.GetComponent<Transform>();
        am = FindObjectOfType<AudioManagerScript>();

        heroMovement = new HeroMovement();
        heroMovement.stepsToMakeNoise = stepsToMakeNoise;
        heroMovement.movementSpeed = movementSpeed;
        heroMovement.runSpeed = runSpeed;
        heroMovement.jumpingForce = jumpingForce;
        heroMovement.mouseSens = mouseSens;
        heroMovement.characterBody = characterBody;
        heroMovement.Cam = Cam;
        heroMovement.am = am;

        heroMovement.SetCursorData();
    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    private void Update()
    {
        heroMovement.CameraRotation(); // Function to rotate hero's camera according to mouse movement 
        heroMovement.Jump(); // Function to let character jump if space button pressed and he is on the grouund
    }

    /// <summary>
    /// Function is called every fixed frame-rate frame
    /// </summary>
    private void FixedUpdate()
    {
        heroMovement.Run(); // Function to let character run if Left or Right Shift being hold
        heroMovement.CharacterMove(); // Function to move character according to the keyboard buttons pressed 
    }

    /// <summary>
    /// Function on trigger with ground set that hero is on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (!heroMovement.IsOnGround() && heroMovement.IsInAir())
        {
            if (other.CompareTag("Ground"))
            {
                heroMovement.Land();
                heroMovement.SetOnGround(true);
            }
            else
                heroMovement.Land();
        }
        heroMovement.SetInAir(false);
    }

    /// <summary>
    /// Function on trigger leaving the ground set that hero is not on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        heroMovement.SetInAir(true);
    }

    #endregion
}
public class HeroMovement
{
    #region Variables

    public float stepsToMakeNoise { get; set; }
    public float movementSpeed { get; set; }
    public float runSpeed { get; set; }
    public float jumpingForce { get; set; }
    public float mouseSens { get; set; }
    public Rigidbody characterBody { get; set; }
    public Transform Cam { get; set; }
    public AudioManagerScript am { get; set; }

    private float currentMovementSpeed = 0; // Variable holding current movement speed of the character
    private bool onGround = true; // Variable holding value if player is on the ground
    private bool inAir = false; // Variable holding value if player is in the air
    private float camYRotation; // Camera up or down rotation
    private float nextNoiseAtStep = 0; // Variable holding value at which step to make noise
    private float stepsMade = 0; // Variable holding value how many steps character has made

    #endregion

    #region Functions for jumping

    /// <summary>
    /// Get is hero on ground
    /// </summary>
    /// <returns></returns>
    public bool IsOnGround()
    {
        return onGround;
    }

    /// <summary>
    /// Set is hero on ground
    /// </summary>
    /// <param name="isOnGround"></param>
    public void SetOnGround(bool isOnGround)
    {
        onGround = isOnGround;
    }

    /// <summary>
    /// Get is hero in air
    /// </summary>
    /// <returns></returns>
    public bool IsInAir()
    {
        return inAir;
    }

    /// <summary>
    /// Set is hero in air
    /// </summary>
    /// <param name="isInAir"></param>
    public void SetInAir(bool isInAir)
    {
        inAir = isInAir;
    }

    #endregion

    #region Functions for hero and camera transformations

    /// <summary>
    /// Function to set cursor data
    /// </summary>
    public void SetCursorData()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor on the center of the game screen
        Cursor.visible = false; // make cursor invisible
    }

    /// <summary>
    /// Function to move hero according to mouse movement 
    /// </summary>
    public void CameraRotation()
    {
        float xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens; // Get Mouse X axis movement
        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens; // Get Mouse Y axis movement
        
        characterBody.transform.Rotate(Vector3.up * xmouse);
        camYRotation -= ymouse;
        camYRotation = Mathf.Clamp(camYRotation, -85f, 60f);
        Cam.localRotation = Quaternion.Euler(camYRotation, 0, 0);
    }

    /// <summary>
    /// Function to let character jump if space button pressed and he is on the ground
    /// </summary>
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && inAir == false)
        {
            characterBody.AddForce(characterBody.transform.up * jumpingForce);
            PlayJumpSound();
            onGround = false;
        }

    }

    /// <summary>
    /// Function to know when character lands
    /// </summary>
    public void Land()
    {
        PlayLandingSound();
        inAir = false;
        nextNoiseAtStep = stepsMade + 1f;
    }

    /// <summary>
    /// Function to let character run if Left or Right Shift being hold
    /// </summary>
    public void Run()
    {
        if (Input.GetButton("Run"))
            currentMovementSpeed = runSpeed;
        else
            currentMovementSpeed = movementSpeed;
    }

    /// <summary>
    /// Function to move character according to the keyboard buttons pressed 
    /// </summary>
    public void CharacterMove()
    {
        Vector3 verticalVector; // Vector holding character vertical movement
        Vector3 horizontalVector; // Vector holding character horizontal movement
        Vector3 jumpVector; // Vector holding character jump movement
        Vector3 moveVector; // Vector holding vertical, horizontal and jump movement vectors

        verticalVector = characterBody.transform.forward * currentMovementSpeed * Input.GetAxis("Vertical"); // Vertical character movement vector
        horizontalVector = characterBody.transform.right * currentMovementSpeed * Input.GetAxis("Horizontal"); // horizontal character movement vector
        jumpVector = characterBody.transform.up * characterBody.velocity.y; // jump character movement vector

        moveVector = verticalVector + horizontalVector + jumpVector; // character movement vector

        characterBody.velocity = moveVector; // set character to move

        if (verticalVector.magnitude > 0 || horizontalVector.magnitude > 0)
            ProgressStepCycle();
    }

    #endregion

    #region Functions for sound effects

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
        am.Play("Jump"); // Play Button Press Audio
    }

    /// <summary>
    /// Function to play landing sound
    /// </summary>
    private void PlayLandingSound()
    {
        am.Play("Land"); // Play Button Press Audio
    }

    /// <summary>
    /// Function to play footstep sound
    /// </summary>
    private void PlayFootStepAudio()
    {
        am.PlayFootstep(); // Play Button Press Audio
    }

    #endregion
}