using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Megarachne
{
    public class CurveToGraphPartGH : GH_Component
    {
        public CurveToGraphPartGH()
            : base("Curve To Graph Part", "Curve To Graph Part",
                "Convert curve to Graph Part." +
                " Start point of curve will be the first vertex, end point will be the second vertex, curve will be the edge.",
                "Megarachne", "Graph Part")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "Curve", "Input curve", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Is Directed", "Is Directed",
                "If true = edge will be directed, false = undirected", GH_ParamAccess.item, true);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph Part", "Graph Part", "Created Graph Part", GH_ParamAccess.item);
            pManager.AddGeometryParameter("Geometry", "Geometry", "Geometry of edge and vertices", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve curve = null;
            bool isDirected = true;

            DA.GetData(0, ref curve);
            DA.GetData(1, ref isDirected);
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
            get { return new Guid("8844714a-35d7-48b7-9ef4-9196d9edc266"); }
        }
    }
}