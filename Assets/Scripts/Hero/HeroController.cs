using UnityEngine;

public class HeroController : MonoBehaviour
{
    // Character movement characteristics
    [Tooltip("Character movement speed")]
    [SerializeField] private float movementSpeed = 3f; // Variable holding value how quick character will go
    [Tooltip("Character runing speed")]
    [SerializeField] private float runSpeed = 5f; // Variable holding value how quick character will run
    [Tooltip("Character jumping force")]
    [SerializeField] private float jumpingForce = 300f; // Variable holding value how high character will jump
    [Tooltip("Mouse sensitivity")]
    [SerializeField] private float mouseSens = 70f; // Variable holding value sensitivity of the mouse
    private float currentMovementSpeed = float.MinValue; // Variable holding current movement speed of the character
    private bool onGround = true; // Variable holding value if player is on the ground
    private bool inAir = false; // Variable holding value if player is in the air

    // Hero and move vector to set hero velocity
    private Rigidbody characterBody = null; // Character body
    Vector3 moveVector; // Character move vector

    // Sound
    [SerializeField] private AudioClip[] footstepSounds = new AudioClip[4]; // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip jumpSound = null; // the sound played when character leaves the ground.
    [SerializeField] private AudioClip landSound = null; // the sound played when character touches back on ground.
    private AudioSource audioSource;
    [SerializeField] private float stepsToMakeNoise = float.MinValue; // Variable holding value how many steps character has to make to make sound
    private float stepsMade = float.MinValue; // Variable holding value how many steps character has made
    private float nextNoiseAtStep = float.MinValue; // Variable holding value at which step to make noise

    Transform Cam;
    float yRotation;

    /// <summary>
    /// Function triggered at the start of the script
    /// </summary>
    void Start()
    {
        characterBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Cam = Camera.main.GetComponent<Transform>();
        nextNoiseAtStep = stepsMade / 2f;
        SetCursorData();

    }

    /// <summary>
    /// Function to set cursor data
    /// </summary>
    void SetCursorData()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor on the center of the game screen
        Cursor.visible = false; // make cursor invisible
    }

    /// <summary>
    /// Function to update game on every frame
    /// </summary>
    void Update()
    {
        CameraRotation(); // Function to rotate hero's camera according to mouse movement 
        Jump(); // Function to let character jump if space button pressed and he is on the grouund
    }

    /// <summary>
    /// Function to update game fixed on every frame
    /// </summary>
    void FixedUpdate()
    {
        Run(); // Function to let character run if Left or Right Shift being hold
        CharacterMove(); // Function to move character according to the keyboard buttons pressed 
    }

    /// <summary>
    /// Function to move hero according to mouse movement 
    /// </summary>
    private void CameraRotation()
    {
        float xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens; // Get Mouse X axis movement
        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens; // Get Mouse Y axis movement
        transform.Rotate(Vector3.up * xmouse);
        yRotation -= ymouse;
        yRotation = Mathf.Clamp(yRotation, -85f, 60f);
        Cam.localRotation = Quaternion.Euler(yRotation, 0, 0);
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

        verticalVector = transform.forward * currentMovementSpeed * Input.GetAxis("Vertical"); // Vertical character movement vector
        horizontalVector = transform.right * currentMovementSpeed * Input.GetAxis("Horizontal"); // horizontal character movement vector
        jumpVector = transform.up * characterBody.velocity.y; // jump character movement vector

        moveVector = verticalVector + horizontalVector + jumpVector; // character movement vector

        characterBody.velocity = moveVector; // set character to move
        if (verticalVector.magnitude > 0 || horizontalVector.magnitude > 0)
            ProgressStepCycle();
    }

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

    private void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.clip = jumpSound;
            //audioSource.volume = PlayerPrefs.GetFloat("volume", 1f);
            audioSource.Play();
        }
    }
    private void PlayLandingSound()
    {
        if (landSound != null)
        {
            audioSource.clip = landSound;
            //audioSource.volume = PlayerPrefs.GetFloat("volume", 1f);
            audioSource.Play();
        }
        //FindObjectOfType<AudioManager>().Play("Land"); // Play Button Press Audio
    }

    private void PlayFootStepAudio()
    {
        //FindObjectOfType<AudioManager>().Play("footStep1"); // Play Button Press Audio
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, footstepSounds.Length);
        audioSource.clip = footstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = audioSource.clip;
    }
}