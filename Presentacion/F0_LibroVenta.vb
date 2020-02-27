Imports Logica.AccesoLogica
Imports DevComponents.Editors
Imports DevComponents.DotNetBar.SuperGrid
Imports System.IO
Imports System.Drawing.Printing
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class F0_LibroVenta

#Region "Variables Globales"

    Dim _DuracionSms As Integer = 5
    Dim _DsLV As DataTable
    Public _modulo As SideNavItem
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim codReporte As String = "LibVen"

#End Region

#Region "Eventos"

    Private Sub P_LibroVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        P_Inicio()
    End Sub

#End Region

#Region "Metodos"
    Private Sub _prCargarComboSector(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)

        Dim dt As New DataTable
        dt = L_prlistarCategoriasActivos()

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
    Private Sub P_Inicio()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboAlmacen(CbAlmacen)
        _prCargarComboModulos(cbModulo)
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "L I B R O   D E   V E N T A S"

        btnNuevo.Visible = False
        btnModificar.Visible = False
        btnEliminar.Visible = False
        btnGrabar.Visible = False
        btnSalir.Visible = False
        btnImprimir.Visible = False

        btnPrimero.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False

        LblPaginacion.Visible = False
        BubbleBarUsuario.Visible = False
        P_ComboRazonSocial()
        P_ArmarGrilla()
        If (Cb3RazonSocial.Items.Count > 0) Then
            Cb3RazonSocial.SelectedIndex = 0
        End If
        _prCargarComboEstados()
        Dim Mes, Ano As Integer
        Mes = Month(Today.Date)
        Ano = Year(Today.Date)
        tbFechaI.Value = "01/" + Mes.ToString + "/" + Ano.ToString
        Mes = Mes + 1
        If Mes = 13 Then
            Ano = Ano + 1
            Mes = 1
        End If
        tbFechaF.Value = "01/" + Str(Mes) + "/" + Str(Ano)
        tbFechaF.Value = tbFechaF.Value.AddDays(-1)
    End Sub
    Private Sub _prCargarComboModulos(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarModulos()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cod").Width = 60
            .DropDownList.Columns("cod").Caption = "COD"
            .DropDownList.Columns.Add("desc").Width = 500
            .DropDownList.Columns("desc").Caption = "Modulo"
            .ValueMember = "cod"
            .DisplayMember = "desc"
            .DataSource = dt
            .Refresh()
            .TabIndex = 3
        End With
    End Sub

    Private Sub _prCargarComboEstados()
        Dim dt As New DataTable
        dt.Columns.Add("numi", GetType(Integer))
        dt.Columns.Add("desc", GetType(String))

        dt.Rows.Add({0, "MANUAL"})
        dt.Rows.Add({1, "AUTOMATICA"})
        dt.Rows.Add({2, "AMBOS"})

        With tbTipoFactura
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("numi").Width = 60
            .DropDownList.Columns("numi").Caption = "COD"
            .DropDownList.Columns.Add("desc").Width = 200
            .DropDownList.Columns("desc").Caption = "DESCRIPCIÓN"
            .ValueMember = "numi"
            .DisplayMember = "desc"
            .DataSource = dt
            .Refresh()

            .SelectedIndex = 1
        End With

    End Sub

    Private Sub _prCargarComboAlmacen(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarAlmacenQueTenganDosificacion()
        dt.Rows.Add(0, "TODOS")
        dt.Rows.Add(-1, "TODOS MENOS LA PRINCIPAL")
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
        'If (gb_userTodasSuc = False And CType(CbAlmacen.DataSource, DataTable).Rows.Count > 0) Then


        '    CbAlmacen.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
        '    CbAlmacen.ReadOnly = True
        'Else
        '    CbAlmacen.ReadOnly = False
        'End If
        If dt.Rows.Count > 0 Then
            mCombo.SelectedIndex = 0
        End If
    End Sub
    Public Function _fnObtenerPosSucursal(numi As Integer)
        Dim length As Integer = CType(CbAlmacen.DataSource, DataTable).Rows.Count - 1
        For i As Integer = 0 To length Step 1
            If (CType(CbAlmacen.DataSource, DataTable).Rows(i).Item("cod") = numi) Then
                Return i
            End If
        Next
        Return -1
    End Function

#End Region



    Private Sub P_ArmarGrilla()

        DgdLCV.PrimaryGrid.Columns.Clear()
        'Alto de la Fila de Nombres de Columnas
        DgdLCV.PrimaryGrid.ColumnHeader.RowHeight = 25

        'Mostrar u Ocultar la Fila de Filtrado
        DgdLCV.PrimaryGrid.EnableColumnFiltering = True
        DgdLCV.PrimaryGrid.EnableFiltering = True
        DgdLCV.PrimaryGrid.EnableRowFiltering = True
        DgdLCV.PrimaryGrid.Filter.Visible = True

        'Para Mostrar u Ocultar la Columna de Cabesera de las Filas
        DgdLCV.PrimaryGrid.ShowRowHeaders = True

        'Para Mostrar el Indice de la Grilla
        DgdLCV.PrimaryGrid.RowHeaderIndexOffset = 1
        DgdLCV.PrimaryGrid.ShowRowGridIndex = True

        'Alto de las Filas
        DgdLCV.PrimaryGrid.DefaultRowHeight = 22

        'Alternar Colores de las Filas
        DgdLCV.PrimaryGrid.UseAlternateRowStyle = True

        'Para permitir o denegar el cambio de tamaño de la Filas
        DgdLCV.PrimaryGrid.AllowRowResize = False

        'Para que el Tamaño de las Columnas se pongan automaticamente
        'DgdLCV.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells

        DgdLCV.PrimaryGrid.SelectionGranularity = SelectionGranularity.RowWithCellHighlight

        Dim col As GridColumn

        ''Nro
        'col = New GridColumn("Nro")
        'col.HeaderText = "Nro"
        'col.EditorType = GetType(GridTextBoxXEditControl)
        'col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        'col.ReadOnly = True
        'col.Visible = True
        'col.Width = 60
        'DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo
        col = New GridColumn("fvanumi")
        col.HeaderText = "Codigo"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Fecha
        col = New GridColumn("fvafec")
        col.HeaderText = "Fecha"
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nro de Factura
        col = New GridColumn("fvanfac")
        col.HeaderText = "Factura"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nro Autorizacion
        col = New GridColumn("fvaautoriz")
        col.HeaderText = "Autorizacion"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Estado
        col = New GridColumn("fvaest")
        col.HeaderText = "Estado"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleCenter
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nit
        col = New GridColumn("fvanitcli")
        col.HeaderText = "NIT"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Razon Social
        col = New GridColumn("fvadescli")
        col.HeaderText = "Razon Social"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 250
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'A
        col = New GridColumn("fvastot")
        col.HeaderText = "A"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'B
        col = New GridColumn("fvaimpsi")
        col.HeaderText = "B"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'C
        col = New GridColumn("fvaimpeo")
        col.HeaderText = "C"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'D
        col = New GridColumn("fvaimptc")
        col.HeaderText = "D"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'E
        col = New GridColumn("fvasubtotal")
        col.HeaderText = "E"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'F
        col = New GridColumn("fvadesc")
        col.HeaderText = "F"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'G
        col = New GridColumn("fvatotal")
        col.HeaderText = "G"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'H
        col = New GridColumn("fvadebfis")
        col.HeaderText = "H"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo de Control
        col = New GridColumn("fvaccont")
        col.HeaderText = "Cod Control"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Fecha Limite
        col = New GridColumn("fvaflim")
        col.HeaderText = ""
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = False
        col.Width = 0
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo de Almacen
        col = New GridColumn("fvaalm")
        col.HeaderText = ""
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = False
        col.Width = 0
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nombre Almacen
        col = New GridColumn("scneg")
        col.HeaderText = ""
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = False
        col.Width = 0
        DgdLCV.PrimaryGrid.Columns.Add(col)

    End Sub

    Private Sub P_ComboRazonSocial()
        Cb3RazonSocial.Items.Clear()
        Dim _Dt As DataTable = L_ObtenerSucursalesFactura()
        Dim _item As ComboItem
        For Each _f As DataRow In _Dt.Rows
            _item = New ComboItem()
            _item.Text = _f.Item("scneg").ToString
            _item.Value = "0" '_f.Item("yacia").ToString
            _item.Tag = _f.Item("scnit").ToString
            Cb3RazonSocial.Items.Add(_item)
        Next
    End Sub

    Private Sub Cb3RazonSocial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb3RazonSocial.SelectedIndexChanged
        Dim _item As ComboItem = Cb3RazonSocial.SelectedItem
        Tb1CodRazonSocial.Text = _item.Value
        Tb2NitRazonSocial.Text = _item.Tag
    End Sub

    Private Sub Bt1Generar_Click(sender As Object, e As EventArgs) Handles Bt1Generar.Click
        P_LlenarDatosGrilla()
    End Sub

    Private Sub Bt2Reporte_Click(sender As Object, e As EventArgs) Handles Bt2Reporte.Click
        P_GenerarReporte()
    End Sub

    Private Sub Bt3Excel_Click(sender As Object, e As EventArgs) Handles Bt3Excel.Click
        P_GenerarExcel()
    End Sub

    Private Sub Bt4Txt_Click(sender As Object, e As EventArgs) Handles Bt4Txt.Click
        P_GenerarTxt()
    End Sub

    Private Sub P_LlenarDatosGrilla()
        _DsLV = New DataTable
        Dim _TotAlm As New DataTable
        Dim _TotMod As New DataTable
        If tbTipoFactura.Value = 2 Then 'es ambos
            _DsLV = L_fnObtenerLibroVentaAmbosTipoFactura(CbAlmacen.Value, tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), cbModulo.Value)
        Else 'filtrado por tipo factura
            _DsLV = L_fnObtenerLibroVenta(CbAlmacen.Value, tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"),
                                      tbTipoFactura.Value, cbModulo.Value)
        End If

        _TotAlm = L_SumarSucursalTotal(cbModulo.Value, CbAlmacen.Value, tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"))
        Try
            TotSuc.Text = _TotAlm.Rows(0).Item(0)
        Catch ex As Exception
            TotSuc.Text = 0
        End Try

        If _DsLV.Rows.Count > 0 Then
            _TotMod = L_SumarModuloTotal(cbModulo.Value, CbAlmacen.Value, tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"))
            Try
                TotMod.Text = _TotMod.Rows(0).Item(0)
            Catch ex As Exception
                TotMod.Text = 0
            End Try
        Else
            TotMod.Text = 0
        End If

        For Each fil As DataRow In _DsLV.Rows
            If (fil.Item("fvaest").ToString.Equals("A")) Then
                fil.Item("fvadescli") = "ANULADA"
                fil.Item("fvanitcli") = "0"
                fil.Item("fvastot") = 0
                fil.Item("fvasubtotal") = 0
                fil.Item("fvadesc") = 0
                fil.Item("fvatotal") = 0
                fil.Item("fvadebfis") = 0
            End If
        Next

        DgdLCV.PrimaryGrid.Rows.Clear()

        DgdLCV.PrimaryGrid.DataSource = _DsLV

        DgdLCV.PrimaryGrid.SetActiveRow(CType(DgdLCV.PrimaryGrid.ActiveRow, GridRow))
    End Sub

    Private Sub P_GenerarReporte()
        Dim _Ds1 As DataSet
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Ds1 = L_ObtenerRutaImpresora("1") ' Datos de Impresion
        If (_Ds1.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador = New Visualizador 'Comentar
        End If

        Dim dtTitulos As DataTable = L_prTitulosAll(codReporte)
        Dim objrep As New LibroVenta3
        objrep.SetDataSource(_DsLV)
        objrep.SetParameterValue("Anho", tbFechaI.Value.ToString("dd/MM/yyyy"))
        objrep.SetParameterValue("Mes", tbFechaF.Value.ToString("dd/MM/yyyy"))
        objrep.SetParameterValue("RazonSocial", Cb3RazonSocial.Text)
        objrep.SetParameterValue("Nit", Tb2NitRazonSocial.Text)

        'objrep.SetParameterValue("empresaDesc", gs_empresaDescSistema)
        objrep.SetParameterValue("empresaDesc", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDesc.ToUpper)
        'gs_empresaDireccion
        objrep.SetParameterValue("empresaDirec", gs_empresaDireccion)
        If gi_empresaNumi = 1 Then 'filial
            Dim dtSucursal As DataTable = L_fnDosificacionObtenerDatosSucursal(CbAlmacen.Value)
            Dim ci As String = ""
            Dim encargado As String = ""
            If dtSucursal.Rows.Count > 0 Then
                Dim cadena As String = dtSucursal.Rows(0).Item("caconcep4")
                Dim elems As String() = cadena.Split(";")
                If elems.Count >= 1 Then
                    ci = elems(0)
                End If
                If elems.Count >= 2 Then
                    encargado = elems(1)
                End If
            End If
            objrep.SetParameterValue("ci", ci)
            objrep.SetParameterValue("nombre", encargado)
        Else 'carburantes
            objrep.SetParameterValue("ci", dtTitulos.Rows(0).Item("yedesc").ToString)
            objrep.SetParameterValue("nombre", dtTitulos.Rows(1).Item("yedesc").ToString)
        End If

        If (_Ds1.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        End If

        Dim pd As New PrintDocument()
        pd.PrinterSettings.PrinterName = _Ds1.Tables(0).Rows(0).Item("cbrut").ToString
        If (Not pd.PrinterSettings.IsValid) Then
            ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds1.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                   My.Resources.INFORMATION, _DuracionSms * 1000,
                                   eToastGlowColor.Blue, eToastPosition.BottomLeft)
        Else
            objrep.PrintOptions.PrinterName = _Ds1.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
            objrep.PrintToPrinter(1, False, 1, 1)
        End If

        'Preguntando si existe el Directorio
        If (Not Directory.Exists(gs_CarpetaRaiz + "\LCV")) Then
            Directory.CreateDirectory(gs_CarpetaRaiz + "\LCV")
        End If
        'Exportando a Excel el Reporte
        objrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\LCV\LCV_" + Cb3RazonSocial.Text + ".pdf")
    End Sub

    Private Sub P_GenerarExcel()
        If (P_ExportarExcel()) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRECIOS EXITOSA..!!!",
                                       My.Resources.OK1, _DuracionSms * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomLeft)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRECIOS..!!!",
                                       My.Resources.WARNING, _DuracionSms * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub P_GenerarTxt()
        If (P_ExportarTxt("3", "|")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRECIOS EXITOSA..!!!",
                                       My.Resources.OK1, _DuracionSms * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomLeft)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRECIOS..!!!",
                                       My.Resources.WARNING, _DuracionSms * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Function P_ExportarExcel() As Boolean
        Dim _ubicacion As String

        _ubicacion = gs_CarpetaRaiz
        Try
            Dim _stream As Stream
            Dim _escritor As StreamWriter
            Dim _fila As Integer = DgdLCV.PrimaryGrid.Rows.Count
            Dim _columna As Integer = DgdLCV.PrimaryGrid.Columns.Count
            Dim _archivo As String = _ubicacion & "\LCV_" & Now.Date.Day &
                "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
            Dim _linea As String = ""
            Dim _filadata = 0, columndata As Int32 = 0
            File.Delete(_archivo)
            _stream = File.OpenWrite(_archivo)
            _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

            For Each _col As GridColumn In DgdLCV.PrimaryGrid.Columns
                If (_col.Visible) Then
                    _linea = _linea & _col.HeaderText & ";"
                End If
            Next
            _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
            _escritor.WriteLine(_linea)
            _linea = Nothing

            For Each _fil As GridRow In DgdLCV.PrimaryGrid.Rows
                For Each _col As GridColumn In DgdLCV.PrimaryGrid.Columns
                    If (_col.Visible) Then
                        _linea = _linea & CStr(_fil.Cells(_col.Name).Value) & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing
            Next
            _escritor.Close()
            Try
                If (MessageBox.Show("DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    Process.Start(_archivo)
                End If
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

        Return False
    End Function

    Private Function P_ExportarTxt(TipoLibroVenta As String, Separador As String) As Boolean
        Dim _ubicacion As String
        _ubicacion = gs_CarpetaRaiz
        Try
            Dim _stream As Stream
            Dim _escritor As StreamWriter
            Dim _fila As Integer = DgdLCV.PrimaryGrid.Rows.Count
            Dim _columna As Integer = DgdLCV.PrimaryGrid.Columns.Count
            Dim _archivo As String = _ubicacion & "\LCV_" & Now.Date.Day &
                "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".txt"
            Dim _linea As String = "" 'TipoLibroVenta + "|"
            Dim _filadata = 0, columndata As Int32 = 0
            File.Delete(_archivo)
            _stream = File.OpenWrite(_archivo)
            _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

            Dim nro As Integer = 1
            For Each _fil As GridRow In DgdLCV.PrimaryGrid.Rows
                _linea = TipoLibroVenta + Separador + nro.ToString + Separador
                '_linea = nro.ToString + Separador

                For Each _col As GridColumn In DgdLCV.PrimaryGrid.Columns
                    If (_col.Visible And Not _col.Name.Equals("factura")) Then
                        _linea = _linea & CStr(_fil.Cells(_col.Name).Value).Trim & Separador
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing
                nro += 1
            Next
            _escritor.Close()
            Try
                If (MessageBox.Show("DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    Process.Start(_archivo)
                End If
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return False
    End Function

    Private Sub _prSalir()
   
            _modulo.Select()
            _tab.Close()


    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub LabelX7_Click(sender As Object, e As EventArgs) Handles LabelX7.Click

    End Sub
End Class