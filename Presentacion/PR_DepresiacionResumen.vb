Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_DepresiacionResumen
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem



    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "r e p o r t e    D E P R E S I A C I O N".ToUpper

        tbMes.Value = Now.Month
        tbGestion.Value = Now.Year

        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        tbGestion.Value = Now.Year
    End Sub






    Private Sub _prCargarReporte1()
        If tbGestion.Value = 0 Or tbGestion.Text = String.Empty Then
            ToastNotification.Show(Me, "INGRESE GESTION..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            Return
        End If
        If tbMes.Value = 0 Or tbMes.Text = String.Empty Then
            ToastNotification.Show(Me, "INGRESE MES..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            Return
        End If


        Dim dt As DataTable
        Dim titulo As String = ""
        If swDepreciable.Value = True Then
            dt = L_prDepreRepResumenGestion(tbGestion.Value, gi_empresaNumi)
            titulo = "RESUMEN GESTION " + tbGestion.Text
        Else
            dt = L_prDepreRepResumenMes(tbMes.Value, tbGestion.Value, gi_empresaNumi)
            titulo = "RESUMEN MES " + tbMes.Text + "-" + tbGestion.Text

        End If

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_DepresiacionResumen
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
            objrep.SetParameterValue("titulo2", titulo)
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
        _prCargarReporte1()
    End Sub
End Class