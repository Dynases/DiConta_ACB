
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

Public Class F00_Movimientos

#Region "Variables Globales"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Lote As Boolean = False
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
#End Region
#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        Me.WindowState = FormWindowState.Maximized
        _prCargarComboLibreriaConcepto(cbConcepto)
        _prCargarComboLibreria(cbSector, 8, 1)
        _prCargarMovimiento()
        _prInhabiliitar()
        'Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'Me.Icon = ico
        ''_prAsignarPermisos()
        Me.Text = "MOVIMIENTO PRODUCTOS"
        tbObservacion.MaxLength = 100
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prProductoLibreriaGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cnnum").Width = 70
            .DropDownList.Columns("cnnum").Caption = "COD"
            .DropDownList.Columns.Add("cndesc1").Width = 200
            .DropDownList.Columns("cndesc1").Caption = "DESCRIPCION"
            .ValueMember = "cnnum"
            .DisplayMember = "cndesc1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarComboLibreriaConcepto(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prListarConceptos()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cpnumi").Width = 60
            .DropDownList.Columns("cpnumi").Caption = "COD"
            .DropDownList.Columns.Add("cpdesc").Width = 250
            .DropDownList.Columns("cpdesc").Caption = "CONCEPTO"
            .ValueMember = "cpnumi"
            .DisplayMember = "cpdesc"
            .DataSource = dt
            .Refresh()
        End With
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

        cbConcepto.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFecha.IsInputReadOnly = True
        cbSector.ReadOnly = True
        ''''''''''
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        grmovimientos.Enabled = True
        If (Not IsNothing(grdetalle.DataSource)) Then
            grdetalle.RootTable.Columns("img").Visible = False
        End If



        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If

        PanelInferior.Enabled = True
        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()
        'cbConcepto.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbFecha.IsInputReadOnly = False
        cbSector.ReadOnly = False
        grmovimientos.Enabled = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        If (tbCodigo.Text.Length > 0) Then
            cbSector.ReadOnly = True
            cbConcepto.ReadOnly = True
        Else
            cbSector.ReadOnly = False
            cbConcepto.ReadOnly = False

        End If
        btnGrabar.Enabled = True
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarMovimiento()
        If grmovimientos.RowCount > 0 Then
            _Mpos = 0
            grmovimientos.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbObservacion.Clear()
        tbFecha.Value = Now.Date
        _prCargarDetalleMovimiento(-1)


        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With

        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelInferior.Visible = True
        End If
        If (CType(cbSector.DataSource, DataTable).Rows.Count > 0) Then
            cbSector.SelectedIndex = 0

        End If
        If (CType(cbConcepto.DataSource, DataTable).Rows.Count > 0) Then
            cbConcepto.SelectedIndex = 0

        End If
        _prAddDetalleVenta()
        cbConcepto.Focus()
        FilaSelectLote = Nothing
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '      a.ibid ,a.ibfdoc ,a.ibconcep ,b.cpdesc as concepto,a.ibobs ,a.ibest ,a.ibalm ,a.ibiddc 
        ',a.ibfact ,a.ibhact ,a.ibuact,ibdepdest

        '      a.ibid ,a.ibfdoc ,a.ibconcep ,b.cpdesc as concepto,a.ibobs ,a.ibest ,a.ibalm,a.ibdepdest  ,a.ibiddc 
        ',a.ibfact ,a.ibhact ,a.ibuact
        With grmovimientos
            tbCodigo.Text = .GetValue("ibid")
            tbFecha.Value = .GetValue("ibfdoc")
            cbConcepto.Value = .GetValue("ibconcep")
            tbObservacion.Text = .GetValue("ibobs")
            lbFecha.Text = CType(.GetValue("ibfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("ibhact").ToString
            lbUsuario.Text = .GetValue("ibuact").ToString
            cbSector.Value = .GetValue("icsector")

        End With

        _prCargarDetalleMovimiento(tbCodigo.Text)
        LblPaginacion.Text = Str(grmovimientos.Row + 1) + "/" + grmovimientos.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleMovimiento(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleMovimiento(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        ' a.icid ,a.icibid ,a.iccprod ,b.cicdprod1  as producto,a.iccant ,
        'a.icsector  ,Cast(null as image ) as img,1 as estado,
        '(Sum(inv.iccven)  +a.iccant ) as stock

        With grdetalle.RootTable.Columns("icid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With grdetalle.RootTable.Columns("icibid")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("iccprod")
            .Width = 120
            .Caption = "Codigo P."
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 250
            .Visible = True


        End With
        With grdetalle.RootTable.Columns("iccant")
            .Width = 160
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0"
            .Caption = "Cantidad".ToUpper
        End With

        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("icsector")
            .Width = 120
            .Caption = "sector".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("stock")
            .Width = 120
            .Caption = "stock".ToUpper
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .BoundMode = Janus.Data.BoundMode.Bound
            .RowHeaders = InheritableBoolean.True
        End With
    End Sub

    Private Sub _prCargarMovimiento()
        Dim dt As New DataTable
        dt = L_prMovimientoGeneral()
        grmovimientos.DataSource = dt
        grmovimientos.RetrieveStructure()
        grmovimientos.AlternatingColors = True


        With grmovimientos.RootTable.Columns("ibid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grmovimientos.RootTable.Columns("ibfdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
            .FormatString = "yyyy/MM/dd"
        End With
        With grmovimientos.RootTable.Columns("ibconcep")
            .Width = 90
            .Visible = False
        End With

        With grmovimientos.RootTable.Columns("concepto")
            .Width = 160
            .Visible = True
            .Caption = "CONCEPTO"
        End With
        With grmovimientos.RootTable.Columns("ibobs")
            .Width = 400
            .Visible = True
            .Caption = "observacion".ToUpper
        End With


        With grmovimientos.RootTable.Columns("ibest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grmovimientos.RootTable.Columns("ibalm")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimientos.RootTable.Columns("ibiddc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        '      a.ibid ,a.ibfdoc ,a.ibconcep ,b.cpdesc as concepto,a.ibobs ,a.ibest ,a.ibalm ,a.ibiddc 
        ',a.ibfact ,a.ibhact ,a.ibuact

        With grmovimientos.RootTable.Columns("ibfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimientos.RootTable.Columns("ibhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimientos.RootTable.Columns("ibuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimientos.RootTable.Columns("icsector")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grmovimientos.RootTable.Columns("ibdepdest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimientos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleMovimiento(-1)
        End If
    End Sub
    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        'a.yfnumi  ,a.yfcdprod1  ,a.yfcdprod2,Sum(b.iccven ) as stock
        'Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("cinumi")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codProducto = grdetalle.GetValue("iccprod")) Then
                        sum = sum + grdetalle.GetValue("iccant")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub
    Private Sub _prCargarProductos()
        Dim dt As New DataTable
        dt = L_prMovimientoListarProductos(CType(grdetalle.DataSource, DataTable), 1, cbSector.Value)  ''1=Almacen

        'a.yfnumi  ,a.yfcdprod1  ,a.yfcdprod2 

        'a.cinumi  ,a.cicdprod1  ,a.cicdprod2,Sum(b.iccven ) as stock 
        actualizarSaldoSinLote(dt)
        grproducto.DataSource = dt
        grproducto.RetrieveStructure()
        grproducto.AlternatingColors = True
        With grproducto.RootTable.Columns("cinumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With
        With grproducto.RootTable.Columns("cicdprod1")
            .Width = 350
            .Caption = "PRODUCTOS"
            .Visible = True

        End With

        With grproducto.RootTable.Columns("cicdprod2")
            .Width = 250
            .Visible = True
            .Caption = "DESCRIPCION CORTA"
        End With

        With grproducto.RootTable.Columns("stock")
            .Width = 120
            .FormatString = "0.00"
            .Caption = "STOCK"
            .Visible = True

        End With
        With grproducto
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
        _prAplicarCondiccionJanusSinLote()
    End Sub

    Public Sub _prAplicarCondiccionJanusSinLote()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grproducto.RootTable.Columns("stock"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.Tan
        grproducto.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub _prAddDetalleVenta()
        'a.icid ,a.icibid ,a.iccprod ,b.cadesc as producto,a.iccant ,Cast(null as image ) as img,1 as estado

        '      a.icid ,a.icibid ,a.iccprod ,b.yfcdprod1  as producto,a.iccant ,
        'a.iclot ,a.icfvenc ,Cast(null as image ) as img,1 as estado,
        '(Sum(inv.iccven )+a.iccant  ) as stock
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, 1, Bin.GetBuffer, 0, 0)
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim mayor As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = IIf(IsDBNull(CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid")), 0, CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid"))
            If (data > mayor) Then
                mayor = data

            End If
        Next
        Return mayor
    End Function
    Public Function _fnAccesible()
        Return tbFecha.IsInputReadOnly = False
    End Function
    Private Sub _HabilitarProductos()
        GPanelProductos.Visible = True
        PanelInferior.Visible = False
        _prCargarProductos()
        grproducto.Focus()
        grproducto.MoveTo(grproducto.FilterRow)
        grproducto.Col = 1
    End Sub
    Private Sub _DesHabilitarProductos()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelInferior.Visible = True
            grdetalle.Select()
            grdetalle.Col = 4
            grdetalle.Row = grdetalle.RowCount - 1
        End If
        FilaSelectLote = Nothing
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccprod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("icid")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                grdetalle.Select()
                grdetalle.Col = 4
                grdetalle.Row = grdetalle.RowCount - 1
            End If
        End If


    End Sub
    Public Function _ValidarCampos() As Boolean
        If (cbConcepto.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Concepto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbConcepto.Focus()
            Return False

        End If
        If (cbSector.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un SECTOR".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSector.Focus()
            Return False
        End If

        If (grdetalle.RowCount <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Inserte un Detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            grdetalle.Focus()

            Return False

        End If
        If (grdetalle.RowCount = 1) Then
            If (CType(grdetalle.DataSource, DataTable).Rows(0).Item("iccprod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Inserte un Detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                grdetalle.Focus()

                Return False
            End If
        End If
        Return True
    End Function
    Public Sub _prGardarDetalleAbm(numi As String)
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)

        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (estado = 0) Then
                'a.icid ,a.icibid ,a.iccprod ,b.yfcdprod1  as producto,a.iccant ,
                'a.iclot ,a.icfvenc ,Cast(null as image ) as img,1 as estado,
                '(Sum(inv.iccven )+a.iccant  ) as stock
                'a.icid ,a.icibid ,a.iccprod ,b.cadesc as producto,a.iccant ,Cast(null as image ) as img,1 as estado
                Dim detalleCopia As DataTable = CType(grdetalle.DataSource, DataTable).Copy
                detalleCopia.Rows.Clear()

                detalleCopia.Rows.Add(0, numi, CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccprod"), "", CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccant"),
                                      cbSector.Value, Bin.GetBuffer, estado, 0)
                L_prMovimientoChoferABMDetalle(numi, 10, detalleCopia)
            End If
            If (estado = 2) Then
                'a.icid ,a.icibid ,a.iccprod ,b.cadesc as producto,a.iccant ,Cast(null as image ) as img,1 as estado
                Dim detalleCopia As DataTable = CType(grdetalle.DataSource, DataTable).Copy
                detalleCopia.Rows.Clear()
                detalleCopia.Rows.Add(CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid"), numi,
                                      CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccprod"), "",
                                      CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccant"), cbSector.Value, Bin.GetBuffer, estado, 0)
                L_prMovimientoChoferABMDetalle(numi, 11, detalleCopia)
            End If
            If (estado = -1) Then
                'a.icid ,a.icibid ,a.iccprod ,b.cadesc as producto,a.iccant ,Cast(null as image ) as img,1 as estado
                Dim detalleCopia As DataTable = CType(grdetalle.DataSource, DataTable).Copy
                detalleCopia.Rows.Clear()
                detalleCopia.Rows.Add(CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid"), numi,
                                      CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccprod"), "", CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccant"),
                                     cbSector.Value, Bin.GetBuffer, estado, 0)
                L_prMovimientoChoferABMDetalle(numi, 12, detalleCopia)
            End If
        Next
    End Sub

    Public Sub _GuardarNuevo()
        'ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String, _almacen As Integer        
        Dim numi As String = ""
        Dim res As Boolean = L_prMovimientoChoferGrabar(numi, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, 1, 0, 0)
        If res Then
            If (numi <> String.Empty) Then
                _prGardarDetalleAbm(numi)
            End If
            _prCargarMovimiento()

            _Limpiar()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Movimiento no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If


    End Sub
    Private Sub _prGuardarModificado()
        Dim res As Boolean = L_prMovimientoModificar(tbCodigo.Text, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, 1)
        If res Then

            _prGardarDetalleAbm(tbCodigo.Text)
            _prCargarMovimiento()

            _prSalir()

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grmovimientos.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If
        Else

            _modulo.Select()
            _tab.Close()

        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        If (cbConcepto.Value <> 3) Then
            For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
                Dim Bin As New MemoryStream
                Dim img As New Bitmap(My.Resources.delete, 28, 28)
                img.Save(Bin, Imaging.ImageFormat.Png)
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
                grdetalle.RootTable.Columns("img").Visible = True
            Next
        End If
    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grmovimientos.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grmovimientos.Row = _MPos
        End If
    End Sub

    Public Sub InsertarProductosSinLote()
        '      a.icid ,a.icibid ,a.iccprod ,b.yfcdprod1  as producto,a.iccant ,
        'a.icsector ,Cast(null as image ) as img,1 as estado,
        '(Sum(inv.iccven )+a.iccant  ) as stock
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnObtenerFilaDetalle(pos, grdetalle.GetValue("icid"))

        Dim existe As Boolean = _fnExisteProducto(grproducto.GetValue("cinumi"))
        If ((pos >= 0) And (Not existe)) Then
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccprod") = grproducto.GetValue("cinumi")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grproducto.GetValue("cicdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grproducto.GetValue("stock")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1

            ''    _DesHabilitarProductos()

            _prAddDetalleVenta()
            _prCargarProductos()
            grproducto.RemoveFilters()
            grproducto.Focus()
            grproducto.MoveTo(grproducto.FilterRow)
            grproducto.Col = 1
        Else
            If (existe) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                grproducto.RemoveFilters()
                grproducto.Focus()
                grproducto.MoveTo(grproducto.FilterRow)
                grproducto.Col = 1
            End If

        End If
    End Sub

#End Region

#Region "Eventos Formulario"



    Private Sub F0_Movimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelInferior.Enabled = False
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then

            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
                e.Cancel = False
            Else
                If ((e.Column.Index = grdetalle.RootTable.Columns("icsector").Index)) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If

        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grdetalle.Col
            f = grdetalle.Row

            If (grdetalle.Col = grdetalle.RootTable.Columns("iccant").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then
                    _prAddDetalleVenta()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then
                    _prAddDetalleVenta()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
salirIf:
        End If

        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            _HabilitarProductos()

        End If
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then

            _prEliminarFila()


        End If



    End Sub


    Private Sub grproducto_KeyDown(sender As Object, e As KeyEventArgs) Handles grproducto.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grproducto.Col
            f = grproducto.Row
            If (f >= 0) Then
                InsertarProductosSinLote()
            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
        End If
    End Sub

    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged

        If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("iccant")) Or grdetalle.GetValue("iccant").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("icid")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If

            Else
                If (grdetalle.GetValue("iccant") > 0) Then
                    Dim lin As Integer = grdetalle.GetValue("icid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grdetalle.GetValue("icid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("iccant")) Or grdetalle.GetValue("iccant").ToString = String.Empty) Then

                grdetalle.SetValue("iccant", 1)
            Else
                If (grdetalle.GetValue("iccant") > 0) Then
                    Dim stock As Double = grdetalle.GetValue("stock")
                    If (grdetalle.GetValue("iccant") > stock And cbConcepto.Value <> 1) Then
                        Dim lin As Integer = grdetalle.GetValue("icid")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = stock
                        grdetalle.SetValue("iccant", stock)
                        Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                        ToastNotification.Show(Me, "La cantidad que se quiere sacar es mayor a la que existe en el stock solo puede Sacar : ".ToUpper + Str(stock).Trim,
                          img,
                          5000,
                          eToastGlowColor.Blue,
                          eToastPosition.BottomLeft)
                    End If
                Else

                    grdetalle.SetValue("iccant", 1)

                End If
            End If
        End If
    End Sub

    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs) Handles grdetalle.MouseClick
        If (Not _fnAccesible()) Then
            Return
        End If
        If (grdetalle.RowCount >= 2) Then
            If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("img").Index) Then
                _prEliminarFila()
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
        If (grmovimientos.RowCount > 0) Then
            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelInferior.Enabled = False
            _prCargarIconELiminar()
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prMovimientoEliminar(tbCodigo.Text)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter
        If (grdetalle.RowCount > 0) Then
            grdetalle.Select()
            grdetalle.Col = 3
            grdetalle.Row = 0
        End If
    End Sub

    Private Sub grmovimientos_KeyDown(sender As Object, e As KeyEventArgs) Handles grmovimientos.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub

    Private Sub cbAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles cbSector.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Enter Then
                grdetalle.Focus()
                'grdetalle.Select()
                grdetalle.Col = 2
                grdetalle.Row = 0
            End If
        End If
    End Sub

    Private Sub cbConcepto_ValueChanged(sender As Object, e As EventArgs) Handles cbConcepto.ValueChanged
        If (_fnAccesible() And tbCodigo.Text = String.Empty) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            _prAddDetalleVenta()
            _DesHabilitarProductos()
        End If

    End Sub

    Private Sub cbAlmacenOrigen_ValueChanged(sender As Object, e As EventArgs) Handles cbSector.ValueChanged
        If (_fnAccesible() And tbCodigo.Text = String.Empty) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            _prAddDetalleVenta()
            _DesHabilitarProductos()

        End If


    End Sub
#End Region

End Class