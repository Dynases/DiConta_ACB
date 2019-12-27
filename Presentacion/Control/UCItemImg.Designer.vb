<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCItemImg
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pbJpg = New System.Windows.Forms.PictureBox()
        Me.pbSombra = New System.Windows.Forms.Panel()
        CType(Me.pbJpg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pbSombra.SuspendLayout()
        Me.SuspendLayout()
        '
        'pbJpg
        '
        Me.pbJpg.BackColor = System.Drawing.Color.Transparent
        Me.pbJpg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbJpg.ErrorImage = Nothing
        Me.pbJpg.Image = Global.Presentacion.My.Resources.Resources.accesorio
        Me.pbJpg.Location = New System.Drawing.Point(5, 5)
        Me.pbJpg.Margin = New System.Windows.Forms.Padding(4)
        Me.pbJpg.Name = "pbJpg"
        Me.pbJpg.Size = New System.Drawing.Size(176, 178)
        Me.pbJpg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbJpg.TabIndex = 2
        Me.pbJpg.TabStop = False
        '
        'pbSombra
        '
        Me.pbSombra.BackColor = System.Drawing.Color.SkyBlue
        Me.pbSombra.Controls.Add(Me.pbJpg)
        Me.pbSombra.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbSombra.Location = New System.Drawing.Point(7, 6)
        Me.pbSombra.Name = "pbSombra"
        Me.pbSombra.Padding = New System.Windows.Forms.Padding(5)
        Me.pbSombra.Size = New System.Drawing.Size(186, 188)
        Me.pbSombra.TabIndex = 3
        '
        'UCItemImg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.pbSombra)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCItemImg"
        Me.Padding = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Size = New System.Drawing.Size(200, 200)
        CType(Me.pbJpg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pbSombra.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbJpg As System.Windows.Forms.PictureBox
    Friend WithEvents pbSombra As Panel
End Class
