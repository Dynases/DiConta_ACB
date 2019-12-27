Imports Logica.AccesoLogica
Public Class F0_TipoCambio_Nuevo

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Dim resp As Boolean = L_prTipoCambioGrabar("", tbFecha.Value.ToString("yyyy/MM/dd"), tbDolar.Value, tbUFV.Value)
        Me.Close()
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        Me.Close()
    End Sub

    Private Sub F0_TipoCambio_Nuevo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbDolar.Focus()
        tbDolar.Select()
    End Sub

    Private Sub ModeloHor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            P_Moverenfoque()
        End If
    End Sub

    Private Sub P_Moverenfoque()
        SendKeys.Send("{TAB}")
    End Sub
End Class