using UnityEngine;

public class HeroController : MonoBehaviour
{
    // Character characteristics
    [Tooltip("Character movement speed")]
    [SerializeField] private float movementSpeed = 3f; // Variable holding value how quick charaver will move
    [Tooltip("Character runing speed")]
    [SerializeField] private float runSpeed = 6f;
    [Tooltip("Character jumping force")]
    [SerializeField] private float jumpingForce = 300f; // Variable holding value how high character will jump
    [Tooltip("Mouse sensitivity")]
    [SerializeField] private float mouseSens = 70f; // Variable holding value sensitivity of the mouse
    public bool onGround = false; // Variable holding value if player is on the ground
    public bool characterLanded = false; // Variable holding value if player landed
    public bool inAir = false;
    private float currentMovementSpeed;

    // Sound
    [SerializeField] private AudioClip[] m_FootstepSounds = new AudioClip[4];    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
    [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
    private AudioSource m_AudioSource;


    // Hero and moving him
    Rigidbody characterBody; // Character body
    Vector3 moveVector; // Character move vector

    Transform Cam;
    float yRotation;

    /// <summary>
    /// Function triggered at the start of the script
    /// </summary>
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        characterBody = GetComponent<Rigidbody>();
        Cam = Camera.main.GetComponent<Transform>();
        currentMovementSpeed = float.MinValue;

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.Confined; // freeze cursor on screen centre
        //Cursor.visible = false; // invisible cursor
    }

    /// <summary>
    /// Function to update game on every frame
    /// </summary>
    void Update()
    {
        CameraRotation(); // Function to rotate hero's camera according to mouse movement 
        Jump(); // Function to let character jump if space button pressed and he is on the grouund
        Land();
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
    /// Function to rotate hero's camera according to mouse movement 
    /// </summary>
    private void CameraRotation()
    {
        float xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens;
        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens;
        transform.Rotate(Vector3.up * xmouse);
        yRotation -= ymouse;
        yRotation = Mathf.Clamp(yRotation, -85f, 60f);
        Cam.localRotation = Quaternion.Euler(yRotation, 0, 0);
    }

    /// <summary>
    /// Function to let character jump if space button pressed and he is on the grouund
    /// </summary>
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && characterLanded == true)
        {
            characterBody.AddForce(transform.up * jumpingForce);
            PlayJumpSound();
            //inAir = true;
        }

    }
    private void Land()
    {
        if (!m_AudioSource.isPlaying && inAir == true && onGround == true)
        {
            PlayLandingSound();
            inAir = false;
        }
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

        if (characterBody.velocity != moveVector)
        {
            characterBody.velocity = moveVector; // set character to move
            if(currentMovementSpeed == runSpeed)
            {

            }
            else { }
        }
    }

    /// <summary>
    /// Function on trigger with ground set that hero is on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = true;
        }
        characterLanded = true; // Set variable that hero is on the ground
    }

    /// <summary>
    /// Function on trigger leaving the ground set that hero is not on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = false;
        }
        characterLanded = false; // Set variable that hero is no on the ground
        inAir = true;
    }

    private void PlayJumpSound()
    {
        if (m_JumpSound != null)
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.volume = PlayerPrefs.GetFloat("volume", 1f);
            m_AudioSource.Play();
        }
    }
    private void PlayLandingSound()
    {
        if (m_LandSound != null)
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.volume = PlayerPrefs.GetFloat("volume", 1f);
            m_AudioSource.Play();
        }
    }

    private void PlayFootStepAudio()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }
}