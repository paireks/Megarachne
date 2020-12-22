using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class VectorToGraphPartGH : GH_Component
    {
        public VectorToGraphPartGH()
          : base("Vector To Graph Part", "Vector To Graph Part",
              "Convert bound vector to the Graph Part." +
              " Point will be the first vertex, vector will be the edge, and at the end of the vector new vertex will be created.",
              "Megarachne", "Graph Part")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddVectorParameter("Vector", "Vector", "Vector input", GH_ParamAccess.item, new Vector3d(1.0, 0, 0));
            pManager.AddPointParameter("Point", "Point", "Start point of the vector", GH_ParamAccess.item,
                new Point3d(0, 0, 0));
            pManager.AddBooleanParameter("Is Directed", "Is Directed",
                "If true = edge will be directed, false = undirected", GH_ParamAccess.item, true);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph Part", "Graph Part", "Created Graph Part", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Vector3d vector = Vector3d.Unset;
            Point3d point = Point3d.Unset;
            bool isDirected = true;

            DA.GetData(0, ref vector);
            DA.GetData(1, ref point);
            DA.GetData(2, ref isDirected);

            GraphPart graphPart = new GraphPart(vector, point, isDirected);

            DA.SetData(0, graphPart);
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
            get { return new Guid("aeebf250-59a0-4395-8ca7-7b27379ac123"); }
        }
    }
}
