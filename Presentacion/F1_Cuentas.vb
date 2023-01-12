Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class F1_Cuentas

    Public _nameButton As String
    Public _tab As SuperTabItem
    'Private _dtAuxiliares As DataTable
    Private Const _maxAuxiliares As Integer = 3
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        Me.Text = "c u e n t a s".ToUpper
        _MTipoInserccionNuevo = False

        _prCargarComboLibreria(tbTipo, gi_LibCuenta, gi_LibCuentaTipo)
        '_prCargarComboAuxiliares()
        _prCargarComboEmpresa()

        _PMIniciarTodo()

        _prAsignarPermisos()

        tvCuentas.SelectedNode.ExpandAll()

        '_prCargarArbol()
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

    'Private Sub _prCargarComboAuxiliares()
    '    Dim dt As New DataTable
    '    dt = L_prAuxiliarGeneral()
    '    _dtAuxiliares = dt

    '    With tbAuxiliar
    '        .DropDownList.Columns.Clear()

    '        .DropDownList.Columns.Add("ccnumi").Width = 70
    '        .DropDownList.Columns("ccnumi").Caption = "COD"

    '        .DropDownList.Columns.Add("ccdesc").Width = 200
    '        .DropDownList.Columns("ccdesc").Caption = "DESCRIPCION"

    '        .ValueMember = "ccdesc"
    '        .DisplayMember = "ccdesc"
    '        .DataSource = dt
    '        .Refresh()
    '    End With
    'End Sub

    Private Sub _prCargarComboEmpresa()
        Dim dt As New DataTable
        dt = L_prEmpresaAyuda()

        With tbEmpresa
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cenumi").Width = 70
            .DropDownList.Columns("cenumi").Caption = "COD"

            .DropDownList.Columns.Add("cedesc").Width = 200
            .DropDownList.Columns("cedesc").Caption = "DESCRIPCION"

            .ValueMember = "cenumi"
            .DisplayMember = "cedesc"
            .DataSource = dt
            .Refresh()
        End With

        tbEmpresa.Text = gs_empresaDescSistema
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
    Private Sub _prCargarArbol()
        'Dim dtReg As DataTable = L_prCuentaGeneral()
        Dim dtReg As DataTable = CType(JGrM_Buscador.DataSource, DataTable)

        tvCuentas.ImageList = ImageList1
        tvCuentas.Nodes.Clear()
        Dim nuevoNodo As New TreeNode

        'Descripción o texto del nodo
        nuevoNodo.Text = "CUENTAS"
        'Si necesito guardar el valor del IdentificadorNodo dentro del mismo nodo
        nuevoNodo.Name = "0"
        'Si necesito guardar el valor del IdentificadorPadre dentro del mismo nodo
        nuevoNodo.Tag = 0
        nuevoNodo.NodeFont = New Font("Arial", 9, FontStyle.Bold)
        nuevoNodo.ImageIndex = 3
        nuevoNodo.SelectedImageIndex = 2

        tvCuentas.Nodes.Add(nuevoNodo)

        '_prCrearNodosDelPadre(0, Nothing, dtReg)
        _prCrearNodosDelPadre(0, nuevoNodo, dtReg)
        tvCuentas.SelectedNode = tvCuentas.Nodes(0)

    End Sub
    Private Sub _prCrearNodosDelPadre(ByVal indicePadre As Integer, ByVal nodePadre As TreeNode, dtReg As DataTable)

        Dim dataViewHijos As DataView

        'Crear un DataView con los Nodos que dependen del Nodo padre pasado como parámetro
        dataViewHijos = New DataView(dtReg)

        'Filtra por cada padre
        dataViewHijos.RowFilter = dtReg.Columns("capadre").ColumnName +
        " = " + indicePadre.ToString()

        ' Agregar al TreeView los nodos Hijos que se han obtenido en el DataView.
        For Each dataRowCurrent As DataRowView In dataViewHijos
            Dim nuevoNodo As New TreeNode

            'Descripción o texto del nodo
            nuevoNodo.Text = dataRowCurrent("cactaucg").ToString().Trim() + " " + dataRowCurrent("cadesc").ToString().Trim()

            'Si necesito guardar el valor del IdentificadorNodo dentro del mismo nodo
            nuevoNodo.Name = dataRowCurrent("canumi").ToString().Trim()

            'Si necesito guardar el valor del IdentificadorPadre dentro del mismo nodo
            nuevoNodo.Tag = dataRowCurrent("capadre").ToString().Trim()

            'Si el parámetro nodoPadre es nulo es porque es la primera llamada, son los Nodos
            'del primer nivel que no dependen de otro nodo.

            'agregar estilo al nodo
            Dim nivel As Integer = dataRowCurrent("caniv").ToString().Trim()
            If nivel <= 3 Then
                nuevoNodo.NodeFont = New Font("Arial", 9, FontStyle.Bold)
            End If
            nuevoNodo.ImageIndex = 3
            nuevoNodo.SelectedImageIndex = 2
            If nivel = 5 Then
                'nuevoNodo.SelectedImageIndex = 0
                Dim moneda As String = dataRowCurrent("camon").ToString().Trim()
                If moneda = "SU" Then
                    nuevoNodo.ImageIndex = 0
                    nuevoNodo.SelectedImageIndex = 0
                Else
                    nuevoNodo.ImageIndex = 1
                    nuevoNodo.SelectedImageIndex = 1
                End If
                nuevoNodo.ForeColor = Color.Blue
                nuevoNodo.NodeFont = New Font("Arial", 9, FontStyle.Bold)
            End If

            If nodePadre Is Nothing Then
                tvCuentas.Nodes.Add(nuevoNodo)
            Else
                'se añade el nuevo nodo al nodo padre.
                nodePadre.Nodes.Add(nuevoNodo)
            End If

            'Llamada recurrente al mismo método para agregar los Hijos del Nodo recién agregado.
            _prCrearNodosDelPadre(Int32.Parse(dataRowCurrent("canumi").ToString()), nuevoNodo, dtReg)
        Next dataRowCurrent
    End Sub
    Private Sub _prBuscarRegistro(numi As Integer)
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            If numi = dt.Rows(i).Item("canumi") Then
                Exit For
            End If
        Next
        If i >= 0 And i <= dt.Rows.Count - 1 Then
            _MPos = i
            _PMOMostrarRegistro(_MPos)
        End If

    End Sub
    Private Sub _prCargarGridDetalle(numi As String)
        Dim dt As New DataTable
        dt = L_prCuentaDetalleGeneral(numi)

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("cenumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("cenumitc1")
            .Width = 50
            .Visible = False
        End With

        Dim dtAuxiliares As New DataTable
        dtAuxiliares = L_prAuxiliarGeneral()
        With grDetalle.RootTable.Columns("cenumitc3")
            .Caption = "AUXILIAR"
            .Width = 200

            .HasValueList = True
            'Set EditType to Combo or DropDownList.
            'In a MultipleValues Column, the dropdown will appear with a CheckBox
            'at the left of each item to let the user select multiple items
            .EditType = EditType.DropDownList
            .ValueList.PopulateValueList(dtAuxiliares.DefaultView, "ccnumi", "ccdesc")
            .CompareTarget = ColumnCompareTarget.Text
            .DefaultGroupInterval = GroupInterval.Text
        End With

        With grDetalle.RootTable.Columns("cenumitc31")
            .Caption = "ITEMS"
            .Width = 150
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("descitem")
            .Caption = "ITEMS"
            .Width = 150
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ccdesc")
            .Caption = "AUXILIAR"
            .Width = 200
            .EditType = EditType.MultiColumnDropDown
            .DropDown = tbAuxiliar.DropDownList

            .Visible = False
        End With


        With grDetalle.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalle.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grDetalle.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub _prEliminarFilaDetalle()
        If grDetalle.Row >= 0 Then

            Dim estado As Integer = grDetalle.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grDetalle.GetRow(grDetalle.Row).BeginEdit()
                grDetalle.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grDetalle.GetRow(grDetalle.Row).BeginEdit()
                grDetalle.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grDetalle.RemoveFilters()
            grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            If grDetalle.RowCount < _maxAuxiliares Then
                grDetalle.AllowAddNew = InheritableBoolean.True
            End If
        End If
    End Sub
#End Region

#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOLuegoDeCargarBuscador()
        _prCargarArbol()
    End Sub
    Public Overrides Sub _PMOHabilitar()
        tbDesc.ReadOnly = False
        'tbCuenta.ReadOnly = False
        tbMoneda.IsReadOnly = False


        If tvCuentas.SelectedNode.Level = 0 Then
            tbTipo.ReadOnly = False
        End If

        If _MNuevo Then
            tbCuenta1.Visible = True
            tbCuenta2.Visible = True
            tbCuenta.Visible = False
            tbCuenta1.BackColor = Color.LightBlue
            tbCuenta2.BackColor = Color.LightGreen
            tbCuenta1.Refresh()
            tbCuenta2.Refresh()
            tvCuentas.Enabled = False
            panelCuentas.Visible = True

            If tvCuentas.SelectedNode.Level = 4 Then
                grDetalle.ContextMenuStrip = ContextMenuStrip1
                grDetalle.AllowAddNew = InheritableBoolean.True
                grDetalle.AllowEdit = InheritableBoolean.True
            End If
        Else
            If tbNivel2.Text = 5 Then
                grDetalle.ContextMenuStrip = ContextMenuStrip1
                grDetalle.AllowAddNew = InheritableBoolean.True
                grDetalle.AllowEdit = InheritableBoolean.True
            End If
        End If

        If grDetalle.RowCount >= _maxAuxiliares Then
            grDetalle.AllowAddNew = InheritableBoolean.False
        End If

    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbDesc.ReadOnly = True
        tbCuenta.ReadOnly = True
        tbTipo.ReadOnly = True
        tbNivel.ReadOnly = True
        tbCuentaMayor.ReadOnly = True

        tbMoneda.IsReadOnly = True
        tbCuenta1.Visible = False
        tbCuenta1.ReadOnly = True
        tbCuenta2.Visible = False
        'tvCuentas.Enabled = True
        tbCuenta.Visible = True

        tvCuentas.Enabled = True
        panelCuentas.Visible = False

        tbEmpresa.ReadOnly = True

        grDetalle.ContextMenuStrip = Nothing
        grDetalle.AllowAddNew = InheritableBoolean.False
        grDetalle.AllowEdit = InheritableBoolean.False

    End Sub

    Public Overrides Sub _PMOLimpiar()
        If tvCuentas.SelectedNode.Level > 0 Then
            tbCuenta1.Text = tbCuenta.Text
            tbCuentaMayor.Text = tbCuenta.Text
            tbNumiPadre.Text = tbNumi.Text
        Else
            tbCuenta1.Text = ""
            tbCuentaMayor.Text = ""
            tbNumiPadre.Text = "0"
        End If
        tbNivel.Text = tvCuentas.SelectedNode.Level + 1
        tbCuenta2.Text = ""

        tbDesc.Text = ""
        tbNumi.Text = ""
        'tbCuenta.Text = ""
        tbMoneda.Value = True
        'tbTipo.Text = ""
        tbPadre.Text = ""

        'VACIO EL DETALLE
        _prCargarGridDetalle(-1)

        'agregar los auxiliares bases
        Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
        dt.Rows.Add(0, 0, 1, 0, "", 0)
        dt.Rows.Add(0, 0, 11, 0, "", 0)


    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbDesc.BackColor = Color.White
        tbMoneda.BackColor = Color.White

        tbCuenta.BackColor = Color.White
        tbTipo.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean
        If L_prCuentaGetByNroCuenta(tbCuenta.Text, gi_empresaNumi).Rows.Count > 0 Then
            ToastNotification.Show(Me, "la cuenta que desea registrar ya existe".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Blue, eToastPosition.TopCenter)
            Return False
        End If

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "cenumi", "cenumitc1", "cenumitc3", "cenumitc31", "estado")
        Dim res As Boolean = L_prCuentaGrabar(tbNumi.Text, gi_empresaNumi, tbCuenta.Text, tbDesc.Text, tbNivel.Text, IIf(tbMoneda.Value = True, "SU", "BO"), tbTipo.Value, tbNumiPadre.Text, dtDetalle)
        If res Then
            ToastNotification.Show(Me, "Registro ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            'tbCuenta.Focus()
            _PSalirRegistro()
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "cenumi", "cenumitc1", "cenumitc3", "cenumitc31", "estado")
        Dim res As Boolean = L_prCuentaModificar(tbNumi.Text, gi_empresaNumi, tbcuenta3.Text, tbDesc.Text, tbNivel2.Text, IIf(tbMoneda.Value = True, "SU", "BO"), tbTipo.Value, tbPadre.Text, dtDetalle)
        If res Then
            ToastNotification.Show(Me, "Registro ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _PSalirRegistro()

        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            If validar(tbNumi.Text) Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_prCuentaBorrar(tbNumi.Text, 1, tbCuenta.Text, tbDesc.Text, tbNivel2.Text, IIf(tbMoneda.Value = True, "SU", "BO"), tbTipo.Value, tbPadre.Text, mensajeError)
                If res Then
                    ToastNotification.Show(Me, "Codigo de cuenta ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PMFiltrar()
                    tvCuentas.SelectedNode.ExpandAll()
                Else
                    ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If
            End If
        End If
    End Sub
    Public Function validar(numi As Integer) As Boolean
        Dim _Tabla As New DataTable
        _Tabla = L_Comprobante(numi)
        If _Tabla.Rows(0).Item("total") > 0 Then
            ToastNotification.Show(Me, "Existen ".ToUpper + Str(_Tabla.Rows(0).Item("total")) + " registros en comprobantes. No se puede Eliminar.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Return False
        End If

        _Tabla = L_hijoscuenta(numi)
        If _Tabla.Rows(0).Item("total") > 0 Then
            ToastNotification.Show(Me, "No se puede Eliminar cuenta contanedora de otras cuentas.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Return False
        End If

        Return True
    End Function
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbDesc.Text = String.Empty Then
            tbDesc.BackColor = Color.Red
            MEP.SetError(tbDesc, "ingrese la descripcion de la cuenta!".ToUpper)
            _ok = False
        Else
            tbDesc.BackColor = Color.White
            MEP.SetError(tbDesc, "")
        End If

        If tbCuenta.Text = String.Empty Then
            tbCuenta.BackColor = Color.Red
            MEP.SetError(tbCuenta, "ingrese la cuenta!".ToUpper)
            _ok = False
        Else
            tbCuenta.BackColor = Color.White
            MEP.SetError(tbCuenta, "")
        End If

        If tbTipo.SelectedIndex < 0 Then
            tbTipo.BackColor = Color.Red
            MEP.SetError(tbTipo, "seleccione el tipo de cuenta!".ToUpper)
            _ok = False
        Else
            tbTipo.BackColor = Color.White
            MEP.SetError(tbTipo, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok

    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prCuentaGeneral(gi_empresaNumi)
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("canumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("caemp", True, "EMPRESA", 100))
        listEstCeldas.Add(New Modelos.Celda("cactaucg", True, "CUENTA", 100))
        listEstCeldas.Add(New Modelos.Celda("cacta", False, "CUENTA", 100))
        listEstCeldas.Add(New Modelos.Celda("cadesc", True, "DESCRIPCION", 200))
        listEstCeldas.Add(New Modelos.Celda("caniv", True, "NIVEL", 100))
        listEstCeldas.Add(New Modelos.Celda("camon", True, "MONEDA", 70))
        listEstCeldas.Add(New Modelos.Celda("catipo", False))
        listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "TIPO", 70))
        listEstCeldas.Add(New Modelos.Celda("capadre", False))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("canumi").ToString
            tbCuenta.Text = .GetValue("cactaucg")
            tbcuenta3.Text = .GetValue("cacta")
            tbDesc.Text = .GetValue("cadesc").ToString
            tbMoneda.Value = IIf(.GetValue("camon").ToString = "SU", True, False)
            tbTipo.Value = .GetValue("catipo")
            tbPadre.Text = .GetValue("capadre")
            tbNivel2.Text = .GetValue("caniv")

            'lbFecha.Text = CType(.GetValue("ecfact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("echact").ToString
            'lbUsuario.Text = .GetValue("ecuact").ToString

            'CARGAR DETALLE
            _prCargarGridDetalle(tbNumi.Text)
        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            '.SetHighlightOnFocus(tbDesc, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbCuenta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbMoneda, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With

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
    Private Sub F1_Cuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbCuenta2.Focus()

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbCuenta.Focus()
    End Sub

    Private Sub tvCuentas_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvCuentas.AfterSelect
        _prBuscarRegistro(e.Node.Name)
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click

        If IsNothing(tvCuentas.SelectedNode) = False Then
            tvCuentas.SelectedNode.Collapse()
        Else
            ToastNotification.Show(Me, "seleccione una cuenta para contraer".ToUpper, My.Resources.WARNING, 4000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If


    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If IsNothing(tvCuentas.SelectedNode) = False Then
            tvCuentas.SelectedNode.ExpandAll()
        Else
            ToastNotification.Show(Me, "seleccione una cuenta para expandir".ToUpper, My.Resources.WARNING, 4000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If

    End Sub

    Private Sub tbCuenta1_TextChanged(sender As Object, e As EventArgs) Handles tbCuenta1.TextChanged, tbCuenta2.TextChanged
        tbCuenta.Text = tbCuenta1.Text + tbCuenta2.Text
    End Sub

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited


        Dim estado As Integer = grDetalle.GetValue("estado")
        If estado = 1 Then
            grDetalle.SetValue("estado", 2)

        End If
        'If e.Column.Key = "ccdesc" Then
        '    Dim filasSelect As DataRow() = _dtAuxiliares.Select("ccdesc='" + grDetalle.GetValue("ccdesc") + "'")
        '    If filasSelect.Count > 0 Then
        '        grDetalle.SetValue("cenumitc3", filasSelect(0).Item("ccnumi"))
        '    End If
        'End If

    End Sub

    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        If grDetalle.Row >= 2 Then
            _prEliminarFilaDetalle()

        End If
    End Sub

    Private Sub grDetalle_RecordAdded(sender As Object, e As EventArgs) Handles grDetalle.RecordAdded
        If grDetalle.RowCount = _maxAuxiliares Then
            grDetalle.AllowAddNew = InheritableBoolean.False
        End If
    End Sub



    Private Sub grDetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalle.EditingCell
        If grDetalle.Row <= 1 And grDetalle.Row >= 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub grDetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grDetalle.KeyDown
        Dim f As Integer = grDetalle.Row
        Dim c As Integer = grDetalle.Col
        If e.KeyData = Keys.Control + Keys.Enter Then
            If grDetalle.RootTable.Columns(c).Key = "descitem" Then

                Dim frmAyuda As Modelos.ModeloAyuda
                Dim dt As DataTable
                Dim numiAux As Integer = grDetalle.GetValue("cenumitc3") 'grDetalle.GetRow(f).Cells.Item("cdnumitc31")
                dt = L_prItemPorModulo(numiAux)
                If numiAux > 0 Then
                    Dim listEstCeldas As New List(Of Modelos.Celda)
                    listEstCeldas.Add(New Modelos.Celda("cdnumi", True, "COD".ToUpper, 70))
                    listEstCeldas.Add(New Modelos.Celda("cdnumitc3", False, "COD".ToUpper, 70))
                    listEstCeldas.Add(New Modelos.Celda("cddesc", True, "DESCRIPCION".ToUpper, 250))
                    listEstCeldas.Add(New Modelos.Celda("cdest", False, "direccion".ToUpper, 150))


                    frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cliente".ToUpper, listEstCeldas)
                    frmAyuda.ShowDialog()

                    If frmAyuda.seleccionado = True Then
                        Dim numi As String = frmAyuda.filaSelect.Cells("cdnumi").Value
                        Dim desc As String = frmAyuda.filaSelect.Cells("cddesc").Value
                        grDetalle.SetValue("cenumitc31", numi)
                        grDetalle.SetValue("descitem", desc)

                        Dim estado As Integer = grDetalle.GetValue("estado")
                        If estado = 1 Then
                            grDetalle.SetValue("estado", 2)

                        End If
                    End If
                Else
                    ToastNotification.Show(Me, "debe seleccionar un auxiliar".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                End If

            End If

        End If
    End Sub

    Private Sub grDetalle_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grDetalle.FormattingRow

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

    End Sub


End Class