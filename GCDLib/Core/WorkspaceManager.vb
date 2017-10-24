﻿Imports System.IO
Imports System.Text

Namespace Core

    Public Class WorkspaceManager

        Private Shared m_diWorkspacePath As DirectoryInfo

        Public Shared ReadOnly Property WorkspacePath As String
            Get
                If TypeOf m_diWorkspacePath Is DirectoryInfo Then
                    Return m_diWorkspacePath.FullName
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public Shared Function GetTempRaster(sRootName As String) As String

            Return naru.os.File.GetNewSafeName(m_diWorkspacePath.FullName, sRootName, "tif").FullName

        End Function

        Public Shared Function GetTempShapeFile(sRootName As String) As String

            If String.IsNullOrEmpty(sRootName) Then
                Throw New ArgumentNullException("sRootName")
            End If

            Return naru.os.File.GetNewSafeName(m_diWorkspacePath.FullName, System.IO.Path.ChangeExtension(sRootName, ""), "shp").FullName

        End Function

        Public Shared Sub Initialize()

            Dim sPath As String = String.Empty
            If String.IsNullOrEmpty(My.Settings.TempWorkspace) OrElse Not System.IO.Directory.Exists(My.Settings.TempWorkspace) Then
                sPath = GetDefaultWorkspace(My.Resources.ApplicationNameShort)
            Else
                ' During AddIn startup, must set the workspace path on the workspace manager
                ' object. This bypasses validation. The workspace path will be validated
                ' (with UI warnings) during new/open project. For now, just set the workspace.
                sPath = My.Settings.TempWorkspace
            End If

            SetWorkspacePath(sPath)

        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearWorkspace()

            '' Only proceed if the workspace exists.
            'If Not TypeOf m_diWorkspacePath Is System.IO.DirectoryInfo OrElse Not m_diWorkspacePath.Exists Then
            '    Exit Sub
            'End If

            ''use datasetname to avoid fail on dataset enumeration
            ''TODO: Develop method to delete .mxd files

            'Dim datasetnames As New ArrayList()

            'Dim pWkSp As IWorkspace
            ''Dim pWkSpFactory As IWorkspaceFactory

            'Dim pEDName As IEnumDatasetName
            'Dim pDSName As IDatasetName
            'Dim pName As ESRI.ArcGIS.esriSystem.IName
            'Dim pDataset As IDataset

            ''get shapefile dataserts in folder
            'Dim pWkSpFactory As IWorkspaceFactory = GISCode.ArcMap.GetWorkspaceFactory(GISDataStructures.GISDataStorageTypes.ShapeFile)
            'pWkSp = pWkSpFactory.OpenFromFile(WorkspacePath, 0)
            'pEDName = pWkSp.DatasetNames(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureClass)
            'pDSName = pEDName.Next
            'Do Until pDSName Is Nothing
            '    datasetnames.Add(pDSName)
            '    pDSName = pEDName.Next
            'Loop
            'Runtime.InteropServices.Marshal.ReleaseComObject(pEDName)

            '' PGB - 2 June 2016 - TINs!
            ''get shapefile dataserts in folder
            'Dim pWkSpFactoryTIN As IWorkspaceFactory = GISCode.ArcMap.GetWorkspaceFactory(GISDataStructures.GISDataStorageTypes.TIN)
            'pWkSp = pWkSpFactoryTIN.OpenFromFile(WorkspacePath, 0)
            'pEDName = pWkSp.DatasetNames(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTTin)
            'pDSName = pEDName.Next
            'Do Until pDSName Is Nothing
            '    datasetnames.Add(pDSName)
            '    pDSName = pEDName.Next
            'Loop
            'Runtime.InteropServices.Marshal.ReleaseComObject(pEDName)

            ''Get raster datasets in folder
            'pWkSpFactory = GISCode.ArcMap.GetWorkspaceFactory(GISDataStructures.GISDataStorageTypes.RasterFile)
            'pWkSp = pWkSpFactory.OpenFromFile(WorkspacePath, 0)
            'pEDName = pWkSp.DatasetNames(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTRasterDataset)
            'pDSName = pEDName.Next
            'Do Until pDSName Is Nothing
            '    datasetnames.Add(pDSName)
            '    pDSName = pEDName.Next
            'Loop

            ''delete datasets
            'If datasetnames.Count > 0 Then
            '    For Each datasetname As IDatasetName In datasetnames
            '        If TypeOf datasetname Is ESRI.ArcGIS.esriSystem.IName Then
            '            pName = datasetname
            '            Try
            '                pDataset = pName.Open
            '                pDataset.Delete()
            '            Catch ex As Exception
            '                Debug.WriteLine("Could not delete dataset in temporary workspace: " & datasetname.Name)
            '            End Try
            '        End If
            '    Next
            'End If
            ''
            '' PGB 14 Mar 2012
            '' Starting to use more and more file geodatabases in the temporary workspace.
            '' delete these too.
            ''
            'Dim GP As New ESRI.ArcGIS.Geoprocessor.Geoprocessor
            'GP.SetEnvironmentValue("workspace", WorkspacePath)
            'Dim gplWorkspaces As IGpEnumList = GP.ListWorkspaces("", "")
            'Dim sWorkspace As String = gplWorkspaces.Next()

            'Dim DeleteTool As New ESRI.ArcGIS.DataManagementTools.Delete

            'Do While Not String.IsNullOrEmpty(sWorkspace)
            '    Try
            '        DeleteTool.in_data = sWorkspace
            '        GP.Execute(DeleteTool, Nothing)
            '    Catch ex As Exception
            '        Debug.WriteLine("Could not delete workspace" & sWorkspace)
            '    End Try
            '    sWorkspace = gplWorkspaces.Next()
            'Loop
        End Sub

        ''' <summary>
        ''' Get the default workspace
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDefaultWorkspace(sApplicationShort As String) As String

            If (String.IsNullOrEmpty(sApplicationShort)) Then
                Throw New ArgumentNullException("sApplicationShort", "Invalid default workspace parameter")
            End If

            Dim sDefault As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            sDefault = IO.Path.Combine(sDefault, sApplicationShort)
            If Not IO.Directory.Exists(sDefault) Then
                IO.Directory.CreateDirectory(sDefault)
            End If

            sDefault = IO.Path.Combine(sDefault, "TempWorkspace")
            If Not IO.Directory.Exists(sDefault) Then
                IO.Directory.CreateDirectory(sDefault)
            End If

            Return sDefault

        End Function

        Public Shared Function ValidateWorkspace(sWorkspacePath As String) As Boolean

            ' This string will remain empty unless there is a problem, in which
            ' case it will contain the relevant message to show the user.
            Dim sWarningMessage As String = String.Empty
            Dim sFixMessage As String = String.Format("Open the {0} Options to assign a valid temporary workspace path.", My.Resources.ApplicationNameShort)

            If String.IsNullOrEmpty(sWorkspacePath) Then
                sWarningMessage = String.Format("The {0} temporary workspace path cannot be empty.{1}", My.Resources.ApplicationNameShort, sFixMessage)
            Else
                If System.IO.Directory.Exists(sWorkspacePath) Then
                    If My.Settings.StartUpWorkspaceWarning Then
                        If System.Text.RegularExpressions.Regex.IsMatch(sWorkspacePath, "[ .]") Then
                            ' Show the message box that asks the user whether they want to proceed.
                            ' This message box also controls whether they are warned again.
                            Dim frm As New UI.Options.frmMessageBoxWithReminder(sWorkspacePath)
                            Return frm.ShowDialog = System.Windows.Forms.DialogResult.Yes
                        End If
                    End If
                Else
                    sWarningMessage = String.Format("The {0} temporary workspace path does not exist.{1}", My.Resources.ApplicationNameLong, sFixMessage)
                End If

            End If

            If Not String.IsNullOrEmpty(sWarningMessage) Then
                System.Windows.Forms.MessageBox.Show(sWarningMessage, String.Format("Invalid {0} Temporary Workspace", My.Resources.ApplicationNameShort), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            End If

            Return String.IsNullOrEmpty(sWarningMessage)

        End Function

        ''' <summary>
        ''' Set the temp workspace to a directory
        ''' </summary>
        ''' <param name="sPath"></param>
        ''' <returns></returns>
        ''' <remarks>PGB Apr 2015 - Throw exception rather than debug assert when the temp workspace does not exist.
        ''' Discovered on Steve Fortneys laptop.</remarks>
        Public Shared Function SetWorkspacePath(sPath As String) As String

            If String.IsNullOrEmpty(sPath) OrElse Not System.IO.Directory.Exists(sPath) Then
                Dim ex As New Exception("The specified temp workspace directory is null or does not exist. Go to GCD Options and set the temp workspace to a valid folder.")
                ex.Data("Path") = sPath
                Throw ex
            End If

            m_diWorkspacePath = New System.IO.DirectoryInfo(sPath)
            My.Settings.TempWorkspace = sPath
            My.Settings.Save()

            Return m_diWorkspacePath.FullName

        End Function

        Public Shared Function SetWorkspacePathDefault() As String

            Dim sPath As String = GetDefaultWorkspace(My.Resources.ApplicationNameShort)
            Return SetWorkspacePath(sPath)

        End Function

    End Class

End Namespace