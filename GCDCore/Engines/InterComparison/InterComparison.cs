﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using GCDCore.Project;
using GCDCore.UserInterface.ChangeDetection;
using System.Text.RegularExpressions;

namespace GCDCore.Engines
{
    public class InterComparison
    {
        /// <summary>
        /// Generate an inter-comparison summary Excel XML spreadsheet
        /// </summary>
        /// <param name="dodStats">Dictionary of DoD statistics.</param>
        /// <param name="output">File path of the output inter-comparison spreadsheet to be generated</param>
        /// <remarks>
        /// This method is used from two places for subtly different purposes:
        /// 
        /// 1. Every budget segregation will generate a single inter-comparison spreadsheet summarizing the DoD statistics for all classes
        /// 2. The user can manually generate an inter-comparison by selecting two or more DoDs in the user interface.
        /// 
        /// In both cases the code that calls this method is responsible for building a dictionary of DoD statistics. In the first
        /// case the dictionary key will be the budget segregation class ("pool", "riffle" etc). In the second case the key will be
        /// the DoD name ("2006 - 2006 Min LoD 0.2m" etc).
        /// 
        /// The processing in this class is identical for both cases.</remarks>
        public static void Generate(List<Tuple<string, GCDConsoleLib.GCD.DoDStats>> dodStats, FileInfo output)
        {
            int DoDCount = 0;

            //get template and throw error if it doesnt exists
            FileInfo template = new FileInfo(Path.Combine(Project.ProjectManager.ExcelTemplatesFolder.FullName, "InterComparison.xml"));
            if (!template.Exists)
            {
                throw new Exception("The GCD intercomparison spreadsheet template cannot be found at " + template.FullName);
            }

            //setup ExcelXMLDocument which does the heavy lifting of updating the XML
            ExcelXMLDocument xmlExcelDoc = new ExcelXMLDocument(template.FullName);

            foreach (Tuple<string, GCDConsoleLib.GCD.DoDStats> kvp in dodStats)
            {
                //turn these into a dictionary of named values to replace in XML
                Dictionary<string, string> dicStatValues = GetStatValues(kvp.Item2);
                dicStatValues.Add("TemplateRowName", kvp.Item1); //Add name so the Named Range for the name (e.g. ArealDoDName) is updated

                DoDCount += 1;

                //update or clone all template rows. All references are maintained by the ExcelXMLDocument
                //e.g. relative references, sum formulas and named ranges
                if (DoDCount > 1)
                {
                    xmlExcelDoc.CloneRow("ArealDoDName", DoDCount - 1, dicStatValues);
                    xmlExcelDoc.CloneRow("VolumeDoDName", DoDCount - 1, dicStatValues);
                    xmlExcelDoc.CloneRow("VerticalDoDName", DoDCount - 1, dicStatValues);
                    xmlExcelDoc.CloneRow("PercentagesDoDName", DoDCount - 1, dicStatValues);
                }
                else
                {
                    xmlExcelDoc.UpdateRow("ArealDoDName", dicStatValues);
                    xmlExcelDoc.UpdateRow("VolumeDoDName", dicStatValues);
                    xmlExcelDoc.UpdateRow("VerticalDoDName", dicStatValues);
                    xmlExcelDoc.UpdateRow("PercentagesDoDName", dicStatValues);
                }

            }

            //cells should be formatted with grey, single weight top and bottom border
            CellStyle oCellStyle = new CellStyle();
            oCellStyle.TopBorder.Weight = 1;
            oCellStyle.TopBorder.Color = "#E7E6E6";
            oCellStyle.BottomBorder.Weight = 1;
            oCellStyle.BottomBorder.Color = "#E7E6E6";

            //loop through all cells and format
            for (int i = 0; i < dodStats.Count; i++)
            {
                xmlExcelDoc.FormatRow("ArealDoDName", i, oCellStyle);
                xmlExcelDoc.FormatRow("VolumeDoDName", i, oCellStyle);
                xmlExcelDoc.FormatRow("VerticalDoDName", i, oCellStyle);
                xmlExcelDoc.FormatRow("PercentagesDoDName", i, oCellStyle);
            }

            //save output
            xmlExcelDoc.Save(output.FullName);
        }

        /// <summary>
        /// Returns a dictionary of named range values in the XML spreadsheet to replace with stat values
        /// </summary>
        /// <param name="dodStat"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetStatValues(GCDConsoleLib.GCD.DoDStats dodStat)
        {
            Dictionary<string, string> StatValues = new Dictionary<string, string>();

            //get settings and options
            UnitsNet.Area ca = ProjectManager.Project.CellArea;
            DoDSummaryDisplayOptions options = new DoDSummaryDisplayOptions(ProjectManager.Project.Units);

            //using same pattern as ucDoDSummary
            StatValues["ArealLoweringRaw"] = dodStat.ErosionRaw.GetArea(ca).As(options.AreaUnits).ToString();
            StatValues["ArealLoweringThresholded"] = dodStat.ErosionThr.GetArea(ca).As(options.AreaUnits).ToString();
            StatValues["ArealRaisingRaw"] = dodStat.DepositionRaw.GetArea(ca).As(options.AreaUnits).ToString();
            StatValues["ArealRaisingThresholded"] = dodStat.DepositionThr.GetArea(ca).As(options.AreaUnits).ToString();

            StatValues["VolumeLoweringRaw"] = dodStat.ErosionRaw.GetVolume(ca, ProjectManager.Project.Units).As(options.VolumeUnits).ToString();
            StatValues["VolumeLoweringThresholded"] = dodStat.ErosionThr.GetVolume(ca, ProjectManager.Project.Units).As(options.VolumeUnits).ToString();
            StatValues["VolumeErrorLowering"] = dodStat.ErosionErr.GetVolume(ca, ProjectManager.Project.Units).As(options.VolumeUnits).ToString();
            StatValues["VolumeRaisingRaw"] = dodStat.DepositionRaw.GetVolume(ca, ProjectManager.Project.Units).As(options.VolumeUnits).ToString();
            StatValues["VolumeRaisingThresholded"] = dodStat.DepositionThr.GetVolume(ca, ProjectManager.Project.Units).As(options.VolumeUnits).ToString();
            StatValues["VolumeErrorRaising"] = dodStat.DepositionErr.GetVolume(ca, ProjectManager.Project.Units).As(options.VolumeUnits).ToString();

            return StatValues;
        }
    }

}
