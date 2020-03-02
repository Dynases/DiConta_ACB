<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F0_ComprobanteImportar
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_ComprobanteImportar))
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.MRlAccion = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.PanelInferior = New DevComponents.DotNetBar.PanelEx()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaDel = New System.Windows.Forms.DateTimePicker()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grDetalle = New Janus.Windows.GridEX.GridEX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaAl = New System.Windows.Forms.DateTimePicker()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbFiltrarFecha = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.PanelEx1.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.PanelEx1.Controls.Add(Me.MRlAccion)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1097, 70)
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
        Me.PanelEx1.TabIndex = 22
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MRlAccion.ForeColor = System.Drawing.Color.White
        Me.MRlAccion.Location = New System.Drawing.Point(5, 0)
        Me.MRlAccion.Margin = New System.Windows.Forms.Padding(5)
        Me.MRlAccion.Name = "MRlAccion"
        Me.MRlAccion.Size = New System.Drawing.Size(579, 65)
        Me.MRlAccion.TabIndex = 9
        Me.MRlAccion.Text = "<b><font size=""-8"">IMPORTAR COMPROBANTE<font color=""#FF0000""></font></font></b>"
        '
        'PanelInferior
        '
        Me.PanelInferior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelInferior.Controls.Add(Me.ButtonX2)
        Me.PanelInferior.Controls.Add(Me.ButtonX1)
        Me.PanelInferior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 409)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(1097, 43)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackgroundImage = CType(resources.GetObject("PanelInferior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        Me.PanelInferior.TabIndex = 32
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.ButtonX2.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonX2.Image = Global.Presentacion.My.Resources.Resources.atras
        Me.ButtonX2.ImageFixedSize = New System.Drawing.Size(24, 24)
        Me.ButtonX2.Location = New System.Drawing.Point(170, 0)
        Me.ButtonX2.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(167, 43)
        Me.ButtonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX2.TabIndex = 2
        Me.ButtonX2.Text = "&SALIR"
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.ButtonX1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonX1.Image = Global.Presentacion.My.Resources.Resources.GRABACION_EXITOSA
        Me.ButtonX1.ImageFixedSize = New System.Drawing.Size(24, 24)
        Me.ButtonX1.Location = New System.Drawing.Point(0, 0)
        Me.ButtonX1.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(170, 43)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 1
        Me.ButtonX1.Text = "S&ELECCIONAR"
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(140, 10)
        Me.LabelX3.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(52, 28)
        Me.LabelX3.TabIndex = 117
        Me.LabelX3.Text = "DEL:"
        '
        'tbFechaDel
        '
        Me.tbFechaDel.Enabled = False
        Me.tbFechaDel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbFechaDel.Location = New System.Drawing.Point(200, 10)
        Me.tbFechaDel.Margin = New System.Windows.Forms.Padding(4)
        Me.tbFechaDel.Name = "tbFechaDel"
        Me.tbFechaDel.Size = New System.Drawing.Size(146, 23)
        Me.tbFechaDel.TabIndex = 116
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.grDetalle)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 150)
        Me.GroupPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(1097, 259)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 129
        Me.GroupPanel1.Text = "A U X I L I A R E S"
        '
        'grDetalle
        '
        Me.grDetalle.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grDetalle.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.grDetalle.Location = New System.Drawing.Point(0, 0)
        Me.grDetalle.Margin = New System.Windows.Forms.Padding(4)
        Me.grDetalle.Name = "grDetalle"
        Me.grDetalle.Size = New System.Drawing.Size(1091, 236)
        Me.grDetalle.TabIndex = 2
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(357, 10)
        Me.LabelX1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(52, 28)
        Me.LabelX1.TabIndex = 131
        Me.LabelX1.Text = "AL:"
        '
        'tbFechaAl
        '
        Me.tbFechaAl.Enabled = False
        Me.tbFechaAl.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbFechaAl.Location = New System.Drawing.Point(417, 10)
        Me.tbFechaAl.Margin = New System.Windows.Forms.Padding(4)
        Me.tbFechaAl.Name = "tbFechaAl"
        Me.tbFechaAl.Size = New System.Drawing.Size(146, 23)
        Me.tbFechaAl.TabIndex = 130
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.tbFiltrarFecha)
        Me.GroupPanel2.Controls.Add(Me.tbFechaAl)
        Me.GroupPanel2.Controls.Add(Me.LabelX1)
        Me.GroupPanel2.Controls.Add(Me.tbFechaDel)
        Me.GroupPanel2.Controls.Add(Me.LabelX3)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 70)
        Me.GroupPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(1097, 80)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 132
        Me.GroupPanel2.Text = "FILTRAR POR FECHAS"
        '
        'tbFiltrarFecha
        '
        '
        '
        '
        Me.tbFiltrarFecha.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFiltrarFecha.Location = New System.Drawing.Point(9, 10)
        Me.tbFiltrarFecha.Name = "tbFiltrarFecha"
        Me.tbFiltrarFecha.OffText = "NO"
        Me.tbFiltrarFecha.OnText = "SI"
        Me.tbFiltrarFecha.Size = New System.Drawing.Size(92, 22)
        Me.tbFiltrarFecha.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFiltrarFecha.TabIndex = 132
        '
        'F0_ComprobanteImportar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1097, 452)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Controls.Add(Me.GroupPanel2)
        Me.Controls.Add(Me.PanelInferior)
        Me.Controls.Add(Me.PanelEx1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "F0_ComprobanteImportar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F0_TipoCambio_Nuevo"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Protected WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents MRlAccion As DevComponents.DotNetBar.Controls.ReflectionLabel
    Protected WithEvents PanelInferior As DevComponents.DotNetBar.PanelEx
    Friend WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaDel As DateTimePicker
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Protected WithEvents grDetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaAl As DateTimePicker
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbFiltrarFecha As DevComponents.DotNetBar.Controls.SwitchButton
End Class
