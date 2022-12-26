Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_EstadoCuentasResultadosV2
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private _numiAuxiliarDetalleModulo As Integer = 1
    Private _numiAuxiliarDetalleSucursal As Integer = 11

    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "reporte estado cuentas de resultados formato 2".ToUpper
        '_prCargarComboAuxiliares()
        _prCargarComboAuxiliaresVariables(_numiAuxiliarDetalleModulo)
        _prCargarComboAuxiliaresVariablesSucursales(_numiAuxiliarDetalleSucursal)

        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    End Sub
    'Private Sub _prCargarComboAuxiliares()
    '    Dim dt As New DataTable
    '    dt = L_prAuxiliarGeneral()

    '    With tbAuxiliar
    '        .DropDownList.Columns.Clear()

    '        .DropDownList.Columns.Add("ccnumi").Width = 70
    '        .DropDownList.Columns("ccnumi").Caption = "COD"

    '        .DropDownList.Columns.Add("ccdesc").Width = 200
    '        .DropDownList.Columns("ccdesc").Caption = "DESCRIPCION"

    '        .ValueMember = "ccnumi"
    '        .DisplayMember = "ccdesc"
    '        .DataSource = dt
    '        .Refresh()
    '    End With
    'End Sub
    Private Sub _prCargarComboAuxiliaresVariables(numi As String)
        Dim dt As New DataTable
        dt = L_prAuxiliarDetalleGeneral(numi)

        With tbVariable
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cdnumi").Width = 70
            .DropDownList.Columns("cdnumi").Caption = "COD"

            .DropDownList.Columns.Add("cddesc").Width = 200
            .DropDownList.Columns("cddesc").Caption = "DESCRIPCION"

            .ValueMember = "cdnumi"
            .DisplayMember = "cddesc"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prCargarComboAuxiliaresVariablesSucursales(numi As String)
        Dim dt As New DataTable
        dt = L_prAuxiliarDetalleGeneral(numi)

        With tbVariableSucursal
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cdnumi").Width = 70
            .DropDownList.Columns("cdnumi").Caption = "COD"

            .DropDownList.Columns.Add("cddesc").Width = 200
            .DropDownList.Columns("cddesc").Caption = "DESCRIPCION"

            .ValueMember = "cdnumi"
            .DisplayMember = "cddesc"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prCargarReporte()
        Dim dt As DataTable = New DataTable
        If tbFiltrar.Value = True Or tbFiltrarSucursal.Value = True Then
            Dim numiModulo As String = "-1"
            Dim numiSuc As String = "-1"

            If tbFiltrar.Value = True Then
                If tbVariable.SelectedIndex >= 0 Then
                    numiModulo = tbVariable.Value
                Else
                    ToastNotification.Show(Me, "debe seleccionar un auxiliar modulo..!!!".ToUpper,
                                           My.Resources.WARNING, 2000,
                                           eToastGlowColor.Blue,
                                           eToastPosition.BottomLeft)
                    Return
                End If
            End If

            If tbFiltrarSucursal.Value = True Then
                If tbVariableSucursal.SelectedIndex >= 0 Then
                    numiSuc = tbVariableSucursal.Value
                Else
                    ToastNotification.Show(Me, "debe seleccionar un auxiliar sucursal..!!!".ToUpper,
                                           My.Resources.WARNING, 2000,
                                           eToastGlowColor.Blue,
                                           eToastPosition.BottomLeft)
                    Return
                End If
            End If

            'llamo al reporte
            dt = L_prCuentaReporteEstadoCuentasResultadosV2PorAuxiliar(gi_empresaNumi, tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"), numiModulo, numiSuc)

        Else
            dt = L_prCuentaReporteEstadoCuentasResultadosV2(gi_empresaNumi, tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"))

        End If

        If (dt.Rows.Count > 0) Then
            Try
                Dim objrep As New R_EstadoCuentasResultadoV2
                objrep.SetDataSource(dt)

                objrep.SetParameterValue("fechaDesde", tbFechaDel.Value.ToString("dd/MM/yyyy"))
                objrep.SetParameterValue("fechaHasta", tbFechaAl.Value.ToString("dd/MM/yyyy"))
                objrep.SetParameterValue("titulo", gs_empresaDesc.ToUpper)
                objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)

                MReportViewer.ReportSource = objrep


                MReportViewer.Show()
                MReportViewer.BringToFront()
            Catch ex As Exception

            End Try

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
        tbVariable.Enabled = tbFiltrar.Value

    End Sub

    Private Sub tbVariable_ValueChanged(sender As Object, e As EventArgs) Handles tbVariable.ValueChanged

    End Sub

    Private Sub tbFiltrarSucursal_ValueChanged(sender As Object, e As EventArgs) Handles tbFiltrarSucursal.ValueChanged
        tbVariableSucursal.Enabled = tbFiltrarSucursal.Value

    End Sub

    'Private Sub tbAuxiliar_ValueChanged(sender As Object, e As EventArgs)
    '    If tbAuxiliar.SelectedIndex >= 0 Then
    '        _prCargarComboAuxiliaresVariables(tbAuxiliar.Value)
    '    Else
    '        tbVariable.DataSource = Nothing

    '    End If
    'End Sub
End Class