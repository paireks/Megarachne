using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using MegarachneEngine;
using Rhino.Geometry;

namespace Megarachne
{
    public class BfsSearchGH : GH_Component
    {
        public BfsSearchGH()
          : base("BFS Search", "BFS Search",
              "Returns Breadth First Search's previous vertices array",
              "Megarachne", "3. Algorithm")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graph", "Graph", "Input graph", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start Vertex Index", "Start Vertex Index",
                "It will start the search with this given vertex", GH_ParamAccess.item, 0);
            pManager.AddBooleanParameter("Keep Searching", "Keep Searching", 
                "True = algorithm will keep searching even if the graph is not connected. False = Naturally stops for not connected graph.", GH_ParamAccess.item, true);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Visited", "Visited", "Visited vertices", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Previous Array", "Previous Array", "Array of previous vertices indexes", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Graph graph = null;
            int startVertex = 0;
            bool keepSearching = true;

            DA.GetData(0, ref graph);
            DA.GetData(1, ref startVertex);
            DA.GetData(2, ref keepSearching);

            Bfs bfs = new Bfs(graph);
            bfs.Search(startVertex, keepSearching);

            DA.SetDataList(0, bfs.VisitedVertices);
            DA.SetDataList(1, bfs.PreviousArray);
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