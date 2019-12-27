
Imports System.ComponentModel
Imports System.Text
Imports DevComponents.DotNetBar
Public Class Efecto
    Public _archivo As String
    Public band As Boolean = False
    Public Header As String = ""
    Public tipo As Integer = 0
    Public Context As String = ""
    Public listEstCeldas As List(Of Modelos.Celda)
    Public dt As DataTable
    Public alto As Integer
    Public ancho As Integer
    Public Row As Janus.Windows.GridEX.GridEXRow
    Public SeleclCol As Integer = -1
    Public nroAutorizacion As String
    Public nroFactura As String


    Public nroControl As String
    Public inicial As Integer
    Public final As Integer
    Public sucursal As Integer
    Public fechaI As String
    Public fechaF As String







    Private Sub Efecto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        Select Case tipo
            Case 1
                 _prMostrarMensaje()
            Case 2
                _prMostrarMensajeDelete()
            Case 3
                _prMostrarFormAyuda()
            Case 4
                _prLogin()
            Case 5
                _prMostrarFormClienteCobrar()
            Case 6
                _prMostrarFormFacturaManual()
            Case 7
                _prMostrarFormClienteBanco()
        End Select
    End Sub
    Public Sub _prLogin()
        Dim Frm As New Login
        Frm.ShowDialog()
        Me.Close()
    End Sub
    Sub _prMostrarFormAyuda()

        Dim frmAyuda As Modelos.ModeloAyuda
        frmAyuda = New Modelos.ModeloAyuda(alto, ancho, dt, Context.ToUpper, listEstCeldas)

        frmAyuda.ShowDialog()
        If frmAyuda.seleccionado = True Then
            Row = frmAyuda.filaSelect
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub

    Sub _prMostrarFormFacturaManual()

        Dim frmAyuda As New F0_SolicitarDatosFactura

        frmAyuda.rangoInicial = inicial
        frmAyuda.rangoFinal = final
        frmAyuda.sucursal = sucursal
        frmAyuda.FechaI = fechaI
        frmAyuda.FechaF = fechaF
        frmAyuda.ShowDialog()
        If frmAyuda.Bandera = True Then
            nroControl = frmAyuda.NroControl
            nroAutorizacion = frmAyuda.NroAutorizacion
            nroFactura = frmAyuda.nameFactura

            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub

    Sub _prMostrarFormClienteCobrar()

        Dim frmAyuda As F1_ClientePorCobrar
        frmAyuda = New F1_ClientePorCobrar(alto, ancho, Context.ToUpper)

        frmAyuda.ShowDialog()
        If frmAyuda.seleccionado = True Then
            Row = frmAyuda.filaSelect
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub

    Sub _prMostrarFormClienteBanco()

        Dim frmAyuda As F1_ClienteBanco
        frmAyuda = New F1_ClienteBanco(alto, ancho, Context.ToUpper)

        frmAyuda.ShowDialog()
        If frmAyuda.seleccionado = True Then
            Row = frmAyuda.filaSelect
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub
    Sub _prMostrarMensaje()
        Dim blah As Bitmap = My.Resources.cuestion
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        If (MessageBox.Show(Context, Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            'Process.Start(_archivo)
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()


        End If
    End Sub
    Sub _prMostrarMensajeDelete()

        Dim info As New TaskDialogInfo(Context, eTaskDialogIcon.Delete, "advertencia".ToUpper, Header, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Default)

        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            band = True
            Me.Close()

        Else
            band = False
            Me.Close()

        End If
    End Sub
End Class