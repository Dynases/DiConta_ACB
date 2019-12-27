Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar

Public Class P_Formularios
    Dim _Dsencabezado As DataSet
    Private _Nuevo As Boolean
    Private _Pos As Integer

    Private Sub P_Roles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()
    End Sub

    Private Sub _PIniciarTodo()
        L_abrirConexion()

        Me.Text = "F O R M U L A R I O S"
        Me.WindowState = FormWindowState.Maximized

        _PCargarComboModulo(JMC_Categoria, 11)

        _PFiltrar()

        _PInhabilitar()

        _PHabilitarFocus()

        _PCargarBuscador()
    End Sub

    Private Sub _PCargarComboModulo(ByVal cb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal concep As Integer)
        Dim _Ds As New DataSet
        _Ds = L_General_LibreriaDetalle(-1, concep)

        cb.DropDownList.Columns.Clear()

        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("cecon").ToString).Width = 50
        cb.DropDownList.Columns(0).Caption = "Código"
        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("cedesc").ToString).Width = 250
        cb.DropDownList.Columns(1).Caption = "Descripcion"

        cb.ValueMember = _Ds.Tables(0).Columns("ceid").ToString
        cb.DisplayMember = _Ds.Tables(0).Columns("cedesc").ToString
        cb.DataSource = _Ds.Tables(0)
        cb.Refresh()
    End Sub

    Private Sub _PFiltrar()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Formulario_General(0)
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
            Tb_Id.Text = .Item("yanumi").ToString
            Tb_Nombre.Text = .Item("yaprog").ToString
            Tb_Direccion.Text = .Item("yatit").ToString

            JMC_Categoria.Value = CInt(.Item("yamod").ToString)
        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.Enabled = False
        Tb_Nombre.Enabled = False
        Tb_Direccion.Enabled = False

        BBtn_Nuevo.Enabled = True
        BBtn_Modificar.Enabled = True
        BBtn_Eliminar.Enabled = True
        BBtn_Grabar.Enabled = False

        JMC_Categoria.Enabled = False
        JGr_Buscador.Enabled = True

        BBtn_Grabar.Image = My.Resources.grabar
        BBtn_Grabar.ImageLarge = My.Resources.grabar

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        EP1.Clear()
        Tb_Nombre.BackColor = Color.White
        Tb_Direccion.BackColor = Color.White
        JMC_Categoria.BackColor = Color.White
    End Sub

    Private Sub _PHabilitarFocus()
        HighLigthter_Focus.SetHighlightOnFocus(Tb_Nombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        HighLigthter_Focus.SetHighlightOnFocus(Tb_Direccion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        Tb_Nombre.TabIndex = 1
        Tb_Direccion.TabIndex = 2
        JMC_Categoria.TabIndex = 3
    End Sub

    Private Sub _PCargarBuscador()
        '_Dsencabezado = New DataSet
        '_Dsencabezado = L_Formulario_General(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("yaprog")
            .Caption = "Programa"
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 7
        End With

        With JGr_Buscador.RootTable.Columns("yatit")
            .Caption = "Titulo"
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 7
        End With

        With JGr_Buscador.RootTable.Columns("yanumi")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("yamod")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("cedesc")
            .Caption = "Modulo"
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

#Region " Evento-Button "
#Region " Metodo-Button "
    Private Sub _PHabilitar()
        Tb_Nombre.Enabled = True
        Tb_Direccion.Enabled = True
        JMC_Categoria.Enabled = True

        BBtn_Nuevo.Enabled = False
        BBtn_Modificar.Enabled = False
        BBtn_Eliminar.Enabled = False
        BBtn_Grabar.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        Tb_Nombre.Text = String.Empty
        Tb_Direccion.Text = String.Empty
        JMC_Categoria.Value = Nothing
        JMC_Categoria.SelectedIndex = -1

        Txt_Paginacion.Text = String.Empty
    End Sub

    Public Overrides Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        EP1.Clear()
        If Tb_Nombre.Text.Trim = String.Empty Then
            Tb_Nombre.BackColor = Color.Red
            EP1.SetError(Tb_Nombre, "Ingrese nombre del programa!")
            _Error = False
        Else
            Tb_Nombre.BackColor = Color.White
            EP1.SetError(Tb_Nombre, String.Empty)
        End If

        If Tb_Direccion.Text.Trim = String.Empty Then
            Tb_Direccion.BackColor = Color.Red
            EP1.SetError(Tb_Direccion, "Ingrese titulo!")
            _Error = False
        Else
            Tb_Direccion.BackColor = Color.White
            EP1.SetError(Tb_Direccion, String.Empty)
        End If

        If JMC_Categoria.SelectedIndex < 0 Then
            JMC_Categoria.BackColor = Color.Red   'error de validacion
            EP1.SetError(JMC_Categoria, "Seleccione un modulo!")
            _Error = False
        Else
            JMC_Categoria.BackColor = Color.White
            EP1.SetError(JMC_Categoria, String.Empty)
        End If

        HighLigthter_Focus.UpdateHighlights()
        Return _Error
    End Function
#End Region

#Region " Nuevo-Button "
    Private Sub BBtn_Nuevo_Click(sender As Object, e As DevComponents.DotNetBar.ClickEventArgs) Handles BBtn_Nuevo.Click
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
    Private Sub BBtn_Grabar_Click(sender As Object, e As DevComponents.DotNetBar.ClickEventArgs) Handles BBtn_Grabar.Click
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
                L_Formulario_Grabar(Tb_Id.Text, Tb_Nombre.Text, Tb_Direccion.Text, JMC_Categoria.Value)

                'actualizar el grid de buscador
                _PFiltrar()
                _PCargarBuscador()

                Tb_Nombre.Focus()
                ToastNotification.Show(Me, "Codigo Rol " + Tb_Id.Text + " Grabado con Exito.", My.Resources.grabar, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PLimpiar()
            Else
                L_Formulario_Modificar(Tb_Id.Text, Tb_Nombre.Text, Tb_Direccion.Text, JMC_Categoria.Value)

                ToastNotification.Show(Me, "Codigo Rol " + Tb_Id.Text + " Modificado con Exito.", My.Resources.grabar, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                _Nuevo = False 'aumentado danny
                _PInhabilitar()
                _PFiltrar()
                _PCargarBuscador()
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
        _PCargarBuscador()
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
            L_Formulario_Borrar(Tb_Id.Text)

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

    Private Sub JGr_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles JGr_Buscador.SelectionChanged
        If JGr_Buscador.Row >= 0 Then
            _PMostrarRegistro(JGr_Buscador.Row)
            Txt_Paginacion.Text = Str(JGr_Buscador.Row + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub JGr_Buscador_EditingCell(sender As Object, e As EditingCellEventArgs) Handles JGr_Buscador.EditingCell
        e.Cancel = True
    End Sub

End Class