using System.Collections;
using UnityEngine;

public class NotificationsScript
{

    /// <summary>
    /// Function to show hands washing notification
    /// </summary>
    /// <param name="enabled"></param>
    public void ShowNotification(bool enabled, GameObject notification)
    {
        if (enabled)
        {
            notification.SetActive(true);
        }
        else
            notification.SetActive(false);
    }


    public IEnumerator Wait(float seconds, bool enabled, GameObject notification)
    {
        yield return new WaitForSeconds(seconds);
        ShowNotification(enabled, notification);
    }

    public IEnumerator StageStatusChange(float seconds, bool finished, int scene)
    {
        yield return new WaitForSeconds(seconds);

        if (finished)
        {
            SceneManageScript.LoadNextLevel(scene);
        }
        else
        {
            SceneManageScript.RestartScene();
        }
    }
}
