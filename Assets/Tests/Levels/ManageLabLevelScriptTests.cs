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
        [TestCase(0, 1, true)]
        [TestCase(1, 0, false)]
        [TestCase(1, 1, false)]
        [TestCase(0, 0, false)]
        [TestCase(2, 2, false)] // No desired test tubes
        [TestCase(3, 3, false)] // Test tubes not inserted
        public void SouthRackFinished_Test(
            int firstEntered,
            int secondEntered,
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

            if (firstEntered != 2)
            {
                southRack.GetSocket(2).SetDesired(desiredObjects[0]);
                southRack.GetSocket(5).SetDesired(desiredObjects[1]);
            }

            if (firstEntered != 3)
            {
                southRack.GetSocket(2).TestTubeInserted(desiredObjects[firstEntered]);
                southRack.GetSocket(5).TestTubeInserted(desiredObjects[secondEntered]);
            }

            labLevel.southRack = southRack;
            bool returnedBool = labLevel.SouthRackFinished(labLevel.southRack);
            Assert.AreEqual(rackFinished, returnedBool);
        }

        [Test]
        [TestCase(0, 1, 2, true)]
        [TestCase(0, 0, 0, false)]
        [TestCase(0, 0, 1, false)]
        [TestCase(0, 1, 0, false)]
        [TestCase(0, 1, 1, false)]
        [TestCase(1, 0, 0, false)]
        [TestCase(1, 0, 1, false)]
        [TestCase(1, 1, 0, false)]
        [TestCase(1, 1, 1, false)]
        [TestCase(2, 0, 0, false)]
        [TestCase(2, 0, 1, false)]
        [TestCase(2, 1, 0, false)]
        [TestCase(2, 1, 1, false)]
        [TestCase(2, 2, 0, false)]
        [TestCase(2, 2, 1, false)]
        [TestCase(2, 2, 2, false)]
        [TestCase(3, 3, 3, false)] // No desired test tubes
        [TestCase(4, 4, 4, false)] // Test tubes not inserted
        public void EastRackFinished_Test(
            int firstEntered,
            int secondEntered,
            int thirdEntered,
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

            if (firstEntered != 3)
            {
                eastRack.GetSocket(1).SetDesired(desiredObjects[0]);
                eastRack.GetSocket(3).SetDesired(desiredObjects[1]);
                eastRack.GetSocket(6).SetDesired(desiredObjects[2]);

            }

            if (firstEntered != 4)
            {
                eastRack.GetSocket(1).TestTubeInserted(desiredObjects[firstEntered]);
                eastRack.GetSocket(3).TestTubeInserted(desiredObjects[secondEntered]);
                eastRack.GetSocket(6).TestTubeInserted(desiredObjects[thirdEntered]);

            }

            labLevel.eastRack = eastRack;
            bool returnedBool = labLevel.EastRackFinished(labLevel.eastRack);
            Assert.AreEqual(rackFinished, returnedBool);
        }
    }
}
