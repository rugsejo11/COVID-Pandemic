using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class NotificationsScriptTests
    {

        [Test]
        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(null, false)]

        public void ShowNotification_Test(
            bool enabled,
            bool result
            )

        {
            GameObject notification = new GameObject();

            NotificationsScript notificationsScript = new NotificationsScript();
            notification.SetActive(false);

            notificationsScript.ShowNotification(enabled, notification);

            Assert.AreEqual(result, notification.activeSelf);
        }
    }
}
