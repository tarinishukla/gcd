﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace GCDCore.Project
{
    public class BudgetSegregation : GCDProjectItem
    {
        public readonly DoDBase DoD;
        public readonly DirectoryInfo Folder;
        public readonly Masks.AttributeFieldMask Mask;
        public readonly FileInfo ClassLegend;
        public readonly FileInfo SummaryXML;
        public readonly string MaskField;
        public readonly Dictionary<string, BudgetSegregationClass> Classes;

        public readonly List<Morphological.MorphologicalAnalysis> MorphologicalAnalyses;

        public bool IsMaskDirectional { get { return Mask is GCDCore.Project.Masks.DirectionalMask; } }

        public override string Noun { get { return "Budget Segregation"; } }

        /// <summary>
        /// Budget segregations are not used by other items
        /// </summary>
        public override bool IsItemInUse
        {
            get
            {
                return false;
            }
        }

        public DirectoryInfo MorphologicalFolder { get { return new DirectoryInfo(Path.Combine(Folder.FullName, "Morph")); } }

        public BindingList<BudgetSegregationClass> FilteredClasses
        {
            get
            {
                BindingList<BudgetSegregationClass> result = new BindingList<BudgetSegregationClass>();

                // Loop over all distinct field values that are flagged to be included
                foreach (GCDCore.Project.Masks.MaskItem item  in Mask.ActiveFieldValues)
                {
                    if (Classes.ContainsKey(item.FieldValue))
                    {
                        BudgetSegregationClass existingClass = Classes[item.FieldValue];
                        result.Add(new BudgetSegregationClass(item.Label, existingClass.Statistics, existingClass.Histograms, existingClass.SummaryXML));
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Engine Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        /// <param name="maskField"></param>
        /// <param name="dod"></param>
        public BudgetSegregation(string name, DirectoryInfo folder, Masks.AttributeFieldMask mask, DoDBase dod)
            : base(name)
        {
            DoD = dod;
            Folder = folder;
            Mask = mask;
            ClassLegend = new FileInfo(Path.Combine(Folder.FullName, "ClassLegend.csv"));
            SummaryXML = new FileInfo(Path.Combine(Folder.FullName, "Summary.xml"));
            Classes = new Dictionary<string, BudgetSegregationClass>();
            MorphologicalAnalyses = new List<Morphological.MorphologicalAnalysis>();
        }

        /// <summary>
        /// Serialization constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        /// <param name="polygonMask"></param>
        /// <param name="maskField"></param>
        /// <param name="dod"></param>
        /// <param name="summaryXML"></param>
        /// <param name="classLegend"></param>
        public BudgetSegregation(string name, DirectoryInfo folder, Masks.AttributeFieldMask mask, DoDBase dod, FileInfo summaryXML, FileInfo classLegend)
            : base(name)
        {
            DoD = dod;
            Folder = folder;
            Mask = mask;
            ClassLegend = classLegend;
            SummaryXML = summaryXML;
            Classes = new Dictionary<string, BudgetSegregationClass>();
            MorphologicalAnalyses = new List<Morphological.MorphologicalAnalysis>();
        }

        public BudgetSegregation(XmlNode nodBS, DoDBase dod)
            : base(nodBS.SelectSingleNode("Name").InnerText)
        {
            DoD = dod;
            Folder = ProjectManager.Project.GetAbsoluteDir(nodBS.SelectSingleNode("Folder").InnerText);
            Mask = ProjectManager.Project.Masks.First(x => x.Name == nodBS.SelectSingleNode("Mask").InnerText) as Masks.AttributeFieldMask;
            SummaryXML = ProjectManager.Project.GetAbsolutePath(nodBS.SelectSingleNode("SummaryXML").InnerText);
            ClassLegend = ProjectManager.Project.GetAbsolutePath(nodBS.SelectSingleNode("ClassLegend").InnerText);

            Classes = new Dictionary<string, BudgetSegregationClass>();
            foreach (XmlNode nodClass in nodBS.SelectNodes("Classes/Class"))
            {
                BudgetSegregationClass bsClass = new BudgetSegregationClass(nodClass);
                Classes[bsClass.Name] = bsClass;
            }

            MorphologicalAnalyses = new List<Morphological.MorphologicalAnalysis>();
            foreach (XmlNode nodMA in nodBS.SelectNodes("MorphologicalAnalyses/MorphologicalAnalysis"))
            {
                Morphological.MorphologicalAnalysis ma = new Morphological.MorphologicalAnalysis(nodMA, this);
                MorphologicalAnalyses.Add(ma);
            }
        }

        public bool IsMorphologicalAnalysisNameUnique(string name, Morphological.MorphologicalAnalysis ignore)
        {
            return !MorphologicalAnalyses.Any(x => x != ignore && string.Compare(name, x.Name, true) == 0);
        }

        public void Serialize(XmlNode nodParent)
        {
            XmlNode nodBS = nodParent.AppendChild(nodParent.OwnerDocument.CreateElement("BudgetSegregation"));
            nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("Name")).InnerText = Name;
            nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("Folder")).InnerText = ProjectManager.Project.GetRelativePath(Folder.FullName);
            nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("Mask")).InnerText = Mask.Name;
            nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("SummaryXML")).InnerText = ProjectManager.Project.GetRelativePath(SummaryXML);
            nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("ClassLegend")).InnerText = ProjectManager.Project.GetRelativePath(ClassLegend);
            nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("MaskField")).InnerText = MaskField;

            XmlNode nodClasses = nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("Classes"));
            Classes.Values.ToList().ForEach(x => x.Serialize(nodClasses));

            if (MorphologicalAnalyses.Count > 0)
            {
                XmlNode nodMA = nodBS.AppendChild(nodParent.OwnerDocument.CreateElement("MorphologicalAnalyses"));
                MorphologicalAnalyses.ForEach(x => x.Serialize(nodMA));
            }
        }

        public override void Delete()
        {
            CheckFilesInUse(Folder);

            try
            {
                // This is the safest way to delete from a list while iterating through it.
                for (int i = MorphologicalAnalyses.Count - 1; i >= 0; i--)
                    MorphologicalAnalyses[i].Delete();
            }
            finally
            {
                MorphologicalAnalyses.Clear();
            }

            foreach (FileInfo file in Folder.GetFiles("*.*", SearchOption.AllDirectories))
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in Folder.GetDirectories("*", SearchOption.AllDirectories))
            {
                dir.Delete();
            }

            try
            {
                Folder.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to delete budget segregation folder", Folder.FullName);
                Console.WriteLine(ex.Message);
            }

            DoD.BudgetSegregations.Remove(this);
            ProjectManager.Project.Save();

            // Remove the "BS" folder if this was the last budget segregation
            if (DoD.BudgetSegregations.Count < 1)
            {
                DoD.BudgetSegFolder.Delete();
            }
        }
    }
}
