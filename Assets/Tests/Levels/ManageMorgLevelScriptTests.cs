using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ManageMorgLevelScriptTests
    {
        [Test]
        [TestCase(true, true, true, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, false)]
        [TestCase(false, true, true, false)]
        [TestCase(false, false, true, false)]
        [TestCase(false, true, true, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, false, false, false)]
        [TestCase(null, null, null, false)]


        public void FirstStage_Test(
           bool smallButtonDone,
           bool hazardousLeverDone,
           bool switcherDone,
           bool isStageDone
           )

        {
            ManageMorgLevel morgLevel = new ManageMorgLevel();
            bool isDone = morgLevel.FirstStage(smallButtonDone, hazardousLeverDone, switcherDone);
            Assert.AreEqual(isDone, isStageDone);

        }

        [Test]
        [TestCase(true, true, true, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, false)]
        [TestCase(false, true, true, false)]
        [TestCase(false, false, true, false)]
        [TestCase(false, true, true, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, false, false, false)]
        [TestCase(null, null, null, false)]


        public void SecondStage_Test(
            bool clownLeverDone,
            bool electricityLeverDone,
            bool elevatorButtonDone,
            bool isStageDone
   )

        {
            ManageMorgLevel morgLevel = new ManageMorgLevel();
            bool isDone = morgLevel.SecondStage(clownLeverDone, electricityLeverDone, elevatorButtonDone);
            Assert.That(isDone, Is.EqualTo(isStageDone));
        }

        [Test]
        [TestCase(true, true, true, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, false)]
        [TestCase(false, true, true, false)]
        [TestCase(false, false, true, false)]
        [TestCase(false, true, true, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, false, false, false)]
        [TestCase(null, null, null, false)]


        public void LastStage_Test(
            bool finishLeverDone,
            bool finishDetonator,
            bool complexLeverDone,
            bool isStageDone
)

        {
            ManageMorgLevel morgLevel = new ManageMorgLevel();
            bool isDone = morgLevel.LastStage(finishLeverDone, finishDetonator, complexLeverDone);
            Assert.AreEqual(isDone, isStageDone);
        }

        [Test]
        [TestCase("complexLeverDone", true, "complexLeverDone True")]
        [TestCase("complexLeverDone", false, "complexLeverDone False")]
        [TestCase("complexLeverDone12", true, "Error! Lever name or isDone entered wrong!")]
        [TestCase(null, null, "Error! Lever name or isDone entered wrong!")]
        [TestCase("complexLeverDone", null, "complexLeverDone False")]

        public void SetDone_Test(
            string LeverName,
            bool isLeverDone,
            string leverState
)

        {
            ManageMorgLevel morgLevel = new ManageMorgLevel();
            string returnedLeverState = morgLevel.SetDone(LeverName, isLeverDone);
            Assert.AreEqual(leverState, returnedLeverState);
        }

        [Test]
        [TestCase("explosions", "explosion")]
        [TestCase("", "Sound not found!")]
        [TestCase(null, "Sound not found!")]


        public void PlaySound_Test(
            string soundName,
            string playedSound
            )

        {
            AudioManagerScript am = Object.FindObjectOfType<AudioManagerScript>();
            ManageMorgLevel morgLevel = new ManageMorgLevel();
            string returnedPlayedSound = morgLevel.PlaySound(soundName, am);
            Assert.AreEqual(playedSound, returnedPlayedSound);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(0, 0)]
        [TestCase(3, 0)]
        [TestCase(null, 0)]

        public void OpenDoors_Test(
            int doorsToOpen,
            int doorsOpened
            )

        {
            ManageMorgLevel morgLevel = new ManageMorgLevel();
            int returnedDoorsOpened = morgLevel.OpenDoors(doorsToOpen);
            Assert.AreEqual(doorsOpened, returnedDoorsOpened);
        }
    }
}
