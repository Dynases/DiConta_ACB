Option Strict Off
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
Imports System.Drawing.Printing
Imports System.Math

Public Class F1_AsientosContables

#Region "Variables Globales"
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim NumiCertificacion As Integer = 6
    Public _ListVentas As DataTable
    Dim NumiAdministracion As Integer = 7

    Dim conRedondeo As Boolean = False


#Region "Variables Multiproposito"
    '    VariableMultiproposito
    Dim Lavadero As Integer = 42
    Dim Remolque As Integer = 55
    Dim Administracion As Integer = 56
    Dim Escuela As Integer = 1
    Dim Certificacion As Integer = 43
    Dim Hotel As Integer = 44
    Dim Linea As Integer = 0
#End Region

#End Region
    'L_prIntegracionBancos
#Region "Metodos SobreEscritos"

    Private Sub _prCargarBancos()
        Dim dt As New DataTable
        dt = L_prIntegracionBancos()
        grbanco.DataSource = dt
        grbanco.RetrieveStructure()
        grbanco.AlternatingColors = True

        'banco.canumi ,cast ('' as image) as img ,banco .canombre ,banco .cacuenta ,banco.caimage ,0 as camonto, 0 as caestado
        With grbanco.RootTable.Columns("canumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grbanco.RootTable.Columns("img")
            .Width = 100
            .Caption = "Imagen"
            .Visible = True

        End With

        With grbanco.RootTable.Columns("canombre")
            .Width = 250
            .Visible = True
            .Caption = "Bancos"
        End With
        With grbanco.RootTable.Columns("cacuenta")
            .Width = 110
            .Visible = True
            .Caption = "Nro Cuenta"
        End With

        With grbanco.RootTable.Columns("caimage")
            .Width = 110
            .Visible = False
            .Caption = "TIPO DE CAMBIO"
        End With

        With grbanco.RootTable.Columns("camonto")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Monto"

        End With
        With grbanco.RootTable.Columns("caestado")
            .Width = 100
            .Caption = "estado"
            .Visible = False

        End With
        With grbanco.RootTable.Columns("ctanumi")
            .Width = 10
            .Caption = "numibanco"
            .Visible = False

        End With

        With grbanco
            .DefaultFilterRowComparison = FilterConditionOperator.Equal
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        _prDibujarImagenes()
    End Sub

    Private Sub _prCargarBancosRegistrados(ifnumi As String)
        Dim dt As New DataTable
        dt = L_prIntegracionBancosRegistrados(ifnumi)
        grbanco.DataSource = dt
        grbanco.RetrieveStructure()
        grbanco.AlternatingColors = True

        'banco.canumi ,cast ('' as image) as img ,banco .canombre ,banco .cacuenta ,banco.caimage ,0 as camonto, 0 as caestado
        With grbanco.RootTable.Columns("canumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grbanco.RootTable.Columns("img")
            .Width = 100
            .Caption = "Imagen"
            .Visible = True

        End With

        With grbanco.RootTable.Columns("canombre")
            .Width = 250
            .Visible = True
            .Caption = "Bancos"
        End With
        With grbanco.RootTable.Columns("cacuenta")
            .Width = 110
            .Visible = True
            .Caption = "Nro Cuenta"
        End With

        With grbanco.RootTable.Columns("caimage")
            .Width = 110
            .Visible = False
            .Caption = "TIPO DE CAMBIO"
        End With

        With grbanco.RootTable.Columns("camonto")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Monto"

        End With
        With grbanco.RootTable.Columns("caestado")
            .Width = 100
            .Caption = "estado"
            .Visible = False

        End With

        With grbanco
            .DefaultFilterRowComparison = FilterConditionOperator.Equal
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        _prDibujarImagenes()
    End Sub
    Public Sub _prDibujarImagenes()
        Dim length As Integer = CType(grbanco.DataSource, DataTable).Rows.Count
        For i As Integer = 0 To length - 1 Step 1
            Dim nameImagen As String = CType(grbanco.DataSource, DataTable).Rows(i).Item("caimage")
            If (nameImagen.Equals("Default.jpg")) Then
                Dim Bin As New MemoryStream
                Dim img As New Bitmap(My.Resources.imageDefault, 100, 80)
                img.Save(Bin, Imaging.ImageFormat.Jpeg)
                Bin.Dispose()

                CType(grbanco.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            Else
                Dim Bin As New MemoryStream
                If (File.Exists(RutaGlobal + "\Imagenes\Imagenes Faubrica" + nameImagen)) Then
                    Dim img As New Bitmap(New Bitmap(RutaGlobal + "\Imagenes\Imagenes Faubrica" + nameImagen), 90, 40)
                    img.Save(Bin, Imaging.ImageFormat.Jpeg)
                    CType(grbanco.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
                    Bin.Dispose()
                Else

                    Dim img As New Bitmap(My.Resources.imageDefault, 100, 80)
                    img.Save(Bin, Imaging.ImageFormat.Jpeg)
                    Bin.Dispose()

                    CType(grbanco.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer

                End If
            End If


        Next
    End Sub
    Private Sub _prCargarMovimiento()
        Dim dt As New DataTable
        dt = L_prIntegracionGeneral()
        grmovimientos.DataSource = dt
        grmovimientos.RetrieveStructure()
        grmovimientos.AlternatingColors = True

        'a.ifnumi ,a.ifto001numi,comprobante .oanumdoc  ,a.iftc ,a.iffechai ,a.iffechaf ,a.ifest 
        With grmovimientos.RootTable.Columns("ifnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grmovimientos.RootTable.Columns("ifto001numi")
            .Width = 100
            .Visible = True
            .Caption = "COD COMPROBANTE"
        End With
        With grmovimientos.RootTable.Columns("oanumdoc")
            .Width = 110
            .Visible = True
            .Caption = "NRO DOCUMENTO"
        End With

        With grmovimientos.RootTable.Columns("iftc")
            .Width = 110
            .Visible = True
            .Caption = "TIPO DE CAMBIO"
        End With
        With grmovimientos.RootTable.Columns("iffechai")
            .Width = 110
            .Visible = True
            .Caption = "FECHA I".ToUpper
            .FormatString = "dd/MM/yyyy"
        End With
        With grmovimientos.RootTable.Columns("iffechaf")
            .Width = 110
            .Visible = True
            .Caption = "FECHA F".ToUpper
            .FormatString = "dd/MM/yyyy"
        End With

        With grmovimientos.RootTable.Columns("ifest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        Dim dtSuc As DataTable
        dtSuc = L_fnListarAlmacenDosificacion()

        With grmovimientos.RootTable.Columns("ifsuc")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Caption = "SUCURSAL"

            .HasValueList = True
            'Set EditType to Combo or DropDownList.
            'In a MultipleValues Column, the dropdown will appear with a CheckBox
            'at the left of each item to let the user select multiple items
            .EditType = EditType.DropDownList
            .ValueList.PopulateValueList(dtSuc.DefaultView, "cod", "desc")
            .CompareTarget = ColumnCompareTarget.Text
            .DefaultGroupInterval = GroupInterval.Text
            .AllowSort = False

        End With
        With grmovimientos
            .DefaultFilterRowComparison = FilterConditionOperator.Equal
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        If (dt.Rows.Count <= 0) Then
            L_prIntegracionDetalle(-1)
        End If
    End Sub

    Public Sub _prArmarDetalleDt(ByRef dt As DataTable)
        Dim tabla As DataTable = dt.Copy
        tabla.Rows.Clear()
        Dim cuenta As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim aux As Integer = dt.Rows(i).Item("canumi")
            If (aux <> cuenta) Then
                Linea = Linea + 1
                Dim dtObtenerCuenta As DataTable = L_prCuentaDiferencia(aux)  ''
                tabla.Rows.Add(dtObtenerCuenta.Rows(0).Item("canumi"), dtObtenerCuenta.Rows(0).Item("cacta"), dtObtenerCuenta.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)

                tabla.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), dtObtenerCuenta.Rows(1).Item("cacta"), dtObtenerCuenta.Rows(1).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)

                Dim debe As Double = IIf(IsDBNull(dt.Rows(i).Item("debe")), 0, dt.Rows(i).Item("debe"))
                Dim haber As Double = IIf(IsDBNull(dt.Rows(i).Item("haber")), 0, dt.Rows(i).Item("haber"))
                Dim debeSus As Double = IIf(IsDBNull(dt.Rows(i).Item("debesus")), 0, dt.Rows(i).Item("debesus"))
                Dim haberSus As Double = IIf(IsDBNull(dt.Rows(i).Item("habersus")), 0, dt.Rows(i).Item("habersus"))
                If (debe = 0 And haber = 0 And debeSus = 0 And haberSus = 0) Then
                    tabla.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, 0, 0)

                Else
                    tabla.Rows.Add(dt.Rows(i).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, dt.Rows(i).Item("tc"), dt.Rows(i).Item("debe"), dt.Rows(i).Item("haber"), dt.Rows(i).Item("debesus"), dt.Rows(i).Item("habersus"), dt.Rows(i).Item("variable"), Linea)
                End If



            Else
                Linea = Linea + 1
                '             cuenta.canumi , cuenta.cacta As nro, detalle.obobs, 0 As chporcen, 0 As chdebe , 0 As chhaber, i.iftc  as tc
                ',detalle .obdebebs  as debe,obhaberbs  as haber,obdebeus  as debesus
                ',obhaberus  as habersus,obaux1  as variable,oblin  as linea

                Dim debe As Integer = IIf(IsDBNull(dt.Rows(i).Item("debe")), 0, dt.Rows(i).Item("debe"))
                Dim haber As Integer = IIf(IsDBNull(dt.Rows(i).Item("haber")), 0, dt.Rows(i).Item("haber"))

                If (debe = 0 And haber = 0) Then
                    tabla.Rows.Add(dt.Rows(i).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, 0, 0)

                Else
                    tabla.Rows.Add(dt.Rows(i).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, dt.Rows(i).Item("tc"), dt.Rows(i).Item("debe"), dt.Rows(i).Item("haber"), dt.Rows(i).Item("debesus"), dt.Rows(i).Item("habersus"), dt.Rows(i).Item("variable"), Linea)
                End If

            End If
            cuenta = aux
        Next
        dt = tabla
    End Sub
    Private Sub _prCargarDetalleMovimiento(_numi As String)
        Dim dt As New DataTable
        dt = L_prIntegracionDetalle(_numi)
        _prArmarDetalleDt(dt)
        grComprobante.DataSource = dt
        grComprobante.RetrieveStructure()
        grComprobante.AlternatingColors = True
        ' a.icid ,a.icibid ,a.iccprod ,b.cicdprod1  as producto,a.iccant ,
        'a.icsector  ,Cast(null as image ) as img,1 as estado,
        '(Sum(inv.iccven)  +a.iccant ) as stock

        With grComprobante.RootTable.Columns("canumi")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("variable")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("linea")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("nro")
            .Width = 120
            .Caption = "NRO CUENTA"
            .Visible = True
        End With
        With grComprobante.RootTable.Columns("cadesc")
            .Width = 500
            .Caption = "DESCRIPCION"
            .Visible = True
        End With
        With grComprobante.RootTable.Columns("chporcen")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("chdebe")
            .Width = 180
            .Caption = "DEBE"
            .Visible = False
        End With
        With grComprobante.RootTable.Columns("chhaber")
            .Width = 180
            .Caption = "HABER"
            .Visible = False
        End With
        With grComprobante.RootTable.Columns("tc")
            .Width = 70
            .Caption = "TC"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grComprobante.RootTable.Columns("debe")
            .Width = 100
            .Caption = "DEBE BS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grComprobante.RootTable.Columns("haber")
            .Width = 100
            .Caption = "HABER BS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With

        With grComprobante.RootTable.Columns("debesus")
            .Width = 100
            .Caption = "DEBE SUS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grComprobante.RootTable.Columns("habersus")
            .Width = 100
            .Caption = "HABER SUS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grComprobante
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .TotalRow = InheritableBoolean.True

            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        _prAplicarCondiccionJanus()
    End Sub

    Private Sub _prCargarDetalleMovimientoBanco(_numi As String)
        Dim dt As New DataTable
        dt = L_prIntegracionDetalleBanco(_numi)
        _prArmarDetalleDt(dt)
        grAsientoBanco.DataSource = dt
        grAsientoBanco.RetrieveStructure()
        grAsientoBanco.AlternatingColors = True
        ' a.icid ,a.icibid ,a.iccprod ,b.cicdprod1  as producto,a.iccant ,
        'a.icsector  ,Cast(null as image ) as img,1 as estado,
        '(Sum(inv.iccven)  +a.iccant ) as stock

        With grAsientoBanco.RootTable.Columns("canumi")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("variable")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("linea")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("nro")
            .Width = 120
            .Caption = "NRO CUENTA"
            .Visible = True
        End With
        With grAsientoBanco.RootTable.Columns("cadesc")
            .Width = 500
            .Caption = "DESCRIPCION"
            .Visible = True
        End With
        With grAsientoBanco.RootTable.Columns("chporcen")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("chdebe")
            .Width = 180
            .Caption = "DEBE"
            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("chhaber")
            .Width = 180
            .Caption = "HABER"
            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("tc")
            .Width = 70
            .Caption = "TC"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grAsientoBanco.RootTable.Columns("debe")
            .Width = 100
            .Caption = "DEBE BS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grAsientoBanco.RootTable.Columns("haber")
            .Width = 100
            .Caption = "HABER BS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With

        With grAsientoBanco.RootTable.Columns("debesus")
            .Width = 100
            .Caption = "DEBE SUS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grAsientoBanco.RootTable.Columns("habersus")
            .Width = 100
            .Caption = "HABER SUS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grAsientoBanco
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .TotalRow = InheritableBoolean.True

            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        _prAplicarCondiccionJanusBanco()
    End Sub
#End Region
#Region "METODOS PRIVADOS"

    Private Sub _IniciarTodo()
        ''L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboAlmacen(cbSucursal)
        'SuperTabItemBuscador.Visible = False
        MSuperTabControl.SelectedTabIndex = 0
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "COMPROBANTES DE SERVICIOS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()
        _prCargarMovimiento()
        _prInhabiliitar()
        _prCargarBancos()
    End Sub

    Private Sub _prCargarComboAlmacen(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarAlmacenDosificacion()
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
        If (gb_userTodasSuc = False And CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then


            mCombo.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
            mCombo.ReadOnly = True
        Else
            mCombo.ReadOnly = False
        End If
    End Sub
    Public Function _fnObtenerPosSucursal(numi As Integer)
        Dim length As Integer = CType(cbSucursal.DataSource, DataTable).Rows.Count - 1
        For i As Integer = 0 To length Step 1
            If (CType(cbSucursal.DataSource, DataTable).Rows(i).Item("cod") = numi) Then
                Return i
            End If
        Next
        Return -1
    End Function

    Public Sub _prArmarCuadre(ByRef dt As DataTable)
        Try
            Dim totaldebe As Double = dt.Compute("Sum(debe)", "")
            Dim totalhaber As Double = dt.Compute("Sum(haber)", "")
            Dim totaldebesus As Double = dt.Compute("Sum(debesus)", "")
            Dim totalhabersus As Double = dt.Compute("Sum(habersus)", "")
            Dim restantedebe As Double = 0
            Dim restanteHaber As Double = 0
            Dim RestanteDebeSus As Double = 0
            Dim RestanteHaberSus As Double = 0
            If (totaldebe > totalhaber) Then
                restanteHaber = totaldebe - totalhaber
            Else
                restantedebe = totalhaber - totaldebe
            End If
            If (totaldebesus > totalhabersus) Then
                RestanteHaberSus = totaldebesus - totalhabersus
            Else
                RestanteDebeSus = totalhabersus - totaldebesus
            End If
            If (restantedebe > 0 Or restanteHaber > 0 Or RestanteDebeSus > 0 Or RestanteHaberSus > 0) Then
                Dim dtObtenerCuenta As DataTable = L_prCuentaDiferencia(652)  '''3=Lavadero
                dt.Rows.Add(dtObtenerCuenta.Rows(0).Item("canumi"), dtObtenerCuenta.Rows(0).Item("cacta"), dtObtenerCuenta.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)
                dt.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), dtObtenerCuenta.Rows(1).Item("cacta"), dtObtenerCuenta.Rows(1).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)
                Linea = Linea + 1
                dt.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), DBNull.Value, "Ajuste de Cambio", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, IIf(restantedebe = 0, DBNull.Value, restantedebe), IIf(restanteHaber = 0, DBNull.Value, restanteHaber), IIf(RestanteDebeSus = 0, DBNull.Value, RestanteDebeSus), IIf(RestanteHaberSus = 0, DBNull.Value, RestanteHaberSus), Administracion, Linea)

                conRedondeo = True
            Else
                conRedondeo = False

            End If
        Catch ex As Exception

        End Try



    End Sub

    Public Sub _prArmarCabanas(ByRef dt As DataTable)
        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalServiciosLavadero(-10, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  '''3=Lavadero
        If (dtServicioTotal.Rows(0).Item("total") > 0) Then

            Dim numeroCuenta As Integer = 0

            Dim numiCuenta As Integer = 295
            Dim dtnombrePadre As DataTable = L_prObtenerNombreCuenta("411106")
            dt.Rows.Add(numiCuenta, "411106", dtnombrePadre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Hotel, 0)

            Dim dtcabana As DataTable = L_prObtenerServiciosCabanas(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
            For i As Integer = 0 To dtcabana.Rows.Count - 1 Step 1
                Dim numiCuentaHijo As Integer = 296
                Dim dttotc As DataTable
                Dim total As Double = dtcabana.Rows(i).Item("total")


                If (total > 0) Then
                    Dim dtnombre As DataTable = L_prObtenerNombreCuenta("41110601")
                    If (dtnombre.Rows.Count > 0) Then

                        If (numeroCuenta <> 41110601) Then
                            Dim totalDescuento As Double = total - Round(to3Decimales((total * 0.13)), 2)

                            dt.Rows.Add(numiCuentaHijo, "41110601", dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Hotel, 0)
                            numeroCuenta = 41110601
                            Linea = Linea + 1
                            Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                            dt.Rows.Add(numiCuentaHijo, DBNull.Value, dtcabana.Rows(i).Item("descripcion"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Hotel, Linea)

                        Else

                            Dim totalDescuento As Double = (total - Round(to3Decimales((total * 0.13)), 2))
                            Linea = Linea + 1

                            Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                            dt.Rows.Add(numiCuentaHijo, DBNull.Value, dtcabana.Rows(i).Item("descripcion"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Hotel, Linea)
                            numeroCuenta = 41110601
                        End If



                    End If


                End If



            Next
        End If

    End Sub
    Public Sub _prArmarCertificacion(ByRef dt As DataTable)
        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalServiciosLavadero(NumiCertificacion, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  '''3=Lavadero
        If (dtServicioTotal.Rows(0).Item("total") > 0) Then
            Dim dtCuentaPadres As DataTable = L_prListarCuentasServicioGeneral(gi_empresaNumi, NumiCertificacion, cbSucursal.Value)
            Dim numeroCuenta As Integer = 0
            For j As Integer = 0 To dtCuentaPadres.Rows.Count - 1 Step 1
                Dim numiCuenta As Integer = dtCuentaPadres.Rows(j).Item("canumi")
                dt.Rows.Add(numiCuenta, dtCuentaPadres.Rows(j).Item("cacta"), dtCuentaPadres.Rows(j).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Certificacion, 0)

                Dim dtCuentaServ As DataTable = L_prServicioListarCuentasServicioGeneral(dtCuentaPadres.Rows(j).Item("cacta"), gi_empresaNumi, NumiCertificacion, cbSucursal.Value)
                For i As Integer = 0 To dtCuentaServ.Rows.Count - 1 Step 1
                    Dim numiCuentaHijo As Integer = dtCuentaServ.Rows(i).Item("canumi")
                    Dim dttotc As DataTable
                    If (dtCuentaServ.Rows(i).Item("seest") = 1) Then

                        dttotc = L_prObtenerTotalPorCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"), dtCuentaServ.Rows(i).Item("seref"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                        If (dttotc.Rows(0).Item("total") > 0) Then
                            Dim dtnombre As DataTable = L_prObtenerNombreCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"))
                            If (dtnombre.Rows.Count > 0) Then

                                If (numeroCuenta <> dtCuentaServ.Rows(i).Item("senrocuenta")) Then
                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))

                                    dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Certificacion, 0)
                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                                    Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad")
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, "POR INGRESO DE " + Str(cantidad) + " " + dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Certificacion, Linea)

                                Else

                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad")
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, "POR INGRESO DE " + Str(cantidad) + " " + dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Certificacion, Linea)
                                End If



                            End If


                        End If

                    End If
                Next
            Next

        End If

    End Sub



    Public Sub _prArmarAdministracionn(ByRef dt As DataTable)
        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalServiciosLavadero(NumiAdministracion, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  '''3=Lavadero
        If (dtServicioTotal.Rows(0).Item("total") > 0) Then
            Dim dtCuentaPadres As DataTable = L_prListarCuentasServicioGeneral(gi_empresaNumi, NumiAdministracion, cbSucursal.Value)
            Dim numeroCuenta As Integer = 0
            For j As Integer = 0 To dtCuentaPadres.Rows.Count - 1 Step 1
                Dim numiCuenta As Integer = dtCuentaPadres.Rows(j).Item("canumi")
                dt.Rows.Add(numiCuenta, dtCuentaPadres.Rows(j).Item("cacta"), dtCuentaPadres.Rows(j).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, NumiAdministracion, 0)

                Dim dtCuentaServ As DataTable = L_prServicioListarCuentasServicioGeneral(dtCuentaPadres.Rows(j).Item("cacta"), gi_empresaNumi, NumiAdministracion, cbSucursal.Value)
                For i As Integer = 0 To dtCuentaServ.Rows.Count - 1 Step 1
                    Dim numiCuentaHijo As Integer = dtCuentaServ.Rows(i).Item("canumi")
                    Dim dttotc As DataTable
                    If (dtCuentaServ.Rows(i).Item("seest") = 1) Then

                        dttotc = L_prObtenerTotalPorCuentaAdministracion(dtCuentaServ.Rows(i).Item("senrocuenta"), dtCuentaServ.Rows(i).Item("seref"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value, NumiAdministracion)
                        If (dttotc.Rows(0).Item("total") > 0) Then
                            Dim dtnombre As DataTable = L_prObtenerNombreCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"))
                            If (dtnombre.Rows.Count > 0) Then

                                If (numeroCuenta <> dtCuentaServ.Rows(i).Item("senrocuenta")) Then 'OJO, ACA PONE EL MONTO PERO DESCONTADO
                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))
                                    Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad")

                                    'AUMENTADO DANNY''''''''''''''
                                    'Dim dtLicenciaInternacional As DataTable = L_prIntegracionObtenerCuentaLicenciaInternacional()
                                    'totalDescuento = totalDescuento - dtLicenciaInternacional.Rows(0).Item("esmonto") * cantidad
                                    '''''''''''''''''''''''''
                                    dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)
                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                                    ''Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad") ''movido por danny unas filas arriba
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, "POR INGRESO DE " + Str(cantidad) + " " + dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)


                                    'AUMENTADO DANNY''''''''''''''''''''''''''''''''
                                    ''Linea = Linea + 1

                                    ''Dim __desc As String = "GASTO POR " + dtnombre.Rows(0).Item("cadesc") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy")
                                    ''Dim __numiCuenta As Integer = dtLicenciaInternacional.Rows(0).Item("esnumitc1")
                                    ''Dim __cuenta As Integer = dtLicenciaInternacional.Rows(0).Item("cacta")
                                    ''Dim __total As Double = dtLicenciaInternacional.Rows(0).Item("esmonto") * cantidad
                                    ''Dim __totalDolares As Double = Round(to3Decimales(__total / (tbTipoCambio.Value)), 2)
                                    ''dt.Rows.Add(__numiCuenta, __cuenta, __desc, DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, __total, DBNull.Value, __totalDolares, Administracion, Linea)

                                    '''''''''''''''''''''''''''''''''''''''''''''''''

                                Else

                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad")
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, "POR INGRESO DE " + Str(cantidad) + " " + dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)
                                End If



                            End If


                        End If

                    End If
                Next
            Next

        End If

    End Sub
    Public Sub _prArmarEscuela(ByRef dt As DataTable)
        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalServiciosLavadero(1, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  '''3=Lavadero
        If (dtServicioTotal.Rows(0).Item("total") > 0) Then
            Dim dtCuentaPadres As DataTable = L_prListarCuentasServicioGeneral(gi_empresaNumi, 1, cbSucursal.Value)
            Dim numeroCuenta As Integer = 0
            For j As Integer = 0 To dtCuentaPadres.Rows.Count - 1 Step 1
                Dim numiCuenta As Integer = dtCuentaPadres.Rows(j).Item("canumi")
                dt.Rows.Add(numiCuenta, dtCuentaPadres.Rows(j).Item("cacta"), dtCuentaPadres.Rows(j).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Escuela, 0)

                Dim dtCuentaServ As DataTable = L_prServicioListarCuentasServicioGeneral(dtCuentaPadres.Rows(j).Item("cacta"), gi_empresaNumi, 1, cbSucursal.Value)
                For i As Integer = 0 To dtCuentaServ.Rows.Count - 1 Step 1
                    Dim numiCuentaHijo As Integer = dtCuentaServ.Rows(i).Item("canumi")
                    Dim dttotc As DataTable
                    If (dtCuentaServ.Rows(i).Item("seest") = 1) Then

                        dttotc = L_prObtenerTotalPorCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"), dtCuentaServ.Rows(i).Item("seref"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                        If (dttotc.Rows(0).Item("total") > 0) Then
                            Dim dtnombre As DataTable = L_prObtenerNombreCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"))
                            If (dtnombre.Rows.Count > 0) Then

                                If (numeroCuenta <> dtCuentaServ.Rows(i).Item("senrocuenta")) Then
                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))

                                    dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Escuela, 0)
                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                                    Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad")
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, "POR INGRESO DE " + Str(cantidad) + " " + dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Escuela, Linea)

                                Else

                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                                    Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad")
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, "POR INGRESO DE " + Str(cantidad) + " " + dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Escuela, Linea)
                                End If



                            End If


                        End If

                    End If
                Next
            Next

        End If

    End Sub

    Public Sub _prArmarSocios(ByRef dt As DataTable)
        Dim totalDescuento As Double
        Dim cant As Integer = 0
        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalServiciosLavadero(2, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  '''3=Lavadero  Ok
        If (dtServicioTotal.Rows(0).Item("total") > 0) Then
            Dim dtCuentaPadres As DataTable = L_prListarCuentasServicioGeneral(gi_empresaNumi, 2, cbSucursal.Value)

            Dim numeroCuenta As Integer = 0
            For j As Integer = 0 To dtCuentaPadres.Rows.Count - 1 Step 1
                Dim numiCuenta As Integer = dtCuentaPadres.Rows(j).Item("canumi")
                dt.Rows.Add(numiCuenta, dtCuentaPadres.Rows(j).Item("cacta"), dtCuentaPadres.Rows(j).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)

                Dim dtCuentaServ As DataTable = L_prServicioListarCuentasServicioGeneralSocio(dtCuentaPadres.Rows(j).Item("cacta"), gi_empresaNumi, 2, cbSucursal.Value)
                For i As Integer = 0 To dtCuentaServ.Rows.Count - 1 Step 1
                    Dim numiCuentaHijo As Integer = dtCuentaServ.Rows(i).Item("canumi")
                    Dim dttotc As DataTable
                    If (dtCuentaServ.Rows(i).Item("seest") = 1) Then

                        dttotc = L_prObtenerTotalPorCuentaAdministracion(dtCuentaServ.Rows(i).Item("senrocuenta"), dtCuentaServ.Rows(i).Item("seref"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value, 2)
                        If (dttotc.Rows(0).Item("total") > 0) Then
                            Dim dtnombre As DataTable = L_prObtenerNombreCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"))
                            If (dtnombre.Rows.Count > 0) Then
                                If (numeroCuenta <> dtCuentaServ.Rows(i).Item("senrocuenta")) Then

                                    Dim servicio As Integer = dtCuentaServ.Rows(i).Item("senumiserv")

                                    'pregunto si la cuenta es mayor a 2
                                    If servicio > 2 Then 'lo trato como una cuenta normal,no como marco
                                        If dtCuentaServ.Rows(i).Item("sefactu") = 1 Then 'dtCuentaServ.Rows(i).Item("senumiserv") = 1
                                            totalDescuento = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))
                                        Else
                                            totalDescuento = dttotc.Rows(0).Item("total")
                                        End If


                                        Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad")

                                        'AUMENTADO DANNY''''''''''''''
                                        'Dim dtLicenciaInternacional As DataTable = L_prIntegracionObtenerCuentaLicenciaInternacional()
                                        'totalDescuento = totalDescuento - dtLicenciaInternacional.Rows(0).Item("esmonto") * cantidad
                                        '''''''''''''''''''''''''
                                        dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)
                                        numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                        Linea = Linea + 1
                                        Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                                        ''Dim cantidad As Integer = dttotc.Rows(0).Item("cantidad") ''movido por danny unas filas arriba
                                        dt.Rows.Add(numiCuentaHijo, DBNull.Value, "POR INGRESO DE " + Str(cantidad) + " " + dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)
                                        'Lb_efec.Text = Lb_efec.Text + totales
                                    Else 'lo trato como normalmente lo trataba marco
                                        dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)

                                        Dim dt_Socios As DataTable = L_prListarSociosCuotas(dtCuentaServ.Rows(i).Item("senrocuenta"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value) '''Lista

                                        For k As Integer = 0 To dt_Socios.Rows.Count - 1 Step 1
                                            totalDescuento = (dt_Socios.Rows(k).Item("total") - (dt_Socios.Rows(k).Item("total") * 0.13))

                                            If (servicio = 1) Then
                                                Dim _detallePagos As DataTable = L_prListarPagosSocioss(dt_Socios.Rows(k).Item("vcnumi"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                                If (_detallePagos.Rows.Count > 1) Then
                                                    Linea = Linea + 1
                                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                                    Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)

                                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, dt_Socios.Rows(k).Item("socio") + " CTAS " + _detallePagos.Rows(0).Item("mes") + " A " + _detallePagos.Rows(_detallePagos.Rows.Count - 1).Item("mes") + " / " + _detallePagos.Rows(_detallePagos.Rows.Count - 1).Item("seano").ToString, DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)

                                                Else
                                                    If (_detallePagos.Rows.Count = 1) Then
                                                        Linea = Linea + 1
                                                        Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                                        Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)
                                                        numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                                        dt.Rows.Add(numiCuentaHijo, DBNull.Value, dt_Socios.Rows(k).Item("socio") + " CTAS " + _detallePagos.Rows(_detallePagos.Rows.Count - 1).Item("mes") + " / " + _detallePagos.Rows(_detallePagos.Rows.Count - 1).Item("seano").ToString, DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)

                                                    End If
                                                End If
                                            End If

                                            If (servicio = 2) Then
                                                Dim _detallePagos As DataTable = L_prListarPagosSociossCuotaMourtotia(dt_Socios.Rows(k).Item("vcnumi"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value) ''Ok LISTA
                                                If (_detallePagos.Rows.Count > 1) Then
                                                    Linea = Linea + 1
                                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                                    Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)
                                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, dt_Socios.Rows(k).Item("socio") + " CTAS MORT GEST /" + _detallePagos.Rows(0).Item("sfgestion").ToString + " A " + _detallePagos.Rows(_detallePagos.Rows.Count - 1).Item("sfgestion").ToString, DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)

                                                Else

                                                    If (_detallePagos.Rows.Count = 1) Then
                                                        cant += 1
                                                        Dim val As Decimal = Round(to3Decimales((dt_Socios.Rows(k).Item("total") * 0.13)), 2)
                                                        totalDescuento = (dt_Socios.Rows(k).Item("total") - val)
                                                        If (cant Mod 2 = 0) Then
                                                            totalDescuento = totalDescuento - 0.01
                                                        End If



                                                        Linea = Linea + 1
                                                        numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                                        Dim totales As Double = Round(to3Decimales(totalDescuento), 3)
                                                        Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)
                                                        dt.Rows.Add(numiCuentaHijo, DBNull.Value, dt_Socios.Rows(k).Item("socio") + " CTAS MORT GEST /" + _detallePagos.Rows(0).Item("sfgestion").ToString, DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)
                                                    End If


                                                End If
                                            End If

                                        Next
                                    End If


                                End If



                            End If


                        End If

                    End If
                Next
            Next

        End If

    End Sub
    Public Sub _prArmarRemolque(ByRef dt As DataTable)
        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalServiciosLavadero(4, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  '''3=Lavadero
        If (dtServicioTotal.Rows(0).Item("total") > 0) Then
            Dim dtCuentaPadres As DataTable = L_prListarCuentasServicioGeneral(gi_empresaNumi, 4, cbSucursal.Value)
            Dim numeroCuenta As Integer = 0
            For j As Integer = 0 To dtCuentaPadres.Rows.Count - 1 Step 1
                Dim numiCuenta As Integer = dtCuentaPadres.Rows(j).Item("canumi")
                dt.Rows.Add(numiCuenta, dtCuentaPadres.Rows(j).Item("cacta"), dtCuentaPadres.Rows(j).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Remolque, 0)

                Dim dtCuentaServ As DataTable = L_prServicioListarCuentasServicioGeneral(dtCuentaPadres.Rows(j).Item("cacta"), gi_empresaNumi, 4, cbSucursal.Value)
                For i As Integer = 0 To dtCuentaServ.Rows.Count - 1 Step 1
                    Dim numiCuentaHijo As Integer = dtCuentaServ.Rows(i).Item("canumi")
                    Dim dttotc As DataTable
                    If (dtCuentaServ.Rows(i).Item("seest") = 1) Then

                        dttotc = L_prObtenerTotalPorCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"), dtCuentaServ.Rows(i).Item("seref"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                        If (dttotc.Rows(0).Item("total") > 0) Then
                            Dim dtnombre As DataTable = L_prObtenerNombreCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"))
                            If (dtnombre.Rows.Count > 0) Then

                                If (numeroCuenta <> dtCuentaServ.Rows(i).Item("senrocuenta")) Then
                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))

                                    dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Remolque, 0)
                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Remolque, Linea)

                                Else

                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - Round(to3Decimales((dttotc.Rows(0).Item("total") * 0.13)), 2))
                                    Linea = Linea + 1
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Remolque, Linea)
                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                End If



                            End If


                        End If

                    End If
                Next
            Next

        End If

    End Sub
    Public Sub _prArmarLavadero(ByRef dt As DataTable)
        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalServiciosLavadero(3, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  '''3=Lavadero
        If (dtServicioTotal.Rows(0).Item("total") > 0) Then
            'cuenta.canumi , cuenta.cacta As nro, cuenta.cadesc, b.chporcen, b.chdebe, b.chhaber, cast(null As Decimal (18, 2)) as debe,cast(null as decimal (18,2)) as haber
            Dim dtCuentaPadres As DataTable = L_prListarCuentasServicioLavadero(gi_empresaNumi)
            Dim numeroCuenta As Integer = 0
            For j As Integer = 0 To dtCuentaPadres.Rows.Count - 1 Step 1
                Dim numiCuenta As Integer = dtCuentaPadres.Rows(j).Item("canumi")
                dt.Rows.Add(numiCuenta, dtCuentaPadres.Rows(j).Item("cacta"), dtCuentaPadres.Rows(j).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Lavadero, 0)

                Dim dtCuentaServ As DataTable = L_prServicioListarCuentasServicioLavadero(dtCuentaPadres.Rows(j).Item("cacta"), gi_empresaNumi)

                For i As Integer = 0 To dtCuentaServ.Rows.Count - 1 Step 1
                    Dim numiCuentaHijo As Integer = dtCuentaServ.Rows(i).Item("canumi")
                    Dim dttotc As DataTable
                    If (dtCuentaServ.Rows(i).Item("seest") = 1) Then

                        dttotc = L_prObtenerTotalPorCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"), dtCuentaServ.Rows(i).Item("seref"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                        If (dttotc.Rows(0).Item("total") > 0) Then

                            Dim dtnombre As DataTable = L_prObtenerNombreCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"))
                            If (dtnombre.Rows.Count > 0) Then

                                If (numeroCuenta <> dtCuentaServ.Rows(i).Item("senrocuenta")) Then

                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - (dttotc.Rows(0).Item("total") * 0.13))
                                    Linea = Linea + 1
                                    dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Lavadero, 0)

                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)

                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Lavadero, Linea)
                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                Else
                                    Linea = Linea + 1
                                    numeroCuenta = dtCuentaServ.Rows(i).Item("senrocuenta")
                                    Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - (dttotc.Rows(0).Item("total") * 0.13))
                                    Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    dt.Rows.Add(numiCuentaHijo, DBNull.Value, dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Lavadero, Linea)
                                End If
                            End If
                        End If
                    Else
                        dttotc = L_prObtenerTotalPorCuentaProductos(dtCuentaServ.Rows(i).Item("senrocuenta"), dtCuentaServ.Rows(i).Item("seref"), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                        If (dttotc.Rows(0).Item("total") > 0) Then
                            Dim dtnombre As DataTable = L_prObtenerNombreCuenta(dtCuentaServ.Rows(i).Item("senrocuenta"))
                            If (dtnombre.Rows.Count > 0) Then

                                Dim totalDescuento As Double = (dttotc.Rows(0).Item("total") - (dttotc.Rows(0).Item("total") * 0.13))
                                dt.Rows.Add(numiCuentaHijo, dtCuentaServ.Rows(i).Item("senrocuenta"), dtnombre.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Lavadero, 0)
                                Linea = Linea + 1
                                Dim totales As Double = Round(to3Decimales(totalDescuento), 2)
                                Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                dt.Rows.Add(numiCuentaHijo, DBNull.Value, dtCuentaServ.Rows(i).Item("seref") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Lavadero, Linea)
                            End If


                        End If

                    End If


                Next
            Next
        End If


    End Sub
    Sub _prCargarNumiVentas(_categoria As Integer)

        Dim dtServicioTotal As DataTable = L_prServicioObtenerNumiPorCategoria(_categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
        If (dtServicioTotal.Rows.Count > 0) Then
            _ListVentas.Merge(dtServicioTotal)
        End If


    End Sub

    Private Sub _prCargarTablaComprobantes()
        Dim k As Integer
        Dim dt As New DataTable
        dt = L_prServicioListarCuentas()  ''Ok
        Dim tabla As DataTable = dt.Copy
        tabla.Rows.Clear()
        Dim dtServicios As New DataTable
        dtServicios = L_prlistarCategoriasActivos() ''Ok
        Dim BanderaCuentaPorCobrar As Boolean = False
        Dim contador As Integer = 0 ''Contador para sacar los numi de las ventas 
        For i As Integer = 0 To dt.Rows.Count - 1

            ''canumi , nro,cadesc ,chporcen,chdebe ,chhaber 
            Dim porcentaje As Double = dt.Rows(i).Item("chporcen")

            Dim dtcc = L_prServicioBuscarPadreCuenta(dt.Rows(i).Item("canumi"))
            If (dtcc.Rows.Count > 0) Then
                tabla.ImportRow(dtcc.Rows(0))
            End If
            tabla.ImportRow(dt.Rows(i))
            Dim numiCuenta As Integer = dt.Rows(i).Item("canumi")
            contador += 1
            Dim numicuentaatc As Integer = 21 'ATC
            Dim Numicuentatran As Integer

            If (porcentaje > 0 And dt.Rows(i).Item("chdebe")) Then

                For j As Integer = 0 To dtServicios.Rows.Count - 1 Step 1
                    Dim categoria As Integer = dtServicios.Rows(j).Item("cenum")
                    Dim dtServicioTotal As DataTable
                    If porcentaje = 3 Then 'porcentaje = 3 CODIGO DANNY PARA SEPARAR TOTALES PARA IT
                        dtServicioTotal = L_prServicioObtenerTotalPorCategoria(categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  ''Ok
                    Else
                        dtServicioTotal = L_prServicioObtenerTotalPorCategoriaTodosConRecibo(categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  ''Ok
                    End If
                    If (dtServicioTotal.Rows.Count > 0) Then
                        If (contador = 1) Then
                            _prCargarNumiVentas(categoria)
                        End If
                        Dim total As Double = dtServicioTotal.Rows(0).Item("total")
                        If (total > 0) Then
                            If (porcentaje = 3) Then

                                If (categoria = 1) Then  ''''Servicios Escuela 
                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    Linea = Linea + 1
                                    tabla.Rows.Add(numiCuenta, DBNull.Value, "IT " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                End If
                                If (categoria = 2) Then   ''''Pagos de Socios
                                    Linea = Linea + 1
                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    tabla.Rows.Add(numiCuenta, DBNull.Value, "IT ADMINISTRACION SOCIOS" + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                End If
                                If (categoria = 3) Then   '''Servicios Lavadero
                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    Linea = Linea + 1
                                    tabla.Rows.Add(numiCuenta, DBNull.Value, "IT " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Lavadero, Linea)
                                End If
                                If (categoria = 4) Then  '''Servicios Remolque
                                    Linea = Linea + 1
                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)

                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    tabla.Rows.Add(numiCuenta, DBNull.Value, "IT " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Remolque, Linea)
                                End If
                                If (categoria = -10) Then  '''Servicios Cabana
                                    Linea = Linea + 1
                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)

                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    tabla.Rows.Add(numiCuenta, DBNull.Value, "IT " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Hotel, Linea)
                                End If
                                If (categoria = 6) Then   '''Servicios Certificacion
                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    Linea = Linea + 1
                                    tabla.Rows.Add(numiCuenta, DBNull.Value, "IT " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Certificacion, Linea)
                                End If
                                If (categoria = 7) Then   '''Servicios ADMINISTRACION, ACA SACA EL IT,OJO
                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    Linea = Linea + 1
                                    tabla.Rows.Add(numiCuenta, DBNull.Value, "IT " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                End If
                            Else
                                BanderaCuentaPorCobrar = True
                                Dim dtserviciostotalcuentacobrar As DataTable 'Dim dtServiciosTotalCuentaCobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                'total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                If (categoria = 1) Then ''''''''Escuela
                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'Efectivo

                                        Linea = Linea + 1
                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        '''''Variable Multiproposito de Lavadero
                                        tabla.Rows.Add(numiCuenta, DBNull.Value, "POR INGRESO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Lb_efec.Text = Lb_efec.Text + totales
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(3, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'ATC

                                        Linea = Linea + 1
                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        '''''Variable Multiproposito de Lavadero
                                        tabla.Rows.Add(numicuentaatc, 11110301, "POR INGRESO ATC DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrartra(4, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows.Count

                                    If total > 0 Then  'TRANSFERENCIA
                                        For k = 0 To dtserviciostotalcuentacobrar.Rows.Count - 1
                                            total = dtserviciostotalcuentacobrar.Rows(k).Item("total")
                                            numiCuenta = dtserviciostotalcuentacobrar.Rows(k).Item("canumi")
                                            Numicuentatran = dtserviciostotalcuentacobrar.Rows(k).Item("cacuenta")
                                            Linea = Linea + 1
                                            Dim conversion As Double = (total * (porcentaje / 100))
                                            conversion = to3Decimales(conversion)
                                            Dim totales As Double = Round(conversion, 2)
                                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                            '''''Variable Multiproposito de Lavadero
                                            tabla.Rows.Add(numiCuenta, Numicuentatran, "POR INGRESO TRASPASO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Next
                                    End If

                                End If

                                If (categoria = 2) Then '''Categoria de Administracion pagos de socios y cuotas mourtorias
                                    '''''Variable Multiproposito de Administracion
                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'Efectivo

                                        Linea = Linea + 1
                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numiCuenta, DBNull.Value, "POR INGRESO ADMINISTRACION Y SOCIOS" + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                        Lb_efec.Text = Lb_efec.Text + totales
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(3, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'ATC

                                        Linea = Linea + 1
                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numicuentaatc, 11110301, "POR INGRESO ATC ADMINISTRACION Y SOCIOS" + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrartra(4, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows.Count

                                    If total > 0 Then  'TRANSFERENCIA
                                        For k = 0 To dtserviciostotalcuentacobrar.Rows.Count - 1
                                            total = dtserviciostotalcuentacobrar.Rows(k).Item("total")
                                            numiCuenta = dtserviciostotalcuentacobrar.Rows(k).Item("canumi")
                                            Numicuentatran = dtserviciostotalcuentacobrar.Rows(k).Item("cacuenta")
                                            Linea = Linea + 1
                                            Dim conversion As Double = (total * (porcentaje / 100))
                                            conversion = to3Decimales(conversion)
                                            Dim totales As Double = Round(conversion, 2)
                                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                            '''''Variable Multiproposito de Lavadero
                                            tabla.Rows.Add(numiCuenta, Numicuentatran, "POR INGRESO TRASPASO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Next
                                    End If
                                End If

                                If (categoria = 3) Then  '''Lavadero
                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'Efectivo

                                        Linea = Linea + 1
                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        '''''Variable Multiproposito de Lavadero
                                        tabla.Rows.Add(numiCuenta, DBNull.Value, "POR INGRESO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Lavadero, Linea)
                                        Lb_efec.Text = Lb_efec.Text + totales
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(3, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'ATC

                                        Linea = Linea + 1
                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        '''''Variable Multiproposito de Lavadero
                                        tabla.Rows.Add(numicuentaatc, 11110301, "POR INGRESO ATC DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Lavadero, Linea)
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrartra(4, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows.Count

                                    If total > 0 Then  'TRANSFERENCIA
                                        For k = 0 To dtserviciostotalcuentacobrar.Rows.Count - 1
                                            total = dtserviciostotalcuentacobrar.Rows(k).Item("total")
                                            numiCuenta = dtserviciostotalcuentacobrar.Rows(k).Item("canumi")
                                            Numicuentatran = dtserviciostotalcuentacobrar.Rows(k).Item("cacuenta")
                                            Linea = Linea + 1
                                            Dim conversion As Double = (total * (porcentaje / 100))
                                            conversion = to3Decimales(conversion)
                                            Dim totales As Double = Round(conversion, 2)
                                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                            '''''Variable Multiproposito de Lavadero
                                            tabla.Rows.Add(numiCuenta, Numicuentatran, "POR INGRESO TRASPASO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Next
                                    End If
                                End If
                                If (categoria = 4) Then '''''Variable Multiproposito de Remolque
                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'Efectivo


                                        Linea = Linea + 1

                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numiCuenta, DBNull.Value, "POR INGRESO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Remolque, Linea)
                                        Lb_efec.Text = Lb_efec.Text + totales
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(3, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'ATC


                                        Linea = Linea + 1

                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numicuentaatc, 11110301, "POR INGRESO ATC DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Remolque, Linea)
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrartra(4, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows.Count

                                    If total > 0 Then  'TRANSFERENCIA
                                        For k = 0 To dtserviciostotalcuentacobrar.Rows.Count - 1
                                            total = dtserviciostotalcuentacobrar.Rows(k).Item("total")
                                            numiCuenta = dtserviciostotalcuentacobrar.Rows(k).Item("canumi")
                                            Numicuentatran = dtserviciostotalcuentacobrar.Rows(k).Item("cacuenta")
                                            Linea = Linea + 1
                                            Dim conversion As Double = (total * (porcentaje / 100))
                                            conversion = to3Decimales(conversion)
                                            Dim totales As Double = Round(conversion, 2)
                                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                            '''''Variable Multiproposito de Lavadero
                                            tabla.Rows.Add(numiCuenta, Numicuentatran, "POR INGRESO TRASPASO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Next
                                    End If
                                End If

                                If (categoria = -10) Then '''''Variable Multiproposito de Cabañas
                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'Efectivo


                                        Linea = Linea + 1

                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numiCuenta, DBNull.Value, "POR INGRESO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Hotel, Linea)
                                        Lb_efec.Text = Lb_efec.Text + totales
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(3, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'Efectivo


                                        Linea = Linea + 1

                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numicuentaatc, 11110301, "POR INGRESO ATC DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Hotel, Linea)
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrartra(4, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows.Count

                                    If total > 0 Then  'TRANSFERENCIA
                                        For k = 0 To dtserviciostotalcuentacobrar.Rows.Count - 1
                                            total = dtserviciostotalcuentacobrar.Rows(k).Item("total")
                                            numiCuenta = dtserviciostotalcuentacobrar.Rows(k).Item("canumi")
                                            Numicuentatran = dtserviciostotalcuentacobrar.Rows(k).Item("cacuenta")
                                            Linea = Linea + 1
                                            Dim conversion As Double = (total * (porcentaje / 100))
                                            conversion = to3Decimales(conversion)
                                            Dim totales As Double = Round(conversion, 2)
                                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                            '''''Variable Multiproposito de Lavadero
                                            tabla.Rows.Add(numiCuenta, Numicuentatran, "POR INGRESO TRASPASO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Next
                                    End If
                                End If

                                If (categoria = 6) Then '''''Variable Multiproposito de 
                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'Efectivo


                                        Linea = Linea + 1

                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numiCuenta, DBNull.Value, "POR INGRESO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Certificacion, Linea)
                                        Lb_efec.Text = Lb_efec.Text + totales
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(3, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    If total > 0 Then  'ATC

                                        Linea = Linea + 1

                                        Dim conversion As Double = (total * (porcentaje / 100))
                                        conversion = to3Decimales(conversion)
                                        Dim totales As Double = Round(conversion, 2)
                                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                        tabla.Rows.Add(numicuentaatc, 11110301, "POR INGRESO ATC DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Certificacion, Linea)
                                    End If

                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrartra(4, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows.Count

                                    If total > 0 Then  'TRANSFERENCIA
                                        For k = 0 To dtserviciostotalcuentacobrar.Rows.Count - 1
                                            total = dtserviciostotalcuentacobrar.Rows(k).Item("total")
                                            numiCuenta = dtserviciostotalcuentacobrar.Rows(k).Item("canumi")
                                            Numicuentatran = dtserviciostotalcuentacobrar.Rows(k).Item("cacuenta")
                                            Linea = Linea + 1
                                            Dim conversion As Double = (total * (porcentaje / 100))
                                            conversion = to3Decimales(conversion)
                                            Dim totales As Double = Round(conversion, 2)
                                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                            '''''Variable Multiproposito de Lavadero
                                            tabla.Rows.Add(numiCuenta, Numicuentatran, "POR INGRESO TRASPASO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Next
                                    End If
                                End If
                                If (categoria = 7) Then '''''Variable Multiproposito de 'OJO DANNY aca ingresan por administracion
                                    '-----------CODIGO DANNY
                                    dtserviciostotalcuentacobrar = L_prServicioObtenerTotalPorCategoriaCuentasCobrar(1, categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                    total = dtserviciostotalcuentacobrar.Rows(0).Item("total")

                                    Dim numiCuentaLicInter As Integer = 23
                                    Dim cuentaLicInter As String = "BCO. UNION CTA. CTE. Nº  1-7621746"
                                    Dim nroCuenta As String = "11110303"

                                    Dim numiCuentaLicInter2 As Integer = 3362
                                    Dim cuentaLicInter2 As String = "GASTOS LICENCIAS INTERNACIONALES "
                                    Dim nroCuenta2 As String = "52211901"

                                    Dim cantidad As Integer = dtserviciostotalcuentacobrar.Rows(0).Item("cantidad")
                                    '--------------

                                    Linea = Linea + 1

                                    Dim conversion As Double = (total * (porcentaje / 100))
                                    conversion = to3Decimales(conversion)
                                    Dim totales As Double = Round(conversion, 2)
                                    Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)

                                    'tabla.Rows.Add(numiCuenta, DBNull.Value, "POR INGRESO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                    '----codigo Danny
                                    totales = 388 * cantidad
                                    TotalSus = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    tabla.Rows.Add(numiCuentaLicInter, nroCuenta, "POR INGRESO DE " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                    'Lb_efec.Text = Lb_efec.Text + totales

                                    Linea = Linea + 1
                                    totales = 212 * cantidad
                                    TotalSus = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                                    tabla.Rows.Add(numiCuentaLicInter2, nroCuenta2, "GASTO POR " + dtServicios.Rows(j).Item("cedesc1") + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                    'Lb_efec.Text = Lb_efec.Text + totales
                                    '----------
                                End If

                            End If

                        End If
                    End If


                Next
                Lb_Saldo.Text = Lb_efec.Text - Lb_Banco.Text
                If Lb_Saldo.Text <> 0 Then
                    Lb_Saldo.ForeColor = Color.Red
                    LabelX7.ForeColor = Color.Red
                    btnGrabar.Enabled = False
                Else
                    Lb_Saldo.ForeColor = Color.Black
                    LabelX7.ForeColor = Color.Black
                    btnGrabar.Enabled = True
                End If

                If (BanderaCuentaPorCobrar = True) Then  ''''''este booleano =true me sirve para poner una sola ves en el haber las cuentas por cobrar


                    ''''CUENTAS POR COBRAR ''''''''''''''''

                    Dim DtNumiCuentaPorCobrar As DataTable = L_prNumiCuentaCobrar()
                    tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuentapadre"), DtNumiCuentaPorCobrar.Rows(0).Item("nropadre"), DtNumiCuentaPorCobrar.Rows(0).Item("descripcionpadre"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, 0, 0)

                    tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DtNumiCuentaPorCobrar.Rows(0).Item("nro"), DtNumiCuentaPorCobrar.Rows(0).Item("descripcion"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, 0, 0)

                    For j As Integer = 0 To dtServicios.Rows.Count - 1 Step 1
                        Dim categoria As Integer = dtServicios.Rows(j).Item("cenum")
                        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalPorCategoriaClientePorCobrar(categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)  ''Ok


                        If (dtServicioTotal.Rows.Count > 0) Then

                            Dim total As Double = dtServicioTotal.Rows(0).Item("total")
                            If (total > 0) Then
                                Dim table As DataTable = L_prListarClientePorCobrarPorSector(categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                                Dim CatDiferente As Integer = -1
                                For k = 0 To table.Rows.Count - 1

                                    Linea += 1
                                    Dim totales As Double = Round(to3Decimales(table.Rows(k).Item("total")), 2)
                                    Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)

                                    If (categoria = -10) Then
                                        If (CatDiferente <> categoria) Then
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, "CABAÑAS", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Hotel, Linea)
                                            Linea += 1
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Hotel, Linea)
                                        Else

                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Hotel, Linea)
                                        End If

                                    End If
                                    If (categoria = 1) Then

                                        If (CatDiferente <> categoria) Then
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, "ESCUELA", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Escuela, Linea)
                                            Linea += 1
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        Else
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                                        End If

                                    End If
                                    If (categoria = 2) Then

                                        If (CatDiferente <> categoria) Then
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, "ADMINISTRACION SOCIOS", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, Linea)
                                            Linea += 1
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                        Else
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                        End If

                                    End If
                                    If (categoria = 3) Then
                                        If (CatDiferente <> categoria) Then
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, "LAVADERO", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Lavadero, Linea)
                                            Linea += 1
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Lavadero, Linea)

                                        Else
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Lavadero, Linea)
                                        End If

                                    End If
                                    If (categoria = 4) Then
                                        If (CatDiferente <> categoria) Then
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, "REMOLQUE", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Remolque, Linea)
                                            Linea += 1
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Remolque, Linea)

                                        Else
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Remolque, Linea)
                                        End If

                                    End If
                                    If (categoria = 6) Then
                                        If (CatDiferente <> categoria) Then
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, "CERTIFICACION", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Certificacion, Linea)
                                            Linea += 1
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Certificacion, Linea)

                                        Else
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Certificacion, Linea)
                                        End If

                                    End If
                                    If (categoria = 7) Then
                                        If (CatDiferente <> categoria) Then
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, "ADMINISTRACION", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, Linea)
                                            Linea += 1
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                        Else
                                            tabla.Rows.Add(DtNumiCuentaPorCobrar.Rows(0).Item("cuenta"), DBNull.Value, table.Rows(k).Item("cjnombre"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Administracion, Linea)
                                        End If

                                    End If

                                    CatDiferente = categoria
                                Next

                            End If

                        End If



                    Next
                    BanderaCuentaPorCobrar = False
                End If
            Else
                If (porcentaje > 0 And dt.Rows(i).Item("chhaber")) Then
                    Dim SubTotal As Double = 0
                    For j As Integer = 0 To dtServicios.Rows.Count - 1 Step 1
                        Dim categoria As Integer = dtServicios.Rows(j).Item("cenum")
                        Dim dtServicioTotal As DataTable = L_prServicioObtenerTotalPorCategoria(categoria, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
                        Dim total As Double = dtServicioTotal.Rows(0).Item("total")


                        SubTotal = SubTotal + (total * (porcentaje / 100))



                    Next
                    If (porcentaje = 13) Then
                        Linea = Linea + 1
                        Dim totales As Double = Round(to3Decimales(SubTotal), 2)
                        Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)

                        tabla.Rows.Add(numiCuenta, DBNull.Value, "IVA DF INGRESOS DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)
                    Else
                        Linea = Linea + 1
                        Dim totales As Double = Round(to3Decimales(SubTotal), 2)
                        Dim TotalSus As Double = Round((totales / (tbTipoCambio.Value)), 2)
                        tabla.Rows.Add(numiCuenta, DBNull.Value, "IT POR PAGAR INGRESOS DEL " + tbFechaI.Value.ToString("dd/MM/yyyy") + " AL " + tbFechaF.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Administracion, Linea)
                    End If

                End If
            End If

        Next

        _prArmarSocios(tabla)
        _prArmarLavadero(tabla)
        _prArmarRemolque(tabla)
        _prArmarEscuela(tabla)
        _prArmarCertificacion(tabla)
        _prArmarCabanas(tabla)
        _prArmarAdministracionn(tabla)
        _prArmarCuadre(tabla)

        ''canumi , nro, cadesc, chporcen, chdebe, chhaber 
        grComprobante.DataSource = tabla
        grComprobante.RetrieveStructure()


        Dim dtt As DataTable = _ListVentas

        With grComprobante.RootTable.Columns("canumi")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("variable")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("linea")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("nro")
            .Width = 120
            .Caption = "NRO CUENTA"
            .Visible = True
        End With
        With grComprobante.RootTable.Columns("cadesc")
            .Width = 580
            .Caption = "DESCRIPCION"
            .Visible = True
        End With
        With grComprobante.RootTable.Columns("chporcen")
            .Width = 100

            .Visible = False
        End With
        With grComprobante.RootTable.Columns("chdebe")
            .Width = 180
            .Caption = "DEBE"
            .Visible = False
        End With
        With grComprobante.RootTable.Columns("chhaber")
            .Width = 180
            .Caption = "HABER"
            .Visible = False
        End With
        With grComprobante.RootTable.Columns("tc")
            .Width = 70
            .Caption = "TC"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grComprobante.RootTable.Columns("debe")
            .Width = 100
            .Caption = "DEBE BS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grComprobante.RootTable.Columns("haber")
            .Width = 100
            .Caption = "HABER BS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With

        With grComprobante.RootTable.Columns("debesus")
            .Width = 100
            .Caption = "DEBE SUS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grComprobante.RootTable.Columns("habersus")
            .Width = 100
            .Caption = "HABER SUS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grComprobante
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .TotalRow = InheritableBoolean.True

            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
        Dim aux As DataTable = CType(grComprobante.DataSource, DataTable)

        'Lb_efec.Text = IIf(IsDBNull(aux.Compute("Sum(debe)", "")), 0, aux.Compute("Sum(debe)", ""))
        'Lb_Saldo.Text = IIf(IsDBNull(aux.Compute("Sum(debe)", "")), 0, aux.Compute("Sum(debe)", ""))
        _prAplicarCondiccionJanus()
    End Sub
    Public Sub _prAplicarCondiccionJanusBanco()





        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grAsientoBanco.RootTable.Columns("tc"), ConditionOperator.Equal, DBNull.Value)

        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.FontSize = 9
        fc.FormatStyle.FontUnderline = TriState.True

        grAsientoBanco.RootTable.FormatConditions.Add(fc)

    End Sub
    Public Sub _prAplicarCondiccionJanus()





        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grComprobante.RootTable.Columns("tc"), ConditionOperator.Equal, DBNull.Value)

        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.FontSize = 9
        fc.FormatStyle.FontUnderline = TriState.True

        grComprobante.RootTable.FormatConditions.Add(fc)

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
    Private Sub _prInhabiliitar()
        tbNumi.ReadOnly = True
        btActualizar.Visible = False
        tbTipoCambio.IsInputReadOnly = True
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        PanelNavegacion.Enabled = True
        btnNuevoTipoCambio.Visible = False
        cbSucursal.ReadOnly = True
        conRedondeo = False

    End Sub
    Private Sub _prhabilitar()
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        'btnGrabar.Enabled = True
        cbSucursal.ReadOnly = False
        tbNumi.ReadOnly = True
        btActualizar.Visible = True
        btnNuevoTipoCambio.Visible = True
        tbTipoCambio.IsInputReadOnly = False
        tbNumi.Clear()

    End Sub

    Private Sub F1_ServicioCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub



    Public Function _fnVisualizarRegistros() As Boolean
        Return btnGrabar.Enabled = True
    End Function
    Public Function _ValidarCampos() As Boolean
        If (tbTipoCambio.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Coloque un Tipo de Cambio Valido".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbTipoCambio.Focus()
            Return False

        End If
        If (Lb_Saldo.Text <> 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Inserte Monto Correctos en los bancos", img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            grbanco.Focus()
            Return False

        End If
        If (grComprobante.RowCount <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "No Existen Datos Para Guardar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            grComprobante.Focus()
            Return False
        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "POR FAVOR SELECCIONE UNA SUCURSAL".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSucursal.Focus()
            Return False

        End If

        If conRedondeo = True Then
            'validar que el redondeo no sea mas alto que el permitido by DANNY
            'verificar si esta cuadrado
            'cargar datos globales
            grComprobante.MoveLast()
            Dim redonDebeBs As Double = IIf(IsDBNull(grComprobante.GetValue("debe")) = True, 0, grComprobante.GetValue("debe"))
            Dim redonDebeSus As Double = IIf(IsDBNull(grComprobante.GetValue("debesus")) = True, 0, grComprobante.GetValue("debesus"))
            Dim redonHaberBs As Double = IIf(IsDBNull(grComprobante.GetValue("haber")) = True, 0, grComprobante.GetValue("haber"))
            Dim redonHaberSus As Double = IIf(IsDBNull(grComprobante.GetValue("habersus")) = True, 0, grComprobante.GetValue("habersus"))


            If redonDebeBs > 0 Or redonDebeSus > 0 Or redonHaberBs > 0 Or redonHaberSus > 0 Then
                Dim dtGlobal As DataTable = L_prConfigGeneralEmpresa(1)
                Dim _difMaximaAjuste As Double
                If dtGlobal.Rows.Count > 0 Then
                    _difMaximaAjuste = dtGlobal.Rows(0).Item("cfdifmax")
                End If

                'Dim diferenciaBs As Double

                Dim diferencia As Double = redonDebeSus - redonHaberSus
                'verifico si la diferencia es para el debe
                If diferencia < 0 Then 'si es negativo la diferencia es para el haber
                    diferencia = diferencia * -1
                End If


                If diferencia > _difMaximaAjuste Then 'si es verdadero,pregunto sin desea hacer el ajuste automatico
                    Dim info As New TaskDialogInfo("advertencia".ToUpper, eTaskDialogIcon.Exclamation, "ajuste de cambio mayor al permitido, ¿desea grabar de todos modos?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.No, eTaskDialogBackgroundColor.Blue)
                    Dim result As eTaskDialogResult = TaskDialog.Show(info)
                    If result = eTaskDialogResult.Yes Then

                    Else
                        Return False

                    End If

                    'ToastNotification.Show(Me, "No se puede grabar la integracion por que el ajuste de cambio es mayor al permitido".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)

                End If

            End If


        End If




        Return True
    End Function

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbNumi.Text = String.Empty) Then
            _prGuardarModificado()
            Lb_efec.Text = 0
            Lb_Saldo.Text = 0
            Lb_Banco.Text = 0
        Else
            'If (tbCodigo.Text <> String.Empty) Then
            '    _prGuardarModificado()
            '    ''    _prInhabiliitar()

            'End If
        End If

        ''    _prInhabiliitar()
    End Sub
    Private Function to3Decimales(num As Double) As Double
        Dim res As Double = 0
        Dim numeroString As String = num.ToString()
        Dim posicionPuntoDecimal As Integer = numeroString.IndexOf(".")

        If posicionPuntoDecimal > 0 Then
            Dim cantidadDecimales As Integer = numeroString.Substring(posicionPuntoDecimal).Count - 1
            If cantidadDecimales >= 3 Then
                numeroString = numeroString.Substring(0, posicionPuntoDecimal + 4)
                res = Convert.ToDouble(numeroString)
            Else
                res = num
            End If

        Else
            res = num
        End If
        Return res
    End Function
    Private Sub _prGuardarModificado()
        's
        'L_prComprobanteGrabarIntegracion(ByRef _numi As String, _numDoc As String, _tipo As String, _anio As String, _mes As String, _num As String, _fecha As String, _tipoCambio As String, _glosa As String, _obs As String, _numiEmpresa As String, _detalle As DataTable, _ifnumi As String, _ifto001numi As Integer, _iftc As Double,
        '                                                    _iffechai As String, _iffechaf As String, _ifest As Integer

        'codigo danny*****************************
        Dim dtDetalle As DataTable = CType(grComprobante.DataSource, DataTable)
        Dim dtDetalle2 As DataTable = CType(grbanco.DataSource, DataTable)
        Dim Reg As DataRow

        dtDetalle2.Rows.Add()
        For Each fila As DataRow In dtDetalle.Rows
            If IsDBNull(fila.Item("debe")) = True Then
                fila.Item("debe") = 0
            End If
            If IsDBNull(fila.Item("haber")) = True Then
                fila.Item("haber") = 0
            End If
            If IsDBNull(fila.Item("debesus")) = True Then
                fila.Item("debesus") = 0
            End If
            If IsDBNull(fila.Item("habersus")) = True Then
                fila.Item("habersus") = 0
            End If
        Next
        '******************************************
        Dim numiComprobante As String = ""
        Dim res As Boolean = L_prComprobanteGrabarIntegracion(numiComprobante, "", 1, tbFechaI.Value.Year.ToString, tbFechaI.Value.Month.ToString, "", tbFechaI.Value.Date.ToString("yyyy/MM/dd"), tbTipoCambio.Value.ToString, "", "", gi_empresaNumi, dtDetalle, dtDetalle2, "", 0, tbTipoCambio.Value, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), 1, _ListVentas, cbSucursal.Value)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Asiento Contable fue generado Exitosamente".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            '' _prCargarTablaServicios()

            'imprimir el reporte de comprobante
            _prImprimirComprobante(numiComprobante)

            _prCargarMovimiento()


            _prInhabiliitar()
            If grmovimientos.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "Los codigos no pudieron ser modificados".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        btnEliminar.Enabled = False

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        _tab.Close()
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' a.ifnumi , a.ifto001numi, comprobante.oanumdoc, a.iftc, a.iffechai, a.iffechaf, a.ifest 
        With grmovimientos
            tbNumi.Text = .GetValue("ifnumi")
            tbFechaI.Value = .GetValue("iffechai")
            tbFechaF.Value = .GetValue("iffechaf")
            tbTipoCambio.Value = .GetValue("iftc")
            cbSucursal.Value = .GetValue("ifsuc")
        End With
        _prCargarBancosRegistrados(tbNumi.Text)
        _prCargarDetalleMovimiento(tbNumi.Text)
        _prCargarDetalleMovimientoBanco(tbNumi.Text)
        _prMostrarbancos(tbNumi.Text)
        LblPaginacion.Text = Str(grmovimientos.Row + 1) + "/" + grmovimientos.RowCount.ToString

    End Sub
    Private Sub _prMostrarbancos(_Numi As String)
        Dim dt As New DataTable
        'Dim fila As DataRow
        dt = L_prIntegracionBanco(_Numi)
        Lb_Banco.Text = 0
        Lb_efec.Text = 0
        Lb_Saldo.Text = 0
        If dt.Rows.Count > 0 Then
            For Each fila As DataRow In dt.Rows
                Lb_Banco.Text = Lb_Banco.Text + fila.Item("ikmonto")
            Next
            Lb_efec.Text = Lb_Banco.Text
        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = False Then
            _prInhabiliitar()
            If grmovimientos.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If
        Else

            _modulo.Select()
            _tab.Close()

        End If
    End Sub

    Sub _prLimpiar()
        conRedondeo = False
        tbTipoCambio.Value = 0
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        If (Not IsDBNull(grComprobante)) Then
            If (grComprobante.RowCount > 0) Then
                CType(grComprobante.DataSource, DataTable).Rows.Clear()
            End If

        End If
        _prCrearColumns()

    End Sub
    Sub _prCrearColumns()
        If (Not IsNothing(_ListVentas)) Then
            _ListVentas.Columns.Clear()
        End If
        _ListVentas = New DataTable
        _ListVentas.Columns.Add("vcnumi", Type.GetType("System.Int32"))

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _prhabilitar()
        _prLimpiar()
        _prCargarBancos()
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        btnEliminar.Enabled = False

        Dim dtTipoCambio As DataTable = L_prTipoCambioGeneralPorFecha(Now.ToString("yyyy/MM/dd"))
        If dtTipoCambio.Rows.Count = 0 Then
            _existTipoCambio = False
            tbTipoCambio.Value = 0
            tbTipoCambio.BackgroundStyle.BackColor = Color.Red
            btnNuevoTipoCambio.Visible = True
        Else
            _existTipoCambio = True
            tbTipoCambio.Value = dtTipoCambio.Rows(0).Item("cbdol")
            tbTipoCambio.BackgroundStyle.BackColor = Color.White
            MEP.SetError(tbTipoCambio, "")
            btnNuevoTipoCambio.Visible = False

        End If

        If (gb_userTodasSuc = False And CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False
        End If
    End Sub

    Private Sub btActualizar_Click(sender As Object, e As EventArgs) Handles btActualizar.Click
        Lb_efec.Text = 0
        Lb_Saldo.Text = 0
        Lb_Banco.Text = 0
        If (IsNothing(tbTipoCambio.Value) Or tbTipoCambio.ToString = String.Empty Or tbTipoCambio.Value = 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "NO HAY TIPO DE CAMBIO REGISTRADO. POR FAVOR REGISTRE EL TIPO DE CAMBIO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSucursal.Focus()
            Return

        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "POR FAVOR SELECCIONE UNA SUCURSAL".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSucursal.Focus()
            Return

        End If
        _prCrearColumns()

        _prCargarTablaComprobantes()

    End Sub

    Private Sub grComprobante_KeyDown(sender As Object, e As KeyEventArgs) Handles grComprobante.KeyDown
        If (e.KeyData = Keys.Enter) Then
            If (GrDatos.Visible = True) Then
                GrDatos.Visible = False
                GroupPanelBanco.Visible = False
                Panel2.Visible = False
            Else
                GrDatos.Visible = True
                GroupPanelBanco.Visible = True
                Panel2.Visible = True
            End If
        End If
    End Sub

    Private Sub btnNuevoTipoCambio_Click(sender As Object, e As EventArgs)
        _prAñadirTipoCambio()
    End Sub
    Private Sub _prAñadirTipoCambio()
        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, "btConfTipoCambio")

        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")

        If add = True Then
            Dim frm As New F0_TipoCambio_Nuevo
            frm.tbFecha.Value = tbFechaI.Value
            frm.ShowDialog()
            tbFechaI.Value = DateAdd(DateInterval.Day, -1, tbFechaI.Value)
            tbFechaI.Value = DateAdd(DateInterval.Day, 1, tbFechaI.Value)
        Else
            ToastNotification.Show(Me, "el usario no cuenta con los permisos para adicionar tipo de cambio".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        _prImprimir()
    End Sub
    Private Sub _prImprimir()
        Dim objrep As New R_ComprobanteIntegracion
        Dim dt As New DataTable
        dt = CType(grComprobante.DataSource, DataTable)

        'ahora lo mando al visualizador
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("fecha", tbFechaI.Value.ToString("dd/MM/yyyy"))
        objrep.SetParameterValue("tc", tbTipoCambio.Value)
        objrep.SetParameterValue("titulo", "COMPROBANTE DE INGRESO")
        objrep.SetParameterValue("titulo2", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
        objrep.SetParameterValue("glosa", cbSucursal.Text)
        'cargar el numero de comprobante
        Dim dtNum As DataTable = L_prObtenerNumFacturaGeneral(1, tbFechaI.Value.Year, tbFechaI.Value.Month, 1)
        If dtNum.Rows.Count > 0 Then
            objrep.SetParameterValue("numero", dtNum.Rows(0).Item("oanumdoc").ToString)
        Else
            objrep.SetParameterValue("numero", "")

        End If

        objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)


        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

    End Sub
    Private Sub _prImprimirBanco()
        Dim objrep As New R_ComprobanteIntegracion
        Dim dt As New DataTable
        dt = CType(grAsientoBanco.DataSource, DataTable)

        'ahora lo mando al visualizador
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("fecha", tbFechaI.Value.ToString("dd/MM/yyyy"))
        objrep.SetParameterValue("tc", tbTipoCambio.Value)
        objrep.SetParameterValue("titulo", "COMPROBANTE DE INGRESO")
        objrep.SetParameterValue("titulo2", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
        objrep.SetParameterValue("glosa", cbSucursal.Text)
        'cargar el numero de comprobante
        Dim dtNum As DataTable = L_prObtenerNumFacturaGeneral(1, tbFechaI.Value.Year, tbFechaI.Value.Month, 1)
        If dtNum.Rows.Count > 0 Then
            objrep.SetParameterValue("numero", dtNum.Rows(0).Item("oanumdoc").ToString)
        Else
            objrep.SetParameterValue("numero", "")

        End If

        objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)


        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

    End Sub
    Private Sub _prImprimirComprobante(numiComprobante)
        Dim objrep As New R_Comprobante
        Dim dt As New DataTable
        dt = L_prComprobanteReporteComprobante(numiComprobante)

        'ahora lo mando al visualizador
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("fechaDesde", "")
        objrep.SetParameterValue("fechaHasta", "")
        objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
        objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)
        objrep.SetParameterValue("ultimoRegistro", 0)

        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grmovimientos.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grmovimientos.Row = _MPos
        End If
    End Sub

    Private Sub grmovimientos_SelectionChanged(sender As Object, e As EventArgs) Handles grmovimientos.SelectionChanged
        If (grmovimientos.RowCount >= 0 And grmovimientos.Row >= 0) Then

            _prMostrarRegistro(grmovimientos.Row)
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grmovimientos.Row
        If _pos < grmovimientos.RowCount - 1 Then
            _pos = grmovimientos.Row + 1
            '' _prMostrarRegistro(_pos)
            grmovimientos.Row = _pos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grmovimientos.Row
        If grmovimientos.RowCount > 0 Then
            _pos = grmovimientos.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grmovimientos.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grmovimientos.Row
        If _MPos > 0 And grmovimientos.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grmovimientos.Row = _MPos
        End If
    End Sub

    Private Sub btnNuevoTipoCambio_Click_1(sender As Object, e As EventArgs) Handles btnNuevoTipoCambio.Click
        _prAñadirTipoCambio()
    End Sub

    Private Sub tbFechaI_ValueChanged(sender As Object, e As EventArgs) Handles tbFechaI.ValueChanged
        'verifico el tipo de cambio de la fecha elegida
        Dim dtTipoCambio As DataTable = L_prTipoCambioGeneralPorFecha(tbFechaI.Value.ToString("yyyy/MM/dd"))
        If dtTipoCambio.Rows.Count = 0 Then
            '_existTipoCambio = False
            tbTipoCambio.Value = Nothing
            tbTipoCambio.Text = ""
            tbTipoCambio.BackgroundStyle.BackColor = Color.Red
            btnNuevoTipoCambio.Visible = True
        Else
            '_existTipoCambio = True
            tbTipoCambio.Value = dtTipoCambio.Rows(0).Item("cbdol")
            tbTipoCambio.BackgroundStyle.BackColor = Color.White
            btnNuevoTipoCambio.Visible = False
            MEP.SetError(tbTipoCambio, "")


        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el asiento contable?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarAsientoContable(tbNumi.Text, mensajeError)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _prCargarMovimiento()


                _prInhabiliitar()
                If grmovimientos.RowCount > 0 Then

                    _prMostrarRegistro(0)

                End If


            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If

    End Sub

    Private Sub grbanco_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grbanco.EditingCell
        'MsgBox("hola")
        If (e.Column.Index = grbanco.RootTable.Columns("camonto").Index) Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub GrDatos_Click(sender As Object, e As EventArgs) Handles GrDatos.Click

    End Sub

    Private Sub grbanco_KeyDown(sender As Object, e As KeyEventArgs) Handles grbanco.KeyDown




    End Sub

    Private Sub grbanco_CurrentCellChanged(sender As Object, e As EventArgs) Handles grbanco.CurrentCellChanged
        Pr_sumarbanco()
    End Sub
    Private Sub Pr_sumarbanco()
        Dim dt As DataTable
        dt = grbanco.DataSource
        Lb_Banco.Text = 0
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            Lb_Banco.Text = Lb_Banco.Text + dt.Rows(i).Item(5)
        Next
        Lb_Saldo.Text = Lb_efec.Text - Lb_Banco.Text
        If Lb_Saldo.Text <> 0 Then
            Lb_Saldo.ForeColor = Color.Red
            LabelX7.ForeColor = Color.Red
            btnGrabar.Enabled = False
        Else
            Lb_Saldo.ForeColor = Color.Black
            LabelX7.ForeColor = Color.Black
            btnGrabar.Enabled = True
        End If

    End Sub

    Private Sub grbanco_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grbanco.KeyPress

    End Sub

    Private Sub btnbanco_Click(sender As Object, e As EventArgs) Handles btnbanco.Click
        _prImprimirBanco()
    End Sub
#End Region


End Class