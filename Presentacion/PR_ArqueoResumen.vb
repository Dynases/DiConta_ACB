Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_ArqueoResumen
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private Sub _prIniciarTodo()
        _PMIniciarTodo()

        tbFechaDel.Value = Now
        tbFechaAl.Value = Now
        Me.Text = "reporte de resumen de arqueo".ToUpper

        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    End Sub

    Private Sub _prCargarReporte()
        Dim dt As DataTable = L_prArqueoReporteResumen(tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"))

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_ArqueoResumen
            objrep.SetDataSource(dt)
            MReportViewer.ReportSource = objrep
            Dim dtTotales As DataTable = L_prArqueoReporteResumenObtenerTotales(tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"))

            objrep.SetParameterValue("fecha", tbFechaDel.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("totalEfectivo", IIf(IsDBNull(dtTotales.Rows(0).Item("totalEfectivo")) = True, 0, dtTotales.Rows(0).Item("totalEfectivo")))
            objrep.SetParameterValue("totalTarjeta", IIf(IsDBNull(dtTotales.Rows(0).Item("totalTarjeta")) = True, 0, dtTotales.Rows(0).Item("totalTarjeta")))
            objrep.SetParameterValue("totalDolares", IIf(IsDBNull(dtTotales.Rows(0).Item("totalDolares")) = True, 0, dtTotales.Rows(0).Item("totalDolares")))
            objrep.SetParameterValue("tipoCambio", IIf(IsDBNull(dtTotales.Rows(0).Item("tipoCambio")) = True, 0, dtTotales.Rows(0).Item("tipoCambio")))
            objrep.SetParameterValue("totalVentasAnticipadas", IIf(IsDBNull(dtTotales.Rows(0).Item("totalAnticipo")) = True, 0, dtTotales.Rows(0).Item("totalAnticipo")))
            objrep.SetParameterValue("totalCredito", IIf(IsDBNull(dtTotales.Rows(0).Item("totalCredito")) = True, 0, dtTotales.Rows(0).Item("totalCredito")))
            objrep.SetParameterValue("totalLubricantes", IIf(IsDBNull(dtTotales.Rows(0).Item("totalProducto")) = True, 0, dtTotales.Rows(0).Item("totalProducto")))

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
End Class