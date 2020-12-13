using System;
using Rhino.Geometry;
using MegarachneEngine;
using Xunit;

namespace MegarachneEngineTests
{
    [Collection("Rhino Collection")]
    public class TestEngine
    {
        #region GraphPart

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

        #endregion

        #region Tools

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0.0001)]
        [InlineData(1, 0, 0, 1, 0, 0, 0.0001)]
        [InlineData(1, 0, 0, 2, 0, 0, 2.0000)]
        [InlineData(1, 0, 1, 1, 0, 1, 0.0001)]
        [InlineData(0, 1, 0, 0, 1, 0, 0.0001)]
        public void TestCheckIfPointsAreSame_True(double x1, double y1, double z1, double x2, double y2, double z2, double tolerance)
        {
            Point3d pointA = new Point3d(x1, y1, z1);
            Point3d pointB = new Point3d(x2, y2, z2);
            bool arePointsTheSame = Tools.CheckIfPointsAreSame(pointA, pointB, tolerance);
            Assert.True(arePointsTheSame);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0.1, 0, 0.0001)]
        [InlineData(1, 0, 0, 5, 0, 0, 0.0001)]
        [InlineData(1, 0, 0, 2, 0, 0, 0.9)]
        [InlineData(1, 0, 1, 1, 0, 0, 0.0001)]
        [InlineData(0, 1, 0, 0, 1, 1, 0.0001)]
        public void TestCheckIfPointsAreSame_False(double x1, double y1, double z1, double x2, double y2, double z2, double tolerance)
        {
            Point3d pointA = new Point3d(x1, y1, z1);
            Point3d pointB = new Point3d(x2, y2, z2);
            bool arePointsTheSame = Tools.CheckIfPointsAreSame(pointA, pointB, tolerance);
            Assert.False(arePointsTheSame);
        }

        #endregion

    }
}
