Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls

Public Class F1_Auxiliares
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()
        Me.Text = "a u x i l i a r e s".ToUpper

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



    Private Sub _prCargarGridDetalle(numi As String)
        Dim dt As New DataTable
        dt = L_prAuxiliarDetalleGeneral(numi)

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("cdnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("cdnumitc3")
            .Width = 50
            .Visible = False
        End With


        With grDetalle.RootTable.Columns("cddesc")
            .Caption = "DESCRIPCION"
            .Width = 250
        End With
        With grDetalle.RootTable.Columns("cdest")
            .Visible = False
            .DefaultValue = 1
        End With

        With grDetalle.RootTable.Columns("cdest2")
            .Caption = "ESTADO"
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .DefaultValue = True
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
        End If
    End Sub
#End Region

#Region "METODOS SOBRECARGADOS"
    Public Overrides Sub _PMOHabilitar()

        tbDesc.ReadOnly = False
        grDetalle.ContextMenuStrip = ContextMenuStrip1
        grDetalle.AllowAddNew = InheritableBoolean.True
        grDetalle.AllowEdit = InheritableBoolean.True
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbDesc.ReadOnly = True
        grDetalle.ContextMenuStrip = Nothing
        grDetalle.AllowAddNew = InheritableBoolean.False
        grDetalle.AllowEdit = InheritableBoolean.False
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbDesc.Text = ""

        'VACIO EL DETALLE
        _prCargarGridDetalle(-1)

    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbDesc.BackColor = Color.White

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "cdnumi", "cdnumitc3", "cddesc", "cdest", "estado")
        Dim res As Boolean = L_prAuxiliarGrabar(tbNumi.Text, tbDesc.Text, dtDetalle)
        If res Then
            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "cdnumi", "cdnumitc3", "cddesc", "cdest", "estado")
        Dim res As Boolean = L_prAuxiliarModificar(tbNumi.Text, tbDesc.Text, dtDetalle)
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
            Dim res As Boolean = L_prAuxiliarBorrar(tbNumi.Text, tbDesc.Text, mensajeError)
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

        If tbDesc.Text = String.Empty Then
            tbDesc.BackColor = Color.Red
            MEP.SetError(tbDesc, "ingrese la descripcion!".ToUpper)
            _ok = False
        Else
            tbDesc.BackColor = Color.White
            MEP.SetError(tbDesc, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prAuxiliarGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("ccnumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("ccdesc", True, "DESCRIPCION", 300))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("ccnumi").ToString
            tbDesc.Text = .GetValue("ccdesc").ToString


            'lbFecha.Text = CType(.GetValue("ybfact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("ybhact").ToString
            'lbUsuario.Text = .GetValue("ybuact").ToString

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

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited
        Dim estado As Integer = grDetalle.GetValue("estado")
        If estado = 1 Then
            grDetalle.SetValue("estado", 2)

        End If
        If e.Column.Key = "cdest2" Then
            grDetalle.SetValue("cdest", grDetalle.GetValue("cdest2"))
        End If

    End Sub

    Private Sub grDetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalle.EditingCell
        'If btnGrabar.Enabled Then
        '    If e.Column.Key <> "ycshow" And e.Column.Key <> "ycadd" And e.Column.Key <> "ycmod" And e.Column.Key <> "ycdel" Then
        '        e.Cancel = True
        '    End If
        'Else
        '    e.Cancel = True
        'End If


    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        _prEliminarFilaDetalle()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbDesc.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbDesc.Focus()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub
End Class