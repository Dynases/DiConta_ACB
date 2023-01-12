Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class F0_TipoCambio
    Public _nameButton As String
    Public _tab As SuperTabItem
    Private _Nuevo As Boolean
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        Me.Text = "t i p o    d e    c a m b i o".ToUpper
        TxtNombreUsu.Text = P_Global.gs_user
        TxtNombreUsu.ReadOnly = True

        Me.WindowState = FormWindowState.Maximized
        Me.SupTabItemBusqueda.Visible = False

        _prCargarGridDetalle()
        _prInhabilitar()

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

    Private Sub _prHabilitar()
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True

    End Sub
    Private Sub _prInhabilitar()
        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False

        grDetalle.AllowEdit = InheritableBoolean.False
    End Sub
    Private Sub _prCargarGridDetalle()
        Dim dt As New DataTable
        dt = L_prTipoCambioGeneral()

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("cbnumi")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cbfecha")
            .Caption = "FECHA"
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With

        With grDetalle.RootTable.Columns("cbdol")
            .Caption = "DOLAR"
            .Width = 100
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With

        With grDetalle.RootTable.Columns("cbufv")
            .Caption = "UFV"
            .Width = 100
            .FormatString = "0.00000"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With

        With grDetalle.RootTable.Columns("estado")
            .Visible = False
        End With

        With grDetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .AllowAddNew = InheritableBoolean.False
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalle.RootTable.Columns("estado"), ConditionOperator.Equal, 1)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grDetalle.RootTable.FormatConditions.Add(fc)

    End Sub
    Private Sub _prGrabarRegistro(_fecha As String, _dolar As String, _ufv As String)
        Dim numi As String = ""
        Dim res As Boolean = L_prTipoCambioGrabar(numi, _fecha, _dolar, _ufv)
        If res Then
            ToastNotification.Show(Me, "Registro ".ToUpper + numi + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _prCargarGridDetalle()
            _prInhabilitar()
        End If
    End Sub
    Private Sub _prModificarRegistro(_detalle As DataTable)
        Dim numi As String = ""
        Dim res As Boolean = L_prTipoCambioModificarTodo(_detalle)
        If res Then
            ToastNotification.Show(Me, "Registros ".ToUpper + "modificados con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _prSalir()
        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled Then
            _prCargarGridDetalle()
            _prInhabilitar()
        Else
            _modulo.Select()
            _tab.Close()
        End If

    End Sub

    Private Sub _prEliminarRegistro()
        Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim numi As String = grDetalle.GetValue("cbnumi")
            Dim res As Boolean = L_prTipoCambioBorrar(numi, grDetalle.GetValue("cbfecha"), grDetalle.GetValue("cbdol"), grDetalle.GetValue("cbufv"), mensajeError)
            If res Then
                ToastNotification.Show(Me, "registro ".ToUpper + numi + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _prCargarGridDetalle()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
#End Region

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        grDetalle.AllowAddNew = InheritableBoolean.True
        _prHabilitar()

        grDetalle.Focus()
        grDetalle.MoveToNewRecord()
        grDetalle.Col = 0
        _Nuevo = True
    End Sub

    Private Sub F0_TipoCambio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub


    Private Sub grDetalle_RecordAdded(sender As Object, e As EventArgs) Handles grDetalle.RecordAdded
        Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
        With dt.Rows(dt.Rows.Count - 1)
            _prGrabarRegistro(.Item("cbfecha"), .Item("cbdol"), .Item("cbufv"))
        End With
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _Nuevo = True Then
            Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
            With dt.Rows(dt.Rows.Count - 1)
                _prGrabarRegistro(.Item("cbfecha"), .Item("cbdol"), .Item("cbufv"))
            End With
        Else 'en el caso de modificar
            Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
            _prModificarRegistro(dt)
        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _prHabilitar()
        grDetalle.AllowEdit = InheritableBoolean.True
        _Nuevo = False
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited
        grDetalle.SetValue("estado", 2)
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        _prEliminarRegistro()
    End Sub
End Class