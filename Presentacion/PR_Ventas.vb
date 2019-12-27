Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_Ventas
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private _numiAuxiliarDetalleModulo As Integer = 1

    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "reporte de ventas".ToUpper
        _prCargarComboSector(tbSector)


        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    End Sub

    Private Sub _prCargarComboSector(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prlistarCategoriasActivos()

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cenum").Width = 60
            .DropDownList.Columns("cenum").Caption = "COD"
            .DropDownList.Columns.Add("cedesc1").Width = 500
            .DropDownList.Columns("cedesc1").Caption = "CATEGORIA"
            .ValueMember = "cenum"
            .DisplayMember = "cedesc1"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        End If
    End Sub

    Private Sub _prCargarReporte()

        Dim dt As DataTable = New DataTable
        If tbFiltrar.Value = True Then
            If tbSector.SelectedIndex >= 0 Then
                dt = L_prVentaReporteGeneral(tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"), tbSector.Value)
            Else
                ToastNotification.Show(Me, "debe seleccionar un sector..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
                Return
            End If
        Else
            dt = L_prVentaReporteGeneral(tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"), "-1")

        End If

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_Ventas
            objrep.SetDataSource(dt)
            MReportViewer.ReportSource = objrep

            objrep.SetParameterValue("fechaDesde", tbFechaDel.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("fechaHasta", tbFechaAl.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
            'objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)

            MReportViewer.Show()
            MReportViewer.BringToFront()
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If

    End Sub


    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        _tab.Close()
    End Sub

    Private Sub PR_ListasCertiTeoPrac2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub SwitchButton1_ValueChanged(sender As Object, e As EventArgs) Handles tbFiltrar.ValueChanged
        'tbAuxiliar.Enabled = tbFiltrar.Value
        tbSector.Enabled = tbFiltrar.Value

    End Sub

    'Private Sub tbAuxiliar_ValueChanged(sender As Object, e As EventArgs)
    '    If tbAuxiliar.SelectedIndex >= 0 Then
    '        _prCargarComboAuxiliaresVariables(tbAuxiliar.Value)
    '    Else
    '        tbVariable.DataSource = Nothing

    '    End If
    'End Sub
End Class