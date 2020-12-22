using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class MeshToGraphPartGH : GH_Component
    {
        public MeshToGraphPartGH()
            : base("Mesh To Graph Parts", "Mesh To Graph Parts",
                "Convert mesh to Graph Parts.",
                "Megarachne", "Graph Part")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "Mesh", "Input mesh", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph Parts", "Graph Parts", "Created Graph Parts", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;

            DA.GetData(0, ref mesh);

            List<GraphPart> graphParts = new List<GraphPart>();
            for (int i = 0; i < mesh.TopologyEdges.Count; i++)
            {
                graphParts.Add(new GraphPart(new LineCurve(mesh.TopologyEdges.EdgeLine(i)),false));
            }

            DA.SetDataList(0, graphParts);
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