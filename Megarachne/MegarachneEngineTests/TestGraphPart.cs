using System;
using Rhino.Geometry;
using MegarachneEngine;
using Xunit;

namespace MegarachneEngineTests
{
    [Collection("Rhino Collection")]
    public class TestGraphPart
    {
        [Fact]
        public void TestGraphPartConstructor()
        {
            Point3d pointA = new Point3d(0, 0, 0);
            Point3d pointB = new Point3d(1, 0, 0);
            bool isDirected = false;

            GraphPart graphPart = new GraphPart(pointA, pointB, isDirected);

            Assert.Equal(pointA.X, pointA.X);
        }
    }
}
