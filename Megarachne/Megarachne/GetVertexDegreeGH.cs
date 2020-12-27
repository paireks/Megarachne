using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class GetVertexDegreeGH : GH_Component
    {
        public GetVertexDegreeGH()
          : base("Get Vertex Degree", "Get Vertex Degree",
              "Get vertex degree",
              "Megarachne", "Graph")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Vertex Index", "Vertex Index", "Input vertex index", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Vertex Degree", "Vertex Degree", "Vertex degree", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            int vertexIndex = 0;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref vertexIndex);

            DA.SetData(0, graph.GetVertexDegree(vertexIndex));
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
            get { return new Guid("5a7f1257-798c-4e2e-8fa3-175bcbbbcf64"); }
        }
    }
}