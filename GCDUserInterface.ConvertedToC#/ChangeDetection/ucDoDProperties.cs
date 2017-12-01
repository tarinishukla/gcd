using GCDCore.Project;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCDUserInterface.ChangeDetection
{

	public partial class ucDoDProperties
	{


		private void DodPropertiesUC_Load(object sender, System.EventArgs e)
		{
			// Make all textboxes have invisible border. Easier to keep border turned on
			// for working in the designer.
			foreach (object aControl_loopVariable in this.Controls) {
				aControl = aControl_loopVariable;
				if (aControl is TextBox) {
					((TextBox)aControl).BorderStyle = System.Windows.Forms.BorderStyle.None;
				}
			}

		}


		public void Initialize(DoDBase dod)
		{
			txtNewDEM.Text = dod.NewDEM.Name;
			txtOldDEM.Text = dod.OldDEM.Name;

			if (dod is DoDMinLoD) {
				txtType.Text = "Minimum Level of Detection (MinLod)";
				var _with1 = (DoDMinLoD)dod;
				txtThreshold.Text = string.Format("{0:0.00}{1}", _with1.Threshold, UnitsNet.Length.GetAbbreviation(ProjectManager.Project.Units.VertUnit));
			} else {
				txtType.Text = "Propagated Error";
				grpPropagated.Visible = true;

				var _with2 = (DoDPropagated)dod;
				txtNewError.Text = _with2.NewError.Name;
				txtOldError.Text = _with2.OldError.Name;
				txtPropErr.Text = _with2.PropagatedError.RelativePath;

				if (dod is DoDProbabilistic) {
					txtType.Text = "Probabilistic";
					grpProbabilistic.Visible = true;
					grpPropagated.Visible = true;

					var _with3 = (DoDProbabilistic)dod;
					txtConfidence.Text = (100 * _with3.ConfidenceLevel).ToString("0") + "%";
					txtProbabilityRaster.Text = _with3.PriorProbability.RelativePath;
					txtBayesian.Text = "None";

					if (_with3.SpatialCoherence is GCDCore.Project.CoherenceProperties) {
						txtPosteriorRaster.Text = _with3.PosteriorProbability.RelativePath;
						txtConditionalRaster.Text = _with3.ConditionalRaster.RelativePath;
						txtErosionalSpatialCoherenceRaster.Text = _with3.SpatialCoherenceErosion.RelativePath;
						txtDepositionSpatialCoherenceRaster.Text = _with3.SpatialCoherenceDeposition.RelativePath;
						txtBayesian.Text = string.Format("Bayesian updating with filter size of {0} X {0} cells", _with3.SpatialCoherence.MovingWindowDimensions);
					}
				}
			}
		}


		private void AddToMapToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			ToolStripMenuItem myItem = (ToolStripMenuItem)sender;
			ContextMenuStrip cms = (ContextMenuStrip)myItem.Owner;

			string sItemName = string.Empty;

			if (cms.SourceControl.Name.ToLower().Contains("dem")) {
				// Get the new or old survey name
				if (cms.SourceControl.Name.ToLower().Contains("new")) {
					sItemName = txtNewDEM.Text;
				} else {
					sItemName = txtOldDEM.Text;
				}

				throw new NotImplementedException("add to map functionality commented out");
				//For Each aDEMSurvey In m_rDoD.ProjectRow.GetDEMSurveyRows
				//    If String.Compare(sItemName, aDEMSurvey.Name, True) = 0 Then
				//        '  Core.GCDProject.ProjectManagerUI.ArcMapManager.AddDEM(aDEMSurvey)
				//        Exit Sub
				//    End If
				//Next


			} else if (cms.SourceControl.Name.ToLower().Contains("error")) {
				// Get the new or old error surface name
				if (cms.SourceControl.Name.ToLower().Contains("old")) {
					sItemName = txtNewError.Text;
				} else {
					sItemName = txtOldError.Text;
				}

				throw new NotImplementedException("Add to map functionaility commented out");
				//For Each aDEMSurvey In m_rDoD.ProjectRow.GetDEMSurveyRows
				//    For Each anErrorSurface In aDEMSurvey.GetErrorSurfaceRows
				//        If String.Compare(anErrorSurface.Name, sItemName, True) = 0 Then
				//            'Core.GCDProject.ProjectManagerUI.ArcMapManager.AddErrSurface(anErrorSurface)
				//            Exit Sub
				//        End If
				//    Next
				//Next
			} else {
				string sPath = cms.SourceControl.Text;
				if (!string.IsNullOrEmpty(sPath)) {
					if (System.IO.File.Exists(sPath)) {
						//GISCode.ArcMap.AddToMap(My.ThisApplication, sPath, IO.Path.GetFileNameWithoutExtension(sPath), GISDataStructures.BasicGISTypes.Raster)
						string sFileName = System.IO.Path.GetFileNameWithoutExtension(sPath);

						switch (sFileName) {

							case "PropErr":
								// TODO 
								throw new Exception("not implemented");
							// Core.GCDProject.ProjectManagerUI.ArcMapManager.AddPropErr(m_rDoD)

							case "priorProb":
								// TODO 
								throw new Exception("not implemented");
							//  Core.GCDProject.ProjectManagerUI.ArcMapManager.AddProbabilityRaster(m_rDoD, m_rDoD.ProbabilityRaster)

							case "nbrErosion":
								// TODO 
								throw new Exception("not implemented");
							// Core.GCDProject.ProjectManagerUI.ArcMapManager.AddProbabilityRaster(m_rDoD, m_rDoD.SpatialCoErosionRaster)

							case "nbrDeposition":
								// TODO 
								throw new Exception("not implemented");
							//  Core.GCDProject.ProjectManagerUI.ArcMapManager.AddProbabilityRaster(m_rDoD, m_rDoD.SpatialCoDepositionRaster)

						}
					}
				}
			}
		}

		private void PropertiesToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			Interaction.MsgBox("Properties not yet implemented", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
		}
		public ucDoDProperties()
		{
			Load += DodPropertiesUC_Load;
			InitializeComponent();
		}

	}

}