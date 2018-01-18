﻿using System;
using System.IO;
using System.Collections.Generic;
using GCDConsoleLib;
using GCDConsoleLib.GCD;
using System.Xml;

namespace GCDCore.Project
{
    public class DoDBase : GCDProjectItem
    {
        public readonly DirectoryInfo Folder;
        public readonly DEMSurvey NewDEM;
        public readonly DEMSurvey OldDEM;

        public GCDProjectRasterItem RawDoD { get; internal set; }
        public GCDProjectRasterItem ThrDoD { get; internal set; }

        public HistogramPair Histograms { get; set; }
        public FileInfo SummaryXML { get; set; }
        public readonly DoDStats Statistics;

        public Dictionary<string, BudgetSegregation> BudgetSegregations { get; internal set; }

        public DoDBase(string name, DirectoryInfo folder, DEMSurvey newDEM, DEMSurvey oldDEM, Raster rawDoD, Raster thrDoD, HistogramPair histograms, FileInfo summaryXML, DoDStats stats)
            : base(name)
        {
            Folder = folder;
            NewDEM = newDEM;
            OldDEM = oldDEM;
            RawDoD = new GCDProjectRasterItem("Raw DoD", rawDoD);
            ThrDoD = new GCDProjectRasterItem("Thresholded DoD", thrDoD);
            Histograms = histograms;
            SummaryXML = summaryXML;
            Statistics = stats;
            BudgetSegregations = new Dictionary<string, BudgetSegregation>();
        }

        public DoDBase(string name, DirectoryInfo folder, DEMSurvey newDEM, DEMSurvey oldDEM, FileInfo rawDoD, FileInfo thrDoD, HistogramPair histograms, FileInfo summaryXML, DoDStats stats)
                  : base(name)
        {
            Folder = folder;
            NewDEM = newDEM;
            OldDEM = oldDEM;
            RawDoD = new GCDProjectRasterItem("Raw DoD", rawDoD);
            ThrDoD = new GCDProjectRasterItem("Thresholded DoD", thrDoD);
            Histograms = histograms;
            SummaryXML = summaryXML;
            Statistics = stats;
            BudgetSegregations = new Dictionary<string, BudgetSegregation>();
        }

        public DoDBase(DoDBase dod)
            : base(dod.Name)
        {
            Folder = dod.Folder;
            NewDEM = dod.NewDEM;
            OldDEM = dod.OldDEM;
            RawDoD = dod.RawDoD;
            ThrDoD = dod.ThrDoD;
            Histograms = dod.Histograms;
            SummaryXML = dod.SummaryXML;
            Statistics = dod.Statistics;

            // Need to rebuild BS library so that the internal DoD references point to this object, and not the argument DoD
            BudgetSegregations = new Dictionary<string, BudgetSegregation>();
            foreach (BudgetSegregation bs in dod.BudgetSegregations.Values)
            {
                BudgetSegregations[bs.Name] = new BudgetSegregation(bs.Name, bs.Folder, bs.PolygonMask, bs.MaskField, this, dod.SummaryXML, null);
                foreach (BudgetSegregationClass bsc in bs.Classes.Values)
                    BudgetSegregations[bs.Name].Classes.Add(bsc.Name, bsc);
            }
        }

        public bool IsBudgetSegNameUnique(string name, BudgetSegregation ignore)
        {
            return BudgetSegregations.ContainsKey(name) ? BudgetSegregations[name] == ignore : true;
        }

        public virtual XmlNode Serialize(XmlDocument xmlDoc, XmlNode nodParent)
        {
            XmlNode nodDoD = nodParent.AppendChild(xmlDoc.CreateElement("DoD"));
            nodDoD.AppendChild(xmlDoc.CreateElement("Name")).InnerText = Name;
            nodDoD.AppendChild(xmlDoc.CreateElement("Folder")).InnerText = ProjectManager.Project.GetRelativePath(Folder.FullName);
            nodDoD.AppendChild(xmlDoc.CreateElement("NewDEM")).InnerText = NewDEM.Name;
            nodDoD.AppendChild(xmlDoc.CreateElement("OldDEM")).InnerText = OldDEM.Name;
            nodDoD.AppendChild(xmlDoc.CreateElement("RawDoD")).InnerText = ProjectManager.Project.GetRelativePath(RawDoD.Raster.GISFileInfo);
            nodDoD.AppendChild(xmlDoc.CreateElement("ThrDoD")).InnerText = ProjectManager.Project.GetRelativePath(ThrDoD.Raster.GISFileInfo);
            nodDoD.AppendChild(xmlDoc.CreateElement("RawHistogram")).InnerText = ProjectManager.Project.GetRelativePath(Histograms.Raw.Path);
            nodDoD.AppendChild(xmlDoc.CreateElement("ThrHistogram")).InnerText = ProjectManager.Project.GetRelativePath(Histograms.Thr.Path);
            nodDoD.AppendChild(xmlDoc.CreateElement("SummaryXML")).InnerText = ProjectManager.Project.GetRelativePath(SummaryXML);

            SerializeDoDStatistics(xmlDoc, nodDoD.AppendChild(xmlDoc.CreateElement("Statistics")), Statistics);

            if (BudgetSegregations.Count > 0)
            {
                XmlNode nodBS = nodDoD.AppendChild(xmlDoc.CreateElement("BudgetSegregations"));
                foreach (BudgetSegregation bs in BudgetSegregations.Values)
                    bs.Serialize(xmlDoc, nodBS);
            }

            // Return this so inherited classes can append to it.
            return nodDoD;
        }

        public static void SerializeDoDStatistics(XmlDocument xmlDoc, XmlNode nodParent, DoDStats stats)
        {
            XmlNode nodErosion = nodParent.AppendChild(xmlDoc.CreateElement("Erosion"));
            SerializeAreaVolume(xmlDoc, nodErosion.AppendChild(xmlDoc.CreateElement("Raw")), stats.ErosionRaw, stats.StatsUnits, stats.CellArea);
            SerializeAreaVolume(xmlDoc, nodErosion.AppendChild(xmlDoc.CreateElement("Thresholded")), stats.ErosionThr, stats.StatsUnits, stats.CellArea);
            nodErosion.AppendChild(xmlDoc.CreateElement("Error")).AppendChild(xmlDoc.CreateElement("Volume")).InnerText =
                stats.ErosionErr.GetVolume(stats.CellArea, stats.StatsUnits.VertUnit).As(stats.StatsUnits.VolUnit).ToString("R");

            XmlNode nodDeposition = nodParent.AppendChild(xmlDoc.CreateElement("Deposition"));
            SerializeAreaVolume(xmlDoc, nodDeposition.AppendChild(xmlDoc.CreateElement("Raw")), stats.DepositionRaw, stats.StatsUnits, stats.CellArea);
            SerializeAreaVolume(xmlDoc, nodDeposition.AppendChild(xmlDoc.CreateElement("Thresholded")), stats.DepositionThr, stats.StatsUnits, stats.CellArea);
            nodDeposition.AppendChild(xmlDoc.CreateElement("Error")).AppendChild(xmlDoc.CreateElement("Volume")).InnerText =
                stats.DepositionErr.GetVolume(stats.CellArea, stats.StatsUnits.VertUnit).As(stats.StatsUnits.VolUnit).ToString("R");
        }

        private static void SerializeAreaVolume(XmlDocument xmlDoc, XmlNode nodParent, GCDAreaVolume areaVol, UnitGroup units, UnitsNet.Area cellArea)
        {
            nodParent.AppendChild(xmlDoc.CreateElement("Area")).InnerText = areaVol.GetArea(cellArea).As(units.ArUnit).ToString("R");
            nodParent.AppendChild(xmlDoc.CreateElement("Volume")).InnerText = areaVol.GetVolume(cellArea, units.VertUnit).As(units.VolUnit).ToString("R");
        }

        protected static DoDBase Deserialize(XmlNode nodDoD, Dictionary<string, DEMSurvey> DEMs)
        {
            string name = nodDoD.SelectSingleNode("Name").InnerText;
            DirectoryInfo folder = ProjectManager.Project.GetAbsoluteDir(nodDoD.SelectSingleNode("Folder").InnerText);

            DEMSurvey newDEM = DeserializeDEM(nodDoD, DEMs, "NewDEM");
            DEMSurvey oldDEM = DeserializeDEM(nodDoD, DEMs, "OldDEM");

            FileInfo rawDoD = ProjectManager.Project.GetAbsolutePath(nodDoD.SelectSingleNode("RawDoD").InnerText);
            FileInfo thrDoD = ProjectManager.Project.GetAbsolutePath(nodDoD.SelectSingleNode("ThrDoD").InnerText);
            FileInfo rawHis = ProjectManager.Project.GetAbsolutePath(nodDoD.SelectSingleNode("RawHistogram").InnerText);
            FileInfo thrHis = ProjectManager.Project.GetAbsolutePath(nodDoD.SelectSingleNode("ThrHistogram").InnerText);
            FileInfo summar = ProjectManager.Project.GetAbsolutePath(nodDoD.SelectSingleNode("SummaryXML").InnerText);

            DoDStats stats = DeserializeStatistics(nodDoD.SelectSingleNode("Statistics"), ProjectManager.Project.CellArea, ProjectManager.Project.Units);

            DoDBase dod = new DoDBase(name, folder, newDEM, oldDEM, rawDoD, thrDoD, new HistogramPair(rawHis, thrHis), summar, stats);

            XmlNode nodBSes = nodDoD.SelectSingleNode("BudgetSegregations");
            if (nodBSes is XmlNode)
                foreach (XmlNode nodBS in nodBSes.SelectNodes("BudgetSegregation"))
                {
                    BudgetSegregation bs = BudgetSegregation.Deserialize(nodBS, dod);
                    dod.BudgetSegregations[bs.Name] = bs;
                }

            return dod;
        }

        private static DEMSurvey DeserializeDEM(XmlNode nodDoD, Dictionary<string, DEMSurvey> DEMs, string nodDEMName)
        {
            string demName = nodDoD.SelectSingleNode(nodDEMName).InnerText;
            if (DEMs.ContainsKey(demName))
            {
                return DEMs[demName];
            }
            else
                return null;
        }

        public static DoDStats DeserializeStatistics(XmlNode nodStatistics, UnitsNet.Area cellArea, UnitGroup units)
        {
            UnitsNet.Area AreaErosion_Raw = UnitsNet.Area.From(double.Parse(nodStatistics.SelectSingleNode("Erosion/Raw/Area").InnerText), units.ArUnit);
            UnitsNet.Area AreaDeposit_Raw = UnitsNet.Area.From(double.Parse(nodStatistics.SelectSingleNode("Deposition/Raw/Area").InnerText), units.ArUnit);
            UnitsNet.Area AreaErosion_Thr = UnitsNet.Area.From(double.Parse(nodStatistics.SelectSingleNode("Erosion/Thresholded/Area").InnerText), units.ArUnit);
            UnitsNet.Area AreaDeposit_Thr = UnitsNet.Area.From(double.Parse(nodStatistics.SelectSingleNode("Deposition/Thresholded/Area").InnerText), units.ArUnit);

            UnitsNet.Volume VolErosion_Raw = UnitsNet.Volume.From(double.Parse(nodStatistics.SelectSingleNode("Erosion/Raw/Volume").InnerText), units.VolUnit);
            UnitsNet.Volume VolDeposit_Raw = UnitsNet.Volume.From(double.Parse(nodStatistics.SelectSingleNode("Deposition/Raw/Volume").InnerText), units.VolUnit);
            UnitsNet.Volume VolErosion_Thr = UnitsNet.Volume.From(double.Parse(nodStatistics.SelectSingleNode("Erosion/Thresholded/Volume").InnerText), units.VolUnit);
            UnitsNet.Volume VolDeposit_Thr = UnitsNet.Volume.From(double.Parse(nodStatistics.SelectSingleNode("Deposition/Thresholded/Volume").InnerText), units.VolUnit);

            UnitsNet.Volume VolErosion_Err = UnitsNet.Volume.From(double.Parse(nodStatistics.SelectSingleNode("Erosion/Error/Volume").InnerText), units.VolUnit);
            UnitsNet.Volume VolDeposit_Err = UnitsNet.Volume.From(double.Parse(nodStatistics.SelectSingleNode("Deposition/Error/Volume").InnerText), units.VolUnit);

            return new DoDStats(
                AreaErosion_Raw, AreaDeposit_Raw, AreaErosion_Thr, AreaDeposit_Thr,
                VolErosion_Raw, VolDeposit_Raw, VolErosion_Thr, VolDeposit_Thr,
                VolErosion_Err, VolDeposit_Err,
                cellArea, units);
        }

        private static UnitsNet.Area DeserializeArea(XmlNode nodParent, string nodName, UnitsNet.Units.AreaUnit unit)
        {
            double value = double.Parse(nodParent.SelectSingleNode(nodName).InnerText);
            return UnitsNet.Area.From(value, unit);
        }

        public virtual void Delete()
        {
            // Delete child budget segregations first
            foreach (BudgetSegregation bs in BudgetSegregations.Values)
            {
                try
                {
                    bs.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to delete budget segregation " + bs.Name);
                }
            }
            BudgetSegregations.Clear();

            // Delete the raw and thresholded rasters
            RawDoD.Delete();
            ThrDoD.Delete();

            // Now delete all the meta files associated with the DoD
            foreach (FileInfo file in RawDoD.Raster.GISFileInfo.Directory.GetFiles("*.*", SearchOption.AllDirectories))
            {
                try
                {
                    file.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to delete file " + file.FullName);
                    Console.WriteLine(ex.Message);
                }
            }

            // Delete the figures folder
                 DirectoryInfo dirFigs = ProjectManager.OutputManager.GetChangeDetectionFiguresFolder(Folder, false);
           try
            {
                System.IO.Directory.Delete(dirFigs.FullName, true);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to delete DoD figures folder " + dirFigs.FullName);
                Console.WriteLine(ex.Message);
            }

            // Try to delete the DoD folder 
            try
            {
                Folder.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to delete DoD folder  " + Folder.FullName);
                Console.WriteLine(ex.Message);
            }

            // If there are no more DoDs try to delete the parent change detection folder "CD"
            if (Folder.Parent.GetDirectories().Length < 1)
            {
                Folder.Parent.Delete();
            }

            // If there are no more analyses then delete this folder
            if (Folder.Parent.Parent.GetDirectories().Length < 1)
            {
                Folder.Parent.Parent.Delete();
            }

            // Remove the DoD from the project
            ProjectManager.Project.DoDs.Remove(Name);           
            ProjectManager.Project.Save();
        }
    }
}
