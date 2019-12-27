Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices

Public Class F0_KardexMovimiento

#Region "Atributos"

    Private Titulo As String
    Private Tipo As Byte

    Public Property pTitulo() As String
        Get
            Return Titulo
        End Get
        Set(ByVal valor As String)
            Titulo = valor
        End Set
    End Property

    Public Property pTipo() As Byte
        Get
            Return Tipo
        End Get
        Set(ByVal valor As Byte)
            Tipo = valor
        End Set
    End Property

#End Region


#Region "Variables Globales"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim Dt1Kardex As DataTable
    Dim Dt2KardexTotal As DataTable
    Dim _DuracionSms As Integer = 5
    Dim Lote As Boolean = False
#End Region



    Private Sub _IniciarTodo()
        ''L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.Text = pTitulo
        Me.WindowState = FormWindowState.Maximized
        _prInhabiliitar()
        ''_prAsignarPermisos()
        PanelToolBar1.Visible = False
        btnImprimir.Visible = False
        PanelInferior.Visible = False
        Tb1CodEquipo.Focus()
        _CrearDiseno()
        _prAsignarPermisos()

    End Sub

    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Public Sub _CrearDiseno()
        GroupPanelDatos.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelDatos.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelDatos.Style.TextColor = Color.White

        GroupPanelKardex.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelKardex.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelKardex.Style.TextColor = Color.White

    End Sub
    Private Sub _prInhabiliitar()
        Tb1CodEquipo.ReadOnly = True
        Tb2DescEquipo.ReadOnly = True
        Dti2FechaFin.Value = Now.Date
        Dti1FechaIni.Value = Now.Date
    End Sub



    Private Sub tbCodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles Tb1CodEquipo.KeyDown


        If e.KeyData = Keys.Control + Keys.Enter Then

            Dim dt As DataTable

            dt = L_prMovimientoListarProductoKardex()
            ' a.yfnumi ,a.yfcdprod1 as producto,a.yfcdprod2 as descripcioncorta,a.yfcprod,b.iccven as stock 
            'a.cinumi  ,a.cicdprod1  ,a.cicdprod2,b.iccven as stock 
            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("cinumi", True, "ID", 50))
            listEstCeldas.Add(New Modelos.Celda("cicdprod1", True, "PRODUCTO", 280))
            listEstCeldas.Add(New Modelos.Celda("cicdprod2", True, "DESC. CORTA".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("stock", True, "StockGeneral".ToUpper, 100))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.Context = "Seleccione PRODUCTO".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                ' a.yfnumi ,a.yfcdprod1 as producto,a.yfcdprod2 as descripcioncorta,a.yfcprod,b.iccven as stock 
                If (IsNothing(Row)) Then
                    Return
                End If
                Tb1CodEquipo.Text = ""
                P_ArmarGrillaDatos()
                Tb1CodEquipo.Text = Row.Cells("cinumi").Value
                Tb2DescEquipo.Text = Row.Cells("cicdprod1").Value
                Tb3Saldo.Text = Row.Cells("stock").Value
                Dti1FechaIni.Focus()
                tbFechaVenc.Clear()

            End If

        End If

    End Sub

    Private Sub F0_KardexMovimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()

    End Sub

    Private Sub BtGenerar_Click(sender As Object, e As EventArgs) Handles BtGenerar.Click
        P_GenerarKardexCliente()
    End Sub

    Private Sub P_GenerarKardexCliente()
        P_ArmarGrillaDatos()
    End Sub
    Private Sub P_ArmarKardex()

        Dim saldo As Double = 0
        Dim ingT As Double = 0
        Dim salT As Double = 0

        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 1"))) Then
            ingT = Dt2KardexTotal.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 1")
        End If
        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 2"))) Then
            salT = Dt2KardexTotal.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 2")
        End If

        saldo = ingT + salT
        Dim ing As Double = 0
        Dim sal As Double = 0
        Dim saldoInicial As Double = 0
        'Sumar ingreso de inventario
        ing = IIf(IsDBNull(Dt1Kardex.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 1")), 0, Dt1Kardex.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 1"))
        'Sumar salida de inventario
        sal = IIf(IsDBNull(Dt1Kardex.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 2")), 0, Dt1Kardex.Compute("Sum(cant)", "cprod = " + Tb1CodEquipo.Text + " and concep = 2"))
        'Saldo inicial al partir de la fecha indicada
        saldoInicial = saldo '+ sal + ing
        'Insertamos la primera fila con el saldo Inicial
        Dim f As DataRow
        'Dim st As System.Type
        f = Dt1Kardex.NewRow
        f.Item(0) = 0
        f.Item(1) = Dti1FechaIni.Value.ToShortDateString
        f.Item(2) = 0
        f.Item(3) = "SALDO INICIAL"
        f.Item(4) = "Saldo Inicial"
        f.Item(5) = 1
        f.Item(6) = 1
        f.Item(7) = 0
        f.Item(8) = Tb1CodEquipo.Text
        f.Item(9) = Tb2DescEquipo.Text

        f.Item(11) = 0
        f.Item(12) = saldoInicial
        f.Item(13) = ""
        f.Item(14) = Now.Date

        Dt1Kardex.Rows.InsertAt(f, 0)

        For Each fil As DataRow In Dt1Kardex.Rows
            Dim s As String = fil.Item("concep").ToString
            If (fil.Item("concep").ToString.Equals("1")) Then '
                saldoInicial = saldoInicial + CDbl(fil.Item("cant"))
                fil.Item("saldo") = saldoInicial
            ElseIf (fil.Item("concep").ToString.Equals("2")) Then
                saldoInicial = saldoInicial + CDbl(fil.Item("cant"))
                fil.Item("saldo") = saldoInicial
            End If
        Next

    End Sub



    Private Sub P_ArmarGrillaDatos()
        Dt1Kardex = New DataTable
        Dt2KardexTotal = New DataTable
        If (Tb1CodEquipo.Text.Length > 0) Then
            Dt2KardexTotal = L_VistaKardexInventarioTodo(Tb1CodEquipo.Text, Dti1FechaIni.Value.ToString("yyyy/MM/dd")).Tables(0)
            Dt1Kardex = L_VistaKardexInventario(Tb1CodEquipo.Text, Dti1FechaIni.Value.ToString("yyyy/MM/dd"), Dti2FechaFin.Value.ToString("yyyy/MM/dd")).Tables(0)
            If (Dt1Kardex.Rows.Count > 0) Then
                P_ArmarKardex()
            Else
                ToastNotification.Show(Me, "No hay kardex para el rango de fecha".ToUpper,
                       My.Resources.INFORMATION,
                       _DuracionSms * 1000,
                       eToastGlowColor.Blue,
                       eToastPosition.BottomLeft)
            End If
        Else
            Dt1Kardex = L_VistaKardexInventario("-1", Dti1FechaIni.Value.ToString("yyyy/MM/dd"), Dti2FechaFin.Value.ToString("yyyy/MM/dd")).Tables(0)
        End If

        Dgj1Datos.BoundMode = Janus.Data.BoundMode.Bound
        Dgj1Datos.DataSource = Dt1Kardex
        Dgj1Datos.RetrieveStructure()

        'dar formato a las columnas
        With Dgj1Datos.RootTable.Columns(0)
            .Caption = ""
            .Key = "id"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(1)
            .Caption = "Fecha"
            .Key = "fdoc"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With Dgj1Datos.RootTable.Columns(2)
            .Caption = ""
            .Key = "concep"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With Dgj1Datos.RootTable.Columns(3)
            .Caption = "Concepto"
            .Key = "descConcep"
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(4)
            .Caption = "Observación"
            .Key = "obs"
            .Width = 450
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With Dgj1Datos.RootTable.Columns(5)
            .Caption = ""
            .Key = "est"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(6)
            .Caption = ""
            .Key = "alm"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(7)
            .Caption = ""
            .Key = "id2"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(8)
            .Caption = "Código"
            .Key = "cprod"
            .Width = 80
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With Dgj1Datos.RootTable.Columns(9)
            .Caption = IIf(pTipo = 1, "Equipo", "Producto")
            .Key = "descProd"
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(10)
            .Caption = ""
            .Key = "desc2Prod"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(11)
            .Caption = "Cantidad"
            .Key = "cant"
            .Width = 80
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With Dgj1Datos.RootTable.Columns(12)
            .Caption = "Saldo"
            .Key = "saldo"
            .Width = 80
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With Dgj1Datos.RootTable.Columns(13)
            .Caption = "Cliente"
            .Key = "cliente"
            .Width = 300
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = IIf(pTipo = 1, True, False)
            .Position = 2
        End With
        With Dgj1Datos.RootTable.Columns(14)
            .Caption = "Cliente"
            .Key = "cliente"
            .Width = 300
            .Visible = False
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = IIf(pTipo = 1, True, False)
            .Position = 2
        End With
        'Habilitar Filtradores
        With Dgj1Datos
            .GroupByBoxVisible = False
            '.FilterRowFormatStyle.BackColor = Color.Blue
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .AlternatingColors = True
            .RecordNavigator = True
            .RecordNavigatorText = "Movimiento"
            .RowHeaders = InheritableBoolean.True
        End With
        _prAplicarCondiccionJanus()
    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(Dgj1Datos.RootTable.Columns("saldo"), ConditionOperator.LessThan, 0)
        fc.FormatStyle.BackColor = Color.Red
        fc.FormatStyle.ForeColor = Color.White
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.FontName = "Open Sans"




        Dgj1Datos.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub btImprimir_Click(sender As Object, e As EventArgs) Handles btImprimir.Click
        If (Dgj1Datos.GetRows.Count > 0) Then
            P_GenerarReporte()
        Else
            ToastNotification.Show(Me, "No hay kardex para el rango de fecha".ToUpper,
                       My.Resources.INFORMATION,
                       _DuracionSms * 1000,
                       eToastGlowColor.Blue,
                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub P_GenerarReporte()

        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        P_Global.Visualizador = New Visualizador
        Dim objrep As Object
        If (pTipo = 1) Then
            ''  objrep = New R_KardexInventarioEquipos
        Else
            objrep = New R_KardexInventarioProducto
        End If
        objrep.SetDataSource(Dt1Kardex)
        objrep.SetParameterValue("FechaIni", Dti1FechaIni.Value.ToShortDateString)
        objrep.SetParameterValue("FechaFin", Dti2FechaFin.Value.ToShortDateString)
        objrep.SetParameterValue("Titulo", IIf(pTipo = 1, "Equipos", "Productos"))
        objrep.SetParameterValue("Saldo", CDbl(Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo").ToString))

        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar
    End Sub

    Private Sub btActualizar_Click(sender As Object, e As EventArgs) Handles btActualizar.Click

        If (Dt1Kardex.Rows.Count > 0) Then
            If (Now.Date.ToString("yyyy/MM/dd").Equals(Dti2FechaFin.Value.ToString("yyyy/MM/dd"))) Then
                L_Actualizar_SaldoInventario(Tb1CodEquipo.Text, Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo").ToString, "1")
                Tb3Saldo.Text = Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo").ToString
                ToastNotification.Show(Me, "Saldo actualizado Correctamente!!!".ToUpper,
                       My.Resources.OK1,
                       _DuracionSms * 200,
                       eToastGlowColor.Green,
                       eToastPosition.MiddleCenter)
            Else
                ToastNotification.Show(Me, "Para actualizar el saldo, la fecha final tiene que ser la de hoy!!!".ToUpper,
                       My.Resources.INFORMATION,
                       _DuracionSms * 1000,
                       eToastGlowColor.Red,
                       eToastPosition.MiddleCenter)

            End If
        End If
    End Sub

 

    Private Sub _prSalir()


        _modulo.Select()
        _tab.Close()


    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub
End Class