using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class DeconstructGraphGH : GH_Component
    {
        public DeconstructGraphGH()
          : base("Deconstruct Graph", "Deconstruct Graph",
              "Deconstruct Graph to it's Graph Array, Vertices, Edges",
              "Megarachne", "Graph")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input Graph", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Graph Array", "Graph Array", "Deconstructed Graph Array", GH_ParamAccess.list);
            pManager.AddPointParameter("Vertices", "Vertices", "Deconstructed Vertices", GH_ParamAccess.list);
            pManager.AddCurveParameter("Edges", "Edges", "Deconstructed edges", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;

            DA.GetData(0, ref graph);

            DA.SetDataList(0, Tools.ShowGraphArrayStringRepresentation(graph.GraphArray));
            DA.SetDataList(1, graph.Vertices);
            DA.SetDataList(2, graph.Edges);
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
            get { return new Guid("7d5da242-9775-4b02-9b28-c0deb5800027"); }
        }
    }
}