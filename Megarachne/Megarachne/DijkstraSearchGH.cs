using System;
using Grasshopper.Kernel;
using MegarachneEngine;

namespace Megarachne
{
    public class DijkstraSearchGH : GH_Component
    {
        public DijkstraSearchGH()
          : base("Dijkstra Search", "Dijkstra Search",
              "Returns Dijkstra's search array elements",
              "Megarachne", "3. Algorithm")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start Vertex Index", "Start Vertex Index", "Index of start vertex",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Visited", "Visited", "Visited vertices", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Previous Array", "Previous Array", "Array of previous vertices indexes", GH_ParamAccess.list);
            pManager.AddNumberParameter("Weights Array", "Weights Array", "Array of weights", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            int startVertexIndex = 0;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref startVertexIndex);

            Dijkstra dijkstra = new Dijkstra(graph);
            dijkstra.Search(startVertexIndex);

            DA.SetDataList(0, dijkstra.VisitedVertices);
            DA.SetDataList(1, dijkstra.PreviousArray);
            DA.SetDataList(2, dijkstra.Weights);
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.DijSearch;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("1f0700ca-fa58-47ca-b32c-d9dc203fd2c4"); }
        }
    }
}