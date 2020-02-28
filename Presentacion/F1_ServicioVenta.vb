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
Imports CrystalDecisions.Shared
Imports Datos
Imports Facturacion




Public Class F1_ServicioVenta
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim _Codbanco As Integer = 0
    Dim _CodVehiculo As Integer
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
    Dim NroCompronbante As Integer
    Dim _CodClienteCredito As Integer
    Dim CodigoControl As String
    Dim NroFactura As String
    Dim NroAutorizacion As String
    Dim NumiDosificacion As Integer
    Dim LimiteEmision As Date
    Dim BanderaServer As Boolean = True

    Dim _conFactura As Boolean = True
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _IniciarTodo()
        ''L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        Me.WindowState = FormWindowState.Maximized
        _prInhabiliitar()
        _prCargarComboAlmacen(cbSucursal)
        _prCargarComboSector(cbsector)
        _prCargarComboTipoVenta(swTipoVenta)
        P_prCargarComboEstadoFatura()
        _prCargarVenta()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "VENTA FACTURACION"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

        _prCargarComboEstados()

        _prAsignarPermisos()

    End Sub

    Private Sub _prCargarComboEstados()
        Dim dt As New DataTable
        dt.Columns.Add("numi", GetType(Integer))
        dt.Columns.Add("desc", GetType(String))

        dt.Rows.Add({1, "VALIDA"})
        dt.Rows.Add({0, "ANULADA"})
        dt.Rows.Add({2, "EXTRAVIADA"})
        dt.Rows.Add({3, "NO UTILIZADA"})
        dt.Rows.Add({4, "EMITIDA POR CONTINGENCIA"})

        With tbEstado
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("numi").Width = 60
            .DropDownList.Columns("numi").Caption = "COD"
            .DropDownList.Columns.Add("desc").Width = 200
            .DropDownList.Columns("desc").Caption = "DESCRIPCIÓN"
            .ValueMember = "numi"
            .DisplayMember = "desc"
            .DataSource = dt
            .Refresh()
        End With

    End Sub

    Private Sub _prCargarComboSector(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        If (BanderaServer = False) Then
            Return
        End If
        Dim dt As New DataTable
        dt = L_prlistarCategoriasActivos()
        If (dt.Columns.Count <= 0) Then
            Dim info As New TaskDialogInfo("INFORMACION", eTaskDialogIcon.Information2, "Error de Conexion con el Servidor. Desea Intentar Nuevamente?." + vbLf + "Si el error persite comuniquese con su administrador de sistemas", eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.DarkBlue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                _prCargarComboSector(mCombo)
            Else

                _prSalir()
                Return
            End If
        End If
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

    Private Sub _prCargarComboTipoVenta(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        If (BanderaServer = False) Then
            Return
        End If
        Dim dt As New DataTable
        dt = L_prlistarCategoriasTipoVenta()
        If (dt.Columns.Count <= 0) Then
            Dim info As New TaskDialogInfo("INFORMACION", eTaskDialogIcon.Information2, "Error de Conexion con el Servidor. Desea Intentar Nuevamente?." + vbLf + "Si el error persite comuniquese con su administrador de sistemas", eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.DarkBlue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                _prCargarComboTipoVenta(mCombo)
            Else

                _prSalir()
                Return
            End If
        End If
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cenum").Width = 60
            .DropDownList.Columns("cenum").Caption = "COD"
            .DropDownList.Columns.Add("cedesc1").Width = 500
            .DropDownList.Columns("cedesc1").Caption = "TIPO"
            .ValueMember = "cenum"
            .DisplayMember = "cedesc1"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 1
        End If
    End Sub

    Private Sub P_prCargarComboEstadoFatura()
        Dim dt As New DataTable
        dt.Columns.Add("key", GetType(String))
        dt.Columns.Add("name", GetType(String))

        dt.Rows.Add({"V", "Válida"})
        dt.Rows.Add({"A", "Anulada"})
        dt.Rows.Add({"E", "Extraviada"})
        dt.Rows.Add({"N", "No Utilizada"})
        dt.Rows.Add({"C", "Emitida por Contingencia"})

        With cbEstadoFactura
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("key").Width = 60
            .DropDownList.Columns("key").Caption = "COD"
            .DropDownList.Columns.Add("name").Width = 200
            .DropDownList.Columns("name").Caption = "DESCRIPCIÓN"
            .ValueMember = "key"
            .DisplayMember = "name"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbEstadoFactura.DataSource, DataTable).Rows.Count > 0) Then
            cbEstadoFactura.SelectedIndex = 0
        End If
    End Sub

    Private Sub _prCargarComboAlmacen(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable

        If (BanderaServer = False) Then
            Return
        End If
        dt = L_fnListarAlmacenQueTenganDosificacion()
        If (dt.Columns.Count <= 0) Then
            Dim info As New TaskDialogInfo("INFORMACION", eTaskDialogIcon.Information2, "Error de Conexion con el Servidor. Desea Intentar Nuevamente?." + vbLf + "Si el error persite comuniquese con su administrador de sistemas", eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.DarkBlue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                _prCargarComboAlmacen(mCombo)
            Else
                _prSalir()

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
        If (gb_userTodasSuc = False And CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
        Else

        End If
    End Sub

    Private Sub _prAsignarPermisos()

        If (BanderaServer = False) Then
            Return
        End If
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
        tbbanco.ReadOnly = True
        tbanular.IsReadOnly = True
        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbbanco.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenc.IsInputReadOnly = True
        swTipoVenta.ReadOnly = True
        cbmoneda.IsReadOnly = True
        'Datos facturacion
        tbNroAutoriz.ReadOnly = True
        tbNroFactura.ReadOnly = True
        tbCodigoControl.ReadOnly = True
        dtiFechaFactura.IsInputReadOnly = True
        dtiFechaFactura.ButtonDropDown.Enabled = False
        cbsector.ReadOnly = True
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        tbsecnumi.ReadOnly = True
        tbSubTotal.IsInputReadOnly = True
        tbtotal.IsInputReadOnly = True
        tbClienteCredito.ReadOnly = True
        grVentas.Enabled = True
        PanelNavegacion.Enabled = True


        TbNit.ReadOnly = True
        TbNombre1.ReadOnly = True

        cbSucursal.ReadOnly = True

        tbEstado.ReadOnly = True

        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
        End If


        tbTipoVenta.IsReadOnly = True

        PanelTotal.Visible = True
        PanelInferior.Visible = True
    End Sub
    Private Sub _prhabilitar()
        tbanular.IsReadOnly = False
        tbbanco.ReadOnly = False
        grVentas.Enabled = False
        tbCodigo.ReadOnly = False
        cbsector.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbbanco.ReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        swTipoVenta.ReadOnly = False
        cbmoneda.IsReadOnly = False
        btnGrabar.Enabled = True

        TbNit.ReadOnly = False
        TbNombre1.ReadOnly = False

        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
        End If
        'If (tbCodigo.Text.Length > 0) Then
        '    cbSucursal.ReadOnly = True
        'Else

        '    If (gb_userTodasSuc = False And CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
        '        cbSucursal.ReadOnly = True
        '    Else
        cbSucursal.ReadOnly = False
        '    End If


        'End If
        tbTipoVenta.IsReadOnly = False

        If tbTipoVenta.Value = False Then
            tbEstado.ReadOnly = False
        End If



    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVenta()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbsecnumi.Clear()
        tbCliente.Clear()
        _Codbanco = 0
        tbObservacion.Clear()
        tbbanco.Clear()
        swTipoVenta.Value = 0
        tbVehiculo.Clear()
        _CodCliente = 0
        _CodEmpleado = 0
        _CodVehiculo = 0
        tbFechaVenta.Value = Now.Date
        tbFechaVenc.Visible = False
        lbCredito.Visible = False
        _prCargarDetalleVenta(-1)
        MSuperTabControl.SelectedTabIndex = 0
        tbSubTotal.Value = 0
        tbPdesc.Value = 0
        tbMdesc.Value = 0
        tbtotal.Value = 0
        TbNit.Clear()
        TbNombre1.Clear()

        tbNroAutoriz.Clear()
        tbNroFactura.Clear()
        tbCodigoControl.Clear()
        dtiFechaFactura.Value = "2000/01/01"
        'If (gb_userTodasSuc = False And CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
        '    cbSucursal.Value = gi_userNumiSucursal
        'Else
        '    cbSucursal.SelectedIndex = 0
        'End If
        NroCompronbante = 0
        TbNit.Text = "0"
        TbNombre1.Text = "S/N"
        _CodClienteCredito = 0
        tbClienteCredito.Clear()
        tbanular.Value = True
        cbsector.Focus()

        If (cbsector.Value = -10) Then
            _prCargarTablaNroOrdenLavaderoCabana("-1", 17)
        Else
            _prCargarTablaNroOrdenLavadero("-1", 15)
        End If
        Try
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
        Catch ex As Exception

        End Try

        _conFactura = True

        tbPdesc.Value = 0

        tbEstado.SelectedIndex = 0
        swTipoVenta.SelectedIndex = 1

        cbmoneda.Value = True
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        'a.vcnumi ,a.vcfdoc , vctipo,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,vehiculo .lbplac ,a.vcalm ,
        'a.vcclie, cliente.lanom, a.vcfvcr, a.vcest, a.vcobs, a.vcdesc, a.vctotal

        With grVentas

            tbCodigo.Text = .GetValue("vcnumi")
            tbFechaVenta.Value = IIf(IsDBNull(.GetValue("vcfdoc")), Now.Date, .GetValue("vcfdoc"))
            swTipoVenta.Value = .GetValue("vctipo")
            cbmoneda.Value = .GetValue("vcmoneda")
            cbsector.Value = .GetValue("vcsector")
            tbsecnumi.Text = .GetValue("vcSecNumi")
            tbVehiculo.Text = .GetValue("lbplac")
            _CodVehiculo = .GetValue("vcnumivehic")
            cbSucursal.Value = .GetValue("vcalm")
            _CodCliente = .GetValue("vcclie")
            tbCliente.Text = .GetValue("lanom")
            tbObservacion.Text = .GetValue("vcobs")
            NroCompronbante = .GetValue("vcidcore")
            _Codbanco = .GetValue("vcbanco")
            tbbanco.Text = .GetValue("banco")
            tbFechaVenc.Value = IIf(IsDBNull(.GetValue("vcfvcr")), Now.Date, .GetValue("vcfvcr"))
            tbClienteCredito.Text = .GetValue("clientecredito")
            _CodClienteCredito = .GetValue("numicredito")

            If (gb_FacturaEmite) Then
                Dim dt As DataTable = L_fnObtenerTabla("TFV001", "fvanitcli, fvadescli1, fvadescli2, fvaautoriz, fvanfac, fvaccont, fvafec,fvaimgqr,fvaest", "fvanumi=" + tbCodigo.Text.Trim)
                If (dt.Rows.Count = 1) Then
                    TbNit.Text = dt.Rows(0).Item("fvanitcli").ToString
                    TbNombre1.Text = dt.Rows(0).Item("fvadescli1").ToString
                    'TbNombre2.Text = dt.Rows(0).Item("fvadescli2").ToString

                    tbNroAutoriz.Text = dt.Rows(0).Item("fvaautoriz").ToString
                    tbNroFactura.Text = dt.Rows(0).Item("fvanfac").ToString
                    tbCodigoControl.Text = dt.Rows(0).Item("fvaccont").ToString
                    dtiFechaFactura.Value = dt.Rows(0).Item("fvafec")
                    tbEstado.Value = Int(dt.Rows(0).Item("fvaest"))

                Else
                    TbNit.Clear()
                    TbNombre1.Clear()
                    'TbNombre2.Clear()

                    tbNroAutoriz.Clear()
                    tbNroFactura.Clear()
                    tbCodigoControl.Clear()
                    dtiFechaFactura.Value = "2000/01/01"
                End If
            End If

            'lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("tahact").ToString
            'lbUsuario.Text = .GetValue("tauact").ToString

        End With
        'If (cbsector.Value = 1) Then
        '    btnModificar.Enabled = True
        'Else
        '    btnModificar.Enabled = False
        'End If
        _prCargarDetalleVenta(tbCodigo.Text)

        If (cbsector.Value = 3) Then ''''Cargo el detalle de las ventas con nro de control de lavadero
            _prCargarTablaNroOrdenLavadero(tbCodigo.Text, 15)
        Else
            If (cbsector.Value = 4) Then ''''Cargo el detalle de las ventas con nro de orden de Remolque
                _prCargarTablaNroOrdenLavadero(tbCodigo.Text, 16)
            End If

            If (cbsector.Value = -10) Then ''''Cargo el detalle de las ventas con nro de orden de Remolque
                _prCargarTablaNroOrdenLavaderoCabana(tbCodigo.Text, 17)
            End If
        End If

        tbTipoVenta.Value = IIf(IsDBNull(grVentas.GetValue("vcfactura")), 1, grVentas.GetValue("vcfactura"))
        'tbanular.Value = grVentas.GetValue("anulada")

        tbMdesc.Value = grVentas.GetValue("vcdesc")
        _prCalcularPrecioTotal()
        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prCargarTablaServicios()
        Dim dt As New DataTable
        dt = L_fnListarServicios(cbsector.Value, CType(grdetalle.DataSource, DataTable), cbSucursal.Value)
        grServicios.DataSource = dt
        grServicios.RetrieveStructure()
        grServicios.AlternatingColors = True
        'a.sdnumi , a.sddesc, a.sdprec 

        With grServicios.RootTable.Columns("sdnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grServicios.RootTable.Columns("sddesc")
            .Width = 250
            .Caption = "SERVICIOS"
            .Visible = True
        End With
        With grServicios.RootTable.Columns("sdprec")
            .Width = 150
            .Caption = "PRECIO"
            .FormatString = "0.00"
            .Visible = True
        End With
        With grServicios.RootTable.Columns("estado")
            .Width = 100
            .Visible = False
        End With

        With grServicios
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
        _prAplicarCondiccionJanus()
    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grServicios.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.FontSize = 9
        fc.FormatStyle.ForeColor = Color.White
        fc.FormatStyle.BackColor = Color.Red
        grServicios.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub _prCargarTablaNroOrdenLavadero(numi As String, tipo As Integer)
        Dim dt As New DataTable
        dt = L_fnListarNroOrdenesLavadero(numi, tipo)
        grventasLavadero.DataSource = dt
        grventasLavadero.RetrieveStructure()
        grventasLavadero.AlternatingColors = True
        ''a.venumi ,a.vetv2numi ,a.vesecnumi,venta .ldnord as orden,
        'vehiculo.lbplac as placa, cliente.lanom As cliente , 1 as estado

        With grventasLavadero.RootTable.Columns("venumi")
            .Width = 100
            .Visible = False
        End With
        With grventasLavadero.RootTable.Columns("vetv2numi")
            .Width = 100
            .Visible = False
        End With
        With grventasLavadero.RootTable.Columns("vesecnumi")
            .Width = 100
            .Visible = False
        End With


        With grventasLavadero.RootTable.Columns("orden")
            .Width = 100
            .Caption = "NRO ORDEN"
            .Visible = True
        End With
        With grventasLavadero.RootTable.Columns("placa")
            .Width = 150
            .Caption = "PLACA"
            .Visible = True
        End With
        With grventasLavadero.RootTable.Columns("cliente")
            .Width = 300
            .Caption = "CLIENTE"
            .Visible = True
        End With
        With grventasLavadero.RootTable.Columns("estado")
            .Width = 100
            .Visible = False
        End With

        With grventasLavadero
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarTablaNroOrdenLavaderoCabana(numi As String, tipo As Integer)
        Dim dt As New DataTable

        dt = L_fnListarNroOrdenesLavadero(numi, 17)
        grventasLavadero.DataSource = dt
        grventasLavadero.RetrieveStructure()
        grventasLavadero.AlternatingColors = True
        'a.venumi , a.vetv2numi, a.vesecnumi, venta.hdfcin As fechai,
        'venta.hdfcou as fechaf, cliente.hanom   As cliente , 1 as estado

        With grventasLavadero.RootTable.Columns("venumi")
            .Width = 100
            .Visible = False
        End With
        With grventasLavadero.RootTable.Columns("vetv2numi")
            .Width = 100
            .Visible = False
        End With
        With grventasLavadero.RootTable.Columns("vesecnumi")
            .Width = 100
            .Visible = False
        End With
        With grventasLavadero.RootTable.Columns("fechai")
            .Width = 120
            .Caption = "FECHA INGRESO"
            .FormatString = "dd/MM/yyyy"
            .Visible = True
        End With
        With grventasLavadero.RootTable.Columns("fechaf")
            .Width = 150
            .Caption = "FECHA SALIDA"
            .FormatString = "dd/MM/yyyy"
            .Visible = True
        End With
        With grventasLavadero.RootTable.Columns("cliente")
            .Width = 300
            .Caption = "CLIENTE"
            .Visible = True
        End With
        With grventasLavadero.RootTable.Columns("cabana")
            .Width = 180
            .Caption = "CABAñA"
            .Visible = True
        End With
        With grventasLavadero.RootTable.Columns("estado")
            .Width = 100
            .Visible = False
        End With

        With grventasLavadero
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleLavadero(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '     vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,b.eddesc as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
        ',vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado

        With grdetalle.RootTable.Columns("vdnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdvc2numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdserv")
            .Width = 90
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("vdprod")
            .Caption = ""
            .Width = 100
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("descripcion")
            .Caption = "DESCRIPCION"
            .Width = 410
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("vdcmin")
            .Width = 160
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad".ToUpper
        End With

        With grdetalle.RootTable.Columns("vdpbas")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U.".ToUpper
        End With

        With grdetalle.RootTable.Columns("vdptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("vdporc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "P.Desc(%)".ToUpper
        End With
        With grdetalle.RootTable.Columns("vddesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "M.Desc".ToUpper
        End With
        With grdetalle.RootTable.Columns("vdtotdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("vdobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdpcos")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdptot2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .ContextMenuStrip = ContextMenuStrip1

        End With
    End Sub

    Private Sub _prCargarDetalleVenta2(_dt As DataTable)
        Dim dt As New DataTable
        grdetalle.DataSource = _dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '     vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,b.eddesc as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
        ',vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado

        With grdetalle.RootTable.Columns("vdnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdvc2numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdserv")
            .Width = 90
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("vdprod")
            .Caption = ""
            .Width = 100
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("descripcion")
            .Caption = "DESCRIPCION"
            .Width = 410
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("vdcmin")
            .Width = 160
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad".ToUpper
        End With

        With grdetalle.RootTable.Columns("vdpbas")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U.".ToUpper
        End With

        With grdetalle.RootTable.Columns("vdptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("vdporc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "P.Desc(%)".ToUpper
        End With
        With grdetalle.RootTable.Columns("vddesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "M.Desc".ToUpper
        End With
        With grdetalle.RootTable.Columns("vdtotdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("vdobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdpcos")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vdptot2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .ContextMenuStrip = ContextMenuStrip1

        End With
    End Sub

    Private Sub _prCargarVenta()
        If (BanderaServer = False) Then
            Return
        End If
        Dim dt As New DataTable
        dt = L_fnGeneralVentaLavadero()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        'a.vcnumi, a.vcidcore, a.vcsector, a.vcSecNumi, a.vcnumivehic, vehiculo.lbplac, a.vcalm, a.vcfdoc,
        ' a.vcclie, cliente.lanom, a.vcfvcr, a.vctipo, a.vcest, a.vcobs, a.vcdesc, a.vctotal
        With grVentas.RootTable.Columns("vcnumi")
            .Width = 90
            .Caption = "CODIGO"
            .Visible = True
            .TextAlignment = TextAlignment.Far


        End With
        With grVentas.RootTable.Columns("sucursal")
            .Width = 180
            .Visible = True
            .Caption = "SUCURSAL"
        End With
        With grVentas.RootTable.Columns("factura")
            .Width = 105
            .Visible = True
            .Caption = "Nro FACTURA"
            .TextAlignment = TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("sector")
            .Width = 120
            .Visible = True
            .Caption = "SECTOR"
        End With
        With grVentas.RootTable.Columns("nit")
            .Width = 90
            .Visible = True
            .Caption = "NIT"
        End With

        With grVentas.RootTable.Columns("vcidcore")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcfactura")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("anulada")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcmoneda")
            .Width = 90
            .Visible = False
        End With
        'With grVentas.RootTable.Columns("anulada")
        '    .Width = 90
        '    .Visible = False
        'End With
        With grVentas.RootTable.Columns("vcsector")
            .Width = 90
            .Visible = False
            .Caption = "Sector"
        End With
        With grVentas.RootTable.Columns("vcSecNumi")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcnumivehic")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcalm")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("lbplac")
            .Width = 100
            .Caption = "VEHICULO"
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcfdoc")
            .Width = 90
            .Caption = "FECHA"
            .FormatString = "dd/MM/yyyy"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("vcobs")
            .Width = 200
            .Caption = "OBSERVACION"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("vcdesc")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("vcclie")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("lanom")
            .Width = 300
            .Caption = "CLIENTE"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("tipo")
            .Width = 200
            .Caption = "TIPO VENTA"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("vcfvcr")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vctipo")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("vcest")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcbanco")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("banco")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vctotal")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With

        With grVentas.RootTable.Columns("numicredito")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("clientecredito")
            .Width = 90
            .Visible = False
        End With
        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("vdnumi=MAX(vdnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("vdnumi")
        End If
        Return 1
    End Function
    Public Function _fnAccesible()
        Return tbObservacion.ReadOnly = False
    End Function

    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("vdnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Public Sub _prCalcularPrecioTotalLavadero()
        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("vdptot"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbSubTotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("vdptot"), AggregateFunction.Sum)
        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("vdptot"), AggregateFunction.Sum) - montodesc
    End Sub
    Public Sub _prCalcularPrecioTotal()
        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("vdtotdesc"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbSubTotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("vdtotdesc"), AggregateFunction.Sum)
        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("vdtotdesc"), AggregateFunction.Sum) - montodesc
    End Sub
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("vdnumi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
                _prCalcularPrecioTotal()
                grdetalle.Select()
                grdetalle.Col = 5
                grdetalle.Row = grdetalle.RowCount - 1
            End If
        End If
    End Sub
    Function _fnValidarDosificacion() As Boolean
        Dim dt As DataTable = L_fnVerificarSiExisteDosificacionFactura(cbSucursal.Value, IIf(tbTipoVenta.Value = True, 1, 0))
        If (dt.Rows.Count > 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function _ValidarCampos() As Boolean
        If (IsNumeric(TbNit.Text) = False) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "el nit solo puede estar formado por numeros".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If

        If (Not _fnValidarDosificacion()) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Error No Existe el Tipo de Dosificacion Para Esa Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbTipoVenta.Focus()
            Return False
        End If
        If (_CodCliente <= 0 And cbsector.Value = 1 And cbsector.Value = 4) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Cliente con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False
        End If

        If (TbNit.Text.Length = 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Inserte un numero de nit".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            TbNit.Focus()
            Return False
        End If
        If (TbNombre1.Text.Length = 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Inserte nombre a quien se va a facturar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            TbNombre1.Focus()
            Return False
        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSucursal.Focus()
            Return False
        End If
        If (grdetalle.RowCount = 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If

        'If (grdetalle.RowCount > 0) Then
        '    grdetalle.Row = grdetalle.RowCount - 1
        '    If (grdetalle.GetValue("vdcmin") = 0) Then
        '        Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
        '        ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '        Return False
        '    End If
        'End If

        If (swTipoVenta.Value = False) Then
            If (_CodClienteCredito > 0 And tbClienteCredito.Text.Length > 2) Then
                Return True
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Cliente PARA EL CREDITO con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbClienteCredito.Focus()
                Return False
            End If

        End If

        'codigo danny
        'validar detalle si todos los servicios son con factura
        Dim cantServSinFactura As Integer = 0
        Dim servSinFactura As String = ""
        Dim dtDetalle As DataTable = CType(grdetalle.DataSource, DataTable)
        For Each fila As DataRow In dtDetalle.Rows
            Dim numiSer As Integer = fila.Item("vdserv")
            Dim dtServEstado As DataTable = L_fnVentaLavaderoObtenerEstadoServ(numiSer)
            If dtServEstado.Rows.Count > 0 Then
                If IsDBNull(dtServEstado.Rows(0).Item("sefactu")) = False Then
                    Dim conFactura As Integer = dtServEstado.Rows(0).Item("sefactu")
                    If conFactura = 0 Then
                        cantServSinFactura = cantServSinFactura + 1
                        servSinFactura = fila.Item("descripcion")
                    End If
                End If

            End If
        Next

        If cantServSinFactura > 0 Then
            If cantServSinFactura <> dtDetalle.Rows.Count Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "el servicio ".ToUpper + servSinFactura + " es sin iva".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbClienteCredito.Focus()
                Return False
            Else
                _conFactura = False
            End If
        End If
        Return True
    End Function
    Private Function P_fnGrabarFacturarTFV001(numi As String, sucursal As String) As Boolean
        Dim a As Double = CDbl(tbtotal.Value + tbMdesc.Value)
        Dim b As Double = CDbl(0) 'Ya esta calculado el 55% del ICE
        Dim c As Double = CDbl("0")
        Dim d As Double = CDbl("0")
        Dim e As Double = a - b - c - d
        Dim f As Double = CDbl(tbMdesc.Value)
        Dim g As Double = e - f
        Dim h As Double = g * (gi_IVA / 100)

        Dim res As Boolean = False
        'Grabado de Cabesera Factura
        ''Dim estado As String = ""
        ''If (tbTipoVenta.Value = False And tbanular.Value = False) Then
        ''    estado = "0"
        ''Else
        ''    estado = "1"
        ''End If
        Dim estado As String = tbEstado.Value

        L_Grabar_Factura(numi,
                        tbFechaVenta.Value.ToString("yyyy/MM/dd"),
                        "0",
                        "0",
                        estado,
                        TbNit.Text.Trim,
                        "0",
                        TbNombre1.Text,
                        "",
                        CStr(Format(a, "####0.00")),
                        CStr(Format(b, "####0.00")),
                        CStr(Format(c, "####0.00")),
                        CStr(Format(d, "####0.00")),
                        CStr(Format(e, "####0.00")),
                        CStr(Format(f, "####0.00")),
                        CStr(Format(g, "####0.00")),
                        CStr(Format(h, "####0.00")),
                        "",
                        tbFechaVenta.Value.ToString("yyyy/MM/dd"),
                        "''",
                        sucursal,
                        numi)

        'Dim s As String = ""
        For Each fil As GridEXRow In grdetalle.GetRows
            If (Not fil.Cells("vdcmin").Value.ToString.Trim.Equals("")) Then
                's = fil.Cells("codP").Value
                's = fil.Cells("des").Value
                's = fil.Cells("can").Value
                's = fil.Cells("imp").Value
                Dim codServ As Integer
                If (fil.Cells("vdserv").Value > 0) Then
                    codServ = fil.Cells("vdserv").Value
                Else
                    codServ = fil.Cells("vdprod").Value
                End If
                L_Grabar_Factura_Detalle(numi.ToString,
                                       Str(codServ).ToString.Trim,
                                        fil.Cells("descripcion").Value.ToString.Trim,
                                        fil.Cells("vdcmin").Value.ToString.Trim,
                                        fil.Cells("vdpbas").Value.ToString.Trim,
                                        numi)
                res = True
            End If
        Next
        Return res
    End Function
    Private Function P_fnValidarFactura() As Boolean
        Return True
    End Function



    Private Function P_fnGenerarFacturaManualModificada(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi, cbSucursal.Value) ' Grabar en la TFV001
        If (res) Then
            If (P_fnValidarFactura()) Then
                'Validar para facturar
                L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(tbNroFactura.Text),
                            CStr(IIf(IsNothing(tbNroAutoriz.Text) = True, 0, tbNroAutoriz.Text)),'revisar don Carlos
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            IIf(IsNothing(tbCodigoControl.Text) = True, "", tbCodigoControl.Text),'revisar don Carlos
                            LimiteEmision.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))


                'L_Actualiza_Dosificacion(NumiDosificacion, NroFactura, numi)
                'L_Actualiza_Dosificacion(NumiDosificacion, tbNroFactura.Text, numi) 'revisar don Carlos
            Else
                'Volver todo al estada anterior
                ToastNotification.Show(Me, "No es posible facturar, vuelva a ingresar a la mesa he intente nuevamente!!!".ToUpper,
                                       My.Resources.OK1,
                                       5 * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.MiddleCenter)
            End If

            If (Not TbNit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(TbNit.Text.Trim, TbNombre1.Text.Trim, "")
            Else
                L_Grabar_Nit(TbNit.Text, "S/N", "")
            End If
        End If

        Return res
    End Function

    Private Function P_fnGenerarFacturaManual(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi, cbSucursal.Value) ' Grabar en la TFV001
        If (res) Then
            If (P_fnValidarFactura()) Then
                'Validar para facturar
                L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(NroFactura),
                            CStr(NroAutorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            CodigoControl,
                            LimiteEmision.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))


                L_Actualiza_Dosificacion(NumiDosificacion, NroFactura, numi)
            Else
                'Volver todo al estada anterior
                ToastNotification.Show(Me, "No es posible facturar, vuelva a ingresar a la mesa he intente nuevamente!!!".ToUpper,
                                       My.Resources.OK1,
                                       5 * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.MiddleCenter)
            End If

            If (Not TbNit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(TbNit.Text.Trim, TbNombre1.Text.Trim, "")
            Else
                L_Grabar_Nit(TbNit.Text, "S/N", "")
            End If
        End If

        Return res
    End Function

    Private Function P_fnGenerarFacturaModificado(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi, cbSucursal.Value) ' Grabar en la TFV001
        If (res) Then
            If (P_fnValidarFactura()) Then
                'Validar para facturar
                P_prImprimirFacturarModificado(numi, True, True, tbNroFactura.Text) '_Codigo de a tabla TV001
            Else
                'Volver todo al estada anterior
                ToastNotification.Show(Me, "No es posible facturar, vuelva a ingresar a la mesa he intente nuevamente!!!".ToUpper,
                                       My.Resources.OK1,
                                       5 * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.MiddleCenter)
            End If

            If (Not TbNit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(TbNit.Text.Trim, TbNombre1.Text.Trim, "")
            Else
                L_Grabar_Nit(TbNit.Text, "S/N", "")
            End If
        End If

        Return res
    End Function
    Private Function P_fnGenerarFactura(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi, cbSucursal.Value) ' Grabar en la TFV001
        If (res) Then
            If (P_fnValidarFactura()) Then
                'Validar para facturar
                P_prImprimirFacturar(numi, True, True, swreporte.Value) '_Codigo de a tabla TV001
            Else
                'Volver todo al estada anterior
                ToastNotification.Show(Me, "No es posible facturar, vuelva a ingresar a la mesa he intente nuevamente!!!".ToUpper,
                                       My.Resources.OK1,
                                       5 * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.MiddleCenter)
            End If

            If (Not TbNit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(TbNit.Text.Trim, TbNombre1.Text.Trim, "")
            Else
                L_Grabar_Nit(TbNit.Text, "S/N", "")
            End If
        End If

        Return res
    End Function
    Public Function _ExisteDatosFactura(inicial As Integer, final As Integer, sucursal As Integer, fechaI As String, fechaF As String) As Boolean

        Dim ef = New Efecto
        ef.tipo = 6
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "".ToUpper
        ef.inicial = inicial
        ef.final = final
        ef.sucursal = sucursal
        ef.fechaI = fechaI
        ef.fechaF = fechaF
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            'cjnumi, cjci, cjnombre, cjtipo

            ''CodigoControl = ef.nroControl'danny
            NroFactura = ef.nroFactura
            ''NroAutorizacion = ef.nroAutorizacion'danny
        End If
        Return bandera
    End Function
    Public Sub _GuardarNuevo()
        Dim numi As String = ""
        Dim dt As DataTable = L_fnEsDosificacionManual(cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), cbsector.Value)
        Dim bandera As Boolean = False

        If tbTipoVenta.Value = False And _conFactura = True Then
            If (dt.Rows.Count > 0) Then
                Dim fechaI As String = CType(dt.Rows(0).Item("sbfdel"), DateTime).ToString("yyyy/MM/dd")
                Dim FechaF As String = CType(dt.Rows(0).Item("sbfal"), DateTime).ToString("yyyy/MM/dd")
                NumiDosificacion = dt.Rows(0).Item("sbnumi")
                LimiteEmision = dt.Rows(0).Item("sbfal")
                '_Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac

                If (_ExisteDatosFactura(dt.Rows(0).Item("sbinicio"), dt.Rows(0).Item("sbfinal"), cbSucursal.Value, fechaI, FechaF) = False) Then
                    Return
                Else
                    bandera = True
                    ''*************codigo Danny
                    Dim codControl1 As String = String.Empty
                    Dim nroAutorizacion1 As String = dt.Rows(0).Item("sbautoriz")
                    CodigoControl = codControl1
                    NroAutorizacion = nroAutorizacion1
                    ''*************

                    'P_fnGenerarFacturaManual(numi)
                End If
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "no existe la dosificacion manual para esta sucursal, por lo tanto no se puede realizar la facturacion manual".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return
            End If
        End If

        If tbTipoVenta.Value = True And _conFactura = True Then
            Dim dtSet As DataTable = L_DosificacionAutomatica(cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), cbsector.Value)
            If dtSet.Rows.Count > 0 Then
                If dtSet.Rows.Count = 0 Then
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, "no existe la dosificacion automatica habilitada para esta sucursal, por lo tanto no se puede realizar la facturacion automatica".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    Return
                End If
            Else

                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "no existe la dosificacion automatica habilitada para esta sucursal, por lo tanto no se puede realizar la facturacion automatica".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return
            End If
        End If
        ' vcnumi ,@vcidcore ,@vcsector ,@vcSecNumi ,@vcnumivehic ,@vcalm ,@vcfdoc ,@vcclie ,@vcfvcr ,@vctipo ,
        '@vcest ,@vcobs ,@vcdesc ,@vctotal 

        Dim res As Boolean = L_fnGrabarVentaLavadero(numi, cbsector.Value, tbsecnumi.Text, _CodVehiculo, cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodCliente, tbFechaVenc.Value.ToString("yyyy/MM/dd"), swTipoVenta.Value, 0, tbObservacion.Text, tbMdesc.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable), _CodClienteCredito, CType(grventasLavadero.DataSource, DataTable), IIf(tbTipoVenta.Value = True, 1, 0), IIf(tbanular.Value = True, 1, 0), IIf(cbmoneda.Value = True, 1, 0), _Codbanco)


        If res Then
            If _conFactura Then 'venta con factura
                If (bandera = True) Then
                    P_fnGenerarFacturaManual(numi)
                Else
                    If (gb_FacturaEmite) Then


                        P_fnGenerarFactura(numi)



                    End If
                End If
            Else 'venta con recibo
                P_prImprimirRecibo(numi, True, True)
            End If


            SuperTabControl1.SelectedTabIndex = 0

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            '_prImiprimirNotaVenta(numi)

            _prCargarVenta()

            _Limpiar()

        Else
            If (numi = -100) Then

                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Venta ya se encuentra registrada vuelva hacia atras y revise los datos".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Venta no pudo ser insertado.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If


        End If

    End Sub

    Private Sub _prGuardarModificado()
        Dim res As Boolean = L_fnModificarVentaLavadero(tbCodigo.Text, CInt(cbsector.Value), tbsecnumi.Text, _CodVehiculo, cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodCliente, tbFechaVenc.Value.ToString("yyyy/MM/dd"), swTipoVenta.Value, 0, tbObservacion.Text, tbMdesc.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable), _CodClienteCredito, IIf(tbTipoVenta.Value = True, 1, 0), IIf(tbanular.Value = True, 1, 0), IIf(cbmoneda.Value = True, 1, 0), _Codbanco)
        If res Then

            If (gb_FacturaEmite) Then
                L_fnEliminarDatos("TFV001", "fvanumi=" + tbCodigo.Text.Trim)
                L_fnEliminarDatos("TFV0011", "fvbnumi=" + tbCodigo.Text.Trim)

                If (tbTipoVenta.Value = True) Then
                    P_fnGenerarFacturaModificado(tbCodigo.Text.Trim)
                Else
                    P_fnGenerarFacturaManualModificada(tbCodigo.Text.Trim)
                End If

            End If
            ' _prImiprimirNotaVenta(tbCodigo.Text)

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )


            _prCargarVenta()

            _prSalir()


        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grVentas.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If
        Else
            _modulo.Select()
            _tab.Close()

            BanderaServer = False


        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
        Next

    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub

    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function


#End Region



#Region "EVENTOS DEL FORMULARIO"
    Private Sub F1_ServicioVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        SuperTabControl1.SelectedTabIndex = 0
        _Limpiar()
        _prhabilitar()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False

        'btnNuevo.Enabled = False
        'btnModificar.Enabled = False
        'btnEliminar.Enabled = False
        'GPanelProductos.Visible = False
        '_prhabilitar()

        '_Limpiar()
        If (cbsector.Value = 2 Or cbsector.Value = 1 Or cbsector.Value = 5) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            _prAddDetalleVenta()

        End If
        cbsector.Focus()
        If (gb_userTodasSuc = False And CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False
        End If


    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()

    End Sub

#End Region



    Public Function _prlimpiardatatbleRemolque(ByVal dtable As DataTable) As DataTable
        Dim dtcopia As DataTable = dtable.Copy
        Dim dt As DataTable = dtable.Copy
        dt.Rows.Clear()
        For i As Integer = 0 To dtcopia.Rows.Count - 1 Step 1
            Dim numi As Integer = dtcopia.Rows(i).Item("renumi")
            If (Not _fnExisteData(numi)) Then
                dt.ImportRow(dtcopia.Rows(i))
            End If

        Next
        Return dt
        'With grventasLavadero.RootTable.Columns("vesecnumi")
        '    .Width = 100
        '    .Visible = False
        'End With

    End Function

    Public Sub _prListarServiciosRemolque()

        Dim dt As DataTable

        dt = L_fnListarVentaRemolque(CType(grventasLavadero.DataSource, DataTable))
        dt = _prlimpiardatatbleRemolque(dt)
        'a.renumi  , a.retcr11vehcli, vehiculo.rbplac, a.refdoc, a.retcr1cli, cliente.ranom, a.refvcr, a.retpago, a.reest  ,Sucursal,total

        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("renumi", False, "ID_VENTA", 50))
        listEstCeldas.Add(New Modelos.Celda("rencont", True, "NRO CONTROL", 100))
        listEstCeldas.Add(New Modelos.Celda("retcr11vehcli", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("placas", True, "PLACA", 120))
        listEstCeldas.Add(New Modelos.Celda("sucursal", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("refdoc", True, "FECHA VENTA", 120, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelos.Celda("retcr1cli", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("nombre", True, "CLIENTE", 180))
        listEstCeldas.Add(New Modelos.Celda("refvcr", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("retpago", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("reest", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("total", True, "TOTAL", 100, "0.00"))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione VENTA".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        Dim renumi As Integer
        bandera = ef.band
        If (bandera = True) Then
            'a.renumi  , a.retcr11vehcli, vehiculo.rbplac, a.refdoc, a.retcr1cli, cliente.ranom, a.refvcr, a.retpago, a.reest  ,Sucursal,total,cliente.ranit ,cliente.rafacnom  
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            tbsecnumi.Text = Row.Cells("renumi").Value 'CFO 28/02/2020
            _CodVehiculo = Row.Cells("retcr11vehcli").Value
            tbVehiculo.Text = Row.Cells("placas").Value
            cbSucursal.Value = Row.Cells("Sucursal").Value
            tbFechaVenta.Value = Now.Date
            _CodCliente = Row.Cells("retcr1cli").Value
            tbCliente.Text = Row.Cells("nombre").Value
            tbFechaVenc.Value = Row.Cells("refvcr").Value
            'swTipoVenta.Value = Row.Cells("retpago").Value  CFO  28/02/20
            _CargarDetalleVentaAyudaRemolque(tbsecnumi.Text)
            TbNit.Text = Row.Cells("ranit").Value
            TbNombre1.Text = Row.Cells("rafacnom").Value

            CType(grventasLavadero.DataSource, DataTable).Rows.Add(_fnSiguienteNumiLavadero(), 0, Row.Cells("renumi").Value, Row.Cells("rencont").Value, Row.Cells("placas").Value, Row.Cells("nombre").Value, 0)

        End If
        grventasLavadero.Select()
    End Sub

    Public Function _fnExisteData(numi As Integer) As Boolean
        Dim dt As DataTable = CType(grventasLavadero.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = dt.Rows(i).Item("vesecnumi")
            If (data = numi) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function _fnExisteDataCabana(numi As Integer) As Boolean
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = dt.Rows(i).Item("vdprod")
            If (data = numi) Then
                Return True
            End If
        Next
        Return False
    End Function




    Public Function _prlimpiardatatble(ByVal dtable As DataTable) As DataTable
        Dim dtcopia As DataTable = dtable.Copy
        Dim dt As DataTable = dtable.Copy
        dt.Rows.Clear()
        For i As Integer = 0 To dtcopia.Rows.Count - 1 Step 1
            Dim numi As Integer = dtcopia.Rows(i).Item("ldnumi")
            If (Not _fnExisteData(numi)) Then
                dt.ImportRow(dtcopia.Rows(i))
            End If

        Next
        Return dt
        'With grventasLavadero.RootTable.Columns("vesecnumi")
        '    .Width = 100
        '    .Visible = False
        'End With

    End Function
    Public Sub _prListarServiciosLavadero()

        Dim dt As DataTable
        If (cbsector.Value = 3) Then

            dt = L_fnListarVentaLavadero(CType(grventasLavadero.DataSource, DataTable))
        End If
        If (cbsector.Value = 8) Then
            dt = L_fnListarVentaLavaderoProductos(CType(grventasLavadero.DataSource, DataTable))

        End If

        dt = _prlimpiardatatble(dt)
        'a.ldnumi ,ldnord,a.ldtcl11veh ,vehiculo .lbplac as placas,a.ldsuc ,a.ldfdoc ,a.ldtcl1cli ,cliente .lanom as nombre,a.ldfvcr ,a.ldtpago ,a.ldest ,Sum(detalle .lcptot)as total 

        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("ldnumi", False, "ID_VENTA", 50))
        listEstCeldas.Add(New Modelos.Celda("ldnord", True, "NRO ORDEN", 90))
        listEstCeldas.Add(New Modelos.Celda("ldtcl11veh", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("placas", True, "PLACA", 120))
        listEstCeldas.Add(New Modelos.Celda("ldsuc", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("ldfdoc", True, "FECHA VENTA", 120, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelos.Celda("ldtcl1cli", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("nombre", True, "CLIENTE", 180))
        listEstCeldas.Add(New Modelos.Celda("ldfvcr", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("ldtven", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("ldtmon", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("ldest", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("ldbanco", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("banco", False, "ID", 50))
        listEstCeldas.Add(New Modelos.Celda("total", True, "TOTAL", 100, "0.00"))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione VENTA".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        Dim Renumi As Integer
        bandera = ef.band
        If (bandera = True) Then
            'a.ldnumi ,a.ldtcl11veh ,vehiculo .lbplac as placas,a.ldsuc ,a.ldfdoc ,a.ldtcl1cli ,cliente .lanom as nombre,a.ldfvcr ,a.ldtpago ,a.ldest ,Sum(detalle .lcptot)as total 
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            tbsecnumi.Text = Row.Cells("ldnumi").Value ' CFO 28/02/20
            _CodVehiculo = Row.Cells("ldtcl11veh").Value
            tbVehiculo.Text = Row.Cells("placas").Value
            cbSucursal.Value = Row.Cells("ldsuc").Value
            tbFechaVenta.Value = Now.Date
            _CodCliente = Row.Cells("ldtcl1cli").Value
            tbCliente.Text = Row.Cells("nombre").Value
            tbFechaVenc.Value = Row.Cells("ldfvcr").Value
            'swTipoVenta.Value = Row.Cells("ldtven").Value CFO 28/02/20
            cbmoneda.Value = Row.Cells("ldtmon").Value
            _Codbanco = Row.Cells("ldbanco").Value
            tbbanco.Text = Row.Cells("banco").Value
            _CargarDetalleVentaAyuda(tbsecnumi.Text) ' CFO 28/02/20

            '    a.venumi , a.vetv2numi, a.vesecnumi, venta.ldnord As orden,
            'vehiculo.lbplac as placa, cliente.lanom As cliente , 1 as estado
            CType(grventasLavadero.DataSource, DataTable).Rows.Add(_fnSiguienteNumiLavadero(), 0, Row.Cells("ldnumi").Value, Row.Cells("ldnord").Value, Row.Cells("placas").Value, Row.Cells("nombre").Value, 0)


        End If
        grventasLavadero.Select()

    End Sub

    Public Function _fnSiguienteNumiLavadero()
        Dim dt As DataTable = CType(grventasLavadero.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("venumi=MAX(venumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("venumi")
        End If
        Return 1
    End Function
    Public Function _prlimpiardatatbleCabana(ByVal dtable As DataTable) As DataTable
        Dim dtcopia As DataTable = dtable.Copy
        Dim dt As DataTable = dtable.Copy
        dt.Rows.Clear()
        For i As Integer = 0 To dtcopia.Rows.Count - 1 Step 1
            Dim numi As Integer = dtcopia.Rows(i).Item("hdnumi")
            If (Not _fnExisteDataCabana(numi)) Then
                dt.ImportRow(dtcopia.Rows(i))
            End If

        Next
        Return dt
        'With grventasLavadero.RootTable.Columns("vesecnumi")
        '    .Width = 100
        '    .Visible = False
        'End With

    End Function

    Public Sub _prListarServiciosCabañas()

        Dim dt As DataTable

        _prCargarTablaNroOrdenLavaderoCabana("-1", 17)

        dt = L_fnListarVentaCabañas(CType(grventasLavadero.DataSource, DataTable))
        dt = _prlimpiardatatbleCabana(dt)
        '      a.hdnumi , a.hdfing, cliente.hanumi, cliente.hanom As cliente,
        'a.hdfcin as fechai , a.hdfcou As fechaf, a.hdtc2cab As numiCabana,
        'cabana.hbnom , isnull(a.hdprecio, 0) As precio, DateDiff(Day, a.hdfcin, a.hdfcou) + 1 As cantidadDias,
        '(isnull(a.hdprecio,0)*(DATEDIFF(day,a.hdfcin,a.hdfcou)+1 )) as total

        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("hdnumi", True, "ID_VENTA", 50))
        listEstCeldas.Add(New Modelos.Celda("hdfing", False))
        listEstCeldas.Add(New Modelos.Celda("hanumi", False))
        listEstCeldas.Add(New Modelos.Celda("cliente", True, "CLIENTE", 220))
        listEstCeldas.Add(New Modelos.Celda("fechai", True, "FECHA INGRESO", 120, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelos.Celda("fechaf", True, "FECHA SALIDA", 120, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelos.Celda("numiCabana", False))
        listEstCeldas.Add(New Modelos.Celda("hbnom", True, "CABAÑA", 180))
        listEstCeldas.Add(New Modelos.Celda("precio", False))
        listEstCeldas.Add(New Modelos.Celda("cantidadDias", False))

        listEstCeldas.Add(New Modelos.Celda("total", True, "TOTAL", 110, "0.00"))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione VENTA".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        Dim Hdnumi As Integer
        If (bandera = True) Then
            '      a.hdnumi , a.hdfing, cliente.hanumi, cliente.hanom As cliente,
            'a.hdfcin as fechai , a.hdfcou As fechaf, a.hdtc2cab As numiCabana,
            'cabana.hbnom , isnull(a.hdprecio, 0) As precio, DateDiff(Day, a.hdfcin, a.hdfcou) + 1 As cantidadDias,
            '(isnull(a.hdprecio,0)*(DATEDIFF(day,a.hdfcin,a.hdfcou)+1 )) as total 

            'CODIGO DANNY
            _prCargarTablaNroOrdenLavaderoCabana("-1", 17)
            '---------------

            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            tbsecnumi.Text = Row.Cells("hdnumi").Value ' CFO 28/02/20
            _CodVehiculo = 0
            tbVehiculo.Text = ""
            cbSucursal.Value = 1
            tbFechaVenta.Value = Now.Date
            _CodCliente = Row.Cells("hanumi").Value
            tbCliente.Text = Row.Cells("cliente").Value
            tbFechaVenc.Value = Now.Date
            swTipoVenta.Value = 1
            '''''AQUI ARMO EL DETALLE PARA LA VENTA
            'CType(grdetalle.DataSource, DataTable).Rows.Clear()
            CType(grdetalle.DataSource, DataTable).Rows.Add()
            Dim pos As Integer = CType(grdetalle.DataSource, DataTable).Rows.Count - 1
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdnumi") = _fnSiguienteNumi()
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdserv") = 149 'Row.Cells("numiCabana").Value
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdprod") = Row.Cells("hdnumi").Value
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descripcion") = "ALQUILER DE LA " + Row.Cells("hbnom").Value + " DEL " + Row.Cells("fechai").Value + " AL " + Row.Cells("fechaf").Value
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = Row.Cells("cantidadDias").Value
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = Row.Cells("precio").Value
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = Row.Cells("total").Value
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdporc") = 0
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vddesc") = 0
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = Row.Cells("total").Value
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 0
            grdetalle.RootTable.Columns("descripcion").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCells

            CType(grventasLavadero.DataSource, DataTable).Rows.Add(_fnSiguienteNumiLavadero(), 0, Row.Cells("hdnumi").Value, Row.Cells("cliente").Value, Row.Cells("fechai").Value, Row.Cells("fechaf").Value, Row.Cells("hbnom").Value, 0)
            _prCalcularPrecioTotal()
        End If
    End Sub



    Private Sub tbsecnumi_KeyDown(sender As Object, e As KeyEventArgs) Handles tbsecnumi.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                If (cbsector.Value = 3 Or cbsector.Value = 8) Then
                    _prListarServiciosLavadero()
                    Return
                End If
                If (cbsector.Value = 4) Then
                    _prListarServiciosRemolque()
                    Return
                End If
                If (cbsector.Value = -10) Then
                    _prListarServiciosCabañas()
                    Return
                End If

                tbsecnumi.Clear()
                tbsecnumi.ReadOnly = True
            End If

        End If


    End Sub
    Public Function _fnExisteServicioProducto(numi As Integer, tipo As Integer, ByRef posicion As Integer) As Boolean
        If (tipo = 1) Then   ''''''SERVICIOS
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                If (dt.Rows(i).Item("vdserv") = numi) Then
                    posicion = i
                    Return True

                End If
            Next
            Return False
        End If
        If (tipo = 2) Then  ''''PRODUCTOS
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                If (dt.Rows(i).Item("vdprod") = numi) Then
                    posicion = i
                    Return True

                End If
            Next
            Return False
        End If

    End Function
    Public Sub _CargarDetalleVentaAyuda(numi As Integer)
        Dim dt As DataTable = L_fnDetalleAyudaLavadero(numi)
        '     lclin ,lcnumi ,lctce4pro,lctcl3pro  ,b.eddesc  ,lctp1emp,lctce42pro,'' as nombre 
        ',lcpuni ,lccant,lcpdes ,lcmdes ,lcptot 
        ',lcfpag ,lcppagper ,lcmpagper ,lcest
        '' CType(grdetalle.DataSource, DataTable).Rows.Clear()
        Dim posicion As Integer = -1
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            'vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,b.eddesc as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
            '			,vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado
            Dim servicio As Integer = IIf(IsDBNull(dt.Rows(i).Item("lctce4pro")), 0, dt.Rows(i).Item("lctce4pro"))
            Dim producto As Integer = IIf(IsDBNull(dt.Rows(i).Item("lctcl3pro")), 0, dt.Rows(i).Item("lctcl3pro"))
            If (servicio > 0) Then
                If (_fnExisteServicioProducto(servicio, 1, posicion)) Then
                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin") = IIf(IsDBNull(dt.Rows(i).Item("lccant")), 0, dt.Rows(i).Item("lccant")) + IIf(IsDBNull(CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin")), 0, CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin"))

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc") = dt.Rows(i).Item("lcmdes") + CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc")

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") + (dt.Rows(i).Item("lcpuni") * dt.Rows(i).Item("lccant"))

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") - CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc")
                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdpbas") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") / CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin")


                Else
                    CType(grdetalle.DataSource, DataTable).Rows.Add()
                    Dim pos As Integer = CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdnumi") = _fnSiguienteNumi()
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdserv") = dt.Rows(i).Item("lctce4pro")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdprod") = dt.Rows(i).Item("lctcl3pro")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descripcion") = dt.Rows(i).Item("eddesc")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = dt.Rows(i).Item("lccant")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = dt.Rows(i).Item("lcpuni")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = dt.Rows(i).Item("lcpuni") * dt.Rows(i).Item("lccant")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdporc") = dt.Rows(i).Item("lcpdes")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vddesc") = dt.Rows(i).Item("lcmdes")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = dt.Rows(i).Item("lcptot")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 0
                End If
            End If


            If (producto > 0) Then
                If (_fnExisteServicioProducto(producto, 2, posicion)) Then
                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin") = dt.Rows(i).Item("lccant") + CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin")

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc") = dt.Rows(i).Item("lcmdes") + CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc")

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") + (dt.Rows(i).Item("lcpuni") * dt.Rows(i).Item("lccant"))

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") - CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc")
                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdpbas") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") / CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin")


                Else
                    CType(grdetalle.DataSource, DataTable).Rows.Add()
                    Dim pos As Integer = CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdnumi") = _fnSiguienteNumi()
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdserv") = dt.Rows(i).Item("lctce4pro")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdprod") = dt.Rows(i).Item("lctcl3pro")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descripcion") = dt.Rows(i).Item("eddesc")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = dt.Rows(i).Item("lccant")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = dt.Rows(i).Item("lcpuni")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = dt.Rows(i).Item("lcpuni") * dt.Rows(i).Item("lccant")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdporc") = dt.Rows(i).Item("lcpdes")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vddesc") = dt.Rows(i).Item("lcmdes")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = dt.Rows(i).Item("lcptot")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 0
                End If
            End If
        Next

        Dim dtDetalle As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim Descuento As Double = 0
        For i As Integer = 0 To dtDetalle.Rows.Count - 1 Step 1
            Descuento = Descuento + dtDetalle.Rows(i).Item("vddesc")
        Next
        tbMdesc.Value = Descuento
        _prCalcularPrecioTotalLavadero()
        If (cbsector.Value <> 3) Then
            TbNit.Focus()
        End If
    End Sub


    Public Sub _CargarDetalleVentaAyudaRemolque(numi As Integer)
        Dim dt As DataTable = L_fnDetalleAyudaRemolque(numi)
        'a.rflin  , a.rfnumi, a.rfTCE04Serv, b.eddesc, a.rfprec, 1 As cantidad, a.rfpdesc, a.rfmdesc, (a.rfprec - a.rfmdesc)as subtotal 
        'CType(grdetalle.DataSource, DataTable).Rows.Clear()
        Dim posicion As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            'a.rflin  , a.rfnumi, a.rfTCE04Serv, b.eddesc, a.rfprec, 1 As cantidad, a.rfpdesc, a.rfmdesc, (a.rfprec - a.rfmdesc)as subtotal 
            Dim servicio As Integer = IIf(IsDBNull(dt.Rows(i).Item("rfTCE04Serv")), 0, dt.Rows(i).Item("rfTCE04Serv"))

            If (servicio > 0) Then
                If (_fnExisteServicioProducto(servicio, 1, posicion)) Then
                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin") = dt.Rows(i).Item("cantidad") + CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin")

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc") = dt.Rows(i).Item("rfmdesc") + CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc")

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") + (dt.Rows(i).Item("rfprec") * dt.Rows(i).Item("cantidad"))

                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") - CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vddesc")
                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdpbas") = CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdptot") / CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("vdcmin")


                Else
                    CType(grdetalle.DataSource, DataTable).Rows.Add()
                    Dim pos As Integer = CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdnumi") = _fnSiguienteNumi()
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdserv") = dt.Rows(i).Item("rfTCE04Serv")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdprod") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descripcion") = dt.Rows(i).Item("eddesc")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = dt.Rows(i).Item("cantidad")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = dt.Rows(i).Item("rfprec")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = dt.Rows(i).Item("rfprec") * dt.Rows(i).Item("cantidad")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdporc") = dt.Rows(i).Item("rfpdesc")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vddesc") = dt.Rows(i).Item("rfmdesc")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = dt.Rows(i).Item("subtotal")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 0
                End If
            End If






        Next

        _prCalcularPrecioTotal()
        TbNit.Focus()


    End Sub

    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs)
        If (swTipoVenta.Value = False) Then
            lbCredito.Visible = True
            tbFechaVenc.Visible = True

            lbClienteCredito.Visible = True
            tbClienteCredito.Visible = True
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
            tbFechaVenc.Value = Now.Date
            lbClienteCredito.Visible = False
            tbClienteCredito.Visible = False
        End If
    End Sub

    Private Sub tbPdesc_ValueChanged(sender As Object, e As EventArgs) Handles tbPdesc.ValueChanged
        If (tbPdesc.Focused) Then
            If (Not tbPdesc.Text = String.Empty And Not tbtotal.Text = String.Empty) Then
                If (tbPdesc.Value = 0 Or tbPdesc.Value > 100) Then
                    tbPdesc.Value = 0
                    tbMdesc.Value = 0

                    _prCalcularPrecioTotal()

                Else

                    Dim porcdesc As Double = tbPdesc.Value
                    Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) * (porcdesc / 100))
                    tbMdesc.Value = montodesc


                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc

                End If


            End If
            If (tbPdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If
    End Sub

    Private Sub tbMdesc_ValueChanged(sender As Object, e As EventArgs) Handles tbMdesc.ValueChanged
        If (tbMdesc.Focused) Then

            Dim total As Double = tbtotal.Value
            If (Not tbMdesc.Text = String.Empty And Not tbMdesc.Text = String.Empty) Then
                If (tbMdesc.Value = 0 Or tbMdesc.Value > total) Then
                    tbMdesc.Value = 0
                    tbPdesc.Value = 0
                    _prCalcularPrecioTotal()
                Else
                    Dim montodesc As Double = tbMdesc.Value
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum))
                    tbPdesc.Value = pordesc

                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc


                End If

            End If

            If (tbMdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If

    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then
            _GuardarNuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prGuardarModificado()
                ''    _prInhabiliitar()

            End If
        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (grVentas.RowCount > 0) Then
            'If (gb_FacturaEmite) Then
            '    If (Not P_fnValidarFacturaVigente()) Then
            'Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            'ToastNotification.Show(Me, "No se puede modificar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta anulada.".ToUpper,
            '                          img, 2000,
            '                          eToastGlowColor.Green,
            '                          eToastPosition.TopCenter)
            'Exit Sub
            'End If
            '    End If

            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelNavegacion.Enabled = False
            tbTipoVenta.IsReadOnly = True
            If (gb_userTodasSuc = False And CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then


                cbSucursal.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
                cbSucursal.ReadOnly = True
            Else
                cbSucursal.ReadOnly = False
            End If
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
    Private Function P_fnValidarFacturaVigente() As Boolean
        Dim est As String = L_fnObtenerDatoTabla("TFV001", "fvaest", "fvanumi=" + tbCodigo.Text.Trim)
        Return (est.Equals("True") Or est = String.Empty)
    End Function
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If (NroCompronbante > 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            ToastNotification.Show(Me, "No se puede eliminar la venta con codigo ".ToUpper + tbCodigo.Text + ", por que esta siendo usado en un comprobante.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)
            Exit Sub
        End If
        'If (gb_FacturaEmite) Then
        '    If (P_fnValidarFacturaVigente()) Then
        '        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

        '        ToastNotification.Show(Me, "No se puede eliminar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta vigente.".ToUpper,
        '                                  img, 2000,
        '                                  eToastGlowColor.Green,
        '                                  eToastPosition.TopCenter)
        '        Exit Sub
        '    End If
        'End If

        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarVentaLavadero(tbCodigo.Text, mensajeError, cbsector.Value, tbsecnumi.Text, _CodVehiculo, cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodCliente, tbFechaVenc.Value.ToString("yyyy/MM/dd"), IIf(swTipoVenta.Value = True, 1, 0), 0, tbObservacion.Text, tbMdesc.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable))
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _prFiltrar()

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If

    End Sub

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then

            _prMostrarRegistro(grVentas.Row)
        End If


    End Sub
    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grVentas.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub

    Private Sub TbNit_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TbNit.Validating
        Dim nom1, nom2 As String
        nom1 = ""
        nom2 = ""
        If (TbNit.Text.Trim = String.Empty) Then
            TbNit.Text = "0"
            TbNombre1.Text = "S/N"
        End If
        If (L_Validar_Nit(TbNit.Text.Trim, nom1, nom2)) Then
            TbNombre1.Text = nom1

        End If
        '' TbNombre1.Text = nom1

    End Sub


    Private Sub swTipoVenta_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub


    Private Sub swTipoVenta_Leave(sender As Object, e As EventArgs)
        grdetalle.Select()
    End Sub
    Public Function _CompletarNroFactura(numero As Integer) As String
        Dim n As Integer = 7 - numero.ToString.Length
        Dim cadena As String = "" + Str(numero).Trim
        For i As Integer = 0 To n Step 1
            cadena = "0" + cadena
        Next
        Return cadena
    End Function
    Public Function _CompletarMonth(numero As Integer) As String
        If (numero < 10) Then
            Return "0".Trim + Str(numero).Trim
        Else
            Return Str(numero).Trim
        End If
    End Function

    Private Sub P_prImprimirFacturarModificado(numi As String, impFactura As Boolean, grabarPDF As Boolean, nroFactura As String)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds2, _Ds3 As New DataSet
        Dim _Ds1 As DataTable
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0
        _Desc = CDbl(tbMdesc.Value)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Fecha = tbFechaVenta.Value '.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        '_Ds1 = L_Dosificacion("1", cbSucursal.Value, _Fecha.ToString("yyyy/MM/dd"))
        _Ds1 = L_DosificacionAutomatica(cbSucursal.Value, _Fecha.ToString("yyyy/MM/dd"), cbsector.Value)
        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Rows(0).Item("sbautoriz").ToString
        _NumFac = CInt(nroFactura)
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = _Fecha.Year.ToString +
                   _CompletarMonth(_Fecha.Month).Trim +
                   _CompletarMonth(_Fecha.Day).Trim
        '_Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
        '            Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
        '            Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Rows(0).Item("sbnumi").ToString
        _Key = _Ds1.Rows(0).Item("sbkey")
        _FechaAl = _Ds1.Rows(0).Item("sbfal")

        _NumFac = CInt(nroFactura)

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("1")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + _CompletarNroFactura(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + Decimal.Round(ice, 2).ToString + "|0.00|0.00|" + Decimal.Round(_Desc, 2).ToString

        L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(_NumFac),
                            CStr(_Autorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            _Cod_Control,
                            _FechaAl.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))

        _Ds = L_Reporte_Factura(numi, numi)
        Dim dt As DataTable = L_fnFacturaLavadero(numi)
        For j As Integer = 0 To dt.Rows.Count - 1 Step 1
            dt.Rows(j).Item("img") = P_fnImageToByteArray(QrFactura.Image)
        Next
        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        If (impFactura) Then
            _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador = New Visualizador 'Comentar
            End If
            Dim objrep As Object = Nothing
            If (gi_FacturaTipo = 1) Then
                'objrep = New R_FacturaG
            ElseIf (gi_FacturaTipo = 2) Then
                objrep = New R_Mam_FacturaLavadero
                If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
                    For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
                        'Insertamos la primera fila con el saldo Inicial
                        Dim f As DataRow = _Ds.Tables(0).NewRow
                        f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
                        f.Item("fvbcant") = -1
                        _Ds.Tables(0).Rows.Add(f)
                    Next
                End If
            End If

            Dim dtActividad As DataTable = L_ObtenerActividadEconomica(cbsector.Value)
            Dim ActividadEconomica As String = ""
            If (dtActividad.Rows.Count > 0) Then
                ActividadEconomica = dtActividad.Rows(0).Item(0)

            End If


            objrep.SetDataSource(dt)
            objrep.SetParameterValue("nroFactura", _CompletarNroFactura(_Ds.Tables(0).Rows(0).Item("fvanfac")))
            objrep.SetParameterValue("nroAutorizacion", _Ds.Tables(0).Rows(0).Item("fvaautoriz"))
            objrep.SetParameterValue("MensajeContribuyente", "''" + _Ds1.Rows(0).Item("sbnota").ToString + "''.")
            objrep.SetParameterValue("nit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
            objrep.SetParameterValue("lugarFecha", "Cochabamba, " + Str(tbFechaVenta.Value.Day) + " De " + MonthName(tbFechaVenta.Value.Month) + " De " + Str(tbFechaVenta.Value.Year))
            objrep.SetParameterValue("nombreFactura", TbNombre1.Text)
            objrep.SetParameterValue("nitCliente", TbNit.Text)
            objrep.SetParameterValue("TotalBs", _Literal)
            objrep.SetParameterValue("CodeControl", _Ds.Tables(0).Rows(0).Item("fvaccont"))
            Dim FechaEmision As Date = _Ds.Tables(0).Rows(0).Item("fvaflim")
            objrep.SetParameterValue("FechaLimiteEmision", _CompletarMonth(FechaEmision.Day).Trim + "/" + _CompletarMonth(FechaEmision.Month).Trim + "/" + _CompletarMonth(FechaEmision.Year).Trim)
            objrep.SetParameterValue("glosa", tbObservacion.Text)
            objrep.SetParameterValue("actividadEconomica", ActividadEconomica)
            objrep.SetParameterValue("mensaje2", _Ds1.Rows(0).Item("sbnota2").ToString)

            Dim dtSucursal As DataTable = L_fnDosificacionObtenerDatosSucursal(cbSucursal.Value)
            If dtSucursal.Rows.Count > 0 Then
                objrep.SetParameterValue("nroSucursal", dtSucursal.Rows(0).Item("caconcep1").ToString)
                objrep.SetParameterValue("direcSucursal", dtSucursal.Rows(0).Item("caconcep2").ToString)
                objrep.SetParameterValue("departamentoSuc", dtSucursal.Rows(0).Item("caconcep3").ToString)
            Else
                objrep.SetParameterValue("nroSucursal", "")
                objrep.SetParameterValue("direcSucursal", "")
                objrep.SetParameterValue("departamentoSuc", "")
            End If

            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
                P_Global.Visualizador.ShowDialog() 'Comentar
                P_Global.Visualizador.BringToFront() 'Comentar
            End If

            Dim pd As New PrintDocument()
            'pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                'objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                'objrep.PrintToPrinter(1, False, 1, 1)

            End If

            If (grabarPDF) Then
                'Copia de Factura en PDF
                If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                    Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
                End If
                Try
                    objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")
                Catch ex As Exception

                End Try


            End If
        End If
        'L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
    End Sub
    Private Sub P_prImprimirFacturar(numi As String, impFactura As Boolean, grabarPDF As Boolean, carta As Boolean)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds2, _Ds3 As New DataSet
        Dim _Ds1 As DataTable
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0
        _Desc = CDbl(tbMdesc.Value)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Fecha = tbFechaVenta.Value '.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        _Ds1 = L_DosificacionAutomatica(cbSucursal.Value, _Fecha.ToString("yyyy/MM/dd"), cbsector.Value)
        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Rows(0).Item("sbautoriz").ToString
        _NumFac = CInt(_Ds1.Rows(0).Item("sbnfac")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = _Fecha.Year.ToString +
                   _CompletarMonth(_Fecha.Month).Trim +
                   _CompletarMonth(_Fecha.Day).Trim
        '_Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
        '            Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
        '            Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Rows(0).Item("sbnumi").ToString
        _Key = _Ds1.Rows(0).Item("sbkey")
        _FechaAl = _Ds1.Rows(0).Item("sbfal")

        Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        _NumFac = maxNFac + 1

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("1")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + _CompletarNroFactura(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + Decimal.Round(ice, 2).ToString + "|0.00|0.00|" + Decimal.Round(_Desc, 2).ToString

        L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(_NumFac),
                            CStr(_Autorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            _Cod_Control,
                            _FechaAl.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))

        _Ds = L_Reporte_Factura(numi, numi)
        Dim sector As Integer = cbsector.Value
        Dim dtActividad As DataTable = L_ObtenerActividadEconomica(sector)
        Dim ActividadEconomica As String = ""
        If (dtActividad.Rows.Count > 0) Then
            ActividadEconomica = dtActividad.Rows(0).Item(0)

        End If

        Dim dt As DataTable = L_fnFacturaLavadero(numi)
        For j As Integer = 0 To dt.Rows.Count - 1 Step 1
            dt.Rows(j).Item("img") = P_fnImageToByteArray(QrFactura.Image)
        Next
        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        If (impFactura) Then
            _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador = New Visualizador 'Comentar
            End If
            Dim objrep As Object = Nothing
            If (gi_FacturaTipo = 1) Then
                'objrep = New R_FacturaG
            ElseIf (gi_FacturaTipo = 2) Then

                If (carta = True) Then
                    objrep = New R_Mam_FacturaLavadero_CartaCompleta

                Else
                    objrep = New R_Mam_FacturaLavadero

                End If



                If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
                    For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
                        'Insertamos la primera fila con el saldo Inicial
                        Dim f As DataRow = _Ds.Tables(0).NewRow
                        f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
                        f.Item("fvbcant") = -1
                        _Ds.Tables(0).Rows.Add(f)
                    Next
                End If
            End If


            objrep.SetDataSource(dt)
            objrep.SetParameterValue("nroFactura", _CompletarNroFactura(_Ds.Tables(0).Rows(0).Item("fvanfac")))
            objrep.SetParameterValue("nroAutorizacion", _Ds.Tables(0).Rows(0).Item("fvaautoriz"))
            objrep.SetParameterValue("MensajeContribuyente", "''" + _Ds1.Rows(0).Item("sbnota").ToString + "''.")
            objrep.SetParameterValue("nit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
            objrep.SetParameterValue("lugarFecha", "Cochabamba, " + Str(tbFechaVenta.Value.Day) + " De " + MonthName(tbFechaVenta.Value.Month) + " De " + Str(tbFechaVenta.Value.Year))
            objrep.SetParameterValue("nombreFactura", TbNombre1.Text)
            objrep.SetParameterValue("nitCliente", TbNit.Text)
            objrep.SetParameterValue("TotalBs", _Literal)
            objrep.SetParameterValue("CodeControl", _Ds.Tables(0).Rows(0).Item("fvaccont"))
            Dim FechaEmision As Date = _Ds.Tables(0).Rows(0).Item("fvaflim")
            objrep.SetParameterValue("FechaLimiteEmision", _CompletarMonth(FechaEmision.Day).Trim + "/" + _CompletarMonth(FechaEmision.Month).Trim + "/" + _CompletarMonth(FechaEmision.Year).Trim)
            objrep.SetParameterValue("glosa", tbObservacion.Text)
            objrep.SetParameterValue("mensaje2", _Ds1.Rows(0).Item("sbnota2").ToString)
            objrep.SetParameterValue("actividadEconomica", ActividadEconomica)
            Dim dtSucursal As DataTable = L_fnDosificacionObtenerDatosSucursal(cbSucursal.Value)
            If dtSucursal.Rows.Count > 0 Then
                objrep.SetParameterValue("nroSucursal", dtSucursal.Rows(0).Item("caconcep1").ToString)
                objrep.SetParameterValue("direcSucursal", dtSucursal.Rows(0).Item("caconcep2").ToString)
                objrep.SetParameterValue("departamentoSuc", dtSucursal.Rows(0).Item("caconcep3").ToString)
            Else
                objrep.SetParameterValue("nroSucursal", "")
                objrep.SetParameterValue("direcSucursal", "")
                objrep.SetParameterValue("departamentoSuc", "")
            End If


            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
                P_Global.Visualizador.ShowDialog() 'Comentar
                P_Global.Visualizador.BringToFront() 'Comentar
            End If

            Dim pd As New PrintDocument()
            'pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                'objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                'objrep.PrintToPrinter(1, False, 1, 1)

            End If

            If (grabarPDF) Then
                'Copia de Factura en PDF
                If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                    Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
                End If
                Try
                    objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")
                Catch ex As Exception

                End Try


            End If
        End If

        L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
    End Sub

    Private Sub P_prImprimirFacturaCopia(numi As String, carta As Boolean)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0
        '_Desc = CDbl(tbMdesc.Value)
        'If Not IsNothing(P_Global.Visualizador) Then
        '    P_Global.Visualizador.Close()
        'End If

        _Fecha = tbFechaVenta.Value '.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        _Ds1 = L_Dosificacion("1", cbSucursal.Value, _Fecha.ToString("yyyy/MM/dd"))

        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
        _NumFac = tbNroFactura.Text 'CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = _Fecha.Year.ToString +
                   _CompletarMonth(_Fecha.Month).Trim +
                   _CompletarMonth(_Fecha.Day).Trim
        '_Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
        '            Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
        '            Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Tables(0).Rows(0).Item("sbnumi").ToString
        _Key = _Ds1.Tables(0).Rows(0).Item("sbkey")
        _FechaAl = _Ds1.Tables(0).Rows(0).Item("sbfal")

        'Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        '_NumFac = maxNFac + 1

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = tbCodigoControl.Text

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("1")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + _CompletarNroFactura(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + Decimal.Round(ice, 2).ToString + "|0.00|0.00|" + Decimal.Round(_Desc, 2).ToString



        _Ds = L_Reporte_Factura(numi, numi)
        Dim sector As Integer = cbsector.Value
        Dim dtActividad As DataTable = L_ObtenerActividadEconomica(sector)
        Dim ActividadEconomica As String = ""
        If (dtActividad.Rows.Count > 0) Then
            ActividadEconomica = dtActividad.Rows(0).Item(0)

        End If
        Dim dt As DataTable = L_fnFacturaLavadero(numi)
        For j As Integer = 0 To dt.Rows.Count - 1 Step 1
            dt.Rows(j).Item("img") = P_fnImageToByteArray(QrFactura.Image)
        Next
        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        If (True) Then
            _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador = New Visualizador 'Comentar
            End If
            Dim objrep As Object = Nothing
            If (gi_FacturaTipo = 1) Then
                'objrep = New R_FacturaG
            ElseIf (gi_FacturaTipo = 2) Then
                If (carta = True) Then
                    objrep = New R_Mam_FacturaLavadero_CartaCompleta
                    If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
                        For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
                            'Insertamos la primera fila con el saldo Inicial
                            Dim f As DataRow = _Ds.Tables(0).NewRow
                            f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
                            f.Item("fvbcant") = -1
                            _Ds.Tables(0).Rows.Add(f)
                        Next
                    End If
                Else
                    objrep = New R_Mam_FacturaLavadero
                    If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
                        For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
                            'Insertamos la primera fila con el saldo Inicial
                            Dim f As DataRow = _Ds.Tables(0).NewRow
                            f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
                            f.Item("fvbcant") = -1
                            _Ds.Tables(0).Rows.Add(f)
                        Next
                    End If
                End If

            End If


            objrep.SetDataSource(dt)
            objrep.SetParameterValue("nroFactura", _CompletarNroFactura(_Ds.Tables(0).Rows(0).Item("fvanfac")))
            objrep.SetParameterValue("nroAutorizacion", _Ds.Tables(0).Rows(0).Item("fvaautoriz"))
            objrep.SetParameterValue("MensajeContribuyente", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota").ToString + "''.")
            objrep.SetParameterValue("nit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
            objrep.SetParameterValue("lugarFecha", "Cochabamba, " + Str(tbFechaVenta.Value.Day) + " De " + MonthName(tbFechaVenta.Value.Month) + " De " + Str(tbFechaVenta.Value.Year))
            objrep.SetParameterValue("nombreFactura", TbNombre1.Text)
            objrep.SetParameterValue("nitCliente", TbNit.Text)
            objrep.SetParameterValue("TotalBs", _Literal)
            objrep.SetParameterValue("CodeControl", _Ds.Tables(0).Rows(0).Item("fvaccont"))
            Dim FechaEmision As Date = _Ds.Tables(0).Rows(0).Item("fvaflim")
            objrep.SetParameterValue("FechaLimiteEmision", _CompletarMonth(FechaEmision.Day).Trim + "/" + _CompletarMonth(FechaEmision.Month).Trim + "/" + _CompletarMonth(FechaEmision.Year).Trim)
            objrep.SetParameterValue("glosa", tbObservacion.Text)
            objrep.SetParameterValue("mensaje2", _Ds1.Tables(0).Rows(0).Item("sbnota2").ToString)
            objrep.SetParameterValue("actividadEconomica", ActividadEconomica)
            Dim dtSucursal As DataTable = L_fnDosificacionObtenerDatosSucursal(cbSucursal.Value)
            If dtSucursal.Rows.Count > 0 Then
                objrep.SetParameterValue("nroSucursal", dtSucursal.Rows(0).Item("caconcep1").ToString)
                objrep.SetParameterValue("direcSucursal", dtSucursal.Rows(0).Item("caconcep2").ToString)
                objrep.SetParameterValue("departamentoSuc", dtSucursal.Rows(0).Item("caconcep3").ToString)
            Else
                objrep.SetParameterValue("nroSucursal", "")
                objrep.SetParameterValue("direcSucursal", "")
                objrep.SetParameterValue("departamentoSuc", "")
            End If


            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
                P_Global.Visualizador.ShowDialog() 'Comentar
                P_Global.Visualizador.BringToFront() 'Comentar
            End If

            Dim pd As New PrintDocument()
            'pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                'objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                'objrep.PrintToPrinter(1, False, 1, 1)

            End If

            'If (grabarPDF) Then
            '    'Copia de Factura en PDF
            '    If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
            '        Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
            '    End If
            '    Try
            '        objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")
            '    Catch ex As Exception

            '    End Try


            'End If
        End If

        'L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
    End Sub

    Private Sub P_prImprimirRecibo(numi As String, impFactura As Boolean, grabarPDF As Boolean)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0
        '_Desc = CDbl(tbMdesc.Value)
        'If Not IsNothing(P_Global.Visualizador) Then
        '    P_Global.Visualizador.Close()
        'End If

        '_Fecha = tbFechaVenta.Value '.ToString("dd/MM/yyyy")
        '_Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        '_Ds1 = L_Dosificacion("1", cbSucursal.Value, _Fecha)

        '_Ds = L_Reporte_Factura(numi, numi)
        '_Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
        '_NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac")) + 1
        '_Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        '_Fechainv = _Fecha.Year.ToString +
        '           _CompletarMonth(_Fecha.Month).Trim +
        '           _CompletarMonth(_Fecha.Day).Trim
        ''_Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
        ''            Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
        ''            Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        '_Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        'ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        '_numidosif = _Ds1.Tables(0).Rows(0).Item("sbnumi").ToString
        '_Key = _Ds1.Tables(0).Rows(0).Item("sbkey")
        '_FechaAl = _Ds1.Tables(0).Rows(0).Item("sbfal")

        'Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        '_NumFac = maxNFac + 1

        '_TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        '_Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = tbtotal.Value
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        ''Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        Dim cad1 As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)).ToString()

        _Literal = cad1 + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        '_Ds2 = L_Reporte_Factura_Cia("1")
        'QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + _CompletarNroFactura(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + Decimal.Round(ice, 2).ToString + "|0.00|0.00|" + Decimal.Round(_Desc, 2).ToString

        'L_Modificar_Factura("fvanumi = " + CStr(numi),
        '                    "",
        '                    CStr(_NumFac),
        '                    CStr(_Autorizacion),
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    "",
        '                    _Cod_Control,
        '                    _FechaAl.ToString("yyyy/MM/dd"),
        '                    "",
        '                    "",
        '                    CStr(numi))

        '_Ds = L_Reporte_Factura(numi, numi)
        Dim dt As DataTable = L_fnFacturaLavadero(numi)
        'For j As Integer = 0 To dt.Rows.Count - 1 Step 1
        '    dt.Rows(j).Item("img") = P_fnImageToByteArray(QrFactura.Image)
        'Next
        'For I = 0 To _Ds.Tables(0).Rows.Count - 1
        '    _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        'Next
        If (impFactura) Then
            _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador = New Visualizador 'Comentar
            End If
            Dim objrep As Object = Nothing
            'If (gi_FacturaTipo = 1) Then
            '    'objrep = New R_FacturaG
            'ElseIf (gi_FacturaTipo = 2) Then
            '    objrep = New R_Mam_FacturaLavadero
            '    If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
            '        For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
            '            'Insertamos la primera fila con el saldo Inicial
            '            Dim f As DataRow = _Ds.Tables(0).NewRow
            '            f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
            '            f.Item("fvbcant") = -1
            '            _Ds.Tables(0).Rows.Add(f)
            '        Next
            '    End If
            'End If
            If CbOP.Checked = False Then
                objrep = New R_Mam_ReciboLavadero
            Else
                objrep = New R_NotaEntregaOptimo
            End If
            Dim _tc As Decimal
            Dim dtTipoCambio As DataTable = L_prTipoCambioGeneralPorFecha(tbFechaVenta.Value.ToString("yyyy/MM/dd"))
            If dtTipoCambio.Rows.Count = 0 Then
                '_existTipoCambio = False
                _tc = 0

            Else
                '_existTipoCambio = True

                _tc = dtTipoCambio.Rows(0).Item("cbdol")
            End If
            If CbOP.Checked = False Then
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("nroFactura", "")
                objrep.SetParameterValue("nroAutorizacion", "")
                objrep.SetParameterValue("MensajeContribuyente", "")
                objrep.SetParameterValue("nit", "")
                objrep.SetParameterValue("lugarFecha", tbFechaVenta.Value.ToString("dd/MM/yyyy"))
                objrep.SetParameterValue("CodeControl", "")
                objrep.SetParameterValue("FechaLimiteEmision", "")
                objrep.SetParameterValue("mensaje2", "")
            Else
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("nombreFactura", TbNombre1.Text)
                objrep.SetParameterValue("nitCliente", TbNit.Text)
                objrep.SetParameterValue("TotalBs", _Literal)
                objrep.SetParameterValue("glosa", tbObservacion.Text)
                objrep.SetParameterValue("descuento", tbPdesc.Value)
                objrep.SetParameterValue("contado", IIf(swTipoVenta.Value = True, "CONTADO", "CREDITO"))
                objrep.SetParameterValue("vendedor", gs_user)
                objrep.SetParameterValue("sucursal", cbSucursal.Text)
                objrep.SetParameterValue("numiVenta", numi)
                objrep.SetParameterValue("tc", _tc)
            End If
            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                    P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
                    P_Global.Visualizador.ShowDialog() 'Comentar
                    P_Global.Visualizador.BringToFront() 'Comentar
                End If

                Dim pd As New PrintDocument()
                'pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                If (Not pd.PrinterSettings.IsValid) Then
                    ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                           My.Resources.WARNING, 5 * 1000,
                                           eToastGlowColor.Blue, eToastPosition.BottomRight)
                Else
                    'objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                    'objrep.PrintToPrinter(1, False, 1, 1)

                End If

                'If (grabarPDF) Then
                '    'Copia de Factura en PDF
                '    If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                '        Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
                '    End If
                '    Try
                '        objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")
                '    Catch ex As Exception

                '    End Try


                'End If
            End If
        'L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)    
    End Sub

    Private Sub TbNombre1_KeyDown(sender As Object, e As KeyEventArgs) Handles TbNombre1.KeyDown
        If e.KeyData = Keys.Enter Then
            'btnGrabar.Focus()
            'btnGrabar.Select()
            'btnGrabar.Focus()
            PanelToolBar1.Select()

            btnGrabar.Focus()




        End If
    End Sub

    Private Sub cbsector_ValueChanged(sender As Object, e As EventArgs) Handles cbsector.ValueChanged
        If (_fnAccesible()) Then
            If (cbsector.Value = -10) Then
                _prCargarTablaNroOrdenLavaderoCabana("-1", 17) 'CODIGO DANNY
                '_prCargarTablaNroOrdenLavadero("-1", 16)

            Else
                _prCargarTablaNroOrdenLavadero("-1", 16)
            End If

            If (cbsector.Value = 3 Or cbsector.Value = 4 Or cbsector.Value = -10 Or cbsector.Value = 8) Then
                CType(grdetalle.DataSource, DataTable).Rows.Clear()
                SuperTabItem2.Visible = True
            End If
            If (cbsector.Value = 2 Or cbsector.Value = 1 Or cbsector.Value = -5 Or cbsector.Value > 4) Then
                If (Not cbsector.Value = 8) Then
                    CType(grdetalle.DataSource, DataTable).Rows.Clear()
                    _prAddDetalleVenta()
                    SuperTabItem2.Visible = False
                End If

            End If
            If (cbsector.ReadOnly = False) Then
                '_Limpiar()
            End If
            If (GPanelProductos.Visible = True) Then
                GPanelProductos.Visible = False

                PanelInferior.Visible = True
                PanelTotal.Visible = True

            End If
        End If
        If (cbsector.Value = 3 Or cbsector.Value = 4 Or cbsector.Value = -10 Or cbsector.Value = 8) Then

            SuperTabItem2.Visible = True
        End If
        If (cbsector.Value = 2 Or cbsector.Value = 1 Or cbsector.Value = -5 Or cbsector.Value > 4) Then


            SuperTabItem2.Visible = False
        End If
        If cbsector.Value = -5 Then
            cbsector.Value = -10
        End If
    End Sub

    Private Sub _prAddDetalleVenta()
        '    [vdnumi],[vdvc1numi],[vdserv],[vdprod],
        '[descripcion],[vdcmin],[vdpbas],[vdptot],
        '[vdporc],[vddesc] [Decimal](18, 2) NULL,
        '[vdtotdesc] [Decimal](18, 2) NULL,
        '[vdobs] [nvarchar](30) NULL,
        '[vdpcos] [Decimal](18, 2) NULL,
        '[vdptot2] [Decimal](18, 2) NULL,
        '[estado] [Int] NULL

        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, 0, "", 0, 0, 0, 0, 0, 0, "", 0, 0, 0)
    End Sub
    Private Sub _HabilitarProductos()
        GPanelProductos.Visible = True

        PanelInferior.Visible = False
        PanelTotal.Visible = False
        _prCargarTablaServicios()
        grServicios.Focus()
        grServicios.MoveTo(grServicios.FilterRow)
        grServicios.Col = 1
    End Sub
    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If

        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grdetalle.Col
            f = grdetalle.Row

            If (grdetalle.Col = grdetalle.RootTable.Columns("vdcmin").Index) Then
                If (grdetalle.GetValue("descripcion").ToString <> String.Empty) Then
                    _prAddDetalleVenta()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Servicio Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("descripcion").Index) Then
                If (grdetalle.GetValue("descripcion").ToString <> String.Empty) Then
                    _prAddDetalleVenta()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Servicio Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
salirIf:
        End If

        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("descripcion").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            _HabilitarProductos()

        End If
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then
            _prEliminarFila()
        End If

    End Sub
    Private Sub _DesHabilitarProductos()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelInferior.Visible = True
            grdetalle.Select()
            grdetalle.Col = 4
            grdetalle.Row = grdetalle.RowCount - 1
            PanelTotal.Visible = True
        End If

    End Sub



    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible() And (cbsector.Value = 6 Or cbsector.Value = 1 Or cbsector.Value = 2 Or cbsector.Value = -10) Or tbTipoVenta.Value = False) Then '(cbsector.Value > 4 Or cbsector.Value = 1)
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("vdcmin").Index Or e.Column.Index = grdetalle.RootTable.Columns("vdpbas").Index) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            If False Then 'precio danny cbSucursal.Value = 7
                If (e.Column.Index = grdetalle.RootTable.Columns("vdcmin").Index Or e.Column.Index = grdetalle.RootTable.Columns("vdpbas").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                e.Cancel = True
            End If
        End If
    End Sub
    Public Function _fnExisteProducto(idserv As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("vdserv")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idserv And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function
    Private Sub grServicios_KeyDown(sender As Object, e As KeyEventArgs) Handles grServicios.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grServicios.Col
            f = grServicios.Row
            If (f >= 0) Then

                If (grServicios.GetValue("estado") = 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                    ToastNotification.Show(Me, "El Servicio no tiene configurado un numero de cuenta".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    grServicios.RemoveFilters()
                    grServicios.Focus()
                    grServicios.MoveTo(grServicios.FilterRow)
                    grServicios.Col = 1
                    Return
                End If

                If grServicios.GetValue("sdnumi") = 1 Or grServicios.GetValue("sdnumi") = 2 Then
                    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                    ToastNotification.Show(Me, "el servicio solo puede ser usado en el programa pago de socios del Dies".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    Return
                End If

                grdetalle.Row = grdetalle.RowCount - 1
                If ((grdetalle.GetValue("vdserv") > 0)) Then
                    _prAddDetalleVenta()
                End If

                Dim pos As Integer = -1
                grdetalle.Row = grdetalle.RowCount - 1
                _fnObtenerFilaDetalle(pos, grdetalle.GetValue("vdnumi"))


                Dim existe As Boolean = _fnExisteProducto(grServicios.GetValue("sdnumi"))
                If ((pos >= 0)) Then 'And (Not existe)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdserv") = grServicios.GetValue("sdnumi")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descripcion") = grServicios.GetValue("sddesc")


                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = grServicios.GetValue("sdprec")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grServicios.GetValue("sdprec")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grServicios.GetValue("sdprec")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot2") = grServicios.GetValue("sdprec")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = 1
                    _prCargarTablaServicios()
                    grServicios.RemoveFilters()
                    grServicios.Focus()
                    grServicios.MoveTo(grServicios.FilterRow)
                    grServicios.Col = 1

                Else
                    If (existe) Then
                        Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                        ToastNotification.Show(Me, "El Servicio ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        grServicios.RemoveFilters()
                        grServicios.Focus()
                        grServicios.MoveTo(grServicios.FilterRow)
                        grServicios.Col = 1
                    End If
                End If
            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
            _prCalcularPrecioTotal()
        End If
    End Sub

    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited


        If (e.Column.Index = grdetalle.RootTable.Columns("vdcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("vdcmin")) Or grdetalle.GetValue("vdcmin").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("vdnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = 1
                grdetalle.SetValue("vdcmin", 1)
                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grdetalle.GetValue("vdpbas")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            Else
                If (grdetalle.GetValue("vdcmin") > 0) Then
                    Dim lin As Integer = grdetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grdetalle.GetValue("vdpbas") * grdetalle.GetValue("vdcmin")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grdetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = 1
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grdetalle.GetValue("vdpbas")
                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If
                    grdetalle.SetValue("vdcmin", 1)
                End If
            End If
        End If


        If (e.Column.Index = grdetalle.RootTable.Columns("vdpbas").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("vdpbas")) Or grdetalle.GetValue("vdpbas").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("vdnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = grdetalle.GetValue("vdptot2")

                grdetalle.SetValue("vdcmin", grdetalle.GetValue("vdptot2"))
                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grdetalle.GetValue("vdptot2") * grdetalle.GetValue("vdcmin")

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grdetalle.GetValue("vdptot2") * grdetalle.GetValue("vdcmin")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            Else
                If (grdetalle.GetValue("vdpbas") > 0) Then
                    Dim lin As Integer = grdetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grdetalle.GetValue("vdpbas") * grdetalle.GetValue("vdcmin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grdetalle.GetValue("vdpbas") * grdetalle.GetValue("vdcmin")
                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grdetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = grdetalle.GetValue("vdptot2")

                    grdetalle.SetValue("vdcmin", grdetalle.GetValue("vdptot2"))
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grdetalle.GetValue("vdptot2") * grdetalle.GetValue("vdcmin")

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grdetalle.GetValue("vdptot2") * grdetalle.GetValue("vdcmin")
                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                End If
            End If
        End If
        _prCalcularPrecioTotal()

    End Sub



    Private Sub cbSucursal_ValueChanged(sender As Object, e As EventArgs) Handles cbSucursal.ValueChanged
        If ((cbsector.Value = 1 Or cbsector.Value > 4) And cbsector.ReadOnly = False) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            _prAddDetalleVenta()
            If (GPanelProductos.Visible = True) Then
                GPanelProductos.Visible = False

                PanelInferior.Visible = True
                PanelTotal.Visible = True

            End If


        End If


    End Sub


    Private Sub cbsector_KeyDown(sender As Object, e As KeyEventArgs) Handles cbsector.KeyDown
        If e.KeyData = Keys.Enter Then
            If (cbsector.Value <> 3 And cbsector.Value <> 4 And cbsector.Value <> -10) Then
                grdetalle.Select()

            Else
                SuperTabControl1.SelectedTabIndex = 1
                grventasLavadero.Focus()
                grventasLavadero.MoveTo(grventasLavadero.FilterRow)
                grventasLavadero.Col = 0

            End If


        End If

    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter
        grdetalle.Focus()
        If (grdetalle.RowCount <= 0) Then
            _prAddDetalleVenta()

        End If
        'grdetalle.Col = 0
        grdetalle.Row = 0
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
    Public Sub _prListarClientesPorCobrar()


        Dim ef = New Efecto
        ef.tipo = 5
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione CLIENTES POR COBRAR".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            'cjnumi, cjci, cjnombre, cjtipo
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            _CodClienteCredito = Row.Cells("cjnumi").Value
            tbClienteCredito.Text = Row.Cells("cjnombre").Value

        End If
    End Sub
    Public Sub _prListarBanco()



        Dim ef = New Efecto
        ef.tipo = 7
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione un banco".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            't.canumi ,t.canombre ,t.cacuenta ,t.caobs 
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            _Codbanco = Row.Cells("canumi").Value
            tbbanco.Text = Row.Cells("canombre").Value.ToString + " " + Row.Cells("cacuenta").Value.ToString

        End If
    End Sub
    Private Sub tbClienteCredito_KeyDown(sender As Object, e As KeyEventArgs) Handles tbClienteCredito.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                _prListarClientesPorCobrar()
            End If

        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs)
        If (_fnAccesible()) Then

            If (cbsector.Value = 3) Then
                _prListarServiciosLavadero()
                Return


            End If
        End If
    End Sub

    Private Sub grventasLavadero_KeyDown(sender As Object, e As KeyEventArgs) Handles grventasLavadero.KeyDown
        If (_fnAccesible()) Then

            If (cbsector.Value = 3 Or cbsector.Value = 4) Then
                If (e.KeyData = Keys.Control + Keys.Enter And grventasLavadero.Row < 0 And
         (grventasLavadero.Col = grventasLavadero.RootTable.Columns("orden").Index Or grventasLavadero.Col = grventasLavadero.RootTable.Columns("placa").Index)) Then
                    If (cbsector.Value = 3) Then
                        _prListarServiciosLavadero()
                        Return



                    End If
                    If (cbsector.Value = 4) Then
                        _prListarServiciosRemolque()
                        Return



                    End If
                    If (cbsector.Value = -10) Then
                        _prListarServiciosCabañas()
                        Return



                    End If
                End If
            End If



            If (cbsector.Value = -10) Then


                If (e.KeyData = Keys.Control + Keys.Enter And grventasLavadero.Row < 0 And
         (grventasLavadero.Col = grventasLavadero.RootTable.Columns("fechai").Index Or grventasLavadero.Col = grventasLavadero.RootTable.Columns("fechaf").Index Or grventasLavadero.Col = grventasLavadero.RootTable.Columns("cliente").Index)) Then


                    _prListarServiciosCabañas()
                    Return



                End If
            End If

        End If


    End Sub

    Private Sub MSuperTabControl_TabIndexChanged(sender As Object, e As EventArgs) Handles MSuperTabControl.TabIndexChanged


    End Sub

    Private Sub grVentas_FilterApplied(sender As Object, e As EventArgs) Handles grVentas.FilterApplied

    End Sub

    Private Sub tbTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles tbTipoVenta.ValueChanged
        If (tbTipoVenta.Value = True) Then
            tbanular.Visible = False

            If btnGrabar.Enabled = True Then
                tbEstado.ReadOnly = True
                tbEstado.Value = 1
            End If

        Else
            tbanular.Value = True
            tbanular.Visible = True

            If btnGrabar.Enabled = True Then
                tbEstado.ReadOnly = False
                tbEstado.Value = 1
            End If
        End If
    End Sub
    Public Sub _prCambiarEstado()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim lenght As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To lenght Step 1
            Dim estado = dt.Rows(i).Item("estado")
            If (estado = 1) Then
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado") = -1
            Else
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado") = -2
                End If
            End If
        Next

    End Sub

    Private Sub tbanular_ValueChanged(sender As Object, e As EventArgs) Handles tbanular.ValueChanged

        Try
            If (_fnAccesible()) Then
                If (tbanular.Value = True) Then
                    _prCambiarEstado()
                    _prAddDetalleVenta()
                    grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                    'poner en valida la factura
                    tbEstado.Value = 1

                Else
                    Dim dt As DataTable = L_fnObtenerNumiServicioAnulado()
                    _prCambiarEstado()
                    _prAddDetalleVenta()
                    grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                    Dim pos As Integer = CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdnumi") = _fnSiguienteNumi()
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdserv") = dt.Rows(0).Item("ServicioAnulado")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdprod") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descripcion") = dt.Rows(0).Item("servicio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vddesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 0
                    grdetalle.RootTable.Columns("descripcion").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCells
                    _prCalcularPrecioTotal()

                    'poner en anulado
                    tbEstado.Value = 0
                End If
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub TbNit_TextChanged(sender As Object, e As EventArgs) Handles TbNit.TextChanged

    End Sub

    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        If grdetalle.Row >= 0 Then
            'Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            'dt.Rows(grdetalle.Row).Delete()

            '_prCargarDetalleVenta2(dt)

            '_prCalcularPrecioTotal()
            _prEliminarFila()

        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If btnGrabar.Enabled = False Then
            If CbOP.Checked = False Then
                P_prImprimirFacturaCopia(tbCodigo.Text, swreporte.Value)
            Else
                P_prImprimirRecibo(tbCodigo.Text, True, True)

            End If
        End If
    End Sub



    Private Sub swTipoVenta_ValueChanged_1(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (swTipoVenta.Value = 0) Then
            lbCredito.Visible = True
            tbFechaVenc.Visible = True

            lbClienteCredito.Visible = True
            tbClienteCredito.Visible = True
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
            tbFechaVenc.Value = Now.Date
            lbClienteCredito.Visible = False
            tbClienteCredito.Visible = False
        End If

        If (swTipoVenta.Value = 4) Then
            lbbanco.Visible = True
            tbbanco.Visible = True
            'lbobservacion.Visible = True
            'tbObservacion.Visible = True
        Else
            lbbanco.Visible = False
            tbbanco.Visible = False
            'lbobservacion.Visible = False
            'tbObservacion.Visible = False
        End If
    End Sub

    Private Sub tbbanco_KeyDown(sender As Object, e As KeyEventArgs) Handles tbbanco.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                _prListarBanco()
            End If

        End If
    End Sub


    Private Sub tbbanco_MouseHover(sender As Object, e As EventArgs) Handles tbbanco.MouseHover

        ttmensaje.SetToolTip(tbbanco, "Presionando Ctrl+Enter")
        ttmensaje.ToolTipTitle = "Elija el Banco"
        ttmensaje.ToolTipIcon = ToolTipIcon.Info

    End Sub

    Private Sub tbsecnumi_TextChanged(sender As Object, e As EventArgs) Handles tbsecnumi.TextChanged

    End Sub
End Class