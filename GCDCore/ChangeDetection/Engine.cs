﻿using System;
using System.IO;
using GCDConsoleLib.GCD;
using GCDCore.Visualization;
using System.Globalization;
using GCDConsoleLib;

namespace GCDCore.ChangeDetection
{
    public abstract class Engine
    {
        protected const int DEFAULTHISTOGRAMNUMBER = 100;

        public readonly DirectoryInfo AnalysisFolder;
        public readonly UnitsNet.Units.LengthUnit LinearUnits;

        protected DirectoryInfo FiguresFolder
        {
            get { return new DirectoryInfo(Path.Combine(AnalysisFolder.FullName, "Figures")); }
        }
        
        public Engine(DirectoryInfo folder, UnitsNet.Units.LengthUnit eUnits)
        {
            AnalysisFolder = folder;
            LinearUnits = eUnits;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="changeStats"></param>
        /// <returns></returns>
        /// <remarks>This method is needed by budget segregation as well</remarks>
        protected void GenerateSummaryXML(DoDStats changeStats, FileInfo outputPath, UnitsNet.Units.LengthUnit linearUnits)
        {
            string templatePath = Path.Combine(Project.ProjectManagerBase.ExcelTemplatesFolder.FullName, "GCDSummary.xml");
            System.Text.StringBuilder outputText = default(System.Text.StringBuilder);

            try
            {
                using (System.IO.StreamReader objReader = new System.IO.StreamReader(templatePath))
                {
                    outputText = new System.Text.StringBuilder(objReader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception("Error reading the GCD summary XML template file", ex);
                ex.Data["Excel Template Path"] = templatePath;
                throw ex2;
            }

            UnitsNet.Area ca = Project.ProjectManagerBase.CellArea;
            UnitsNet.Units.LengthUnit lu = Project.ProjectManagerBase.Units.VertUnit;
            UnitsNet.Units.AreaUnit au = Project.ProjectManagerBase.Units.ArUnit;
            UnitsNet.Units.VolumeUnit vu = Project.ProjectManagerBase.Units.VolUnit;

            outputText.Replace("[LinearUnits]", UnitsNet.Length.GetAbbreviation(linearUnits));

            outputText.Replace("[TotalAreaOfErosionRaw]", changeStats.ErosionRaw.GetArea(ca).As(au).ToString(CultureInfo.InvariantCulture));
            outputText.Replace("[TotalAreaOfErosionThresholded]", changeStats.ErosionThr.GetArea(ca).As(au).ToString(CultureInfo.InvariantCulture));

            outputText.Replace("[TotalAreaOfDepositionRaw]", changeStats.DepositionRaw.GetArea(ca).As(au).ToString(CultureInfo.InvariantCulture));
            outputText.Replace("[TotalAreaOfDepositionThresholded]", changeStats.DepositionThr.GetArea(ca).As(au).ToString(CultureInfo.InvariantCulture));

            outputText.Replace("[TotalVolumeOfErosionRaw]", changeStats.ErosionRaw.GetVolume(ca, lu).As(vu).ToString(CultureInfo.InvariantCulture));
            outputText.Replace("[TotalVolumeOfErosionThresholded]", changeStats.ErosionThr.GetVolume(ca, lu).As(vu).ToString(CultureInfo.InvariantCulture));
            outputText.Replace("[ErrorVolumeOfErosion]", changeStats.ErosionErr.GetVolume(ca, lu).As(vu).ToString(CultureInfo.InvariantCulture));

            outputText.Replace("[TotalVolumeOfDepositionRaw]", changeStats.DepositionRaw.GetVolume(ca, lu).As(vu).ToString(CultureInfo.InvariantCulture));
            outputText.Replace("[TotalVolumeOfDepositionThresholded]", changeStats.DepositionThr.GetVolume(ca, lu).As(vu).ToString(CultureInfo.InvariantCulture));
            outputText.Replace("[ErrorVolumeOfDeposition]", changeStats.DepositionErr.GetVolume(ca, lu).As(vu).ToString(CultureInfo.InvariantCulture));

            try
            {
                using (StreamWriter objWriter = new StreamWriter(outputPath.FullName))
                {
                    objWriter.Write(outputText);
                }
            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception("Error writing the GCD summary XML template file", ex);
                ex.Data["Excel Template Path"] = templatePath;
                throw ex2;
            }
        }

        protected void GenerateChangeBarGraphicFiles(DoDStats stats, int fChartWidth, int fChartHeight, string sFilePrefix = "")
        {
            ElevationChangeBarViewer barViewer = new ElevationChangeBarViewer(LinearUnits);

            if (!string.IsNullOrEmpty(sFilePrefix))
            {
                if (!sFilePrefix.EndsWith("_"))
                {
                    sFilePrefix += "_";
                }
            }

            FiguresFolder.Create();

            UnitsNet.Area ca = GCDCore.Project.ProjectManagerBase.CellArea;
            UnitsNet.Units.LengthUnit lu = Project.ProjectManagerBase.Units.VertUnit;
            UnitsNet.Units.AreaUnit au = Project.ProjectManagerBase.Units.ArUnit;
            UnitsNet.Units.VolumeUnit vu = Project.ProjectManagerBase.Units.VolUnit;

            barViewer.Refresh(
                stats.ErosionThr.GetArea(ca).As(au),
                stats.DepositionThr.GetArea(ca).As(au), LinearUnits, ElevationChangeBarViewer.BarTypes.Area, true);
            barViewer.Save(Path.Combine(FiguresFolder.FullName, sFilePrefix + "ChangeBars_AreaAbsolute.png"), fChartWidth, fChartHeight);

            barViewer.Refresh(
                stats.ErosionThr.GetArea(ca).As(au),
                stats.DepositionThr.GetArea(ca).As(au), LinearUnits, ElevationChangeBarViewer.BarTypes.Area, false);
            barViewer.Save(Path.Combine(FiguresFolder.FullName, sFilePrefix + "ChangeBars_AreaRelative.png"), fChartWidth, fChartHeight);

            barViewer.Refresh(
                stats.ErosionThr.GetVolume(ca, lu).As(vu),
                stats.DepositionThr.GetVolume(ca, lu).As(vu),
                stats.NetVolumeOfDifference_Thresholded.As(vu),
                stats.ErosionErr.GetVolume(ca, lu).As(vu),
                stats.DepositionErr.GetVolume(ca, lu).As(vu),
                stats.NetVolumeOfDifference_Error.As(vu), LinearUnits, ElevationChangeBarViewer.BarTypes.Volume, true);
            barViewer.Save(Path.Combine(FiguresFolder.FullName, sFilePrefix + "ChangeBars_VolumeAbsolute.png"), fChartWidth, fChartHeight);

            barViewer.Refresh(
                stats.ErosionThr.GetVolume(ca, lu).As(vu),
                stats.DepositionThr.GetVolume(ca, lu).As(vu),
                stats.NetVolumeOfDifference_Thresholded.As(vu),
                stats.ErosionErr.GetVolume(ca, lu).As(vu),
                stats.DepositionErr.GetVolume(ca, lu).As(vu),
                stats.NetVolumeOfDifference_Error.As(vu), LinearUnits, ElevationChangeBarViewer.BarTypes.Volume, false);
            barViewer.Save(Path.Combine(FiguresFolder.FullName, sFilePrefix + "ChangeBars_VolumeRelative.png"), fChartWidth, fChartHeight);

            barViewer.Refresh(
                stats.AverageDepthErosion_Thresholded.As(lu),
                stats.AverageDepthDeposition_Thresholded.As(lu),
                stats.AverageNetThicknessOfDifferenceADC_Thresholded.As(lu),
                stats.AverageDepthErosion_Error.As(lu),
                stats.AverageDepthDeposition_Error.As(lu),
                stats.AverageThicknessOfDifferenceADC_Error.As(lu), LinearUnits, ElevationChangeBarViewer.BarTypes.Vertical, true);
            barViewer.Save(Path.Combine(FiguresFolder.FullName, sFilePrefix + "ChangeBars_DepthAbsolute.png"), fChartWidth, fChartHeight);

            barViewer.Refresh(
                stats.AverageDepthErosion_Thresholded.As(lu),
                stats.AverageDepthDeposition_Thresholded.As(lu),
                stats.AverageNetThicknessOfDifferenceADC_Thresholded.As(lu),
                stats.AverageDepthErosion_Error.As(lu),
                stats.AverageDepthDeposition_Error.As(lu),
                stats.AverageThicknessOfDifferenceADC_Error.As(lu), LinearUnits, ElevationChangeBarViewer.BarTypes.Vertical, false);
            barViewer.Save(Path.Combine(FiguresFolder.FullName, sFilePrefix + "ChangeBars_DepthRelative.png"), fChartWidth, fChartHeight);
        }
        
        protected void WriteHistogram(ref Histogram histo, FileInfo outputFile)
        {
            histo.WriteFile(outputFile, Project.ProjectManagerBase.CellArea, Project.ProjectManagerBase.Units);
        }

        protected void GenerateHistogramGraphicFiles(ref Histogram rawHisto, ref Histogram thrHisto, int fChartWidth, int fChartHeight)
        {
            FiguresFolder.Create();

            string areaHistPath = Path.Combine(FiguresFolder.FullName, "Histogram_Area.png");
            string volhistPath = Path.Combine(FiguresFolder.FullName, "Histogram_Volume.png");

            //DoDHistogramViewerClass ExportHistogramViewer = new DoDHistogramViewerClass(ref rawHisto, ref thrHisto, LinearUnits);
            //ExportHistogramViewer.ExportCharts(areaHistPath, volhistPath, fChartWidth, fChartHeight);
        }
    }
}