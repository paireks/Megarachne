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
        public void TestGraphPartConstructor_2Points()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            bool isDirected = false;

            GraphPart graphPart = new GraphPart(pointA, pointB, isDirected);

            Assert.Equal(0.01, graphPart.StartVertex.X);
            Assert.Equal(0.2, graphPart.StartVertex.Y);
            Assert.Equal(0.5, graphPart.StartVertex.Z);
            Assert.Equal(1, graphPart.EndVertex.X);
            Assert.Equal(7, graphPart.EndVertex.Y);
            Assert.Equal(10, graphPart.EndVertex.Z);
            Assert.False(graphPart.IsDirected);
        }

        [Fact]
        public void TestGraphPartConstructor_PointAndVector()
        {
            Point3d point = new Point3d(0.01, 0.2, 0.5);
            Vector3d vector = new Vector3d(1, 7, 10);
            bool isDirected = true;

            GraphPart graphPart = new GraphPart(vector, point, isDirected);

            Assert.Equal(0.01, graphPart.StartVertex.X);
            Assert.Equal(0.2, graphPart.StartVertex.Y);
            Assert.Equal(0.5, graphPart.StartVertex.Z);
            Assert.Equal(1.01, graphPart.EndVertex.X);
            Assert.Equal(7.2, graphPart.EndVertex.Y);
            Assert.Equal(10.5, graphPart.EndVertex.Z);
            Assert.True(graphPart.IsDirected);
        }

        [Fact]
        public void TestGraphPartConstructor_Curve()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            LineCurve line = new LineCurve(pointA, pointB);
            bool isDirected = true;

            GraphPart graphPart = new GraphPart(line, isDirected);

            Assert.Equal(0.01, graphPart.StartVertex.X);
            Assert.Equal(0.2, graphPart.StartVertex.Y);
            Assert.Equal(0.5, graphPart.StartVertex.Z);
            Assert.Equal(1, graphPart.EndVertex.X);
            Assert.Equal(7, graphPart.EndVertex.Y);
            Assert.Equal(10, graphPart.EndVertex.Z);
            Assert.True(graphPart.IsDirected);
        }
    }
}
