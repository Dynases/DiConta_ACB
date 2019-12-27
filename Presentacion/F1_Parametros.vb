Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar.Controls

Public Class F1_Parametros
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()
        Me.Text = "c o n f i g u r a c i o n".ToUpper
        _prCargarComboEmpresa()


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

    End Sub

    Private Sub _prCargarGridCobrar(numi As String)
        Dim dt As New DataTable
        dt = L_prConfigDetalleGeneral(numi, 1)

        grCuentasCobrar.DataSource = dt
        grCuentasCobrar.RetrieveStructure()

        'dar formato a las columnas
        With grCuentasCobrar.RootTable.Columns("cinumi")
            .Width = 50
            .Visible = False
        End With
        With grCuentasCobrar.RootTable.Columns("cinumitc6")
            .Width = 50
            .Visible = False
        End With

        With grCuentasCobrar.RootTable.Columns("cinumitc1")
            .Width = 50
            .Visible = False
        End With

        With grCuentasCobrar.RootTable.Columns("cacta")
            .Caption = "COD"
            .Width = 100
            .EditType = EditType.NoEdit
        End With

        With grCuentasCobrar.RootTable.Columns("cadesc")
            .Caption = "CUENTA"
            .Width = 300
            .EditType = EditType.NoEdit

        End With
        With grCuentasCobrar.RootTable.Columns("citipo")
            .Width = 50
            .DefaultValue = 1
            .Visible = False
        End With

        With grCuentasCobrar.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grCuentasCobrar
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grCuentasCobrar.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grCuentasCobrar.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub _prCargarGridPagar(numi As String)
        Dim dt As New DataTable
        dt = L_prConfigDetalleGeneral(numi, 2)

        grCuentasPagar.DataSource = dt
        grCuentasPagar.RetrieveStructure()

        'dar formato a las columnas
        With grCuentasPagar.RootTable.Columns("cinumi")
            .Width = 50
            .Visible = False
        End With
        With grCuentasPagar.RootTable.Columns("cinumitc6")
            .Width = 50
            .Visible = False
        End With

        With grCuentasPagar.RootTable.Columns("cinumitc1")
            .Width = 50
            .Visible = False
        End With

        With grCuentasPagar.RootTable.Columns("cacta")
            .Caption = "COD"
            .Width = 100
            .EditType = EditType.NoEdit
        End With

        With grCuentasPagar.RootTable.Columns("cadesc")
            .Caption = "CUENTA"
            .Width = 300
            .EditType = EditType.NoEdit

        End With
        With grCuentasPagar.RootTable.Columns("citipo")
            .Width = 50
            .DefaultValue = 2
            .Visible = False
        End With

        With grCuentasPagar.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grCuentasPagar
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grCuentasPagar.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grCuentasPagar.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub _prCargarGridCompras(numi As String)
        Dim dt As New DataTable
        dt = L_prConfigDetalleGeneral(numi, 3)

        grCuentasCompra.DataSource = dt
        grCuentasCompra.RetrieveStructure()

        'dar formato a las columnas
        With grCuentasCompra.RootTable.Columns("cinumi")
            .Width = 50
            .Visible = False
        End With
        With grCuentasCompra.RootTable.Columns("cinumitc6")
            .Width = 50
            .Visible = False
        End With

        With grCuentasCompra.RootTable.Columns("cinumitc1")
            .Width = 50
            .Visible = False
        End With

        With grCuentasCompra.RootTable.Columns("cacta")
            .Caption = "COD"
            .Width = 100
            .EditType = EditType.NoEdit
        End With

        With grCuentasCompra.RootTable.Columns("cadesc")
            .Caption = "CUENTA"
            .Width = 300
            .EditType = EditType.NoEdit

        End With
        With grCuentasCompra.RootTable.Columns("citipo")
            .Width = 50
            .DefaultValue = 3
            .Visible = False
        End With

        With grCuentasCompra.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grCuentasCompra
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grCuentasCompra.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grCuentasCompra.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub _prCargarGridIngresos(numi As String)
        Dim dt As New DataTable
        dt = L_prConfigDetalleGeneral(numi, 4)

        grIngreso.DataSource = dt
        grIngreso.RetrieveStructure()

        'dar formato a las columnas
        With grIngreso.RootTable.Columns("cinumi")
            .Width = 50
            .Visible = False
        End With
        With grIngreso.RootTable.Columns("cinumitc6")
            .Width = 50
            .Visible = False
        End With

        With grIngreso.RootTable.Columns("cinumitc1")
            .Width = 50
            .Visible = False
        End With

        With grIngreso.RootTable.Columns("cacta")
            .Caption = "COD"
            .Width = 100
            .EditType = EditType.NoEdit
        End With

        With grIngreso.RootTable.Columns("cadesc")
            .Caption = "CUENTA"
            .Width = 300
            .EditType = EditType.NoEdit

        End With
        With grIngreso.RootTable.Columns("citipo")
            .Width = 50
            .DefaultValue = 4
            .Visible = False
        End With

        With grIngreso.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grIngreso
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grIngreso.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grIngreso.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub _prCargarGridEgresos(numi As String)
        Dim dt As New DataTable
        dt = L_prConfigDetalleGeneral(numi, 5)

        grEgreso.DataSource = dt
        grEgreso.RetrieveStructure()

        'dar formato a las columnas
        With grEgreso.RootTable.Columns("cinumi")
            .Width = 50
            .Visible = False
        End With
        With grEgreso.RootTable.Columns("cinumitc6")
            .Width = 50
            .Visible = False
        End With

        With grEgreso.RootTable.Columns("cinumitc1")
            .Width = 50
            .Visible = False
        End With

        With grEgreso.RootTable.Columns("cacta")
            .Caption = "COD"
            .Width = 100
            .EditType = EditType.NoEdit
        End With

        With grEgreso.RootTable.Columns("cadesc")
            .Caption = "CUENTA"
            .Width = 300
            .EditType = EditType.NoEdit

        End With
        With grEgreso.RootTable.Columns("citipo")
            .Width = 50
            .DefaultValue = 5
            .Visible = False
        End With

        With grEgreso.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grEgreso
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grEgreso.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grEgreso.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub _prEliminarFilaDetallePagar()
        If grCuentasPagar.Row >= 0 Then

            Dim estado As Integer = grCuentasPagar.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grCuentasPagar.GetRow(grCuentasPagar.Row).BeginEdit()
                grCuentasPagar.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grCuentasPagar.GetRow(grCuentasPagar.Row).BeginEdit()
                grCuentasPagar.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grCuentasPagar.RemoveFilters()
            grCuentasPagar.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grCuentasPagar.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))


        End If
    End Sub
    Private Sub _prEliminarFilaDetalleCobrar()
        If grCuentasCobrar.Row >= 0 Then

            Dim estado As Integer = grCuentasCobrar.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grCuentasCobrar.GetRow(grCuentasCobrar.Row).BeginEdit()
                grCuentasCobrar.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grCuentasCobrar.GetRow(grCuentasCobrar.Row).BeginEdit()
                grCuentasCobrar.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grCuentasCobrar.RemoveFilters()
            grCuentasCobrar.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grCuentasCobrar.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))


        End If
    End Sub
    Private Sub _prEliminarFilaDetalleComprar()
        If grCuentasCompra.Row >= 0 Then

            Dim estado As Integer = grCuentasCompra.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grCuentasCompra.GetRow(grCuentasCompra.Row).BeginEdit()
                grCuentasCompra.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grCuentasCompra.GetRow(grCuentasCompra.Row).BeginEdit()
                grCuentasCompra.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grCuentasCompra.RemoveFilters()
            grCuentasCompra.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grCuentasCompra.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))


        End If
    End Sub

    Private Sub _prEliminarFilaDetalleIngreso()
        If grIngreso.Row >= 0 Then

            Dim estado As Integer = grIngreso.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grIngreso.GetRow(grIngreso.Row).BeginEdit()
                grIngreso.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grIngreso.GetRow(grIngreso.Row).BeginEdit()
                grIngreso.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grIngreso.RemoveFilters()
            grIngreso.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grIngreso.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))


        End If
    End Sub

    Private Sub _prEliminarFilaDetalleEgreso()
        If grEgreso.Row >= 0 Then

            Dim estado As Integer = grEgreso.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grEgreso.GetRow(grEgreso.Row).BeginEdit()
                grEgreso.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grEgreso.GetRow(grEgreso.Row).BeginEdit()
                grEgreso.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grEgreso.RemoveFilters()
            grEgreso.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grEgreso.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))


        End If
    End Sub
#End Region

#Region "METODOS SOBRECARGADOS"
    Public Overrides Sub _PMOHabilitar()
        'tbCuenta1.ReadOnly = False
        'tbCuenta2.ReadOnly = False
        'tbCuenta3.ReadOnly = False
        'tbCuenta4.ReadOnly = False
        tbEmpresa.ReadOnly = False
        tbMaxAjuste.IsInputReadOnly = False

        grCuentasCobrar.AllowAddNew = InheritableBoolean.True
        grCuentasCobrar.AllowEdit = InheritableBoolean.True
        grCuentasCobrar.ContextMenuStrip = ContextMenuStripCobrar


        grCuentasPagar.AllowAddNew = InheritableBoolean.True
        grCuentasPagar.AllowEdit = InheritableBoolean.True
        grCuentasPagar.ContextMenuStrip = ContextMenuStripPagar


        grCuentasCompra.AllowAddNew = InheritableBoolean.True
        grCuentasCompra.AllowEdit = InheritableBoolean.True
        grCuentasCompra.ContextMenuStrip = ContextMenuStripCompra

        grIngreso.AllowAddNew = InheritableBoolean.True
        grIngreso.AllowEdit = InheritableBoolean.True
        grIngreso.ContextMenuStrip = ContextMenuIngreso

        grEgreso.AllowAddNew = InheritableBoolean.True
        grEgreso.AllowEdit = InheritableBoolean.True
        grEgreso.ContextMenuStrip = ContextMenuEgreso

    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbCuenta1.ReadOnly = True
        tbCuenta2.ReadOnly = True
        tbCuenta3.ReadOnly = True
        tbCuenta4.ReadOnly = True
        tbEmpresa.ReadOnly = True
        tbMaxAjuste.IsInputReadOnly = True

        tbCuentaCobDebe.ReadOnly = True
        tbCuentaCobHaber.ReadOnly = True
        tbCuentaPagDebe.ReadOnly = True
        tbCuentaPagHaber.ReadOnly = True

        grCuentasCobrar.AllowAddNew = InheritableBoolean.False
        grCuentasCobrar.AllowEdit = InheritableBoolean.False
        grCuentasCobrar.ContextMenuStrip = Nothing


        grCuentasPagar.AllowAddNew = InheritableBoolean.False
        grCuentasPagar.AllowEdit = InheritableBoolean.False
        grCuentasPagar.ContextMenuStrip = Nothing


        grCuentasCompra.AllowAddNew = InheritableBoolean.False
        grCuentasCompra.AllowEdit = InheritableBoolean.False
        grCuentasCompra.ContextMenuStrip = Nothing

        grIngreso.AllowAddNew = InheritableBoolean.False
        grIngreso.AllowEdit = InheritableBoolean.False
        grIngreso.ContextMenuStrip = Nothing

        grEgreso.AllowAddNew = InheritableBoolean.False
        grEgreso.AllowEdit = InheritableBoolean.False
        grEgreso.ContextMenuStrip = Nothing

    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbCuenta1.Text = ""
        tbCuenta2.Text = ""
        tbCuenta3.Text = ""
        tbCuenta4.Text = ""
        tbEmpresa.Text = ""
        tbMaxAjuste.Value = 0

        tbCuentaCobDebe.Text = ""
        tbCuentaCobHaber.Text = ""
        tbCuentaPagDebe.Text = ""
        tbCuentaPagHaber.Text = ""

        _prCargarGridCobrar(-1)
        _prCargarGridPagar(-1)
        _prCargarGridCompras(-1)
        _prCargarGridIngresos(-1)
        _prCargarGridEgresos(-1)
    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbCuenta1.BackColor = Color.White
        tbCuenta2.BackColor = Color.White
        tbCuenta3.BackColor = Color.White
        tbCuenta4.BackColor = Color.White
        tbEmpresa.BackColor = Color.White
        tbMaxAjuste.BackColor = Color.White

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim dtDetalleCobrar As DataTable = CType(grCuentasCobrar.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtDetallePagar As DataTable = CType(grCuentasPagar.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtDetalleCompras As DataTable = CType(grCuentasCompra.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtIngreso As DataTable = CType(grIngreso.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtEgreso As DataTable = CType(grEgreso.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")


        dtDetalleCobrar.Merge(dtDetallePagar)
        dtDetalleCobrar.Merge(dtDetalleCompras)
        dtDetalleCobrar.Merge(dtIngreso)
        dtDetalleCobrar.Merge(dtEgreso)

        Dim res As Boolean = L_prConfigGrabar(tbNumi.Text, tbEmpresa.Value, tbCuenta1.Tag, tbCuenta2.Tag, tbCuenta3.Tag, tbCuenta4.Tag, tbMaxAjuste.Value, tbCuentaCobDebe.Tag, tbCuentaCobHaber.Tag, tbCuentaPagDebe.Tag, tbCuentaPagHaber.Tag, dtDetalleCobrar)
        If res Then
            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim dtDetalleCobrar As DataTable = CType(grCuentasCobrar.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtDetallePagar As DataTable = CType(grCuentasPagar.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtDetalleCompras As DataTable = CType(grCuentasCompra.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtIngreso As DataTable = CType(grIngreso.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")
        Dim dtEgreso As DataTable = CType(grEgreso.DataSource, DataTable).DefaultView.ToTable(True, "cinumi", "cinumitc6", "cinumitc1", "citipo", "estado")

        dtDetalleCobrar.Merge(dtDetallePagar)
        dtDetalleCobrar.Merge(dtDetalleCompras)
        dtDetalleCobrar.Merge(dtIngreso)
        dtDetalleCobrar.Merge(dtEgreso)

        Dim res As Boolean = L_prConfigModificar(tbNumi.Text, tbEmpresa.Value, tbCuenta1.Tag, tbCuenta2.Tag, tbCuenta3.Tag, tbCuenta4.Tag, tbMaxAjuste.Value, tbCuentaCobDebe.Tag, tbCuentaCobHaber.Tag, tbCuentaPagDebe.Tag, tbCuentaPagHaber.Tag, dtDetalleCobrar)
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
            Dim res As Boolean = L_prConfigBorrar(tbNumi.Text, tbEmpresa.Value, tbCuenta1.Tag, tbCuenta2.Tag, tbCuenta3.Tag, tbCuenta4.Tag, tbMaxAjuste.Value, tbCuentaCobDebe.Tag, tbCuentaCobHaber.Tag, tbCuentaPagDebe.Tag, tbCuentaPagHaber.Tag, mensajeError)
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

        If tbEmpresa.SelectedIndex < 0 Then
            tbEmpresa.BackColor = Color.Red
            MEP.SetError(tbEmpresa, "seleccione la empresa!".ToUpper)
            _ok = False
        Else
            tbEmpresa.BackColor = Color.White
            MEP.SetError(tbEmpresa, "")
        End If

        If tbCuenta1.Text = String.Empty Then
            tbCuenta1.BackColor = Color.Red
            MEP.SetError(tbCuenta1, "ingrese cuenta!".ToUpper)
            _ok = False
        Else
            tbCuenta1.BackColor = Color.White
            MEP.SetError(tbCuenta1, "")
        End If

        'If tbCuenta2.Text = String.Empty Then
        '    tbCuenta2.BackColor = Color.Red
        '    MEP.SetError(tbCuenta2, "ingrese cuenta!".ToUpper)
        '    _ok = False
        'Else
        '    tbCuenta2.BackColor = Color.White
        '    MEP.SetError(tbCuenta2, "")
        'End If

        'If tbCuenta3.Text = String.Empty Then
        '    tbCuenta3.BackColor = Color.Red
        '    MEP.SetError(tbCuenta3, "ingrese cuenta!".ToUpper)
        '    _ok = False
        'Else
        '    tbCuenta3.BackColor = Color.White
        '    MEP.SetError(tbCuenta3, "")
        'End If

        'If tbCuenta4.Text = String.Empty Then
        '    tbCuenta4.BackColor = Color.Red
        '    MEP.SetError(tbCuenta4, "ingrese cuenta!".ToUpper)
        '    _ok = False
        'Else
        '    tbCuenta4.BackColor = Color.White
        '    MEP.SetError(tbCuenta4, "")
        'End If

        'If tbCuentaCobDebe.Text = String.Empty Then
        '    tbCuentaCobDebe.BackColor = Color.Red
        '    MEP.SetError(tbCuentaCobDebe, "ingrese cuenta!".ToUpper)
        '    _ok = False
        'Else
        '    tbCuentaCobDebe.BackColor = Color.White
        '    MEP.SetError(tbCuentaCobDebe, "")
        'End If

        'If tbCuentaCobHaber.Text = String.Empty Then
        '    tbCuentaCobHaber.BackColor = Color.Red
        '    MEP.SetError(tbCuentaCobHaber, "ingrese cuenta!".ToUpper)
        '    _ok = False
        'Else
        '    tbCuentaCobHaber.BackColor = Color.White
        '    MEP.SetError(tbCuentaCobHaber, "")
        'End If

        'If tbCuentaPagDebe.Text = String.Empty Then
        '    tbCuentaPagDebe.BackColor = Color.Red
        '    MEP.SetError(tbCuentaPagDebe, "ingrese cuenta!".ToUpper)
        '    _ok = False
        'Else
        '    tbCuentaPagDebe.BackColor = Color.White
        '    MEP.SetError(tbCuentaPagDebe, "")
        'End If

        'If tbCuentaPagHaber.Text = String.Empty Then
        '    tbCuentaPagHaber.BackColor = Color.Red
        '    MEP.SetError(tbCuentaPagHaber, "ingrese cuenta!".ToUpper)
        '    _ok = False
        'Else
        '    tbCuentaPagHaber.BackColor = Color.White
        '    MEP.SetError(tbCuentaPagHaber, "")
        'End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prConfigGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("cfnumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("cftnumitc4", False))
        listEstCeldas.Add(New Modelos.Celda("cedesc", True, "EMPRESA", 200))
        listEstCeldas.Add(New Modelos.Celda("cfnumitc11", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc1", True, "AJUSTE DE CAMBIO(US):", 200))
        listEstCeldas.Add(New Modelos.Celda("cfdifmax", True, "DIFERENCIA MAX DE AJUSTE", 150, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("cfnumitc12", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc2", False, "CUENTA 2", 200))
        listEstCeldas.Add(New Modelos.Celda("cfnumitc13", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc3", False, "CUENTA 3", 200))
        listEstCeldas.Add(New Modelos.Celda("cfnumitc14", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc4", False, "CUENTA 4", 200))

        listEstCeldas.Add(New Modelos.Celda("cfcobdebe", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc5", False, "CUENTA COBRAR", 200))
        listEstCeldas.Add(New Modelos.Celda("cfcobhaber", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc6", False, "CUENTA COBRAR HABER", 200))
        listEstCeldas.Add(New Modelos.Celda("cfpagdebe", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc7", False, "CUENTA PAGAR", 200))
        listEstCeldas.Add(New Modelos.Celda("cfpaghaber", False))
        listEstCeldas.Add(New Modelos.Celda("cadesc8", False, "CUENTA PAGAR HABER", 200))
        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("cfnumi").ToString
            tbEmpresa.Value = .GetValue("cftnumitc4")

            tbCuenta1.Tag = .GetValue("cfnumitc11")
            tbCuenta1.Text = .GetValue("cadesc1").ToString
            tbCuenta2.Tag = .GetValue("cfnumitc12")
            tbCuenta2.Text = .GetValue("cadesc2").ToString
            tbCuenta3.Tag = .GetValue("cfnumitc13")
            tbCuenta3.Text = .GetValue("cadesc3").ToString
            tbCuenta4.Tag = .GetValue("cfnumitc14")
            tbCuenta4.Text = .GetValue("cadesc4").ToString

            'tbCuentaCobDebe.Tag = .GetValue("cfcobdebe")
            'tbCuentaCobDebe.Text = .GetValue("cadesc5").ToString
            'tbCuentaCobHaber.Tag = .GetValue("cfcobhaber")
            'tbCuentaCobHaber.Text = .GetValue("cadesc6").ToString
            'tbCuentaPagDebe.Tag = .GetValue("cfpagdebe")
            'tbCuentaPagDebe.Text = .GetValue("cadesc7").ToString
            'tbCuentaPagHaber.Tag = .GetValue("cfpaghaber")
            'tbCuentaPagHaber.Text = .GetValue("cadesc8").ToString

            tbMaxAjuste.Value = IIf(IsDBNull(.GetValue("cfdifmax")) = True, 0, .GetValue("cfdifmax"))
            'lbFecha.Text = CType(.GetValue("ybfact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("ybhact").ToString
            'lbUsuario.Text = .GetValue("ybuact").ToString

            _prCargarGridCobrar(tbNumi.Text)
            _prCargarGridPagar(tbNumi.Text)
            _prCargarGridCompras(tbNumi.Text)
            _prCargarGridIngresos(tbNumi.Text)
            _prCargarGridEgresos(tbNumi.Text)
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
        tbEmpresa.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbEmpresa.Focus()
    End Sub

    Private Sub tbCuenta1_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuenta1.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuenta1.Text = desc
                tbCuenta1.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub tbEmpresa_ValueChanged(sender As Object, e As EventArgs) Handles tbEmpresa.ValueChanged
        tbCuenta1.Text = ""
        tbCuenta1.Tag = 0
        tbCuenta2.Text = ""
        tbCuenta2.Tag = 0
        tbCuenta3.Text = ""
        tbCuenta3.Tag = 0
        tbCuenta4.Text = ""
        tbCuenta4.Tag = 0
    End Sub

    Private Sub tbCuenta2_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuenta2.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuenta2.Text = desc
                tbCuenta2.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub tbCuenta3_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuenta3.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuenta3.Text = desc
                tbCuenta3.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub tbCuenta4_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuenta4.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuenta4.Text = desc
                tbCuenta4.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub tbCuentaCobDebe_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuentaCobDebe.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuentaCobDebe.Text = desc
                tbCuentaCobDebe.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub tbCuentaCobHaber_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuentaCobHaber.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuentaCobHaber.Text = desc
                tbCuentaCobHaber.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub tbCuentaPagDebe_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuentaPagDebe.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuentaPagDebe.Text = desc
                tbCuentaPagDebe.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub tbCuentaPagHaber_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuentaPagHaber.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                tbCuentaPagHaber.Text = desc
                tbCuentaPagHaber.Tag = numiCuenta
            End If

        End If
    End Sub

    Private Sub grCuentasCobrar_KeyDown(sender As Object, e As KeyEventArgs) Handles grCuentasCobrar.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter And tbEmpresa.SelectedIndex >= 0 Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                grCuentasCobrar.SetValue("cinumitc1", numiCuenta)
                grCuentasCobrar.SetValue("cacta", cod)
                grCuentasCobrar.SetValue("cadesc", desc)
            End If

        End If
    End Sub

    Private Sub grCuentasPagar_KeyDown(sender As Object, e As KeyEventArgs) Handles grCuentasPagar.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter And tbEmpresa.SelectedIndex >= 0 Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                grCuentasPagar.SetValue("cinumitc1", numiCuenta)
                grCuentasPagar.SetValue("cacta", cod)
                grCuentasPagar.SetValue("cadesc", desc)
            End If

        End If
    End Sub

    Private Sub grCuentasCompra_KeyDown(sender As Object, e As KeyEventArgs) Handles grCuentasCompra.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter And tbEmpresa.SelectedIndex >= 0 Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametros(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                grCuentasCompra.SetValue("cinumitc1", numiCuenta)
                grCuentasCompra.SetValue("cacta", cod)
                grCuentasCompra.SetValue("cadesc", desc)
            End If

        End If
    End Sub


    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        _prEliminarFilaDetalleComprar()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        _prEliminarFilaDetalleCobrar()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        _prEliminarFilaDetallePagar()
    End Sub

    Private Sub GroupPanel8_Click(sender As Object, e As EventArgs) Handles GroupPanel8.Click

    End Sub

    Private Sub grIngreso_InputMaskError(sender As Object, e As InputMaskErrorEventArgs) Handles grIngreso.InputMaskError

    End Sub

    Private Sub grIngreso_KeyDown(sender As Object, e As KeyEventArgs) Handles grIngreso.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter And tbEmpresa.SelectedIndex >= 0 Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametrosMayores(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                grIngreso.SetValue("cinumitc1", numiCuenta)
                grIngreso.SetValue("cacta", cod)
                grIngreso.SetValue("cadesc", desc)
            End If

        End If
    End Sub

    Private Sub grEgreso_KeyDown(sender As Object, e As KeyEventArgs) Handles grEgreso.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter And tbEmpresa.SelectedIndex >= 0 Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable

            dt = L_prCuentaGeneralBasicoParaParametrosMayores(tbEmpresa.Value)

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("canumi", False))
            listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
            listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
            listEstCeldas.Add(New Modelos.Celda("catipo", False))
            listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))


            frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value

                grEgreso.SetValue("cinumitc1", numiCuenta)
                grEgreso.SetValue("cacta", cod)
                grEgreso.SetValue("cadesc", desc)
            End If

        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        _prEliminarFilaDetalleIngreso()

    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        _prEliminarFilaDetalleEgreso()
    End Sub
End Class