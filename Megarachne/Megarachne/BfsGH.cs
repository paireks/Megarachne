using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class BfsGH : GH_Component
    {
        public BfsGH()
          : base("BFS", "BFS",
              "Returns Breadth First Search's previous vertices array",
              "Megarachne", "Algorithm")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Previous Array", "Previous Array",
                "Array of previous vertices indices", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;

            DA.GetData(0, ref graph);

            DA.SetDataList(0, Bfs.GetPrevious(graph));
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
            get { return new Guid("9cf05046-aaa4-42b0-ac42-0ac27f62b00a"); }
        }
    }
}