﻿Namespace UI.SurveyLibrary

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmDEMSurveyProperties
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDEMSurveyProperties))
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnOK = New System.Windows.Forms.Button()
            Me.DEMSurveyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.DesignProjectDS = New ProjectDS()
            Me.DEMSurfaceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.ErrorTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.btnHlp = New System.Windows.Forms.Button()
            Me.ttpTooltip = New System.Windows.Forms.ToolTip(Me.components)
            Me.pgeErrors = New System.Windows.Forms.TabPage()
            Me.btnCalculateError = New System.Windows.Forms.Button()
            Me.btnAddErrorToMap = New System.Windows.Forms.Button()
            Me.ErrorTableDataGridView = New System.Windows.Forms.DataGridView()
            Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.btnErrorDelete = New System.Windows.Forms.Button()
            Me.btnErrorSettings = New System.Windows.Forms.Button()
            Me.btnAddError = New System.Windows.Forms.Button()
            Me.pgeSurfaces = New System.Windows.Forms.TabPage()
            Me.DEMSurfaceDataGridView = New System.Windows.Forms.DataGridView()
            Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.btnAddAssociatedSurface = New System.Windows.Forms.Button()
            Me.btnAddToMap = New System.Windows.Forms.Button()
            Me.btnDeleteAssociatedSurface = New System.Windows.Forms.Button()
            Me.btnSettingsAssociatedSurface = New System.Windows.Forms.Button()
            Me.pgeSurvey = New System.Windows.Forms.TabPage()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.txtProperties = New System.Windows.Forms.TextBox()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.txtMask = New System.Windows.Forms.TextBox()
            Me.cboSingle = New System.Windows.Forms.ComboBox()
            Me.SurveyTypesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.SurveyTypes = New SurveyTypes()
            Me.btnBrowseMask = New System.Windows.Forms.Button()
            Me.cmdAddToMap = New System.Windows.Forms.Button()
            Me.cboIdentify = New System.Windows.Forms.ComboBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.rdoMulti = New System.Windows.Forms.RadioButton()
            Me.rdoSingle = New System.Windows.Forms.RadioButton()
            Me.txtRasterPath = New System.Windows.Forms.TextBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.txtName = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.tabControl = New System.Windows.Forms.TabControl()
            Me.txtFolder = New System.Windows.Forms.TextBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.cmdDateTime = New System.Windows.Forms.Button()
            Me.lblDatetime = New System.Windows.Forms.Label()
            CType(Me.DEMSurveyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DesignProjectDS, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DEMSurfaceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ErrorTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pgeErrors.SuspendLayout()
            CType(Me.ErrorTableDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pgeSurfaces.SuspendLayout()
            CType(Me.DEMSurfaceDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pgeSurvey.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            CType(Me.SurveyTypesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SurveyTypes, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tabControl.SuspendLayout()
            Me.SuspendLayout()
            '
            'btnCancel
            '
            Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.Location = New System.Drawing.Point(300, 577)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 23)
            Me.btnCancel.TabIndex = 8
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            Me.btnCancel.Visible = False
            '
            'btnOK
            '
            Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnOK.Location = New System.Drawing.Point(482, 577)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(140, 23)
            Me.btnOK.TabIndex = 7
            Me.btnOK.Text = "Save Survey and Close"
            Me.btnOK.UseVisualStyleBackColor = True
            '
            'DEMSurveyBindingSource
            '
            Me.DEMSurveyBindingSource.DataMember = "DEMSurvey"
            Me.DEMSurveyBindingSource.DataSource = Me.DesignProjectDS
            '
            'DesignProjectDS
            '
            Me.DesignProjectDS.DataSetName = "ProjectDS"
            Me.DesignProjectDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'DEMSurfaceBindingSource
            '
            Me.DEMSurfaceBindingSource.DataMember = "AssociatedSurface"
            Me.DEMSurfaceBindingSource.DataSource = Me.DesignProjectDS
            '
            'ErrorTableBindingSource
            '
            Me.ErrorTableBindingSource.DataMember = "ErrorSurface"
            Me.ErrorTableBindingSource.DataSource = Me.DesignProjectDS
            '
            'btnHlp
            '
            Me.btnHlp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnHlp.Location = New System.Drawing.Point(12, 577)
            Me.btnHlp.Name = "btnHlp"
            Me.btnHlp.Size = New System.Drawing.Size(75, 23)
            Me.btnHlp.TabIndex = 9
            Me.btnHlp.Text = "Help"
            Me.btnHlp.UseVisualStyleBackColor = True
            '
            'pgeErrors
            '
            Me.pgeErrors.Controls.Add(Me.btnCalculateError)
            Me.pgeErrors.Controls.Add(Me.btnAddErrorToMap)
            Me.pgeErrors.Controls.Add(Me.ErrorTableDataGridView)
            Me.pgeErrors.Controls.Add(Me.btnErrorDelete)
            Me.pgeErrors.Controls.Add(Me.btnErrorSettings)
            Me.pgeErrors.Controls.Add(Me.btnAddError)
            Me.pgeErrors.Location = New System.Drawing.Point(4, 22)
            Me.pgeErrors.Name = "pgeErrors"
            Me.pgeErrors.Padding = New System.Windows.Forms.Padding(3)
            Me.pgeErrors.Size = New System.Drawing.Size(602, 460)
            Me.pgeErrors.TabIndex = 2
            Me.pgeErrors.Text = "Error Calculations"
            Me.pgeErrors.UseVisualStyleBackColor = True
            '
            'btnCalculateError
            '
            Me.btnCalculateError.Image = My.Resources.Resources.sigma
            Me.btnCalculateError.Location = New System.Drawing.Point(40, 6)
            Me.btnCalculateError.Name = "btnCalculateError"
            Me.btnCalculateError.Size = New System.Drawing.Size(29, 23)
            Me.btnCalculateError.TabIndex = 5
            Me.btnCalculateError.UseVisualStyleBackColor = True
            '
            'btnAddErrorToMap
            '
            Me.btnAddErrorToMap.Enabled = False
            Me.btnAddErrorToMap.Image = My.Resources.Resources.AddToMap
            Me.btnAddErrorToMap.Location = New System.Drawing.Point(144, 6)
            Me.btnAddErrorToMap.Name = "btnAddErrorToMap"
            Me.btnAddErrorToMap.Size = New System.Drawing.Size(29, 23)
            Me.btnAddErrorToMap.TabIndex = 3
            Me.btnAddErrorToMap.UseVisualStyleBackColor = True
            '
            'ErrorTableDataGridView
            '
            Me.ErrorTableDataGridView.AllowUserToAddRows = False
            Me.ErrorTableDataGridView.AllowUserToDeleteRows = False
            Me.ErrorTableDataGridView.AutoGenerateColumns = False
            Me.ErrorTableDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
            Me.ErrorTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ErrorTableDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
            Me.ErrorTableDataGridView.DataSource = Me.ErrorTableBindingSource
            Me.ErrorTableDataGridView.Location = New System.Drawing.Point(3, 35)
            Me.ErrorTableDataGridView.MultiSelect = False
            Me.ErrorTableDataGridView.Name = "ErrorTableDataGridView"
            Me.ErrorTableDataGridView.ReadOnly = True
            Me.ErrorTableDataGridView.RowHeadersVisible = False
            Me.ErrorTableDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ErrorTableDataGridView.Size = New System.Drawing.Size(593, 493)
            Me.ErrorTableDataGridView.TabIndex = 4
            '
            'DataGridViewTextBoxColumn7
            '
            Me.DataGridViewTextBoxColumn7.DataPropertyName = "Name"
            Me.DataGridViewTextBoxColumn7.HeaderText = "Name"
            Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
            Me.DataGridViewTextBoxColumn7.ReadOnly = True
            Me.DataGridViewTextBoxColumn7.Width = 150
            '
            'DataGridViewTextBoxColumn8
            '
            Me.DataGridViewTextBoxColumn8.DataPropertyName = "Type"
            Me.DataGridViewTextBoxColumn8.HeaderText = "Type"
            Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
            Me.DataGridViewTextBoxColumn8.ReadOnly = True
            Me.DataGridViewTextBoxColumn8.Width = 150
            '
            'DataGridViewTextBoxColumn9
            '
            Me.DataGridViewTextBoxColumn9.DataPropertyName = "Source"
            Me.DataGridViewTextBoxColumn9.HeaderText = "Source"
            Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
            Me.DataGridViewTextBoxColumn9.ReadOnly = True
            Me.DataGridViewTextBoxColumn9.Width = 290
            '
            'btnErrorDelete
            '
            Me.btnErrorDelete.Enabled = False
            Me.btnErrorDelete.Image = My.Resources.Resources.Delete
            Me.btnErrorDelete.Location = New System.Drawing.Point(74, 6)
            Me.btnErrorDelete.Name = "btnErrorDelete"
            Me.btnErrorDelete.Size = New System.Drawing.Size(29, 23)
            Me.btnErrorDelete.TabIndex = 2
            Me.btnErrorDelete.UseVisualStyleBackColor = True
            '
            'btnErrorSettings
            '
            Me.btnErrorSettings.Enabled = False
            Me.btnErrorSettings.Image = My.Resources.Resources.Settings
            Me.btnErrorSettings.Location = New System.Drawing.Point(109, 6)
            Me.btnErrorSettings.Name = "btnErrorSettings"
            Me.btnErrorSettings.Size = New System.Drawing.Size(29, 23)
            Me.btnErrorSettings.TabIndex = 1
            Me.btnErrorSettings.UseVisualStyleBackColor = True
            '
            'btnAddError
            '
            Me.btnAddError.Image = My.Resources.Resources.Add
            Me.btnAddError.Location = New System.Drawing.Point(6, 6)
            Me.btnAddError.Name = "btnAddError"
            Me.btnAddError.Size = New System.Drawing.Size(29, 23)
            Me.btnAddError.TabIndex = 0
            Me.btnAddError.UseVisualStyleBackColor = True
            '
            'pgeSurfaces
            '
            Me.pgeSurfaces.AutoScroll = True
            Me.pgeSurfaces.Controls.Add(Me.DEMSurfaceDataGridView)
            Me.pgeSurfaces.Controls.Add(Me.btnAddAssociatedSurface)
            Me.pgeSurfaces.Controls.Add(Me.btnAddToMap)
            Me.pgeSurfaces.Controls.Add(Me.btnDeleteAssociatedSurface)
            Me.pgeSurfaces.Controls.Add(Me.btnSettingsAssociatedSurface)
            Me.pgeSurfaces.Location = New System.Drawing.Point(4, 22)
            Me.pgeSurfaces.Name = "pgeSurfaces"
            Me.pgeSurfaces.Padding = New System.Windows.Forms.Padding(3)
            Me.pgeSurfaces.Size = New System.Drawing.Size(602, 460)
            Me.pgeSurfaces.TabIndex = 1
            Me.pgeSurfaces.Text = "Associated Surfaces"
            Me.pgeSurfaces.UseVisualStyleBackColor = True
            '
            'DEMSurfaceDataGridView
            '
            Me.DEMSurfaceDataGridView.AllowUserToAddRows = False
            Me.DEMSurfaceDataGridView.AllowUserToDeleteRows = False
            Me.DEMSurfaceDataGridView.AutoGenerateColumns = False
            Me.DEMSurfaceDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DEMSurfaceDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DEMSurfaceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DEMSurfaceDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
            Me.DEMSurfaceDataGridView.DataSource = Me.DEMSurfaceBindingSource
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DEMSurfaceDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.DEMSurfaceDataGridView.Location = New System.Drawing.Point(3, 35)
            Me.DEMSurfaceDataGridView.MultiSelect = False
            Me.DEMSurfaceDataGridView.Name = "DEMSurfaceDataGridView"
            Me.DEMSurfaceDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DEMSurfaceDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DEMSurfaceDataGridView.RowHeadersVisible = False
            Me.DEMSurfaceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DEMSurfaceDataGridView.Size = New System.Drawing.Size(593, 425)
            Me.DEMSurfaceDataGridView.TabIndex = 4
            '
            'DataGridViewTextBoxColumn3
            '
            Me.DataGridViewTextBoxColumn3.DataPropertyName = "Name"
            Me.DataGridViewTextBoxColumn3.HeaderText = "Name"
            Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
            Me.DataGridViewTextBoxColumn3.ReadOnly = True
            Me.DataGridViewTextBoxColumn3.Width = 150
            '
            'DataGridViewTextBoxColumn4
            '
            Me.DataGridViewTextBoxColumn4.DataPropertyName = "Type"
            Me.DataGridViewTextBoxColumn4.HeaderText = "Type"
            Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
            Me.DataGridViewTextBoxColumn4.ReadOnly = True
            Me.DataGridViewTextBoxColumn4.Width = 150
            '
            'DataGridViewTextBoxColumn5
            '
            Me.DataGridViewTextBoxColumn5.DataPropertyName = "Source"
            Me.DataGridViewTextBoxColumn5.HeaderText = "Source"
            Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
            Me.DataGridViewTextBoxColumn5.ReadOnly = True
            Me.DataGridViewTextBoxColumn5.Width = 290
            '
            'btnAddAssociatedSurface
            '
            Me.btnAddAssociatedSurface.Image = My.Resources.Resources.Add
            Me.btnAddAssociatedSurface.Location = New System.Drawing.Point(3, 6)
            Me.btnAddAssociatedSurface.Name = "btnAddAssociatedSurface"
            Me.btnAddAssociatedSurface.Size = New System.Drawing.Size(29, 23)
            Me.btnAddAssociatedSurface.TabIndex = 0
            Me.btnAddAssociatedSurface.UseVisualStyleBackColor = True
            '
            'btnAddToMap
            '
            Me.btnAddToMap.Enabled = False
            Me.btnAddToMap.Image = My.Resources.Resources.AddToMap
            Me.btnAddToMap.Location = New System.Drawing.Point(108, 6)
            Me.btnAddToMap.Name = "btnAddToMap"
            Me.btnAddToMap.Size = New System.Drawing.Size(29, 23)
            Me.btnAddToMap.TabIndex = 3
            Me.btnAddToMap.UseVisualStyleBackColor = True
            '
            'btnDeleteAssociatedSurface
            '
            Me.btnDeleteAssociatedSurface.Enabled = False
            Me.btnDeleteAssociatedSurface.Image = My.Resources.Resources.Delete
            Me.btnDeleteAssociatedSurface.Location = New System.Drawing.Point(73, 6)
            Me.btnDeleteAssociatedSurface.Name = "btnDeleteAssociatedSurface"
            Me.btnDeleteAssociatedSurface.Size = New System.Drawing.Size(29, 23)
            Me.btnDeleteAssociatedSurface.TabIndex = 2
            Me.btnDeleteAssociatedSurface.UseVisualStyleBackColor = True
            '
            'btnSettingsAssociatedSurface
            '
            Me.btnSettingsAssociatedSurface.Enabled = False
            Me.btnSettingsAssociatedSurface.Image = My.Resources.Resources.Settings
            Me.btnSettingsAssociatedSurface.Location = New System.Drawing.Point(38, 6)
            Me.btnSettingsAssociatedSurface.Name = "btnSettingsAssociatedSurface"
            Me.btnSettingsAssociatedSurface.Size = New System.Drawing.Size(29, 23)
            Me.btnSettingsAssociatedSurface.TabIndex = 1
            Me.btnSettingsAssociatedSurface.UseVisualStyleBackColor = True
            '
            'pgeSurvey
            '
            Me.pgeSurvey.Controls.Add(Me.GroupBox2)
            Me.pgeSurvey.Controls.Add(Me.GroupBox1)
            Me.pgeSurvey.Location = New System.Drawing.Point(4, 22)
            Me.pgeSurvey.Name = "pgeSurvey"
            Me.pgeSurvey.Padding = New System.Windows.Forms.Padding(3)
            Me.pgeSurvey.Size = New System.Drawing.Size(602, 460)
            Me.pgeSurvey.TabIndex = 0
            Me.pgeSurvey.Text = "DEM Survey"
            Me.pgeSurvey.UseVisualStyleBackColor = True
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.txtProperties)
            Me.GroupBox2.Location = New System.Drawing.Point(18, 239)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(565, 209)
            Me.GroupBox2.TabIndex = 1
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Survey Raster Properties"
            '
            'txtProperties
            '
            Me.txtProperties.Location = New System.Drawing.Point(8, 20)
            Me.txtProperties.Multiline = True
            Me.txtProperties.Name = "txtProperties"
            Me.txtProperties.ReadOnly = True
            Me.txtProperties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtProperties.Size = New System.Drawing.Size(551, 183)
            Me.txtProperties.TabIndex = 0
            Me.txtProperties.TabStop = False
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.txtMask)
            Me.GroupBox1.Controls.Add(Me.cboSingle)
            Me.GroupBox1.Controls.Add(Me.btnBrowseMask)
            Me.GroupBox1.Controls.Add(Me.cmdAddToMap)
            Me.GroupBox1.Controls.Add(Me.cboIdentify)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label6)
            Me.GroupBox1.Controls.Add(Me.rdoMulti)
            Me.GroupBox1.Controls.Add(Me.rdoSingle)
            Me.GroupBox1.Controls.Add(Me.txtRasterPath)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Location = New System.Drawing.Point(18, 16)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(565, 217)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Source"
            '
            'txtMask
            '
            Me.txtMask.Location = New System.Drawing.Point(119, 144)
            Me.txtMask.Name = "txtMask"
            Me.txtMask.ReadOnly = True
            Me.txtMask.Size = New System.Drawing.Size(405, 20)
            Me.txtMask.TabIndex = 8
            Me.txtMask.TabStop = False
            '
            'cboSingle
            '
            Me.cboSingle.DataSource = Me.SurveyTypesBindingSource
            Me.cboSingle.DisplayMember = "Name"
            Me.cboSingle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSingle.FormattingEnabled = True
            Me.cboSingle.Location = New System.Drawing.Point(41, 80)
            Me.cboSingle.Name = "cboSingle"
            Me.cboSingle.Size = New System.Drawing.Size(186, 21)
            Me.cboSingle.TabIndex = 5
            Me.cboSingle.ValueMember = "Name"
            '
            'SurveyTypesBindingSource
            '
            Me.SurveyTypesBindingSource.AllowNew = False
            Me.SurveyTypesBindingSource.DataMember = "SurveyTypes"
            Me.SurveyTypesBindingSource.DataSource = Me.SurveyTypes
            '
            'SurveyTypes
            '
            Me.SurveyTypes.DataSetName = "SurveyTypes"
            Me.SurveyTypes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'btnBrowseMask
            '
            Me.btnBrowseMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBrowseMask.Image = My.Resources.Resources.BrowseFolder
            Me.btnBrowseMask.Location = New System.Drawing.Point(530, 143)
            Me.btnBrowseMask.Name = "btnBrowseMask"
            Me.btnBrowseMask.Size = New System.Drawing.Size(29, 23)
            Me.btnBrowseMask.TabIndex = 9
            Me.btnBrowseMask.UseVisualStyleBackColor = True
            '
            'cmdAddToMap
            '
            Me.cmdAddToMap.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdAddToMap.Image = My.Resources.AddToMap
            Me.cmdAddToMap.Location = New System.Drawing.Point(530, 25)
            Me.cmdAddToMap.Name = "cmdAddToMap"
            Me.cmdAddToMap.Size = New System.Drawing.Size(29, 23)
            Me.cmdAddToMap.TabIndex = 2
            Me.cmdAddToMap.UseVisualStyleBackColor = True
            '
            'cboIdentify
            '
            Me.cboIdentify.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboIdentify.FormattingEnabled = True
            Me.cboIdentify.Location = New System.Drawing.Point(119, 181)
            Me.cboIdentify.Name = "cboIdentify"
            Me.cboIdentify.Size = New System.Drawing.Size(149, 21)
            Me.cboIdentify.TabIndex = 11
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(41, 184)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(72, 13)
            Me.Label5.TabIndex = 10
            Me.Label5.Text = "Identifier field:"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(41, 148)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(36, 13)
            Me.Label6.TabIndex = 7
            Me.Label6.Text = "Mask:"
            '
            'rdoMulti
            '
            Me.rdoMulti.AutoSize = True
            Me.rdoMulti.Location = New System.Drawing.Point(18, 122)
            Me.rdoMulti.Name = "rdoMulti"
            Me.rdoMulti.Size = New System.Drawing.Size(119, 17)
            Me.rdoMulti.TabIndex = 6
            Me.rdoMulti.TabStop = True
            Me.rdoMulti.Text = "Multi-method survey"
            Me.rdoMulti.UseVisualStyleBackColor = True
            '
            'rdoSingle
            '
            Me.rdoSingle.AutoSize = True
            Me.rdoSingle.Checked = True
            Me.rdoSingle.Location = New System.Drawing.Point(18, 57)
            Me.rdoSingle.Name = "rdoSingle"
            Me.rdoSingle.Size = New System.Drawing.Size(126, 17)
            Me.rdoSingle.TabIndex = 4
            Me.rdoSingle.TabStop = True
            Me.rdoSingle.Text = "Single method survey"
            Me.rdoSingle.UseVisualStyleBackColor = True
            '
            'txtRasterPath
            '
            Me.txtRasterPath.Location = New System.Drawing.Point(97, 26)
            Me.txtRasterPath.Name = "txtRasterPath"
            Me.txtRasterPath.ReadOnly = True
            Me.txtRasterPath.Size = New System.Drawing.Size(427, 20)
            Me.txtRasterPath.TabIndex = 1
            Me.txtRasterPath.TabStop = False
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(15, 30)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(76, 13)
            Me.Label3.TabIndex = 0
            Me.Label3.Text = "Raster source:"
            '
            'txtName
            '
            Me.txtName.Location = New System.Drawing.Point(89, 19)
            Me.txtName.Name = "txtName"
            Me.txtName.Size = New System.Drawing.Size(286, 20)
            Me.txtName.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 19)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(74, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Survey Name:"
            '
            'tabControl
            '
            Me.tabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tabControl.Controls.Add(Me.pgeSurvey)
            Me.tabControl.Controls.Add(Me.pgeSurfaces)
            Me.tabControl.Controls.Add(Me.pgeErrors)
            Me.tabControl.Location = New System.Drawing.Point(12, 85)
            Me.tabControl.Name = "tabControl"
            Me.tabControl.SelectedIndex = 0
            Me.tabControl.Size = New System.Drawing.Size(610, 486)
            Me.tabControl.TabIndex = 6
            '
            'txtFolder
            '
            Me.txtFolder.Location = New System.Drawing.Point(89, 49)
            Me.txtFolder.Name = "txtFolder"
            Me.txtFolder.ReadOnly = True
            Me.txtFolder.Size = New System.Drawing.Size(533, 20)
            Me.txtFolder.TabIndex = 5
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(12, 53)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(39, 13)
            Me.Label4.TabIndex = 4
            Me.Label4.Text = "Folder:"
            '
            'cmdDateTime
            '
            Me.cmdDateTime.Location = New System.Drawing.Point(382, 18)
            Me.cmdDateTime.Name = "cmdDateTime"
            Me.cmdDateTime.Size = New System.Drawing.Size(108, 23)
            Me.cmdDateTime.TabIndex = 2
            Me.cmdDateTime.Text = "Survey Date/Time"
            Me.cmdDateTime.UseVisualStyleBackColor = True
            '
            'lblDatetime
            '
            Me.lblDatetime.AutoSize = True
            Me.lblDatetime.Location = New System.Drawing.Point(497, 23)
            Me.lblDatetime.Name = "lblDatetime"
            Me.lblDatetime.Size = New System.Drawing.Size(99, 13)
            Me.lblDatetime.TabIndex = 3
            Me.lblDatetime.Text = "10 Dec 2012 23:59"
            '
            'SurveyPropertiesForm
            '
            Me.AcceptButton = Me.btnOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(634, 612)
            Me.ControlBox = False
            Me.Controls.Add(Me.lblDatetime)
            Me.Controls.Add(Me.cmdDateTime)
            Me.Controls.Add(Me.txtFolder)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.btnHlp)
            Me.Controls.Add(Me.tabControl)
            Me.Controls.Add(Me.btnOK)
            Me.Controls.Add(Me.txtName)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "SurveyPropertiesForm"
            Me.Text = "DEM Survey Properties"
            CType(Me.DEMSurveyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DesignProjectDS, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DEMSurfaceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ErrorTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pgeErrors.ResumeLayout(False)
            CType(Me.ErrorTableDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pgeSurfaces.ResumeLayout(False)
            CType(Me.DEMSurfaceDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pgeSurvey.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.SurveyTypesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.SurveyTypes, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tabControl.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnOK As System.Windows.Forms.Button
        Friend WithEvents btnHlp As System.Windows.Forms.Button
        Friend WithEvents DEMSurveyBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents DesignProjectDS As ProjectDS
        Friend WithEvents DEMSurfaceBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents ErrorTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents ttpTooltip As System.Windows.Forms.ToolTip
        Friend WithEvents pgeErrors As System.Windows.Forms.TabPage
        Friend WithEvents btnAddErrorToMap As System.Windows.Forms.Button
        Friend WithEvents ErrorTableDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents btnErrorDelete As System.Windows.Forms.Button
        Friend WithEvents btnErrorSettings As System.Windows.Forms.Button
        Friend WithEvents btnAddError As System.Windows.Forms.Button
        Friend WithEvents pgeSurfaces As System.Windows.Forms.TabPage
        Friend WithEvents DEMSurfaceDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents btnAddAssociatedSurface As System.Windows.Forms.Button
        Friend WithEvents btnAddToMap As System.Windows.Forms.Button
        Friend WithEvents btnDeleteAssociatedSurface As System.Windows.Forms.Button
        Friend WithEvents btnSettingsAssociatedSurface As System.Windows.Forms.Button
        Friend WithEvents pgeSurvey As System.Windows.Forms.TabPage
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents cboSingle As System.Windows.Forms.ComboBox
        Friend WithEvents btnBrowseMask As System.Windows.Forms.Button
        Friend WithEvents cmdAddToMap As System.Windows.Forms.Button
        Friend WithEvents cboIdentify As System.Windows.Forms.ComboBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents rdoMulti As System.Windows.Forms.RadioButton
        Friend WithEvents rdoSingle As System.Windows.Forms.RadioButton
        Friend WithEvents txtRasterPath As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents txtName As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents tabControl As System.Windows.Forms.TabControl
        Friend WithEvents txtProperties As System.Windows.Forms.TextBox
        Friend WithEvents SurveyTypesBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents SurveyTypes As SurveyTypes
        Friend WithEvents txtMask As System.Windows.Forms.TextBox
        Friend WithEvents btnCalculateError As System.Windows.Forms.Button
        Friend WithEvents txtFolder As System.Windows.Forms.TextBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents cmdDateTime As System.Windows.Forms.Button
        Friend WithEvents lblDatetime As System.Windows.Forms.Label
    End Class

End Namespace