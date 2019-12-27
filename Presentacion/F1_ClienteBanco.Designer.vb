<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F1_ClienteBanco
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GPPanelP = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grAyudaCuenta = New Janus.Windows.GridEX.GridEX()
        Me.cmOpcionesAyuda = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GPPanelP.SuspendLayout()
        CType(Me.grAyudaCuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmOpcionesAyuda.SuspendLayout()
        Me.SuspendLayout()
        '
        'GPPanelP
        '
        Me.GPPanelP.BackColor = System.Drawing.Color.Transparent
        Me.GPPanelP.CanvasColor = System.Drawing.SystemColors.Control
        Me.GPPanelP.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GPPanelP.Controls.Add(Me.grAyudaCuenta)
        Me.GPPanelP.DisabledBackColor = System.Drawing.Color.Empty
        Me.GPPanelP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GPPanelP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPPanelP.Location = New System.Drawing.Point(0, 0)
        Me.GPPanelP.Margin = New System.Windows.Forms.Padding(4)
        Me.GPPanelP.Name = "GPPanelP"
        Me.GPPanelP.Size = New System.Drawing.Size(1046, 341)
        '
        '
        '
        Me.GPPanelP.Style.BackColor = System.Drawing.SystemColors.HotTrack
        Me.GPPanelP.Style.BackColor2 = System.Drawing.SystemColors.HotTrack
        Me.GPPanelP.Style.BackColorGradientAngle = 90
        Me.GPPanelP.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPPanelP.Style.BorderBottomWidth = 1
        Me.GPPanelP.Style.BorderColor = System.Drawing.Color.RoyalBlue
        Me.GPPanelP.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPPanelP.Style.BorderLeftWidth = 1
        Me.GPPanelP.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPPanelP.Style.BorderRightWidth = 1
        Me.GPPanelP.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPPanelP.Style.BorderTopWidth = 1
        Me.GPPanelP.Style.CornerDiameter = 4
        Me.GPPanelP.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GPPanelP.Style.Font = New System.Drawing.Font("Georgia", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPPanelP.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GPPanelP.Style.TextColor = System.Drawing.Color.White
        Me.GPPanelP.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GPPanelP.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GPPanelP.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GPPanelP.TabIndex = 3
        Me.GPPanelP.Text = "CLIENTES POR COBRAR"
        '
        'grAyudaCuenta
        '
        Me.grAyudaCuenta.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grAyudaCuenta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grAyudaCuenta.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle
        Me.grAyudaCuenta.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grAyudaCuenta.HeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grAyudaCuenta.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grAyudaCuenta.Location = New System.Drawing.Point(0, 0)
        Me.grAyudaCuenta.Margin = New System.Windows.Forms.Padding(4)
        Me.grAyudaCuenta.Name = "grAyudaCuenta"
        Me.grAyudaCuenta.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grAyudaCuenta.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grAyudaCuenta.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grAyudaCuenta.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grAyudaCuenta.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grAyudaCuenta.Size = New System.Drawing.Size(1040, 313)
        Me.grAyudaCuenta.TabIndex = 0
        Me.grAyudaCuenta.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmOpcionesAyuda
        '
        Me.cmOpcionesAyuda.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.cmOpcionesAyuda.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.cmOpcionesAyuda.Name = "ContextMenuStrip1"
        Me.cmOpcionesAyuda.Size = New System.Drawing.Size(216, 28)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(215, 24)
        Me.ToolStripMenuItem1.Text = "MODIFICAR CLIENTE"
        '
        'F1_ClienteBanco
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1046, 341)
        Me.ControlBox = False
        Me.Controls.Add(Me.GPPanelP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "F1_ClienteBanco"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F1_ClienteBanco"
        Me.GPPanelP.ResumeLayout(False)
        CType(Me.grAyudaCuenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmOpcionesAyuda.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GPPanelP As DevComponents.DotNetBar.Controls.GroupPanel
    Public WithEvents grAyudaCuenta As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmOpcionesAyuda As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
End Class
