using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class MeshToGraphGH : GH_Component
    {
        public MeshToGraphGH()
          : base("Mesh To Graph", "Mesh To Graph",
              "Convert Mesh to Graph",
              "Megarachne", "2. Graph")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "Mesh", "Input mesh", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Created Graph", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;

            DA.GetData(0, ref mesh);

            Graph graph = new Graph(mesh);

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
            get { return new Guid("0927415e-1e70-47b7-b824-1504d1434ed7"); }
        }
    }
}