Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.SuperGrid
Imports System.IO
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX

Public Class F0_AnularFactura

#Region "Variables Globales"

    Dim _DuracionSms As Integer = 5
    Dim NroFactura As String
    Dim NroAutorizacion As String
    Public _nameButton As String
    Public _tab As SuperTabItem
#End Region

#Region "Eventos"

    Private Sub P_AnularFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        P_Inicio()
    End Sub

#End Region

#Region "Metodos"

    Private Sub P_Inicio()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboAlmacen(CbAlmacen)
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "A N U L A R   F A C T U R A"
        MSuperTabControl.SelectedTabIndex = 0

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

        btnGrabar.Enabled = False
        btnNuevo.Visible = False
        btnEliminar.Visible = False
        btnImprimir.Visible = False
        btnSalir.Dock = DockStyle.Left

        If (CbAlmacen.SelectedIndex >= 0) Then
            P_ArmarGrilla(CbAlmacen.Value)
        End If




    End Sub
    Private Sub _prCargarComboAlmacen(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarAlmacenQueTenganDosificacion()
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
        If (gb_userTodasSuc = False And CType(CbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
            CbAlmacen.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
            CbAlmacen.ReadOnly = True
        Else
            CbAlmacen.ReadOnly = False
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

    Private Sub P_ArmarGrilla(almacen As String)

        DgdFactura.PrimaryGrid.Columns.Clear()
        'Alto de la Fila de Nombres de Columnas
        DgdFactura.PrimaryGrid.ColumnHeader.RowHeight = 25

        'Mostrar u Ocultar la Fila de Filtrado
        DgdFactura.PrimaryGrid.EnableColumnFiltering = True
        DgdFactura.PrimaryGrid.EnableFiltering = True
        DgdFactura.PrimaryGrid.EnableRowFiltering = True
        DgdFactura.PrimaryGrid.Filter.Visible = True

        'Para Mostrar u Ocultar la Columna de Cabesera de las Filas
        DgdFactura.PrimaryGrid.ShowRowHeaders = True

        'Para Mostrar el Indice de la Grilla
        DgdFactura.PrimaryGrid.RowHeaderIndexOffset = 1
        DgdFactura.PrimaryGrid.ShowRowGridIndex = True

        'Alto de las Filas
        DgdFactura.PrimaryGrid.DefaultRowHeight = 22

        'Alternar Colores de las Filas
        DgdFactura.PrimaryGrid.UseAlternateRowStyle = True

        'Para permitir o denegar el cambio de tamaño de la Filas
        DgdFactura.PrimaryGrid.AllowRowResize = False

        'Para que el Tamaño de las Columnas se pongan automaticamente
        'DgdFactura.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells

        DgdFactura.PrimaryGrid.SelectionGranularity = SelectionGranularity.RowWithCellHighlight

        Dim col As GridColumn

        'nfa
        col = New GridColumn("Archivo")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Codigo
        col = New GridColumn("Codigo")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 70
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nro. Factura
        col = New GridColumn("Nro Factura")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 90
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Fecha
        col = New GridColumn("Fecha")
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 90
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Cod Cliente
        col = New GridColumn("Cod Cliente")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nombre 1
        col = New GridColumn("Nombre 1")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nombre 2
        col = New GridColumn("Nombre 2")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nit
        col = New GridColumn("Nit")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Subtotal
        col = New GridColumn("Subtotal")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Descuento
        col = New GridColumn("Descuento")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Total
        col = New GridColumn("Total")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Cod Control
        col = New GridColumn("Cod Control")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Fec Limite
        col = New GridColumn("Fec Limite")
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Estado
        col = New GridColumn("Estado")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 60
        DgdFactura.PrimaryGrid.Columns.Add(col)

        P_LlenarDatosGrilla(almacen)

    End Sub

    Private Sub DgdFactura_RowActivated(sender As Object, e As GridRowActivatedEventArgs) Handles DgdFactura.RowActivated
        If (DgdFactura.PrimaryGrid.ActiveRow Is Nothing) Then
            ToastNotification.Show(Me, "Selecione un Fila de la gt_Tabla",
                                   My.Resources.INFORMATION, _DuracionSms * 1000,
                                   eToastGlowColor.Blue, eToastPosition.BottomLeft)
            Exit Sub
        End If
        If (DgdFactura.PrimaryGrid.ActiveRow.Index > -1) Then
            P_LlenarDatos(CType(e.NewActiveRow, GridRow))
        End If

    End Sub

    Private Sub P_LlenarDatos(fil As GridRow)
        If (fil.Cells.Count = 14) Then
            NroFactura = fil.Cells(0).Value.ToString.Split("_")(0)
            NroAutorizacion = fil.Cells(0).Value.ToString.Split("_")(1)
            Tb1Codigo.Text = fil.Cells(1).Value
            Tb2NroFactura.Text = fil.Cells(2).Value
            Tb2Fecha.Text = fil.Cells(3).Value
            Tb3CodCliente.Text = fil.Cells(4).Value
            Tb4Desc1.Text = fil.Cells(5).Value
            If (IsDBNull(fil.Cells(6).Value)) Then
                Tb5Desc2.Text = "0"
            Else
                Tb5Desc2.Text = fil.Cells(6).Value
            End If

            Tb6Nit.Text = fil.Cells(7).Value
            Tb7SubTotal.Text = fil.Cells(8).Value
            Tb8Descuento.Text = fil.Cells(9).Value
            Tb9Total.Text = fil.Cells(10).Value
            Tb10CodControl.Text = fil.Cells(11).Value
            Tb11FechaLim.Text = fil.Cells(12).Value
            Sb1Estado.Value = IIf(fil.Cells(13).Value = 0, False, True)

            If (Sb1Estado.Value) Then
                GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Green
            Else
                GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Red
            End If

            P_MostrarFactura(fil.Cells(0).Value.ToString, fil.Cells(1).Value.ToString)
        End If
    End Sub

    Private Sub P_MostrarFactura(Cod As String, Cod2 As String)
        If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
            ToastNotification.Show(Me, "La ruta de la carpeta " + gs_CarpetaRaiz + "\Facturas, No EXISTE!!!",
                                   My.Resources.INFORMATION, _DuracionSms * 1000,
                                   eToastGlowColor.Blue, eToastPosition.BottomLeft)
        End If
        Dim bool As Boolean
        If (File.Exists(gs_CarpetaRaiz + "\Facturas\" + Cod + ".pdf")) Then
            ArFactura.LoadFile(gs_CarpetaRaiz + "\Facturas\" + Cod + ".pdf")
            ArFactura.setZoom(95)
            bool = True
        ElseIf (File.Exists(gs_CarpetaRaiz + "\Facturas\" + Cod2 + ".pdf")) Then
            ArFactura.LoadFile(gs_CarpetaRaiz + "\Facturas\" + Cod2 + ".pdf")
            ArFactura.setZoom(95)
            bool = False
        Else
            ToastNotification.Show(Me, "El archivo " + IIf(bool, Cod, Cod2) + ".pdf, No EXISTE!!!",
                                   My.Resources.INFORMATION, _DuracionSms * 1000,
                                   eToastGlowColor.Blue, eToastPosition.BottomLeft)
        End If

    End Sub

    Private Sub Bt1Guardar_Click(sender As Object, e As EventArgs) Handles Bt1Guardar.Click
        If (Sb1Estado.Value) Then
            If (MessageBox.Show("Esta seguro de poner VIGENTE la Factura " + Tb2NroFactura.Text + "?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                L_Modificar_Factura("fvanumi = " + Tb1Codigo.Text + " and fvanfac = " + NroFactura + " and fvaautoriz = " + NroAutorizacion, "", "", "", IIf(Sb1Estado.Value, "1", "0"))
                'P_ActStock()
                P_LlenarDatosGrilla(CbAlmacen.Value)
                ToastNotification.Show(Me, "La Factura con Codigo " + Tb2NroFactura.Text + ", Se puso VIGENTE correctamente",
                                       My.Resources.OK1, _DuracionSms * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomLeft)
            End If
        Else
            If (MessageBox.Show("Esta seguro de ANULAR la Factura " + Tb2NroFactura.Text + "?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                L_Modificar_Factura("fvanumi = " + Tb1Codigo.Text + " and fvanfac = " + NroFactura + " and fvaautoriz = " + NroAutorizacion, "", "", "", IIf(Sb1Estado.Value, "1", "0"))
                'P_ActStock()
                _AnularDies()
                P_LlenarDatosGrilla(CbAlmacen.Value)
                ToastNotification.Show(Me, "La Factura " + Tb2NroFactura.Text + ", Se ANULADO correctamente",
                                       My.Resources.OK1, _DuracionSms * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomLeft)

            End If
        End If
    End Sub
    Private Sub _AnularDies()
        Dim mensajeError As String = ""
        Dim res As Boolean = L_AnularVenta(Tb1Codigo.Text, Tb2Fecha.Text)
    End Sub
    Private Sub Sb1Estado_ValueChanged(sender As Object, e As EventArgs) Handles Sb1Estado.ValueChanged
        If (Sb1Estado.Value) Then
            GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Green
        Else
            GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Red
        End If
    End Sub

    Private Sub P_LlenarDatosGrilla(almacen As String)
        DgdFactura.PrimaryGrid.Rows.Clear()

        DgdFactura.PrimaryGrid.DataSource = L_Obtener_Facturas(almacen, IIf(swtipo.Value = True, 1, 0))

        DgdFactura.PrimaryGrid.SetActiveRow(CType(DgdFactura.PrimaryGrid.ActiveRow, GridRow))



    End Sub

    'Private Sub P_ActStock()
    '    Dim _Ds As DataSet
    '    _Ds = L_ObtenerDetalleFactura(Tb1Codigo.Text)
    '    G_ActStock(_Ds.Tables(0), Not Sb1Estado.Value, 1)
    'End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click

    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click

    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click

    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click

    End Sub

    Private Sub CbAlmacen_ValueChanged(sender As Object, e As EventArgs) Handles CbAlmacen.ValueChanged
        If (CbAlmacen.SelectedIndex >= 0) Then
            P_ArmarGrilla(CbAlmacen.Value)

        End If
    End Sub

    Private Sub swtipo_ValueChanged(sender As Object, e As EventArgs) Handles swtipo.ValueChanged
        If (CbAlmacen.SelectedIndex >= 0) Then
            P_ArmarGrilla(CbAlmacen.Value)
        End If

    End Sub
End Class