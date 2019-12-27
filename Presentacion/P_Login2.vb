Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica

'DESARROLLADO POR: DANNY GUTIERREZ
Public Class P_Login2
    Dim _DuracionSms As Integer = 2
    Dim _i As Integer = 100

    Private Sub Bt1_Ingresa_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click

        If tbUsuario.Text = "" Then
            ToastNotification.Show(Me, "No Puede Dejar Nombre en Blanco..!!!".ToUpper, My.Resources.WARNING, _DuracionSms * 1000, eToastGlowColor.Red, eToastPosition.BottomLeft)
            Exit Sub
        End If
        If tbPassword.Text = "" Then
            ToastNotification.Show(Me, "No Puede Dejar Password en Blanco..!!!".ToUpper, My.Resources.WARNING, _DuracionSms * 1000, eToastGlowColor.Red, eToastPosition.BottomLeft)
            Exit Sub
        End If
        Dim dtUsuario As DataTable = L_Validar_Usuario(tbUsuario.Text, tbPassword.Text, gi_empresaNumi)
        If dtUsuario.Rows.Count = 0 Then
            ToastNotification.Show(Me, "Codigo de Usuario y Password Incorrecto..!!!".ToUpper, My.Resources.WARNING, _DuracionSms * 1000, eToastGlowColor.Red, eToastPosition.BottomLeft)
        Else
            gs_user = tbUsuario.Text
            gi_userFuente = dtUsuario.Rows(0).Item("ydfontsize")
            gi_userNumi = dtUsuario.Rows(0).Item("ydnumi")
            gi_userRol = dtUsuario.Rows(0).Item("ydrol")
            gi_userNumiEmpresa = dtUsuario.Rows(0).Item("ydsuc")
            'gb_userTodasSuc = IIf(dtUsuario.Rows(0).Item("ydall") = 1, True, False)

            _prDesvenecerPantalla()
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

    Private Sub P_Inicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbUsuario.Focus()
    End Sub
    Private Sub ModeloHor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress, tbPassword.KeyPress, tbUsuario.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            P_Moverenfoque()
        End If

        If (e.KeyChar = ChrW(Keys.Escape)) Then
            _prDesvenecerPantalla()
            Me.Close()
        End If
    End Sub

    Private Sub P_Moverenfoque()
        SendKeys.Send("{TAB}")
    End Sub
End Class