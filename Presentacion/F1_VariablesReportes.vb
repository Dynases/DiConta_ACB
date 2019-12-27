Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica

Public Class F1_VariablesReportes

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()

        Me.Text = "v a r i a b l e s    r e p o r t e s  ".ToUpper

        btnNuevo.Visible = False
        btnEliminar.Visible = False

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

#End Region

#Region "METODOS SOBRECARGADOS"
    Public Overrides Sub _PMOHabilitar()
        tbDesc.ReadOnly = False



    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbNum.ReadOnly = True
        tbDesc.ReadOnly = True

        tbCod.ReadOnly = True

    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbNum.Text = ""
        tbDesc.Text = ""

        tbCod.Text = ""

    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbNum.BackColor = Color.White
        tbDesc.BackColor = Color.White

        tbCod.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean
        'Dim res As Boolean = L_prEmpresaGrabar(tbNumi.Text, tbCod.Text, tbNum.Text, tbDesc.Text, tbConcep3.Text, tbConcep4.Text)
        'If res Then
        '    ToastNotification.Show(Me, "Codigo de empresa ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        '    tbCod.Focus()
        'End If
        'Return res

        Return False
    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean = L_prTitulosModificar(tbNumi.Text, tbDesc.Text)
        If res Then
            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _PSalirRegistro()
        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        'Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)

        'Dim result As eTaskDialogResult = TaskDialog.Show(info)
        'If result = eTaskDialogResult.Yes Then
        '    Dim mensajeError As String = ""
        '    Dim res As Boolean = L_prTitulosBorrar(tbNumi.Text, mensajeError)
        '    If res Then
        '        ToastNotification.Show(Me, "Codigo de empresa ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        '        _PMFiltrar()
        '    Else
        '        'mensajeError = getMensaje(mensajeError)
        '        ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '    End If
        'End If

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

        'If tbIp.Text = String.Empty Then
        '    tbIp.BackColor = Color.Red
        '    MEP.SetError(tbIp, "ingrese la direccion ip del reloj!".ToUpper)
        '    _ok = False
        'Else
        '    tbIp.BackColor = Color.White
        '    MEP.SetError(tbIp, "")
        'End If

        'If tbConcep1.Text = String.Empty Then
        '    tbConcep1.BackColor = Color.Red
        '    MEP.SetError(tbConcep1, "ingrese el concepto 1 de la sucursal!".ToUpper)
        '    _ok = False
        'Else
        '    tbConcep1.BackColor = Color.White
        '    MEP.SetError(tbConcep1, "")
        'End If

        'If tbConcep2.Text = String.Empty Then
        '    tbConcep2.BackColor = Color.Red
        '    MEP.SetError(tbConcep2, "ingrese el concepto 1 de la sucursal!".ToUpper)
        '    _ok = False
        'Else
        '    tbConcep2.BackColor = Color.White
        '    MEP.SetError(tbConcep2, "")
        'End If

        'If tbConcep3.Text = String.Empty Then
        '    tbConcep3.BackColor = Color.Red
        '    MEP.SetError(tbConcep3, "ingrese el concepto 1 de la sucursal!".ToUpper)
        '    _ok = False
        'Else
        '    tbConcep3.BackColor = Color.White
        '    MEP.SetError(tbConcep3, "")
        'End If

        'If tbConcep4.Text = String.Empty Then
        '    tbConcep4.BackColor = Color.Red
        '    MEP.SetError(tbConcep4, "ingrese el concepto 1 de la sucursal!".ToUpper)
        '    _ok = False
        'Else
        '    tbConcep4.BackColor = Color.White
        '    MEP.SetError(tbConcep4, "")
        'End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prTitulosAll2()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)

        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("yenumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("yecod", True, "CODIGO", 200))
        listEstCeldas.Add(New Modelos.Celda("yenum", True, "NUMERO", 150))
        listEstCeldas.Add(New Modelos.Celda("yedesc", True, "DESCRIPCION", 400))


        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("yenumi").ToString
            tbCod.Text = .GetValue("yecod").ToString
            tbNum.Text = .GetValue("yenum").ToString
            tbDesc.Text = .GetValue("yedesc").ToString


        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            '.SetHighlightOnFocus(tbCod, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbNum, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbDesc, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbNumi, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With
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


    Private Sub F1_Almacen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbCod.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbCod.Focus()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub
End Class