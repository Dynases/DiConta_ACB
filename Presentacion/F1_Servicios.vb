Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX
Public Class F1_Servicios


#Region "Variables locales"

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Public Limpiar As Boolean = False  'Bandera para indicar si limpiar todos los datos o mantener datos ya registrados
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            _modulo.Select()
            _tab.Close()
        End If
    End Sub
    Private Sub _prIniciarTodo()
        Me.Text = "SERVICIOS"
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prMaxLength()

        _prCargarComboLibreria(cbtipo, 8, 1)
        _prCargarComboAlmacen(cbSucursal)
        _prAsignarPermisos()
        _PMIniciarTodo()
        Dim blah As New Bitmap(New Bitmap(My.Resources.arqueo), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
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


    Public Sub _prMaxLength()
        tbCodigo.MaxLength = 20
        tbDesc.MaxLength = 100

    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prProductoLibreriaGeneral(cod1, cod2)
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
    Public Function _fnActionNuevo() As Boolean
        Return tbCodigo.Text = String.Empty And tbDesc.ReadOnly = False
    End Function



#End Region
#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()
        tbCodigo.ReadOnly = False
        tbDesc.ReadOnly = False
        tbEstado.IsReadOnly = False
        tbPrecio.ReadOnly = False
        cbtipo.ReadOnly = False
        cbSucursal.ReadOnly = False
        btnImprimir.Visible = False
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbCodigo.ReadOnly = True
        tbCodigo.ReadOnly = True
        tbDesc.ReadOnly = True
        tbEstado.IsReadOnly = True
        tbPrecio.ReadOnly = True
        cbtipo.ReadOnly = True
        cbSucursal.ReadOnly = True
        btnImprimir.Visible = True

        JGrM_Buscador.Focus()
        Limpiar = False
        btnImprimir.Visible = True
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbCodigo.Clear()
        tbDesc.Clear()
        tbEstado.Value = True
        tbNumi.Clear()
        tbPrecio.Clear()
        tbCodigo.Focus()
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        If (CType(cbtipo.DataSource, DataTable).Rows.Count > 0) Then
            cbtipo.SelectedIndex = 0
        End If
    End Sub
    Public Sub _prSeleccionarCombo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        Else
            mCombo.SelectedIndex = -1
        End If
    End Sub


    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbCodigo.BackColor = Color.White
        tbDesc.BackColor = Color.White
        tbEstado.BackColor = Color.White
        tbNumi.BackColor = Color.White
        tbPrecio.BackColor = Color.White
        cbSucursal.BackColor = Color.White
        cbtipo.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        '_sdnumi As String, _sdcprod As String, _sddesc As String, _sdprec As Double, _sdtipo As Integer, _sdsuc As Integer, _sdest As Integer


        Dim res As Boolean = L_prServiciosGrabar(tbNumi.Text, tbCodigo.Text, tbDesc.Text, tbPrecio.Text, cbtipo.Value, cbSucursal.Value, IIf(tbEstado.Value = True, 1, 0))


        If res Then
            Modificado = False
           

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Servicio ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            tbCodigo.Focus()
            Limpiar = True
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Servicio no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean


        res = L_prServicioModificar(tbNumi.Text, tbCodigo.Text, tbDesc.Text, tbPrecio.Text, cbtipo.Value, cbSucursal.Value, IIf(tbEstado.Value = True, 1, 0))

        If res Then
            nameImg = "Default.jpg"
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Servicio ".ToUpper + tbCodigo.Text + " modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "EL producto no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        _PMInhabilitar()
        _PMPrimerRegistro()
        Return res
    End Function
    Public Overrides Sub _PMOEliminarRegistro()
        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prServiciosBorrar(tbNumi.Text, tbCodigo.Text, tbDesc.Text, tbPrecio.Text, cbtipo.Value, cbSucursal.Value, IIf(tbEstado.Value = True, 1, 0), mensajeError)
            If res Then
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Servicio ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _PMFiltrar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If


    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbDesc.Text = String.Empty Then
            tbDesc.BackColor = Color.Red

            MEP.SetError(tbDesc, "ingrese el descripcion del servicio!".ToUpper)
            _ok = False
        Else
            tbDesc.BackColor = Color.White
            MEP.SetError(tbDesc, "")
        End If
        If tbPrecio.Text = String.Empty Then
            tbPrecio.BackColor = Color.Red

            MEP.SetError(tbPrecio, "ingrese precio de producto!".ToUpper)
            _ok = False
        Else
            tbPrecio.BackColor = Color.White
            MEP.SetError(tbPrecio, "")
        End If
        If cbSucursal.SelectedIndex < 0 Then
            cbSucursal.BackColor = Color.Red
            MEP.SetError(cbSucursal, "Selecciones una sucursal!".ToUpper)
            _ok = False
        Else
            cbSucursal.BackColor = Color.White
            MEP.SetError(cbSucursal, "")
        End If

        If cbtipo.SelectedIndex < 0 Then
            cbtipo.BackColor = Color.Red
            MEP.SetError(cbtipo, "Selecciones un Tipo!".ToUpper)
            _ok = False
        Else
            cbtipo.BackColor = Color.White
            MEP.SetError(cbtipo, "")
        End If


        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prServicioGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        'a.sdnumi ,a.sdcod ,a.sddesc,a.sdprec ,a.sdtipo ,c.cndesc1 as tipo,a.sdsuc ,b.cadesc as sucursal ,a.sdest
        listEstCeldas.Add(New Modelos.Celda("sdnumi", False, "Código".ToUpper, 80))
        listEstCeldas.Add(New Modelos.Celda("sdcod", True, "Codigo P.".ToUpper, 90))
        listEstCeldas.Add(New Modelos.Celda("sddesc", True, "Servicio".ToUpper, 450))
        listEstCeldas.Add(New Modelos.Celda("sdprec", True, "Precio".ToUpper, 90, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("sdtipo", False))
        listEstCeldas.Add(New Modelos.Celda("tipo", True, "Tipo".ToUpper, 200))
        listEstCeldas.Add(New Modelos.Celda("sdsuc", False))
        listEstCeldas.Add(New Modelos.Celda("sucursal", True, "Sucursal".ToUpper, 250))
        listEstCeldas.Add(New Modelos.Celda("sdest", True, "Estado".ToUpper, 80))
        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos
         'a.sdnumi ,a.sdcod ,a.sddesc,a.sdprec ,a.sdtipo ,c.cndesc1 as tipo,a.sdsuc ,b.cadesc as sucursal ,a.sdest
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodigo.Text = JGrM_Buscador.GetValue("sdnumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            tbNumi.Text = .GetValue("sdnumi").ToString
            tbCodigo.Text = .GetValue("sdcod").ToString
            tbDesc.Text = .GetValue("sddesc").ToString
            tbPrecio.Text = .GetValue("sdprec").ToString
            cbtipo.Value = .GetValue("sdtipo")
            cbSucursal.Value = .GetValue("sdsuc")
            tbEstado.Value = .GetValue("sdest")
         

        End With
       

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()

        'With MHighlighterFocus
        '    .SetHighlightOnFocus(tbCodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodProd, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbStockMinimo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodBarra, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        '    .SetHighlightOnFocus(tbDescPro, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbDescCort, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo2, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo3, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo4, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUMed, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(swEstado, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUniVenta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUnidMaxima, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbConversion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)


        'End With
    End Sub

#End Region

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub F1_Servicios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub

    Private Sub cbtipo_ValueChanged(sender As Object, e As EventArgs) Handles cbtipo.ValueChanged
        If cbtipo.SelectedIndex < 0 And cbtipo.Text <> String.Empty Then
            btTipo.Visible = True
        Else
            btTipo.Visible = False
        End If
    End Sub

    Private Sub btTipo_Click(sender As Object, e As EventArgs) Handles btTipo.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, 8, 1, cbtipo.Text, "") Then
            _prCargarComboLibreria(cbtipo, 8, 1)
            cbtipo.SelectedIndex = CType(cbtipo.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub
End Class