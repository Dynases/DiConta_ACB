Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_EstadoCuentasResultados
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private _numiAuxiliarDetalleModulo As Integer = 1
    Private _numiAuxiliarDetalleSucursal As Integer = 11

    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "reporte estado cuentas de resultados".ToUpper

        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        _prCargarComboAuxiliaresVariables(_numiAuxiliarDetalleModulo)
        _prCargarComboAuxiliaresVariablesSucursales(_numiAuxiliarDetalleSucursal)

        ''poner formato a la fecha
        'tbFechaDel.Format = DateTimePickerFormat.Custom
        'tbFechaDel.CustomFormat = "MMMM yyyy"
        ''poner formato a la fecha
        'tbFechaAl.Format = DateTimePickerFormat.Custom
        'tbFechaAl.CustomFormat = "MMMM yyyy"
        tbGestion.Value = Now.Date.Year
        tbFechaDel.Value = New Date(tbGestion.Value, 1, 1)
        tbFechaAl.Value = New Date(tbGestion.Value, 1, 1)
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

        'If tbFechaDel.Value.Year <> tbFechaAl.Value.Year Then
        '    ToastNotification.Show(Me, "seleccione fechas en el mismo año..!!!",
        '                               My.Resources.WARNING, 2000,
        '                               eToastGlowColor.Blue,
        '                               eToastPosition.BottomLeft)
        '    MReportViewer.ReportSource = Nothing
        '    Exit Sub
        'End If
        'If tbFechaDel.Value.Month > tbFechaAl.Value.Month Then
        '    ToastNotification.Show(Me, "seleccione un rango de fecha correcto, la decha de inicio es mayor a la del fin..!!!",
        '                               My.Resources.WARNING, 2000,
        '                               eToastGlowColor.Blue,
        '                               eToastPosition.BottomLeft)
        '    MReportViewer.ReportSource = Nothing
        '    Exit Sub
        'End If

        If tbFechaAl.Value.Year <> tbGestion.Value Or tbFechaDel.Value.Year <> tbGestion.Value Then
            ToastNotification.Show(Me, "seleccione un rango de fecha dentro de la gestion seleccionada..!!!".ToUpper,
                                       My.Resources.WARNING, 2000,
                                           eToastGlowColor.Blue,
                                           eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
            Exit Sub
        Else
            If tbFechaDel.Value.Month > tbFechaAl.Value.Month Then
                ToastNotification.Show(Me, "seleccione un rango de fecha correcto, la fecha de inicio es mayor a la del fin..!!!".ToUpper,
                                           My.Resources.WARNING, 2000,
                                           eToastGlowColor.Blue,
                                           eToastPosition.BottomLeft)
                MReportViewer.ReportSource = Nothing
                Exit Sub
            End If
        End If

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


        '----------------------------------------------------------------------------------------------------
        'cargar reporte
        Dim dt As DataTable = L_prCuentaReporteEstadoCuentasResultadosEstructura(gi_empresaNumi, tbFechaDel.Value.Date.ToString("yyyy/MM/dd"), tbFechaAl.Value.Date.ToString("yyyy/MM/dd"), numiModulo, numiSuc)
        Dim nmes As Integer = 12 'tbMes.Value
        Dim iColum As Integer = 10 'esto es por la consulta ya que hay 11 campos antes de la columna enero,osea la columna enero es la 11 contando con el cero

        Dim mesIni As Integer = tbFechaDel.Value.Month
        Dim mesFin As Integer = tbFechaAl.Value.Month

        For i = 0 To dt.Rows.Count - 1
            Dim sumTotal As Double = 0
            For j = mesIni To mesFin
                Dim fechaDel As String
                Dim ultimoDia As Integer
                Dim fechaAl As String
                If j = mesIni Then 'es el primer mes del rengo de fecha
                    fechaDel = tbFechaDel.Value.Date.ToString("yyyy/MM/dd")
                    ultimoDia = DateSerial(tbGestion.Value, j + 1, 0).Day
                    fechaAl = New Date(tbGestion.Value, j, ultimoDia).ToString("yyyy/MM/dd")
                Else
                    If j = mesFin Then 'es el ultimo mes del rango de fecha
                        fechaDel = New Date(tbGestion.Value, j, 1).ToString("yyyy/MM/dd")
                        ultimoDia = tbFechaAl.Value.Day
                        fechaAl = New Date(tbGestion.Value, j, ultimoDia).ToString("yyyy/MM/dd")
                    Else 'tomo todo el mes normalmente
                        fechaDel = New Date(tbGestion.Value, j, 1).ToString("yyyy/MM/dd")
                        ultimoDia = DateSerial(tbGestion.Value, j + 1, 0).Day
                        fechaAl = New Date(tbGestion.Value, j, ultimoDia).ToString("yyyy/MM/dd")

                    End If
                End If
                Dim dtDatos As DataTable = L_prCuentaReporteEstadoCuentasResultadosDebeHaber(dt.Rows(i).Item("canumi"), fechaDel, fechaAl, gi_empresaNumi, numiModulo, numiSuc)
                Dim saldoTotal As Double = IIf(IsDBNull(dtDatos.Rows(0).Item("debetot")) = True, 0, dtDatos.Rows(0).Item("debetot")) - IIf(IsDBNull(dtDatos.Rows(0).Item("habertot")) = True, 0, dtDatos.Rows(0).Item("habertot"))
                dt.Rows(i).Item(iColum + j) = saldoTotal
                sumTotal = sumTotal + saldoTotal
            Next
            dt.Rows(i).Item("total") = sumTotal
        Next

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_EstadoCuentasResultado
            objrep.SetDataSource(dt)


            objrep.SetParameterValue("fechaDesde", tbFechaDel.Value.Date.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("fechaHasta", tbFechaAl.Value.Date.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
            objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)
            objrep.SetParameterValue("nmes", nmes)

            'cargo para mostrar los meses o no
            For k = 1 To 12
                Dim nombre As String = "mes" + Str(k).Trim
                Dim estado As Integer = 0
                If k >= mesIni And k <= mesFin Then
                    estado = 1
                End If
                objrep.SetParameterValue(nombre, estado)
            Next
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

    Private Sub tbGestion_ValueChanged(sender As Object, e As EventArgs) Handles tbGestion.ValueChanged
        tbFechaDel.Value = New Date(tbGestion.Value, 1, 1)
        tbFechaAl.Value = New Date(tbGestion.Value, 1, 1)
    End Sub

    Private Sub SwitchButton1_ValueChanged(sender As Object, e As EventArgs) Handles tbFiltrar.ValueChanged
        'tbAuxiliar.Enabled = tbFiltrar.Value
        tbVariable.Enabled = tbFiltrar.Value

    End Sub

    Private Sub tbFiltrarSucursal_ValueChanged(sender As Object, e As EventArgs) Handles tbFiltrarSucursal.ValueChanged
        tbVariableSucursal.Enabled = tbFiltrarSucursal.Value

    End Sub
End Class