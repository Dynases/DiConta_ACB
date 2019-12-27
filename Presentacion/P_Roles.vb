Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX

Public Class P_Roles
    Private _Dsencabezado As DataSet
    Private moDataTableTipoDato As New DataTable
    Private mdtnColeccion As New Dictionary(Of Long, DataTable)
    Private intModulo As Integer
    Private _Nuevo As Boolean
    Private _Pos As Integer

    Private Sub P_Roles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()
    End Sub

    Private Sub _PIniciarTodo()
        L_abrirConexion()

        Me.Text = "R O L E S"
        Me.WindowState = FormWindowState.Maximized

        _PCargarListModulo()

        _PFiltrar()
        _PInhabilitar()

        _PHabilitarFocus()

        _PCargarBuscador()
    End Sub

    Private Sub _PCargarListModulo()
        Dim dataTable As DataTable
        dataTable = L_General_LibreriaDetalle(-1, 11).Tables(0)

        ListB_Empleados.ItemHeight = 30
        ListB_Empleados.SelectionMode = eSelectionMode.One
        ListB_Empleados.BackColor = Color.AliceBlue
        ListB_Empleados.DataSource = dataTable
        ListB_Empleados.DisplayMember = "cedesc"
        ListB_Empleados.ValueMember = "ceid"

        If dataTable.Rows.Count > 0 Then
            ListB_Empleados.SetSelected(0, False)
        End If
    End Sub

    Private Sub _PFiltrar()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Rol_General(0)
        _Pos = 0

        If _Dsencabezado.Tables(0).Rows.Count <> 0 Then
            _PMostrarRegistro(0)
            Txt_Paginacion.Text = Str(1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
            If _Dsencabezado.Tables(0).Rows.Count > 0 Then
                BBtn_Inicio.Visible = True
                BBtn_Anterior.Visible = True
                BBtn_Siguiente.Visible = True
                BBtn_Ultimo.Visible = True
            End If
        End If
    End Sub

    Private Sub _PMostrarRegistro(_N As Integer)
        With _Dsencabezado.Tables(0).Rows(_N)
            Tb_Id.Text = .Item("ybnumi").ToString
            Tb_Nombre.Text = .Item("ybrol").ToString
        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.Enabled = False
        Tb_Nombre.Enabled = False

        BBtn_Nuevo.Enabled = True
        BBtn_Modificar.Enabled = True
        BBtn_Eliminar.Enabled = True
        BBtn_Grabar.Enabled = False

        Txt_Actualizar.Enabled = False
        ListB_Empleados.Enabled = False
        JGr_Detalle.Enabled = False

        BBtn_Grabar.Image = My.Resources.grabar
        BBtn_Grabar.ImageLarge = My.Resources.grabar

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        EP1.Clear()
        Tb_Nombre.BackColor = Color.White
    End Sub

    Private Sub _PHabilitarFocus()
        HighLigthter_Focus.SetHighlightOnFocus(Tb_Nombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        Tb_Nombre.TabIndex = 1
    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Rol_General(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("ybnumi")
            .Caption = "Codigo"
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 7
        End With

        With JGr_Buscador.RootTable.Columns("ybrol")
            .Caption = "Rol"
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 7
        End With

        'Habilitar Filtradores
        With JGr_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False

            'diseño de la grilla
            JGr_Buscador.VisualStyle = VisualStyle.Office2007
        End With
    End Sub

#Region " Eventos-ListBox "
    Private Sub ListB_Empleados_ItemClick(sender As Object, e As EventArgs) Handles ListB_Empleados.ItemClick
        If ListB_Empleados.SelectedIndex >= 0 Then 'selecciono el item
            If moDataTableTipoDato.Rows.Count > 0 Then
                mdtnColeccion.Remove(intModulo)
                mdtnColeccion.Add(intModulo, moDataTableTipoDato)
            End If

            intModulo = ListB_Empleados.SelectedValue
            Call DataSetTable()

            If mdtnColeccion.ContainsKey(ListB_Empleados.SelectedValue) Then
                moDataTableTipoDato = mdtnColeccion.Item(ListB_Empleados.SelectedValue)
            Else
                Dim dtInsu As DataTable
                If Tb_Id.Text <> String.Empty Then
                    dtInsu = L_RolDetalle_General(-1, Tb_Id.Text, ListB_Empleados.SelectedValue)
                    If dtInsu.Rows.Count = 0 Then
                        dtInsu = L_Formulario_General(1, " and ZY001.yamod=" & ListB_Empleados.SelectedValue).Tables(0)
                        For Each campo As DataRow In dtInsu.Rows
                            moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
                        Next
                    Else
                        For Each campo As DataRow In dtInsu.Rows
                            moDataTableTipoDato.Rows.Add(RowNewTable(campo, False))
                        Next
                    End If
                Else
                    dtInsu = L_Formulario_General(1, " and ZY001.yamod=" & ListB_Empleados.SelectedValue).Tables(0)
                    For Each campo As DataRow In dtInsu.Rows
                        moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
                    Next
                End If
            End If

            JGr_Detalle.DataSource = moDataTableTipoDato
            JGr_Detalle.RetrieveStructure()

            Call grdIntTable()
        End If
    End Sub
#End Region

#Region " DETALLE "
#Region " DataSet-Detalle "
    Private Sub DataSetTable()
        moDataTableTipoDato = New DataTable("Prueba")
        moDataTableTipoDato.Columns.Add("numi", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("numi1", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("Programa", Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("Titulo", Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("Show", Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Add", Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Mod", Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Del", Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Estado", Type.GetType("System.String"))
    End Sub

    Private Function RowNewTable(ByVal oDataRow As DataRow, ByVal boolSw As Boolean) As DataRow
        Dim oRow As DataRow
        oRow = moDataTableTipoDato.NewRow

        If boolSw Then
            oRow("numi") = 0
            oRow("numi1") = CInt(oDataRow("yanumi").ToString.Trim)
            oRow("Programa") = oDataRow("yaprog").ToString().Trim
            oRow("Titulo") = oDataRow("yatit").ToString.Trim
            oRow("Show") = False
            oRow("Add") = False
            oRow("Mod") = False
            oRow("Del") = False
            oRow("Estado") = "New"
        Else
            oRow("numi") = CInt(oDataRow("ycnumi").ToString().Trim)
            oRow("numi1") = CInt(oDataRow("ycyanumi").ToString.Trim)
            oRow("Programa") = oDataRow("yaprog").ToString().Trim
            oRow("Titulo") = oDataRow("yatit").ToString.Trim
            oRow("Show") = CBool(oDataRow("ycshow")) 'False ' oDataRow("IsKey")
            oRow("Add") = CBool(oDataRow("ycadd")) 'False ' oDataRow("IsKey")
            oRow("Mod") = CBool(oDataRow("ycmod")) 'False ' oDataRow("IsKey")
            oRow("Del") = CBool(oDataRow("ycdel")) 'False ' oDataRow("IsKey")
            oRow("Estado") = "Show"
        End If
        Return oRow
    End Function

    Private Sub EditarRow(ByRef oRow As DataRow)
        If CBool(JGr_Detalle.GetValue("Show")) <> CBool(oRow("Show")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If

        If CBool(JGr_Detalle.GetValue("Add")) <> CBool(oRow("Add")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If

        If CBool(JGr_Detalle.GetValue("Mod")) <> CBool(oRow("Mod")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If

        If CBool(JGr_Detalle.GetValue("Del")) <> CBool(oRow("Del")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If
    End Sub

    Private Sub DeleteRolDetalle()
        For Each oRow As DataRow In moDataTableTipoDato.Rows
            If oRow("Estado") <> "New" Then
                L_RolDetalle_Borrar(oRow("numi").ToString.Trim, oRow("numi1").ToString.Trim)
            End If
        Next
    End Sub
#End Region

#Region " Grid-Detalle "
    Private Sub grdIntTable()
        With JGr_Detalle
            .RootTable.Columns("numi").Visible = False
            .RootTable.Columns("numi1").Visible = False

            .RootTable.Columns("Programa").Caption = "Programa"
            .RootTable.Columns("Programa").Width = 150
            .RootTable.Columns("Programa").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("Programa").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Programa").EditType = Janus.Windows.GridEX.EditType.NoEdit

            .RootTable.Columns("Titulo").Caption = "Titulo"
            .RootTable.Columns("Titulo").Width = 150
            .RootTable.Columns("Titulo").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("Titulo").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Titulo").EditType = Janus.Windows.GridEX.EditType.NoEdit

            .RootTable.Columns("Show").Caption = "Show"
            .RootTable.Columns("Show").Width = 50
            .RootTable.Columns("Show").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Show").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Add").Caption = "Add"
            .RootTable.Columns("Add").Width = 50
            .RootTable.Columns("Add").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Add").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Mod").Caption = "Mod"
            .RootTable.Columns("Mod").Width = 50
            .RootTable.Columns("Mod").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Mod").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Del").Caption = "Del"
            .RootTable.Columns("Del").Width = 50
            .RootTable.Columns("Del").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Del").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Estado").Visible = False
        End With

        Dim fc As Janus.Windows.GridEX.GridEXFormatCondition

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(JGr_Detalle.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "Edit")
        fc.FormatStyle.BackColor = Color.LightYellow 'Color.LightGreen ' Color.DarkRed
        JGr_Detalle.RootTable.FormatConditions.Add(fc)

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(JGr_Detalle.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "New")
        fc.FormatStyle.BackColor = Color.LightGreen ' Color.DarkRed
        JGr_Detalle.RootTable.FormatConditions.Add(fc)

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(JGr_Detalle.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "Show")
        fc.FormatStyle.BackColor = Color.White 'Color.LightGreen ' Color.DarkRed
        JGr_Detalle.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub JGr_Detalle_CellUpdated(sender As Object, e As ColumnActionEventArgs) Handles JGr_Detalle.CellUpdated
        If moDataTableTipoDato.Rows.Count = JGr_Detalle.GetRows.Count Then
            EditarRow(moDataTableTipoDato.Rows(JGr_Detalle.GetRow.Position))
            Call grdIntTable()
        End If
    End Sub
#End Region
#End Region

#Region " Evento-Button "
#Region " Metodo-Button "
    Private Sub _PHabilitar()
        Tb_Nombre.Enabled = True

        BBtn_Nuevo.Enabled = False
        BBtn_Modificar.Enabled = False
        BBtn_Eliminar.Enabled = False
        BBtn_Grabar.Enabled = True

        Txt_Actualizar.Enabled = True
        ListB_Empleados.Enabled = True
        JGr_Detalle.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        Tb_Nombre.Text = String.Empty
        intModulo = 0

        Txt_Paginacion.Text = String.Empty
        moDataTableTipoDato.Rows.Clear()
        mdtnColeccion.Clear()
        JGr_Detalle.ClearStructure()
        
        Call _PCargarListModulo()
    End Sub

    Public Overrides Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        EP1.Clear()
        If Tb_Nombre.Text.Trim = String.Empty Then
            Tb_Nombre.BackColor = Color.Red
            EP1.SetError(Tb_Nombre, "Ingrese rol!")
            _Error = False
        Else
            Tb_Nombre.BackColor = Color.White
            EP1.SetError(Tb_Nombre, String.Empty)
        End If

        HighLigthter_Focus.UpdateHighlights()
        Return _Error
    End Function

    Private Sub Detalle_Grabar()
        Dim table As DataTable = ListB_Empleados.DataSource
        For Each oRow As DataRow In table.Rows
            If mdtnColeccion.ContainsKey(CInt(oRow("ceid"))) Then
                Dim Detalle As DataTable = mdtnColeccion.Item(CInt(oRow("ceid")))
                Dim numi1 As Integer
                Dim show, add, mod1, del As Boolean
                For Each oDataRow As DataRow In Detalle.Rows
                    If oDataRow("Estado") = "Edit" Then
                        numi1 = oDataRow("numi1")
                        show = oDataRow("Show")
                        add = oDataRow("Add")
                        mod1 = oDataRow("Mod")
                        del = oDataRow("Del")

                        L_RolDetalle_Modificar(Tb_Id.Text, numi1, show, add, mod1, del)
                    ElseIf oDataRow("Estado") = "New" Then
                        numi1 = oDataRow("numi1")
                        show = oDataRow("Show")
                        add = oDataRow("Add")
                        mod1 = oDataRow("Mod")
                        del = oDataRow("Del")

                        L_RolDetalle_Grabar(Tb_Id.Text, numi1, show, add, mod1, del)
                    End If
                Next
            End If
        Next
    End Sub
#End Region

#Region " Actualizar-Button "
    Private Sub Txt_Actualizar_Click(sender As Object, e As EventArgs) Handles Txt_Actualizar.Click
        If ListB_Empleados.SelectedIndex >= 0 Then 'selecciono el item
            Call DeleteRolDetalle()
            Call DataSetTable()

            Dim dtInsu As DataTable
            dtInsu = L_Formulario_General(1, " and ZY001.yamod=" & ListB_Empleados.SelectedValue).Tables(0)
            For Each campo As DataRow In dtInsu.Rows
                moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
            Next

            JGr_Detalle.DataSource = moDataTableTipoDato
            JGr_Detalle.RetrieveStructure()

            Call grdIntTable()
        End If
    End Sub
#End Region

#Region " Nuevo-Button "
    Private Sub BBtn_Nuevo_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Nuevo.Click
        _PNuevoRegistro()
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _PNuevoRegistro()
        _PHabilitar()
        BBtn_Nuevo.Enabled = True

        _PLimpiar()
        Tb_Nombre.Focus()
        _Nuevo = True
    End Sub
#End Region

#Region " Grabar-Button "
    Private Sub BBtn_Grabar_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Grabar.Click
        JGr_Detalle.UpdateData()
        If moDataTableTipoDato.Rows.Count > 0 Then
            mdtnColeccion.Remove(ListB_Empleados.SelectedValue) 'es solo en caso edit
            mdtnColeccion.Add(ListB_Empleados.SelectedValue, moDataTableTipoDato)
        End If
        _PGrabarRegistro()
    End Sub

    Private Sub _PGrabarRegistro()
        Dim _Error As Boolean = False
        If P_Validar() Then

            If BBtn_Grabar.Tag = 0 Then
                BBtn_Grabar.Tag = 1
                BubbleBar1.Refresh()
                Exit Sub
            Else
                BBtn_Grabar.Tag = 0
                BubbleBar1.Refresh()
            End If

            If _Nuevo Then
                L_Rol_Grabar(Tb_Id.Text, Tb_Nombre.Text)
                Call Detalle_Grabar()
              
                'actualizar el grid de buscador
                _PCargarBuscador()

                Tb_Nombre.Focus()
                ToastNotification.Show(Me, "Codigo Rol " + Tb_Id.Text + " Grabado con Exito.", My.Resources.grabar, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PLimpiar()
            Else
                L_Rol_Modificar(Tb_Id.Text, Tb_Nombre.Text)
                Call Detalle_Grabar()

                _PCargarBuscador()
                ToastNotification.Show(Me, "Codigo Rol " + Tb_Id.Text + " Modificado con Exito.", My.Resources.grabar, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                _Nuevo = False 'aumentado danny
                _PInhabilitar()
                _PFiltrar()

                intModulo = 0
                JGr_Detalle.ClearStructure()
                moDataTableTipoDato.Rows.Clear()
                mdtnColeccion.Clear()

                JGr_Detalle.Update()
                Call _PCargarListModulo()
            End If
        End If
    End Sub
#End Region

#Region " Cancelar-Button "
    Private Sub BBtn_Cancelar_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Cancelar.Click
        _PSalirRegistro()
    End Sub

    Private Sub _PSalirRegistro()
        _PLimpiar()
        _PInhabilitar()
        _PFiltrar()
    End Sub
#End Region

#Region " Modificar-Button "
    Private Sub BBtn_Modificar_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Modificar.Click
        _PModificarRegistro()
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _PModificarRegistro()
        _Nuevo = False
        _PHabilitar()
        BBtn_Modificar.Enabled = True 'aumentado para q funcione con el modelo de guido
    End Sub
#End Region

#Region " Eliminar-Button "
    Private Sub BBtn_Eliminar_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Eliminar.Click
        _PEliminarRegistro()
    End Sub

    Private Sub _PEliminarRegistro()
        Dim _Result As MsgBoxResult
        _Result = MsgBox("Esta seguro de Eliminar el Registro?", MsgBoxStyle.YesNo, "Advertencia")
        If _Result = MsgBoxResult.Yes Then
            L_Rol_Borrar(Tb_Id.Text)
            'borro el detalle del encabezado
            L_RolDetalle_Borrar(Tb_Id.Text)

            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()
        Else
            _PInhabilitar()
        End If
    End Sub
#End Region
#End Region

    Private Sub BBtn_Inicio_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Inicio.Click
        _PPrimerRegistro()
    End Sub

    Private Sub _PPrimerRegistro()
        _Pos = 0
        _PMostrarRegistro(0)
        Txt_Paginacion.Text = Str(1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    End Sub

    Private Sub BBtn_Ultimo_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Ultimo.Click
        _PUltimoRegistro()
    End Sub

    Private Sub _PUltimoRegistro()
        _Pos = _Dsencabezado.Tables(0).Rows.Count - 1
        _PMostrarRegistro(_Dsencabezado.Tables(0).Rows.Count - 1)
        Txt_Paginacion.Text = Str(_Dsencabezado.Tables(0).Rows.Count) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    End Sub

    Private Sub BBtn_Anterior_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Anterior.Click
        _PAnteriorRegistro()
    End Sub

    Private Sub _PAnteriorRegistro()
        If _Pos > 0 Then
            _Pos = _Pos - 1
            _PMostrarRegistro(_Pos)
            Txt_Paginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub BBtn_Siguiente_Click(sender As Object, e As ClickEventArgs) Handles BBtn_Siguiente.Click
        _PSiguienteRegistro()
    End Sub

    Private Sub _PSiguienteRegistro()
        If _Pos < _Dsencabezado.Tables(0).Rows.Count - 1 Then
            _Pos = _Pos + 1
            _PMostrarRegistro(_Pos)
            Txt_Paginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub
End Class