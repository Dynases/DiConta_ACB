Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class PR_Presupuesto
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem


    Private _tipoIngreso As Integer = 1
    Private _tipoEgreso As Integer = 2

    Private _numiAuxiliarDetalleModulo As Integer = 1
    Private _numiAuxiliarDetalleSucursal As Integer = 11
    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "r e p o r t e    p r e s u p u e s t o".ToUpper
        _prCargarComboAuxiliaresSucursales(_numiAuxiliarDetalleSucursal)
        _prCargarComboAuxiliaresModulos(_numiAuxiliarDetalleModulo)


        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        tbGestion.Value = Now.Year
    End Sub

    Private Sub _prCargarComboAuxiliaresSucursales(numi As String)
        Dim dt As New DataTable
        dt = L_prAuxiliarDetalleGeneral(numi)

        With tbSucursal
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

    Private Sub _prCargarComboAuxiliaresModulos(numi As String)
        Dim dt As New DataTable
        dt = L_prAuxiliarDetalleGeneral(numi)

        With tbModulo
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
    Private Function _prCargarGridPresupuestoGrabadoIngreso(gestion As Integer) As DataTable
        Dim dt As New DataTable
        'empiezo a cargar la gestion
        dt = L_prPresupuestoObtenenerServiciosPorTipo(-1, _tipoIngreso)
        Dim dtSectores As DataTable = L_prlistarCategoriasActivos()

        'hago un for a todos los tipos que existen
        For Each fila As DataRow In dtSectores.Rows
            dt.Rows.Add(0, "INGRESOS UNIDAD DE " + fila("cedesc1"))
            dt.Rows(dt.Rows.Count - 1).Item("chtipo") = _tipoIngreso
            Dim dtTemp As New DataTable
            If fila("cenum") = 3 Then 'entonces son los servicios de lavadero
                dtTemp = L_prPresupuestoObtenenerServiciosLavadero()
                Dim dtServLav As DataTable = L_prPresupuestoObtenenerServiciosPorTipo(fila("cenum"), _tipoIngreso)
                dtTemp.Merge(dtServLav)
            Else
                dtTemp = L_prPresupuestoObtenenerServiciosPorTipo(fila("cenum"), _tipoIngreso)

            End If
            dt.Merge(dtTemp)

        Next

        'ahora a empezar a cargar datos

        'For Each fila As DataRow In dt.Rows
        '    Dim numiServ As Integer = fila.Item("ednumi")
        '    If numiServ <> 0 Then
        '        Dim j As Integer = 0
        '        Dim montoAcu As Double = 0
        '        Dim montosServicio As DataTable = L_prPresupuestoObtenerValoresPorServicio(gestion, numiServ)
        '        For i As Integer = 2 To dt.Columns.Count - 3 Step 2
        '            Dim montoMes As Double = montosServicio.Rows(j).Item("chmonto")
        '            fila.Item(i) = montoMes
        '            montoAcu = montoAcu + montoMes
        '            fila.Item(i + 1) = montoAcu
        '            j = j + 1
        '        Next
        '        fila.Item("total") = montoAcu

        '    End If
        'Next

        '----------


        Return dt

    End Function

    Private Function _prCargarGridPresupuestoGrabadoEgreso(gestion As Integer) As DataTable
        Dim dt As New DataTable
        'empiezo a cargar la gestion
        dt = L_prPresupuestoObtenenerCuentasPorPadre(-1, _tipoEgreso)
        Dim dtSectores As DataTable = L_prPresupuestoObtenenerCuentasEgresoPadres(gi_empresaNumi)

        'hago un for a todos los tipos que existen
        For Each fila As DataRow In dtSectores.Rows
            dt.Rows.Add(0, fila("cadesc"))
            dt.Rows(dt.Rows.Count - 1).Item("chtipo") = _tipoEgreso

            Dim dtTemp As New DataTable
            dtTemp = L_prPresupuestoObtenenerCuentasPorPadre(fila("canumi"), _tipoEgreso)
            dt.Merge(dtTemp)
        Next

        'ahora a empezar a cargar datos

        'For Each fila As DataRow In dt.Rows
        '    Dim numiCuenta As Integer = fila.Item("ednumi")
        '    If numiCuenta <> 0 Then
        '        Dim j As Integer = 0
        '        Dim montoAcu As Double = 0
        '        Dim montosCuenta As DataTable = L_prPresupuestoObtenerValoresPorServicio(gestion, numiCuenta)
        '        For i As Integer = 2 To dt.Columns.Count - 3 Step 2
        '            Dim montoMes As Double = montosCuenta.Rows(j).Item("chmonto")
        '            fila.Item(i) = montoMes
        '            montoAcu = montoAcu + montoMes
        '            fila.Item(i + 1) = montoAcu
        '            j = j + 1
        '        Next
        '        fila.Item("total") = montoAcu

        '    End If
        'Next

        '----------
        Return dt

    End Function
    Private Sub _prCargarReporte()
        If tbGestion.Value = 0 Or tbGestion.Text = String.Empty Then
            ToastNotification.Show(Me, "INGRESE GESTION..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            Return
        End If
        Dim dt As New DataTable

        'Dim dtBuscador As DataTable = L_prPresupuestoGeneralGestiones()
        'Dim filaFiltrada As DataRow() = dtBuscador.Select("chanio=" + tbGestion.Value.ToString)
        'If filaFiltrada.Count > 0 Then

        '    Dim dtIngresos As DataTable = _prCargarGridPresupuestoGrabadoIngreso(tbGestion.Value)
        '    Dim dtEgresos As DataTable = _prCargarGridPresupuestoGrabadoEgreso(tbGestion.Value)
        '    dt.Merge(dtIngresos)
        '    dt.Merge(dtEgresos)

        '    'cargar los resultados
        '    Dim dtResultado As DataTable = dt.Copy
        '    dtResultado.Clear()
        '    dtResultado.Rows.Add(0, "RESULTADO DE LA GESTION:")
        '    dtResultado.Rows(dtResultado.Rows.Count - 1).Item("chtipo") = 3

        '    For i As Integer = 2 To dtResultado.Columns.Count - 2 Step 2
        '        Dim totIngreso As Double = 0
        '        For Each filaI As DataRow In dtIngresos.Rows
        '            If filaI.Item("ednumi") <> 0 Then
        '                totIngreso = totIngreso + filaI.Item(i)
        '            End If
        '        Next

        '        Dim totEgreso As Double = 0
        '        For Each filaE As DataRow In dtEgresos.Rows
        '            If filaE.Item("ednumi") <> 0 Then
        '                totEgreso = totEgreso + filaE.Item(i)
        '            End If
        '        Next

        '        dtResultado.Rows(0).Item(i) = totIngreso - totEgreso
        '    Next

        '    dt.Merge(dtResultado)
        'End If

        'If (dt.Rows.Count > 0) Then

        '    Dim objrep As New R_Presupuesto
        '    objrep.SetDataSource(dt)
        '    objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)

        '    MReportViewer.ReportSource = objrep


        '    MReportViewer.Show()
        '    MReportViewer.BringToFront()
        'Else
        '    ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
        '                               My.Resources.WARNING, 2000,
        '                               eToastGlowColor.Blue,
        '                               eToastPosition.BottomLeft)
        '    MReportViewer.ReportSource = Nothing
        'End If

    End Sub


    Private Sub _prCargarReporte1()
        If tbGestion.Value = 0 Or tbGestion.Text = String.Empty Then
            ToastNotification.Show(Me, "INGRESE GESTION..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            Return
        End If
        If tbModulo.SelectedIndex < 0 Then
            tbModulo.BackColor = Color.Red
            MEP.SetError(tbModulo, "seleccione modulo!".ToUpper)
            Return
        Else
            tbModulo.BackColor = Color.White
            MEP.SetError(tbModulo, "")
        End If


        If tbSucursal.SelectedIndex < 0 Then
            tbSucursal.BackColor = Color.Red
            MEP.SetError(tbSucursal, "seleccione sucursal!".ToUpper)
            Return
        Else
            tbSucursal.BackColor = Color.White
            MEP.SetError(tbSucursal, "")
        End If

        Dim dt As DataTable = L_prPresupuestoReporteResumen(tbGestion.Value, tbMes.Value, tbModulo.Value, tbSucursal.Value, gi_empresaNumi)

        If (dt.Rows.Count > 0) Then

            Dim objrep As New R_PresupuestoResumen
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)

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