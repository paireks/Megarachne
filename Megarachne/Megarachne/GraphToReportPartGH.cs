using System;

using Grasshopper.Kernel;
using MegarachneEngine;

namespace Megarachne
{
    public class GraphToReportPartGH : GH_Component
    {
        public GraphToReportPartGH()
          : base("Graph To Report Part", "Graph To Report Part",
              "Convert graph to Report Part to use it in Pterodactyl. It works decent to visualize small graphs, 100 edges is the limit.",
              "Megarachne", "2. Graph")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Graph to convert", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Direction", "Direction",
                "Set direction, True = from left to right, False = from top to bottom", GH_ParamAccess.item, true);
            pManager.AddBooleanParameter("Show Weights", "Show Weights",
                "True = show weights of edges, False = hide them", GH_ParamAccess.item, true);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Report Part", "Report Part", "Created part of the report", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            bool direction = true;
            bool showWeights = true;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref direction);
            DA.GetData(2, ref showWeights);

            DA.SetData(0, Tools.GraphArrayToReportPart(graph.GraphArray, graph.EdgesWeights, direction, showWeights));
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
            get { return new Guid("10816ff1-9933-4a99-93a7-a9626bafb8e7"); }
        }
    }
}