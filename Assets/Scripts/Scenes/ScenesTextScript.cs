using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesTextScript : MonoBehaviour
{
    // Text objects
    [SerializeField] private GameObject First = null;
    [SerializeField] private GameObject Second = null;
    [SerializeField] private GameObject Third = null;
    [SerializeField] private GameObject Fourth = null;

    [SerializeField] private float appearTime = 2f; // How long till text appears
    [SerializeField] private float disappearTime = 3f; // How long till text disappears
    [SerializeField] private bool isFinish = false; // Is finish scene

    private NotificationsScript notifications;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        StartCoroutine(ShowText(appearTime, disappearTime));
    }

    /// <summary>
    /// Function to make text appear and disappear and load next scene
    /// </summary>
    /// <param name="appearTime"></param>
    /// <param name="disappearTime"></param>
    /// <returns></returns>
    private IEnumerator ShowText(float appearTime, float disappearTime)
    {
        notifications = new NotificationsScript();
        yield return new WaitForSeconds(appearTime);
        notifications.ShowNotification(true, First);
        yield return new WaitForSeconds(disappearTime);
        notifications.ShowNotification(false, First);
        yield return new WaitForSeconds(appearTime);
        notifications.ShowNotification(true, Second);
        yield return new WaitForSeconds(appearTime);
        notifications.ShowNotification(true, Third);
        yield return new WaitForSeconds(disappearTime);
        notifications.ShowNotification(false, Second);
        notifications.ShowNotification(false, Third);

        if(Fourth != null)
        {
            yield return new WaitForSeconds(appearTime);
            notifications.ShowNotification(true, Fourth);
            yield return new WaitForSeconds(disappearTime);
        }

        if(!isFinish)
        {
            SceneManageScript.LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManageScript.GetToMenu();
        }
    }
}
