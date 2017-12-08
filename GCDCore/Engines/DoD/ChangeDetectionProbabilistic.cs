﻿using GCDConsoleLib;
using GCDConsoleLib.GCD;
using System.IO;
using GCDCore.Project;

namespace GCDCore.Engines
{
    public class ChangeDetectionEngineProbabilistic : ChangeDetectionEnginePropProb
    {
        public readonly decimal Threshold;
        public readonly CoherenceProperties SpatialCoherence;
        private FileInfo m_PriorProbRaster;
        private FileInfo m_PosteriorRaster;
        private FileInfo m_ConditionalRaster;
        private FileInfo m_SpatialCoErosionRaster;

        private FileInfo m_SpatialCoDepositionRaster;

        public ChangeDetectionEngineProbabilistic(string name, DirectoryInfo folder, DEMSurvey newDEM, DEMSurvey oldDEM, ErrorSurface newError, ErrorSurface oldError,
            decimal fThreshold, CoherenceProperties spatCoherence = null)
        : base(name, folder, newDEM, oldDEM, newError, oldError)
        {
            Threshold = fThreshold;
            SpatialCoherence = spatCoherence;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawDoD"></param>
        /// <param name="thrDoDPath"></param>
        /// <returns></returns>
        /// <remarks>Let the base class build pyramids for the thresholded raster</remarks>
        protected override Raster ThresholdRawDoD(Raster rawDoD, FileInfo thrDoDPath)
        {
            Raster propErrorRaster = GeneratePropagatedErrorRaster();
            Raster thrDoD = null;

            // Create the prior probability raster
            m_PriorProbRaster = new FileInfo(Path.ChangeExtension(Path.Combine(AnalysisFolder.FullName, "priorprob"), OutputManager.RasterExtension));
            Raster priorPRob = RasterOperators.CreatePriorProbabilityRaster(rawDoD, propErrorRaster, m_PriorProbRaster);

            // Build Pyramids
            ProjectManager.PyramidManager.PerformRasterPyramids(RasterPyramidManager.PyramidRasterTypes.ProbabilityRasters, m_PriorProbRaster);

            if (SpatialCoherence == null)
            {
                thrDoD = RasterOperators.ThresholdDoDProbability(rawDoD, priorPRob, thrDoDPath, Threshold);
            }
            else
            {
                m_PosteriorRaster = new FileInfo(Path.ChangeExtension(Path.Combine(AnalysisFolder.FullName, "postProb"), OutputManager.RasterExtension));
                m_ConditionalRaster = new FileInfo(Path.ChangeExtension(Path.Combine(AnalysisFolder.FullName, "condProb"), OutputManager.RasterExtension));
                m_SpatialCoErosionRaster = new FileInfo(Path.ChangeExtension(Path.Combine(AnalysisFolder.FullName, "nbrErosion"), OutputManager.RasterExtension));
                m_SpatialCoDepositionRaster = new FileInfo(Path.ChangeExtension(Path.Combine(AnalysisFolder.FullName, "nbrDeposition"), OutputManager.RasterExtension));

                thrDoD = RasterOperators.ThresholdDoDProbWithSpatialCoherence(rawDoD, new FileInfo(thrDoDPath.FullName), priorPRob,
                    m_PosteriorRaster, m_ConditionalRaster, m_SpatialCoErosionRaster, m_SpatialCoDepositionRaster,
                    SpatialCoherence.MovingWindowDimensions, SpatialCoherence.InflectionA, SpatialCoherence.InflectionB, Threshold);

                // Build Pyramids
                ProjectManager.PyramidManager.PerformRasterPyramids(RasterPyramidManager.PyramidRasterTypes.ProbabilityRasters, m_SpatialCoErosionRaster);
                ProjectManager.PyramidManager.PerformRasterPyramids(RasterPyramidManager.PyramidRasterTypes.ProbabilityRasters, m_SpatialCoDepositionRaster);
                ProjectManager.PyramidManager.PerformRasterPyramids(RasterPyramidManager.PyramidRasterTypes.ProbabilityRasters, m_ConditionalRaster);
                ProjectManager.PyramidManager.PerformRasterPyramids(RasterPyramidManager.PyramidRasterTypes.ProbabilityRasters, m_PosteriorRaster);
            }

            return thrDoD;
        }

        protected override DoDStats CalculateChangeStats(Raster rawDoD, Raster thrDoD, UnitGroup units)
        {
            Raster propErr = PropagatedErrRaster;
            return RasterOperators.GetStatsProbalistic(rawDoD, thrDoD, propErr, units);
        }

        protected override DoDBase GetDoDResult(DoDStats changeStats, Raster rawDoD, Raster thrDoD, HistogramPair histograms, FileInfo summaryXML)
        {
            bool bBayesian = SpatialCoherence is CoherenceProperties;
            int nFilter = 0;
            if (SpatialCoherence is CoherenceProperties)
            {
                nFilter = SpatialCoherence.MovingWindowDimensions;
            }

            return new DoDProbabilistic(Name, AnalysisFolder, NewDEM, OldDEM, histograms, summaryXML, rawDoD, thrDoD, NewError, OldError,
                PropagatedErrRaster, m_PriorProbRaster, m_PosteriorRaster, m_ConditionalRaster, m_SpatialCoErosionRaster, m_SpatialCoDepositionRaster,
                SpatialCoherence, Threshold, changeStats);
        }
    }
}
