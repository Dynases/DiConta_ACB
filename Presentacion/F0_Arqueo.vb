Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Modelos
Imports DevComponents.DotNetBar.Controls

'pagos anticipados en negativo y credito en positivo para el detalle de arqueo

Public Class F0_Arqueo
#Region "ATRIBUTOS"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _existTipoCambio As Boolean

    Private _dtMaquina As DataTable

    Public _modulo As SideNavItem

    Private _dtsDesgloseCredito As DataSet = New DataSet
    Private _dtsDesgloseAnticipo As DataSet = New DataSet
    Private _dtModelo As DataTable
#End Region

#Region "VARIABLES LOCALES"
    Public _MPos As Integer
    Public _MNuevo As Boolean
    Public _MModificar As Boolean

    Public _MListEstBuscador As List(Of Celda)

    Public _MTipoInserccionNuevo As Boolean = True

#End Region


#Region "METODOS PRIVADOS MODELO"

    Public Sub _PMIniciarTodo()
        _dtModelo = New DataTable
        _dtModelo.Columns.Add("ahnumi", GetType(Integer))
        _dtModelo.Columns.Add("ahnumita11", GetType(Integer))
        _dtModelo.Columns.Add("ahmonto", GetType(Decimal))
        _dtModelo.Columns.Add("estado", GetType(Integer))

        TxtNombreUsu.Text = MGlobal.gs_usuario
        TxtNombreUsu.ReadOnly = True

        Me.WindowState = FormWindowState.Maximized
        Me.SupTabItemBusqueda.Visible = True

        _MListEstBuscador = _PMOGetListEstructuraBuscador()

        _PMInhabilitar()

        _PMOHabilitarFocus()

        _PMFiltrar()
        '_PMInhabilitar()

        '_PMOHabilitarFocus()
        _dtsDesgloseCredito = New DataSet
        _dtsDesgloseAnticipo = New DataSet


        AddHandler JGrM_Buscador.SelectionChanged, AddressOf JGrM_Buscador_SelectionChanged

    End Sub

    Private Sub _PMCargarBuscador()

        Dim dtBuscador As DataTable = _PMOGetTablaBuscador()

        JGrM_Buscador.DataSource = dtBuscador
        JGrM_Buscador.RetrieveStructure()

        For i = 0 To _MListEstBuscador.Count - 1
            Dim campo As String = _MListEstBuscador.Item(i).campo
            With JGrM_Buscador.RootTable.Columns(campo)
                If _MListEstBuscador.Item(i).visible = True Then
                    .Caption = _MListEstBuscador.Item(i).titulo
                    .Width = _MListEstBuscador.Item(i).tamano
                    .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                    Dim col As DataColumn = dtBuscador.Columns(campo)
                    Dim tipo As Type = col.DataType
                    If tipo.ToString = "System.Int32" Or tipo.ToString = "System.Decimal" Or tipo.ToString = "System.Double" Then
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End If
                    If _MListEstBuscador.Item(i).formato <> String.Empty Then
                        .FormatString = _MListEstBuscador.Item(i).formato
                    End If
                Else
                    .Visible = False
                End If
            End With
        Next

        'Habilitar Filtradores
        With JGrM_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        'metodo para hacer la actualizacion de algo cuando cambia el datasource del buscador
        '_PMOLuegoDeCargarBuscador()

    End Sub
    Private Sub _PMCargarBuscador1()

        Dim dtBuscador As DataTable = _PMOGetTablaBuscador()

        JGrM_Buscador.DataSource = dtBuscador
        JGrM_Buscador.RetrieveStructure()

        For i = 0 To dtBuscador.Columns.Count - 1
            With JGrM_Buscador.RootTable.Columns(i)
                If _MListEstBuscador.Item(i).visible = True Then
                    .Caption = _MListEstBuscador.Item(i).titulo
                    .Width = _MListEstBuscador.Item(i).tamano
                    .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                    Dim col As DataColumn = dtBuscador.Columns(i)
                    Dim tipo As Type = col.DataType
                    If tipo.ToString = "System.Int32" Or tipo.ToString = "System.Decimal" Then
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End If
                    If _MListEstBuscador.Item(i).formato = String.Empty Then
                        .FormatString = _MListEstBuscador.Item(i).formato
                    End If
                Else
                    .Visible = False
                End If
            End With
        Next

        'Habilitar Filtradores
        With JGrM_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

    End Sub


    Public Sub _PMInhabilitar()
        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False
        PanelNavegacion.Enabled = True
        JGrM_Buscador.Enabled = True
        MRlAccion.Text = ""

        '_PMOLimpiarErrores()

        _PMOInhabilitar()

        _PMOLimpiarErrores()
    End Sub

    Private Sub _PMHabilitar()
        JGrM_Buscador.Enabled = False
        _PMOHabilitar()
    End Sub
    Public Sub _PMFiltrar()
        'cargo el buscador
        _PMCargarBuscador()
        If JGrM_Buscador.RowCount > 0 Then
            _MPos = 0
            _PMOMostrarRegistro(_MPos)
        Else
            _PMOLimpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub

    Public Sub _PMPrimerRegistro()
        If JGrM_Buscador.RowCount > 0 Then
            _MPos = 0
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub
    Private Sub _PMAnteriorRegistro()
        If _MPos > 0 And JGrM_Buscador.RowCount > 0 Then
            _MPos = _MPos - 1
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub
    Private Sub _PMSiguienteRegistro()
        If _MPos < JGrM_Buscador.RowCount - 1 Then
            _MPos = _MPos + 1
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub
    Private Sub _PMUltimoRegistro()
        If JGrM_Buscador.RowCount > 0 Then
            _MPos = JGrM_Buscador.RowCount - 1
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub

    Private Sub _PMNuevo()
        _MNuevo = True
        _MModificar = False

        'aca estaban

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False


        _PMHabilitar()
        _PMOLimpiar()
        '_PMHabilitar()

        MRlAccion.Text = "NUEVO"

        '_PMOLimpiar()

    End Sub

    Private Sub _PMModificar()
        If JGrM_Buscador.Row >= 0 Then
            _MNuevo = False
            _MModificar = True

            _PMHabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelNavegacion.Enabled = False

            MRlAccion.Text = "MODIFICAR"
        End If
    End Sub

    Private Sub _PMEliminar()
        'Dim _Result As MsgBoxResult
        '_Result = MsgBox("¿Esta seguro de Eliminar el Registro?".ToUpper, MsgBoxStyle.YesNo, "Advertencia".ToUpper)
        'If _Result = MsgBoxResult.Yes Then
        '    _PMOEliminarRegistro()
        '    _PMFiltrar()

        'End If
        _PMOEliminarRegistro()
    End Sub

    Private Sub _PMGuardar()

        If _PMOValidarCampos() = False Then
            Exit Sub
        End If

        If _MNuevo Then
            If _PMOGrabarRegistro() = True Then
                'actualizar el grid de buscador
                _PMCargarBuscador()

                If _MTipoInserccionNuevo Then
                    _PMOLimpiar()
                Else
                    _PMSalir()
                End If
            Else
                Exit Sub
            End If

        Else

            _PMOModificarRegistro()

            'actualizar el grid de buscador
            _PMCargarBuscador()

            _PMSalir()
        End If
    End Sub

    Private Sub _PMSalir()
        _PSalirRegistro()
    End Sub
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        Me.Text = "a r q u e o".ToUpper
        _prCargarComboLibreria(tbTurno, gi_LibArqueo, gi_LibARQUEOTurno)
        _prCargarComboVendedor()
        _prCargarComboMaquina()
        _prCargarComboCajero()
        '_prCargarGridAyudaCalculadora()
        _PMIniciarTodo()

        _prAsignarPermisos()
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaDetalleGeneral(cod1, cod2)

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

    Private Sub _prCargarComboVendedor()
        Dim dt As New DataTable
        dt = L_prArqueoVendedorAyuda(1)

        With tbVendedor
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("panumi").Width = 70
            .DropDownList.Columns("panumi").Caption = "COD"

            .DropDownList.Columns.Add("panom1").Width = 200
            .DropDownList.Columns("panom1").Caption = "DESCRIPCION"

            .ValueMember = "panumi"
            .DisplayMember = "panom1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub



    Private Sub _prCargarComboCajero()
        Dim dt As New DataTable
        dt = L_prArqueoVendedorAyuda(0)

        With tbCajero
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("panumi").Width = 70
            .DropDownList.Columns("panumi").Caption = "COD"

            .DropDownList.Columns.Add("panom1").Width = 200
            .DropDownList.Columns("panom1").Caption = "DESCRIPCION"

            .ValueMember = "panumi"
            .DisplayMember = "panom1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prCargarComboMaquina()
        Dim dt As New DataTable
        dt = L_prArqueoMaquinaAyudaTodos()
        _dtMaquina = dt
        With tbMaquina
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("aenumi").Width = 70
            .DropDownList.Columns("aenumi").Caption = "COD"

            .DropDownList.Columns.Add("aedesc").Width = 200
            .DropDownList.Columns("aedesc").Caption = "DESCRIPCION"

            .ValueMember = "aenumi"
            .DisplayMember = "aedesc"
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
    Private Sub _prCargarGridMangueras(numi As String)
        Dim dt As New DataTable
        dt = L_prArqueoDetalleMaquinasGeneral(numi)

        grMangueras.DataSource = dt
        grMangueras.RetrieveStructure()

        'dar formato a las columnas
        With grMangueras.RootTable.Columns("ajnumi")
            .Width = 50
            .Visible = False
        End With
        With grMangueras.RootTable.Columns("ajnumita1")
            .Width = 50
            .Visible = False
        End With

        With grMangueras.RootTable.Columns("ajman")
            .Caption = "COD"
            .HeaderAlignment = TextAlignment.Center
            .Width = 40
            .CellStyle.TextAlignment = TextAlignment.Far
            .AllowSort = False
            .EditType = EditType.NoEdit
            .Visible = False
        End With

        With grMangueras.RootTable.Columns("cndesc1")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "MANGUERA"
            .Width = 70
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grMangueras.RootTable.Columns("agdesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "COMBUSTIBLE"
            .Width = 80
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grMangueras.RootTable.Columns("agprecio")
            .Caption = "PRECIO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .EditType = EditType.NoEdit
            .AllowSort = False
        End With

        With grMangueras.RootTable.Columns("ajmitini")
            .Caption = "MIT INI"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .EditType = EditType.NoEdit
            .AllowSort = False
        End With

        With grMangueras.RootTable.Columns("ajmitfin")
            .Caption = "MIT FIN"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .AllowSort = False
        End With
        With grMangueras.RootTable.Columns("ajmitcali")
            .Caption = "CALIBRACION"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .AllowSort = False
        End With

        With grMangueras.RootTable.Columns("mitTotal")
            .Caption = "MIT TOTAL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .EditType = EditType.NoEdit
            .AllowSort = False
        End With

        With grMangueras.RootTable.Columns("ajtotal")
            .Caption = "TOTAL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 120
            .FormatString = "0.00"
            .EditType = EditType.NoEdit
            .AllowSort = False
            .AggregateFunction = AggregateFunction.Sum

        End With

        With grMangueras.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grMangueras
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007



        End With




    End Sub
    Private Sub _prCargarGridDetalle1(numi As String)
        Dim dt As New DataTable
        dt = L_prArqueoDetalle1General(numi)

        grDetalle1.DataSource = dt
        grDetalle1.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle1.RootTable.Columns("abnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle1.RootTable.Columns("abnumita1")
            .Width = 50
            .Visible = False
        End With

        With grDetalle1.RootTable.Columns("abcli")
            .Caption = "COD"
            .HeaderAlignment = TextAlignment.Center
            .Width = 40
            .CellStyle.TextAlignment = TextAlignment.Far
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grDetalle1.RootTable.Columns("adnom")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "CLIENTE"
            .Width = 120
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grDetalle1.RootTable.Columns("abmon")
            .Caption = "MONTO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .AggregateFunction = AggregateFunction.Sum
            .FormatString = "0.00"
            .EditType = EditType.NoEdit
            .AllowSort = False
        End With

        With grDetalle1.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle1
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

        End With



        'sumar el total y ponerlo en credito
        Dim total As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
        tbTotalCredito.Value = total

        grPanelAyudaExcel.Visible = True
        '134; 677
        grPanelAyudaExcel.Size = New Size(113, 677)

        grDetalle1.SelectionMode = SelectionMode.SingleSelection
        grDetalle1.SelectedFormatStyle.BackColor = Color.LightYellow

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalle1.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grDetalle1.RootTable.FormatConditions.Add(fc)

        'cargar detalle del detalle
        Dim dtDetalle As DataTable = L_prArqueoDetalleDetalle1General(numi)
        _dtsDesgloseCredito.Tables.Clear()
        For Each fila As DataRow In dt.Rows
            Dim filasFiltradas As DataRow()
            filasFiltradas = dtDetalle.Select("ahnumita11=" + fila.Item("abnumi").ToString.Trim)
            If filasFiltradas.Count > 0 Then
                _dtsDesgloseCredito.Tables.Add(filasFiltradas.CopyToDataTable)
            Else
                _dtsDesgloseCredito.Tables.Add(_dtModelo.Copy)
            End If

        Next
    End Sub

    Private Sub _prCargarGridPagosAnticipados(numi As String)
        Dim dt As New DataTable
        dt = L_prArqueoDetalle3PagosAnticipadosGeneral(numi)

        grPagosAnticipados.DataSource = dt
        grPagosAnticipados.RetrieveStructure()

        'dar formato a las columnas
        With grPagosAnticipados.RootTable.Columns("abnumi")
            .Width = 50
            .Visible = False
        End With
        With grPagosAnticipados.RootTable.Columns("abnumita1")
            .Width = 50
            .Visible = False
        End With

        With grPagosAnticipados.RootTable.Columns("abcli")
            .Caption = "COD"
            .HeaderAlignment = TextAlignment.Center
            .Width = 50
            .CellStyle.TextAlignment = TextAlignment.Far
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grPagosAnticipados.RootTable.Columns("adnom")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "CLIENTE"
            .Width = 120
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grPagosAnticipados.RootTable.Columns("abmon")
            .Caption = "MONTO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .AggregateFunction = AggregateFunction.Sum
            .CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit

        End With

        With grPagosAnticipados.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grPagosAnticipados
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

        End With

        'sumar el total y ponerlo en credito
        Dim total As Double = grPagosAnticipados.GetTotal(grPagosAnticipados.RootTable.Columns("abmon"), AggregateFunction.Sum)
        tbTotalAnticipo.Value = total

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grPagosAnticipados.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grPagosAnticipados.RootTable.FormatConditions.Add(fc)

        'cargar detalle del detalle
        Dim dtDetalle As DataTable = L_prArqueoDetalleDetalle1General(numi)
        _dtsDesgloseAnticipo.Tables.Clear()
        For Each fila As DataRow In dt.Rows
            Dim filasFiltradas As DataRow()
            filasFiltradas = dtDetalle.Select("ahnumita11=" + fila.Item("abnumi").ToString.Trim)
            If filasFiltradas.Count > 0 Then
                _dtsDesgloseAnticipo.Tables.Add(filasFiltradas.CopyToDataTable)
            Else
                _dtsDesgloseAnticipo.Tables.Add(_dtModelo.Copy)
            End If

        Next
    End Sub

    Private Sub _prCargarGridDetalle2(numi As String)
        Dim dt As New DataTable
        dt = L_prArqueoDetalle2General(numi)

        grDetalle2.DataSource = dt
        grDetalle2.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle2.RootTable.Columns("acnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle2.RootTable.Columns("acnumita1")
            .Width = 50
            .Visible = False
        End With


        With grDetalle2.RootTable.Columns("accorte")
            .Visible = False
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grDetalle2.RootTable.Columns("cndesc1")
            .HeaderAlignment = TextAlignment.Center
            .CellStyle.TextAlignment = TextAlignment.Far
            .Caption = "CORTE"
            .Width = 100
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With
        With grDetalle2.RootTable.Columns("valor")
            .Width = 50
            .Visible = False
        End With
        With grDetalle2.RootTable.Columns("accant")
            .Caption = "CANTIDAD"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 90
            .CellStyle.BackColor = Color.AliceBlue
            .AllowSort = False
            .FormatString = "0.00"
        End With

        With grDetalle2.RootTable.Columns("total")
            .Caption = "TOTAL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 90
            .AggregateFunction = AggregateFunction.Sum
            .EditType = EditType.NoEdit
            .AllowSort = False
            .FormatString = "0.00"
        End With

        With grDetalle2.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle2.RootTable.Columns("orden1")
            .Visible = False
        End With

        With grDetalle2
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalle2.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grDetalle2.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub _prCargarGridDetalle3(numi As String)
        Dim dt As New DataTable
        dt = L_prArqueoDetalle3General(numi)

        grTarjetas.DataSource = dt
        grTarjetas.RetrieveStructure()

        'dar formato a las columnas
        With grTarjetas.RootTable.Columns("ahnumi")
            .Width = 50
            .Visible = False
        End With
        With grTarjetas.RootTable.Columns("ahnumita1")
            .Width = 50
            .Visible = False
        End With

        With grTarjetas.RootTable.Columns("ahcod")
            .Caption = "COD"
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .CellStyle.TextAlignment = TextAlignment.Far
            .Visible = False
            .AllowSort = False

        End With

        With grTarjetas.RootTable.Columns("ahmon")
            .Caption = "MONTO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .AggregateFunction = AggregateFunction.Sum
            .CellStyle.BackColor = Color.AliceBlue
            .AllowSort = False
            .FormatString = "0.00"
        End With

        With grTarjetas.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grTarjetas
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

        End With

        'sumar el total y ponerlo en credito
        Dim total As Double = grTarjetas.GetTotal(grTarjetas.RootTable.Columns("ahmon"), AggregateFunction.Sum)
        tbTotalTarjeta.Value = total

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grTarjetas.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grTarjetas.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub _prCargarGridProductos(numi As String)
        Dim dt As New DataTable
        dt = L_prArqueoDetalle4ProductosGeneral(numi)

        grProductos.DataSource = dt
        grProductos.RetrieveStructure()

        'dar formato a las columnas
        With grProductos.RootTable.Columns("ainumi")
            .Width = 50
            .Visible = False
        End With
        With grProductos.RootTable.Columns("ainumita1")
            .Width = 50
            .Visible = False
        End With

        With grProductos.RootTable.Columns("ainumitc8")
            .Caption = "COD"
            .HeaderAlignment = TextAlignment.Center
            .Width = 50
            .CellStyle.TextAlignment = TextAlignment.Far
            .EditType = EditType.NoEdit
            .Visible = False
        End With

        With grProductos.RootTable.Columns("cicdprod1")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "PRODUCTO"
            .Width = 120
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grProductos.RootTable.Columns("aicant")
            .Caption = "CANT"
            .HeaderAlignment = TextAlignment.Center
            .Width = 60
            .AllowSort = False
            .CellStyle.TextAlignment = TextAlignment.Far
        End With

        With grProductos.RootTable.Columns("aiprecio")
            .Caption = "PRECIO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .EditType = EditType.NoEdit
            .AllowSort = False
            .FormatString = "0.00"
        End With

        With grProductos.RootTable.Columns("aitot")
            .Caption = "TOTAL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .AggregateFunction = AggregateFunction.Sum
            .FormatString = "0.00"
            .EditType = EditType.NoEdit
            .AllowSort = False

        End With

        With grProductos.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grProductos
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

        End With

        'sumar el total y ponerlo en credito
        'Dim total As Double = grProductos.GetTotal(grProductos.RootTable.Columns("aitot"), AggregateFunction.Sum)
        'tbTotalAnticipo.Value = total


    End Sub


    'Private Sub _prCargarGridAyudaCalculadora()
    '    Dim dt As New DataTable
    '    dt.Columns.Add("monto", GetType(Double))

    '    grAyuda.DataSource = dt
    '    grAyuda.RetrieveStructure()



    '    With grAyuda.RootTable.Columns("monto")
    '        .Caption = "MONTO"
    '        .CellStyle.TextAlignment = TextAlignment.Far
    '        .HeaderAlignment = TextAlignment.Center
    '        .Width = 104
    '        .AggregateFunction = AggregateFunction.Sum
    '        .CellStyle.BackColor = Color.AliceBlue
    '        .FormatString = "0.00"
    '    End With


    '    With grAyuda
    '        .GroupByBoxVisible = False
    '        'diseño de la grilla
    '        .VisualStyle = VisualStyle.Office2007

    '        'poner fila de totales
    '        '.TotalRow = InheritableBoolean.True
    '        '.TotalRowFormatStyle.BackColor = Color.Gold
    '        '.TotalRowPosition = TotalRowPosition.BottomFixed
    '        .AllowAddNew = InheritableBoolean.True
    '        .NewRowPosition = NewRowPosition.BottomRow
    '    End With

    '    'sumar el total y ponerlo en credito
    '    Dim total As Double = grAyuda.GetTotal(grAyuda.RootTable.Columns("monto"), AggregateFunction.Sum)
    '    tbTotalAyuda.Value = total

    '    'Dim fc As GridEXFormatCondition
    '    'fc = New GridEXFormatCondition(grTarjetas.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
    '    'fc.FormatStyle.BackColor = Color.LightGreen
    '    'grTarjetas.RootTable.FormatConditions.Add(fc)
    'End Sub

    Private Sub _prCargarGridDesgloseCredito(pos As Integer)
        Dim dt As New DataTable
        dt = _dtsDesgloseCredito.Tables(pos)

        grAyuda.DataSource = dt
        grAyuda.RetrieveStructure()

        With grAyuda.RootTable.Columns("ahnumi")
            .Width = 50
            .Visible = False
        End With
        With grAyuda.RootTable.Columns("ahnumita11")
            .Width = 50
            .Visible = False
        End With

        With grAyuda.RootTable.Columns("ahmonto")
            .Caption = "MONTO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 104
            .AggregateFunction = AggregateFunction.Sum
            .CellStyle.BackColor = Color.AliceBlue
            .AllowSort = False
            .FormatString = "0.00"
        End With

        With grAyuda.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grAyuda
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed
            .AllowAddNew = grDetalle1.AllowAddNew
            .NewRowPosition = NewRowPosition.BottomRow
            .AllowEdit = grDetalle1.AllowEdit
        End With

        grAyuda.RemoveFilters()
        grAyuda.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grAyuda.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

        'sumar el total y ponerlo en credito
        Dim total As Double = grAyuda.GetTotal(grAyuda.RootTable.Columns("ahmonto"), AggregateFunction.Sum)
        'tbTotalAyuda.Value = total

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grTarjetas.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grTarjetas.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub _prCargarGridDesgloseAnticipo(pos As Integer)
        Dim dt As New DataTable
        dt = _dtsDesgloseAnticipo.Tables(pos)

        grAyuda.DataSource = dt
        grAyuda.RetrieveStructure()

        With grAyuda.RootTable.Columns("ahnumi")
            .Width = 50
            .Visible = False
        End With
        With grAyuda.RootTable.Columns("ahnumita11")
            .Width = 50
            .Visible = False
        End With

        With grAyuda.RootTable.Columns("ahmonto")
            .Caption = "MONTO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 104
            .AggregateFunction = AggregateFunction.Sum
            .CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With grAyuda.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grAyuda
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed
            .AllowAddNew = grDetalle1.AllowAddNew
            .NewRowPosition = NewRowPosition.BottomRow
            .AllowEdit = grDetalle1.AllowEdit
        End With

        grAyuda.RemoveFilters()
        grAyuda.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grAyuda.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

        'sumar el total y ponerlo en credito
        Dim total As Double = grAyuda.GetTotal(grAyuda.RootTable.Columns("ahmonto"), AggregateFunction.Sum)
        'tbTotalAyuda.Value = total

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grTarjetas.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grTarjetas.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub _prEliminarFilaDetalleMangueras()
        If grMangueras.Row >= 0 Then

            Dim estado As Integer = grMangueras.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grMangueras.GetRow(grMangueras.Row).BeginEdit()
                grMangueras.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grMangueras.GetRow(grMangueras.Row).BeginEdit()
                grMangueras.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grMangueras.RemoveFilters()
            grMangueras.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grMangueras.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            Dim total As Double = grMangueras.GetTotal(grMangueras.RootTable.Columns("ajtotal"), AggregateFunction.Sum)
            tbTotal.Value = total

        End If
    End Sub

    Private Sub _prEliminarFilaDetalle1()
        If grDetalle1.Row >= 0 Then

            Dim estado As Integer = grDetalle1.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grDetalle1.GetRow(grDetalle1.Row).BeginEdit()
                grDetalle1.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grDetalle1.GetRow(grDetalle1.Row).BeginEdit()
                grDetalle1.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grDetalle1.RemoveFilters()
            grDetalle1.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle1.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            Dim total As Double
            total = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
            tbTotalCredito.Value = total

            'eliminar el datatable del desglose del dataset
            _dtsDesgloseCredito.Tables.RemoveAt(grDetalle1.Row)

            '_prIsBalanceado()
        End If
    End Sub

    Private Sub _prEliminarFilaDetalle3()
        If grTarjetas.Row >= 0 Then

            Dim estado As Integer = grTarjetas.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grTarjetas.GetRow(grTarjetas.Row).BeginEdit()
                grTarjetas.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grTarjetas.GetRow(grTarjetas.Row).BeginEdit()
                grTarjetas.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grTarjetas.RemoveFilters()
            grTarjetas.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grTarjetas.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            '_prIsBalanceado()

            'calcular el total tarjetas PANDA
            Dim total As Double = grTarjetas.GetTotal(grTarjetas.RootTable.Columns("ahmon"), AggregateFunction.Sum)
            tbTotalTarjeta.Value = total
        End If
    End Sub

    Private Sub _prEliminarFilaDetalleDesglose()
        If grAyuda.Row >= 0 Then

            Dim estado As Integer = grAyuda.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grAyuda.GetRow(grAyuda.Row).BeginEdit()
                grAyuda.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grAyuda.GetRow(grAyuda.Row).BeginEdit()
                grAyuda.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grAyuda.RemoveFilters()
            grAyuda.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grAyuda.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            Dim total As Double = grAyuda.GetTotal(grAyuda.RootTable.Columns("ahmonto"), AggregateFunction.Sum)

            If SuperTabControl2.SelectedTabIndex = 0 Then
                'Dim dt As DataTable = CType(grDetalle1.DataSource, DataTable)
                'dt.Rows(grDetalle1.Row).Item("abmon") = total

                grDetalle1.GetRow(grDetalle1.Row).BeginEdit()
                grDetalle1.CurrentRow.Cells.Item("abmon").Value = total
                grDetalle1.GetRow(grDetalle1.Row).EndEdit()
                grDetalle1.Refresh()

                total = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
                tbTotalCredito.Value = total

            End If

            If SuperTabControl2.SelectedTabIndex = 2 Then
                'Dim dt As DataTable = CType(grPagosAnticipados.DataSource, DataTable)
                'dt.Rows(grPagosAnticipados.Row).Item("abmon") = total

                grPagosAnticipados.GetRow(grPagosAnticipados.Row).BeginEdit()
                grPagosAnticipados.CurrentRow.Cells.Item("abmon").Value = total
                grPagosAnticipados.GetRow(grPagosAnticipados.Row).EndEdit()
                grPagosAnticipados.Refresh()

                total = grPagosAnticipados.GetTotal(grPagosAnticipados.RootTable.Columns("abmon"), AggregateFunction.Sum)
                tbTotalAnticipo.Value = total
            Else

            End If

            'Dim dt As DataTable = CType(grDetalle1.DataSource, DataTable)
            'dt.Rows(grDetalle1.Row).Item("abmon") = total

            'total = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
            'tbTotalCredito.Value = total


        End If
    End Sub

    Private Sub _prEliminarFilaDetalle3Anticipo()
        If grPagosAnticipados.Row >= 0 Then

            Dim estado As Integer = grPagosAnticipados.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grPagosAnticipados.GetRow(grPagosAnticipados.Row).BeginEdit()
                grPagosAnticipados.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grPagosAnticipados.GetRow(grPagosAnticipados.Row).BeginEdit()
                grPagosAnticipados.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grPagosAnticipados.RemoveFilters()
            grPagosAnticipados.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grPagosAnticipados.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            'eliminar el datatable del desglose del dataset
            _dtsDesgloseAnticipo.Tables.RemoveAt(grPagosAnticipados.Row)

            '_prIsBalanceado()
        End If
    End Sub
    Private Sub _prEliminarFilaDetalle4()
        If grProductos.Row >= 0 Then

            Dim estado As Integer = grProductos.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grProductos.GetRow(grProductos.Row).BeginEdit()
                grProductos.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grProductos.GetRow(grProductos.Row).BeginEdit()
                grProductos.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grProductos.RemoveFilters()
            grProductos.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grProductos.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            'calcular el total productos PANDA
            Dim total As Double = grProductos.GetTotal(grProductos.RootTable.Columns("aitot"), AggregateFunction.Sum)
            tbTotalProd.Value = total
        End If
    End Sub

    'Private Sub _prIsBalanceado()
    '    'consulto si esta descuadrado el total
    '    Dim debeBs As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("obdebebs"), AggregateFunction.Sum)
    '    Dim haberBs As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("obhaberbs"), AggregateFunction.Sum)
    '    Dim debeSus As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("obdebeus"), AggregateFunction.Sum)
    '    Dim haberSus As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("obhaberus"), AggregateFunction.Sum)

    '    tbBalanceBs.Tag = False
    '    If debeBs = haberBs Then
    '        tbBalanceBs.Value = True
    '    Else
    '        tbBalanceBs.Value = False
    '    End If

    '    tbBalanceSus.Tag = False
    '    If debeSus = haberSus Then
    '        tbBalanceSus.Value = True
    '    Else
    '        tbBalanceSus.Value = False
    '    End If

    '    tbBalanceBs.Tag = True
    '    tbBalanceSus.Tag = True
    'End Sub
    Private Sub _prImprimir()
        Dim objrep As New R_Arqueo
        Dim dt As New DataTable
        dt = L_prArqueoReporte(tbNumi.Text)

        'ahora lo mando al visualizador
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("combustible1", tbCombustible1.Text)
        objrep.SetParameterValue("combustible2", tbCombustible2.Text)
        objrep.SetParameterValue("combustible3", tbCombustible3.Text)
        objrep.SetParameterValue("combustible4", tbCombustible4.Text)

        objrep.SetParameterValue("man1", tbMan1.Text)
        objrep.SetParameterValue("man2", tbMan2.Text)
        objrep.SetParameterValue("man3", tbMan3.Text)
        objrep.SetParameterValue("man4", tbMan4.Text)

        objrep.SetParameterValue("precio1", tbPrecio1.Text)
        objrep.SetParameterValue("precio2", tbPrecio2.Text)
        objrep.SetParameterValue("precio3", tbPrecio3.Text)
        objrep.SetParameterValue("precio4", tbPrecio4.Text)

        objrep.SetParameterValue("maquina", tbMaquina.Text)

        'INSERTAR LAS MONEDAS
        Dim dtDetalleMonedas As DataTable = CType(grDetalle2.DataSource, DataTable)
        objrep.SetParameterValue("mo200", dtDetalleMonedas.Rows(0).Item("total"))
        objrep.SetParameterValue("mo100", dtDetalleMonedas.Rows(1).Item("total"))
        objrep.SetParameterValue("mo50", dtDetalleMonedas.Rows(2).Item("total"))
        objrep.SetParameterValue("mo20", dtDetalleMonedas.Rows(3).Item("total"))
        objrep.SetParameterValue("mo10", dtDetalleMonedas.Rows(4).Item("total"))
        objrep.SetParameterValue("mo5", dtDetalleMonedas.Rows(5).Item("total"))
        objrep.SetParameterValue("monedas", dtDetalleMonedas.Rows(6).Item("total"))
        objrep.SetParameterValue("totalSumado", tbTotalSumado.Value)
        objrep.SetParameterValue("totalCredito", tbTotalCredito.Value)
        objrep.SetParameterValue("totalAnticipo", tbTotalAnticipo.Value)
        objrep.SetParameterValue("totalEfectivo", tbTotalEfec.Value)
        objrep.SetParameterValue("montoDiferencia", tbDescuadre.Value)
        objrep.SetParameterValue("totalProductos", tbTotalProd.Value)

        Dim total As Double = grMangueras.GetTotal(grMangueras.RootTable.Columns("mitTotal"), AggregateFunction.Sum)
        objrep.SetParameterValue("totalMitters", total)


        total = grMangueras.GetTotal(grMangueras.RootTable.Columns("ajtotal"), AggregateFunction.Sum)
        'objrep.SetParameterValue("totalMittersSuma", total)
        objrep.SetParameterValue("totalMittersSuma", tbTotal.Value)

        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

    End Sub

#End Region

#Region "METODOS PARA LLENAR"

    Public Sub _PMOHabilitar()
        If _MNuevo = True Then
            tbMaquina.ReadOnly = False

        End If

        tbFin1.IsInputReadOnly = False
        tbFin2.IsInputReadOnly = False
        tbFin3.IsInputReadOnly = False
        tbFin4.IsInputReadOnly = False

        tbCali1.IsInputReadOnly = False
        tbCali2.IsInputReadOnly = False
        tbCali3.IsInputReadOnly = False
        tbCali4.IsInputReadOnly = False

        tbTotalDol.IsInputReadOnly = False
        'tbTotalEfec.IsInputReadOnly = False
        'tbTotalTarjeta.IsInputReadOnly = False

        tbTurno.ReadOnly = False
        'tbTipoCambio.IsInputReadOnly = False
        tbFecha.Enabled = True
        tbVendedor.ReadOnly = False

        tbObs.ReadOnly = False

        grDetalle1.ContextMenuStrip = ContextMenuStrip1
        grDetalle1.AllowAddNew = InheritableBoolean.True
        grDetalle1.AllowEdit = InheritableBoolean.True

        grDetalle2.AllowEdit = InheritableBoolean.True


        grPagosAnticipados.ContextMenuStrip = ContextMenuStrip2
        grPagosAnticipados.AllowAddNew = InheritableBoolean.True
        grPagosAnticipados.AllowEdit = InheritableBoolean.True

        grTarjetas.ContextMenuStrip = ContextMenuStrip3
        grTarjetas.AllowAddNew = InheritableBoolean.True
        grTarjetas.AllowEdit = InheritableBoolean.True

        grAyuda.ContextMenuStrip = ContextMenuStripDesglose
        grAyuda.AllowAddNew = InheritableBoolean.True
        grAyuda.AllowEdit = InheritableBoolean.True

        grProductos.ContextMenuStrip = ContextMenuStrip4
        grProductos.AllowAddNew = InheritableBoolean.True
        grProductos.AllowEdit = InheritableBoolean.True

        BubbleBarUsuario.Visible = False

        btnNuevoTipoCambio.Visible = True

        If _MNuevo = True Then
            grMangueras.ContextMenuStrip = ContextMenuStripMangueras
        End If
    End Sub

    Public Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True

        tbMaquina.ReadOnly = True

        tbMan1.ReadOnly = True
        tbMan2.ReadOnly = True
        tbMan3.ReadOnly = True
        tbMan4.ReadOnly = True

        tbFin1.IsInputReadOnly = True
        tbFin2.IsInputReadOnly = True
        tbFin3.IsInputReadOnly = True
        tbFin4.IsInputReadOnly = True

        tbCali1.IsInputReadOnly = True
        tbCali2.IsInputReadOnly = True
        tbCali3.IsInputReadOnly = True
        tbCali4.IsInputReadOnly = True

        tbIni1.IsInputReadOnly = True
        tbIni2.IsInputReadOnly = True
        tbIni3.IsInputReadOnly = True
        tbIni4.IsInputReadOnly = True

        tbTotal1.IsInputReadOnly = True
        tbTotal2.IsInputReadOnly = True
        tbTotal3.IsInputReadOnly = True
        tbTotal4.IsInputReadOnly = True

        tbTotalDol.IsInputReadOnly = True
        tbTotalEfec.IsInputReadOnly = True
        tbTotalTarjeta.IsInputReadOnly = True
        tbTotalProd.IsInputReadOnly = True

        tbTurno.ReadOnly = True
        tbTipoCambio.IsInputReadOnly = True
        tbFecha.Enabled = False
        tbVendedor.ReadOnly = True

        tbCombustible1.ReadOnly = True
        tbCombustible2.ReadOnly = True
        tbCombustible3.ReadOnly = True
        tbCombustible4.ReadOnly = True

        tbMitTotal1.IsInputReadOnly = True
        tbMitTotal2.IsInputReadOnly = True
        tbMitTotal3.IsInputReadOnly = True
        tbMitTotal4.IsInputReadOnly = True

        tbPrecio1.IsInputReadOnly = True
        tbPrecio2.IsInputReadOnly = True
        tbPrecio3.IsInputReadOnly = True
        tbPrecio4.IsInputReadOnly = True

        tbTotalCredito.IsInputReadOnly = True
        tbTotalAnticipo.IsInputReadOnly = True
        tbTotalSumado.IsInputReadOnly = True
        tbDescuadre.IsInputReadOnly = True
        tbTotal.IsInputReadOnly = True

        tbObs.ReadOnly = True

        grDetalle1.ContextMenuStrip = Nothing
        grDetalle1.AllowAddNew = InheritableBoolean.False
        grDetalle1.AllowEdit = InheritableBoolean.False

        grDetalle2.ContextMenuStrip = Nothing
        grDetalle2.AllowAddNew = InheritableBoolean.False
        grDetalle2.AllowEdit = InheritableBoolean.False

        grPagosAnticipados.ContextMenuStrip = Nothing
        grPagosAnticipados.AllowAddNew = InheritableBoolean.False
        grPagosAnticipados.AllowEdit = InheritableBoolean.False

        grTarjetas.ContextMenuStrip = Nothing
        grTarjetas.AllowAddNew = InheritableBoolean.False
        grTarjetas.AllowEdit = InheritableBoolean.False

        grProductos.ContextMenuStrip = Nothing
        grProductos.AllowAddNew = InheritableBoolean.False
        grProductos.AllowEdit = InheritableBoolean.False

        BubbleBarUsuario.Visible = True

        btnNuevoTipoCambio.Visible = False

    End Sub

    Public Sub _PMOLimpiar()
        'VACIO EL DETALLE
        _prCargarGridDetalle1(-1)
        _prCargarGridDetalle2(-1)
        _prCargarGridPagosAnticipados(-1)
        _prCargarGridDetalle3(-1)
        _prCargarGridProductos(-1)
        _prCargarGridMangueras(-1)

        tbMaquina.Text = ""

        tbMan1.Text = ""
        tbMan2.Text = ""
        tbMan3.Text = ""
        tbMan4.Text = ""

        tbFin1.Value = 0
        tbFin2.Value = 0
        tbFin3.Value = 0
        tbFin4.Value = 0

        tbCali1.Value = 0
        tbCali2.Value = 0
        tbCali3.Value = 0
        tbCali4.Value = 0

        tbIni1.Value = 0
        tbIni2.Value = 0
        tbIni3.Value = 0
        tbIni4.Value = 0

        tbTotal1.Value = 0
        tbTotal2.Value = 0
        tbTotal3.Value = 0
        tbTotal4.Value = 0

        tbTotalDol.Value = 0
        tbTotalEfec.Value = 0
        tbTotalTarjeta.Value = 0
        tbTotalProd.Value = 0

        tbTurno.Text = ""
        tbTipoCambio.Text = gd_tipoCambioCarburantes
        tbVendedor.Text = ""
        tbCajero.Text = ""
        tbFecha.Value = Now.Date

        tbMitTotal1.Value = 0
        tbCombustible1.Text = ""
        tbMitTotal2.Value = 0
        tbCombustible2.Text = ""

        tbMitTotal3.Value = 0
        tbCombustible3.Text = ""
        tbMitTotal4.Value = 0
        tbCombustible4.Text = ""

        tbPrecio1.Value = 0
        tbPrecio2.Value = 0
        tbPrecio3.Value = 0
        tbPrecio4.Value = 0

        tbTotalCredito.Value = 0
        tbTotalAnticipo.Value = 0
        tbTotal.Value = 0

        tbObs.Text = ""

    End Sub

    Public Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbMan1.BackColor = Color.White
        tbMan2.BackColor = Color.White

        tbFin1.BackColor = Color.White
        tbFin2.BackColor = Color.White
        tbIni1.BackColor = Color.White
        tbIni2.BackColor = Color.White
        tbTotal1.BackColor = Color.White
        tbTotal2.BackColor = Color.White
        tbTotalDol.BackColor = Color.White
        tbTotalEfec.BackColor = Color.White
        tbTotalTarjeta.BackColor = Color.White

        tbTurno.BackColor = Color.White
        tbTipoCambio.BackColor = Color.White
        tbVendedor.BackColor = Color.White
        tbCajero.BackColor = Color.White
        tbFecha.Value = Now.Date
    End Sub

    Public Function _PMOGrabarRegistro() As Boolean

        Dim dtDetalle1 As DataTable = CType(grDetalle1.DataSource, DataTable).DefaultView.ToTable(True, "abnumi", "abnumita1", "abcli", "abmon", "estado")
        Dim dtDetalle2 As DataTable = CType(grDetalle2.DataSource, DataTable).DefaultView.ToTable(True, "acnumi", "acnumita1", "accorte", "accant", "estado")
        Dim dtPagosAnticipados As DataTable = CType(grPagosAnticipados.DataSource, DataTable).DefaultView.ToTable(True, "abnumi", "abnumita1", "abcli", "abmon", "estado")
        Dim dttarjetas As DataTable = CType(grTarjetas.DataSource, DataTable)
        Dim dtDetalle4 As DataTable = CType(grProductos.DataSource, DataTable).DefaultView.ToTable(True, "ainumi", "ainumita1", "ainumitc8", "aicant", "aiprecio", "aitot", "estado")

        Dim dtDetalleMangueras As DataTable = CType(grMangueras.DataSource, DataTable).DefaultView.ToTable(True, "ajnumi", "ajnumita1", "ajman", "ajmitini", "ajmitfin", "ajmitcali", "ajtotal", "estado")

        'preparar el datatable sub detalle
        Dim i As Integer = 1
        For Each fila As DataRow In dtDetalle1.Rows
            If fila.Item("estado") = 0 Or fila.Item("estado") = 1 Or fila.Item("estado") = 2 Then
                fila.Item("abnumi") = i
                Dim dtElem As DataTable = _dtsDesgloseCredito.Tables(i - 1)
                For Each filaDetalle As DataRow In dtElem.Rows
                    filaDetalle.Item("ahnumita11") = i
                Next
                i = i + 1
            End If
        Next
        Dim dtDetalleDetalle As DataTable = _dtModelo.Copy
        For Each dtTable As DataTable In _dtsDesgloseCredito.Tables
            dtDetalleDetalle.Merge(dtTable)
        Next

        'preparar el datatable sub detalle
        i = 1
        For Each fila As DataRow In dtPagosAnticipados.Rows
            If fila.Item("estado") = 0 Or fila.Item("estado") = 1 Or fila.Item("estado") = 2 Then
                fila.Item("abnumi") = i
                Dim dtElem As DataTable = _dtsDesgloseAnticipo.Tables(i - 1)
                For Each filaDetalle As DataRow In dtElem.Rows
                    filaDetalle.Item("ahnumita11") = i
                Next
                i = i + 1
            End If

        Next
        Dim dtDetalleDetalleAnticipo As DataTable = _dtModelo.Copy
        For Each dtTable As DataTable In _dtsDesgloseAnticipo.Tables
            dtDetalleDetalleAnticipo.Merge(dtTable)
        Next


        For Each fila As DataRow In dtPagosAnticipados.Rows
            fila.Item("abmon") = IIf(IsDBNull(fila.Item("abmon")) = True, 0, fila.Item("abmon")) * -1
        Next
        dtDetalle1.Merge(dtPagosAnticipados)

        Dim res As Boolean = L_prArqueoGrabar(tbNumi.Text, tbFecha.Value.ToString("yyyy/MM/dd"), tbTurno.Value, tbVendedor.Value, tbMaquina.Value, tbMan1.Tag, tbIni1.Value, tbFin1.Value, tbTotal1.Value, tbMan2.Tag, tbIni2.Value, tbFin2.Value, tbTotal2.Value, tbMan3.Tag, tbIni3.Value, tbFin3.Value, tbTotal3.Value, tbMan4.Tag, tbIni4.Value, tbFin4.Value, tbTotal4.Value, tbTotalEfec.Value, tbTotalTarjeta.Value, tbTotalDol.Value, tbTipoCambio.Value, tbObs.Text, tbCajero.Value, tbTotalProd.Value, tbCali1.Value, tbCali2.Value, tbCali3.Value, tbCali4.Value, dtDetalle1, dtDetalle2, dttarjetas, dtDetalleDetalle, dtDetalleDetalleAnticipo, dtDetalle4, dtDetalleMangueras)

        If res Then
            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)


            Dim info As New TaskDialogInfo("arqueo".ToUpper, eTaskDialogIcon.Delete, "arqueo de caja".ToUpper, "¿Desea imprimir el arqueo de caja?".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                _prImprimir()
            End If
        End If
        Return res

    End Function

    Public Function _PMOModificarRegistro() As Boolean

        Dim dtDetalle1 As DataTable = CType(grDetalle1.DataSource, DataTable).DefaultView.ToTable(True, "abnumi", "abnumita1", "abcli", "abmon", "estado")
        Dim dtDetalle2 As DataTable = CType(grDetalle2.DataSource, DataTable).DefaultView.ToTable(True, "acnumi", "acnumita1", "accorte", "accant", "estado")
        Dim dtPagosAnticipados As DataTable = CType(grPagosAnticipados.DataSource, DataTable).DefaultView.ToTable(True, "abnumi", "abnumita1", "abcli", "abmon", "estado")
        Dim dttarjetas As DataTable = CType(grTarjetas.DataSource, DataTable)
        Dim dtDetalle4 As DataTable = CType(grProductos.DataSource, DataTable).DefaultView.ToTable(True, "ainumi", "ainumita1", "ainumitc8", "aicant", "aiprecio", "aitot", "estado")

        Dim dtDetalleMangueras As DataTable = CType(grMangueras.DataSource, DataTable).DefaultView.ToTable(True, "ajnumi", "ajnumita1", "ajman", "ajmitini", "ajmitfin", "ajmitcali", "ajtotal", "estado")

        'preparar el datatable sub detalle
        Dim i As Integer = 1
        For Each fila As DataRow In dtDetalle1.Rows
            If fila.Item("estado") = 0 Or fila.Item("estado") = 1 Or fila.Item("estado") = 2 Then
                fila.Item("abnumi") = i
                Dim dtElem As DataTable = _dtsDesgloseCredito.Tables(i - 1)
                For Each filaDetalle As DataRow In dtElem.Rows
                    filaDetalle.Item("ahnumita11") = i
                Next
                i = i + 1
            End If


        Next
        Dim dtDetalleDetalle As DataTable = _dtModelo.Copy
        For Each dtTable As DataTable In _dtsDesgloseCredito.Tables
            dtDetalleDetalle.Merge(dtTable)
        Next

        'preparar el datatable sub detalle
        i = 1
        For Each fila As DataRow In dtPagosAnticipados.Rows
            If fila.Item("estado") = 0 Or fila.Item("estado") = 1 Or fila.Item("estado") = 2 Then
                fila.Item("abnumi") = i
                Dim dtElem As DataTable = _dtsDesgloseAnticipo.Tables(i - 1)
                For Each filaDetalle As DataRow In dtElem.Rows
                    filaDetalle.Item("ahnumita11") = i
                Next
                i = i + 1
            End If


        Next
        Dim dtDetalleDetalleAnticipo As DataTable = _dtModelo.Copy
        For Each dtTable As DataTable In _dtsDesgloseAnticipo.Tables
            dtDetalleDetalleAnticipo.Merge(dtTable)
        Next


        For Each fila As DataRow In dtPagosAnticipados.Rows
            fila.Item("abmon") = IIf(IsDBNull(fila.Item("abmon")) = True, 0, fila.Item("abmon")) * -1
        Next
        dtDetalle1.Merge(dtPagosAnticipados)

        Dim res As Boolean = L_prArqueoModificar(tbNumi.Text, tbFecha.Value.ToString("yyyy/MM/dd"), tbTurno.Value, tbVendedor.Value, tbMaquina.Value, tbMan1.Tag, tbIni1.Value, tbFin1.Value, tbTotal1.Value, tbMan2.Tag, tbIni2.Value, tbFin2.Value, tbTotal2.Value, tbMan3.Tag, tbIni3.Value, tbFin3.Value, tbTotal3.Value, tbMan4.Tag, tbIni4.Value, tbFin4.Value, tbTotal4.Value, tbTotalEfec.Value, tbTotalTarjeta.Value, tbTotalDol.Value, tbTipoCambio.Value, tbObs.Text, tbCajero.Value, tbTotalProd.Value, tbCali1.Value, tbCali2.Value, tbCali3.Value, tbCali4.Value, dtDetalle1, dtDetalle2, dttarjetas, dtDetalleDetalle, dtDetalleDetalleAnticipo, dtDetalle4, dtDetalleMangueras)
        If res Then

            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            '_PSalirRegistro()
        End If
        Return res
    End Function

    Public Sub _PMOEliminarRegistro()

        Dim dtDetallas As DataTable = CType(grMangueras.DataSource, DataTable)
        For Each fila As DataRow In dtDetallas.Rows
            Dim dt As DataTable = L_prArqueoObtenerUltimoRegistroManguera(fila.Item("ajman").ToString)

            If dt.Rows(0).Item("aanumi") <> tbNumi.Text Then
                ToastNotification.Show(Me, "solo se puede eliminar el ultimo registro de esta maquina".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
                Exit Sub
            End If
        Next


        Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prArqueoBorrar(tbNumi.Text, tbFecha.Value.ToString("yyyy/MM/dd"), tbTurno.Value, tbVendedor.Value, tbMaquina.Value, tbMan1.Tag, tbIni1.Value, tbFin1.Value, tbTotal1.Value, tbMan2.Tag, tbIni2.Value, tbFin2.Value, tbTotal2.Value, tbMan3.Tag, tbIni3.Value, tbFin3.Value, tbTotal3.Value, tbMan4.Tag, tbIni4.Value, tbFin4.Value, tbTotal4.Value, tbTotalEfec.Value, tbTotalTarjeta.Value, tbTotalDol.Value, tbTipoCambio.Value, tbObs.Text, tbCajero.Value, tbTotalProd.Value, tbCali1.Value, tbCali2.Value, tbCali3.Value, tbCali4.Value, mensajeError)
            If res Then
                ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
    Public Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()



        If tbTurno.SelectedIndex < 0 Then
            tbTurno.BackColor = Color.Red
            MEP.SetError(tbTurno, "seleccione el turno!".ToUpper)
            _ok = False
        Else
            tbTurno.BackColor = Color.White
            MEP.SetError(tbTurno, "")
        End If

        If tbVendedor.SelectedIndex < 0 Then
            tbVendedor.BackColor = Color.Red
            MEP.SetError(tbVendedor, "seleccione el vendedor!".ToUpper)
            _ok = False
        Else
            tbVendedor.BackColor = Color.White
            MEP.SetError(tbVendedor, "")
        End If

        If tbMaquina.SelectedIndex < 0 Then
            tbMan1.BackColor = Color.Red
            MEP.SetError(tbMan1, "seleccione maquina!".ToUpper)
            _ok = False
        Else
            tbMan1.BackColor = Color.White
            MEP.SetError(tbMan1, "")
        End If

        Dim dtMan As DataTable = CType(grMangueras.DataSource, DataTable)
        For Each fila As DataRow In dtMan.Rows
            Dim mitIni As Double = fila.Item("ajmitini")
            Dim mitFin As Double = fila.Item("ajmitfin")
            If fila.Item("estado") >= 0 Then
                If mitFin < mitIni Then
                    ToastNotification.Show(Me, "la manguera ".ToUpper + fila.Item("cndesc1") + " tiene el mitter final menor al inicial".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    _ok = False
                    Exit For
                End If
            End If

        Next

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prArqueoGeneral()
        Return dtBuscador
    End Function

    Public Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        'listEstCeldas.Add(New Modelos.Celda("aanumi", True, "ID", 70))
        'listEstCeldas.Add(New Modelos.Celda("aafec", True, "FECHA", 150))
        'listEstCeldas.Add(New Modelos.Celda("aatur", False))
        'listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "TURNO", 100))
        'listEstCeldas.Add(New Modelos.Celda("aaven", False))
        'listEstCeldas.Add(New Modelos.Celda("panom1", True, "VENDEDOR", 100))

        'listEstCeldas.Add(New Modelos.Celda("aamaq1", False))
        'listEstCeldas.Add(New Modelos.Celda("aedesc1", True, "MAQUINA 1", 100))
        'listEstCeldas.Add(New Modelos.Celda("aamitini1", True, "MIT INICIAL 1", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aamitfin1", True, "MIT FINAL 1", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aatotal1", True, "TOTAL", 120, "0.00"))

        'listEstCeldas.Add(New Modelos.Celda("aamaq2", False))
        'listEstCeldas.Add(New Modelos.Celda("aedesc2", True, "MAQUINA 1", 100))
        'listEstCeldas.Add(New Modelos.Celda("aamitini2", True, "MIT INICIAL 1", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aamitfin2", True, "MIT FINAL 1", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aatotal2", True, "TOTAL", 120, "0.00"))

        'listEstCeldas.Add(New Modelos.Celda("aatotefe", True, "TOTAL EFECTIVO", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aatottar", True, "TOTAL TARJETA", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aatotdol", True, "TOTAL DOLARES", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aatc", True, "TIPO CAMBIO", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aaobs", True, "OBSERVACION", 250))

        Return listEstCeldas
    End Function

    Public Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("aanumi").ToString
            tbFecha.Value = .GetValue("aafec")
            tbTurno.Value = .GetValue("aatur")
            tbVendedor.Value = .GetValue("aaven")
            tbCajero.Value = .GetValue("aacaj")
            tbMaquina.Value = .GetValue("aamaq")


            'tbMan1.Value = .GetValue("aamaq1")
            tbIni1.Value = .GetValue("aamitini1")
            tbFin1.Value = .GetValue("aamitfin1")
            tbTotal1.Value = .GetValue("aatotal1")

            'tbMan2.Value = .GetValue("aamaq2")
            tbIni2.Value = .GetValue("aamitini2")
            tbFin2.Value = .GetValue("aamitfin2")
            tbTotal2.Value = .GetValue("aatotal2")

            tbIni3.Value = .GetValue("aamitini3")
            tbFin3.Value = .GetValue("aamitfin3")
            tbTotal3.Value = .GetValue("aatotal3")

            tbIni4.Value = .GetValue("aamitini4")
            tbFin4.Value = .GetValue("aamitfin4")
            tbTotal4.Value = .GetValue("aatotal4")

            tbTotalEfec.Value = .GetValue("aatotefe")
            tbTotalTarjeta.Value = .GetValue("aatottar")
            tbTotalDol.Value = .GetValue("aatotdol")
            tbTipoCambio.Value = .GetValue("aatc")
            tbObs.Text = .GetValue("aaobs").ToString

            tbTotalProd.Value = IIf(IsDBNull(.GetValue("aaprod")) = True, 0, .GetValue("aaprod"))

            tbCali1.Value = IIf(IsDBNull(.GetValue("aamitcali1")) = True, 0, .GetValue("aamitcali1"))
            tbCali2.Value = IIf(IsDBNull(.GetValue("aamitcali2")) = True, 0, .GetValue("aamitcali2"))
            tbCali3.Value = IIf(IsDBNull(.GetValue("aamitcali3")) = True, 0, .GetValue("aamitcali3"))
            tbCali4.Value = IIf(IsDBNull(.GetValue("aamitcali4")) = True, 0, .GetValue("aamitcali4"))



            'CARGAR DETALLE
            _prCargarGridDetalle1(tbNumi.Text)

            _prCargarGridDetalle2(tbNumi.Text)
            _prCargarGridPagosAnticipados(tbNumi.Text)
            _prCargarGridDetalle3(tbNumi.Text)
            _prCargarGridProductos(tbNumi.Text)

            _prCargarGridMangueras(tbNumi.Text)

            Dim total As Double = grMangueras.GetTotal(grMangueras.RootTable.Columns("ajtotal"), AggregateFunction.Sum)
            ' Dim diferencia As Double =
            tbTotal.Value = total


        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Sub _PMOHabilitarFocus()


    End Sub

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            _modulo.Select()
            _tab.Close()
        End If
    End Sub
#End Region

#Region "EVENTOS"
    Private Sub JGrM_Buscador_SelectionChanged(sender As Object, e As EventArgs)
        If JGrM_Buscador.Row >= 0 And btnGrabar.Enabled = False Then
            _MPos = JGrM_Buscador.Row
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub

    Private Sub MFlyoutUsuario_PrepareContent(sender As Object, e As EventArgs) Handles MFlyoutUsuario.PrepareContent
        If sender Is BubbleBarUsuario And btnGrabar.Enabled = False Then
            MFlyoutUsuario.BorderColor = Color.FromArgb(&HC0, 0, 0)
            MFlyoutUsuario.Content = PanelUsuario
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbFecha.Focus()

        _PMNuevo()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'codigo Carlos
        'Dim dtDetallas As DataTable = CType(grMangueras.DataSource, DataTable)
        'For Each fila As DataRow In dtDetallas.Rows
        '    Dim dt As DataTable = L_prArqueoObtenerUltimoRegistroManguera(fila.Item("ajman").ToString)

        '    If dt.Rows(0).Item("aanumi") <> tbNumi.Text Then
        '        ToastNotification.Show(Me, "solo se puede modificar el ultimo registro de esta maquina".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '        Exit Sub
        '    End If
        'Next

        tbFecha.Focus()
        _PMModificar()

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        _PMEliminar()


    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        _PMGuardar()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PMSalir()
    End Sub

    Private Sub JGrM_Buscador_EditingCell(sender As Object, e As EditingCellEventArgs) Handles JGrM_Buscador.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PMPrimerRegistro()
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        _PMAnteriorRegistro()
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        _PMSiguienteRegistro()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        _PMUltimoRegistro()
    End Sub

    Private Sub F0_Comprobante_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        _prEliminarFilaDetalle1()
    End Sub

    Private Sub grDetalle1_KeyDown(sender As Object, e As KeyEventArgs) Handles grDetalle1.KeyDown
        Dim f As Integer = grDetalle1.Row
        Dim c As Integer = grDetalle1.Col
        If e.KeyData = Keys.Control + Keys.Enter Then
            If grDetalle1.RootTable.Columns(c).Key = "abcli" Or grDetalle1.RootTable.Columns(c).Key = "adnom" Then

                Dim frmAyuda As Modelos.ModeloAyuda
                Dim dt As DataTable

                dt = L_prArqueoClienteAyuda(1)

                Dim listEstCeldas As New List(Of Modelos.Celda)
                listEstCeldas.Add(New Modelos.Celda("adnumi", False))
                listEstCeldas.Add(New Modelos.Celda("adnom", True, "nombre".ToUpper, 250))
                listEstCeldas.Add(New Modelos.Celda("adtelef", True, "telefono".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("addirec", True, "direccion".ToUpper, 150))


                frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cliente".ToUpper, listEstCeldas)
                frmAyuda.ShowDialog()

                If frmAyuda.seleccionado = True Then
                    Dim numi As String = frmAyuda.filaSelect.Cells("adnumi").Value
                    Dim nombre As String = frmAyuda.filaSelect.Cells("adnom").Value

                    If f >= 0 Then
                        grDetalle1.SetValue("abcli", numi)
                        grDetalle1.SetValue("adnom", nombre)
                    Else
                        Dim dt1 As DataTable = CType(grDetalle1.DataSource, DataTable)
                        dt1.Rows.Add(0, 0, numi, nombre, 0, 0)

                        Dim total As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
                        tbTotalCredito.Value = total

                        _dtsDesgloseCredito.Tables.Add(_dtModelo.Copy)
                    End If

                End If
            End If

        End If
    End Sub

    Private Sub grDetalle3Anticipo_KeyDown(sender As Object, e As KeyEventArgs) Handles grPagosAnticipados.KeyDown
        Dim f As Integer = grPagosAnticipados.Row
        Dim c As Integer = grPagosAnticipados.Col
        If e.KeyData = Keys.Control + Keys.Enter Then
            If grPagosAnticipados.RootTable.Columns(c).Key = "abcli" Or grPagosAnticipados.RootTable.Columns(c).Key = "adnom" Then

                Dim frmAyuda As Modelos.ModeloAyuda
                Dim dt As DataTable

                dt = L_prArqueoClienteAyuda(2)

                Dim listEstCeldas As New List(Of Modelos.Celda)
                listEstCeldas.Add(New Modelos.Celda("adnumi", False))
                listEstCeldas.Add(New Modelos.Celda("adnom", True, "nombre".ToUpper, 250))
                listEstCeldas.Add(New Modelos.Celda("adtelef", True, "telefono".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("addirec", True, "direccion".ToUpper, 150))


                frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cliente".ToUpper, listEstCeldas)
                frmAyuda.ShowDialog()

                If frmAyuda.seleccionado = True Then
                    Dim numi As String = frmAyuda.filaSelect.Cells("adnumi").Value
                    Dim nombre As String = frmAyuda.filaSelect.Cells("adnom").Value

                    If f >= 0 Then
                        grPagosAnticipados.SetValue("abcli", numi)
                        grPagosAnticipados.SetValue("adnom", nombre)
                    Else
                        Dim dt1 As DataTable = CType(grPagosAnticipados.DataSource, DataTable)
                        dt1.Rows.Add(0, 0, numi, nombre, 0, 0)

                        Dim total As Double = grPagosAnticipados.GetTotal(grPagosAnticipados.RootTable.Columns("abmon"), AggregateFunction.Sum)
                        tbTotalAnticipo.Value = total

                        _dtsDesgloseAnticipo.Tables.Add(_dtModelo.Copy)
                    End If

                End If
            End If

        End If
    End Sub

    Private Sub tbMaquina1_ValueChanged(sender As Object, e As EventArgs)
        'If tbMan1.SelectedIndex >= 0 Then
        '    Dim fila As DataRow = _dtMaquina.Rows(tbMan1.SelectedIndex)
        '    tbCombustible1.Text = fila.Item("cndesc1")
        '    tbPrecio1.Value = fila.Item("aeprecio")
        '    tbIni1.Value = fila.Item("aemitfin")

        '    tbMan2.Value = fila.Item("aenumita2")
        'End If
    End Sub

    Private Sub tbMaquina2_ValueChanged(sender As Object, e As EventArgs)
        'If tbMan2.SelectedIndex >= 0 Then
        '    Dim fila As DataRow = _dtMaquina.Rows(tbMan2.SelectedIndex)
        '    tbCombustible2.Text = fila.Item("cndesc1")
        '    tbPrecio2.Value = fila.Item("aeprecio")
        '    tbIni2.Value = fila.Item("aemitfin")

        'End If
    End Sub

    Private Sub tbFin1_ValueChanged(sender As Object, e As EventArgs) Handles tbFin1.ValueChanged
        tbMitTotal1.Value = tbFin1.Value - tbIni1.Value
    End Sub

    Private Sub tbFin2_ValueChanged(sender As Object, e As EventArgs) Handles tbFin2.ValueChanged
        tbMitTotal2.Value = tbFin2.Value - tbIni2.Value
    End Sub

    Private Sub tbFin3_ValueChanged(sender As Object, e As EventArgs) Handles tbFin3.ValueChanged
        tbMitTotal3.Value = tbFin3.Value - tbIni3.Value
    End Sub
    Private Sub tbFin4_ValueChanged(sender As Object, e As EventArgs) Handles tbFin4.ValueChanged
        tbMitTotal4.Value = tbFin4.Value - tbIni4.Value
    End Sub

    Private Sub tbMitTotal1_ValueChanged(sender As Object, e As EventArgs) Handles tbMitTotal1.ValueChanged
        tbTotal1.Value = tbMitTotal1.Value * tbPrecio1.Value
    End Sub

    Private Sub tbMitTotal2_ValueChanged(sender As Object, e As EventArgs) Handles tbMitTotal2.ValueChanged
        tbTotal2.Value = tbMitTotal2.Value * tbPrecio2.Value
    End Sub
    Private Sub tbMitTotal3_ValueChanged(sender As Object, e As EventArgs) Handles tbMitTotal3.ValueChanged
        tbTotal3.Value = tbMitTotal3.Value * tbPrecio3.Value
    End Sub
    Private Sub tbMitTotal4_ValueChanged(sender As Object, e As EventArgs) Handles tbMitTotal4.ValueChanged
        tbTotal4.Value = tbMitTotal4.Value * tbPrecio4.Value
    End Sub

    Private Sub tbTotal1_ValueChanged(sender As Object, e As EventArgs) Handles tbTotal4.ValueChanged, tbTotal3.ValueChanged, tbTotal2.ValueChanged, tbTotal1.ValueChanged
        tbTotal.Value = tbTotal1.Value + tbTotal2.Value + tbTotal3.Value + tbTotal4.Value
    End Sub

    Private Sub grDetalle_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles grDetalle2.UpdatingRecord
        If grDetalle2.RootTable.Columns(grDetalle2.Col).Key = "accant" Then
            Dim valor As Double = grDetalle2.GetValue("valor")
            Dim cant As Double = grDetalle2.GetValue("accant")
            Dim cant2 As Integer = grDetalle2.GetValue("accant")
            If grDetalle2.Row <> 6 Then
                cant = cant2
                grDetalle2.SetValue("accant", cant2)
            End If
            grDetalle2.SetValue("total", FormatNumber(valor * cant, 2))
        End If

    End Sub
#End Region

    Private Sub grDetalle1_RecordUpdated(sender As Object, e As EventArgs) Handles grDetalle1.RecordUpdated
        Dim total As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
        tbTotalCredito.Value = total
    End Sub
    Private Sub grDetalle2_RecordUpdated(sender As Object, e As EventArgs) Handles grDetalle2.RecordUpdated
        Dim dt As DataTable = CType(grDetalle2.DataSource, DataTable)
        Dim totalBs As Double = 0
        Dim totalSus As Double = 0
        Dim i As Integer
        For i = 0 To 6
            totalBs = totalBs + dt.Rows(i).Item("total")
        Next
        tbTotalEfec.Value = totalBs

        For i = 7 To dt.Rows.Count - 1
            totalSus = totalSus + dt.Rows(i).Item("total")
        Next
        tbTotalDol.Value = totalSus

        'Dim total As Double = grDetalle2.GetTotal(grDetalle2.RootTable.Columns("total"), AggregateFunction.Sum)
        'tbTotalEfec.Value = total
    End Sub

    Private Sub grDetalle3Anticipo_RecordUpdated(sender As Object, e As EventArgs) Handles grPagosAnticipados.RecordUpdated
        Dim total As Double = grPagosAnticipados.GetTotal(grPagosAnticipados.RootTable.Columns("abmon"), AggregateFunction.Sum)
        tbTotalAnticipo.Value = total
    End Sub
    Private Sub grTarjetas_RecordUpdated(sender As Object, e As EventArgs) Handles grTarjetas.RecordUpdated
        Dim total As Double = grTarjetas.GetTotal(grTarjetas.RootTable.Columns("ahmon"), AggregateFunction.Sum)
        tbTotalTarjeta.Value = total
    End Sub
    Private Sub grAyuda_RecordUpdated(sender As Object, e As EventArgs) Handles grAyuda.RecordUpdated
        Dim total As Double = grAyuda.GetTotal(grAyuda.RootTable.Columns("ahmonto"), AggregateFunction.Sum)
        'tbTotalAyuda.Value = total

        'grDetalle1.SetValue("abmon", total)
        If SuperTabControl2.SelectedTabIndex = 0 Then
            'Dim dt As DataTable = CType(grDetalle1.DataSource, DataTable)
            'dt.Rows(grDetalle1.Row).Item("abmon") = total

            grDetalle1.GetRow(grDetalle1.Row).BeginEdit()
            grDetalle1.CurrentRow.Cells.Item("abmon").Value = total
            grDetalle1.GetRow(grDetalle1.Row).EndEdit()
            grDetalle1.Refresh()

            total = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
            tbTotalCredito.Value = total

        End If

        If SuperTabControl2.SelectedTabIndex = 2 Then
            'Dim dt As DataTable = CType(grPagosAnticipados.DataSource, DataTable)
            'dt.Rows(grPagosAnticipados.Row).Item("abmon") = total

            grPagosAnticipados.GetRow(grPagosAnticipados.Row).BeginEdit()
            grPagosAnticipados.CurrentRow.Cells.Item("abmon").Value = total
            grPagosAnticipados.GetRow(grPagosAnticipados.Row).EndEdit()
            grPagosAnticipados.Refresh()

            total = grPagosAnticipados.GetTotal(grPagosAnticipados.RootTable.Columns("abmon"), AggregateFunction.Sum)
            tbTotalAnticipo.Value = total
        Else

        End If
    End Sub

    Private Sub tbTotalEfec_ValueChanged(sender As Object, e As EventArgs) Handles tbTotalTarjeta.ValueChanged, tbTotalEfec.ValueChanged, tbTotalDol.ValueChanged, tbTotalCredito.ValueChanged, tbTotalAnticipo.ValueChanged
        tbTotalSumado.Value = tbTotalCredito.Value + tbTotalEfec.Value + tbTotalTarjeta.Value + (tbTotalDol.Value * tbTipoCambio.Value) + tbTotalAnticipo.Value
    End Sub

    Private Sub grDetalle1_RecordAdded(sender As Object, e As EventArgs) Handles grDetalle1.RecordAdded
        Dim total As Double = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
        tbTotalCredito.Value = total

        _dtsDesgloseCredito.Tables.Add(_dtModelo.Copy)
    End Sub
    Private Sub grDetalle3Anticipo_RecordAdded(sender As Object, e As EventArgs) Handles grPagosAnticipados.RecordAdded
        Dim total As Double = grPagosAnticipados.GetTotal(grPagosAnticipados.RootTable.Columns("abmon"), AggregateFunction.Sum)
        tbTotalAnticipo.Value = total

        _dtsDesgloseAnticipo.Tables.Add(_dtModelo.Copy)
    End Sub
    Private Sub grTarjeta_RecordAdded(sender As Object, e As EventArgs) Handles grTarjetas.RecordAdded
        Dim total As Double = grTarjetas.GetTotal(grTarjetas.RootTable.Columns("ahmon"), AggregateFunction.Sum)
        tbTotalTarjeta.Value = total

        'lo bueno al primer lugar
        'grTarjetas.MoveTo(0) 'JGr_Detalle.NewRowPosition
        'grTarjetas.Col = -1
    End Sub
    Private Sub grAyuda_RecordAdded(sender As Object, e As EventArgs) Handles grAyuda.RecordAdded
        Dim total As Double = grAyuda.GetTotal(grAyuda.RootTable.Columns("ahmonto"), AggregateFunction.Sum)
        'tbTotalAyuda.Value = total

        'grDetalle1.SetValue("abmon", total)

        If SuperTabControl2.SelectedTabIndex = 0 Then
            'Dim dt As DataTable = CType(grDetalle1.DataSource, DataTable)
            'dt.Rows(grDetalle1.Row).Item("abmon") = total

            grDetalle1.GetRow(grDetalle1.Row).BeginEdit()
            grDetalle1.CurrentRow.Cells.Item("abmon").Value = total
            grDetalle1.GetRow(grDetalle1.Row).EndEdit()
            grDetalle1.Refresh()

            total = grDetalle1.GetTotal(grDetalle1.RootTable.Columns("abmon"), AggregateFunction.Sum)
            tbTotalCredito.Value = total
        End If

        If SuperTabControl2.SelectedTabIndex = 2 Then
            'Dim dt As DataTable = CType(grPagosAnticipados.DataSource, DataTable)
            'dt.Rows(grPagosAnticipados.Row).Item("abmon") = total

            grPagosAnticipados.GetRow(grPagosAnticipados.Row).BeginEdit()
            grPagosAnticipados.CurrentRow.Cells.Item("abmon").Value = total
            grPagosAnticipados.GetRow(grPagosAnticipados.Row).EndEdit()
            grPagosAnticipados.Refresh()

            total = grPagosAnticipados.GetTotal(grPagosAnticipados.RootTable.Columns("abmon"), AggregateFunction.Sum)
            tbTotalAnticipo.Value = total
        End If

    End Sub
    Private Sub tbTotal_ValueChanged(sender As Object, e As EventArgs) Handles tbTotalProd.ValueChanged, tbTotal.ValueChanged
        'tbDescuadre.Value = tbTotalSumado.Value - tbTotal.Value
        'If tbDescuadre.Value < 0 Then
        '    tbDescuadre.BackgroundStyle.BackColor = Color.Red
        'Else
        '    tbDescuadre.BackgroundStyle.BackColor = Color.White
        'End If
        tbTotalVentas.Value = tbTotal.Value + tbTotalProd.Value
    End Sub

    Private Sub grDetalle1_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle1.CellEdited
        Dim estado As Integer = grDetalle1.GetValue("estado")
        If estado = 1 Then
            grDetalle1.SetValue("estado", 2)

        End If

    End Sub

    Private Sub grDetalle3Anticipo_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grPagosAnticipados.CellEdited
        Dim estado As Integer = grPagosAnticipados.GetValue("estado")
        If estado = 1 Then
            grPagosAnticipados.SetValue("estado", 2)

        End If

    End Sub

    Private Sub grTarjeta_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grTarjetas.CellEdited
        Dim estado As Integer = grTarjetas.GetValue("estado")
        If estado = 1 Then
            grTarjetas.SetValue("estado", 2)

        End If

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        _prImprimir()
    End Sub

    Private Sub tbVendedor_ValueChanged(sender As Object, e As EventArgs) Handles tbVendedor.ValueChanged

    End Sub

    Private Sub tbMaquina_ValueChanged(sender As Object, e As EventArgs) Handles tbMaquina.ValueChanged
        'If tbMaquina.SelectedIndex >= 0 Then 'And btnGrabar.Enabled = True
        '    btnNuevoTipoCambio.Visible = True

        '    Dim dt As DataTable = L_prMaquinaDetalleGeneral(tbMaquina.Value)
        '    If dt.Rows.Count <= 2 Then
        '        gpMangueras34.Visible = False
        '        'cargar datos manguera 1
        '        tbMan1.Text = dt.Rows(0).Item("cndesc1")
        '        tbMan1.Tag = dt.Rows(0).Item("afnumi")
        '        tbCombustible1.Text = dt.Rows(0).Item("agdesc")
        '        tbPrecio1.Value = dt.Rows(0).Item("agprecio")
        '        tbIni1.Value = dt.Rows(0).Item("afmit")

        '        'cargar datos manguera 2
        '        tbMan2.Text = dt.Rows(1).Item("cndesc1")
        '        tbMan2.Tag = dt.Rows(1).Item("afnumi")
        '        tbCombustible2.Text = dt.Rows(1).Item("agdesc")
        '        tbPrecio2.Value = dt.Rows(1).Item("agprecio")
        '        tbIni2.Value = dt.Rows(1).Item("afmit")

        '        tbMan3.Tag = 0
        '        tbMan4.Tag = 0
        '    Else
        '        gpMangueras34.Visible = True
        '        'cargar datos manguera 1
        '        tbMan1.Text = dt.Rows(0).Item("cndesc1")
        '        tbMan1.Tag = dt.Rows(0).Item("afnumi")
        '        tbCombustible1.Text = dt.Rows(0).Item("agdesc")
        '        tbPrecio1.Value = dt.Rows(0).Item("agprecio")
        '        tbIni1.Value = dt.Rows(0).Item("afmit")

        '        'cargar datos manguera 2
        '        tbMan2.Text = dt.Rows(1).Item("cndesc1")
        '        tbMan2.Tag = dt.Rows(1).Item("afnumi")
        '        tbCombustible2.Text = dt.Rows(1).Item("agdesc")
        '        tbPrecio2.Value = dt.Rows(1).Item("agprecio")
        '        tbIni2.Value = dt.Rows(1).Item("afmit")

        '        'cargar datos manguera 3
        '        tbMan3.Text = dt.Rows(2).Item("cndesc1")
        '        tbMan3.Tag = dt.Rows(2).Item("afnumi")
        '        tbCombustible3.Text = dt.Rows(2).Item("agdesc")
        '        tbPrecio3.Value = dt.Rows(2).Item("agprecio")
        '        tbIni3.Value = dt.Rows(2).Item("afmit")

        '        'cargar datos manguera 4
        '        tbMan4.Text = dt.Rows(3).Item("cndesc1")
        '        tbMan4.Tag = dt.Rows(3).Item("afnumi")
        '        tbCombustible4.Text = dt.Rows(3).Item("agdesc")
        '        tbPrecio4.Value = dt.Rows(3).Item("agprecio")
        '        tbIni4.Value = dt.Rows(3).Item("afmit")
        '    End If

        'Else
        '    btnNuevoTipoCambio.Visible = False
        'End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        _prEliminarFilaDetalle3Anticipo()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        _prEliminarFilaDetalle3()

    End Sub

    Private Sub grTarjetas_KeyDown(sender As Object, e As KeyEventArgs) Handles grTarjetas.KeyDown
        'If (e.KeyCode = Keys.Enter) Then
        '    e.Handled = True
        '    SendKeys.Send("{TAB}")
        '    'If grTarjetas.Col = grTarjetas.RootTable.Columns.Count - 1 Then
        '    '    grTarjetas.MoveTo(0) 'JGr_Detalle.NewRowPosition
        '    '    grTarjetas.Col = -1
        '    'End If

        'End If
    End Sub

    Private Sub grTarjetas_CurrentCellChanged(sender As Object, e As EventArgs) Handles grTarjetas.CurrentCellChanged

    End Sub

    Private Sub grDetalle1_FirstChange(sender As Object, e As RowActionEventArgs) Handles grDetalle1.FirstChange
        'grTarjetas.MoveTo(0) 'JGr_Detalle.NewRowPosition
        'grTarjetas.Col = -1
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        If ButtonX2.Tag = 0 Then
            grPanelAyudaExcel.Visible = True
            '134; 677
            grPanelAyudaExcel.Size = New Size(113, 677)
            '128; 588
            'grAyuda.Size = New Size(128, 588)
            'grAyuda.Dock = DockStyle.Fill
            CType(grAyuda.DataSource, DataTable).Rows.Clear()
            'tbTotalAyuda.Value = 0
            ButtonX2.Tag = 1
            ButtonX2.Text = "OCULTAR AYUDA"
            ButtonX1.Visible = True
        Else
            grPanelAyudaExcel.Visible = False
            ButtonX2.Text = "AYUDA SUMA"
            ButtonX1.Visible = False
            ButtonX2.Tag = 0

        End If
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        CType(grAyuda.DataSource, DataTable).Rows.Clear()
        'tbTotalAyuda.Value = 0

    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs)
        'tbTotalTarjeta.Value = tbTotalAyuda.Value
    End Sub

    Private Sub grDetalle1_SelectionChanged(sender As Object, e As EventArgs) Handles grDetalle1.SelectionChanged
        If grDetalle1.Row >= 0 And _dtsDesgloseCredito.Tables.Count > 0 Then
            _prCargarGridDesgloseCredito(grDetalle1.Row)
            grPanelAyudaExcel.Text = grDetalle1.GetValue("adnom")
            grAyuda.Focus()
            grAyuda.Col = 0

        Else
            grAyuda.DataSource = Nothing
            grPanelAyudaExcel.Text = ""
        End If
    End Sub

    Private Sub SuperTabControl2_SelectedTabChanged(sender As Object, e As SuperTabStripSelectedTabChangedEventArgs) Handles SuperTabControl2.SelectedTabChanged
        If SuperTabControl2.SelectedTabIndex = 0 Or SuperTabControl2.SelectedTabIndex = 2 Then
            grPanelAyudaExcel.Visible = True
            '134; 677
            grPanelAyudaExcel.Size = New Size(113, 677)
            '128; 588
            'grAyuda.Size = New Size(128, 588)
            'grAyuda.Dock = DockStyle.Fill

        Else
            grPanelAyudaExcel.Visible = False
        End If

        grAyuda.DataSource = Nothing
    End Sub

    Private Sub grProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles grProductos.KeyDown
        Dim f As Integer = grProductos.Row
        Dim c As Integer = grProductos.Col
        If e.KeyData = Keys.Control + Keys.Enter And c >= 0 And btnGrabar.Enabled = True Then
            If grProductos.RootTable.Columns(c).Key = "ainumitc8" Or grProductos.RootTable.Columns(c).Key = "cicdprod1" Then

                Dim frmAyuda As Modelos.ModeloAyuda
                Dim dt As DataTable

                dt = L_prArqueoObtenerProductosGeneral()

                Dim listEstCeldas As New List(Of Modelos.Celda)
                listEstCeldas.Add(New Modelos.Celda("cinumi", True, "id".ToUpper, 100))
                listEstCeldas.Add(New Modelos.Celda("cicbarra", True, "codigo".ToUpper, 100))
                listEstCeldas.Add(New Modelos.Celda("cicdprod1", True, "producto".ToUpper, 200))
                listEstCeldas.Add(New Modelos.Celda("ciprecio", True, "precio".ToUpper, 150, "0.00"))


                frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione producto".ToUpper, listEstCeldas)


                frmAyuda.ShowDialog()

                If frmAyuda.seleccionado = True Then
                    Dim numi As String = frmAyuda.filaSelect.Cells("cinumi").Value
                    Dim desc As String = frmAyuda.filaSelect.Cells("cicdprod1").Value
                    Dim precio As Double = IIf(IsDBNull(frmAyuda.filaSelect.Cells("ciprecio").Value) = True, 0, frmAyuda.filaSelect.Cells("ciprecio").Value)

                    grProductos.SetValue("ainumitc8", numi)
                    grProductos.SetValue("cicdprod1", desc)
                    grProductos.SetValue("aiprecio", precio)
                    grProductos.SetValue("aicant", 0)

                End If
            End If
        End If
    End Sub

    Private Sub grProductos_UpdatingCell(sender As Object, e As UpdatingCellEventArgs) Handles grProductos.UpdatingCell
        If e.Column.Index = grProductos.RootTable.Columns("aicant").Index Then
            Dim cantidad, precio As Integer
            cantidad = e.Value
            precio = grProductos.CurrentRow.Cells("aiprecio").Value
            grProductos.CurrentRow.Cells("aitot").Value = cantidad * precio
        End If
    End Sub

    Private Sub grProductos_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grProductos.CellEdited
        Dim estado As Integer = grProductos.GetValue("estado")
        If estado = 1 Then
            grProductos.SetValue("estado", 2)

        End If
    End Sub
    Private Sub grDetalle2_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle2.CellEdited
        Dim estado As Integer = grDetalle2.GetValue("estado")
        If estado = 1 Then
            grDetalle2.SetValue("estado", 2)

        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        _prEliminarFilaDetalle4()
    End Sub

    Private Sub grProductos_RecordAdded(sender As Object, e As EventArgs) Handles grProductos.RecordAdded
        Dim total As Double = grProductos.GetTotal(grProductos.RootTable.Columns("aitot"), AggregateFunction.Sum)
        tbTotalProd.Value = total
    End Sub

    Private Sub grProductos_RecordUpdated(sender As Object, e As EventArgs) Handles grProductos.RecordUpdated
        Dim total As Double = grProductos.GetTotal(grProductos.RootTable.Columns("aitot"), AggregateFunction.Sum)
        tbTotalProd.Value = total
    End Sub

    Private Sub tbTotalVentas_ValueChanged(sender As Object, e As EventArgs) Handles tbTotalVentas.ValueChanged, tbTotalSumado.ValueChanged
        tbDescuadre.Value = tbTotalSumado.Value - tbTotalVentas.Value
        If tbDescuadre.Value < 0 Then
            tbDescuadre.BackgroundStyle.BackColor = Color.Red
        Else
            tbDescuadre.BackgroundStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub grPagosAnticipados_SelectionChanged(sender As Object, e As EventArgs) Handles grPagosAnticipados.SelectionChanged
        If grPagosAnticipados.Row >= 0 And _dtsDesgloseAnticipo.Tables.Count > 0 Then
            _prCargarGridDesgloseAnticipo(grPagosAnticipados.Row)
            grPanelAyudaExcel.Text = grPagosAnticipados.GetValue("adnom")

            grAyuda.Focus()
            grAyuda.Col = 0
        Else
            grAyuda.DataSource = Nothing
            grPanelAyudaExcel.Text = ""
        End If
    End Sub



    Private Sub tbCali1_ValueChanged(sender As Object, e As EventArgs) Handles tbCali1.ValueChanged
        tbMitTotal1.Value = tbFin1.Value - tbIni1.Value - tbCali1.Value
    End Sub

    Private Sub tbCali2_ValueChanged(sender As Object, e As EventArgs) Handles tbCali2.ValueChanged
        tbMitTotal2.Value = tbFin2.Value - tbIni2.Value - tbCali2.Value

    End Sub
    Private Sub tbCali3_ValueChanged(sender As Object, e As EventArgs) Handles tbCali3.ValueChanged
        tbMitTotal3.Value = tbFin3.Value - tbIni3.Value - tbCali3.Value

    End Sub

    Private Sub tbCali4_ValueChanged(sender As Object, e As EventArgs) Handles tbCali4.ValueChanged
        tbMitTotal4.Value = tbFin4.Value - tbIni4.Value - tbCali4.Value

    End Sub

    Private Sub grDetalle1_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grDetalle1.FormattingRow

    End Sub

    Private Sub F0_Arqueo_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If btnGrabar.Enabled = True Then
            If e.KeyData = Keys.F1 Then
                SuperTabControl2.SelectedTabIndex = 0
                grDetalle1.Focus()
                grDetalle1.Col = 0
                grDetalle1.Row = -1
            End If

            If e.KeyData = Keys.F2 Then
                SuperTabControl2.SelectedTabIndex = 1
                grDetalle2.Focus()

            End If

            If e.KeyData = Keys.F3 Then
                SuperTabControl2.SelectedTabIndex = 2
                grPagosAnticipados.Focus()
                grPagosAnticipados.Col = 0
                grPagosAnticipados.Row = -1

            End If

            If e.KeyData = Keys.F4 Then
                SuperTabControl2.SelectedTabIndex = 3
                grTarjetas.Focus()
                grTarjetas.Row = -1

            End If

            If e.KeyData = Keys.F5 Then
                SuperTabControl2.SelectedTabIndex = 4
                grProductos.Focus()
                grProductos.Row = -1

            End If
        End If

    End Sub

    Private Sub grAyuda_KeyDown(sender As Object, e As KeyEventArgs) Handles grAyuda.KeyDown
        If SuperTabControl2.SelectedTabIndex = 0 Then
            If e.KeyData = Keys.Control + Keys.Up And btnGrabar.Enabled = True Then
                grDetalle1.MovePrevious()
            End If
            If e.KeyData = Keys.Control + Keys.Down And btnGrabar.Enabled = True Then
                grDetalle1.MoveNext()
            End If
        End If

        If SuperTabControl2.SelectedTabIndex = 2 Then
            If e.KeyData = Keys.Control + Keys.Up And btnGrabar.Enabled = True Then
                grPagosAnticipados.MovePrevious()
            End If
            If e.KeyData = Keys.Control + Keys.Down And btnGrabar.Enabled = True Then
                grPagosAnticipados.MoveNext()
            End If
        End If


    End Sub

    Private Sub btnNuevoTipoCambio_Click(sender As Object, e As EventArgs) Handles btnNuevoTipoCambio.Click
        If tbMaquina.SelectedIndex >= 0 Then
            Dim dt As DataTable = L_prArqueoObtenerDatosMaquina(tbMaquina.Value)
            Dim dtDetalle As DataTable = CType(grMangueras.DataSource, DataTable)

            Dim i As Integer = 0
            While i <= dt.Rows.Count - 1
                Dim filaFiltradas As DataRow() = dtDetalle.Select("ajman=" + dt.Rows(i).Item("ajman").ToString)
                If filaFiltradas.Count > 0 Then
                    dt.Rows(i).Delete()
                End If
                i = i + 1

            End While

            dtDetalle.Merge(dt)

        End If
    End Sub

    Private Sub grMangueras_RecordUpdated(sender As Object, e As EventArgs) Handles grMangueras.RecordUpdated

        If grMangueras.Col = grMangueras.RootTable.Columns("ajmitfin").Index Or grMangueras.Col = grMangueras.RootTable.Columns("ajmitcali").Index Then
            Dim mitIni As Double = grMangueras.GetValue("ajmitini")
            Dim mitFin As Double = grMangueras.GetValue("ajmitfin")
            Dim mitCalibracion As Double = grMangueras.GetValue("ajmitcali")

            Dim mitTotal As Double = mitFin - mitIni - mitCalibracion
            Dim precio As Double = grMangueras.GetValue("agprecio")

            grMangueras.GetRow(grMangueras.Row).BeginEdit()
            grMangueras.CurrentRow.Cells.Item("mitTotal").Value = mitTotal
            grMangueras.CurrentRow.Cells.Item("ajtotal").Value = mitTotal * precio

            grMangueras.GetRow(grMangueras.Row).EndEdit()
            grMangueras.Refresh()

            Dim total As Double = grMangueras.GetTotal(grMangueras.RootTable.Columns("ajtotal"), AggregateFunction.Sum)
            tbTotal.Value = total
        End If



    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        _prEliminarFilaDetalleMangueras()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        _prEliminarFilaDetalleDesglose()
    End Sub

    Private Sub tbDescuadre_ValueChanged(sender As Object, e As EventArgs) Handles tbDescuadre.ValueChanged
        If btnGrabar.Enabled = False Then
            If tbDescuadre.Value <= 1 And tbDescuadre.Value >= -1 Then
                Dim descuadre As Double = tbDescuadre.Value
                If Convert.ToDouble(tbDescuadre.Text) <= -0.01 And Convert.ToDouble(tbDescuadre.Text) >= -0.01 Then
                    'tbTotalVentas.Value = tbTotalSumado.Value
                    tbTotal.Value = tbTotal.Value - 0.01
                End If

                If Convert.ToDouble(tbDescuadre.Text) <= 0.01 And Convert.ToDouble(tbDescuadre.Text) >= 0.01 Then
                    'tbTotalVentas.Value = tbTotalSumado.Value
                    tbTotal.Value = tbTotal.Value + 0.01
                End If
            End If
        End If
    End Sub

    Private Sub BubbleBarUsuario_ButtonClick(sender As Object, e As ClickEventArgs) Handles BubbleBarUsuario.ButtonClick

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

    End Sub

    Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening

    End Sub

    Private Sub ContextMenuStrip4_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip4.Opening

    End Sub
End Class