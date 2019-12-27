Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar.Controls

Public Class F1_CuemtaAutomatica

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem


#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()
        Me.Text = "c u e n t a s     a u t o m a t i c a s".ToUpper

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


        End If
    End Sub

    Private Sub _prCargarGridDetalle(numi As String)
        Dim dt As New DataTable
        dt = L_prCuentasAutomaticaDetalleGeneral(numi)

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("chnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("chnumitc7")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("chnumitc1")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cacta")
            .Caption = "COD"
            .Width = 120
            .HeaderStyle.TextAlignment = TextAlignment.Center
            .EditType = EditType.NoEdit
        End With
        With grDetalle.RootTable.Columns("cadesc")
            .Caption = "CUENTA"
            .Width = 300
            .HeaderStyle.TextAlignment = TextAlignment.Center
            .EditType = EditType.NoEdit

        End With

        With grDetalle.RootTable.Columns("chporcen")
            .Caption = "PORCENTAJE"
            .Width = 120
            .HeaderStyle.TextAlignment = TextAlignment.Center
            .FormatString = "0"
            .CellStyle.TextAlignment = TextAlignment.Far
        End With

        With grDetalle.RootTable.Columns("chdebe")
            .Caption = "DEBE"
            .HeaderStyle.TextAlignment = TextAlignment.Center
            .Width = 80
            .DefaultValue = False

        End With

        With grDetalle.RootTable.Columns("chhaber")
            .Caption = "HABER"
            .Width = 80
            .HeaderStyle.TextAlignment = TextAlignment.Center
            .DefaultValue = False
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
        tbEstado.IsReadOnly = False

        tbIngreso.Enabled = True
        tbEgreso.Enabled = True
        tbTraspaso.Enabled = True

        grDetalle.ContextMenuStrip = ContextMenuStrip1
        grDetalle.AllowAddNew = InheritableBoolean.True
        grDetalle.AllowEdit = InheritableBoolean.True


    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbNombre.ReadOnly = True
        tbEstado.IsReadOnly = True

        tbIngreso.Enabled = False
        tbEgreso.Enabled = False
        tbTraspaso.Enabled = False

        grDetalle.ContextMenuStrip = Nothing
        grDetalle.AllowAddNew = InheritableBoolean.False
        grDetalle.AllowEdit = InheritableBoolean.False
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbNombre.Text = ""
        tbEstado.Value = True

        tbIngreso.Checked = False
        tbEgreso.Checked = False
        tbTraspaso.Checked = False

        'VACIO EL DETALLE
        _prCargarGridDetalle(-1)
    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbNombre.BackColor = Color.White
        tbEstado.BackColor = Color.White
        'tbMit.BackColor = Color.White

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean
        tbEstado.Focus()
        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "chnumi", "chnumitc7", "chnumitc1", "chporcen", "chdebe", "chhaber", "estado")
        Dim res As Boolean = L_prCuentasAutomaticaGrabar(tbNumi.Text, tbNombre.Text, IIf(tbEstado.Value = True, 1, 0), IIf(tbIngreso.Checked = True, 1, 0), IIf(tbEgreso.Checked = True, 1, 0), IIf(tbTraspaso.Checked = True, 1, 0), dtDetalle)
        If res Then
            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        tbEstado.Focus()

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "chnumi", "chnumitc7", "chnumitc1", "chporcen", "chdebe", "chhaber", "estado")

        Dim res As Boolean = L_prCuentasAutomaticaModificar(tbNumi.Text, tbNombre.Text, IIf(tbEstado.Value = True, 1, 0), IIf(tbIngreso.Checked = True, 1, 0), IIf(tbEgreso.Checked = True, 1, 0), IIf(tbTraspaso.Checked = True, 1, 0), dtDetalle)
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
            Dim res As Boolean = L_prCuentasAutomaticaBorrar(tbNumi.Text, tbNombre.Text, IIf(tbEstado.Value = True, 1, 0), IIf(tbIngreso.Checked = True, 1, 0), IIf(tbEgreso.Checked = True, 1, 0), IIf(tbTraspaso.Checked = True, 1, 0), mensajeError)
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

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
        For Each fila As DataRow In dtDetalle.Rows
            Dim debe As Boolean = fila.Item("chdebe")
            Dim haber As Boolean = fila.Item("chhaber")
            If debe = True And haber = True Then
                _ok = False
                ToastNotification.Show(Me, "no debe estar seleccionado el debe y el haber al mismo tiempo en el detalle".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                Exit For
            End If
        Next

        Dim dtRegistros As DataTable = New DataTable
        Dim tipo As String = ""
        If tbIngreso.Checked = True Then
            dtRegistros = L_prCuentasAutomaticaObtenerIngresos()
            tipo = "INGRESO"
        Else
            If tbEgreso.Checked = True Then
                dtRegistros = L_prCuentasAutomaticaObtenerEgresos()
                tipo = "EGRESO"
            Else
                If tbTraspaso.Checked = True Then
                    dtRegistros = L_prCuentasAutomaticaObtenerTraspaso()
                    tipo = "TRASPASO"
                End If
            End If
        End If

        If dtRegistros.Rows.Count >= 10 Then
            _ok = False

            ToastNotification.Show(Me, "ya existen registrados 10 cuentas automaticas de tipo: ".ToUpper + tipo, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If
        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prCuentasAutomaticaGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("cgnumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("cgdesc", True, "DESCRIPCION", 250))
        listEstCeldas.Add(New Modelos.Celda("cgest", False, "ESTADO", 150))
        listEstCeldas.Add(New Modelos.Celda("cgest2", True, "ESTADO", 150))
        listEstCeldas.Add(New Modelos.Celda("cging", True, "INGRESO", 100))
        listEstCeldas.Add(New Modelos.Celda("cgegre", True, "EGRESO", 100))
        listEstCeldas.Add(New Modelos.Celda("cgtras", True, "TRASPASO", 100))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("cgnumi").ToString
            tbNombre.Text = .GetValue("cgdesc").ToString
            tbIngreso.Checked = IIf(IsDBNull(.GetValue("cging")) = True, 0, .GetValue("cging"))
            tbEgreso.Checked = IIf(IsDBNull(.GetValue("cgegre")) = True, 0, .GetValue("cgegre"))
            tbTraspaso.Checked = IIf(IsDBNull(.GetValue("cgtras")) = True, 0, .GetValue("cgtras"))

            tbEstado.Value = IIf(.GetValue("cgest") = 1, True, False)


            'CARGAR DETALLE
            _prCargarGridDetalle(tbNumi.Text)



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
        'If e.Column.Key = "chdebe" Then
        '    If grDetalle.GetValue("chdebe") = 1 Then
        '        grDetalle.SetValue("chhaber", 0)
        '    Else
        '        grDetalle.SetValue("chhaber", 1)

        '    End If
        'End If
        'If e.Column.Key = "chhaber" = 1 Then
        '    If grDetalle.GetValue("chhaber") = 1 Then
        '        grDetalle.SetValue("chdebe", 0)
        '    Else
        '        grDetalle.SetValue("chdebe", 1)
        '    End If
        'End If
    End Sub


    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        _prEliminarFilaDetalle()
    End Sub
    Private Sub grDetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grDetalle.KeyDown
        Dim f As Integer = grDetalle.Row
        Dim c As Integer = grDetalle.Col
        If e.KeyData = Keys.Control + Keys.Enter And c >= 0 And btnGrabar.Enabled = True Then
            If grDetalle.RootTable.Columns(c).Key = "cacta" Or grDetalle.RootTable.Columns(c).Key = "cadesc" Then

                Dim frmAyuda As Modelos.ModeloAyuda
                Dim dt As DataTable

                dt = L_prCuentaGeneralBasico(gi_empresaNumi)

                Dim listEstCeldas As New List(Of Modelos.Celda)
                listEstCeldas.Add(New Modelos.Celda("canumi", False))
                listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
                listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("catipo", False))
                listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("cadesc2", False))
                listEstCeldas.Add(New Modelos.Celda("numAux", False))
                listEstCeldas.Add(New Modelos.Celda("catipo1", False))
                listEstCeldas.Add(New Modelos.Celda("caniv", False))

                frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
                frmAyuda.grJBuscador.DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
                Dim fc As GridEXFormatCondition
                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 1)
                fc.FormatStyle.BackColor = Color.LightGreen
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 2)
                fc.FormatStyle.BackColor = Color.LightYellow
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 3)
                fc.FormatStyle.BackColor = Color.LightBlue
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 4)
                fc.FormatStyle.BackColor = Color.LightCoral
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 5)
                fc.FormatStyle.BackColor = Color.LightSlateGray
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 6)
                fc.FormatStyle.BackColor = Color.LightGreen
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                frmAyuda.ShowDialog()

                If frmAyuda.seleccionado = True Then
                    Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                    Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                    Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value
                    Dim numAux As Integer = frmAyuda.filaSelect.Cells("numAux").Value
                    Dim cuentaPadre As String = frmAyuda.filaSelect.Cells("cadesc2").Value

                    grDetalle.SetValue("chnumitc1", numiCuenta)
                    grDetalle.SetValue("cacta", cod)
                    grDetalle.SetValue("cadesc", desc)


                End If
            End If
        End If
    End Sub
End Class