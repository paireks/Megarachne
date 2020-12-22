using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class CreateGraphGH : GH_Component
    {
        public CreateGraphGH()
          : base("Create Graph", "Create Graph",
              "Create Graph from Graph Parts",
              "Megarachne", "Graph")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph Parts", "Graph Parts", "Input Graph Parts as list", GH_ParamAccess.list);
            pManager.AddNumberParameter("Tolerance", "Tolerance",
                "Tolerance to decide if two vertices with the same coordinates should be treated as one",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Created Graph", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<GraphPart> graphParts = new List<GraphPart>();
            double tolerance = double.NaN;

            DA.GetDataList(0, graphParts);
            DA.GetData(1, ref tolerance);

            Graph graph = new Graph(graphParts, tolerance);

            DA.SetData(0, graph);
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
            get { return new Guid("ab39e396-5115-4991-b81e-88f88a2b3e9b"); }
        }
    }
}