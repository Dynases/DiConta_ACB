<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PR_ResumenVentasDetallado
    Inherits Modelos.ModeloR0

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PR_ResumenVentasDetallado))
        Dim cbSucursal_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbServicio_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbsector_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.cbSucursal = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.cbServicio = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.swFiltroServ = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.cbsector = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbFiltrar = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbfec2 = New System.Windows.Forms.DateTimePicker()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbfec1 = New System.Windows.Forms.DateTimePicker()
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
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.cbServicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbsector, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Size = New System.Drawing.Size(521, 561)
        '
        'MReportViewer
        '
        Me.MReportViewer.Size = New System.Drawing.Size(521, 561)
        '
        'MGPFiltros
        '
        Me.MGPFiltros.Size = New System.Drawing.Size(363, 561)
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
        Me.PanelIzq.Size = New System.Drawing.Size(363, 561)
        Me.PanelIzq.Controls.SetChildIndex(Me.MGPFiltros, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.PanelEx1, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.GroupPanelFiltros, 0)
        '
        'PanelEx1
        '
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
        Me.btnSalir.Text = "&SALIR"
        '
        'btnGenerar
        '
        Me.btnGenerar.Text = "&GENERAR"
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
        Me.GroupPanelFiltros.Controls.Add(Me.cbSucursal)
        Me.GroupPanelFiltros.Controls.Add(Me.LabelX5)
        Me.GroupPanelFiltros.Controls.Add(Me.GroupPanel1)
        Me.GroupPanelFiltros.Controls.Add(Me.LabelX1)
        Me.GroupPanelFiltros.Controls.Add(Me.tbfec2)
        Me.GroupPanelFiltros.Controls.Add(Me.LabelX3)
        Me.GroupPanelFiltros.Controls.Add(Me.tbfec1)
        Me.GroupPanelFiltros.Size = New System.Drawing.Size(363, 485)
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
        'cbSucursal
        '
        Me.cbSucursal.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        cbSucursal_DesignTimeLayout.LayoutString = resources.GetString("cbSucursal_DesignTimeLayout.LayoutString")
        Me.cbSucursal.DesignTimeLayout = cbSucursal_DesignTimeLayout
        Me.cbSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSucursal.Location = New System.Drawing.Point(129, 104)
        Me.cbSucursal.Name = "cbSucursal"
        Me.cbSucursal.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbSucursal.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbSucursal.SelectedIndex = -1
        Me.cbSucursal.SelectedItem = Nothing
        Me.cbSucursal.Size = New System.Drawing.Size(195, 22)
        Me.cbSucursal.TabIndex = 261
        Me.cbSucursal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(23, 103)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(79, 23)
        Me.LabelX5.TabIndex = 262
        Me.LabelX5.Text = "SUCURSAL:"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.cbServicio)
        Me.GroupPanel1.Controls.Add(Me.LabelX6)
        Me.GroupPanel1.Controls.Add(Me.swFiltroServ)
        Me.GroupPanel1.Controls.Add(Me.LabelX7)
        Me.GroupPanel1.Controls.Add(Me.cbsector)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.Controls.Add(Me.tbFiltrar)
        Me.GroupPanel1.Controls.Add(Me.LabelX2)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 163)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(357, 156)
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
        Me.GroupPanel1.TabIndex = 260
        Me.GroupPanel1.Text = "AUXILIAR MODULOS"
        '
        'cbServicio
        '
        Me.cbServicio.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        cbServicio_DesignTimeLayout.LayoutString = resources.GetString("cbServicio_DesignTimeLayout.LayoutString")
        Me.cbServicio.DesignTimeLayout = cbServicio_DesignTimeLayout
        Me.cbServicio.Enabled = False
        Me.cbServicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbServicio.Location = New System.Drawing.Point(100, 99)
        Me.cbServicio.Name = "cbServicio"
        Me.cbServicio.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbServicio.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbServicio.SelectedIndex = -1
        Me.cbServicio.SelectedItem = Nothing
        Me.cbServicio.Size = New System.Drawing.Size(198, 22)
        Me.cbServicio.TabIndex = 246
        Me.cbServicio.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.Location = New System.Drawing.Point(20, 70)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(61, 23)
        Me.LabelX6.TabIndex = 249
        Me.LabelX6.Text = "FILTRAR:"
        '
        'swFiltroServ
        '
        '
        '
        '
        Me.swFiltroServ.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swFiltroServ.Location = New System.Drawing.Point(100, 75)
        Me.swFiltroServ.Margin = New System.Windows.Forms.Padding(2)
        Me.swFiltroServ.Name = "swFiltroServ"
        Me.swFiltroServ.OffText = "NO"
        Me.swFiltroServ.OnText = "SI"
        Me.swFiltroServ.Size = New System.Drawing.Size(68, 18)
        Me.swFiltroServ.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swFiltroServ.TabIndex = 248
        '
        'LabelX7
        '
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX7.Location = New System.Drawing.Point(20, 99)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(92, 23)
        Me.LabelX7.TabIndex = 247
        Me.LabelX7.Text = "SERVICIO:"
        '
        'cbsector
        '
        Me.cbsector.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        cbsector_DesignTimeLayout.LayoutString = resources.GetString("cbsector_DesignTimeLayout.LayoutString")
        Me.cbsector.DesignTimeLayout = cbsector_DesignTimeLayout
        Me.cbsector.Enabled = False
        Me.cbsector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbsector.Location = New System.Drawing.Point(100, 39)
        Me.cbsector.Name = "cbsector"
        Me.cbsector.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbsector.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbsector.SelectedIndex = -1
        Me.cbsector.SelectedItem = Nothing
        Me.cbsector.Size = New System.Drawing.Size(178, 22)
        Me.cbsector.TabIndex = 241
        Me.cbsector.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(20, 13)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(61, 23)
        Me.LabelX4.TabIndex = 245
        Me.LabelX4.Text = "FILTRAR:"
        '
        'tbFiltrar
        '
        '
        '
        '
        Me.tbFiltrar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFiltrar.Location = New System.Drawing.Point(100, 16)
        Me.tbFiltrar.Margin = New System.Windows.Forms.Padding(2)
        Me.tbFiltrar.Name = "tbFiltrar"
        Me.tbFiltrar.OffText = "NO"
        Me.tbFiltrar.OnText = "SI"
        Me.tbFiltrar.Size = New System.Drawing.Size(68, 18)
        Me.tbFiltrar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFiltrar.TabIndex = 244
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(20, 41)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(71, 23)
        Me.LabelX2.TabIndex = 243
        Me.LabelX2.Text = "SECTOR:"
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(23, 62)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(97, 23)
        Me.LabelX1.TabIndex = 259
        Me.LabelX1.Text = "FECHA HASTA:"
        '
        'tbfec2
        '
        Me.tbfec2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbfec2.Location = New System.Drawing.Point(128, 63)
        Me.tbfec2.Name = "tbfec2"
        Me.tbfec2.Size = New System.Drawing.Size(118, 22)
        Me.tbfec2.TabIndex = 258
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(23, 34)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(82, 23)
        Me.LabelX3.TabIndex = 257
        Me.LabelX3.Text = "FECHA DEL:"
        '
        'tbfec1
        '
        Me.tbfec1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbfec1.Location = New System.Drawing.Point(128, 35)
        Me.tbfec1.Name = "tbfec1"
        Me.tbfec1.Size = New System.Drawing.Size(118, 22)
        Me.tbfec1.TabIndex = 256
        '
        'PR_ResumenVentasDetallado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "PR_ResumenVentasDetallado"
        Me.Text = "PR_ResumenVentasDetallado"
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
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.cbServicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbsector, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cbSucursal As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cbsector As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFiltrar As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbfec2 As DateTimePicker
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbfec1 As DateTimePicker
    Friend WithEvents cbServicio As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents swFiltroServ As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
End Class
