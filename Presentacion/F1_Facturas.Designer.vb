<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F1_Facturas
    Inherits Modelos.ModeloF1

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1_Facturas))
        Dim tbTipo_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbNumi = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
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
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
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
        Me.TbiNroFactura = New DevComponents.Editors.IntegerInput()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.MPanelSup.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.GroupPanelBuscador.SuspendLayout()
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.TbiNroFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SuperTabPrincipal
        '
        '
        '
        '
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.MenuBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabPrincipal.ControlBox.MenuBox, Me.SuperTabPrincipal.ControlBox.CloseBox})
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanelBuscador.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(1101, 624)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(1102, 624)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelSuperior.Size = New System.Drawing.Size(1102, 89)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        '
        'PanelInferior
        '
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelInferior.Size = New System.Drawing.Size(1102, 44)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.DarkSlateGray
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
        'TxtNombreUsu
        '
        Me.TxtNombreUsu.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtNombreUsu.Size = New System.Drawing.Size(267, 44)
        '
        'btnSalir
        '
        '
        'btnModificar
        '
        '
        'btnNuevo
        '
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Location = New System.Drawing.Point(995, 0)
        Me.PanelToolBar2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        '
        'MPanelSup
        '
        Me.MPanelSup.Controls.Add(Me.GroupPanel2)
        Me.MPanelSup.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MPanelSup.Size = New System.Drawing.Size(1102, 353)
        Me.MPanelSup.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.MPanelSup.Controls.SetChildIndex(Me.GroupPanel2, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelPrincipal.Size = New System.Drawing.Size(1102, 491)
        '
        'GroupPanelBuscador
        '
        Me.GroupPanelBuscador.Location = New System.Drawing.Point(0, 353)
        Me.GroupPanelBuscador.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupPanelBuscador.Size = New System.Drawing.Size(1102, 138)
        '
        '
        '
        Me.GroupPanelBuscador.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanelBuscador.Style.BackColorGradientAngle = 90
        Me.GroupPanelBuscador.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanelBuscador.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderBottomWidth = 1
        Me.GroupPanelBuscador.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanelBuscador.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderLeftWidth = 1
        Me.GroupPanelBuscador.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderRightWidth = 1
        Me.GroupPanelBuscador.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderTopWidth = 1
        Me.GroupPanelBuscador.Style.CornerDiameter = 4
        Me.GroupPanelBuscador.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelBuscador.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelBuscador.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanelBuscador.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'JGrM_Buscador
        '
        Me.JGrM_Buscador.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.JGrM_Buscador.Size = New System.Drawing.Size(1096, 111)
        '
        'PanelUsuario
        '
        Me.PanelUsuario.Location = New System.Drawing.Point(868, 36)
        Me.PanelUsuario.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(171, 0)
        Me.btnUltimo.Margin = New System.Windows.Forms.Padding(2)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(835, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MRlAccion.Size = New System.Drawing.Size(494, 89)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(643, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(54, 4)
        Me.LabelX1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(100, 28)
        Me.LabelX1.TabIndex = 38
        Me.LabelX1.Text = "Cod:"
        '
        'tbNumi
        '
        '
        '
        '
        Me.tbNumi.Border.Class = "TextBoxBorder"
        Me.tbNumi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNumi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNumi.Location = New System.Drawing.Point(188, 4)
        Me.tbNumi.Margin = New System.Windows.Forms.Padding(4)
        Me.tbNumi.Name = "tbNumi"
        Me.tbNumi.PreventEnterBeep = True
        Me.tbNumi.Size = New System.Drawing.Size(133, 26)
        Me.tbNumi.TabIndex = 37
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.tbDui)
        Me.GroupPanel2.Controls.Add(Me.GroupPanel5)
        Me.GroupPanel2.Controls.Add(Me.LabelX5)
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
        Me.GroupPanel2.Controls.Add(Me.TbiNroFactura)
        Me.GroupPanel2.Controls.Add(Me.LabelX12)
        Me.GroupPanel2.Controls.Add(Me.tbNumi)
        Me.GroupPanel2.Controls.Add(Me.LabelX1)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(1102, 353)
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
        Me.GroupPanel2.TabIndex = 133
        Me.GroupPanel2.Text = "DATOS"
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
        Me.tbDui.Location = New System.Drawing.Point(188, 228)
        Me.tbDui.MinValue = 0
        Me.tbDui.Name = "tbDui"
        Me.tbDui.Size = New System.Drawing.Size(150, 26)
        Me.tbDui.TabIndex = 120
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
        Me.GroupPanel5.Location = New System.Drawing.Point(492, 38)
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
        Me.GroupPanel5.TabIndex = 123
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
        Me.tbCreditoFiscal.Size = New System.Drawing.Size(135, 26)
        Me.tbCreditoFiscal.TabIndex = 4
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
        Me.tbImporteBaseCreditoFiscal.Size = New System.Drawing.Size(135, 26)
        Me.tbImporteBaseCreditoFiscal.TabIndex = 3
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
        Me.tbSujetoCreditoFiscal.Size = New System.Drawing.Size(135, 26)
        Me.tbSujetoCreditoFiscal.TabIndex = 5
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
        Me.TbdMontoFactura.Size = New System.Drawing.Size(135, 26)
        Me.TbdMontoFactura.TabIndex = 0
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
        Me.TbSubTotal.Size = New System.Drawing.Size(135, 26)
        Me.TbSubTotal.TabIndex = 1
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
        Me.TbdDescuento.Size = New System.Drawing.Size(135, 26)
        Me.TbdDescuento.TabIndex = 2
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
        'LabelX5
        '
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(42, 228)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(138, 23)
        Me.LabelX5.TabIndex = 131
        Me.LabelX5.Text = "DUI:"
        '
        'tbTipo
        '
        tbTipo_DesignTimeLayout.LayoutString = resources.GetString("tbTipo_DesignTimeLayout.LayoutString")
        Me.tbTipo.DesignTimeLayout = tbTipo_DesignTimeLayout
        Me.tbTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTipo.Location = New System.Drawing.Point(188, 38)
        Me.tbTipo.Margin = New System.Windows.Forms.Padding(4)
        Me.tbTipo.Name = "tbTipo"
        Me.tbTipo.SelectedIndex = -1
        Me.tbTipo.SelectedItem = Nothing
        Me.tbTipo.Size = New System.Drawing.Size(250, 23)
        Me.tbTipo.TabIndex = 115
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.Location = New System.Drawing.Point(43, 36)
        Me.LabelX6.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(128, 28)
        Me.LabelX6.TabIndex = 130
        Me.LabelX6.Text = "Tipo de Compra:"
        '
        'DtiFechaFactura
        '
        Me.DtiFechaFactura.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtiFechaFactura.Location = New System.Drawing.Point(189, 68)
        Me.DtiFechaFactura.Name = "DtiFechaFactura"
        Me.DtiFechaFactura.Size = New System.Drawing.Size(149, 26)
        Me.DtiFechaFactura.TabIndex = 116
        '
        'LabelX8
        '
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.Location = New System.Drawing.Point(43, 65)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(114, 23)
        Me.LabelX8.TabIndex = 124
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
        Me.TbNit.Location = New System.Drawing.Point(188, 100)
        Me.TbNit.MaxLength = 20
        Me.TbNit.Name = "TbNit"
        Me.TbNit.PreventEnterBeep = True
        Me.TbNit.Size = New System.Drawing.Size(150, 26)
        Me.TbNit.TabIndex = 117
        '
        'TbCodigoControl
        '
        '
        '
        '
        Me.TbCodigoControl.Border.Class = "TextBoxBorder"
        Me.TbCodigoControl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbCodigoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbCodigoControl.Location = New System.Drawing.Point(188, 292)
        Me.TbCodigoControl.MaxLength = 14
        Me.TbCodigoControl.Name = "TbCodigoControl"
        Me.TbCodigoControl.PreventEnterBeep = True
        Me.TbCodigoControl.Size = New System.Drawing.Size(150, 26)
        Me.TbCodigoControl.TabIndex = 122
        '
        'LabelX9
        '
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.Location = New System.Drawing.Point(43, 100)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(114, 23)
        Me.LabelX9.TabIndex = 125
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
        Me.TbNroAutorizacion.Location = New System.Drawing.Point(188, 260)
        Me.TbNroAutorizacion.MaxLength = 18
        Me.TbNroAutorizacion.Name = "TbNroAutorizacion"
        Me.TbNroAutorizacion.PreventEnterBeep = True
        Me.TbNroAutorizacion.Size = New System.Drawing.Size(150, 26)
        Me.TbNroAutorizacion.TabIndex = 121
        '
        'TbRazonSocial
        '
        '
        '
        '
        Me.TbRazonSocial.Border.Class = "TextBoxBorder"
        Me.TbRazonSocial.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbRazonSocial.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbRazonSocial.Location = New System.Drawing.Point(188, 132)
        Me.TbRazonSocial.MaxLength = 50
        Me.TbRazonSocial.Multiline = True
        Me.TbRazonSocial.Name = "TbRazonSocial"
        Me.TbRazonSocial.PreventEnterBeep = True
        Me.TbRazonSocial.Size = New System.Drawing.Size(250, 58)
        Me.TbRazonSocial.TabIndex = 118
        '
        'LabelX10
        '
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX10.Location = New System.Drawing.Point(43, 132)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(114, 23)
        Me.LabelX10.TabIndex = 126
        Me.LabelX10.Text = "Razon Social:"
        '
        'LabelX11
        '
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX11.Location = New System.Drawing.Point(44, 196)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.Size = New System.Drawing.Size(114, 23)
        Me.LabelX11.TabIndex = 127
        Me.LabelX11.Text = "Nro. Factura:"
        '
        'LabelX13
        '
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.Location = New System.Drawing.Point(42, 294)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(138, 23)
        Me.LabelX13.TabIndex = 129
        Me.LabelX13.Text = "Código Control:"
        '
        'TbiNroFactura
        '
        '
        '
        '
        Me.TbiNroFactura.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.TbiNroFactura.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbiNroFactura.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.TbiNroFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbiNroFactura.Location = New System.Drawing.Point(189, 196)
        Me.TbiNroFactura.MinValue = 0
        Me.TbiNroFactura.Name = "TbiNroFactura"
        Me.TbiNroFactura.Size = New System.Drawing.Size(149, 26)
        Me.TbiNroFactura.TabIndex = 119
        '
        'LabelX12
        '
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX12.Location = New System.Drawing.Point(42, 262)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.Size = New System.Drawing.Size(138, 23)
        Me.LabelX12.TabIndex = 128
        Me.LabelX12.Text = "Nro. Autorización:"
        '
        'F1_Facturas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 624)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "F1_Facturas"
        Me.Text = "F1_Personal"
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.MPanelSup.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.GroupPanelBuscador.ResumeLayout(False)
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.TbiNroFactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNumi As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbDui As DevComponents.Editors.IntegerInput
    Friend WithEvents GroupPanel5 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbCreditoFiscal As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbImporteBaseCreditoFiscal As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbSujetoCreditoFiscal As DevComponents.Editors.DoubleInput
    Friend WithEvents TbdMontoFactura As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbSubTotal As DevComponents.Editors.DoubleInput
    Friend WithEvents TbdDescuento As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbTipo As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents DtiFechaFactura As DateTimePicker
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbNit As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents TbCodigoControl As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbNroAutorizacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents TbRazonSocial As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbiNroFactura As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
End Class
