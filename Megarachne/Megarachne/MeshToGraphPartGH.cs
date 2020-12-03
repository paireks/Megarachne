using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Megarachne
{
    public class MeshToGraphPartGH : GH_Component
    {
        public MeshToGraphPartGH()
            : base("Mesh To Graph Part", "Mesh To Graph Part",
                "Convert mesh to Graph Part.",
                "Megarachne", "Graph Part")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "Mesh", "Input mesh", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph Part", "Graph Part", "Created Graph Part", GH_ParamAccess.item);
            pManager.AddGeometryParameter("Geometry", "Geometry", "Geometry of edge and vertices", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;

            DA.GetData(0, ref mesh);
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
            get { return new Guid("7580929f-11eb-428d-83df-bd89dcda2576"); }
        }
    }
}