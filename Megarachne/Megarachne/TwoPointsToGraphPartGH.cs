using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class TwoPointsToGraphPartGH : GH_Component
    {
        public TwoPointsToGraphPartGH()
          : base("Two Points To Graph Part", "Two Points To Graph Part",
              "Convert two points to Graph Part." +
              " First point will be the first vertex, second point will be the second vertex, edge will be between them.",
              "Megarachne", "1. Graph Part")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point A", "Point A", "Start point", GH_ParamAccess.item,
                new Point3d(0, 0, 0));
            pManager.AddPointParameter("Point B", "Point B", "End point", GH_ParamAccess.item,
                new Point3d(1, 0, 0));
            pManager.AddBooleanParameter("Is Directed", "Is Directed",
                "If true = edge will be directed, false = undirected", GH_ParamAccess.item, true);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph Part", "Graph Part", "Created Graph Part", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d pointA = Point3d.Unset;
            Point3d pointB = Point3d.Unset;
            bool isDirected = true;

            DA.GetData(0, ref pointA);
            DA.GetData(1, ref pointB);
            DA.GetData(2, ref isDirected);

            GraphPart graphPart = new GraphPart(pointA, pointB, isDirected);

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
            get { return new Guid("4b48d3b2-e686-40a4-8c8b-9e038b4cb69d"); }
        }
    }
}