<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PR_Presupuesto
    Inherits Modelos.ModeloR0

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PR_Presupuesto))
        Dim tbModulo_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim tbSucursal_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.tbGestion = New DevComponents.Editors.IntegerInput()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbModulo = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbSucursal = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbMes = New DevComponents.Editors.IntegerInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelIzq.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanelFiltros.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.tbGestion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbModulo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSucursal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Location = New System.Drawing.Point(246, 0)
        Me.PanelPrincipal.Size = New System.Drawing.Size(638, 561)
        '
        'MReportViewer
        '
        Me.MReportViewer.Size = New System.Drawing.Size(638, 561)
        '
        'MGPFiltros
        '
        Me.MGPFiltros.Size = New System.Drawing.Size(246, 561)
        '
        '
        '
        Me.MGPFiltros.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.MGPFiltros.Style.BackColorGradientAngle = 90
        Me.MGPFiltros.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.MGPFiltros.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderBottomWidth = 1
        Me.MGPFiltros.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.MGPFiltros.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderLeftWidth = 1
        Me.MGPFiltros.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderRightWidth = 1
        Me.MGPFiltros.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderTopWidth = 1
        Me.MGPFiltros.Style.CornerDiameter = 4
        Me.MGPFiltros.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.MGPFiltros.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.MGPFiltros.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.MGPFiltros.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.MGPFiltros.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.MGPFiltros.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'PanelIzq
        '
        Me.PanelIzq.Size = New System.Drawing.Size(246, 561)
        Me.PanelIzq.Controls.SetChildIndex(Me.MGPFiltros, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.PanelEx1, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.GroupPanelFiltros, 0)
        '
        'PanelEx1
        '
        Me.PanelEx1.Size = New System.Drawing.Size(246, 76)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelEx1.Style.BackgroundImage = CType(resources.GetObject("PanelEx1.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelEx1.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelEx1.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelEx1.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelEx1.StyleMouseOver.BackgroundImage = CType(resources.GetObject("PanelEx1.StyleMouseOver.BackgroundImage"), System.Drawing.Image)
        '
        'btnSalir
        '
        '
        'btnGenerar
        '
        '
        'PanelInferior
        '
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackgroundImage = CType(resources.GetObject("PanelInferior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        '
        'BubbleBarUsuario
        '
        '
        '
        '
        Me.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        '
        'GroupPanelFiltros
        '
        Me.GroupPanelFiltros.Controls.Add(Me.tbMes)
        Me.GroupPanelFiltros.Controls.Add(Me.LabelX1)
        Me.GroupPanelFiltros.Controls.Add(Me.tbSucursal)
        Me.GroupPanelFiltros.Controls.Add(Me.LabelX4)
        Me.GroupPanelFiltros.Controls.Add(Me.tbModulo)
        Me.GroupPanelFiltros.Controls.Add(Me.LabelX3)
        Me.GroupPanelFiltros.Controls.Add(Me.tbGestion)
        Me.GroupPanelFiltros.Controls.Add(Me.LabelX2)
        Me.GroupPanelFiltros.Size = New System.Drawing.Size(246, 485)
        '
        '
        '
        Me.GroupPanelFiltros.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanelFiltros.Style.BackColorGradientAngle = 90
        Me.GroupPanelFiltros.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanelFiltros.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFiltros.Style.BorderBottomWidth = 1
        Me.GroupPanelFiltros.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanelFiltros.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFiltros.Style.BorderLeftWidth = 1
        Me.GroupPanelFiltros.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFiltros.Style.BorderRightWidth = 1
        Me.GroupPanelFiltros.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFiltros.Style.BorderTopWidth = 1
        Me.GroupPanelFiltros.Style.CornerDiameter = 4
        Me.GroupPanelFiltros.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelFiltros.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelFiltros.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanelFiltros.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelFiltros.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelFiltros.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(42, 0)
        '
        'tbGestion
        '
        '
        '
        '
        Me.tbGestion.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbGestion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbGestion.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbGestion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbGestion.Location = New System.Drawing.Point(83, 11)
        Me.tbGestion.Name = "tbGestion"
        Me.tbGestion.Size = New System.Drawing.Size(82, 21)
        Me.tbGestion.TabIndex = 135
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(18, 11)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(59, 23)
        Me.LabelX2.TabIndex = 134
        Me.LabelX2.Text = "GESTION:"
        '
        'tbModulo
        '
        Me.tbModulo.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        tbModulo_DesignTimeLayout.LayoutString = resources.GetString("tbModulo_DesignTimeLayout.LayoutString")
        Me.tbModulo.DesignTimeLayout = tbModulo_DesignTimeLayout
        Me.tbModulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbModulo.Location = New System.Drawing.Point(3, 95)
        Me.tbModulo.Name = "tbModulo"
        Me.tbModulo.SelectedIndex = -1
        Me.tbModulo.SelectedItem = Nothing
        Me.tbModulo.Size = New System.Drawing.Size(225, 22)
        Me.tbModulo.TabIndex = 137
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(3, 75)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(55, 23)
        Me.LabelX3.TabIndex = 136
        Me.LabelX3.Text = "MODULO:"
        '
        'tbSucursal
        '
        Me.tbSucursal.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        tbSucursal_DesignTimeLayout.LayoutString = resources.GetString("tbSucursal_DesignTimeLayout.LayoutString")
        Me.tbSucursal.DesignTimeLayout = tbSucursal_DesignTimeLayout
        Me.tbSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSucursal.Location = New System.Drawing.Point(3, 149)
        Me.tbSucursal.Name = "tbSucursal"
        Me.tbSucursal.SelectedIndex = -1
        Me.tbSucursal.SelectedItem = Nothing
        Me.tbSucursal.Size = New System.Drawing.Size(225, 22)
        Me.tbSucursal.TabIndex = 139
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(3, 129)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(68, 23)
        Me.LabelX4.TabIndex = 138
        Me.LabelX4.Text = "SUCURSAL:"
        '
        'tbMes
        '
        '
        '
        '
        Me.tbMes.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMes.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMes.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMes.Location = New System.Drawing.Point(83, 38)
        Me.tbMes.Name = "tbMes"
        Me.tbMes.Size = New System.Drawing.Size(82, 21)
        Me.tbMes.TabIndex = 141
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(18, 38)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(59, 23)
        Me.LabelX1.TabIndex = 140
        Me.LabelX1.Text = "MES:"
        '
        'PR_Presupuesto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "PR_Presupuesto"
        Me.Text = "PR_EstadoCuentasActivoPasivo"
        Me.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelIzq.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanelFiltros.ResumeLayout(False)
        Me.GroupPanelFiltros.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.tbGestion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbModulo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSucursal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tbGestion As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbModulo As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbSucursal As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbMes As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
End Class
