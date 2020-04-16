using UnityEngine;

public class ClickOnO : MonoBehaviour
{
    #region Variables
    public float force = 10; // Force Used To Throw Object
    #endregion

    #region Functions
    // Update is called once per frame
    void Update()
    {
        LaunchObject(); // If Mouse 1 Pressed Launch Object
        GetToMenu(); // If ESC Button Pressed Get To Menu Scene
    }

    /// <summary>
    /// On Button Pressed Launch Object
    /// </summary>
    private void LaunchObject()
    {
        if (Input.GetMouseButtonDown(0)) // If Button 1 Pressed
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    Rigidbody rb;
                    if (rb = hit.transform.GetComponent<Rigidbody>())
                    {
                        LaunchIntoAir(rb); // Use Force On Rigidbody
                    }
                }
            }
        }

    }

    /// <summary>
    /// Launch Object Into Air
    /// </summary>
    /// <param name="rig"></param>
    private void LaunchIntoAir(Rigidbody rig)
    {
        rig.AddForce(rig.transform.up * force, ForceMode.Impulse); // Use Force On Rigidbody
    }

    /// <summary>
    /// On Button Pressed Get To Menu Scene
    /// </summary>
    private void GetToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // If ESC Button Pressed
        {
            MenuControl.GetToMenu(); // Get To Menu Scene
            FindObjectOfType<AudioManager>().Play("buttonPress"); // Play Button Press Audio
        }
    }
    #endregion
}
