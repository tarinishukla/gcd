namespace GCDCore.UserInterface.BudgetSegregation
{
    partial class frmBudgetSegProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBudgetSegProperties));
            this.Label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDoD = new System.Windows.Forms.ComboBox();
            this.txtUncertaintyAnalysis = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtOldDEM = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtNewDEM = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.txtField = new System.Windows.Forms.TextBox();
            this.cboMasks = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(53, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(35, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 9);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(411, 20);
            this.txtName.TabIndex = 1;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cboDoD);
            this.GroupBox1.Controls.Add(this.txtUncertaintyAnalysis);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.txtOldDEM);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.txtNewDEM);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Location = new System.Drawing.Point(13, 41);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(490, 150);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Change Detection Analysis";
            // 
            // cboDoD
            // 
            this.cboDoD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoD.FormattingEnabled = true;
            this.cboDoD.Location = new System.Drawing.Point(120, 27);
            this.cboDoD.Name = "cboDoD";
            this.cboDoD.Size = new System.Drawing.Size(355, 21);
            this.cboDoD.TabIndex = 1;
            // 
            // txtUncertaintyAnalysis
            // 
            this.txtUncertaintyAnalysis.Location = new System.Drawing.Point(120, 118);
            this.txtUncertaintyAnalysis.Name = "txtUncertaintyAnalysis";
            this.txtUncertaintyAnalysis.ReadOnly = true;
            this.txtUncertaintyAnalysis.Size = new System.Drawing.Size(355, 20);
            this.txtUncertaintyAnalysis.TabIndex = 7;
            this.txtUncertaintyAnalysis.TabStop = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(10, 122);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(101, 13);
            this.Label5.TabIndex = 6;
            this.Label5.Text = "Uncertainty analysis";
            // 
            // txtOldDEM
            // 
            this.txtOldDEM.Location = new System.Drawing.Point(120, 88);
            this.txtOldDEM.Name = "txtOldDEM";
            this.txtOldDEM.ReadOnly = true;
            this.txtOldDEM.Size = new System.Drawing.Size(355, 20);
            this.txtOldDEM.TabIndex = 5;
            this.txtOldDEM.TabStop = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(50, 92);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(61, 13);
            this.Label4.TabIndex = 4;
            this.Label4.Text = "Old surface";
            // 
            // txtNewDEM
            // 
            this.txtNewDEM.Location = new System.Drawing.Point(120, 58);
            this.txtNewDEM.Name = "txtNewDEM";
            this.txtNewDEM.ReadOnly = true;
            this.txtNewDEM.Size = new System.Drawing.Size(355, 20);
            this.txtNewDEM.TabIndex = 3;
            this.txtNewDEM.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(44, 62);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(67, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "New surface";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(76, 31);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Name";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(40, 27);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(71, 13);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Feature Class";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(82, 57);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(29, 13);
            this.Label7.TabIndex = 2;
            this.Label7.Text = "Field";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.txtField);
            this.GroupBox2.Controls.Add(this.cboMasks);
            this.GroupBox2.Controls.Add(this.Label7);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Location = new System.Drawing.Point(13, 200);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(493, 88);
            this.GroupBox2.TabIndex = 3;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Polygon Mask";
            // 
            // txtField
            // 
            this.txtField.Location = new System.Drawing.Point(120, 53);
            this.txtField.Name = "txtField";
            this.txtField.ReadOnly = true;
            this.txtField.Size = new System.Drawing.Size(355, 20);
            this.txtField.TabIndex = 3;
            // 
            // cboMasks
            // 
            this.cboMasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMasks.FormattingEnabled = true;
            this.cboMasks.Location = new System.Drawing.Point(120, 23);
            this.cboMasks.Name = "cboMasks";
            this.cboMasks.Size = new System.Drawing.Size(355, 21);
            this.cboMasks.TabIndex = 1;
            this.cboMasks.SelectedIndexChanged += new System.EventHandler(this.cboMasks_SelectedIndexChanged);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(20, 304);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(68, 13);
            this.Label8.TabIndex = 4;
            this.Label8.Text = "Output folder";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(95, 300);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(411, 20);
            this.txtOutputFolder.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(428, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Image = global::GCDCore.Properties.Resources.Save;
            this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdOK.Location = new System.Drawing.Point(347, 340);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.Location = new System.Drawing.Point(13, 340);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(75, 23);
            this.cmdHelp.TabIndex = 8;
            this.cmdHelp.Text = "Help";
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // frmBudgetSegProperties
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(517, 375);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBudgetSegProperties";
            this.Text = "Budget Segregation Properties";
            this.Load += new System.EventHandler(this.frmBudgetSegProperties_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox txtUncertaintyAnalysis;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtOldDEM;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtNewDEM;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.Button cmdHelp;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtOutputFolder;
        internal System.Windows.Forms.ComboBox cboDoD;
        private System.Windows.Forms.TextBox txtField;
        private System.Windows.Forms.ComboBox cboMasks;
    }
}
