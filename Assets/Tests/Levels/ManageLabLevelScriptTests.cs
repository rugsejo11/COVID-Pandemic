using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ManageLabLevelScriptTests
    {
        [Test]
        [TestCase("explosions", "explosion")]
        [TestCase("", "Sound not found!")]
        [TestCase(null, "Sound not found!")]


        public void PlaySound_Test(
            string soundName,
            string playedSound
            )

        {
            ManageLabLevelScript labLevel = new ManageLabLevelScript();
            AudioManagerScript am = Object.FindObjectOfType<AudioManagerScript>();
            ManageMorgLevel morgLevel = new ManageMorgLevel();
            string returnedPlayedSound = morgLevel.PlaySound(soundName, am);
            Assert.AreEqual(playedSound, returnedPlayedSound);
        }
    }
}
