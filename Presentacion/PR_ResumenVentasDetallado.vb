Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica

Public Class PR_ResumenVentasDetallado
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        _prCargarComboAlmacen(cbSucursal)
        _prCargarComboSector(cbsector)
        '_prCargarComboServicio(cbServicio)
        Me.Text = "reporte resumen de ventas".ToUpper

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

    Private Sub _prCargarComboAlmacen(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable

        dt = L_fnListarAlmacenQueTenganDosificacion()
        If (dt.Columns.Count <= 0) Then
            Dim info As New TaskDialogInfo("INFORMACION", eTaskDialogIcon.Information2, "Error de Conexion con el Servidor. Desea Intentar Nuevamente?." + vbLf + "Si el error persite comuniquese con su administrador de sistemas", eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.DarkBlue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                _prCargarComboAlmacen(mCombo)
            Else
                Return
            End If
        End If

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cod").Width = 60
            .DropDownList.Columns("cod").Caption = "COD"
            .DropDownList.Columns.Add("desc").Width = 500
            .DropDownList.Columns("desc").Caption = "ALMACEN"
            .ValueMember = "cod"
            .DisplayMember = "desc"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        End If
    End Sub
    Private Sub _prCargarComboServicio(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, numiSector As String)
        Dim dt As New DataTable

        dt = L_prTraerServ(numiSector)
        If (dt.Columns.Count <= 0) Then
            Dim info As New TaskDialogInfo("INFORMACION", eTaskDialogIcon.Information2, "Error de Conexion con el Servidor. Desea Intentar Nuevamente?." + vbLf + "Si el error persite comuniquese con su administrador de sistemas", eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.DarkBlue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                _prCargarComboAlmacen(mCombo)
            Else
                Return
            End If
        End If

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ednumi").Width = 60
            .DropDownList.Columns("ednumi").Caption = "COD"
            .DropDownList.Columns.Add("eddesc").Width = 500
            .DropDownList.Columns("eddesc").Caption = "SERVICIO"
            .ValueMember = "ednumi"
            .DisplayMember = "eddesc"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        End If
    End Sub


    Private Sub _prCargarReporte()
        Dim dt As New DataTable
        Dim dt1 As DataTable = New DataTable
        '= L_prTraerReporteCierreTipo(tbfec1.Value.ToString("yyyy/MM/dd"), tbfec2.Value.ToString("yyyy/MM/dd"), cbSucursal.Value, cbsector.Value.ToString)
        'If cbsector.SelectedIndex = -1 Then
        '    ToastNotification.Show(Me, "seleccione un sector..!!!".ToUpper,
        '                         My.Resources.WARNING, 2000,
        '                             eToastGlowColor.Blue,
        '                             eToastPosition.BottomLeft)
        '    MReportViewer.ReportSource = Nothing
        '    Exit Sub
        'End If
        If tbFiltrar.Value = True And swFiltroServ.Value = True Then
            dt = L_prTraerReporteVentasDetallado(tbfec1.Value.ToString("yyyy/MM/dd"), tbfec2.Value.ToString("yyyy/MM/dd"), cbSucursal.Value, cbsector.Value, cbServicio.Value)
        Else
            If tbFiltrar.Value = False And swFiltroServ.Value = True Then
                dt = L_prTraerReporteVentasDetalladoServ(tbfec1.Value.ToString("yyyy/MM/dd"), tbfec2.Value.ToString("yyyy/MM/dd"), cbSucursal.Value, cbServicio.Value)
            Else
                If tbFiltrar.Value = True And swFiltroServ.Value = False Then
                    dt = L_prTraerReporteVentasDetallado(tbfec1.Value.ToString("yyyy/MM/dd"), tbfec2.Value.ToString("yyyy/MM/dd"), cbSucursal.Value, cbsector.Value)
                    dt1 = L_prTraerReporteCierreTipo(tbfec1.Value.ToString("yyyy/MM/dd"), tbfec2.Value.ToString("yyyy/MM/dd"), cbSucursal.Value, cbsector.Value.ToString)
                Else
                    dt = L_prTraerReporteVentasDetallado(tbfec1.Value.ToString("yyyy/MM/dd"), tbfec2.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                    dt1 = L_prTraerReporteCierreTipoVenta(tbfec1.Value.ToString("yyyy/MM/dd"), tbfec2.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                End If
            End If
        End If

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_VentasDetallado
            objrep.Subreports.Item("R_TipoVenta.rpt").SetDataSource(dt1)
            objrep.SetDataSource(dt)

            objrep.SetParameterValue("fecha1", tbfec1.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("fecha2", tbfec2.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
            objrep.SetParameterValue("sucursal", cbSucursal.Text.ToUpper)
            'objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)


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

    Private Sub tbFiltrar_ValueChanged(sender As Object, e As EventArgs) Handles tbFiltrar.ValueChanged
        cbsector.Enabled = tbFiltrar.Value
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        _tab.Close()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub PR_ResumenVentasDetallado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub SwitchButton1_ValueChanged(sender As Object, e As EventArgs) Handles swFiltroServ.ValueChanged
        cbServicio.Enabled = swFiltroServ.Value
    End Sub

    Private Sub cbsector_ValueChanged(sender As Object, e As EventArgs) Handles cbsector.ValueChanged
        If cbsector.SelectedIndex >= 0 And cbSucursal.SelectedIndex >= 0 Then
            _prCargarComboServicio(cbServicio, cbsector.Value)

        End If
    End Sub
End Class