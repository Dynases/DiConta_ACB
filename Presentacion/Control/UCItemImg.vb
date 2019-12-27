Public Class UCItemImg






    Private Sub pbJpg_MouseLeave(sender As Object, e As EventArgs) Handles pbJpg.MouseLeave
        pbSombra.BackColor = Color.White

    End Sub

    Private Sub pbJpg_MouseEnter(sender As Object, e As EventArgs) Handles pbJpg.MouseEnter
        pbSombra.BackColor = Color.SkyBlue
    End Sub
End Class
