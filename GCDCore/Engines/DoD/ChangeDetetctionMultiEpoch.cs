﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCDCore.Project;
using GCDCore.UserInterface.ChangeDetection;

namespace GCDCore.Engines.DoD
{
    public class ChangeDetectionMultiEpoch
    {
        public readonly List<ThresholdProps> Thresholds;
        public readonly DEMSurvey NewDEM;
        public readonly DEMSurvey OldDEM;
        public readonly ErrorSurface NewError;
        public readonly ErrorSurface OldError;

        private readonly Dictionary<ThresholdProps.ThresholdMethods, ChangeDetectionEngineBase> DoDEngines;

        public ChangeDetectionMultiEpoch(DEMSurvey newDEM, DEMSurvey oldDEM, ErrorSurface newError, ErrorSurface oldError, List<ThresholdProps> tProps)
        {
            NewDEM = newDEM;
            OldDEM = oldDEM;
            NewError = newError;
            OldError = oldError;
            Thresholds = tProps;

            DoDEngines = new Dictionary<ThresholdProps.ThresholdMethods, ChangeDetectionEngineBase>();
        }

        public void Run(BackgroundWorker bgWorker)
        {

            foreach (ThresholdProps tp in Thresholds)
            {
                PerformDoD(tp);
            }

            ProjectManager.Project.Save();
        }

        private void PerformDoD(ThresholdProps tProps)
        {
            string dodName = frmDoDProperties.GetUniqueAnalysisName(NewDEM.Name, OldDEM.Name, tProps.ThresholdString);
            System.IO.DirectoryInfo dFolder = ProjectManager.OutputManager.GetDoDOutputFolder(dodName);

            ChangeDetectionEngineBase cdEngine = null;
            switch (tProps.Method)
            {
                case ThresholdProps.ThresholdMethods.MinLoD:
                    cdEngine = new ChangeDetectionEngineMinLoD(NewDEM, OldDEM, tProps.Threshold);
                    break;

                case ThresholdProps.ThresholdMethods.Propagated:
                    cdEngine = new ChangeDetectionEnginePropProb(NewDEM, OldDEM, NewError, OldError);
                    break;

                case ThresholdProps.ThresholdMethods.Probabilistic:
                    cdEngine = new ChangeDetectionEngineProbabilistic(NewDEM, OldDEM, NewError, OldError, tProps.Threshold, tProps.SpatialCoherenceProps);
                    break;
            }

            DoDBase dod = cdEngine.Calculate(dodName, dFolder, true, ProjectManager.Project.Units);
            ProjectManager.Project.DoDs[dodName] = dod;
        }
    }
}
