<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PR_LibroMayor
    Inherits Modelos.ModeloF0

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PR_LibroMayor))
        Dim cbAuxiliar02_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbAuxiliar01_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim tbMoneda_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.GroupPanel4 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lbAuxiliar02 = New DevComponents.DotNetBar.LabelX()
        Me.cbAuxiliar02 = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.swAuxiliar02 = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lbAuxiliar01 = New DevComponents.DotNetBar.LabelX()
        Me.cbAuxiliar01 = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.swAuxiliar01 = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbReferencia = New System.Windows.Forms.TextBox()
        Me.tbFiltrarRef = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbMeses = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbCliente = New System.Windows.Forms.TextBox()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbMoneda = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaDel = New System.Windows.Forms.DateTimePicker()
        Me.tbCuenta = New System.Windows.Forms.TextBox()
        Me.tbFechaAl = New System.Windows.Forms.DateTimePicker()
        Me.tbNumi = New System.Windows.Forms.TextBox()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.gpGrilla = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grDetalle = New Janus.Windows.GridEX.GridEX()
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel1.SuspendLayout()
        Me.GroupPanel4.SuspendLayout()
        CType(Me.cbAuxiliar02, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel3.SuspendLayout()
        CType(Me.cbAuxiliar01, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.tbMoneda, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpGrilla.SuspendLayout()
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SuperTabControlPanelBuscador.Margin = New System.Windows.Forms.Padding(4)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(952, 561)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Margin = New System.Windows.Forms.Padding(4)
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(952, 561)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelSuperior.Size = New System.Drawing.Size(952, 72)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackgroundImage = CType(resources.GetObject("PanelSuperior.StyleMouseOver.BackgroundImage"), System.Drawing.Image)
        '
        'PanelInferior
        '
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelInferior.Size = New System.Drawing.Size(952, 39)
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
        'TxtNombreUsu
        '
        Me.TxtNombreUsu.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtNombreUsu.Size = New System.Drawing.Size(135, 32)
        '
        'btnSalir
        '
        Me.btnSalir.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSalir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSalir.Size = New System.Drawing.Size(117, 72)
        Me.btnSalir.Text = "&SALIR"
        '
        'btnGrabar
        '
        Me.btnGrabar.Image = Global.Presentacion.My.Resources.Resources.reporte
        Me.btnGrabar.Text = "&GENERAR"
        '
        'btnEliminar
        '
        Me.btnEliminar.Image = Global.Presentacion.My.Resources.Resources.printer
        Me.btnEliminar.Text = "IM&PRIMIR"
        '
        'btnModificar
        '
        Me.btnModificar.Visible = False
        '
        'btnNuevo
        '
        Me.btnNuevo.Visible = False
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Location = New System.Drawing.Point(872, 0)
        Me.PanelToolBar2.Margin = New System.Windows.Forms.Padding(4)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Controls.Add(Me.gpGrilla)
        Me.PanelPrincipal.Controls.Add(Me.GroupPanel1)
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelPrincipal.Size = New System.Drawing.Size(952, 450)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.GroupPanel1, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.gpGrilla, 0)
        '
        'PanelUsuario
        '
        Me.PanelUsuario.Location = New System.Drawing.Point(662, 3)
        Me.PanelUsuario.Margin = New System.Windows.Forms.Padding(4)
        '
        'btnImprimir
        '
        Me.btnImprimir.Visible = False
        '
        'PanelNavegacion
        '
        Me.PanelNavegacion.Visible = False
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(129, 0)
        Me.btnUltimo.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(752, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(4)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Margin = New System.Windows.Forms.Padding(4)
        Me.MRlAccion.Size = New System.Drawing.Size(496, 72)
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.GroupPanel4)
        Me.GroupPanel1.Controls.Add(Me.GroupPanel3)
        Me.GroupPanel1.Controls.Add(Me.GroupPanel2)
        Me.GroupPanel1.Controls.Add(Me.Panel1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(952, 180)
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
        Me.GroupPanel1.TabIndex = 20
        Me.GroupPanel1.Text = "FILTROS"
        '
        'GroupPanel4
        '
        Me.GroupPanel4.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel4.Controls.Add(Me.lbAuxiliar02)
        Me.GroupPanel4.Controls.Add(Me.cbAuxiliar02)
        Me.GroupPanel4.Controls.Add(Me.swAuxiliar02)
        Me.GroupPanel4.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupPanel4.Location = New System.Drawing.Point(755, 0)
        Me.GroupPanel4.Name = "GroupPanel4"
        Me.GroupPanel4.Size = New System.Drawing.Size(164, 159)
        '
        '
        '
        Me.GroupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel4.Style.BackColorGradientAngle = 90
        Me.GroupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderBottomWidth = 1
        Me.GroupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderLeftWidth = 1
        Me.GroupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderRightWidth = 1
        Me.GroupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderTopWidth = 1
        Me.GroupPanel4.Style.CornerDiameter = 4
        Me.GroupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel4.TabIndex = 250
        Me.GroupPanel4.Text = "AUXILIAR 02"
        '
        'lbAuxiliar02
        '
        Me.lbAuxiliar02.AutoSize = True
        Me.lbAuxiliar02.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbAuxiliar02.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbAuxiliar02.Location = New System.Drawing.Point(3, 35)
        Me.lbAuxiliar02.Name = "lbAuxiliar02"
        Me.lbAuxiliar02.Size = New System.Drawing.Size(114, 15)
        Me.lbAuxiliar02.TabIndex = 248
        Me.lbAuxiliar02.Text = "Seleccione un Auxiliar:"
        Me.lbAuxiliar02.Visible = False
        '
        'cbAuxiliar02
        '
        cbAuxiliar02_DesignTimeLayout.LayoutString = resources.GetString("cbAuxiliar02_DesignTimeLayout.LayoutString")
        Me.cbAuxiliar02.DesignTimeLayout = cbAuxiliar02_DesignTimeLayout
        Me.cbAuxiliar02.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAuxiliar02.Location = New System.Drawing.Point(3, 52)
        Me.cbAuxiliar02.Name = "cbAuxiliar02"
        Me.cbAuxiliar02.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbAuxiliar02.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbAuxiliar02.SelectedIndex = -1
        Me.cbAuxiliar02.SelectedItem = Nothing
        Me.cbAuxiliar02.Size = New System.Drawing.Size(135, 20)
        Me.cbAuxiliar02.TabIndex = 247
        Me.cbAuxiliar02.Visible = False
        '
        'swAuxiliar02
        '
        '
        '
        '
        Me.swAuxiliar02.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swAuxiliar02.Location = New System.Drawing.Point(10, 10)
        Me.swAuxiliar02.Margin = New System.Windows.Forms.Padding(2)
        Me.swAuxiliar02.Name = "swAuxiliar02"
        Me.swAuxiliar02.OffBackColor = System.Drawing.Color.Gold
        Me.swAuxiliar02.OffText = "Uno"
        Me.swAuxiliar02.OnBackColor = System.Drawing.Color.Aqua
        Me.swAuxiliar02.OnText = "Todos"
        Me.swAuxiliar02.ReadOnlyMarkerColor = System.Drawing.Color.Empty
        Me.swAuxiliar02.Size = New System.Drawing.Size(112, 18)
        Me.swAuxiliar02.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swAuxiliar02.TabIndex = 246
        Me.swAuxiliar02.Value = True
        Me.swAuxiliar02.ValueObject = "Y"
        '
        'GroupPanel3
        '
        Me.GroupPanel3.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.lbAuxiliar01)
        Me.GroupPanel3.Controls.Add(Me.cbAuxiliar01)
        Me.GroupPanel3.Controls.Add(Me.swAuxiliar01)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupPanel3.Location = New System.Drawing.Point(589, 0)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(166, 159)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel3.TabIndex = 249
        Me.GroupPanel3.Text = "AUXILIAR 01"
        '
        'lbAuxiliar01
        '
        Me.lbAuxiliar01.AutoSize = True
        Me.lbAuxiliar01.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbAuxiliar01.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbAuxiliar01.Location = New System.Drawing.Point(3, 35)
        Me.lbAuxiliar01.Name = "lbAuxiliar01"
        Me.lbAuxiliar01.Size = New System.Drawing.Size(114, 15)
        Me.lbAuxiliar01.TabIndex = 246
        Me.lbAuxiliar01.Text = "Seleccione un Auxiliar:"
        Me.lbAuxiliar01.Visible = False
        '
        'cbAuxiliar01
        '
        cbAuxiliar01_DesignTimeLayout.LayoutString = resources.GetString("cbAuxiliar01_DesignTimeLayout.LayoutString")
        Me.cbAuxiliar01.DesignTimeLayout = cbAuxiliar01_DesignTimeLayout
        Me.cbAuxiliar01.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAuxiliar01.Location = New System.Drawing.Point(3, 52)
        Me.cbAuxiliar01.Name = "cbAuxiliar01"
        Me.cbAuxiliar01.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbAuxiliar01.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbAuxiliar01.SelectedIndex = -1
        Me.cbAuxiliar01.SelectedItem = Nothing
        Me.cbAuxiliar01.Size = New System.Drawing.Size(135, 20)
        Me.cbAuxiliar01.TabIndex = 245
        Me.cbAuxiliar01.Visible = False
        '
        'swAuxiliar01
        '
        '
        '
        '
        Me.swAuxiliar01.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swAuxiliar01.Location = New System.Drawing.Point(3, 10)
        Me.swAuxiliar01.Margin = New System.Windows.Forms.Padding(2)
        Me.swAuxiliar01.Name = "swAuxiliar01"
        Me.swAuxiliar01.OffBackColor = System.Drawing.Color.Gold
        Me.swAuxiliar01.OffText = "Uno"
        Me.swAuxiliar01.OnBackColor = System.Drawing.Color.Aqua
        Me.swAuxiliar01.OnText = "Todos"
        Me.swAuxiliar01.ReadOnlyMarkerColor = System.Drawing.Color.Empty
        Me.swAuxiliar01.Size = New System.Drawing.Size(112, 18)
        Me.swAuxiliar01.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swAuxiliar01.TabIndex = 244
        Me.swAuxiliar01.Value = True
        Me.swAuxiliar01.ValueObject = "Y"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.tbReferencia)
        Me.GroupPanel2.Controls.Add(Me.tbFiltrarRef)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupPanel2.Location = New System.Drawing.Point(422, 0)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(167, 159)
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
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
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
        Me.GroupPanel2.TabIndex = 247
        Me.GroupPanel2.Text = "FILTRAR POR REFERENCIA"
        '
        'tbReferencia
        '
        Me.tbReferencia.Location = New System.Drawing.Point(3, 35)
        Me.tbReferencia.Multiline = True
        Me.tbReferencia.Name = "tbReferencia"
        Me.tbReferencia.Size = New System.Drawing.Size(157, 57)
        Me.tbReferencia.TabIndex = 245
        '
        'tbFiltrarRef
        '
        '
        '
        '
        Me.tbFiltrarRef.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFiltrarRef.Location = New System.Drawing.Point(3, 10)
        Me.tbFiltrarRef.Margin = New System.Windows.Forms.Padding(2)
        Me.tbFiltrarRef.Name = "tbFiltrarRef"
        Me.tbFiltrarRef.OffText = "NO"
        Me.tbFiltrarRef.OnText = "SI"
        Me.tbFiltrarRef.Size = New System.Drawing.Size(68, 18)
        Me.tbFiltrarRef.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFiltrarRef.TabIndex = 244
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.LabelX1)
        Me.Panel1.Controls.Add(Me.tbMeses)
        Me.Panel1.Controls.Add(Me.LabelX5)
        Me.Panel1.Controls.Add(Me.tbCliente)
        Me.Panel1.Controls.Add(Me.LabelX2)
        Me.Panel1.Controls.Add(Me.tbMoneda)
        Me.Panel1.Controls.Add(Me.LabelX3)
        Me.Panel1.Controls.Add(Me.LabelX4)
        Me.Panel1.Controls.Add(Me.tbFechaDel)
        Me.Panel1.Controls.Add(Me.tbCuenta)
        Me.Panel1.Controls.Add(Me.tbFechaAl)
        Me.Panel1.Controls.Add(Me.tbNumi)
        Me.Panel1.Controls.Add(Me.LabelX6)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(422, 159)
        Me.Panel1.TabIndex = 248
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(26, 2)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(61, 23)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "CUENTA"
        '
        'tbMeses
        '
        '
        '
        '
        Me.tbMeses.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMeses.Location = New System.Drawing.Point(114, 128)
        Me.tbMeses.Margin = New System.Windows.Forms.Padding(2)
        Me.tbMeses.Name = "tbMeses"
        Me.tbMeses.OffText = "SIN TOTAL POR MES"
        Me.tbMeses.OnText = "CON TOTAL POR MES"
        Me.tbMeses.Size = New System.Drawing.Size(154, 18)
        Me.tbMeses.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbMeses.TabIndex = 129
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(58, 50)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(52, 23)
        Me.LabelX5.TabIndex = 127
        Me.LabelX5.Text = "CLIENTE:"
        '
        'tbCliente
        '
        Me.tbCliente.Location = New System.Drawing.Point(116, 52)
        Me.tbCliente.Name = "tbCliente"
        Me.tbCliente.ReadOnly = True
        Me.tbCliente.Size = New System.Drawing.Size(239, 20)
        Me.tbCliente.TabIndex = 126
        Me.tbCliente.Tag = "0"
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(116, 76)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(61, 23)
        Me.LabelX2.TabIndex = 1
        Me.LabelX2.Text = "DESDE"
        '
        'tbMoneda
        '
        Me.tbMoneda.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        tbMoneda_DesignTimeLayout.LayoutString = resources.GetString("tbMoneda_DesignTimeLayout.LayoutString")
        Me.tbMoneda.DesignTimeLayout = tbMoneda_DesignTimeLayout
        Me.tbMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMoneda.Location = New System.Drawing.Point(328, 102)
        Me.tbMoneda.Name = "tbMoneda"
        Me.tbMoneda.SelectedIndex = -1
        Me.tbMoneda.SelectedItem = Nothing
        Me.tbMoneda.Size = New System.Drawing.Size(78, 22)
        Me.tbMoneda.TabIndex = 124
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(225, 76)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(61, 23)
        Me.LabelX3.TabIndex = 2
        Me.LabelX3.Text = "HASTA"
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(328, 76)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(61, 23)
        Me.LabelX4.TabIndex = 123
        Me.LabelX4.Text = "MONEDA"
        '
        'tbFechaDel
        '
        Me.tbFechaDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbFechaDel.Location = New System.Drawing.Point(116, 102)
        Me.tbFechaDel.Name = "tbFechaDel"
        Me.tbFechaDel.Size = New System.Drawing.Size(99, 20)
        Me.tbFechaDel.TabIndex = 119
        '
        'tbCuenta
        '
        Me.tbCuenta.Location = New System.Drawing.Point(116, 28)
        Me.tbCuenta.Name = "tbCuenta"
        Me.tbCuenta.ReadOnly = True
        Me.tbCuenta.Size = New System.Drawing.Size(239, 20)
        Me.tbCuenta.TabIndex = 122
        '
        'tbFechaAl
        '
        Me.tbFechaAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbFechaAl.Location = New System.Drawing.Point(225, 102)
        Me.tbFechaAl.Name = "tbFechaAl"
        Me.tbFechaAl.Size = New System.Drawing.Size(97, 20)
        Me.tbFechaAl.TabIndex = 120
        '
        'tbNumi
        '
        Me.tbNumi.Location = New System.Drawing.Point(26, 28)
        Me.tbNumi.Name = "tbNumi"
        Me.tbNumi.ReadOnly = True
        Me.tbNumi.Size = New System.Drawing.Size(84, 20)
        Me.tbNumi.TabIndex = 121
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.Green
        Me.LabelX6.Location = New System.Drawing.Point(119, 8)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(83, 23)
        Me.LabelX6.TabIndex = 128
        Me.LabelX6.Text = "CTRL+ENTER"
        '
        'gpGrilla
        '
        Me.gpGrilla.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpGrilla.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpGrilla.Controls.Add(Me.grDetalle)
        Me.gpGrilla.DisabledBackColor = System.Drawing.Color.Empty
        Me.gpGrilla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gpGrilla.Location = New System.Drawing.Point(0, 180)
        Me.gpGrilla.Name = "gpGrilla"
        Me.gpGrilla.Size = New System.Drawing.Size(952, 270)
        '
        '
        '
        Me.gpGrilla.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpGrilla.Style.BackColorGradientAngle = 90
        Me.gpGrilla.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpGrilla.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpGrilla.Style.BorderBottomWidth = 1
        Me.gpGrilla.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpGrilla.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpGrilla.Style.BorderLeftWidth = 1
        Me.gpGrilla.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpGrilla.Style.BorderRightWidth = 1
        Me.gpGrilla.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpGrilla.Style.BorderTopWidth = 1
        Me.gpGrilla.Style.CornerDiameter = 4
        Me.gpGrilla.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpGrilla.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.gpGrilla.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpGrilla.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.gpGrilla.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.gpGrilla.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.gpGrilla.TabIndex = 21
        Me.gpGrilla.Text = "DATOS"
        '
        'grDetalle
        '
        Me.grDetalle.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grDetalle.Location = New System.Drawing.Point(0, 0)
        Me.grDetalle.Name = "grDetalle"
        Me.grDetalle.Size = New System.Drawing.Size(946, 249)
        Me.grDetalle.TabIndex = 3
        '
        'PR_LibroMayor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "PR_LibroMayor"
        Me.Text = "PR_LibroMayor"
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel4.ResumeLayout(False)
        Me.GroupPanel4.PerformLayout()
        CType(Me.cbAuxiliar02, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel3.ResumeLayout(False)
        Me.GroupPanel3.PerformLayout()
        CType(Me.cbAuxiliar01, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.tbMoneda, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpGrilla.ResumeLayout(False)
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gpGrilla As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaAl As DateTimePicker
    Friend WithEvents tbFechaDel As DateTimePicker
    Friend WithEvents tbCuenta As TextBox
    Friend WithEvents tbNumi As TextBox
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbMoneda As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Protected WithEvents grDetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents tbCliente As System.Windows.Forms.TextBox
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbMeses As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbReferencia As TextBox
    Friend WithEvents tbFiltrarRef As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupPanel4 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents swAuxiliar02 As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents swAuxiliar01 As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents cbAuxiliar01 As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents lbAuxiliar01 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbAuxiliar02 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbAuxiliar02 As Janus.Windows.GridEX.EditControls.MultiColumnCombo
End Class
