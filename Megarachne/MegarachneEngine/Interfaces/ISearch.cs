﻿using System.Collections.Generic;
using Rhino.Geometry;

namespace MegarachneEngine.Interfaces
{
    public interface ISearch
    {
        List<Point3d> VisitedVertices { get; }
        int[] PreviousArray { get; }
        Graph Graph { get; }
    }
}