Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

'DESARROLLADO POR: DANNY GUTIERREZ
Public Class F0_Usuarios

#Region "ATRIBUTOS"
    Dim _Dsencabezado As DataSet
    Dim _Nuevo As Boolean
    Private _Pos As Integer
    Public _tab As SuperTabItem
    Public _nameButton As String
    Public _modulo As SideNavItem
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _PIniciarTodo()

        Me.Text = "U S U A R I O S"
        Me.WindowState = FormWindowState.Maximized

        _PCargarComboRol(JMC_Categoria)
        _prCargarComboSucursal()
        _prCargarComboAlmacen(cbSucursal)
        _PFiltrar()
        _PInhabilitar()

        _PHabilitarFocus()

        _PCargarBuscador()

        _pCambiarFuente()

        _prAsignarPermisos()
        Dim blah As New Bitmap(New Bitmap(My.Resources.man_18), 20, 20)


        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
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

    Private Sub _pCambiarFuente()
        'Dim fuente As New Font("Tahoma", gi_fuenteTamano, FontStyle.Regular)
        'Dim xCtrl As Control
        'For Each xCtrl In PanelEx3.Controls
        '    Try
        '        xCtrl.Font = fuente
        '    Catch ex As Exception
        '    End Try
        'Next
        'For Each xCtrl In PanelEx4.Controls
        '    Try
        '        xCtrl.Font = fuente
        '    Catch ex As Exception
        '    End Try
        'Next

    End Sub

    Private Sub _PCargarComboRol(ByVal cb As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim _Ds As New DataSet
        _Ds = L_Rol_General(0)

        cb.DropDownList.Columns.Clear()

        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("ybnumi").ToString).Width = 50
        cb.DropDownList.Columns(0).Caption = "Código"
        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("ybrol").ToString).Width = 250
        cb.DropDownList.Columns(1).Caption = "Rol"

        cb.ValueMember = _Ds.Tables(0).Columns("ybnumi").ToString
        cb.DisplayMember = _Ds.Tables(0).Columns("ybrol").ToString
        cb.DataSource = _Ds.Tables(0)
        cb.Refresh()


    End Sub

    Private Sub _prCargarComboSucursal()
        Dim dt As New DataTable
        dt = L_prEmpresaAyuda()

        With cbEmp
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
    End Sub
    Private Sub _prCargarComboAlmacen(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarAlmacenDosificacion()
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
    End Sub
    Private Sub _PFiltrar()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Usuario_General2(0)
        _Pos = 0

        If _Dsencabezado.Tables(0).Rows.Count <> 0 Then
            _PMostrarRegistro(0)
            LblPaginacion.Text = Str(1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
            If _Dsencabezado.Tables(0).Rows.Count > 0 Then
                btnPrimero.Visible = True
                btnAnterior.Visible = True
                btnSiguiente.Visible = True
                btnUltimo.Visible = True
            End If
        End If
    End Sub

    Private Sub _PMostrarRegistro(_N As Integer)
        With _Dsencabezado.Tables(0).Rows(_N)
            Tb_Id.Text = .Item("ydnumi").ToString
            Tb_Nombre.Text = .Item("yduser").ToString
            TextBoxX1.Text = .Item("ydpass").ToString
            Tb_Estado.Value = CBool(.Item("ydest").ToString)
            tbTodasSucursales.Value = CBool(IIf(.Item("ydall") = 1, True, False))
            Tb_DiasPedidos.Value = CInt(.Item("ydcant").ToString)
            Tb_fuenteTam.Value = CInt(.Item("ydfontsize").ToString)
            JMC_Categoria.Value = CInt(.Item("ybnumi").ToString)
            cbEmp.Value = CInt(.Item("ydemp").ToString)
            cbSucursal.Value = CInt(.Item("ydsuc").ToString)
        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.Enabled = False
        Tb_Nombre.Enabled = False
        TextBoxX1.Enabled = False
        Tb_DiasPedidos.Enabled = False
        Tb_fuenteTam.Enabled = False
        cbEmp.ReadOnly = True
        cbSucursal.ReadOnly = True
        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False

        JMC_Categoria.Enabled = False
        Tb_Estado.Enabled = False
        tbTodasSucursales.Enabled = False
        JGr_Buscador.Enabled = True

        btnGrabar.Image = My.Resources.GUARDAR

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        MEP.Clear()
        Tb_Nombre.BackColor = Color.White
        TextBoxX1.BackColor = Color.White
        Tb_DiasPedidos.BackColor = Color.White
        Tb_fuenteTam.BackColor = Color.White
        JMC_Categoria.BackColor = Color.White
        cbEmp.BackColor = Color.White
    End Sub

    Private Sub _PHabilitarFocus()
        MHighlighterFocus.SetHighlightOnFocus(Tb_Nombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(TextBoxX1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(cbEmp, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        Tb_Nombre.TabIndex = 1
        TextBoxX1.TabIndex = 2
        JMC_Categoria.TabIndex = 3
        Tb_Estado.TabIndex = 4
        Tb_DiasPedidos.TabIndex = 5
        Tb_fuenteTam.TabIndex = 6
    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Usuario_General2(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("ydnumi")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("yduser")
            '   .Visible = False
            .Caption = "Usuario"
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
        End With

        With JGr_Buscador.RootTable.Columns("ydpass")
            .Visible = False
            '.Caption = "Contraseña"
            '.Width = 150
            '.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.FontSize = gi_fuenteTamano
        End With

        With JGr_Buscador.RootTable.Columns("ydest")
            .Caption = "Estado"
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
        End With

        With JGr_Buscador.RootTable.Columns("ybnumi")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("ydcant")
            .Caption = "Dias Venc."
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .CellStyle.FontSize = gi_userFuente
        End With

        With JGr_Buscador.RootTable.Columns("ydfontsize")
            .Caption = "Tam. Fuente"
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .CellStyle.FontSize = gi_userFuente
        End With

        With JGr_Buscador.RootTable.Columns("ybrol")
            .Caption = "Rol"
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
        End With

        With JGr_Buscador.RootTable.Columns("ydsuc")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("ydall")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("cedesc")
            .Caption = "Empresa"
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
        End With


        With JGr_Buscador.RootTable.Columns("cadesc")
            .Caption = "Sucursal"
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
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

#End Region

    Private Sub F0_Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()
    End Sub


#Region " Evento-Button "
#Region " Metodo-Button "
    Private Sub _PHabilitar()
        Tb_Nombre.Enabled = True
        TextBoxX1.Enabled = True
        JMC_Categoria.Enabled = True
        Tb_Estado.Enabled = True
        tbTodasSucursales.Enabled = True
        Tb_DiasPedidos.Enabled = True
        Tb_fuenteTam.Enabled = True
        cbEmp.ReadOnly = False
        cbSucursal.ReadOnly = False

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        Tb_Nombre.Text = String.Empty
        TextBoxX1.Text = String.Empty
        JMC_Categoria.Value = Nothing
        JMC_Categoria.SelectedIndex = -1
        Tb_Estado.Value = True
        tbTodasSucursales.Value = False
        Tb_DiasPedidos.Value = 0
        Tb_fuenteTam.Value = 7
        If (CType(cbEmp.DataSource, DataTable).Rows.Count > 0) Then
            cbEmp.SelectedIndex = 0
        End If
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        LblPaginacion.Text = String.Empty
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()
        If Tb_Nombre.Text.Trim = String.Empty Then
            Tb_Nombre.BackColor = Color.Red
            MEP.SetError(Tb_Nombre, "Ingrese usuario!".ToUpper)
            _Error = False
        Else
            Tb_Nombre.BackColor = Color.White
            MEP.SetError(Tb_Nombre, String.Empty)
        End If

        If TextBoxX1.Text.Trim = String.Empty Then
            TextBoxX1.BackColor = Color.Red
            MEP.SetError(TextBoxX1, "Ingrese contraseña!".ToUpper)
            _Error = False
        Else
            TextBoxX1.BackColor = Color.White
            MEP.SetError(TextBoxX1, String.Empty)
        End If

        If JMC_Categoria.SelectedIndex < 0 Then
            JMC_Categoria.BackColor = Color.Red   'error de validacion
            MEP.SetError(JMC_Categoria, "Seleccione un rol!".ToUpper)
            _Error = False
        Else
            JMC_Categoria.BackColor = Color.White
            MEP.SetError(JMC_Categoria, String.Empty)
        End If

        If cbEmp.SelectedIndex < 0 Then
            cbEmp.BackColor = Color.Red   'error de validacion
            MEP.SetError(cbEmp, "Seleccione empresa!".ToUpper)
            _Error = False
        Else
            cbEmp.BackColor = Color.White
            MEP.SetError(cbEmp, String.Empty)
        End If

        If Tb_DiasPedidos.Text.Trim = String.Empty Then
            Tb_DiasPedidos.BackColor = Color.Red
            MEP.SetError(Tb_DiasPedidos, "Ingrese dias vencimiento!".ToUpper)
            _Error = False
        Else
            Tb_DiasPedidos.BackColor = Color.White
            MEP.SetError(Tb_DiasPedidos, String.Empty)
        End If

        If Tb_fuenteTam.Text.Trim = String.Empty Then
            Tb_fuenteTam.BackColor = Color.Red
            MEP.SetError(Tb_fuenteTam, "Ingrese el tamaño de fuente!".ToUpper)
            _Error = False
        Else
            Tb_fuenteTam.BackColor = Color.White
            MEP.SetError(Tb_fuenteTam, String.Empty)
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _Error
    End Function
#End Region

#Region " Nuevo-Button "
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _PNuevoRegistro()
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _PNuevoRegistro()
        _PHabilitar()
        'btnNuevo.Enabled = True

        _PLimpiar()
        Tb_Nombre.Focus()
        _Nuevo = True
    End Sub

#Region " Grabar-Button "
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        _PGrabarRegistro()
    End Sub

    Private Sub _PGrabarRegistro()
        Dim _Error As Boolean = False
        If P_Validar() Then

            If False Then 'btnGrabar.Tag = 0
                btnGrabar.Tag = 1
                btnGrabar.Refresh()
                Exit Sub
            Else
                btnGrabar.Tag = 0
                btnGrabar.Refresh()
            End If

            If _Nuevo Then
                L_Usuario_Grabar(Tb_Id.Text, Tb_Nombre.Text, TextBoxX1.Text, JMC_Categoria.Value, Tb_Estado.Value, Tb_DiasPedidos.Value, Tb_fuenteTam.Value, cbSucursal.Value, IIf(tbTodasSucursales.Value = True, "1", "0"), cbEmp.Value)

                'actualizar el grid de buscador
                _PCargarBuscador()

                Tb_Nombre.Focus()
                ToastNotification.Show(Me, "Codigo Usuario ".ToUpper + Tb_Id.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PLimpiar()
            Else
                L_Usuario_Modificar(Tb_Id.Text, Tb_Nombre.Text, TextBoxX1.Text, JMC_Categoria.Value, Tb_Estado.Value, Tb_DiasPedidos.Value, Tb_fuenteTam.Value, cbSucursal.Value, IIf(tbTodasSucursales.Value = True, "1", "0"), cbEmp.Value)

                _PCargarBuscador()
                ToastNotification.Show(Me, "Codigo Usuario ".ToUpper + Tb_Id.Text + " Modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                _Nuevo = False 'aumentado danny
                _PInhabilitar()
                _PFiltrar()
            End If
        End If
    End Sub
#End Region

#Region " Cancelar-Button "
    Private Sub BBtn_Cancelar_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PLimpiar()
            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()
        Else
            _modulo.Select()
            Me.Close()
            _tab.Close()

        End If
    End Sub
#End Region

#Region " Modificar-Button "
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _PModificarRegistro()
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _PModificarRegistro()
        _Nuevo = False
        _PHabilitar()
        'btnModificar.Enabled = True 'aumentado para q funcione con el modelo de guido
    End Sub
#End Region

#Region " Eliminar-Button "
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        _PEliminarRegistro()
    End Sub

    Private Sub _PEliminarRegistro()
        Dim _Result As MsgBoxResult
        _Result = MsgBox("Esta seguro de Eliminar el Registro?", MsgBoxStyle.YesNo, "Advertencia")
        If _Result = MsgBoxResult.Yes Then
            L_Usuario_Borrar(Tb_Id.Text)

            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()
        Else
            _PInhabilitar()
        End If
    End Sub
#End Region
#End Region
#End Region

    Private Sub BBtn_Inicio_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PPrimerRegistro()
    End Sub

    Private Sub _PPrimerRegistro()
        _Pos = 0
        _PMostrarRegistro(0)
        LblPaginacion.Text = Str(1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    End Sub

    Private Sub BBtn_Ultimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        _PUltimoRegistro()
    End Sub

    Private Sub _PUltimoRegistro()
        _Pos = _Dsencabezado.Tables(0).Rows.Count - 1
        _PMostrarRegistro(_Dsencabezado.Tables(0).Rows.Count - 1)
        LblPaginacion.Text = Str(_Dsencabezado.Tables(0).Rows.Count) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    End Sub

    Private Sub BBtn_Anterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        _PAnteriorRegistro()
    End Sub

    Private Sub _PAnteriorRegistro()
        If _Pos > 0 Then
            _Pos = _Pos - 1
            _PMostrarRegistro(_Pos)
            LblPaginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub BBtn_Siguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        _PSiguienteRegistro()
    End Sub

    Private Sub _PSiguienteRegistro()
        If _Pos < _Dsencabezado.Tables(0).Rows.Count - 1 Then
            _Pos = _Pos + 1
            _PMostrarRegistro(_Pos)
            LblPaginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub JGr_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles JGr_Buscador.SelectionChanged
        If JGr_Buscador.Row >= 0 Then
            _PMostrarRegistro(JGr_Buscador.Row)
            LblPaginacion.Text = Str(JGr_Buscador.Row + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub JGr_Buscador_EditingCell(sender As Object, e As EditingCellEventArgs) Handles JGr_Buscador.EditingCell
        e.Cancel = True
    End Sub

    Private Function SuperTabControl1() As Object
        Throw New NotImplementedException
    End Function

    Private Function SuperTabControlPanel1() As Object
        Throw New NotImplementedException
    End Function

End Class