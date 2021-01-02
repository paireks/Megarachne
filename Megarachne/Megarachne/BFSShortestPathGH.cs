using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class BfsShortestPathGH : GH_Component
    {
        public BfsShortestPathGH()
          : base("BFS Shortest Path", "BFS Shortest Path",
              "Returns Breadth First Search's shortest path as a list of ordered vertices",
              "Megarachne", "Algorithm")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start Vertex", "Start Vertex", "Index of start vertex",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("End Vertex", "End Vertex", "Index of end vertex", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            /*pManager.AddIntegerParameter("Vertices List", "Vertices List",
                "List of ordered vertices as the shortest path", GH_ParamAccess.list);*/
            pManager.AddCurveParameter("Path", "Path", "List of ordered edges of calculated shortest path",
                GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            int startVertexIndex = 0;
            int endVertexIndex = 0;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref startVertexIndex);
            DA.GetData(2, ref endVertexIndex);

            DA.SetDataList(0, Bfs.GetShortestPath(graph, startVertexIndex, endVertexIndex));
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("7c3906f8-861b-41f5-8fc9-abceca13dfc9"); }
        }
    }
}