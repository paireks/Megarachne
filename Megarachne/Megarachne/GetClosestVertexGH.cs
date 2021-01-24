using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class GetClosestVertexGH : GH_Component
    {
        public GetClosestVertexGH()
          : base("Get Closest Vertex", "Get Closest Vertex",
              "Get vertex closest to given point, return it's index",
              "Megarachne", "2. Graph")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "Point", "Input point to find closest vertex", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Vertex Index", "Vertex Index", "Closest vertex index", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            Point3d point3d = Point3d.Unset;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref point3d);

            DA.SetData(0, graph.GetClosestVertexIndex(point3d));
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.GetClosestVertex;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("bbb0cd82-3155-4545-9574-4b5b8384a548"); }
        }
    }
}