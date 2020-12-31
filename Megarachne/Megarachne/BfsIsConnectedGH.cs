using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class BfsIsConnectedGH : GH_Component
    {

        public BfsIsConnectedGH()
          : base("BFS Is Graph Connected", "BFS Is Graph Connected",
              "Check if the graph is connected by using Breadth First Search",
              "Megarachne", "Algorithm")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start Vertex", "Start Vertex", "Index of start vertex",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Is Connected", "Is Connected",
                "True = graph is connected, False = graph is not connected", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            int startVertex = 0;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref startVertex);

            DA.SetData(0, Bfs.IsGraphConnected(graph, startVertex));
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
            get { return new Guid("6c1e9cea-5a8e-4ebc-9783-ff476606ba6a"); }
        }
    }
}