using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlaceToRackScriptTests
    {
        [Test]
        [TestCase("-1,-1,-1", "-1,-1,-1")]
        [TestCase("0,0,0", "0,0,0")]
        [TestCase("1,1,1", "1,1,1")]

        public void SetTubePosition_Test(
            string firstVector3,
            string secondVector3
            )

        {
            var firstVector = firstVector3.Split(',').Select(float.Parse).ToArray();
            Vector3 input = new Vector3(firstVector[0], firstVector[1], firstVector[2]);

            var secondVector = secondVector3.Split(',').Select(float.Parse).ToArray();
            Vector3 result = new Vector3(secondVector[0], secondVector[1], secondVector[2]);

            PlaceToRackScript placeToRack = new PlaceToRackScript();
            GameObject gameObject = new GameObject();

            Vector3 resultReturned;

            resultReturned = placeToRack.SetTubePosition(gameObject, input);

            Assert.AreEqual(result, resultReturned);
        }
    }
}
