
Imports DevComponents.DotNetBar

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Logica.AccesoLogica
Public Class Login

    Dim _DuracionSms As Integer = 2
    Dim _i As Integer = 100
    Public _ok As Boolean

#Region "metodos privados"
    Private Sub _prIngresar()
        If tbUsuario.Text = "" Then
            ToastNotification.Show(Me, "No Puede Dejar Nombre en Blanco..!!!".ToUpper, My.Resources.WARNING, _DuracionSms * 1000, eToastGlowColor.Red, eToastPosition.TopCenter)
            Exit Sub
        End If
        If tbPassword.Text = "" Then
            ToastNotification.Show(Me, "No Puede Dejar Password en Blanco..!!!".ToUpper, My.Resources.WARNING, _DuracionSms * 1000, eToastGlowColor.Red, eToastPosition.TopCenter)
            Exit Sub
        End If
        Dim dtUsuario As DataTable = L_Validar_Usuario(tbUsuario.Text, tbPassword.Text, gi_empresaNumi)
        If dtUsuario.Rows.Count = 0 Then
            ToastNotification.Show(Me, "Codigo de Usuario y Password Incorrecto..!!!".ToUpper, My.Resources.WARNING, _DuracionSms * 1000, eToastGlowColor.Red, eToastPosition.TopCenter)
        Else
            gs_user = tbUsuario.Text


            gi_userFuente = dtUsuario.Rows(0).Item("ydfontsize")
            gi_userNumi = dtUsuario.Rows(0).Item("ydnumi")
            gi_userRol = dtUsuario.Rows(0).Item("ydrol")
            gi_userNumiEmpresa = dtUsuario.Rows(0).Item("ydemp")
            gb_userTodasSuc = IIf(dtUsuario.Rows(0).Item("ydall") = 1, True, False)
            gi_userNumiSucursal = dtUsuario.Rows(0).Item("ydsuc")
            _prDesvenecerPantalla()
            _ok = True
            Close()
        End If
    End Sub

    Private Sub _prDesvenecerPantalla()
        Dim a, b As Decimal
        For a = 100 To 0 Step -1
            b = a / 100
            Me.Opacity = b
            Me.Refresh()
        Next
    End Sub
#End Region
    Public Sub _habilitarFocus()
        With Highlighter1
            .SetHighlightOnFocus(tbUsuario, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbPassword, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnIngresar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With
    End Sub
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _habilitarFocus()

        btnIngresar.TextAlign = ContentAlignment.MiddleCenter
        btnIngresar.ForeColor = Color.White
        tbUsuario.Multiline = False
        tbPassword.Multiline = False


        'Dim blah As Bitmap = My.Resources
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        'Me.Icon = ico
        Me.Text = "L O G I N"

        tbUsuario.CharacterCasing = CharacterCasing.Upper


        Me.Opacity = 0.01
        Timer1.Interval = 10
        Timer1.Enabled = True

        tbUsuario.Focus()



    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.Opacity < 1 Then
            Me.Opacity += 0.018
        Else
            Timer1.Enabled = False
        End If



    End Sub

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs)


    End Sub


    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub



    Private Sub tbname_KeyDown(sender As Object, e As KeyEventArgs) Handles tbUsuario.KeyDown
        If (e.KeyData = Keys.Enter) Then
            tbPassword.Focus()

        End If


    End Sub

    Private Sub tbpass_KeyDown(sender As Object, e As KeyEventArgs) Handles tbPassword.KeyDown
        If (e.KeyData = Keys.Enter) Then
            btnIngresar.Focus()

        End If
    End Sub


    Private Sub tbname_ChangeUICues(sender As Object, e As UICuesEventArgs) Handles tbUsuario.ChangeUICues

    End Sub

    Private Sub btnIngresar_Click_1(sender As Object, e As EventArgs) Handles btnIngresar.Click
        _prIngresar()
    End Sub

    Private Sub Login_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

        If (e.KeyChar = ChrW(Keys.Escape)) Then
            _prDesvenecerPantalla()
            _ok = False
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _ok = False
        Me.Close()
        Exit Sub
    End Sub
End Class