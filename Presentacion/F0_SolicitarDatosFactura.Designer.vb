<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F0_SolicitarDatosFactura
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnsalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnguardar = New DevComponents.DotNetBar.ButtonX()
        Me.tbcodigocontrol = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbnroautorizacion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbnrofactura = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX19 = New DevComponents.DotNetBar.LabelX()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ReflectionLabel1 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.MEP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MHighlighterFocusTT = New DevComponents.DotNetBar.Validator.Highlighter()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.btnsalir)
        Me.Panel1.Controls.Add(Me.btnguardar)
        Me.Panel1.Controls.Add(Me.tbcodigocontrol)
        Me.Panel1.Controls.Add(Me.LabelX2)
        Me.Panel1.Controls.Add(Me.tbnroautorizacion)
        Me.Panel1.Controls.Add(Me.LabelX1)
        Me.Panel1.Controls.Add(Me.tbnrofactura)
        Me.Panel1.Controls.Add(Me.LabelX19)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(731, 343)
        Me.Panel1.TabIndex = 0
        '
        'btnsalir
        '
        Me.btnsalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnsalir.BackColor = System.Drawing.Color.Transparent
        Me.btnsalir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnsalir.FadeEffect = False
        Me.btnsalir.FocusCuesEnabled = False
        Me.btnsalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsalir.Image = Global.Presentacion.My.Resources.Resources.atras
        Me.btnsalir.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.btnsalir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnsalir.Location = New System.Drawing.Point(395, 240)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnsalir.Size = New System.Drawing.Size(132, 89)
        Me.btnsalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnsalir.TabIndex = 4
        Me.btnsalir.Text = "SALIR"
        Me.btnsalir.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'btnguardar
        '
        Me.btnguardar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnguardar.BackColor = System.Drawing.Color.Transparent
        Me.btnguardar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnguardar.FadeEffect = False
        Me.btnguardar.FocusCuesEnabled = False
        Me.btnguardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnguardar.Image = Global.Presentacion.My.Resources.Resources.save
        Me.btnguardar.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.btnguardar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnguardar.Location = New System.Drawing.Point(239, 240)
        Me.btnguardar.Name = "btnguardar"
        Me.btnguardar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnguardar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.btnguardar.Size = New System.Drawing.Size(129, 89)
        Me.btnguardar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnguardar.TabIndex = 3
        Me.btnguardar.Text = "ACEPTAR"
        Me.btnguardar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'tbcodigocontrol
        '
        '
        '
        '
        Me.tbcodigocontrol.Border.Class = "TextBoxBorder"
        Me.tbcodigocontrol.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcodigocontrol.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcodigocontrol.Location = New System.Drawing.Point(336, 193)
        Me.tbcodigocontrol.Name = "tbcodigocontrol"
        Me.tbcodigocontrol.PreventEnterBeep = True
        Me.tbcodigocontrol.Size = New System.Drawing.Size(338, 30)
        Me.tbcodigocontrol.TabIndex = 2
        Me.tbcodigocontrol.Visible = False
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(90, 194)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(228, 25)
        Me.LabelX2.TabIndex = 158
        Me.LabelX2.Text = "CODIGO DE CONTROL:"
        Me.LabelX2.Visible = False
        '
        'tbnroautorizacion
        '
        '
        '
        '
        Me.tbnroautorizacion.Border.Class = "TextBoxBorder"
        Me.tbnroautorizacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbnroautorizacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbnroautorizacion.Location = New System.Drawing.Point(336, 157)
        Me.tbnroautorizacion.Name = "tbnroautorizacion"
        Me.tbnroautorizacion.PreventEnterBeep = True
        Me.tbnroautorizacion.Size = New System.Drawing.Size(338, 30)
        Me.tbnroautorizacion.TabIndex = 1
        Me.tbnroautorizacion.Visible = False
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(72, 158)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(246, 25)
        Me.LabelX1.TabIndex = 156
        Me.LabelX1.Text = "NRO DE AUTORIZACION:"
        Me.LabelX1.Visible = False
        '
        'tbnrofactura
        '
        '
        '
        '
        Me.tbnrofactura.Border.Class = "TextBoxBorder"
        Me.tbnrofactura.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbnrofactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbnrofactura.Location = New System.Drawing.Point(336, 111)
        Me.tbnrofactura.Name = "tbnrofactura"
        Me.tbnrofactura.PreventEnterBeep = True
        Me.tbnrofactura.Size = New System.Drawing.Size(206, 30)
        Me.tbnrofactura.TabIndex = 0
        Me.tbnrofactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelX19
        '
        Me.LabelX19.AutoSize = True
        Me.LabelX19.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX19.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX19.Location = New System.Drawing.Point(129, 112)
        Me.LabelX19.Name = "LabelX19"
        Me.LabelX19.Size = New System.Drawing.Size(189, 25)
        Me.LabelX19.TabIndex = 154
        Me.LabelX19.Text = "NRO DE FACTURA:"
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.Presentacion.My.Resources.Resources.fondoPanel
        Me.Panel2.Controls.Add(Me.ReflectionLabel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(731, 86)
        Me.Panel2.TabIndex = 0
        '
        'ReflectionLabel1
        '
        Me.ReflectionLabel1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.ReflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ReflectionLabel1.Font = New System.Drawing.Font("Georgia", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReflectionLabel1.ForeColor = System.Drawing.Color.White
        Me.ReflectionLabel1.Location = New System.Drawing.Point(9, 9)
        Me.ReflectionLabel1.Name = "ReflectionLabel1"
        Me.ReflectionLabel1.Size = New System.Drawing.Size(587, 70)
        Me.ReflectionLabel1.TabIndex = 0
        Me.ReflectionLabel1.Text = "DATOS DE LA FACTURACION MANUAL"
        '
        'MEP
        '
        Me.MEP.ContainerControl = Me
        '
        'MHighlighterFocusTT
        '
        Me.MHighlighterFocusTT.ContainerControl = Me

        '
        'F0_SolicitarDatosFactura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(731, 343)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "F0_SolicitarDatosFactura"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F0_SolicitarDatosFactura"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ReflectionLabel1 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents tbnrofactura As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX19 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbnroautorizacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbcodigocontrol As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnsalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnguardar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents MEP As ErrorProvider
    Friend WithEvents MHighlighterFocusTT As DevComponents.DotNetBar.Validator.Highlighter
End Class
