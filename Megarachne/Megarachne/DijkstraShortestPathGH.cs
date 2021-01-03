using System;
using Grasshopper.Kernel;
using MegarachneEngine;

namespace Megarachne
{
    public class DijkstraShortestPathGH : GH_Component
    {
        public DijkstraShortestPathGH()
          : base("Dijkstra Shortest Path", "Dijkstra Shortest Path",
              "Returns Dijkstra's shortest path",
              "Megarachne", "3. Algorithm")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start Vertex Index", "Start Vertex Index", "Index of start vertex",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("End Vertex Index", "End Vertex Index", "Index of end vertex", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Edges", "Edges", "List of ordered edges (curves) of calculated shortest path",
                GH_ParamAccess.list);
            pManager.AddPointParameter("Vertices", "Vertices", "List of ordered vertices (points) of calculated shortest path",
                GH_ParamAccess.list);
            pManager.AddIntegerParameter("Edges Indexes", "Edges Indexes", "List of ordered edges indexes",
                GH_ParamAccess.list);
            pManager.AddIntegerParameter("Vertices Indexes", "Vertices Indexes",
                "List of ordered indexes of vertices", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            int startVertexIndex = 0;
            int endVertexIndex = 0;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref startVertexIndex);
            DA.GetData(2, ref endVertexIndex);

            Path path = Dijkstra.GetShortestPath(graph, startVertexIndex, endVertexIndex);

            DA.SetDataList(0, path.Edges);
            DA.SetDataList(1, path.Vertices);
            DA.SetDataList(2, path.EdgesIndexes);
            DA.SetDataList(3, path.VerticesIndexes);
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
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
            get { return new Guid("f967b24b-7280-44ac-b92c-ba2877bd6a55"); }
        }
    }
}