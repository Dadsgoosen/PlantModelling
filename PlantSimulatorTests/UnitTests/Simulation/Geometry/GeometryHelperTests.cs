using System.Collections;
using System.Numerics;
using System.Threading.Tasks;
using NUnit.Framework;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulatorTests.UnitTests.Simulation.Geometry
{
    [TestFixture]
    public class GeometryHelperTests
    {
        private IGeometryHelper helper;

        [SetUp]
        public void SetUp()
        {
            helper = new GeometryHelper();
        }

        [Test]
        public void IsInsidePolygon_WhenGivenAPointInside_ShouldReturnTrue()
        {
            var point = new Vector2();

            var face = new[]
            {
                new Vector2(-5, 5), 
                new Vector2(5, 5), 
                new Vector2(5, -5), 
                new Vector2(-5, -5), 
            };

            var inside = helper.IsInsidePolygon(point, face);

            Assert.True(inside);
        }

        [Test]
        public void IsInsidePolygon_WhenGivenAPointOutside_ShouldReturnFalse()
        {
            var point = new Vector2(0, -6);

            var face = new[]
            {
                new Vector2(-5, 5),
                new Vector2(5, 5),
                new Vector2(5, -5),
                new Vector2(-5, -5),
            };

            var inside = helper.IsInsidePolygon(point, face);

            Assert.False(inside);
        }

        [Test]
        public void IsInsidePolygon_WhenGivenAPointOnLine_ShouldReturnTrue()
        {
            var point = new Vector2(0, -5);

            var face = new[]
            {
                new Vector2(-5, 5),
                new Vector2(5, 5),
                new Vector2(5, -5),
                new Vector2(-5, -5),
            };

            var inside = helper.IsInsidePolygon(point, face);

            Assert.True(inside);
        }

        [Test]
        public void ClosestPointOnLine_WhenGivenPoint_ShouldReturnClosestPoint()
        {
            var line = new[]
            {
                new Vector2(-5, 5),
                new Vector2(5, 5)
            };

            var point = new Vector2(2.5F, 0);

            var nearest = helper.NearestPointOnLine(point, line);

            Assert.AreEqual(2.5, nearest.X, 0.0001);
            Assert.AreEqual(5, nearest.Y, 0.0001);
        }

        [Test]
        public void IsPointOnLine_WhenGivenPointOnLine_ShouldReturnTrue()
        {
            var line = new[]
            {
                new Vector2(-5, 5),
                new Vector2(5, 5)
            };

            var point = new Vector2(2.5F, 5);

            var onLine = helper.IsOnLine(point, line);

            Assert.True(onLine);
        }

        [TestCaseSource(typeof(PointLineTestData))]
        public bool IsPointOnLine_WhenGivenPointNotOnLine_ShouldReturnFalse(float x, float y)
        {
            var line = new[]
            {
                new Vector2(-5, 5),
                new Vector2(5, 5)
            };

            return helper.IsOnLine(new Vector2(x, y), line);
        }

        [Test]
        public void CreateFacePairs_WhenGivenAPolygon_CreateFacePairs()
        {
            var polygon = new[]
            {
                new Vector2(-5, 5),
                new Vector2(5, 5),
                new Vector2(5, -5),
                new Vector2(-5, -5) 
            };

            var pairs = helper.CreateFacePairs(polygon);

            Assert.AreEqual(pairs.Length, polygon.Length);

            for (int i = 0; i < polygon.Length; i++)
            {
                Assert.AreEqual(2, pairs[i].Length);

                if (i == polygon.Length - 1)
                {

                    Assert.AreEqual(pairs[i][0], polygon[i]);
                    Assert.AreEqual(pairs[i][1], polygon[0]);
                }
                else
                {
                    Assert.AreEqual(pairs[i][0], polygon[i]);
                    Assert.AreEqual(pairs[i][1], polygon[i + 1]);
                }
            }
        }

        [TestCaseSource(typeof(HeightTestData))]
        public bool IsWithinHeight_WhenGivenPoint_ShouldReturnAppropriateBoolean(Vector3 point, Vector3 top, Vector3 bottom, bool onLine)
        {
            return helper.IsWithinHeight(point, top, bottom, onLine);
        }
    }

    internal class PointLineTestData : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new TestCaseData(2.5F, 6).Returns(false);
            yield return new TestCaseData(-6, 5).Returns(false);
            yield return new TestCaseData(6, 10).Returns(false);
        }
    }

    internal class HeightTestData : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new TestCaseData(new Vector3(0, 5, 0), new Vector3(0, 10, 0), new Vector3(0, 0, 0), true).Returns(true);
            yield return new TestCaseData(new Vector3(0, 15, 0), new Vector3(0, 10, 0), new Vector3(0, 0, 0), true).Returns(false);
            yield return new TestCaseData(new Vector3(0, 10, 0), new Vector3(0, 10, 0), new Vector3(0, 0, 0), true).Returns(true);
            yield return new TestCaseData(new Vector3(0, 10, 0), new Vector3(0, 10, 0), new Vector3(0, 0, 0), false).Returns(false);
        }
    }
}