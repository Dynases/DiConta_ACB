Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_PlanCuentas
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "reporte de resumen de arqueo".ToUpper

        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    End Sub
    Private Function _prCargarArbol() As DataTable
        Dim dtReg As DataTable = L_prCuentaReportePlanCuentas(gi_empresaNumi)
        Dim dtFinal As DataTable = dtReg.Copy
        dtFinal.Rows.Clear()


        _prCrearNodosDelPadre(0, dtFinal, dtReg)

        Return dtFinal
    End Function
    Private Sub _prCrearNodosDelPadre(ByVal indicePadre As Integer, ByVal dtCuentas As DataTable, dtReg As DataTable)

        Dim dataViewHijos As DataView

        'Crear un DataView con los Nodos que dependen del Nodo padre pasado como parámetro
        dataViewHijos = New DataView(dtReg)

        'Filtra por cada padre
        dataViewHijos.RowFilter = dtReg.Columns("capadre").ColumnName +
        " = " + indicePadre.ToString()

        ' Agregar al TreeView los nodos Hijos que se han obtenido en el DataView.
        For Each dataRowCurrent As DataRowView In dataViewHijos

            dtCuentas.Rows.Add(dataRowCurrent.Row.ItemArray)

            'Llamada recurrente al mismo método para agregar los Hijos del Nodo recién agregado.
            _prCrearNodosDelPadre(Int32.Parse(dataRowCurrent("canumi").ToString()), dtCuentas, dtReg)
        Next dataRowCurrent
    End Sub


    Private Sub _prCargarReporte()
        Dim dt As DataTable = _prCargarArbol()

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_PlanCuentas
            objrep.SetDataSource(dt)
            MReportViewer.ReportSource = objrep

            objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)

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