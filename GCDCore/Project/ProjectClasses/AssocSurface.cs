﻿using System.IO;
using System.Xml;

namespace GCDCore.Project
{
    public class AssocSurface : GCDProjectItem
    {
        public readonly ProjectRaster Raster;
        public string AssocSurfaceType { get; set; }
        public readonly DEMSurvey DEM;

        public AssocSurface(string name, FileInfo rasterPath, string sType, DEMSurvey dem)
            : base(name)
        {
            Raster = new ProjectRaster(rasterPath);
            AssocSurfaceType = sType;
            DEM = dem;
        }

        public void Serialize(XmlDocument xmlDoc, XmlNode nodParent)
        {
            XmlNode nodAssoc = nodParent.AppendChild(xmlDoc.CreateElement("Assoc"));
            nodAssoc.AppendChild(xmlDoc.CreateElement("Name")).InnerText = Name;
            nodAssoc.AppendChild(xmlDoc.CreateElement("Type")).InnerText = AssocSurfaceType.ToString();
            nodAssoc.AppendChild(xmlDoc.CreateElement("Path")).InnerText = string.Empty;
        }
    }
}
