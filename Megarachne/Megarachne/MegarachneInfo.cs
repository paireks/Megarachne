using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Megarachne
{
    public class MegarachneInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Megarachne";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override string Description
        {
            get
            {
                return "Graph theory plugin for Grasshopper";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("dbefe6f6-50df-458b-a32b-61a514b3f101");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "code-structures Wojciech Radaczyński";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
        public override string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }
    }
}
