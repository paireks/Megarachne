using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Megarachne
{
    public class DeconstructGraphPartGH : GH_Component
    {
        public DeconstructGraphPartGH()
            : base("Deconstruct Graph Part", "Deconstruct Graph Part",
                "Deconstruct Graph Part to vertices and edge",
                "Megarachne", "Graph Part")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph Part", "Graph Part", "Input Graph Part", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Edge", "Edge", "Deconstructed edge", GH_ParamAccess.item);
            pManager.AddPointParameter("Vertices", "Vertices", "Deconstructed vertices", GH_ParamAccess.list);

        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {

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
            get { return new Guid("fe094189-35af-4ee1-96d3-412948331aef"); }
        }
    }
}