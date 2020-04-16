using UnityEngine;

public class RotateO : MonoBehaviour
{
    #region Variables
    private float speed = 50; // Rotation Speed
    #endregion

    #region Functions
    void Update()
    {
        RotateCube(); // If Mouse 2 Pressed Rotate Object
    }
    /// <summary>
    /// On Button Clicked Rotate Object
    /// </summary>
    public void RotateCube()
    {
        if (Input.GetKey(KeyCode.Mouse1)) // If Mouse 2 Pressed
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * speed * Time.deltaTime, 0);
            transform.Rotate(Input.GetAxis("Mouse Y") * speed * Time.deltaTime, 0, 0);
        }
    }
    #endregion
}