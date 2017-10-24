﻿Imports GCDLib.Core

Namespace UI.SurveyLibrary

    Public Class frmAssocSurfaceProperties

        Private Enum AssociatedSurfaceMethods
            Browse
            PointDensity
            SlopePercent
            SlopeDegree
            Roughness
            PointQuality3D
            InterpolationError
        End Enum

#Region "Members"
        Private m_frmPointDensity As frmPointDensity
        Private m_ImportForm As frmImportRaster
        Private m_SurfaceRoughnessForm As frmSurfaceRoughness
        '
        ' This method tracks whether the surface is from an existing
        ' raster or to be generated by GCD from slope or point density.
        ' Note that browsing to an existing point density or slope raster
        ' is considered "Browsing".
        '
        Private m_eMethod As AssociatedSurfaceMethods

#End Region

#Region "Methods"

        Private ReadOnly Property AssociatedSurfaceRow As ProjectDS.AssociatedSurfaceRow
            Get

                'Dim dr As DataRowView = AssociatedSurfaceBindingSource.Current
                'Dim assocRow As ProjectDS.AssociatedSurfaceRow = dr.Row
                'Return assocRow
            End Get
        End Property


        Public Sub New(ByVal nSurveyID As Integer, Optional ByVal nSurfaceID As Integer = 0)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            m_eMethod = AssociatedSurfaceMethods.Browse

            ' TODO: This form used to use binding sources. Requires refactoring
            Throw New Exception("Not implement. Need to remove old binding source code")
            '
            ' Bind to the run time datasource
            '
            'AssociatedSurfaceBindingSource.DataSource = GCDProject.ProjectManagerBase.ds
            'Debug.Assert(nSurveyID > 0, "The survey ID must be greater than zero")
            ''m_nDEMSurveyID = nSurveyID
            'If nSurfaceID = 0 Then
            '    ' assign the DEM Survey ID to this surface.
            '    'Dim demRow As ProjectDS.DEMSurveyRow = GCD.GCDProject.ProjectManager.ds.DEMSurvey.FindByDEMSurveyID(nSurveyID)
            '    Dim cr As DataRowView = AssociatedSurfaceBindingSource.AddNew
            '    DirectCast(cr.Row, ProjectDS.AssociatedSurfaceRow).DEMSurveyID = nSurveyID
            'Else
            '    Dim nIndex As Integer = AssociatedSurfaceBindingSource.Find("AssociatedSurfaceID", nSurfaceID)
            '    AssociatedSurfaceBindingSource.Position = nIndex
            '    DirectCast(AssociatedSurfaceBindingSource.Current, DataRowView).BeginEdit()
            'End If

        End Sub
#End Region

#Region "Events"

        Private Sub SurfacePropertiesForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ttpTooltip.SetToolTip(btnCancel, "Cancel and close this form.")
            ttpTooltip.SetToolTip(btnHelp, My.Resources.ttpHelp)
            ttpTooltip.SetToolTip(cboType, "Associated surface type. Use this to define what physical phenomenon this associated surface represents.")
            ttpTooltip.SetToolTip(btnBrowse, "Browse and choose an existing raster that represents the associated surface.")
            ttpTooltip.SetToolTip(btnSlopePercent, "Calculate a slope raster - in percent - from the DEM survey.")
            ttpTooltip.SetToolTip(btnSlopeDegree, "Calculate a slope raster - in degrees - from the DEM survey.")
            ttpTooltip.SetToolTip(btnDensity, "Calculate a point density raster from the DEM Survey. You will be presented with options for the point density calculation.")
            ttpTooltip.SetToolTip(btnRoughness, "Calculate a surface roughness raster from space delimited point cloud file.")

            cboType.Items.Clear()
            cboType.Items.Add(New naru.db.NamedObject(AssociatedSurfaceMethods.PointDensity, "Point Density"))
            cboType.Items.Add(New naru.db.NamedObject(AssociatedSurfaceMethods.SlopeDegree, "Slope (Degrees)"))
            cboType.Items.Add(New naru.db.NamedObject(AssociatedSurfaceMethods.SlopePercent, "Slope (Percent)"))
            cboType.Items.Add(New naru.db.NamedObject(AssociatedSurfaceMethods.Roughness, "Roughness"))
            cboType.Items.Add(New naru.db.NamedObject(AssociatedSurfaceMethods.PointQuality3D, "3D Point Quality"))
            cboType.Items.Add(New naru.db.NamedObject(AssociatedSurfaceMethods.InterpolationError, "Interpolation Error"))
            cboType.Items.Add(New naru.db.NamedObject(AssociatedSurfaceMethods.Browse, "Unknown"))

            cboType.SelectedIndex = 0
            Dim dr As DataRowView '= AssociatedSurfaceBindingSource.Current
            If Not dr.IsNew Then
                Dim assocRow As ProjectDS.AssociatedSurfaceRow = dr.Row
                txtName.Text = assocRow.Name
                If Not assocRow.IsOriginalSourceNull Then
                    txtOriginalRaster.Text = assocRow.OriginalSource
                End If
                txtProjectRaster.Text = assocRow.Source
                txtProjectRaster.ReadOnly = True
                btnSlopePercent.Visible = False
                btnDensity.Visible = False
                btnBrowse.Visible = False
                btnSlopeDegree.Visible = False
                btnRoughness.Visible = False
                txtOriginalRaster.Width = txtName.Width

                For i As Integer = 0 To cboType.Items.Count - 1
                    If String.Compare(cboType.Items(i).ToString, assocRow.Type, True) = 0 Then
                        cboType.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboType.Focus()
            End If

        End Sub

        Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

            If Not ValidateForm() Then
                Me.DialogResult = System.Windows.Forms.DialogResult.None
                Exit Sub
            End If

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim dr As DataRowView '= AssociatedSurfaceBindingSource.Current
            Dim bRasterSavedOK As Boolean = True
            If dr.IsNew Then
                Try
                    ImportRaster()
                Catch ex As Exception
                    bRasterSavedOK = False
                    naru.error.ExceptionUI.HandleException(ex, "The GCD project associated surface raster was not created successfully. The GCD project will not be updated. You should check the GCD project folder to ensure that no remnants of the raster remain.")
                End Try
            End If

            If bRasterSavedOK Then
                Try
                    ' Now try and save the GCD project attribute information.
                    Dim assocRow As ProjectDS.AssociatedSurfaceRow = dr.Row

                    'TODO Finish this code that updates the raster symbology if the type is adjusted via the viewing the settings of an associated surface

                    'Added code to check if the associated surface type has been changed, if it has then update the symbology in the raster layer - Hensleigh 12/9/2013
                    'Dim eType As ArcMap.RasterLayerTypes = GISCode.GCD.GCDArcMapManager.GetAssociatedSurfaceType(assocRow)
                    'Dim symbologyFile As String = GISCode.GCD.GCDArcMapManager.GetSymbologyLayerFile(eType)
                    'Dim sSource As String = dr.Item("Source")
                    'Dim gRaster As New GCDConsoleLib.Raster(sSource)
                    'If String.Compare(assocRow.Type, cboType.Text, False) = 0 Then
                    '    gRaster.ApplySymbology(symbologyFile)
                    '    Dim pMXDoc As ESRI.ArcGIS.ArcMapUI.IMxDocument = m_pArcMap.Document
                    '    Dim pMap As ESRI.ArcGIS.Carto.IMap = pMXDoc.FocusMap
                    '    pMXDoc.UpdateContents()
                    '    pMXDoc.ActiveView.Refresh()
                    '    pMXDoc.CurrentContentsView.Refresh(Nothing)
                    'End If

                    assocRow.Type = cboType.Text
                    assocRow.Name = txtName.Text
                    If dr.IsNew Then
                        assocRow.Source = Core.GCDProject.ProjectManager.GetRelativePath(txtProjectRaster.Text)
                        assocRow.OriginalSource = txtProjectRaster.Text
                    End If
                    'AssociatedSurfaceBindingSource.EndEdit()
                    GCDProject.ProjectManagerBase.save()

                    If My.Settings.AddOutputLayersToMap Then
                        ' TODO: implement this up in AddIn
                        Throw New Exception("not implemented")
                        'GCDProject.ProjectManagerUI.ArcMapManager.AddAssociatedSurface(assocRow)
                    End If

                Catch ex As Exception
                    naru.error.ExceptionUI.HandleException(ex, "The GCD attribute information failed to save to the GCD project file. The associated surface raster still exists.")
                    DialogResult = System.Windows.Forms.DialogResult.None
                End Try
            Else
                ' AssociatedSurfaceBindingSource.CancelEdit()
                Me.DialogResult = System.Windows.Forms.DialogResult.None
            End If

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        End Sub

        Private Sub ImportRaster()

            Try
                ' Make sure that the destination folder exists where the associated surface will be output
                Dim sWorkspacePath As String = System.IO.Path.GetDirectoryName(txtProjectRaster.Text)
                IO.Directory.CreateDirectory(sWorkspacePath)

                ' Create the slope surface or point density rasters
                Select Case m_eMethod
                    Case AssociatedSurfaceMethods.SlopeDegree,
                    AssociatedSurfaceMethods.SlopePercent,
                    AssociatedSurfaceMethods.PointDensity,
                    AssociatedSurfaceMethods.Roughness

                        Dim gDEMRaster As GCDConsoleLib.Raster = GetDEMSurveyRaster()

                        Select Case m_eMethod
                            Case AssociatedSurfaceMethods.SlopeDegree
                                External.CreateSlope(gDEMRaster.FilePath, txtProjectRaster.Text, External.RasterManager.SlopeTypes.Degrees, GCDProject.ProjectManagerBase.GCDNARCError.ErrorString)

                            Case AssociatedSurfaceMethods.SlopePercent
                                External.CreateSlope(gDEMRaster.FilePath, txtProjectRaster.Text, External.RasterManager.SlopeTypes.Percent, GCDProject.ProjectManagerBase.GCDNARCError.ErrorString)

                            Case AssociatedSurfaceMethods.PointDensity
                                Dim sTemp As String = WorkspaceManager.GetTempRaster("PDensity.tif")
                                ' TODO: Call ArcGIS point density tool
                                Throw New Exception("Not implemented")
                                'GP.SpatialAnalyst.PointDensity(m_frmPointDensity.ucPointCloud.SelectedItem, gDEMRaster, sTemp, m_frmPointDensity.Neighborhood, "SQUARE_MAP_UNITS")

                                ' Need to mask the raster so that it has the same NoData extent as the DEM
                                Throw New Exception("not implemented")
                                'GP.SpatialAnalyst.Con(gDEMRaster.FullPath, sTemp, "", txtProjectRaster.Text, """VALUE"" > 0")

                            Case AssociatedSurfaceMethods.Roughness
                                m_SurfaceRoughnessForm.CalculateRoughness(txtProjectRaster.Text, gDEMRaster)

                        End Select

                        ' Build raster pyramids
                        If RasterPyramidManager.AutomaticallyBuildPyramids(RasterPyramidManager.PyramidRasterTypes.AssociatedSurfaces) Then
                            RasterPyramidManager.PerformRasterPyramids(RasterPyramidManager.PyramidRasterTypes.AssociatedSurfaces, txtProjectRaster.Text)
                        End If

                    Case Else
                        m_ImportForm.ProcessRaster()

                End Select

            Catch ex As Exception

                ' Something went wrong. Check if the raster exists and safely attempt to clean it up if it does.
                If GCDConsoleLib.GISDataset.FileExists(txtProjectRaster.Text) Then
                    Try
                        External.Delete(txtProjectRaster.Text, GCDProject.ProjectManagerBase.GCDNARCError.ErrorString)
                    Catch ex2 As Exception

                    End Try
                End If

                If ex.Message.ToString.ToLower.Contains("DEMSurfaceName") Then
                    MsgBox("The name must be unique.", MsgBoxStyle.Exclamation, My.Resources.ApplicationNameLong)
                    Me.DialogResult = System.Windows.Forms.DialogResult.None
                Else
                    naru.error.ExceptionUI.HandleException(ex, "An error occured while trying to save the information.")
                End If

                Me.DialogResult = System.Windows.Forms.DialogResult.None
            End Try
        End Sub

        Private Function ValidateForm() As Boolean

            If String.IsNullOrEmpty(txtName.Text) Then
                MsgBox("Please provide a name for the associated surface.", MsgBoxStyle.Information, My.Resources.ApplicationNameLong)
                txtName.Focus()
                Return False
            Else
                If Not IsNameUniqueForSurvey() Then
                    MsgBox("The name '" & txtName.Text & "' is already in use by another associated surface within this survey. Please choose a unique name.", MsgBoxStyle.Exclamation, My.Resources.ApplicationNameLong)
                    txtName.Focus()
                    Return False
                End If
            End If

            If String.IsNullOrEmpty(txtProjectRaster.Text) Then
                If m_eMethod = AssociatedSurfaceMethods.Browse Then
                    MsgBox("You must either browse and select an existing raster for this associated surface, or choose to generate a slope or point density raster from the DEM Survey raster.", MsgBoxStyle.Information, My.Resources.ApplicationNameLong)
                    Return False
                End If
            Else
                Dim dr As DataRowView '= AssociatedSurfaceBindingSource.Current
                If dr.IsNew Then
                    If GCDConsoleLib.GISDataset.FileExists(txtProjectRaster.Text) Then
                        MsgBox("The associated surface project raster path already exists. Changing the name of the associated surface will change the raster path.", MsgBoxStyle.Information, My.Resources.ApplicationNameLong)
                        Return False
                    End If
                End If
            End If

            If cboType.SelectedItem Is Nothing Then
                MsgBox("Please select an error type to continue.", MsgBoxStyle.Exclamation, My.Resources.ApplicationNameLong)
                cboType.Focus()
                Return False
            End If

            Return True

        End Function

        Private Function IsNameUniqueForSurvey() As Boolean

            Dim dr As DataRowView '= AssociatedSurfaceBindingSource.Current
            Dim assocRow As ProjectDS.AssociatedSurfaceRow = dr.Row
            Dim rows As ProjectDS.AssociatedSurfaceRow() = GCDProject.ProjectManagerBase.ds.AssociatedSurface.Select("DEMSurveyID = " & assocRow.DEMSurveyID)
            If Not rows Is Nothing Then
                For Each r As ProjectDS.AssociatedSurfaceRow In rows
                    If assocRow.AssociatedSurfaceID <> r.AssociatedSurfaceID Then
                        If String.Compare(r.Name, txtName.Text, True) = 0 AndAlso r.AssociatedSurfaceID <> assocRow.AssociatedSurfaceID Then
                            Return False
                        End If
                    End If
                Next
            End If

            Return True

        End Function

        Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click

            If Not TypeOf m_ImportForm Is frmImportRaster Then
                Dim gAssocRow As ProjectDS.AssociatedSurfaceRow '= DirectCast(AssociatedSurfaceBindingSource.Current, DataRowView).Row
                Dim gDEMSurveyRaster As New GCDConsoleLib.Raster(GCDProject.ProjectManagerBase.GetAbsolutePath(gAssocRow.DEMSurveyRow.Source))
                m_ImportForm = New frmImportRaster(gDEMSurveyRaster, gAssocRow.DEMSurveyRow, frmImportRaster.ImportRasterPurposes.AssociatedSurface, "Associated Surface")
            End If

            m_ImportForm.txtName.Text = txtName.Text
            If m_ImportForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                txtName.Text = m_ImportForm.txtName.Text
                txtOriginalRaster.Text = m_ImportForm.ucRaster.SelectedItem.FilePath
            End If

        End Sub

#End Region

        Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
            'AssociatedSurfaceBindingSource.CancelEdit()
        End Sub

        Private Sub btnSlope_Click(sender As System.Object, e As System.EventArgs) Handles btnSlopePercent.Click

            Dim sSlopeName As String = "Slope Percent Rise"
            m_eMethod = AssociatedSurfaceMethods.SlopePercent
            If String.IsNullOrEmpty(txtName.Text) Then
                txtName.Text = sSlopeName
            End If
            '
            ' Select the appropriate type in the dropdown box
            '
            For i As Integer = 0 To cboType.Items.Count - 1
                If String.Compare(cboType.Items(i).ToString, sSlopeName, True) = 0 Then
                    cboType.SelectedIndex = i
                    Exit For
                End If
            Next

            txtOriginalRaster.Text = GCDProject.ProjectManagerBase.GetAbsolutePath(AssociatedSurfaceRow.DEMSurveyRow.Source)

            MsgBox("The slope raster will be generated after you click OK.", MsgBoxStyle.Information, My.Resources.ApplicationNameLong)

        End Sub

        Private Sub btnSlopeDegree_Click(sender As System.Object, e As System.EventArgs) Handles btnSlopeDegree.Click

            Dim sSlopeName As String = "Slope Degrees"
            m_eMethod = AssociatedSurfaceMethods.SlopeDegree
            If String.IsNullOrEmpty(txtName.Text) Then
                txtName.Text = sSlopeName
            End If
            '
            ' Select the appropriate type in the dropdown box
            '
            For i As Integer = 0 To cboType.Items.Count - 1
                If String.Compare(cboType.Items(i).ToString, sSlopeName, True) = 0 Then
                    cboType.SelectedIndex = i
                    Exit For
                End If
            Next

            txtOriginalRaster.Text = GCDProject.ProjectManagerBase.GetAbsolutePath(AssociatedSurfaceRow.DEMSurveyRow.Source)

            MsgBox("The slope raster will be generated after you click OK.", MsgBoxStyle.Information, My.Resources.ApplicationNameLong)

        End Sub

        Private Function GetDEMSurveyRaster() As GCDConsoleLib.Raster

            Dim gResult As GCDConsoleLib.Raster = Nothing
            Dim dr As DataRowView ' = AssociatedSurfaceBindingSource.Current
            Dim assocRow As ProjectDS.AssociatedSurfaceRow = dr.Row
            Dim demRow As ProjectDS.DEMSurveyRow = assocRow.DEMSurveyRow
            If GCDConsoleLib.GISDataset.FileExists(GCDProject.ProjectManagerBase.GetAbsolutePath(demRow.Source)) Then
                gResult = New GCDConsoleLib.Raster(GCDProject.ProjectManagerBase.GetAbsolutePath(demRow.Source))
            Else
                Dim ex As New Exception("The DEM Survey raster does not exist.")
                ex.Data.Add("DEM Survey Raster Path", demRow.Source)
                Throw ex
            End If
            Return gResult

        End Function

        Private Sub btnDensity_Click(sender As System.Object, e As System.EventArgs) Handles btnDensity.Click

            If m_frmPointDensity Is Nothing Then
                Dim gDEMSurveyRaster As GCDConsoleLib.Raster = GetDEMSurveyRaster()
                m_frmPointDensity = New frmPointDensity(gDEMSurveyRaster.VerticalUnits)
            End If

            If m_frmPointDensity.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                If String.IsNullOrEmpty(txtName.Text) Then
                    txtName.Text = "PDensity"
                End If
                m_eMethod = AssociatedSurfaceMethods.PointDensity

                'Dim sOutputRasterPath As String = GISCode.ChangeDetection.OutputManager.GetAssociatedSurfaceCopyPath(ProjectManager.ds.DEMSurvey.FindByDEMSurveyID(m_nDEMSurveyID).Name, txtName.Text, txtName.Text)
                'sOutputRasterPath = IO.Path.ChangeExtension(sOutputRasterPath, GetDefaultRasterExtension)
                'txtSource.Text = FileSystem.GetNewSafeFileName(sOutputRasterPath)
                '
                ' Select the appropriate type in the dropdown box
                '
                For i As Integer = 0 To cboType.Items.Count - 1
                    If String.Compare(cboType.Items(i).ToString, "Point Density", True) = 0 Then
                        cboType.SelectedIndex = i
                        Exit For
                    End If
                Next

                txtOriginalRaster.Text = m_frmPointDensity.ucPointCloud.SelectedItem.FilePath
            End If
        End Sub

        Private Function CalculatePointDensity(sDEM As String, sPointCloud As String, fSampleDistance As Double, sOutputRaster As String, gReferenceRaster As GCDConsoleLib.Raster) As GCDConsoleLib.Raster

            Dim gResult As GCDConsoleLib.Raster = Nothing
            Try

            Catch ex As Exception
                Dim ex2 As New Exception("Error calculating point density raster.", ex)
                ex2.Data.Add("DEM", sDEM)
                ex2.Data.Add("Point Cloud", sPointCloud)
                ex2.Data.Add("Sample Distance", fSampleDistance)
                ex2.Data.Add("Output raster", sOutputRaster)
                If TypeOf gReferenceRaster Is GCDConsoleLib.Raster Then
                    ex2.Data.Add("Reference raster", gReferenceRaster.FilePath)
                End If
                Throw ex2
            End Try

            Return gResult

        End Function

        Private Sub txtName_TextChanged(sender As Object, e As System.EventArgs) Handles txtName.TextChanged

            Dim dr As DataRowView ' = AssociatedSurfaceBindingSource.Current
            If dr.IsNew Then
                Dim assocRow As ProjectDS.AssociatedSurfaceRow = dr.Row
                txtProjectRaster.Text = Core.GCDProject.ProjectManager.OutputManager.AssociatedSurfaceRasterPath(assocRow.DEMSurveyRow.Name, txtName.Text)
            End If
        End Sub

        Private Sub btnRoughness_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRoughness.Click

            If m_SurfaceRoughnessForm Is Nothing Then
                Dim gDEMSurveyRaster As GCDConsoleLib.Raster = GetDEMSurveyRaster()
                Dim dReferenceResolution As Double = Math.Abs(gDEMSurveyRaster.Extent.CellWidth)
                m_SurfaceRoughnessForm = New frmSurfaceRoughness(dReferenceResolution)
            End If

            If m_SurfaceRoughnessForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                If String.IsNullOrEmpty(txtName.Text) Then
                    txtName.Text = "Roughness"
                End If
                m_eMethod = AssociatedSurfaceMethods.Roughness

                ' Select the appropriate type in the dropdown box
                '
                For i As Integer = 0 To cboType.Items.Count - 1
                    If String.Compare(cboType.Items(i).ToString, "Roughness", True) = 0 Then
                        cboType.SelectedIndex = i
                        Exit For
                    End If
                Next
                Try
                    txtOriginalRaster.Text = m_SurfaceRoughnessForm.ucToPCAT_Inputs.txtBox_RawPointCloudFile.Text
                Catch ex As Exception
                    naru.error.ExceptionUI.HandleException(ex)
                End Try
            End If

        End Sub

        Private Sub btnHelp_Click(sender As System.Object, e As System.EventArgs) Handles btnHelp.Click
            Process.Start(My.Resources.HelpBaseURL & "gcd-command-reference/gcd-project-explorer/d-dem-context-menu/iv-add-associated-surface")
        End Sub
    End Class

End Namespace