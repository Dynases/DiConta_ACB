Option Strict Off
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports CrystalDecisions.Shared
Public Class F1_ServicioCuentas

#Region "Variables Globales"

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#End Region

#Region "METODOS PRIVADOS"

    Private Sub _IniciarTodo()
        ''L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        SuperTabItemBuscador.Visible = False
        btnNuevo.Visible = False
        btnEliminar.Visible = False
        MSuperTabControl.SelectedTabIndex = 0
        Me.WindowState = FormWindowState.Maximized
        _prInhabiliitar()
        _prCargarComboCategoriasServicios(cbCategoria)


        Me.Text = "NUMERO CUENTAS SERVICIO"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()

    End Sub
    Private Sub _prCargarComboSector(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralLibreriaLavadero()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cnnum").Width = 60
            .DropDownList.Columns("cnnum").Caption = "COD"
            .DropDownList.Columns.Add("cndesc1").Width = 200
            .DropDownList.Columns("cndesc1").Caption = "SECTOR"
            .ValueMember = "cnnum"
            .DisplayMember = "cndesc1"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbCategoria.DataSource, DataTable).Rows.Count > 0) Then
            cbCategoria.SelectedIndex = 0
        End If
    End Sub
    Private Sub _prCargarTablaServicios()
        Dim dt As New DataTable
        dt = L_prlistarMostrarServiciosnumeroCuenta(cbCategoria.Value)

        grServicios.DataSource = dt
        grServicios.RetrieveStructure()
        grServicios.AlternatingColors = True
        ' senumi,senumiserv, servicio,senrocuenta, estado

        With grServicios.RootTable.Columns("senumi")
            .Width = 100

            .Visible = False
        End With
        With grServicios.RootTable.Columns("senumiserv")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grServicios.RootTable.Columns("servicio")
            .Width = 350
            .Caption = "SERVICIOS"
            .Visible = True
        End With
        With grServicios.RootTable.Columns("senrocuenta")
            .Width = 200
            .Caption = "NRO CUENTA"
            .Visible = True
        End With
        With grServicios.RootTable.Columns("seref")
            .Width = 350
            .Caption = "REFERENCIA"
            .Visible = True
        End With

        With grServicios.RootTable.Columns("sefactu")
            .Width = 100
            .Caption = "IVA"
            .CellStyle.TextAlignment = TextAlignment.Center
            .Visible = True
        End With

        With grServicios.RootTable.Columns("sucursal")
            .Width = 250
            .Caption = "SUCURSAL"
            .Visible = True
        End With
        With grServicios.RootTable.Columns("estado")
            .Width = 100

            .Visible = False
        End With
        With grServicios.RootTable.Columns("seest")
            .Width = 100

            .Visible = False
        End With

        With grServicios.RootTable.Columns("existe")
            .Width = 100

            .Visible = True
        End With
        With grServicios
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grServicios.RootTable.Columns("existe"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.Red
        grServicios.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub _prCargarComboCategoriasServicios(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prlistarCategoriasActivos()
        dt.Rows.Add(20, "PRODUCTOS")
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cenum").Width = 60
            .DropDownList.Columns("cenum").Caption = "COD"
            .DropDownList.Columns.Add("cedesc1").Width = 500
            .DropDownList.Columns("cedesc1").Caption = "CATEGORIA"
            .ValueMember = "cenum"
            .DisplayMember = "cedesc1"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        End If
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
    Private Sub _prInhabiliitar()

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        PanelNavegacion.Enabled = True
        _prCargarTablaServicios()
    End Sub
    Private Sub _prhabilitar()
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        'btnGrabar.Enabled = True

    End Sub

    Private Sub F1_ServicioCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Private Sub cbCategoria_ValueChanged(sender As Object, e As EventArgs) Handles cbCategoria.ValueChanged
        _prCargarTablaServicios()
    End Sub

    Private Sub grServicios_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grServicios.CellEdited
        Dim rowIndex As Integer = grServicios.CurrentRow.RowIndex
        'Columna de Precio Venta
        If (e.Column.Index = grServicios.RootTable.Columns("senrocuenta").Index Or e.Column.Index = grServicios.RootTable.Columns("seref").Index Or e.Column.Index = grServicios.RootTable.Columns("sefactu").Index) Then

            If (grServicios.GetValue("senrocuenta").ToString.Length > 0) Then
                Dim estado = grServicios.GetValue("estado")
                If (estado = 1) Then
                    grServicios.SetValue("estado", 2)

                End If
            Else
                Dim estado = grServicios.GetValue("estado")
                If (estado = 1 And grServicios.GetValue("seref").ToString.Length <= 0) Then
                    grServicios.SetValue("estado", -1)

                End If
            End If

            If (grServicios.GetValue("seref").ToString.Length > 0) Then
                Dim estado = grServicios.GetValue("estado")
                If (estado = 1) Then
                    grServicios.SetValue("estado", 2)

                End If
            Else
                Dim estado = grServicios.GetValue("estado")
                If (estado = 1 And grServicios.GetValue("senrocuenta").ToString.Length <= 0) Then
                    grServicios.SetValue("estado", -1)

                End If
            End If

            'If (grServicios.GetValue("seref").ToString.Length > 0) Then
            '    Dim estado = grServicios.GetValue("estado")
            '    If (estado = 1) Then
            '        grServicios.SetValue("estado", 2)

            '    End If
            'Else
            '    Dim estado = grServicios.GetValue("estado")
            '    If (estado = 1 And grServicios.GetValue("senrocuenta").ToString.Length <= 0) Then
            '        grServicios.SetValue("estado", -1)

            '    End If
            'End If

        End If
    End Sub
    Public Function _fnVisualizarRegistros() As Boolean
        Return btnGrabar.Enabled = True
    End Function
    Private Sub grServicios_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grServicios.EditingCell
        If (_fnVisualizarRegistros()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grServicios.RootTable.Columns("senrocuenta").Index Or e.Column.Index = grServicios.RootTable.Columns("seref").Index Or e.Column.Index = grServicios.RootTable.Columns("sefactu").Index) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        _prGuardarModificado()
        ''    _prInhabiliitar()
    End Sub

    Private Sub _prGuardarModificado()
        Dim dt As DataTable = CType(grServicios.DataSource, DataTable)
        dt = dt.DefaultView.ToTable(False, "senumi", "senumiserv", "servicio", "senrocuenta", "seref", "seest", "sefactu", "estado", "sucursal")

        Dim res As Boolean = L_prServicioCuentaModificar("", dt)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Numero de cuentas en servicio modificados".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            _prCargarTablaServicios()
            btnModificar.Enabled = True
            btnGrabar.Enabled = False
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "Los codigos no pudieron ser modificados".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        btnModificar.Enabled = False
        btnGrabar.Enabled = True

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()

        Else
            _modulo.Select()
            _tab.Close()
        End If
    End Sub

    Private Sub grServicios_RecordUpdated(sender As Object, e As EventArgs) Handles grServicios.RecordUpdated
        If grServicios.RootTable.Columns(grServicios.Col).Key = "senrocuenta" Then
            Dim nroCuenta As String = grServicios.GetValue("senrocuenta").ToString()
            Dim dtCuenta As DataTable = L_prObtenerNombreCuenta(nroCuenta)
            If dtCuenta.Rows.Count = 0 Then
                grServicios.SetValue("existe", 0)
            Else
                grServicios.SetValue("existe", 1)
            End If

        End If
    End Sub

#End Region



End Class