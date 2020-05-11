using System.Collections;
using UnityEngine;

public class NotificationsScript
{

    /// <summary>
    /// Function to show hands washing notification
    /// </summary>
    /// <param name="enabled">to show notification or hide</param>
    /// <param name="notification">notification to activate or disable</param>
    public void ShowNotification(bool enabled, GameObject notification)
    {
        if (enabled)
        {
            notification.SetActive(true);
        }
        else
            notification.SetActive(false);
    }

    /// <summary>
    /// Interface to wait before showing notification
    /// </summary>
    /// <param name="seconds">seconds to wait before showing notification</param>
    /// <param name="enabled">to enable notification or disable</param>
    /// <param name="notification">notification to show or to disable</param>
    /// <returns></returns>
    public IEnumerator Wait(float seconds, bool enabled, GameObject notification)
    {
        yield return new WaitForSeconds(seconds);
        ShowNotification(enabled, notification);
    }

    /// <summary>
    /// Interface to show notification and wait before changing scenes
    /// </summary>
    /// <param name="seconds">seconds to wait before/after showing notification</param>
    /// <param name="finished">is stage finished or player lost</param>
    /// <param name="scene">current scene index</param>
    /// <returns></returns>
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
