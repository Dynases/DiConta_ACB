Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica

Public Class PR_LibroDiario
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "reporte de libro diario".ToUpper

        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    End Sub

    Private Sub _prCargarReporte()
        Dim dt As DataTable = L_prComprobanteReporteLibroDiario(gi_empresaNumi, tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"))

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_LibroDiario2
            objrep.SetDataSource(dt)
            MReportViewer.ReportSource = objrep

            objrep.SetParameterValue("fechaDesde", tbFechaDel.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("fechaHasta", tbFechaAl.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
            'objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)
            'objrep.SetParameterValue("ultimoRegistro", dt.Rows(dt.Rows.Count - 1).Item("oanumi"))


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