using NUnit.Framework;

namespace Tests
{
    public class ObjectDistanceTests
    {
        [Test]
        [TestCase(59, 1, true)]
        [TestCase(59, 3, false)]
        [TestCase(59, 4, false)]
        [TestCase(60, 1, false)]
        [TestCase(60, 3, false)]
        [TestCase(60, 4, false)]
        [TestCase(61, 1, false)]
        [TestCase(61, 3, false)]
        [TestCase(61, 4, false)]
        [TestCase(null, null, false)]
        [TestCase(null, 1, false)]
        [TestCase(59, null, false)]
        [TestCase(0, 0, false)]

        public void CheckDistanceAndAngle_Test(
            float angleView,
            float distance,
            bool isInRange
            )

        {
            ObjectDistanceScript objectDistance = new ObjectDistanceScript();
            bool returnedIsInInRange = objectDistance.CheckDistanceAndAngle(distance, angleView);
            Assert.AreEqual(isInRange, returnedIsInInRange);
        }
    }
}
