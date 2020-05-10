using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HPScriptTest
    {
        [Test]
        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(null, false)]

        public void ActiveHearts_Test(
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
