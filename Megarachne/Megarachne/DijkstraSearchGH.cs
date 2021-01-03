using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class DijkstraSearchGH : GH_Component
    {
        public DijkstraSearchGH()
          : base("Dijkstra Search", "Dijkstra Search",
              "Returns Dijkstra's search array elements",
              "Megarachne", "Algorithm")
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
            pManager.AddIntegerParameter("Previous", "Previous", "Previous vertex array", GH_ParamAccess.list);
            pManager.AddNumberParameter("Weights", "Weights", "Weights to get to vertices array", GH_ParamAccess.list);
            pManager.AddBooleanParameter("IsDone", "IsDone", "Is done Array", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            int startVertexIndex = 0;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref startVertexIndex);

            DijkstraElement[] dijkstraElements = Dijkstra.Search(graph, startVertexIndex);

            int[] array = new int[graph.Vertices.Count];
            double[] weights = new double[graph.Vertices.Count];
            bool[] wasDone = new bool[graph.Vertices.Count];

            for (int i = 0; i < dijkstraElements.Length; i++)
            {
                array[i] = dijkstraElements[i].PreviousVertexIndex;
                weights[i] = dijkstraElements[i].Priority;
                wasDone[i] = dijkstraElements[i].IsDone;
            }

            DA.SetDataList(0, array);
            DA.SetDataList(1, weights);
            DA.SetDataList(2, wasDone);
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
            get { return new Guid("1f0700ca-fa58-47ca-b32c-d9dc203fd2c4"); }
        }
    }
}