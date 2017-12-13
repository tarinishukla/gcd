using GCDCore.Project;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace GCDCore.UserInterface.ChangeDetection
{
    public partial class frmDoDProperties
    {
        private bool m_bUserEditedName;

        private CoherenceProperties CoherenceProps;
        // These are the results of the analysis. They are not populated until
        // the user clicks OK and the change detection completes successfully.

        private DoDBase m_DoD;
        /// <summary>
        /// Retrieve the GCD project dataset DoD row generated by the change detection
        /// </summary>
        /// <returns>GCD project dataset DoD row generated by the change detection</returns>
        /// <remarks>Returns nothing if not calculated.</remarks>
        public DoDBase DoD
        {
            get { return m_DoD; }
        }

        public frmDoDProperties()
        {
            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            m_bUserEditedName = false;
        }

        public frmDoDProperties(DEMSurvey newDEM, DEMSurvey oldDEM)
        {
            // This call is required by the designer.
            InitializeComponent();

            ucDEMs.NewDEM = newDEM;
            ucDEMs.OldDEM = oldDEM;

            // Add any initialization after the InitializeComponent() call.
            //m_pArcMap = pArcMap
            m_bUserEditedName = false;
        }

        private void DoDPropertiesForm_Load(object sender, System.EventArgs e)
        {
            // Initialize coherence properties in case they are needed.
            CoherenceProps = new CoherenceProperties();

            EnableDisableControls();

            lblMinLodThreshold.Text = string.Format("Threshold ({0})", UnitsNet.Length.GetAbbreviation(ProjectManager.Project.Units.VertUnit));

            // Subscribe to the event when DEM selection changes
            ucDEMs.SelectedDEMsChanged += UpdateAnalysisName;

            UpdateAnalysisName(sender, e);
        }

        private void cmdOK_Click(System.Object sender, System.EventArgs e)
        {
            if (!ValidateForm())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            try
            {
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                System.IO.DirectoryInfo dFolder = new System.IO.DirectoryInfo(txtOutputFolder.Text);
                GCDCore.Engines.ChangeDetectionEngineBase cdEngine = null;

                if (rdoMinLOD.Checked)
                {
                    cdEngine = new GCDCore.Engines.ChangeDetectionEngineMinLoD(txtName.Text, dFolder, ucDEMs.NewDEM, ucDEMs.OldDEM, valMinLodThreshold.Value);
                }
                else
                {
                    if (rdoPropagated.Checked)
                    {
                        cdEngine = new GCDCore.Engines.ChangeDetectionEnginePropProb(txtName.Text, dFolder, ucDEMs.NewDEM, ucDEMs.OldDEM, ucDEMs.NewError, ucDEMs.OldError);
                    }
                    else
                    {
                        CoherenceProperties spatCoherence = chkBayesian.Checked ? spatCoherence = CoherenceProps : null;
                        cdEngine = new Engines.ChangeDetectionEngineProbabilistic(txtName.Text, dFolder, ucDEMs.NewDEM, ucDEMs.OldDEM, ucDEMs.NewError, ucDEMs.OldError, valConfidence.Value, spatCoherence);
                    }
                }

                m_DoD = cdEngine.Calculate(true, ProjectManager.Project.Units);

                ProjectManager.Project.DoDs[txtName.Name] = m_DoD;
                ProjectManager.Project.Save();

            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
                m_DoD = null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ValidateForm()
        {
            // Safety step to prevent names with just spaces or difficult to detect trailing spaces
            txtName.Text = txtName.Text.Trim();

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Please enter a name for the analysis.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            if (System.IO.Directory.Exists(txtOutputFolder.Text))
            {
                MessageBox.Show("An analysis folder with the same output path already exists. Please change the analysis name so that a different output folder will be used.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            if (!ProjectManager.Project.IsDoDNameUnique(txtName.Text, null))
            {
                MessageBox.Show("A change detection already exists with this name. Please choose a unique name.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            if (!ucDEMs.ValidateForm())
            {
                ucDEMs.Select();
                return false;
            }

            return true;
        }

        private void rdoProbabilistic_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableDisableControls();
            UpdateAnalysisName(sender, e);
        }

        private void EnableDisableControls()
        {
            ucDEMs.EnableErrorSurfaces(!rdoMinLOD.Checked);

            valMinLodThreshold.Enabled = rdoMinLOD.Checked;
            lblMinLodThreshold.Enabled = rdoMinLOD.Checked;

            lblConfidence.Enabled = rdoProbabilistic.Checked;
            valConfidence.Enabled = rdoProbabilistic.Checked;
            chkBayesian.Enabled = rdoProbabilistic.Checked;
            cmdBayesianProperties.Enabled = rdoProbabilistic.Checked && chkBayesian.Checked;
        }

        #region "DEM Selection Changed"  

        private void UpdateAnalysisName(object sender, EventArgs e)
        {
            if (m_bUserEditedName)
            {
                return;
            }

            if (ucDEMs.NewDEM == null || ucDEMs.OldDEM == null)
            {
                return;
            }

            string sAnalysisName = naru.os.File.RemoveDangerousCharacters(ucDEMs.NewDEM.Name);
            if (!string.IsNullOrEmpty(sAnalysisName))
            {
                sAnalysisName += "_";
            }

            if (!string.IsNullOrEmpty(ucDEMs.OldDEM.Name))
            {
                sAnalysisName += naru.os.File.RemoveDangerousCharacters(ucDEMs.OldDEM.Name);
            }

            if (rdoMinLOD.Checked)
            {
                sAnalysisName += " MinLoD " + valMinLodThreshold.Value.ToString("#0.00");
            }
            else if (rdoPropagated.Checked)
            {
                sAnalysisName += " Prop";
            }
            else
            {
                sAnalysisName += " Prob " + valConfidence.Value.ToString("#0.00");
            }

            txtName.Text = sAnalysisName.Trim();
        }

        #endregion

        private void rdoCommonArea_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableDisableControls();
        }

        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            m_bUserEditedName = true;
        }

        private void txtName_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    txtOutputFolder.Text = string.Empty;
                }
                else
                {
                    txtOutputFolder.Text = ProjectManager.OutputManager.GetDoDOutputFolder(txtName.Text);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void cmdBayesianProperties_Click(System.Object sender, System.EventArgs e)
        {
            frmCoherenceProperties frm = new frmCoherenceProperties(CoherenceProps);
            frm.ShowDialog();
        }

        private void chkBayesian_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableDisableControls();
        }

        private void Threshold_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateAnalysisName(sender, e);
        }

        private void cmdHelp_Click(System.Object sender, System.EventArgs e)
        {
            Process.Start(GCDCore.Properties.Resources.HelpBaseURL + "gcd-command-reference/gcd-project-explorer/j-change-detection-context-menu/i-add-change-detection");
        }
    }
}
