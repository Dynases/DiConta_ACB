Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_BalanceComprobacionSumasSaldos
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Private _numiAuxiliarDetalleModulo As Integer = 1
    Private _numiAuxiliarDetalleSucursal As Integer = 11

    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "reporte balance de comprobacion de sumas y saldos".ToUpper

        _prCargarComboAuxiliaresVariables(_numiAuxiliarDetalleModulo)
        _prCargarComboAuxiliaresVariablesSucursales(_numiAuxiliarDetalleSucursal)


        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
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
    Private Sub _prCargarReporte()
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

        Dim dt As DataTable = L_prCuentaReporteBalanceComprabacionSumasSaldos(gi_empresaNumi, tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"), numiModulo, numiSuc)

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_BalanceComprobacionSumasSaldos
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("fechaDesde", tbFechaDel.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("fechaHasta", tbFechaAl.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("titulo", gs_empresaDesc.ToUpper)
            objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)

            MReportViewer.ReportSource = objrep



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
        tbVariable.Enabled = tbFiltrar.Value

    End Sub

    Private Sub tbFiltrarSucursal_ValueChanged(sender As Object, e As EventArgs) Handles tbFiltrarSucursal.ValueChanged
        tbVariableSucursal.Enabled = tbFiltrarSucursal.Value

    End Sub
End Class