using UnityEngine;

public class ObjectManipulationScript
{
    #region Variables

    private float distance; // Variable holding distance between hero and item
    private bool objectInHands = false; // Variable holding value if object is at hero's hands
    private ObjectDistanceScript objectDistance = new ObjectDistanceScript();
    private bool objectGrabbed = false;
    private bool atSocket = false;

    #endregion 

    #region Get Set Functions
    public bool IsObjectGrabbed() { return objectGrabbed; }
    public bool IsAtSocket() { return atSocket; }
    public void SetAtSocket(bool isAtSocket) { atSocket = isAtSocket; }
    public AudioManagerScript am { get; set; }

    #endregion

    #region Interaction Functions

    /// <summary>
    /// Function to grab or drop object
    /// </summary>
    public void GrabAnObject(Transform transform, Camera main, HeroDataScript hero, Transform HeroHandsPosition, Rigidbody objectToTake)
    {
        // If in range, button pressed and item don't grab, grab item
        if (objectDistance.IsObjectInRange(transform.position, main.transform) && Input.GetKeyDown(KeyCode.Mouse0) && !objectGrabbed && !hero.IsObjectGrabbed()) // If button pressed and object in range
        {
            if (atSocket)
            {
                hero.SetTimeAfterTakingFromRack(0);
            }

            atSocket = false; // Remove object from socket
            objectGrabbed = true; // Grab object
            hero.GrabObject(); // Set that hero has an object in he's hands
            objectInHands = false; // Set that object is not grabbed just yet
            objectToTake.transform.rotation = Quaternion.Euler(0, 0, 0);
            //am.Play("objectTaken"); // Play explosion sound effect
        }

        // If item grabbed
        else if (objectGrabbed) // If object grabbed
        {
            // If mouse 1 released, drop
            if (Input.GetKeyUp(KeyCode.Mouse0)) // If Mouse 1 button realesed
            {
                objectGrabbed = false; // Drop object
                hero.DropObject(); // Free hero's hands
                objectToTake.drag = 0f;
                objectToTake.angularDrag = .5f;
                ////am.Play("objectDropped"); // Play explosion sound effect
            }

            // Follow item with char
            else
            {
                if (!objectInHands) // If object not in hands just yet
                {
                    objectToTake.drag = 10f;
                    objectToTake.angularDrag = 10f;
                    objectToTake.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                else
                {
                    distance = objectDistance.GetDistance(transform.position, main.transform.position);
                    if (distance > 3f) // If object too far, drop object
                    {
                        objectGrabbed = false;
                        hero.DropObject(); // Free hero's hands
                        //am.Play("objectDropped"); // Play explosion sound effect
                    }
                }

                objectInHands = true;
                objectToTake.transform.position = Vector3.Lerp(objectToTake.transform.position, HeroHandsPosition.position, 1f); // Follow object in character's hands
            }
        }
    }

    /// <summary>
    /// Function to place an object to a socket
    /// </summary>
    public Vector3 PlaceObjectToRack(GameObject gameObject, Vector3 position)
    {
        gameObject.transform.position = position; // Set grabbed object position to socket position

        return gameObject.transform.position;
    }

    #endregion
}