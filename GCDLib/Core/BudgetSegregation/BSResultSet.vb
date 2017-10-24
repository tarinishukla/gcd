﻿
Namespace Core.BudgetSegregation

    Public Class BSResultSet

        Private m_DodProps As ChangeDetection.ChangeDetectionProperties
        Private m_MaskResults As Dictionary(Of String, MaskResult)

        Private m_pieCharts As BudgetSegregation.BudgetSegregationOutputsClass.PieChartOutputs
        Private m_dMaskOutputs As New Dictionary(Of String, BudgetSegregationOutputsClass.MaskOutputClass)
        Private m_sClassLegendPath As String
        Private m_sClassSummaryPath As String ' Class summary XML file that combines all budget summaries into one file.

        Public ReadOnly Property MaskResults As Dictionary(Of String, MaskResult)
            Get
                Return m_MaskResults
            End Get
        End Property

        Public Sub New(dodProps As ChangeDetection.ChangeDetectionProperties)
            m_DodProps = dodProps
        End Sub

    End Class

    Public Class MaskResult

        Private m_sMaskName As String
        Private m_nMaskValue As Integer
        Private m_changeStats As ChangeDetection.ChangeStats

        Public ReadOnly Property Name As String
            Get
                Return m_sMaskName
            End Get
        End Property

        Public ReadOnly Property MaskValue As Integer
            Get
                Return m_nMaskValue
            End Get
        End Property

        Public ReadOnly Property ChangeStats As ChangeDetection.ChangeStats
            Get
                Return m_changeStats
            End Get
        End Property

        Public Sub New(sMaskName As String, nMaskValue As Integer, chStats As ChangeDetection.ChangeStats)
            m_sMaskName = sMaskName
            m_nMaskValue = nMaskValue
            m_changeStats = chStats
        End Sub

    End Class

End Namespace