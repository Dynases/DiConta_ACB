Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar.Controls

Public Class F1_Maquina

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private _dtMangueras As DataTable
    Private _dtCombustible As DataTable

    Private Const _maxAuxiliares As Integer = 4

#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()
        Me.Text = "m a q u i n a s".ToUpper
        '_prCargarComboMaquina1()
        _prCargarComboCombustibles()
        _prCargarComboLibreriaManguera(tbManguera, gi_LibMaquina, gi_LibMAQUINAManguera)
        _PMIniciarTodo()

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

    Private Sub _prCargarComboLibreriaManguera(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaDetalleGeneral(cod1, cod2)
        _dtMangueras = dt

        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cnnum").Width = 70
            .DropDownList.Columns("cnnum").Caption = "COD"

            .DropDownList.Columns.Add("cndesc1").Width = 200
            .DropDownList.Columns("cndesc1").Caption = "DESCRIPCION"

            .ValueMember = "cndesc1"
            .DisplayMember = "cndesc1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    'Private Sub _prCargarComboMaquina1()
    '    Dim dt As New DataTable
    '    dt = L_prArqueoMaquinaAyuda()
    '    With tbMaquina1
    '        .DropDownList.Columns.Clear()

    '        .DropDownList.Columns.Add("aenumi").Width = 70
    '        .DropDownList.Columns("aenumi").Caption = "COD"

    '        .DropDownList.Columns.Add("aedesc").Width = 200
    '        .DropDownList.Columns("aedesc").Caption = "DESCRIPCION"

    '        .ValueMember = "aenumi"
    '        .DisplayMember = "aedesc"
    '        .DataSource = dt
    '        .Refresh()
    '    End With
    'End Sub

    Private Sub _prCargarComboCombustibles()
        Dim dt As New DataTable
        dt = L_prMaquinaObtenerCombustible()
        _dtCombustible = dt
        With tbCombustible
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("agnumi").Width = 70
            .DropDownList.Columns("agnumi").Caption = "COD"

            .DropDownList.Columns.Add("agdesc").Width = 200
            .DropDownList.Columns("agdesc").Caption = "DESCRIPCION"

            .ValueMember = "agdesc"
            .DisplayMember = "agdesc"
            .DataSource = dt
            .Refresh()
        End With
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

            'If grDetalle.RowCount < _maxAuxiliares Then
            '    grDetalle.AllowAddNew = InheritableBoolean.True
            'End If
        End If
    End Sub

    Private Sub _prCargarGridDetalle(numi As String)
        Dim dt As New DataTable
        dt = L_prMaquinaDetalleGeneral(numi)

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("afnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("afnumita3")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("afmang")
            .Caption = "COD"
            .Width = 100
            .EditType = EditType.NoEdit
        End With
        With grDetalle.RootTable.Columns("cndesc1")
            .Caption = "MANGUERA"
            .Width = 150
            .EditType = EditType.MultiColumnDropDown
            .DropDown = tbManguera.DropDownList
        End With

        With grDetalle.RootTable.Columns("afnumita4")
            .Caption = "COD"
            .Width = 100
            .EditType = EditType.NoEdit
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("agdesc")
            .Caption = "COMBUSTIBLE"
            .Width = 150
            .EditType = EditType.MultiColumnDropDown
            .DropDown = tbCombustible.DropDownList
        End With
        With grDetalle.RootTable.Columns("agprecio")
            .Caption = "PRECIO"
            .Width = 150
            .EditType = EditType.NoEdit
            .FormatString = "0.00"
        End With

        With grDetalle.RootTable.Columns("afmit")
            .Caption = "MIT"
            .Width = 150
            .FormatString = "0.00"
        End With

        With grDetalle.RootTable.Columns("afest")
            .Caption = "ESTADO"
            .Width = 150
            .DefaultValue = 1
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
#End Region

#Region "METODOS SOBRECARGADOS"
    Public Overrides Sub _PMOHabilitar()

        tbNombre.ReadOnly = False
        tbCombustible.ReadOnly = False
        tbEstado.IsReadOnly = False
        'tbMaquina1.ReadOnly = False
        'tbMit.IsInputReadOnly = False
        'tbPrecio.IsInputReadOnly = False

        grDetalle.ContextMenuStrip = ContextMenuStrip1
        grDetalle.AllowAddNew = InheritableBoolean.True
        grDetalle.AllowEdit = InheritableBoolean.True

        'If grDetalle.RowCount >= _maxAuxiliares Then
        '    grDetalle.AllowAddNew = InheritableBoolean.False
        'End If
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbNombre.ReadOnly = True
        tbCombustible.ReadOnly = True
        tbEstado.IsReadOnly = True
        'tbMaquina1.ReadOnly = True
        'tbMit.IsInputReadOnly = True
        'tbPrecio.IsInputReadOnly = True

        grDetalle.ContextMenuStrip = Nothing
        grDetalle.AllowAddNew = InheritableBoolean.False
        grDetalle.AllowEdit = InheritableBoolean.False
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbNombre.Text = ""
        tbCombustible.Text = ""
        tbEstado.Value = True
        'tbMaquina1.Text = ""
        'tbMit.Value = 0
        'tbPrecio.Value = 0

        'VACIO EL DETALLE
        _prCargarGridDetalle(-1)
    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbNombre.BackColor = Color.White
        tbCombustible.BackColor = Color.White
        tbEstado.BackColor = Color.White
        'tbMit.BackColor = Color.White

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean
        tbEstado.Focus()
        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "afnumi", "afnumita3", "afmang", "afnumita4", "afmit", "afest", "estado")
        Dim res As Boolean = L_prMaquinaGrabar(tbNumi.Text, tbNombre.Text, IIf(tbEstado.Value = True, 1, 0), dtDetalle)
        If res Then
            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        tbEstado.Focus()

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "afnumi", "afnumita3", "afmang", "afnumita4", "afmit", "afest", "estado")

        Dim res As Boolean = L_prMaquinaModificar(tbNumi.Text, tbNombre.Text, IIf(tbEstado.Value = True, 1, 0), dtDetalle)
        If res Then

            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _PSalirRegistro()
        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prMaquinaBorrar(tbNumi.Text, tbNombre.Text, IIf(tbEstado.Value = True, 1, 0), mensajeError)
            If res Then
                ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbNombre.Text = String.Empty Then
            tbNombre.BackColor = Color.Red
            MEP.SetError(tbNombre, "ingrese la descripcion de la maquina!".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If

        'If tbCombustible.SelectedIndex < 0 Then
        '    tbCombustible.BackColor = Color.Red
        '    MEP.SetError(tbCombustible, "seleccione el tipo de combustible!".ToUpper)
        '    _ok = False
        'Else
        '    tbCombustible.BackColor = Color.White
        '    MEP.SetError(tbCombustible, "")
        'End If

        'If tbMaquina1.SelectedIndex < 0 Then
        '    tbMaquina1.BackColor = Color.Red
        '    MEP.SetError(tbMaquina1, "seleccione la maquina asociada!".ToUpper)
        '    _ok = False
        'Else
        '    tbMaquina1.BackColor = Color.White
        '    MEP.SetError(tbMaquina1, "")
        'End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prMaquinaGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("aenumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("aedesc", True, "NOMBRE", 250))
        'listEstCeldas.Add(New Modelos.Celda("aenumita2", False))
        'listEstCeldas.Add(New Modelos.Celda("aedesc2", True, "MAQUINA ASOCIADA", 200))
        listEstCeldas.Add(New Modelos.Celda("aeest", False, "ESTADO", 150))
        listEstCeldas.Add(New Modelos.Celda("aeest2", True, "ESTADO", 150))
        'listEstCeldas.Add(New Modelos.Celda("aeconbus", False))
        'listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "COMBUSTIBLE", 200))
        'listEstCeldas.Add(New Modelos.Celda("aeprecio", True, "PRECIO", 150, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("aemitfin", True, "MIT", 150, "0.00"))
        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("aenumi").ToString
            tbNombre.Text = .GetValue("aedesc").ToString
            'tbMaquina1.Text = .GetValue("aedesc2").ToString.Trim

            tbEstado.Value = IIf(.GetValue("aeest") = 1, True, False)
            'tbCombustible.Value = .GetValue("aeconbus")
            'tbPrecio.Value = .GetValue("aeprecio")
            'tbMit.Value = .GetValue("aemitfin")

            'CARGAR DETALLE
            _prCargarGridDetalle(tbNumi.Text)

            'lbFecha.Text = CType(.GetValue("ybfact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("ybhact").ToString
            'lbUsuario.Text = .GetValue("ybuact").ToString

        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()


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

    Private Sub F1_Rol_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbNombre.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbNombre.Focus()
    End Sub

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited
        Dim estado As Integer = grDetalle.GetValue("estado")
        If estado = 1 Then
            grDetalle.SetValue("estado", 2)

        End If
        If e.Column.Key = "cndesc1" Then
            Dim filasSelect As DataRow() = _dtMangueras.Select("cndesc1='" + grDetalle.GetValue("cndesc1") + "'")
            If filasSelect.Count > 0 Then
                grDetalle.SetValue("afmang", filasSelect(0).Item("cnnum"))
            End If
        End If
        If e.Column.Key = "agdesc" Then
            Dim filasSelect As DataRow() = _dtCombustible.Select("agdesc='" + grDetalle.GetValue("agdesc") + "'")
            If filasSelect.Count > 0 Then
                grDetalle.SetValue("afnumita4", filasSelect(0).Item("agnumi"))
                grDetalle.SetValue("agprecio", filasSelect(0).Item("agprecio"))

            End If
        End If
    End Sub

    Private Sub grDetalle_RecordAdded(sender As Object, e As EventArgs) Handles grDetalle.RecordAdded
        'If grDetalle.RowCount = _maxAuxiliares Then
        '    grDetalle.AllowAddNew = InheritableBoolean.False
        'End If
    End Sub
    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        _prEliminarFilaDetalle()
    End Sub

End Class