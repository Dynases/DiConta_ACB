Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica

Public Class F1_Empresas

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()

        Me.Text = "e m p r e s a s ".ToUpper
        tbConcep4.Visible = False

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
        tbConcep1.ReadOnly = False
        tbConcep2.ReadOnly = False
        tbConcep3.ReadOnly = False
        tbConcep4.ReadOnly = False
        tbDesc.ReadOnly = False


    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbConcep1.ReadOnly = True
        tbConcep2.ReadOnly = True
        tbConcep3.ReadOnly = True
        tbConcep4.ReadOnly = True
        tbDesc.ReadOnly = True

    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbConcep1.Text = ""
        tbConcep2.Text = ""
        tbConcep3.Text = ""
        tbConcep4.Text = ""
        tbDesc.Text = ""

    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbConcep1.BackColor = Color.White
        tbConcep2.BackColor = Color.White
        tbConcep3.BackColor = Color.White
        tbConcep4.BackColor = Color.White
        tbDesc.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean
        Dim res As Boolean = L_prEmpresaGrabar(tbNumi.Text, tbDesc.Text, tbConcep1.Text, tbConcep2.Text, tbConcep3.Text, tbConcep4.Text)
        If res Then
            ToastNotification.Show(Me, "Codigo de empresa ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            tbDesc.Focus()
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean = L_prEmpresaModificar(tbNumi.Text, tbDesc.Text, tbConcep1.Text, tbConcep2.Text, tbConcep3.Text, tbConcep4.Text)
        If res Then
            ToastNotification.Show(Me, "Codigo de empresa ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _PSalirRegistro()
        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)

        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prEmpresaBorrar(tbNumi.Text, tbDesc.Text, tbConcep1.Text, tbConcep2.Text, tbConcep3.Text, tbConcep4.Text, mensajeError)
            If res Then
                ToastNotification.Show(Me, "Codigo de empresa ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                'mensajeError = getMensaje(mensajeError)
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbDesc.Text = String.Empty Then
            tbDesc.BackColor = Color.Red
            MEP.SetError(tbDesc, "ingrese la descripcion de la sucursal!".ToUpper)
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
        Dim dtBuscador As DataTable = L_prEmpresaGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)

        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("cenumi", True, "COD", 70))
        listEstCeldas.Add(New Modelos.Celda("cedesc", True, "DESCRIPCION", 300))
        listEstCeldas.Add(New Modelos.Celda("cecon1", True, "NIT", 150))
        listEstCeldas.Add(New Modelos.Celda("cecon2", True, "DIRECCION", 150))
        listEstCeldas.Add(New Modelos.Celda("cecon3", False, "CONCEPTO 3", 150))
        listEstCeldas.Add(New Modelos.Celda("cecon4", False, "CONCEPTO 4", 150))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("cenumi").ToString
            tbDesc.Text = .GetValue("cedesc").ToString
            tbConcep1.Text = .GetValue("cecon1").ToString
            tbConcep2.Text = .GetValue("cecon2").ToString
            tbConcep3.Text = .GetValue("cecon3").ToString
            tbConcep4.Text = .GetValue("cecon4").ToString

        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbDesc, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbConcep1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbConcep2, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbConcep3, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbNumi, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbConcep4, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

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
        tbDesc.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbDesc.Focus()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub
End Class