using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ManageLabLevelScriptTests
    {
        ManageLabLevel labLevel = new ManageLabLevel();
        TubesRack southRack = new TubesRack(); TubesRack eastRack = new TubesRack();
        Socket socket, socket2, socket3, socket4, socket5, socket6;


        [Test]
        [TestCase(0, 1, false, true, true, true)]
        [TestCase(1, 0, false, true, true, false)]
        [TestCase(1, 1, false, true, true, false)]
        [TestCase(0, 0, false, true, true, false)]
        [TestCase(0, 1, false, false, true, false)] // No desired test tubes
        [TestCase(0, 1, false, true, false, false)] // Test tubes not inserted
        [TestCase(0, 1, true, true, true, false)]  // Three tubes entered instead of two


        public void IsSouthRackFinished_Test(
            int firstEntered,
            int secondEntered,
            bool thirdEntered,
            bool desiredTestTubesExist,
            bool testTubesInserted,
            bool rackFinished
            )

        {
            socket = new Socket(); socket2 = new Socket(); socket3 = new Socket(); socket4 = new Socket(); socket5 = new Socket(); socket6 = new Socket();

            southRack.SetSocket(1, socket);
            southRack.SetSocket(2, socket2);
            southRack.SetSocket(3, socket3);
            southRack.SetSocket(4, socket4);
            southRack.SetSocket(5, socket5);
            southRack.SetSocket(6, socket6);

            GameObject desiredObject = new GameObject();
            GameObject desiredObject2 = new GameObject();
            GameObject desiredObject3 = new GameObject();
            GameObject[] desiredObjects = { desiredObject, desiredObject2, desiredObject3 };

            if (desiredTestTubesExist)
            {
                southRack.GetSocket(2).SetDesired(desiredObjects[0]);
                southRack.GetSocket(5).SetDesired(desiredObjects[1]);
            }

            if (testTubesInserted)
            {
                southRack.GetSocket(2).TestTubeInserted(desiredObjects[firstEntered]);
                southRack.GetSocket(5).TestTubeInserted(desiredObjects[secondEntered]);
            }

            if(thirdEntered)
            {
                southRack.GetSocket(4).TestTubeInserted(desiredObjects[secondEntered]);
            }

            bool returnedBool = labLevel.IsSouthRackFinished(southRack);
            Assert.AreEqual(rackFinished, returnedBool);
        }

        [Test]
        [TestCase(0, 1, 2, false, true, true, true)]
        [TestCase(0, 0, 0, false, true, true, false)]
        [TestCase(0, 0, 1, false, true, true, false)]
        [TestCase(0, 1, 0, false, true, true, false)]
        [TestCase(0, 1, 1, false, true, true, false)]
        [TestCase(1, 0, 0, false, true, true, false)]
        [TestCase(1, 0, 1, false, true, true, false)]
        [TestCase(1, 1, 0, false, true, true, false)]
        [TestCase(1, 1, 1, false, true, true, false)]
        [TestCase(2, 0, 0, false, true, true, false)]
        [TestCase(2, 0, 1, false, true, true, false)]
        [TestCase(2, 1, 0, false, true, true, false)]
        [TestCase(2, 1, 1, false, true, true, false)]
        [TestCase(2, 2, 0, false, true, true, false)]
        [TestCase(2, 2, 1, false, true, true, false)]
        [TestCase(2, 2, 2, false, true, true, false)]
        [TestCase(0, 1, 2, false, false, true, false)] // No desired test tubes
        [TestCase(0, 1, 2, false, true, false, false)] // Test tubes not inserted
        [TestCase(0, 1, 2, true, true, true, false)] // Four tubes entered instead of three


        public void IsEastRackFinished_Test(
            int firstEntered,
            int secondEntered,
            int thirdEntered,
            bool fourthEntered,
            bool desiredTestTubesExist,
            bool testTubesInserted,
            bool rackFinished
            )

        {
            socket = new Socket(); socket2 = new Socket(); socket3 = new Socket(); socket4 = new Socket(); socket5 = new Socket(); socket6 = new Socket();

            eastRack.SetSocket(1, socket);
            eastRack.SetSocket(2, socket2);
            eastRack.SetSocket(3, socket3);
            eastRack.SetSocket(4, socket4);
            eastRack.SetSocket(5, socket5);
            eastRack.SetSocket(6, socket6);

            GameObject desiredObject = new GameObject();
            GameObject desiredObject2 = new GameObject();
            GameObject desiredObject3 = new GameObject();
            GameObject desiredObject4 = new GameObject();
            GameObject[] desiredObjects = { desiredObject, desiredObject2, desiredObject3, desiredObject4 };

            if (desiredTestTubesExist)
            {
                eastRack.GetSocket(1).SetDesired(desiredObjects[0]);
                eastRack.GetSocket(3).SetDesired(desiredObjects[1]);
                eastRack.GetSocket(6).SetDesired(desiredObjects[2]);

            }

            if (testTubesInserted)
            {
                eastRack.GetSocket(1).TestTubeInserted(desiredObjects[firstEntered]);
                eastRack.GetSocket(3).TestTubeInserted(desiredObjects[secondEntered]);
                eastRack.GetSocket(6).TestTubeInserted(desiredObjects[thirdEntered]);

            }

            if(fourthEntered)
            {
                eastRack.GetSocket(2).TestTubeInserted(desiredObjects[secondEntered]);
            }

            bool returnedBool = labLevel.IsEastRackFinished(eastRack);
            Assert.AreEqual(rackFinished, returnedBool);
        }
    }
}
