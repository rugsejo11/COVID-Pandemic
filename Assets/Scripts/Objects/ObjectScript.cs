using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    #region Variables

    // Hero and object
    [SerializeField] private Transform HeroHandsPosition = null; // Hands position of the character
    [SerializeField] private Rigidbody characterBody = null; // Character body
    private HeroDataScript hero; // Game character
    private Rigidbody objectToTake; // Object that hero is trying to take

    // Grab and object
    private GrabObjectScript grabAnObject = new GrabObjectScript();
    [SerializeField] private bool possibleToGrab = false; // Is it possible to grab this item


    // Test tube rack slot
    [SerializeField] private bool testTube = false;
    private GameObject currentSocket = null; // In which slot object is placed
    private PlaceToRackScript placeObjectToRack; // Game character

    #endregion

    #region Functions

    void Awake()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
        objectToTake = GetComponent<Rigidbody>(); // Get object to take

        if (possibleToGrab)
        {
            grabAnObject = new GrabObjectScript();

            if (testTube)
            {
                placeObjectToRack = new PlaceToRackScript(); // Get hero object
                grabAnObject.TimeAfterRemovingFromRack = 2;
            }
        }
    }

    /// <summary>
    /// Function triggered at every frame
    /// </summary>
    void Update()
    {
        // Grab object
        if (possibleToGrab)
        {
            grabAnObject.GrabAnObject(transform, Camera.main, hero, HeroHandsPosition, objectToTake);
        }

        // Put to rack if test tube
        if (testTube)
        {
            if (grabAnObject.AtSocket)
            {
                placeObjectToRack.SetTubePosition(gameObject, currentSocket.transform.position);
            }
            else
            {
                grabAnObject.TimeAfterRemovingFromRack += Time.deltaTime;
            }
        }
    }
    /// <summary>
    /// On collision between object and socket
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Socket" && grabAnObject.TimeAfterRemovingFromRack >= 2)
        {
            grabAnObject.AtSocket = true;
            currentSocket = other.gameObject;
            other.gameObject.tag = "UsedSocket";
        }
    }

    /// <summary>
    /// On collission with the room, push character back, that he would not push item through wall
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Room") && grabAnObject.ObjectGrabbed)
        {
            float force = 1500;
            Vector3 dir = collision.contacts[0].point - objectToTake.transform.position;
            dir = -dir.normalized;
            characterBody.AddForce(dir * force);
        }
    }
}

#endregion
