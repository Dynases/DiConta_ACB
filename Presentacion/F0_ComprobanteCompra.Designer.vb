<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F0_ComprobanteCompra
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_ComprobanteCompra))
        Dim tbTipo_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.MRlAccion = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.PanelInferior = New DevComponents.DotNetBar.PanelEx()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbinrofactura = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbDui = New DevComponents.Editors.IntegerInput()
        Me.GroupPanel5 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbCreditoFiscal = New DevComponents.Editors.DoubleInput()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbImporteBaseCreditoFiscal = New DevComponents.Editors.DoubleInput()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.tbSujetoCreditoFiscal = New DevComponents.Editors.DoubleInput()
        Me.TbdMontoFactura = New DevComponents.Editors.DoubleInput()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.TbSubTotal = New DevComponents.Editors.DoubleInput()
        Me.TbdDescuento = New DevComponents.Editors.DoubleInput()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbTipo = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.DtiFechaFactura = New System.Windows.Forms.DateTimePicker()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.TbNit = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.TbCodigoControl = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.TbNroAutorizacion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.TbRazonSocial = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.MEP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PanelEx1.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        CType(Me.tbDui, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel5.SuspendLayout()
        CType(Me.tbCreditoFiscal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbImporteBaseCreditoFiscal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSujetoCreditoFiscal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TbdMontoFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TbSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TbdDescuento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTipo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.PanelEx1.Size = New System.Drawing.Size(886, 70)
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
        Me.MRlAccion.Text = "<b><font size=""-8"">INGRESAR COMPRA<font color=""#FF0000""></font></font></b>"
        '
        'PanelInferior
        '
        Me.PanelInferior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelInferior.Controls.Add(Me.ButtonX2)
        Me.PanelInferior.Controls.Add(Me.ButtonX1)
        Me.PanelInferior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 405)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(886, 43)
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
        Me.ButtonX2.TabIndex = 15
        Me.ButtonX2.Text = "SALIR"
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
        Me.ButtonX1.TabIndex = 14
        Me.ButtonX1.Text = "GRABAR"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.tbinrofactura)
        Me.GroupPanel2.Controls.Add(Me.tbDui)
        Me.GroupPanel2.Controls.Add(Me.GroupPanel5)
        Me.GroupPanel2.Controls.Add(Me.LabelX1)
        Me.GroupPanel2.Controls.Add(Me.tbTipo)
        Me.GroupPanel2.Controls.Add(Me.LabelX6)
        Me.GroupPanel2.Controls.Add(Me.DtiFechaFactura)
        Me.GroupPanel2.Controls.Add(Me.LabelX8)
        Me.GroupPanel2.Controls.Add(Me.TbNit)
        Me.GroupPanel2.Controls.Add(Me.TbCodigoControl)
        Me.GroupPanel2.Controls.Add(Me.LabelX9)
        Me.GroupPanel2.Controls.Add(Me.TbNroAutorizacion)
        Me.GroupPanel2.Controls.Add(Me.TbRazonSocial)
        Me.GroupPanel2.Controls.Add(Me.LabelX10)
        Me.GroupPanel2.Controls.Add(Me.LabelX11)
        Me.GroupPanel2.Controls.Add(Me.LabelX13)
        Me.GroupPanel2.Controls.Add(Me.LabelX12)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 70)
        Me.GroupPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(886, 335)
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
        Me.GroupPanel2.Text = "DATOS"
        '
        'tbinrofactura
        '
        '
        '
        '
        Me.tbinrofactura.Border.Class = "TextBoxBorder"
        Me.tbinrofactura.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbinrofactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbinrofactura.Location = New System.Drawing.Point(155, 174)
        Me.tbinrofactura.MaxLength = 18
        Me.tbinrofactura.Name = "tbinrofactura"
        Me.tbinrofactura.PreventEnterBeep = True
        Me.tbinrofactura.Size = New System.Drawing.Size(150, 23)
        Me.tbinrofactura.TabIndex = 4
        '
        'tbDui
        '
        '
        '
        '
        Me.tbDui.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbDui.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbDui.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbDui.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDui.Location = New System.Drawing.Point(154, 205)
        Me.tbDui.MinValue = 0
        Me.tbDui.Name = "tbDui"
        Me.tbDui.Size = New System.Drawing.Size(150, 23)
        Me.tbDui.TabIndex = 5
        '
        'GroupPanel5
        '
        Me.GroupPanel5.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel5.Controls.Add(Me.tbCreditoFiscal)
        Me.GroupPanel5.Controls.Add(Me.LabelX4)
        Me.GroupPanel5.Controls.Add(Me.tbImporteBaseCreditoFiscal)
        Me.GroupPanel5.Controls.Add(Me.LabelX3)
        Me.GroupPanel5.Controls.Add(Me.LabelX2)
        Me.GroupPanel5.Controls.Add(Me.LabelX14)
        Me.GroupPanel5.Controls.Add(Me.tbSujetoCreditoFiscal)
        Me.GroupPanel5.Controls.Add(Me.TbdMontoFactura)
        Me.GroupPanel5.Controls.Add(Me.LabelX15)
        Me.GroupPanel5.Controls.Add(Me.TbSubTotal)
        Me.GroupPanel5.Controls.Add(Me.TbdDescuento)
        Me.GroupPanel5.Controls.Add(Me.LabelX16)
        Me.GroupPanel5.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel5.Location = New System.Drawing.Point(458, 15)
        Me.GroupPanel5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel5.Name = "GroupPanel5"
        Me.GroupPanel5.Size = New System.Drawing.Size(378, 225)
        '
        '
        '
        Me.GroupPanel5.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel5.Style.BackColorGradientAngle = 90
        Me.GroupPanel5.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel5.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderBottomWidth = 1
        Me.GroupPanel5.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel5.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderLeftWidth = 1
        Me.GroupPanel5.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderRightWidth = 1
        Me.GroupPanel5.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderTopWidth = 1
        Me.GroupPanel5.Style.CornerDiameter = 4
        Me.GroupPanel5.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel5.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel5.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel5.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel5.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel5.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel5.TabIndex = 8
        Me.GroupPanel5.Text = "MONTOS"
        '
        'tbCreditoFiscal
        '
        '
        '
        '
        Me.tbCreditoFiscal.BackgroundStyle.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tbCreditoFiscal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbCreditoFiscal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCreditoFiscal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbCreditoFiscal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCreditoFiscal.Increment = 1.0R
        Me.tbCreditoFiscal.IsInputReadOnly = True
        Me.tbCreditoFiscal.Location = New System.Drawing.Point(219, 163)
        Me.tbCreditoFiscal.MinValue = 0R
        Me.tbCreditoFiscal.Name = "tbCreditoFiscal"
        Me.tbCreditoFiscal.Size = New System.Drawing.Size(135, 23)
        Me.tbCreditoFiscal.TabIndex = 13
        '
        'LabelX4
        '
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(12, 163)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(200, 23)
        Me.LabelX4.TabIndex = 34
        Me.LabelX4.Text = "Crédito Fiscal:"
        '
        'tbImporteBaseCreditoFiscal
        '
        '
        '
        '
        Me.tbImporteBaseCreditoFiscal.BackgroundStyle.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tbImporteBaseCreditoFiscal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbImporteBaseCreditoFiscal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbImporteBaseCreditoFiscal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbImporteBaseCreditoFiscal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbImporteBaseCreditoFiscal.Increment = 1.0R
        Me.tbImporteBaseCreditoFiscal.IsInputReadOnly = True
        Me.tbImporteBaseCreditoFiscal.Location = New System.Drawing.Point(219, 131)
        Me.tbImporteBaseCreditoFiscal.MinValue = 0R
        Me.tbImporteBaseCreditoFiscal.Name = "tbImporteBaseCreditoFiscal"
        Me.tbImporteBaseCreditoFiscal.Size = New System.Drawing.Size(135, 23)
        Me.tbImporteBaseCreditoFiscal.TabIndex = 12
        '
        'LabelX3
        '
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(12, 131)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(200, 23)
        Me.LabelX3.TabIndex = 32
        Me.LabelX3.Text = "Importe Base Cre. Fiscal:"
        '
        'LabelX2
        '
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(12, 70)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(200, 23)
        Me.LabelX2.TabIndex = 30
        Me.LabelX2.Text = "No Sujeto a Crédito Fiscal:"
        '
        'LabelX14
        '
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX14.Location = New System.Drawing.Point(12, 3)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.Size = New System.Drawing.Size(200, 23)
        Me.LabelX14.TabIndex = 25
        Me.LabelX14.Text = "Total:"
        '
        'tbSujetoCreditoFiscal
        '
        '
        '
        '
        Me.tbSujetoCreditoFiscal.BackgroundStyle.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tbSujetoCreditoFiscal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbSujetoCreditoFiscal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbSujetoCreditoFiscal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbSujetoCreditoFiscal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSujetoCreditoFiscal.Increment = 1.0R
        Me.tbSujetoCreditoFiscal.IsInputReadOnly = True
        Me.tbSujetoCreditoFiscal.Location = New System.Drawing.Point(219, 67)
        Me.tbSujetoCreditoFiscal.MinValue = 0R
        Me.tbSujetoCreditoFiscal.Name = "tbSujetoCreditoFiscal"
        Me.tbSujetoCreditoFiscal.Size = New System.Drawing.Size(135, 23)
        Me.tbSujetoCreditoFiscal.TabIndex = 10
        '
        'TbdMontoFactura
        '
        '
        '
        '
        Me.TbdMontoFactura.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.TbdMontoFactura.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbdMontoFactura.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.TbdMontoFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbdMontoFactura.Increment = 1.0R
        Me.TbdMontoFactura.Location = New System.Drawing.Point(219, 3)
        Me.TbdMontoFactura.MinValue = 0R
        Me.TbdMontoFactura.Name = "TbdMontoFactura"
        Me.TbdMontoFactura.Size = New System.Drawing.Size(135, 23)
        Me.TbdMontoFactura.TabIndex = 8
        '
        'LabelX15
        '
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX15.Location = New System.Drawing.Point(12, 38)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.Size = New System.Drawing.Size(200, 23)
        Me.LabelX15.TabIndex = 27
        Me.LabelX15.Text = "Sujeto a Crédito Fiscal:"
        '
        'TbSubTotal
        '
        '
        '
        '
        Me.TbSubTotal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.TbSubTotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbSubTotal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.TbSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbSubTotal.Increment = 1.0R
        Me.TbSubTotal.Location = New System.Drawing.Point(219, 35)
        Me.TbSubTotal.MinValue = 0R
        Me.TbSubTotal.Name = "TbSubTotal"
        Me.TbSubTotal.Size = New System.Drawing.Size(135, 23)
        Me.TbSubTotal.TabIndex = 9
        '
        'TbdDescuento
        '
        '
        '
        '
        Me.TbdDescuento.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.TbdDescuento.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbdDescuento.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.TbdDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbdDescuento.Increment = 1.0R
        Me.TbdDescuento.Location = New System.Drawing.Point(219, 99)
        Me.TbdDescuento.MinValue = 0R
        Me.TbdDescuento.Name = "TbdDescuento"
        Me.TbdDescuento.Size = New System.Drawing.Size(135, 23)
        Me.TbdDescuento.TabIndex = 11
        '
        'LabelX16
        '
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX16.Location = New System.Drawing.Point(12, 99)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.Size = New System.Drawing.Size(200, 23)
        Me.LabelX16.TabIndex = 29
        Me.LabelX16.Text = "Descuento:"
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(8, 205)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(138, 23)
        Me.LabelX1.TabIndex = 114
        Me.LabelX1.Text = "DUI:"
        '
        'tbTipo
        '
        tbTipo_DesignTimeLayout.LayoutString = resources.GetString("tbTipo_DesignTimeLayout.LayoutString")
        Me.tbTipo.DesignTimeLayout = tbTipo_DesignTimeLayout
        Me.tbTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTipo.Location = New System.Drawing.Point(154, 15)
        Me.tbTipo.Margin = New System.Windows.Forms.Padding(4)
        Me.tbTipo.Name = "tbTipo"
        Me.tbTipo.SelectedIndex = -1
        Me.tbTipo.SelectedItem = Nothing
        Me.tbTipo.Size = New System.Drawing.Size(250, 20)
        Me.tbTipo.TabIndex = 0
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.Location = New System.Drawing.Point(9, 13)
        Me.LabelX6.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(128, 28)
        Me.LabelX6.TabIndex = 112
        Me.LabelX6.Text = "Tipo de Compra:"
        '
        'DtiFechaFactura
        '
        Me.DtiFechaFactura.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtiFechaFactura.Location = New System.Drawing.Point(155, 45)
        Me.DtiFechaFactura.Name = "DtiFechaFactura"
        Me.DtiFechaFactura.Size = New System.Drawing.Size(149, 22)
        Me.DtiFechaFactura.TabIndex = 1
        '
        'LabelX8
        '
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.Location = New System.Drawing.Point(9, 42)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(114, 23)
        Me.LabelX8.TabIndex = 15
        Me.LabelX8.Text = "Fecha Factura:"
        '
        'TbNit
        '
        '
        '
        '
        Me.TbNit.Border.Class = "TextBoxBorder"
        Me.TbNit.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbNit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbNit.Location = New System.Drawing.Point(154, 77)
        Me.TbNit.MaxLength = 20
        Me.TbNit.Name = "TbNit"
        Me.TbNit.PreventEnterBeep = True
        Me.TbNit.Size = New System.Drawing.Size(150, 23)
        Me.TbNit.TabIndex = 2
        '
        'TbCodigoControl
        '
        '
        '
        '
        Me.TbCodigoControl.Border.Class = "TextBoxBorder"
        Me.TbCodigoControl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbCodigoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbCodigoControl.Location = New System.Drawing.Point(154, 269)
        Me.TbCodigoControl.MaxLength = 14
        Me.TbCodigoControl.Name = "TbCodigoControl"
        Me.TbCodigoControl.PreventEnterBeep = True
        Me.TbCodigoControl.Size = New System.Drawing.Size(150, 23)
        Me.TbCodigoControl.TabIndex = 7
        '
        'LabelX9
        '
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.Location = New System.Drawing.Point(9, 77)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(114, 23)
        Me.LabelX9.TabIndex = 16
        Me.LabelX9.Text = "Nit Proveedor:"
        '
        'TbNroAutorizacion
        '
        '
        '
        '
        Me.TbNroAutorizacion.Border.Class = "TextBoxBorder"
        Me.TbNroAutorizacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbNroAutorizacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbNroAutorizacion.Location = New System.Drawing.Point(154, 237)
        Me.TbNroAutorizacion.MaxLength = 18
        Me.TbNroAutorizacion.Name = "TbNroAutorizacion"
        Me.TbNroAutorizacion.PreventEnterBeep = True
        Me.TbNroAutorizacion.Size = New System.Drawing.Size(150, 23)
        Me.TbNroAutorizacion.TabIndex = 6
        '
        'TbRazonSocial
        '
        '
        '
        '
        Me.TbRazonSocial.Border.Class = "TextBoxBorder"
        Me.TbRazonSocial.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbRazonSocial.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbRazonSocial.Location = New System.Drawing.Point(154, 109)
        Me.TbRazonSocial.MaxLength = 200
        Me.TbRazonSocial.Multiline = True
        Me.TbRazonSocial.Name = "TbRazonSocial"
        Me.TbRazonSocial.PreventEnterBeep = True
        Me.TbRazonSocial.Size = New System.Drawing.Size(250, 58)
        Me.TbRazonSocial.TabIndex = 3
        '
        'LabelX10
        '
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX10.Location = New System.Drawing.Point(9, 109)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(114, 23)
        Me.LabelX10.TabIndex = 18
        Me.LabelX10.Text = "Razon Social:"
        '
        'LabelX11
        '
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX11.Location = New System.Drawing.Point(10, 173)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.Size = New System.Drawing.Size(114, 23)
        Me.LabelX11.TabIndex = 20
        Me.LabelX11.Text = "Nro. Factura:"
        '
        'LabelX13
        '
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.Location = New System.Drawing.Point(8, 271)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(138, 23)
        Me.LabelX13.TabIndex = 24
        Me.LabelX13.Text = "Código Control:"
        '
        'LabelX12
        '
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX12.Location = New System.Drawing.Point(8, 239)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.Size = New System.Drawing.Size(138, 23)
        Me.LabelX12.TabIndex = 22
        Me.LabelX12.Text = "Nro. Autorización:"
        '
        'MEP
        '
        Me.MEP.ContainerControl = Me
        '
        'F0_ComprobanteCompra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(886, 448)
        Me.Controls.Add(Me.GroupPanel2)
        Me.Controls.Add(Me.PanelInferior)
        Me.Controls.Add(Me.PanelEx1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "F0_ComprobanteCompra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F0_TipoCambio_Nuevo"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel2.PerformLayout()
        CType(Me.tbDui, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel5.ResumeLayout(False)
        CType(Me.tbCreditoFiscal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbImporteBaseCreditoFiscal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSujetoCreditoFiscal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TbdMontoFactura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TbSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TbdDescuento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTipo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Protected WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents MRlAccion As DevComponents.DotNetBar.Controls.ReflectionLabel
    Protected WithEvents PanelInferior As DevComponents.DotNetBar.PanelEx
    Friend WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Protected WithEvents MEP As ErrorProvider
    Friend WithEvents TbdDescuento As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbSubTotal As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbNit As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents TbCodigoControl As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbNroAutorizacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents TbRazonSocial As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents TbdMontoFactura As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents DtiFechaFactura As DateTimePicker
    Friend WithEvents tbTipo As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbSujetoCreditoFiscal As DevComponents.Editors.DoubleInput
    Friend WithEvents GroupPanel5 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbDui As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCreditoFiscal As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbImporteBaseCreditoFiscal As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbinrofactura As DevComponents.DotNetBar.Controls.TextBoxX
End Class
