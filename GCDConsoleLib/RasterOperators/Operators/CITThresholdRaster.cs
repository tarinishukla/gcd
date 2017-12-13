﻿using System;
using System.Collections.Generic;
namespace GCDConsoleLib.Internal.Operators
{
    /// <summary>
    /// We do Hillshade as a float on purpose since we need it to be fast and accuracy is less important
    /// </summary>
    class CITThresholdRaster : CellByCellOperator<double>
    {
        private static int rawDod = 0;
        private static int priorProb = 1;

        double zCutoff;

        /// <summary>
        /// Pass-through constructor for Creating Prior Probability Rasters
        /// </summary>
        /// <param name="rInput"></param>
        /// <param name="rOutputRaster"></param>
        public CITThresholdRaster(Raster rawDoD, Raster rPriorProb, Raster rOutputRaster, decimal cutoff) :
            base(new List<Raster> { rawDoD, rPriorProb }, rOutputRaster)
        {
            zCutoff = (double)cutoff;
        }
        // Raster rawDoD, string thrHistPath, Raster newError, Raster oldError, FileInfo sPriorProbRaster, decimal fThreshold

        /// <summary>
        /// The Cell op for Prior Probability Rasters
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override void CellOp(List<double[]> data, List<double[]> outputs, int id)
        {
            // If Nothing is Nodata (as long as there is a nodata value) 
            if ((data[rawDod][id] != inNodataVals[rawDod] || !_inputRasters[rawDod].HasNodata) &&
                (data[priorProb][id] != inNodataVals[priorProb] || !_inputRasters[priorProb].HasNodata) &&
                // AND this math is greater than the cutoff
                (Math.Abs(data[rawDod][id]) / data[priorProb][id] >= zCutoff))

                outputs[0][id] = data[rawDod][id];
            else
                outputs[0][id] = outNodataVals[0];
        }
    }
}
