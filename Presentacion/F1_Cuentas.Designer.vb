<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F1_Cuentas
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1_Cuentas))
        Dim tbEmpresa_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim tbAuxiliar_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim tbTipo_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbDesc = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbCuenta = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbNumi = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbMoneda = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.gpFamiliarSocio = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tvCuentas = New System.Windows.Forms.TreeView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grDetalle = New Janus.Windows.GridEX.GridEX()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbEmpresa = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbAuxiliar = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbNivel2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbPadre = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.panelCuentas = New System.Windows.Forms.Panel()
        Me.tbNumiPadre = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbNivel = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCuentaMayor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCuenta2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCuenta1 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.tbTipo = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ELIMINARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.gpFamiliarSocio.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        CType(Me.tbEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbAuxiliar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelCuentas.SuspendLayout()
        CType(Me.tbTipo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.SuperTabPrincipal.Margin = New System.Windows.Forms.Padding(4)
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(853, 602)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanelBuscador.Margin = New System.Windows.Forms.Padding(4)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(821, 507)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Margin = New System.Windows.Forms.Padding(4)
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(821, 602)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelSuperior.Size = New System.Drawing.Size(821, 72)
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
        Me.PanelInferior.Location = New System.Drawing.Point(0, 566)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelInferior.Size = New System.Drawing.Size(821, 36)
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
        Me.TxtNombreUsu.Margin = New System.Windows.Forms.Padding(4)
        '
        'PanelToolBar1
        '
        Me.PanelToolBar1.Controls.Add(Me.ButtonX1)
        Me.PanelToolBar1.Controls.Add(Me.ButtonX2)
        Me.PanelToolBar1.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelToolBar1.Size = New System.Drawing.Size(543, 72)
        Me.PanelToolBar1.Controls.SetChildIndex(Me.btnNuevo, 0)
        Me.PanelToolBar1.Controls.SetChildIndex(Me.btnModificar, 0)
        Me.PanelToolBar1.Controls.SetChildIndex(Me.btnEliminar, 0)
        Me.PanelToolBar1.Controls.SetChildIndex(Me.btnGrabar, 0)
        Me.PanelToolBar1.Controls.SetChildIndex(Me.btnSalir, 0)
        Me.PanelToolBar1.Controls.SetChildIndex(Me.ButtonX2, 0)
        Me.PanelToolBar1.Controls.SetChildIndex(Me.ButtonX1, 0)
        '
        'btnSalir
        '
        Me.btnSalir.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSalir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSalir.Size = New System.Drawing.Size(86, 72)
        '
        'btnGrabar
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
        Me.PanelToolBar2.Location = New System.Drawing.Point(741, 0)
        Me.PanelToolBar2.Margin = New System.Windows.Forms.Padding(4)
        '
        'MPanelSup
        '
        Me.MPanelSup.Controls.Add(Me.gpFamiliarSocio)
        Me.MPanelSup.Controls.Add(Me.Panel1)
        Me.MPanelSup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MPanelSup.Margin = New System.Windows.Forms.Padding(4)
        Me.MPanelSup.Size = New System.Drawing.Size(821, 494)
        Me.MPanelSup.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.MPanelSup.Controls.SetChildIndex(Me.Panel1, 0)
        Me.MPanelSup.Controls.SetChildIndex(Me.gpFamiliarSocio, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelPrincipal.Size = New System.Drawing.Size(821, 494)
        '
        'GroupPanelBuscador
        '
        Me.GroupPanelBuscador.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupPanelBuscador.Location = New System.Drawing.Point(0, 438)
        Me.GroupPanelBuscador.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanelBuscador.Size = New System.Drawing.Size(821, 56)
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
        Me.GroupPanelBuscador.Visible = False
        '
        'JGrM_Buscador
        '
        Me.JGrM_Buscador.Margin = New System.Windows.Forms.Padding(4)
        Me.JGrM_Buscador.Size = New System.Drawing.Size(815, 33)
        '
        'PanelUsuario
        '
        Me.PanelUsuario.Location = New System.Drawing.Point(782, 29)
        Me.PanelUsuario.Margin = New System.Windows.Forms.Padding(4)
        '
        'btnUltimo
        '
        Me.btnUltimo.Margin = New System.Windows.Forms.Padding(2)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(621, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(4)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Location = New System.Drawing.Point(543, 0)
        Me.MRlAccion.Margin = New System.Windows.Forms.Padding(4)
        Me.MRlAccion.Size = New System.Drawing.Size(198, 72)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(477, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        '
        'LabelX3
        '
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(7, 172)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(96, 23)
        Me.LabelX3.TabIndex = 94
        Me.LabelX3.Text = "DESCRIPCION:"
        '
        'tbDesc
        '
        '
        '
        '
        Me.tbDesc.Border.Class = "TextBoxBorder"
        Me.tbDesc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDesc.Location = New System.Drawing.Point(106, 175)
        Me.tbDesc.Multiline = True
        Me.tbDesc.Name = "tbDesc"
        Me.tbDesc.PreventEnterBeep = True
        Me.tbDesc.Size = New System.Drawing.Size(245, 51)
        Me.tbDesc.TabIndex = 2
        '
        'LabelX2
        '
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(7, 87)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(75, 23)
        Me.LabelX2.TabIndex = 93
        Me.LabelX2.Text = "CUENTA:"
        '
        'tbCuenta
        '
        '
        '
        '
        Me.tbCuenta.Border.Class = "TextBoxBorder"
        Me.tbCuenta.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCuenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCuenta.Location = New System.Drawing.Point(106, 90)
        Me.tbCuenta.Name = "tbCuenta"
        Me.tbCuenta.PreventEnterBeep = True
        Me.tbCuenta.Size = New System.Drawing.Size(136, 22)
        Me.tbCuenta.TabIndex = 89
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(7, 58)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(75, 23)
        Me.LabelX1.TabIndex = 92
        Me.LabelX1.Text = "ID:"
        '
        'tbNumi
        '
        '
        '
        '
        Me.tbNumi.Border.Class = "TextBoxBorder"
        Me.tbNumi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNumi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNumi.Location = New System.Drawing.Point(106, 61)
        Me.tbNumi.Name = "tbNumi"
        Me.tbNumi.PreventEnterBeep = True
        Me.tbNumi.Size = New System.Drawing.Size(72, 22)
        Me.tbNumi.TabIndex = 91
        Me.tbNumi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbMoneda
        '
        '
        '
        '
        Me.tbMoneda.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMoneda.Location = New System.Drawing.Point(106, 147)
        Me.tbMoneda.Name = "tbMoneda"
        Me.tbMoneda.OffText = "BOLIVIANO"
        Me.tbMoneda.OnText = "DOLAR"
        Me.tbMoneda.Size = New System.Drawing.Size(136, 22)
        Me.tbMoneda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbMoneda.TabIndex = 95
        '
        'LabelX15
        '
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX15.Location = New System.Drawing.Point(7, 144)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.Size = New System.Drawing.Size(75, 23)
        Me.LabelX15.TabIndex = 96
        Me.LabelX15.Text = "MONEDA:"
        '
        'gpFamiliarSocio
        '
        Me.gpFamiliarSocio.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpFamiliarSocio.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpFamiliarSocio.Controls.Add(Me.tvCuentas)
        Me.gpFamiliarSocio.DisabledBackColor = System.Drawing.Color.Empty
        Me.gpFamiliarSocio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gpFamiliarSocio.Location = New System.Drawing.Point(0, 0)
        Me.gpFamiliarSocio.Name = "gpFamiliarSocio"
        Me.gpFamiliarSocio.Size = New System.Drawing.Size(455, 494)
        '
        '
        '
        Me.gpFamiliarSocio.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpFamiliarSocio.Style.BackColorGradientAngle = 90
        Me.gpFamiliarSocio.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpFamiliarSocio.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpFamiliarSocio.Style.BorderBottomWidth = 1
        Me.gpFamiliarSocio.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpFamiliarSocio.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpFamiliarSocio.Style.BorderLeftWidth = 1
        Me.gpFamiliarSocio.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpFamiliarSocio.Style.BorderRightWidth = 1
        Me.gpFamiliarSocio.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpFamiliarSocio.Style.BorderTopWidth = 1
        Me.gpFamiliarSocio.Style.CornerDiameter = 4
        Me.gpFamiliarSocio.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpFamiliarSocio.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.gpFamiliarSocio.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpFamiliarSocio.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.gpFamiliarSocio.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.gpFamiliarSocio.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.gpFamiliarSocio.TabIndex = 97
        Me.gpFamiliarSocio.Text = "CUENTAS"
        '
        'tvCuentas
        '
        Me.tvCuentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvCuentas.Location = New System.Drawing.Point(0, 0)
        Me.tvCuentas.Name = "tvCuentas"
        Me.tvCuentas.Size = New System.Drawing.Size(449, 471)
        Me.tvCuentas.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupPanel1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(455, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(366, 494)
        Me.Panel1.TabIndex = 98
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.grDetalle)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 308)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(366, 186)
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
        Me.GroupPanel1.TabIndex = 108
        Me.GroupPanel1.Text = "A U X I L I A R E S"
        '
        'grDetalle
        '
        Me.grDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grDetalle.Location = New System.Drawing.Point(0, 0)
        Me.grDetalle.Name = "grDetalle"
        Me.grDetalle.Size = New System.Drawing.Size(360, 163)
        Me.grDetalle.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupPanel2)
        Me.Panel2.Controls.Add(Me.tbAuxiliar)
        Me.Panel2.Controls.Add(Me.LabelX1)
        Me.Panel2.Controls.Add(Me.tbNivel2)
        Me.Panel2.Controls.Add(Me.tbDesc)
        Me.Panel2.Controls.Add(Me.tbPadre)
        Me.Panel2.Controls.Add(Me.LabelX3)
        Me.Panel2.Controls.Add(Me.panelCuentas)
        Me.Panel2.Controls.Add(Me.LabelX2)
        Me.Panel2.Controls.Add(Me.tbCuenta2)
        Me.Panel2.Controls.Add(Me.LabelX15)
        Me.Panel2.Controls.Add(Me.tbCuenta1)
        Me.Panel2.Controls.Add(Me.tbCuenta)
        Me.Panel2.Controls.Add(Me.LabelX6)
        Me.Panel2.Controls.Add(Me.tbMoneda)
        Me.Panel2.Controls.Add(Me.tbTipo)
        Me.Panel2.Controls.Add(Me.tbNumi)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(366, 308)
        Me.Panel2.TabIndex = 107
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.tbEmpresa)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanel2.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(366, 52)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.GroupPanel2.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal
        Me.GroupPanel2.Style.TextColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 137
        Me.GroupPanel2.Text = "E M P R E S A"
        '
        'tbEmpresa
        '
        tbEmpresa_DesignTimeLayout.LayoutString = resources.GetString("tbEmpresa_DesignTimeLayout.LayoutString")
        Me.tbEmpresa.DesignTimeLayout = tbEmpresa_DesignTimeLayout
        Me.tbEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbEmpresa.Location = New System.Drawing.Point(6, 2)
        Me.tbEmpresa.Name = "tbEmpresa"
        Me.tbEmpresa.SelectedIndex = -1
        Me.tbEmpresa.SelectedItem = Nothing
        Me.tbEmpresa.Size = New System.Drawing.Size(213, 22)
        Me.tbEmpresa.TabIndex = 98
        '
        'tbAuxiliar
        '
        tbAuxiliar_DesignTimeLayout.LayoutString = resources.GetString("tbAuxiliar_DesignTimeLayout.LayoutString")
        Me.tbAuxiliar.DesignTimeLayout = tbAuxiliar_DesignTimeLayout
        Me.tbAuxiliar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAuxiliar.Location = New System.Drawing.Point(255, 269)
        Me.tbAuxiliar.Name = "tbAuxiliar"
        Me.tbAuxiliar.SelectedIndex = -1
        Me.tbAuxiliar.SelectedItem = Nothing
        Me.tbAuxiliar.Size = New System.Drawing.Size(66, 22)
        Me.tbAuxiliar.TabIndex = 107
        Me.tbAuxiliar.Visible = False
        '
        'tbNivel2
        '
        '
        '
        '
        Me.tbNivel2.Border.Class = "TextBoxBorder"
        Me.tbNivel2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNivel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNivel2.Location = New System.Drawing.Point(341, 315)
        Me.tbNivel2.Name = "tbNivel2"
        Me.tbNivel2.PreventEnterBeep = True
        Me.tbNivel2.Size = New System.Drawing.Size(40, 22)
        Me.tbNivel2.TabIndex = 106
        Me.tbNivel2.Visible = False
        '
        'tbPadre
        '
        '
        '
        '
        Me.tbPadre.Border.Class = "TextBoxBorder"
        Me.tbPadre.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbPadre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPadre.Location = New System.Drawing.Point(249, 315)
        Me.tbPadre.Name = "tbPadre"
        Me.tbPadre.PreventEnterBeep = True
        Me.tbPadre.Size = New System.Drawing.Size(72, 22)
        Me.tbPadre.TabIndex = 106
        Me.tbPadre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbPadre.Visible = False
        '
        'panelCuentas
        '
        Me.panelCuentas.Controls.Add(Me.tbNumiPadre)
        Me.panelCuentas.Controls.Add(Me.LabelX4)
        Me.panelCuentas.Controls.Add(Me.LabelX5)
        Me.panelCuentas.Controls.Add(Me.tbNivel)
        Me.panelCuentas.Controls.Add(Me.tbCuentaMayor)
        Me.panelCuentas.Location = New System.Drawing.Point(2, 235)
        Me.panelCuentas.Name = "panelCuentas"
        Me.panelCuentas.Size = New System.Drawing.Size(242, 74)
        Me.panelCuentas.TabIndex = 105
        '
        'tbNumiPadre
        '
        '
        '
        '
        Me.tbNumiPadre.Border.Class = "TextBoxBorder"
        Me.tbNumiPadre.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNumiPadre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNumiPadre.Location = New System.Drawing.Point(122, 72)
        Me.tbNumiPadre.Name = "tbNumiPadre"
        Me.tbNumiPadre.PreventEnterBeep = True
        Me.tbNumiPadre.Size = New System.Drawing.Size(99, 22)
        Me.tbNumiPadre.TabIndex = 105
        Me.tbNumiPadre.Visible = False
        '
        'LabelX4
        '
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(7, 6)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(75, 23)
        Me.LabelX4.TabIndex = 102
        Me.LabelX4.Text = "NIVEL:"
        '
        'LabelX5
        '
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(7, 35)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(110, 23)
        Me.LabelX5.TabIndex = 104
        Me.LabelX5.Text = "CUENTA MAYOR:"
        '
        'tbNivel
        '
        '
        '
        '
        Me.tbNivel.Border.Class = "TextBoxBorder"
        Me.tbNivel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNivel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNivel.Location = New System.Drawing.Point(121, 7)
        Me.tbNivel.Name = "tbNivel"
        Me.tbNivel.PreventEnterBeep = True
        Me.tbNivel.Size = New System.Drawing.Size(40, 22)
        Me.tbNivel.TabIndex = 101
        '
        'tbCuentaMayor
        '
        '
        '
        '
        Me.tbCuentaMayor.Border.Class = "TextBoxBorder"
        Me.tbCuentaMayor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCuentaMayor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCuentaMayor.Location = New System.Drawing.Point(121, 36)
        Me.tbCuentaMayor.Name = "tbCuentaMayor"
        Me.tbCuentaMayor.PreventEnterBeep = True
        Me.tbCuentaMayor.Size = New System.Drawing.Size(99, 22)
        Me.tbCuentaMayor.TabIndex = 103
        '
        'tbCuenta2
        '
        '
        '
        '
        Me.tbCuenta2.Border.Class = "TextBoxBorder"
        Me.tbCuenta2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCuenta2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCuenta2.Location = New System.Drawing.Point(179, 90)
        Me.tbCuenta2.Name = "tbCuenta2"
        Me.tbCuenta2.PreventEnterBeep = True
        Me.tbCuenta2.Size = New System.Drawing.Size(63, 22)
        Me.tbCuenta2.TabIndex = 1
        '
        'tbCuenta1
        '
        '
        '
        '
        Me.tbCuenta1.Border.Class = "TextBoxBorder"
        Me.tbCuenta1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCuenta1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCuenta1.Location = New System.Drawing.Point(106, 90)
        Me.tbCuenta1.Name = "tbCuenta1"
        Me.tbCuenta1.PreventEnterBeep = True
        Me.tbCuenta1.Size = New System.Drawing.Size(72, 22)
        Me.tbCuenta1.TabIndex = 0
        '
        'LabelX6
        '
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.Location = New System.Drawing.Point(7, 118)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(96, 23)
        Me.LabelX6.TabIndex = 98
        Me.LabelX6.Text = "TIPO:"
        '
        'tbTipo
        '
        tbTipo_DesignTimeLayout.LayoutString = resources.GetString("tbTipo_DesignTimeLayout.LayoutString")
        Me.tbTipo.DesignTimeLayout = tbTipo_DesignTimeLayout
        Me.tbTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTipo.Location = New System.Drawing.Point(107, 118)
        Me.tbTipo.Name = "tbTipo"
        Me.tbTipo.SelectedIndex = -1
        Me.tbTipo.SelectedItem = Nothing
        Me.tbTipo.Size = New System.Drawing.Size(135, 22)
        Me.tbTipo.TabIndex = 97
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "dolar.png")
        Me.ImageList1.Images.SetKeyName(1, "boliviano.png")
        Me.ImageList1.Images.SetKeyName(2, "seleccionado.png")
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.ButtonX2.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX2.Image = Global.Presentacion.My.Resources.Resources.contraer
        Me.ButtonX2.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.ButtonX2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX2.Location = New System.Drawing.Point(374, 0)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(81, 72)
        Me.ButtonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX2.TabIndex = 12
        Me.ButtonX2.Text = "CONTRAER"
        Me.ButtonX2.TextColor = System.Drawing.Color.White
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.ButtonX1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX1.Image = Global.Presentacion.My.Resources.Resources.expandir
        Me.ButtonX1.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.ButtonX1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX1.Location = New System.Drawing.Point(455, 0)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(81, 72)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 13
        Me.ButtonX1.Text = "EXPANDIR"
        Me.ButtonX1.TextColor = System.Drawing.Color.White
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ELIMINARToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(141, 40)
        '
        'ELIMINARToolStripMenuItem
        '
        Me.ELIMINARToolStripMenuItem.Image = Global.Presentacion.My.Resources.Resources.elim_fila2
        Me.ELIMINARToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ELIMINARToolStripMenuItem.Name = "ELIMINARToolStripMenuItem"
        Me.ELIMINARToolStripMenuItem.Size = New System.Drawing.Size(140, 36)
        Me.ELIMINARToolStripMenuItem.Text = "ELIMINAR"
        '
        'F1_Cuentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 602)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "F1_Cuentas"
        Me.Text = "F1_Cuentas"
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
        Me.gpFamiliarSocio.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel2.PerformLayout()
        CType(Me.tbEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbAuxiliar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelCuentas.ResumeLayout(False)
        CType(Me.tbTipo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tbMoneda As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbDesc As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCuenta As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNumi As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents gpFamiliarSocio As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tvCuentas As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbTipo As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Protected WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Protected WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents tbCuenta2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbCuenta1 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCuentaMayor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNivel As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents panelCuentas As System.Windows.Forms.Panel
    Friend WithEvents tbNumiPadre As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbPadre As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbNivel2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel2 As Panel
    Protected WithEvents grDetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents tbAuxiliar As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ELIMINARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbEmpresa As Janus.Windows.GridEX.EditControls.MultiColumnCombo
End Class
