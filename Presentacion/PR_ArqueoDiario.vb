Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica
Public Class PR_ArqueoDiario

    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "reporte arqueo diario".ToUpper

        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    End Sub


    Private Sub _prCargarReporte()
        Dim dt As New DataTable
        dt = L_prTraerReporteArqueoDiario(dtfecha.Value.ToString("yyyy/MM/dd"))

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_ArqueoDiario
            objrep.SetDataSource(dt)

            objrep.SetParameterValue("fecha", dtfecha.Value.ToString("dd/MM/yyyy"))
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

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub PR_ArqueoDiario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        _tab.Close()
    End Sub
End Class