using UnityEngine;
using UnityEngine.SceneManagement;


public class ClickOnO : MonoBehaviour
{
    public float force = 20;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void PrintName(GameObject ob)
    {
        print(ob.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                        LaunchIntoAir(rb);
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuControl.GetToMenu();
        }
    }

    private void LaunchIntoAir(Rigidbody rig)
    {
        rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
    }
}
