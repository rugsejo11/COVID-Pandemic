using UnityEngine;

public class HeroController : MonoBehaviour
{
    // Character characteristics
    [Tooltip("Character movement speed")]
    public float movementSpeed = 3f; // Variable holding value how quick charaver will move
    [Tooltip("Character jumping force")]
    public float jumpingForce = 300f; // Variable holding value how high character will jump
    [Tooltip("Mouse sensitivity")]
    public float mouseSens = 70f; // Variable holding value sensitivity of the mouse
    bool onGround = false; // Variable holding value if player is on the ground

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
        characterBody = GetComponent<Rigidbody>();
        Cam = Camera.main.GetComponent<Transform>();


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
        if (Input.GetButtonDown("Jump") && onGround == true)
            characterBody.AddForce(transform.up * jumpingForce);

    }

    /// <summary>
    /// Function to let character run if Left or Right Shift being hold
    /// </summary>
    private void Run()
    {
        if (Input.GetButton("Run"))
            movementSpeed = 6f;
        else
            movementSpeed = 3f;
    }

    /// <summary>
    /// Function to move character according to the keyboard buttons pressed 
    /// </summary>
    private void CharacterMove()
    {
        Vector3 verticalVector; // Vector holding character vertical movement
        Vector3 horizontalVector; // Vector holding character horizontal movement
        Vector3 jumpVector; // Vector holding character jump movement

        verticalVector = transform.forward * movementSpeed * Input.GetAxis("Vertical"); // Vertical character movement vector
        horizontalVector = transform.right * movementSpeed * Input.GetAxis("Horizontal"); // horizontal character movement vector
        jumpVector = transform.up * characterBody.velocity.y; // jump character movement vector

        moveVector = verticalVector + horizontalVector + jumpVector; // character movement vector

        characterBody.velocity = moveVector; // set character to move
    }

    /// <summary>
    /// Function on trigger with ground set that hero is on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        onGround = true; // Set variable that hero is on the ground
    }

    /// <summary>
    /// Function on trigger leaving the ground set that hero is not on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        onGround = false; // Set variable that hero is no on the ground
    }
}