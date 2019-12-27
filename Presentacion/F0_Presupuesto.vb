Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Modelos
Imports DevComponents.DotNetBar.Controls
Imports System.Math
Imports System.IO

Public Class F0_Presupuesto


#Region "VARIABLES LOCALES"
    Public _MPos As Integer
    Public _MNuevo As Boolean
    Public _MModificar As Boolean


    Public _MListEstBuscador As List(Of Celda)

    Public _MTipoInserccionNuevo As Boolean = True

    Private _numiAuxiliarDetalleModulo As Integer = 1
    Private _numiAuxiliarDetalleSucursal As Integer = 11
#End Region

#Region "METODOS PRIVADOS MODELO"

    Public Sub _PMIniciarTodo()

        Me.Text = "p r e s u p u e s t o s".ToUpper
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

        AddHandler JGrM_Buscador.SelectionChanged, AddressOf JGrM_Buscador_SelectionChanged


        btnModificar.Visible = False
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
            '_PMOLimpiar()
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

#Region "EVENTOS DEL MODELO"
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

        _PMNuevo()

        'Dim dtBuscador As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        'If dtBuscador.Rows.Count > 0 Then
        '    Dim info As New TaskDialogInfo("recuperacion".ToUpper, eTaskDialogIcon.Information, "¿desea recuperar el ultimo presupuesto grabado?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.No, eTaskDialogBackgroundColor.Blue)
        '    Dim result As eTaskDialogResult = TaskDialog.Show(info)
        '    If result = eTaskDialogResult.Yes Then
        '        Dim filasFiltradas As DataRow() = dtBuscador.Select("1=1", "chanio desc")
        '        Dim gestion As Integer = filasFiltradas(0).Item("chanio")
        '        _prCargarGridPresupuestoGrabadoIngreso(gestion)
        '        _prCargarGridPresupuestoGrabadoEgreso(gestion)
        '        _prCargarGridPresupuestoResultado()
        '        _prCalcularResultado()
        '    End If

        'End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

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


#End Region


#Region "ATRIBUTOS"
    Public _nameButton As String
    Public _tab As SuperTabItem

    Public _modulo As SideNavItem

    Private _tipoIngreso As Integer = 1
    Private _tipoEgreso As Integer = 2
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        Me.Text = "c o m p r o b a n t e s".ToUpper


        _PMIniciarTodo()

        _prCargarComboAuxiliaresSucursales(_numiAuxiliarDetalleSucursal)
        _prCargarComboAuxiliaresModulos(_numiAuxiliarDetalleModulo)

        _prAsignarPermisos()
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

    Private Sub _prSetearColumnas(grid As GridEX)
        With grid.RootTable.Columns("ednumi")
            .Width = 50
            .Visible = False
        End With

        With grid.RootTable.Columns("eddesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "DESCRIPCION"
            .Width = 300
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grid.RootTable.Columns("ene")
            .Caption = "ENERO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("eneacu")
            .Caption = "ACU. ENE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("feb")
            .Caption = "FEBRERO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("febacu")
            .Caption = "ACU. FEB"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("mar")
            .Caption = "MARZO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("maracu")
            .Caption = "ACU. MAR"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("abr")
            .Caption = "ABRIL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("abracu")
            .Caption = "ACU. ABR"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("may")
            .Caption = "MAYO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("mayacu")
            .Caption = "ACU. MAY"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("jun")
            .Caption = "JUNIO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("junacu")
            .Caption = "ACU. JUN"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("jul")
            .Caption = "JULIO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
        End With
        With grid.RootTable.Columns("julacu")
            .Caption = "ACU. JUL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("ago")
            .Caption = "AGOSTO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("agoacu")
            .Caption = "ACU. AGO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("sep")
            .Caption = "SEPTIEMBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("sepacu")
            .Caption = "ACU. SEP"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("oct")
            .Caption = "OCTUBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("octacu")
            .Caption = "ACU. OCT"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("nov")
            .Caption = "NOVIEMBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("novacu")
            .Caption = "ACU. NOV"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("dic")
            .Caption = "DICIEMBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grid.RootTable.Columns("dicacu")
            .Caption = "ACU. DIC"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid.RootTable.Columns("total")
            .Caption = "TOTAL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightYellow
            .EditType = EditType.NoEdit
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With

        With grid
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
    End Sub

    'Private Sub _prCargarGridPresupuestoNuevoIngreso()
    '    Dim dt As New DataTable
    '    'empiezo a cargar la gestion
    '    dt = L_prPresupuestoObtenenerServiciosPorTipo(-1, _tipoIngreso)
    '    Dim dtSectores As DataTable = L_prlistarCategoriasActivos()

    '    'hago un for a todos los tipos que existen
    '    For Each fila As DataRow In dtSectores.Rows
    '        dt.Rows.Add(0, "INGRESOS UNIDAD DE " + fila("cedesc1"))
    '        Dim dtTemp As New DataTable
    '        If fila("cenum") = 3 Then 'entonces son los servicios de lavadero
    '            dtTemp = L_prPresupuestoObtenenerServiciosLavadero()
    '            Dim dtServLav As DataTable = L_prPresupuestoObtenenerServiciosPorTipo(fila("cenum"), _tipoIngreso)
    '            dtTemp.Merge(dtServLav)
    '        Else
    '            dtTemp = L_prPresupuestoObtenenerServiciosPorTipo(fila("cenum"), _tipoIngreso)

    '        End If
    '        dt.Merge(dtTemp)

    '    Next


    '    grDetalleIngreso.DataSource = dt
    '    grDetalleIngreso.RetrieveStructure()

    '    'dar formato a las columnas
    '    _prSetearColumnas(grDetalleIngreso)



    '    Dim fc As GridEXFormatCondition
    '    fc = New GridEXFormatCondition(grDetalleIngreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, 0)
    '    fc.FormatStyle.BackColor = Color.LightBlue
    '    fc.FormatStyle.FontBold = TriState.True
    '    grDetalleIngreso.RootTable.FormatConditions.Add(fc)


    'End Sub

    Private Sub _prCargarGridPresupuestoNuevoIngreso()
        Dim dt As New DataTable
        'empiezo a cargar la gestion
        dt = L_prPresupuestoObtenenerServiciosPorTipo(-1, _tipoIngreso)
        Dim dtCuentasIngreso As DataTable = L_prPresupuestoObtenenerCuentasIngresoPorEmpresaConMayores(gi_empresaNumi, tbModulo.Value, tbSucursal.Value)

        'Dim dtMayorMayorMayor As DataTable = dtCuentasIngreso.DefaultView.ToTable(True, "canumi4", "cacta4", "cadesc4", "capadre4")
        Dim dtMayorMayor As DataTable = dtCuentasIngreso.DefaultView.ToTable(True, "canumi3", "cacta3", "cadesc3", "capadre3")
        Dim dtMayor As DataTable = dtCuentasIngreso.DefaultView.ToTable(True, "canumi2", "cacta2", "cadesc2", "capadre2")

        'hago un for a todos los tipos que existen
        dt.Rows.Add(0, "INGRESOS")
        ' Dim filasMayorMayor As DataRow() = dtMayorMayor.Select("capadre3=" + filaMayorMayorMayor("canumi4").ToString, "cacta3 asc")

        For Each filaMayorMayor As DataRow In dtMayorMayor.Rows
            dt.Rows.Add(-1, "  " + filaMayorMayor("cadesc3"))
            Dim filasMayor As DataRow() = dtMayor.Select("capadre2=" + filaMayorMayor("canumi3").ToString, "cacta2 asc")

            For Each filaMayor As DataRow In filasMayor
                dt.Rows.Add(-2, "    " + filaMayor("cadesc2"))
                Dim filasCuentas As DataRow() = dtCuentasIngreso.Select("capadre1=" + filaMayor.Item("canumi2").ToString, "cacta1 asc")
                For Each filaCuenta As DataRow In filasCuentas
                    _prInsertarFila(dt, filaCuenta("canumi1"), filaCuenta("cadesc1"), _tipoIngreso)
                Next
            Next
        Next

        grDetalleIngreso.DataSource = dt
        grDetalleIngreso.RetrieveStructure()

        'dar formato a las columnas
        _prSetearColumnas(grDetalleIngreso)

        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grDetalleIngreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.LightSteelBlue
        fc.FormatStyle.FontBold = TriState.True
        grDetalleIngreso.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDetalleIngreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, -1)
        fc.FormatStyle.BackColor = Color.LightGray
        fc.FormatStyle.FontBold = TriState.True
        grDetalleIngreso.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDetalleIngreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, -2)
        fc.FormatStyle.BackColor = Color.LightBlue
        fc.FormatStyle.FontBold = TriState.True
        grDetalleIngreso.RootTable.FormatConditions.Add(fc)

        grDetalleIngreso.Refresh()


    End Sub

    Private Sub _prInsertarFila(ByRef dt As DataTable, numiCuenta As Integer, cuenta As String, tipo As Integer)
        dt.Rows.Add(numiCuenta, "       " + cuenta,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0,'total 
                    tipo) 'tipo

    End Sub

    'Private Sub _prCargarGridPresupuestoNuevoEgreso()
    '    Dim dt As New DataTable
    '    'empiezo a cargar la gestion
    '    dt = L_prPresupuestoObtenenerCuentasPorPadre(-1, _tipoEgreso)
    '    Dim dtSectores As DataTable = L_prPresupuestoObtenenerCuentasEgresoPadres(gi_empresaNumi)

    '    'hago un for a todos los tipos que existen
    '    For Each fila As DataRow In dtSectores.Rows
    '        dt.Rows.Add(0, fila("cadesc"))
    '        Dim dtTemp As New DataTable
    '        dtTemp = L_prPresupuestoObtenenerCuentasPorPadre(fila("canumi"), _tipoEgreso)
    '        dt.Merge(dtTemp)
    '    Next


    '    grDetalleEgreso.DataSource = dt
    '    grDetalleEgreso.RetrieveStructure()

    '    'dar formato a las columnas
    '    _prSetearColumnas(grDetalleEgreso)

    '    Dim fc As GridEXFormatCondition
    '    fc = New GridEXFormatCondition(grDetalleEgreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, 0)
    '    fc.FormatStyle.BackColor = Color.LightBlue
    '    fc.FormatStyle.FontBold = TriState.True
    '    grDetalleEgreso.RootTable.FormatConditions.Add(fc)


    'End Sub

    Private Sub _prCargarGridPresupuestoNuevoEgreso()
        Dim dt As New DataTable
        'empiezo a cargar la gestion
        dt = L_prPresupuestoObtenenerServiciosPorTipo(-1, _tipoIngreso)
        Dim dtCuentasIngreso As DataTable = L_prPresupuestoObtenenerCuentasEgresoPorEmpresaConMayores(gi_empresaNumi, tbModulo.Value, tbSucursal.Value)

        'Dim dtMayorMayorMayor As DataTable = dtCuentasIngreso.DefaultView.ToTable(True, "canumi4", "cacta4", "cadesc4", "capadre4")
        Dim dtMayorMayor As DataTable = dtCuentasIngreso.DefaultView.ToTable(True, "canumi3", "cacta3", "cadesc3", "capadre3")
        Dim dtMayor As DataTable = dtCuentasIngreso.DefaultView.ToTable(True, "canumi2", "cacta2", "cadesc2", "capadre2")

        'hago un for a todos los tipos que existen
        dt.Rows.Add(0, "EGRESOS")
        ' Dim filasMayorMayor As DataRow() = dtMayorMayor.Select("capadre3=" + filaMayorMayorMayor("canumi4").ToString, "cacta3 asc")

        For Each filaMayorMayor As DataRow In dtMayorMayor.Rows
            dt.Rows.Add(-1, "  " + filaMayorMayor("cadesc3"))
            Dim filasMayor As DataRow() = dtMayor.Select("capadre2=" + filaMayorMayor("canumi3").ToString, "cacta2 asc")

            For Each filaMayor As DataRow In filasMayor
                dt.Rows.Add(-2, "    " + filaMayor("cadesc2"))
                Dim filasCuentas As DataRow() = dtCuentasIngreso.Select("capadre1=" + filaMayor.Item("canumi2").ToString, "cacta1 asc")
                For Each filaCuenta As DataRow In filasCuentas
                    _prInsertarFila(dt, filaCuenta("canumi1"), filaCuenta("cadesc1"), _tipoEgreso)
                Next
            Next
        Next

        grDetalleEgreso.DataSource = dt
        grDetalleEgreso.RetrieveStructure()

        'dar formato a las columnas
        _prSetearColumnas(grDetalleEgreso)

        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grDetalleEgreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.LightSteelBlue
        fc.FormatStyle.FontBold = TriState.True
        grDetalleEgreso.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDetalleEgreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, -1)
        fc.FormatStyle.BackColor = Color.LightGray
        fc.FormatStyle.FontBold = TriState.True
        grDetalleEgreso.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDetalleEgreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, -2)
        fc.FormatStyle.BackColor = Color.LightBlue
        fc.FormatStyle.FontBold = TriState.True
        grDetalleEgreso.RootTable.FormatConditions.Add(fc)

        grDetalleEgreso.Refresh()


    End Sub


    Private Sub _prCargarGridPresupuestoGrabadoIngreso(gestion As Integer)
        _prCargarGridPresupuestoNuevoIngreso()
        Dim dt As DataTable = CType(grDetalleIngreso.DataSource, DataTable)

        'ahora a empezar a cargar datos

        For Each fila As DataRow In dt.Rows
            Dim numiServ As Integer = fila.Item("ednumi")
            If numiServ > 0 Then
                Dim j As Integer = 0
                Dim montoAcu As Double = 0
                Dim montosServicio As DataTable = L_prPresupuestoObtenerValoresPorServicio(gestion, numiServ, tbModulo.Value, tbSucursal.Value, gi_empresaNumi)
                For i As Integer = 2 To dt.Columns.Count - 3 Step 2
                    Dim montoMes As Double = montosServicio.Rows(j).Item("chmonto")
                    fila.Item(i) = montoMes
                    montoAcu = montoAcu + montoMes
                    fila.Item(i + 1) = montoAcu
                    j = j + 1
                Next
                fila.Item("total") = montoAcu

            End If
        Next

        '----------


        'grDetalleIngreso.DataSource = dt
        'grDetalleIngreso.RetrieveStructure()

        ''dar formato a las columnas
        '_prSetearColumnas(grDetalleIngreso)



        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalleIngreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightBlue
        'fc.FormatStyle.FontBold = TriState.True
        'grDetalleIngreso.RootTable.FormatConditions.Add(fc)


    End Sub

    Private Sub _prCargarGridPresupuestoGrabadoEgreso(gestion As Integer)
        _prCargarGridPresupuestoNuevoEgreso()
        Dim dt As DataTable = CType(grDetalleEgreso.DataSource, DataTable)

        'ahora a empezar a cargar datos

        For Each fila As DataRow In dt.Rows
            Dim numiCuenta As Integer = fila.Item("ednumi")
            If numiCuenta > 0 Then
                Dim j As Integer = 0
                Dim montoAcu As Double = 0
                Dim montosCuenta As DataTable = L_prPresupuestoObtenerValoresPorServicio(gestion, numiCuenta, tbModulo.Value, tbSucursal.Value, gi_empresaNumi)
                For i As Integer = 2 To dt.Columns.Count - 3 Step 2
                    Dim montoMes As Double = montosCuenta.Rows(j).Item("chmonto")
                    fila.Item(i) = montoMes
                    montoAcu = montoAcu + montoMes
                    fila.Item(i + 1) = montoAcu
                    j = j + 1
                Next
                fila.Item("total") = montoAcu

            End If
        Next

        '----------


        'grDetalleEgreso.DataSource = dt
        'grDetalleEgreso.RetrieveStructure()

        ''dar formato a las columnas
        '_prSetearColumnas(grDetalleEgreso)

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalleEgreso.RootTable.Columns("ednumi"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightBlue
        'fc.FormatStyle.FontBold = TriState.True
        'grDetalleEgreso.RootTable.FormatConditions.Add(fc)


    End Sub

    Private Sub _prCargarGridPresupuestoResultado()
        grResultado.Height = 40

        Dim dt As New DataTable
        'empiezo a cargar la gestion
        dt = L_prPresupuestoObtenenerCuentasPorPadre(-1, _tipoEgreso)

        dt.Rows.Add(0, "RESULTADOS DE LA GESTION:")

        grResultado.DataSource = dt
        grResultado.RetrieveStructure()

        'dar formato a las columnas
        With grResultado.RootTable.Columns("ednumi")
            .Width = 50
            .Visible = False
        End With

        With grResultado.RootTable.Columns("eddesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "INGRESOS"
            .Width = 300
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grResultado.RootTable.Columns("ene")
            .Caption = "ENERO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("eneacu")
            .Caption = "ACU. ENE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("feb")
            .Caption = "FEBRERO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("febacu")
            .Caption = "ACU. FEB"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("mar")
            .Caption = "MARZO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("maracu")
            .Caption = "ACU. MAR"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("abr")
            .Caption = "ABRIL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("abracu")
            .Caption = "ACU. ABR"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("may")
            .Caption = "MAYO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("mayacu")
            .Caption = "ACU. MAY"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("jun")
            .Caption = "JUNIO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("junacu")
            .Caption = "ACU. JUN"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("jul")
            .Caption = "JULIO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen
        End With
        With grResultado.RootTable.Columns("julacu")
            .Caption = "ACU. JUL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("ago")
            .Caption = "AGOSTO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("agoacu")
            .Caption = "ACU. AGO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("sep")
            .Caption = "SEPTIEMBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("sepacu")
            .Caption = "ACU. SEP"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("oct")
            .Caption = "OCTUBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("octacu")
            .Caption = "ACU. OCT"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("nov")
            .Caption = "NOVIEMBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("novacu")
            .Caption = "ACU. NOV"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("dic")
            .Caption = "DICIEMBRE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightGreen


        End With
        With grResultado.RootTable.Columns("dicacu")
            .Caption = "ACU. DIC"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .EditType = EditType.NoEdit


        End With

        With grResultado.RootTable.Columns("total")
            .Caption = "TOTAL"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .FormatString = "0.00"
            .AllowSort = False
            .CellStyle.BackColor = Color.LightYellow
            .EditType = EditType.NoEdit


        End With

        With grResultado
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

        End With

        'tratando de ocultar las cabeceras
        grResultado.ColumnHeaders = InheritableBoolean.False

    End Sub

    Private Function _prObtenerDetalle() As DataTable
        'cargar ingresos
        Dim dtDetalle As DataTable = CType(grDetalleIngreso.DataSource, DataTable)
        Dim dtFinal As DataTable = L_prPresupuestoGeneralPorAnio(-1)
        'chnumi,chnumitc4,chmes,chanio,chmonto,chfact,chhact,chuact,1 as estado
        For Each fila As DataRow In dtDetalle.Rows
            Dim numiServ As Integer = fila.Item("ednumi")
            If numiServ > 0 Then
                Dim j As Integer = 1
                For i = 2 To 24 Step 2
                    dtFinal.Rows.Add(0, numiServ, j, tbGestion.Value, fila.Item(i), _tipoIngreso, gi_empresaNumi, tbModulo.Value, tbSucursal.Value, DBNull.Value, DBNull.Value, DBNull.Value, 1)
                    j = j + 1
                Next
            End If
        Next

        'cargar egresos
        dtDetalle = CType(grDetalleEgreso.DataSource, DataTable)
        'chnumi,chnumitc4,chmes,chanio,chmonto,chfact,chhact,chuact,1 as estado
        For Each fila As DataRow In dtDetalle.Rows
            Dim numiServ As Integer = fila.Item("ednumi")
            If numiServ > 0 Then
                Dim j As Integer = 1
                For i = 2 To 24 Step 2
                    dtFinal.Rows.Add(0, numiServ, j, tbGestion.Value, fila.Item(i), _tipoEgreso, gi_empresaNumi, tbModulo.Value, tbSucursal.Value, DBNull.Value, DBNull.Value, DBNull.Value, 1)
                    j = j + 1
                Next
            End If
        Next
        Return dtFinal
    End Function

    Private Sub _prCalcularResultado()
        Dim dtResultado As DataTable = CType(grResultado.DataSource, DataTable)
        For i As Integer = 2 To grDetalleEgreso.RootTable.Columns.Count - 2 Step 2
            Dim totIngreso As Double = grDetalleIngreso.GetTotal(grDetalleIngreso.RootTable.Columns(i), AggregateFunction.Sum)
            Dim totEgreso As Double = grDetalleEgreso.GetTotal(grDetalleEgreso.RootTable.Columns(i), AggregateFunction.Sum)
            dtResultado.Rows(0).Item(i) = totIngreso - totEgreso

        Next

    End Sub
#End Region

#Region "METODOS PARA LLENAR"

    Public Sub _PMOHabilitar()

        'tbAnio.ReadOnly = False
        'tbMes.ReadOnly = False
        tbGestion.IsInputReadOnly = False
        PanelInferior.Visible = False
        tbModulo.ReadOnly = False
        tbSucursal.ReadOnly = False

    End Sub

    Public Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbGestion.IsInputReadOnly = True
        PanelInferior.Visible = True

        tbModulo.ReadOnly = True
        tbSucursal.ReadOnly = True
        tbModulo.SelectedIndex = -1
        tbSucursal.SelectedIndex = -1
    End Sub

    Public Sub _PMOLimpiar()
        tbGestion.Value = 0
        '_prCargarGridPresupuestoNuevoIngreso()
        '_prCargarGridPresupuestoNuevoEgreso()
        '_prCargarGridPresupuestoResultado()

        grDetalleEgreso.DataSource = Nothing
        grDetalleIngreso.DataSource = Nothing
        grResultado.DataSource = Nothing

    End Sub

    Public Sub _PMOLimpiarErrores()
        MEP.Clear()

    End Sub

    Public Function _PMOGrabarRegistro() As Boolean
        Dim dtDetalle As DataTable = _prObtenerDetalle()

        Dim res As Boolean = L_prPresupuestoGrabar(dtDetalle)
        If res Then

            ToastNotification.Show(Me, "Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Function _PMOModificarRegistro() As Boolean


        'Dim res As Boolean = L_prComprobanteModificar(tbNumi.Text, tbNroDoc.Text, tbTipo.Value, tbAnio.Text, tbMes.Text, tbNum.Text, fecha.ToString("yyyy/MM/dd hh:mm:ss"), tbTipoCambio.Value, tbGlosa.Text, tbObs.Text, gi_empresaNumi, dtDetalle)
        'If res Then

        '    ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        '    '_PSalirRegistro()
        'End If
        'Return res
        Return False
    End Function

    Public Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prPresupuestoEliminar(gi_empresaNumi, tbGestion.Value, tbModulo.Value, tbSucursal.Value, mensajeError)
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

        If tbGestion.Value = 0 Or tbGestion.Text = String.Empty Then
            tbGestion.BackgroundStyle.BackColor = Color.Red
            MEP.SetError(tbGestion, "instrodusca la gestion!".ToUpper)
            _ok = False
        Else
            tbGestion.BackgroundStyle.BackColor = Color.White
            MEP.SetError(tbGestion, "")
        End If

        If tbModulo.SelectedIndex < 0 Then
            tbModulo.BackColor = Color.Red
            MEP.SetError(tbModulo, "seleccione modulo!".ToUpper)
            _ok = False
        Else
            tbModulo.BackColor = Color.White
            MEP.SetError(tbModulo, "")
        End If


        If tbSucursal.SelectedIndex < 0 Then
            tbSucursal.BackColor = Color.Red
            MEP.SetError(tbSucursal, "seleccione sucursal!".ToUpper)
            _ok = False
        Else
            tbSucursal.BackColor = Color.White
            MEP.SetError(tbSucursal, "")
        End If


        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prPresupuestoGeneralGestiones(gi_empresaNumi)
        Return dtBuscador
    End Function

    Public Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        'listEstCeldas.Add(New Modelos.Celda("oanumi", True, "ID", 70))
        'listEstCeldas.Add(New Modelos.Celda("oatip", False))
        'listEstCeldas.Add(New Modelos.Celda("oanumdoc", True, "NRO. DOCUMENTO", 150))
        'listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "TIPO", 100))
        'listEstCeldas.Add(New Modelos.Celda("oaano", False))
        'listEstCeldas.Add(New Modelos.Celda("oames", False))
        'listEstCeldas.Add(New Modelos.Celda("oanum", True, "NUMERO", 100))
        'listEstCeldas.Add(New Modelos.Celda("oafdoc", True, "FECHA", 100))
        'listEstCeldas.Add(New Modelos.Celda("oatc", True, "TIPO DE CAMBIO", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("oaglosa", True, "GLOSA", 200))
        'listEstCeldas.Add(New Modelos.Celda("oaobs", True, "OBSERVACION", 200))
        'listEstCeldas.Add(New Modelos.Celda("oaemp", False))

        Return listEstCeldas
    End Function

    Public Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbGestion.Value = .GetValue("chanio")
            tbModulo.Value = .GetValue("chmod")
            tbSucursal.Value = .GetValue("chsuc")
            _prCargarGridPresupuestoGrabadoIngreso(tbGestion.Value)
            _prCargarGridPresupuestoGrabadoEgreso(tbGestion.Value)
            _prCargarGridPresupuestoResultado()

            _prCalcularResultado()
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


    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalleIngreso.CellEdited
        If e.Column.Key = "ene" Or e.Column.Key = "feb" Or e.Column.Key = "mar" Or e.Column.Key = "abr" Or e.Column.Key = "may" Or
            e.Column.Key = "jun" Or e.Column.Key = "jul" Or e.Column.Key = "ago" Or e.Column.Key = "sep" Or e.Column.Key = "oct" Or
            e.Column.Key = "nov" Or e.Column.Key = "dic" Then

            Dim montoAcu As Double = 0
            For i As Integer = 2 To grDetalleIngreso.RootTable.Columns.Count - 2 Step 2
                Dim montoMes As Double = grDetalleIngreso.GetValue(i)
                montoAcu = montoAcu + montoMes
                grDetalleIngreso.SetValue(i + 1, montoAcu)
            Next
            grDetalleIngreso.SetValue("total", montoAcu)

        End If

    End Sub

    Private Sub grDetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalleIngreso.EditingCell
        If grDetalleIngreso.GetValue("ednumi") <= 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub grDetalleEgreso_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalleEgreso.CellEdited
        If e.Column.Key = "ene" Or e.Column.Key = "feb" Or e.Column.Key = "mar" Or e.Column.Key = "abr" Or e.Column.Key = "may" Or
            e.Column.Key = "jun" Or e.Column.Key = "jul" Or e.Column.Key = "ago" Or e.Column.Key = "sep" Or e.Column.Key = "oct" Or
            e.Column.Key = "nov" Or e.Column.Key = "dic" Then

            Dim montoAcu As Double = 0
            For i As Integer = 2 To grDetalleEgreso.RootTable.Columns.Count - 2 Step 2
                Dim montoMes As Double = grDetalleEgreso.GetValue(i)
                montoAcu = montoAcu + montoMes
                grDetalleEgreso.SetValue(i + 1, montoAcu)
            Next
            grDetalleEgreso.SetValue("total", montoAcu)

        End If

    End Sub

    Private Sub grDetalleEgreso_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalleEgreso.EditingCell
        If grDetalleEgreso.GetValue("ednumi") <= 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub grDetalleIngreso_RecordUpdated(sender As Object, e As EventArgs) Handles grDetalleIngreso.RecordUpdated, grDetalleEgreso.RecordUpdated
        If btnGrabar.Enabled = True Then
            _prCalcularResultado()
        End If

    End Sub

    Private Sub grDetalleIngreso_Scroll(sender As Object, e As EventArgs) Handles grDetalleIngreso.Scroll
        grDetalleEgreso.HorizontalScrollPosition = grDetalleIngreso.HorizontalScrollPosition
        grResultado.HorizontalScrollPosition = grDetalleIngreso.HorizontalScrollPosition

    End Sub

    Private Sub grDetalleEgreso_Scroll(sender As Object, e As EventArgs) Handles grDetalleEgreso.Scroll
        grDetalleIngreso.HorizontalScrollPosition = grDetalleEgreso.HorizontalScrollPosition
        grResultado.HorizontalScrollPosition = grDetalleEgreso.HorizontalScrollPosition
    End Sub

    Private Sub grResultado_Scroll(sender As Object, e As EventArgs) Handles grResultado.Scroll
        grDetalleIngreso.HorizontalScrollPosition = grResultado.HorizontalScrollPosition
        grDetalleEgreso.HorizontalScrollPosition = grResultado.HorizontalScrollPosition
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'Dim _ok As Boolean = True
        'If tbModulo.SelectedIndex < 0 Then
        '    tbModulo.BackColor = Color.Red
        '    MEP.SetError(tbModulo, "seleccione modulo!".ToUpper)
        '    _ok = False
        'Else
        '    tbModulo.BackColor = Color.White
        '    MEP.SetError(tbModulo, "")
        'End If


        'If tbSucursal.SelectedIndex < 0 Then
        '    tbSucursal.BackColor = Color.Red
        '    MEP.SetError(tbSucursal, "seleccione sucursal!".ToUpper)
        '    _ok = False
        'Else
        '    tbSucursal.BackColor = Color.White
        '    MEP.SetError(tbSucursal, "")
        'End If

        'If _ok Then
        '    _prCargarGridPresupuestoNuevoIngreso()
        '    _prCargarGridPresupuestoNuevoEgreso()
        '    _prCargarGridPresupuestoResultado()
        'End If
    End Sub

    Private Sub tbSucursal_ValueChanged(sender As Object, e As EventArgs) Handles tbSucursal.ValueChanged, tbModulo.ValueChanged
        If btnGrabar.Enabled = False Then
            Return
        End If
        Dim _ok As Boolean = True
        If tbModulo.SelectedIndex < 0 Then
            tbModulo.BackColor = Color.Red
            MEP.SetError(tbModulo, "seleccione modulo!".ToUpper)
            _ok = False
        Else
            tbModulo.BackColor = Color.White
            MEP.SetError(tbModulo, "")
        End If


        If tbSucursal.SelectedIndex < 0 Then
            tbSucursal.BackColor = Color.Red
            MEP.SetError(tbSucursal, "seleccione sucursal!".ToUpper)
            _ok = False
        Else
            tbSucursal.BackColor = Color.White
            MEP.SetError(tbSucursal, "")
        End If

        If _ok Then
            _prCargarGridPresupuestoNuevoIngreso()
            _prCargarGridPresupuestoNuevoEgreso()
            _prCargarGridPresupuestoResultado()
        Else
            grDetalleEgreso.DataSource = Nothing
            grDetalleIngreso.DataSource = Nothing
            grResultado.DataSource = Nothing
        End If
    End Sub
End Class