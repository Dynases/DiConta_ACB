<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class P_Login2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.PanelSuperior = New DevComponents.DotNetBar.PanelEx()
        Me.MRlAccion = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.PanelInferior = New DevComponents.DotNetBar.PanelEx()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbUsuario = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbPassword = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnIngresar = New DevComponents.DotNetBar.ButtonX()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelSuperior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.PanelSuperior.Controls.Add(Me.MRlAccion)
        Me.PanelSuperior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSuperior.Location = New System.Drawing.Point(0, 0)
        Me.PanelSuperior.Name = "PanelSuperior"
        Me.PanelSuperior.Size = New System.Drawing.Size(373, 50)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.Yellow
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.Khaki
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.TabIndex = 4
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MRlAccion.ForeColor = System.Drawing.Color.Black
        Me.MRlAccion.Location = New System.Drawing.Point(127, 3)
        Me.MRlAccion.Name = "MRlAccion"
        Me.MRlAccion.Size = New System.Drawing.Size(124, 44)
        Me.MRlAccion.TabIndex = 8
        Me.MRlAccion.Text = "<b><font size=""+6"">LOGIN<font color=""#FF0000""></font></font></b>"
        '
        'PanelInferior
        '
        Me.PanelInferior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelInferior.Controls.Add(Me.Label3)
        Me.PanelInferior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 207)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(373, 36)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        Me.PanelInferior.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(351, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Dinases SRL - Dinámicos Servicios en Sistemas Informáticos"
        '
        'tbUsuario
        '
        '
        '
        '
        Me.tbUsuario.Border.Class = "TextBoxBorder"
        Me.tbUsuario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbUsuario.Location = New System.Drawing.Point(164, 86)
        Me.tbUsuario.Name = "tbUsuario"
        Me.tbUsuario.PreventEnterBeep = True
        Me.tbUsuario.Size = New System.Drawing.Size(202, 26)
        Me.tbUsuario.TabIndex = 0
        '
        'tbPassword
        '
        '
        '
        '
        Me.tbPassword.Border.Class = "TextBoxBorder"
        Me.tbPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPassword.Location = New System.Drawing.Point(163, 134)
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbPassword.PreventEnterBeep = True
        Me.tbPassword.Size = New System.Drawing.Size(203, 26)
        Me.tbPassword.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(161, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 16)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Usuario"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(160, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 16)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Contraseña"
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.Presentacion.My.Resources.Resources.Password
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Location = New System.Drawing.Point(131, 130)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(30, 30)
        Me.Panel3.TabIndex = 21
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.Presentacion.My.Resources.Resources.User
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Location = New System.Drawing.Point(131, 82)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(30, 30)
        Me.Panel2.TabIndex = 20
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.Presentacion.My.Resources.Resources.Logo
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Location = New System.Drawing.Point(12, 70)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(105, 105)
        Me.Panel1.TabIndex = 19
        '
        'btnIngresar
        '
        Me.btnIngresar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnIngresar.BackColor = System.Drawing.Color.Transparent
        Me.btnIngresar.BackgroundImage = Global.Presentacion.My.Resources.Resources.ingresar
        Me.btnIngresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnIngresar.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btnIngresar.Image = Global.Presentacion.My.Resources.Resources.ingresar
        Me.btnIngresar.ImageFixedSize = New System.Drawing.Size(160, 35)
        Me.btnIngresar.Location = New System.Drawing.Point(189, 166)
        Me.btnIngresar.Name = "btnIngresar"
        Me.btnIngresar.Size = New System.Drawing.Size(157, 35)
        Me.btnIngresar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnIngresar.TabIndex = 2
        '
        'P_Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(373, 243)
        Me.Controls.Add(Me.btnIngresar)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbPassword)
        Me.Controls.Add(Me.tbUsuario)
        Me.Controls.Add(Me.PanelInferior)
        Me.Controls.Add(Me.PanelSuperior)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "P_Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "P_Login"
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        Me.PanelInferior.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents PanelSuperior As DevComponents.DotNetBar.PanelEx
    Protected WithEvents PanelInferior As DevComponents.DotNetBar.PanelEx
    Friend WithEvents tbUsuario As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbPassword As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents MRlAccion As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnIngresar As DevComponents.DotNetBar.ButtonX
End Class
