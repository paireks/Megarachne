using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class GetGraphDegreeGH : GH_Component
    {
        public GetGraphDegreeGH()
          : base("Get Graph Degree", "Get Graph Degree",
              "Description",
              "Megarachne", "Graph")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Graph Degree", "Graph Degree", "Graph degree", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;

            DA.GetData(0, ref graph);

            DA.SetData(0, graph.GetGraphDegree());
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("1d78fd7e-c968-4d85-a5ca-5e9521bab0fb"); }
        }
    }
}