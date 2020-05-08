using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{

    public class TimeManagementTests
    {

        [Test]
        [TestCase(601, "10:01")]
        [TestCase(61, "01:01")]
        [TestCase(60, "01:00")]
        [TestCase(59, "00:59")]
        [TestCase(1, "00:01")]
        [TestCase(0, "00:00")]
        [TestCase(-1, "Error")]

        public void ReturnTimerString_Test(
            float roundedSecondsLeft,
            string timerString
            )

        {
            TimeManagement tm = new TimeManagement();
            tm.ReturnTimerString(roundedSecondsLeft);

            Assert.AreEqual(timerString, tm.TimerString);

        }

        [Test]
        [TestCase(601, "ticking_slow")]
        [TestCase(61, "ticking_slow")]
        [TestCase(60, "ticking_fast")]
        [TestCase(59, "ticking_fast")]
        [TestCase(1, "ticking_fast")]
        [TestCase(0, "explosion")]
        [TestCase(-1, "error")]

        public void ReturnTimerSound(
            float roundedSecondsLeft,
            string timerSound
            )

        {
            TimeManagement tm = new TimeManagement();
            tm.ReturnTimerSound(roundedSecondsLeft);
            Assert.AreEqual(timerSound, tm.TimerSound);

        }
    }
}
