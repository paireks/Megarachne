using System;
using System.Collections.Generic;
using Rhino.Geometry;
using MegarachneEngine;
using Xunit;

namespace MegarachneEngineTests
{
    [Collection("Rhino Collection")]
    public class TestEngine
    {
        #region AdjacencyList

        [Fact]
        public void TestAdjacencyListStringRepresentation_3PointsTriangle()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            List<string> expected = new List<string>();
            expected.Add("1;");
            expected.Add("2;");
            expected.Add("0;");

            Assert.Equal(expected, graph.AdjacencyList.GetStringRepresentation());
        }

        [Fact]
        public void TestAdjacencyListStringRepresentation_3PointsTriangleNotDirected()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            List<string> expected = new List<string>();
            expected.Add("1;2;");
            expected.Add("0;2;");
            expected.Add("1;0;");

            Assert.Equal(expected, graph.AdjacencyList.GetStringRepresentation());
        }

        #endregion

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

        [Fact]
        public void TestShowGraphArrayStringRepresentation_3PointsTriangle()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            List<string> expected = new List<string>();
            expected.Add("0;1");
            expected.Add("1;2");
            expected.Add("2;0");

            Assert.Equal(expected, Tools.ShowGraphArrayStringRepresentation(graph.GraphArray));
        }

        [Fact]
        public void TestShowGraphArrayStringRepresentation_3PointsTriangleNotDirected()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            List<string> expected = new List<string>();
            expected.Add("0;1");
            expected.Add("1;0");
            expected.Add("1;2");
            expected.Add("2;1");
            expected.Add("2;0");
            expected.Add("0;2");

            Assert.Equal(expected, Tools.ShowGraphArrayStringRepresentation(graph.GraphArray));
        }

        #endregion

        #region Graph

        [Fact]
        public void TestGraphConstructor_2Points_GraphArray()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart = new GraphPart(pointA, pointB, false);

            Graph graph = new Graph(new List<GraphPart>{graphPart}, 0.001);
            int[,] expectedGraphArray = new int[2,2];

            expectedGraphArray[0, 0] = 0;
            expectedGraphArray[1, 0] = 1;
            expectedGraphArray[0, 1] = 1;
            expectedGraphArray[1, 1] = 0;
            Assert.Equal(expectedGraphArray.Length, graph.GraphArray.Length);
            Assert.Equal(expectedGraphArray, graph.GraphArray);
        }

        [Fact]
        public void TestGraphConstructor_2Points_AdjacencyList()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart = new GraphPart(pointA, pointB, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart }, 0.001);
            List<int>[] expectedAdjacencyListVertices = new List<int>[2];
            List<int>[] expectedAdjacencyListEdges = new List<int>[2];

            expectedAdjacencyListVertices[0] = new List<int> { 1 };
            expectedAdjacencyListVertices[1] = new List<int> { 0 };

            expectedAdjacencyListEdges[0] = new List<int> { 0 };
            expectedAdjacencyListEdges[1] = new List<int> { 1 };

            Assert.Equal(expectedAdjacencyListVertices, graph.AdjacencyList.Vertices);
            Assert.Equal(expectedAdjacencyListEdges, graph.AdjacencyList.Edges);
        }

        [Fact]
        public void TestGraphDegrees_2Points_VertexDegrees()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart = new GraphPart(pointA, pointB, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart }, 0.001);

            Assert.Equal(2, graph.GetVertexDegree(0));
            Assert.Equal(1, graph.GetVertexOutdegree(0));
            Assert.Equal(1, graph.GetVertexIndegree(0));

            Assert.Equal(2, graph.GetVertexDegree(1));
            Assert.Equal(1, graph.GetVertexOutdegree(1));
            Assert.Equal(1, graph.GetVertexIndegree(1));
        }

        [Fact]
        public void TestGraphDegrees_2Points_Degree()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart = new GraphPart(pointA, pointB, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart }, 0.001);

            Assert.Equal(2, graph.GetGraphDegree());
        }

        [Fact]
        public void TestGraphConstructor_2Points2Times_GraphArray()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);
            int[,] expectedGraphArray = new int[2, 2];

            expectedGraphArray[0, 0] = 0;
            expectedGraphArray[1, 0] = 1;
            expectedGraphArray[0, 1] = 0;
            expectedGraphArray[1, 1] = 1;
            Assert.Equal(expectedGraphArray.Length, graph.GraphArray.Length);
            Assert.Equal(expectedGraphArray, graph.GraphArray);
        }

        [Fact]
        public void TestGraphConstructor_2Points2Times_AdjacencyList()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);
            List<int>[] expectedAdjacencyListVertices = new List<int>[2];
            List<int>[] expectedAdjacencyListEdges = new List<int>[2];

            expectedAdjacencyListVertices[0] = new List<int> { 1, 1 };
            expectedAdjacencyListVertices[1] = new List<int>();
            expectedAdjacencyListEdges[0] = new List<int> { 0, 1 };
            expectedAdjacencyListEdges[1] = new List<int>();

            Assert.Equal(expectedAdjacencyListVertices, graph.AdjacencyList.Vertices);
            Assert.Equal(expectedAdjacencyListEdges, graph.AdjacencyList.Edges);
        }

        [Fact]
        public void TestGraphDegrees_2Points2Times_VertexDegrees()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);

            Assert.Equal(2, graph.GetVertexDegree(0));
            Assert.Equal(2, graph.GetVertexOutdegree(0));
            Assert.Equal(0, graph.GetVertexIndegree(0));

            Assert.Equal(2, graph.GetVertexDegree(1));
            Assert.Equal(0, graph.GetVertexOutdegree(1));
            Assert.Equal(2, graph.GetVertexIndegree(1));
        }

        [Fact]
        public void TestGraphDegrees_2Points2Times_Degree()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);

            Assert.Equal(2, graph.GetGraphDegree());
        }

        [Fact]
        public void TestGraphConstructor_3Points_GraphArray()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);
            int[,] expectedGraphArray = new int[2, 2];

            expectedGraphArray[0, 0] = 0;
            expectedGraphArray[1, 0] = 1;
            expectedGraphArray[0, 1] = 1;
            expectedGraphArray[1, 1] = 2;
            Assert.Equal(expectedGraphArray.Length, graph.GraphArray.Length);
            Assert.Equal(expectedGraphArray, graph.GraphArray);
        }

        [Fact]
        public void TestGraphConstructor_3Points_AdjacencyList()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);
            List<int>[] expectedAdjacencyListVertices = new List<int>[3];
            List<int>[] expectedAdjacencyListEdges = new List<int>[3];

            expectedAdjacencyListVertices[0] = new List<int> { 1 };
            expectedAdjacencyListVertices[1] = new List<int> { 2 };
            expectedAdjacencyListVertices[2] = new List<int>();

            expectedAdjacencyListEdges[0] = new List<int> { 0 };
            expectedAdjacencyListEdges[1] = new List<int> { 1 };
            expectedAdjacencyListEdges[2] = new List<int>();

            Assert.Equal(expectedAdjacencyListVertices, graph.AdjacencyList.Vertices);
            Assert.Equal(expectedAdjacencyListEdges, graph.AdjacencyList.Edges);
        }

        [Fact]
        public void TestGraphDegrees_3Points_VertexDegrees()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);

            Assert.Equal(1, graph.GetVertexDegree(0));
            Assert.Equal(1, graph.GetVertexOutdegree(0));
            Assert.Equal(0, graph.GetVertexIndegree(0));

            Assert.Equal(2, graph.GetVertexDegree(1));
            Assert.Equal(1, graph.GetVertexOutdegree(1));
            Assert.Equal(1, graph.GetVertexIndegree(1));

            Assert.Equal(1, graph.GetVertexDegree(2));
            Assert.Equal(0, graph.GetVertexOutdegree(2));
            Assert.Equal(1, graph.GetVertexIndegree(2));
        }

        [Fact]
        public void TestGraphDegrees_3Points_Degree()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2 }, 0.001);

            Assert.Equal(2, graph.GetGraphDegree());
        }

        [Fact]
        public void TestGraphConstructor_3PointsSame1Edge_GraphArray()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            int[,] expectedGraphArray = new int[2, 3];

            expectedGraphArray[0, 0] = 0;
            expectedGraphArray[1, 0] = 1;
            expectedGraphArray[0, 1] = 1;
            expectedGraphArray[1, 1] = 2;
            expectedGraphArray[0, 2] = 0;
            expectedGraphArray[1, 2] = 1;
            Assert.Equal(expectedGraphArray.Length, graph.GraphArray.Length);
            Assert.Equal(expectedGraphArray, graph.GraphArray);
        }

        [Fact]
        public void TestGraphConstructor_3PointsSame1Edge_AdjacencyList()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            List<int>[] expectedAdjacencyListVertices = new List<int>[3];
            List<int>[] expectedAdjacencyListEdges = new List<int>[3];

            expectedAdjacencyListVertices[0] = new List<int> { 1, 1 };
            expectedAdjacencyListVertices[1] = new List<int> { 2 };
            expectedAdjacencyListVertices[2] = new List<int>();

            expectedAdjacencyListEdges[0] = new List<int> { 0, 2 };
            expectedAdjacencyListEdges[1] = new List<int> { 1 };
            expectedAdjacencyListEdges[2] = new List<int>();

            Assert.Equal(expectedAdjacencyListVertices, graph.AdjacencyList.Vertices);
            Assert.Equal(expectedAdjacencyListEdges, graph.AdjacencyList.Edges);
        }

        [Fact]
        public void TestGraphDegrees_3PointsSame1Edge_VertexDegrees()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            Assert.Equal(2, graph.GetVertexDegree(0));
            Assert.Equal(2, graph.GetVertexOutdegree(0));
            Assert.Equal(0, graph.GetVertexIndegree(0));

            Assert.Equal(3, graph.GetVertexDegree(1));
            Assert.Equal(1, graph.GetVertexOutdegree(1));
            Assert.Equal(2, graph.GetVertexIndegree(1));

            Assert.Equal(1, graph.GetVertexDegree(2));
            Assert.Equal(0, graph.GetVertexOutdegree(2));
            Assert.Equal(1, graph.GetVertexIndegree(2));
        }

        [Fact]
        public void TestGraphDegrees_3PointsSame1Edge_Degree()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointA, pointB, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            Assert.Equal(3, graph.GetGraphDegree());
        }

        [Fact]
        public void TestGraphConstructor_3PointsTriangle_GraphArray()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            int[,] expectedGraphArray = new int[2, 3];

            expectedGraphArray[0, 0] = 0;
            expectedGraphArray[1, 0] = 1;
            expectedGraphArray[0, 1] = 1;
            expectedGraphArray[1, 1] = 2;
            expectedGraphArray[0, 2] = 2;
            expectedGraphArray[1, 2] = 0;
            Assert.Equal(expectedGraphArray.Length, graph.GraphArray.Length);
            Assert.Equal(expectedGraphArray, graph.GraphArray);
        }

        [Fact]
        public void TestGraphConstructor_3PointsTriangle_AdjacencyList()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            List<int>[] expectedAdjacencyListVertices = new List<int>[3];
            List<int>[] expectedAdjacencyListEdges = new List<int>[3];

            expectedAdjacencyListVertices[0] = new List<int> { 1 };
            expectedAdjacencyListVertices[1] = new List<int> { 2 };
            expectedAdjacencyListVertices[2] = new List<int> { 0 };

            expectedAdjacencyListEdges[0] = new List<int> { 0 };
            expectedAdjacencyListEdges[1] = new List<int> { 1 };
            expectedAdjacencyListEdges[2] = new List<int> { 2 };

            Assert.Equal(expectedAdjacencyListVertices, graph.AdjacencyList.Vertices);
            Assert.Equal(expectedAdjacencyListEdges, graph.AdjacencyList.Edges);
        }

        [Fact]
        public void TestGraphDegrees_3PointsTriangle_VertexDegrees()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            Assert.Equal(2, graph.GetVertexDegree(0));
            Assert.Equal(1, graph.GetVertexOutdegree(0));
            Assert.Equal(1, graph.GetVertexIndegree(0));

            Assert.Equal(2, graph.GetVertexDegree(1));
            Assert.Equal(1, graph.GetVertexOutdegree(1));
            Assert.Equal(1, graph.GetVertexIndegree(1));

            Assert.Equal(2, graph.GetVertexDegree(2));
            Assert.Equal(1, graph.GetVertexOutdegree(2));
            Assert.Equal(1, graph.GetVertexIndegree(2));
        }

        [Fact]
        public void TestGraphDegrees_3PointsTriangle_Degree()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, true);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, true);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, true);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            Assert.Equal(2, graph.GetGraphDegree());
        }

        [Fact]
        public void TestGraphConstructor_3PointsTriangleNotDirected_GraphArray()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            int[,] expectedGraphArray = new int[2, 6];

            expectedGraphArray[0, 0] = 0;
            expectedGraphArray[1, 0] = 1;
            expectedGraphArray[0, 1] = 1;
            expectedGraphArray[1, 1] = 0;
            expectedGraphArray[0, 2] = 1;
            expectedGraphArray[1, 2] = 2;
            expectedGraphArray[0, 3] = 2;
            expectedGraphArray[1, 3] = 1;
            expectedGraphArray[0, 4] = 2;
            expectedGraphArray[1, 4] = 0;
            expectedGraphArray[0, 5] = 0;
            expectedGraphArray[1, 5] = 2;
            Assert.Equal(expectedGraphArray.Length, graph.GraphArray.Length);
            Assert.Equal(expectedGraphArray, graph.GraphArray);
        }

        [Fact]
        public void TestGraphConstructor_3PointsTriangleNotDirected_AdjacencyList()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);
            List<int>[] expectedAdjacencyListVertices = new List<int>[3];
            List<int>[] expectedAdjacencyListEdges = new List<int>[3];

            expectedAdjacencyListVertices[0] = new List<int> { 1, 2 };
            expectedAdjacencyListVertices[1] = new List<int> { 0, 2 };
            expectedAdjacencyListVertices[2] = new List<int> { 1, 0 };

            expectedAdjacencyListEdges[0] = new List<int> { 0, 5 };
            expectedAdjacencyListEdges[1] = new List<int> { 1, 2 };
            expectedAdjacencyListEdges[2] = new List<int> { 3, 4 };

            Assert.Equal(expectedAdjacencyListVertices, graph.AdjacencyList.Vertices);
            Assert.Equal(expectedAdjacencyListEdges, graph.AdjacencyList.Edges);
        }

        [Fact]
        public void TestGraphDegrees_3PointsTriangleNotDirected_VertexDegrees()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            Assert.Equal(4, graph.GetVertexDegree(0));
            Assert.Equal(2, graph.GetVertexOutdegree(0));
            Assert.Equal(2, graph.GetVertexIndegree(0));

            Assert.Equal(4, graph.GetVertexDegree(1));
            Assert.Equal(2, graph.GetVertexOutdegree(1));
            Assert.Equal(2, graph.GetVertexIndegree(1));

            Assert.Equal(4, graph.GetVertexDegree(2));
            Assert.Equal(2, graph.GetVertexOutdegree(2));
            Assert.Equal(2, graph.GetVertexIndegree(2));
        }

        [Fact]
        public void TestGraphDegrees_3PointsTriangleNotDirected_Degree()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            Assert.Equal(4, graph.GetGraphDegree());
        }

        [Fact]
        public void TestGraphToString_3PointsTriangleNotDirected_String()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            string expected = "Graph\r\nVertices: 3\r\nEdges: 6";

            Assert.Equal(expected, graph.ToString());
        }

        [Fact]
        public void TestGraphGetClosestVertexIndex_3PointsTriangleNotDirected_Index()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3 }, 0.001);

            Point3d pointToFindClosestVertex = new Point3d(0.9,6.9,11);

            Assert.Equal(1, graph.GetClosestVertexIndex(pointToFindClosestVertex));
        }

        [Fact]
        public void TestGraphConstructor_Mesh_GraphArray()
        {
            Mesh mesh = new Mesh();
            mesh.Vertices.Add(0.0, 0.0, 1.0); //0
            mesh.Vertices.Add(1.0, 0.0, 1.0); //1
            mesh.Vertices.Add(0.0, 1.0, 1.0); //2

            mesh.Faces.AddFace(0, 1, 2);

            int[,] expectedGraphArray = new int[2, 6];

            expectedGraphArray[0, 0] = 0;
            expectedGraphArray[1, 0] = 1;
            expectedGraphArray[0, 1] = 1;
            expectedGraphArray[1, 1] = 0;
            expectedGraphArray[0, 2] = 0;
            expectedGraphArray[1, 2] = 2;
            expectedGraphArray[0, 3] = 2;
            expectedGraphArray[1, 3] = 0;
            expectedGraphArray[0, 4] = 1;
            expectedGraphArray[1, 4] = 2;
            expectedGraphArray[0, 5] = 2;
            expectedGraphArray[1, 5] = 1;

            Graph graph = new Graph(mesh);

            Assert.Equal(expectedGraphArray.Length, graph.GraphArray.Length);
            Assert.Equal(expectedGraphArray, graph.GraphArray);
        }

        [Fact]
        public void TestGraphConstructor_Mesh_AdjacencyList()
        {
            Mesh mesh = new Mesh();
            mesh.Vertices.Add(0.0, 0.0, 1.0); //0
            mesh.Vertices.Add(1.0, 0.0, 1.0); //1
            mesh.Vertices.Add(0.0, 1.0, 1.0); //2

            mesh.Faces.AddFace(0, 1, 2);

            List<int>[] expectedAdjacencyListVertices = new List<int>[3];
            List<int>[] expectedAdjacencyListEdges = new List<int>[3];

            expectedAdjacencyListVertices[0] = new List<int> { 1, 2 };
            expectedAdjacencyListVertices[1] = new List<int> { 0, 2 };
            expectedAdjacencyListVertices[2] = new List<int> { 0, 1 };

            expectedAdjacencyListEdges[0] = new List<int> { 0, 2 };
            expectedAdjacencyListEdges[1] = new List<int> { 1, 4 };
            expectedAdjacencyListEdges[2] = new List<int> { 3, 5 };

            Graph graph = new Graph(mesh);

            Assert.Equal(expectedAdjacencyListVertices, graph.AdjacencyList.Vertices);
            Assert.Equal(expectedAdjacencyListEdges, graph.AdjacencyList.Edges);
        }

        #endregion

        [Fact]

        public void TestDijkstra()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            Point3d pointD = new Point3d(3, 4, 11);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);
            GraphPart graphPart4 = new GraphPart(pointC, pointD, false);
            GraphPart graphPart5 = new GraphPart(pointB, pointD, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3, graphPart4, graphPart5 }, 0.001);

            Dijkstra dijkstra = new Dijkstra(graph);
            dijkstra.Search(0);
        }

        [Fact]

        public void TestDijkstraShortest()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            Point3d pointD = new Point3d(3, 4, 11);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);
            GraphPart graphPart4 = new GraphPart(pointC, pointD, false);
            GraphPart graphPart5 = new GraphPart(pointB, pointD, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3, graphPart4, graphPart5 }, 0.001);

            Dijkstra dijkstra = new Dijkstra(graph);
            dijkstra.GetShortestPath(0, 3);
        }

        [Fact]

        public void TestAStarShortest()
        {
            Point3d pointA = new Point3d(0.01, 0.2, 0.5);
            Point3d pointB = new Point3d(1, 7, 10);
            Point3d pointC = new Point3d(1.2, 5.3, 10);
            Point3d pointD = new Point3d(3, 4, 11);
            GraphPart graphPart1 = new GraphPart(pointA, pointB, false);
            GraphPart graphPart2 = new GraphPart(pointB, pointC, false);
            GraphPart graphPart3 = new GraphPart(pointC, pointA, false);
            GraphPart graphPart4 = new GraphPart(pointC, pointD, false);
            GraphPart graphPart5 = new GraphPart(pointB, pointD, false);

            Graph graph = new Graph(new List<GraphPart> { graphPart1, graphPart2, graphPart3, graphPart4, graphPart5 }, 0.001);

            AStar aStar = new AStar(graph);
            aStar.GetShortestPath(0, 3);
        }
    }
}
