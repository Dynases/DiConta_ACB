Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar.Controls

Public Class F1_ClienteCarburantes

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()
        Me.Text = "c l i e n t e s".ToUpper

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

        tbNombre.ReadOnly = False
        tbDireccion.ReadOnly = False
        tbTelefono.ReadOnly = False

        tbTipo.IsReadOnly = False

    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbNombre.ReadOnly = True
        tbDireccion.ReadOnly = True
        tbTelefono.ReadOnly = True
        tbTipo.IsReadOnly = True

    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbNombre.Text = ""
        tbDireccion.Text = ""
        tbTelefono.Text = ""
        tbTipo.Value = True
    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbNombre.BackColor = Color.White
        tbDireccion.BackColor = Color.White
        tbTelefono.BackColor = Color.White

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim res As Boolean = L_prClienteCarburadorGrabar(tbNumi.Text, tbNombre.Text, tbDireccion.Text, tbTelefono.Text, IIf(tbTipo.Value = True, 1, 2))
        If res Then
            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean


        Dim res As Boolean = L_prClienteCarburadorModificar(tbNumi.Text, tbNombre.Text, tbDireccion.Text, tbTelefono.Text, IIf(tbTipo.Value = True, 1, 2))
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
            Dim res As Boolean = L_prClienteCarburadorBorrar(tbNumi.Text, tbNombre.Text, tbDireccion.Text, tbTelefono.Text, mensajeError)
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

        If tbNombre.Text = String.Empty Then
            tbNombre.BackColor = Color.Red
            MEP.SetError(tbNombre, "ingrese el nombre del cliente!".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prClienteCarburadorGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("adnumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("adnom", True, "NOMBRE", 300))
        listEstCeldas.Add(New Modelos.Celda("addirec", True, "DIRECCION", 300))
        listEstCeldas.Add(New Modelos.Celda("adtelef", True, "TELEFONO", 150))
        listEstCeldas.Add(New Modelos.Celda("adtipo", False))
        listEstCeldas.Add(New Modelos.Celda("adtipo2", True, "TIPO", 120))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("adnumi").ToString
            tbNombre.Text = .GetValue("adnom").ToString
            tbDireccion.Text = .GetValue("addirec").ToString
            tbTelefono.Text = .GetValue("adtelef").ToString
            tbTipo.Value = IIf(.GetValue("adtipo") = 1, True, False)

            'lbFecha.Text = CType(.GetValue("ybfact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("ybhact").ToString
            'lbUsuario.Text = .GetValue("ybuact").ToString

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
        tbNombre.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbNombre.Focus()
    End Sub

End Class