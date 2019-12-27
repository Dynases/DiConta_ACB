<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F0_KardexMovimiento
    Inherits Modelos.ModeloF00


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_KardexMovimiento))
        Me.GroupPanelDatos = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tbFechaVenc = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btActualizar = New DevComponents.DotNetBar.ButtonX()
        Me.btImprimir = New DevComponents.DotNetBar.ButtonX()
        Me.BtGenerar = New DevComponents.DotNetBar.ButtonX()
        Me.Tb3Saldo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.Dti2FechaFin = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.Dti1FechaIni = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.Tb2DescEquipo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.Tb1CodEquipo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanelKardex = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Dgj1Datos = New Janus.Windows.GridEX.GridEX()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
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
        Me.PanelContent.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MSuperTabControlPanel1.SuspendLayout()
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MSuperTabControl.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanelDatos.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.Dti2FechaFin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dti1FechaIni, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanelKardex.SuspendLayout()
        CType(Me.Dgj1Datos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Controls.Add(Me.Panel3)
        Me.PanelSuperior.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelSuperior.Size = New System.Drawing.Size(1392, 89)
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
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar1, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar2, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PictureBox1, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.Panel3, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.MRlAccion, 0)
        '
        'PanelInferior
        '
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelInferior.Size = New System.Drawing.Size(1392, 48)
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
        Me.TxtNombreUsu.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtNombreUsu.ReadOnly = True
        Me.TxtNombreUsu.Size = New System.Drawing.Size(179, 38)
        Me.TxtNombreUsu.Text = "DEFAULT"
        '
        'btnSalir
        '
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Location = New System.Drawing.Point(1285, 0)
        Me.PanelToolBar2.Margin = New System.Windows.Forms.Padding(5)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelPrincipal.Size = New System.Drawing.Size(1392, 690)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.Panel1, 0)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(1125, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(5)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Font = New System.Drawing.Font("Georgia", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MRlAccion.ForeColor = System.Drawing.Color.Cyan
        Me.MRlAccion.Location = New System.Drawing.Point(634, 0)
        Me.MRlAccion.Margin = New System.Windows.Forms.Padding(5)
        Me.MRlAccion.Size = New System.Drawing.Size(299, 89)
        Me.MRlAccion.Text = "KARDEX DE PRODUCTOS"
        '
        'PanelContent
        '
        Me.PanelContent.Controls.Add(Me.GroupPanelKardex)
        Me.PanelContent.Controls.Add(Me.GroupPanelDatos)
        Me.PanelContent.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelContent.Size = New System.Drawing.Size(1355, 553)
        '
        'Panel1
        '
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Size = New System.Drawing.Size(1392, 553)
        '
        'MSuperTabControlPanel1
        '
        Me.MSuperTabControlPanel1.Margin = New System.Windows.Forms.Padding(5)
        Me.MSuperTabControlPanel1.Size = New System.Drawing.Size(1355, 553)
        '
        'MSuperTabItem1
        '
        Me.MSuperTabItem1.Text = "KARDEX"
        '
        'MSuperTabControl
        '
        '
        '
        '
        '
        '
        '
        Me.MSuperTabControl.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.MSuperTabControl.ControlBox.MenuBox.Name = ""
        Me.MSuperTabControl.ControlBox.Name = ""
        Me.MSuperTabControl.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.MSuperTabControl.ControlBox.MenuBox, Me.MSuperTabControl.ControlBox.CloseBox})
        Me.MSuperTabControl.Margin = New System.Windows.Forms.Padding(5)
        Me.MSuperTabControl.Size = New System.Drawing.Size(1392, 553)
        Me.MSuperTabControl.Controls.SetChildIndex(Me.MSuperTabControlPanel1, 0)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(933, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(5)
        '
        'PanelBuscador
        '
        Me.PanelBuscador.Size = New System.Drawing.Size(1275, 553)
        '
        'GroupPanelDatos
        '
        Me.GroupPanelDatos.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanelDatos.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanelDatos.Controls.Add(Me.Panel2)
        Me.GroupPanelDatos.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanelDatos.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanelDatos.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanelDatos.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanelDatos.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanelDatos.Name = "GroupPanelDatos"
        Me.GroupPanelDatos.Size = New System.Drawing.Size(1355, 267)
        '
        '
        '
        Me.GroupPanelDatos.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanelDatos.Style.BackColorGradientAngle = 90
        Me.GroupPanelDatos.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanelDatos.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelDatos.Style.BorderBottomWidth = 1
        Me.GroupPanelDatos.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanelDatos.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelDatos.Style.BorderLeftWidth = 1
        Me.GroupPanelDatos.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelDatos.Style.BorderRightWidth = 1
        Me.GroupPanelDatos.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelDatos.Style.BorderTopWidth = 1
        Me.GroupPanelDatos.Style.CornerDiameter = 4
        Me.GroupPanelDatos.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelDatos.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelDatos.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanelDatos.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelDatos.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelDatos.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanelDatos.TabIndex = 0
        Me.GroupPanelDatos.Text = "DATOS"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tbFechaVenc)
        Me.Panel2.Controls.Add(Me.btActualizar)
        Me.Panel2.Controls.Add(Me.btImprimir)
        Me.Panel2.Controls.Add(Me.BtGenerar)
        Me.Panel2.Controls.Add(Me.Tb3Saldo)
        Me.Panel2.Controls.Add(Me.LabelX2)
        Me.Panel2.Controls.Add(Me.Dti2FechaFin)
        Me.Panel2.Controls.Add(Me.LabelX4)
        Me.Panel2.Controls.Add(Me.Dti1FechaIni)
        Me.Panel2.Controls.Add(Me.LabelX3)
        Me.Panel2.Controls.Add(Me.Tb2DescEquipo)
        Me.Panel2.Controls.Add(Me.Tb1CodEquipo)
        Me.Panel2.Controls.Add(Me.LabelX1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1349, 240)
        Me.Panel2.TabIndex = 0
        '
        'tbFechaVenc
        '
        Me.tbFechaVenc.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbFechaVenc.Border.Class = "TextBoxBorder"
        Me.tbFechaVenc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.DisabledBackColor = System.Drawing.Color.White
        Me.tbFechaVenc.Font = New System.Drawing.Font("Open Sans Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaVenc.ForeColor = System.Drawing.Color.Black
        Me.tbFechaVenc.Location = New System.Drawing.Point(997, 20)
        Me.tbFechaVenc.Margin = New System.Windows.Forms.Padding(4)
        Me.tbFechaVenc.Name = "tbFechaVenc"
        Me.tbFechaVenc.PreventEnterBeep = True
        Me.tbFechaVenc.Size = New System.Drawing.Size(145, 28)
        Me.tbFechaVenc.TabIndex = 243
        Me.tbFechaVenc.Visible = False
        '
        'btActualizar
        '
        Me.btActualizar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btActualizar.BackColor = System.Drawing.Color.SkyBlue
        Me.btActualizar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btActualizar.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btActualizar.Image = Global.Presentacion.My.Resources.Resources.reload_5
        Me.btActualizar.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.btActualizar.Location = New System.Drawing.Point(511, 149)
        Me.btActualizar.Margin = New System.Windows.Forms.Padding(4)
        Me.btActualizar.Name = "btActualizar"
        Me.btActualizar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(4)
        Me.btActualizar.Size = New System.Drawing.Size(176, 60)
        Me.btActualizar.SubItemsExpandWidth = 10
        Me.btActualizar.TabIndex = 238
        Me.btActualizar.Text = "ACTUALIZAR SALDO"
        Me.btActualizar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.btActualizar.Visible = False
        '
        'btImprimir
        '
        Me.btImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btImprimir.BackColor = System.Drawing.Color.SkyBlue
        Me.btImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btImprimir.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btImprimir.Image = Global.Presentacion.My.Resources.Resources.printee
        Me.btImprimir.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.btImprimir.Location = New System.Drawing.Point(329, 149)
        Me.btImprimir.Margin = New System.Windows.Forms.Padding(4)
        Me.btImprimir.Name = "btImprimir"
        Me.btImprimir.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(4)
        Me.btImprimir.Size = New System.Drawing.Size(168, 60)
        Me.btImprimir.SubItemsExpandWidth = 10
        Me.btImprimir.TabIndex = 237
        Me.btImprimir.Text = "IMPRIMIR"
        Me.btImprimir.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'BtGenerar
        '
        Me.BtGenerar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.BtGenerar.BackColor = System.Drawing.Color.SkyBlue
        Me.BtGenerar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.BtGenerar.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtGenerar.Image = Global.Presentacion.My.Resources.Resources.list
        Me.BtGenerar.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.BtGenerar.Location = New System.Drawing.Point(145, 149)
        Me.BtGenerar.Margin = New System.Windows.Forms.Padding(4)
        Me.BtGenerar.Name = "BtGenerar"
        Me.BtGenerar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(4)
        Me.BtGenerar.Size = New System.Drawing.Size(168, 60)
        Me.BtGenerar.SubItemsExpandWidth = 10
        Me.BtGenerar.TabIndex = 236
        Me.BtGenerar.Text = "GENERAR"
        Me.BtGenerar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'Tb3Saldo
        '
        Me.Tb3Saldo.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.Tb3Saldo.Border.Class = "TextBoxBorder"
        Me.Tb3Saldo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb3Saldo.DisabledBackColor = System.Drawing.Color.White
        Me.Tb3Saldo.Font = New System.Drawing.Font("Open Sans Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb3Saldo.ForeColor = System.Drawing.Color.Black
        Me.Tb3Saldo.Location = New System.Drawing.Point(145, 90)
        Me.Tb3Saldo.Margin = New System.Windows.Forms.Padding(4)
        Me.Tb3Saldo.Name = "Tb3Saldo"
        Me.Tb3Saldo.PreventEnterBeep = True
        Me.Tb3Saldo.Size = New System.Drawing.Size(107, 28)
        Me.Tb3Saldo.TabIndex = 234
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(25, 92)
        Me.LabelX2.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(51, 20)
        Me.LabelX2.TabIndex = 235
        Me.LabelX2.Text = "Saldo:"
        '
        'Dti2FechaFin
        '
        '
        '
        '
        Me.Dti2FechaFin.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.Dti2FechaFin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti2FechaFin.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.Dti2FechaFin.ButtonDropDown.Visible = True
        Me.Dti2FechaFin.Font = New System.Drawing.Font("Open Sans Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dti2FechaFin.IsPopupCalendarOpen = False
        Me.Dti2FechaFin.Location = New System.Drawing.Point(383, 52)
        Me.Dti2FechaFin.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        '
        '
        '
        Me.Dti2FechaFin.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti2FechaFin.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.Dti2FechaFin.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.Dti2FechaFin.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.Dti2FechaFin.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.Dti2FechaFin.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.Dti2FechaFin.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.Dti2FechaFin.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.Dti2FechaFin.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.Dti2FechaFin.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti2FechaFin.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.Dti2FechaFin.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.Dti2FechaFin.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.Dti2FechaFin.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.Dti2FechaFin.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.Dti2FechaFin.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti2FechaFin.MonthCalendar.TodayButtonVisible = True
        Me.Dti2FechaFin.Name = "Dti2FechaFin"
        Me.Dti2FechaFin.Size = New System.Drawing.Size(164, 28)
        Me.Dti2FechaFin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Dti2FechaFin.TabIndex = 233
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX4.Location = New System.Drawing.Point(344, 57)
        Me.LabelX4.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(25, 20)
        Me.LabelX4.TabIndex = 232
        Me.LabelX4.Text = "Al:"
        '
        'Dti1FechaIni
        '
        '
        '
        '
        Me.Dti1FechaIni.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.Dti1FechaIni.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti1FechaIni.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.Dti1FechaIni.ButtonDropDown.Visible = True
        Me.Dti1FechaIni.Font = New System.Drawing.Font("Open Sans Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dti1FechaIni.IsPopupCalendarOpen = False
        Me.Dti1FechaIni.Location = New System.Drawing.Point(145, 52)
        Me.Dti1FechaIni.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        '
        '
        '
        Me.Dti1FechaIni.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti1FechaIni.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.Dti1FechaIni.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.Dti1FechaIni.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.Dti1FechaIni.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.Dti1FechaIni.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.Dti1FechaIni.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.Dti1FechaIni.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.Dti1FechaIni.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.Dti1FechaIni.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti1FechaIni.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.Dti1FechaIni.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.Dti1FechaIni.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.Dti1FechaIni.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.Dti1FechaIni.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.Dti1FechaIni.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Dti1FechaIni.MonthCalendar.TodayButtonVisible = True
        Me.Dti1FechaIni.Name = "Dti1FechaIni"
        Me.Dti1FechaIni.Size = New System.Drawing.Size(164, 28)
        Me.Dti1FechaIni.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Dti1FechaIni.TabIndex = 231
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX3.Location = New System.Drawing.Point(25, 57)
        Me.LabelX3.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(84, 20)
        Me.LabelX3.TabIndex = 230
        Me.LabelX3.Text = "Fecha Del:"
        '
        'Tb2DescEquipo
        '
        Me.Tb2DescEquipo.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.Tb2DescEquipo.Border.Class = "TextBoxBorder"
        Me.Tb2DescEquipo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb2DescEquipo.DisabledBackColor = System.Drawing.Color.White
        Me.Tb2DescEquipo.Font = New System.Drawing.Font("Open Sans Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb2DescEquipo.ForeColor = System.Drawing.Color.Black
        Me.Tb2DescEquipo.Location = New System.Drawing.Point(261, 17)
        Me.Tb2DescEquipo.Margin = New System.Windows.Forms.Padding(4)
        Me.Tb2DescEquipo.Name = "Tb2DescEquipo"
        Me.Tb2DescEquipo.PreventEnterBeep = True
        Me.Tb2DescEquipo.Size = New System.Drawing.Size(421, 28)
        Me.Tb2DescEquipo.TabIndex = 228
        '
        'Tb1CodEquipo
        '
        Me.Tb1CodEquipo.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.Tb1CodEquipo.Border.Class = "TextBoxBorder"
        Me.Tb1CodEquipo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb1CodEquipo.DisabledBackColor = System.Drawing.Color.White
        Me.Tb1CodEquipo.Font = New System.Drawing.Font("Open Sans Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb1CodEquipo.ForeColor = System.Drawing.Color.Black
        Me.Tb1CodEquipo.Location = New System.Drawing.Point(145, 17)
        Me.Tb1CodEquipo.Margin = New System.Windows.Forms.Padding(4)
        Me.Tb1CodEquipo.Name = "Tb1CodEquipo"
        Me.Tb1CodEquipo.PreventEnterBeep = True
        Me.Tb1CodEquipo.Size = New System.Drawing.Size(107, 28)
        Me.Tb1CodEquipo.TabIndex = 226
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX1.Location = New System.Drawing.Point(25, 20)
        Me.LabelX1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(79, 20)
        Me.LabelX1.TabIndex = 227
        Me.LabelX1.Text = "Producto:"
        '
        'GroupPanelKardex
        '
        Me.GroupPanelKardex.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanelKardex.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanelKardex.Controls.Add(Me.Dgj1Datos)
        Me.GroupPanelKardex.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanelKardex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanelKardex.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanelKardex.Location = New System.Drawing.Point(0, 267)
        Me.GroupPanelKardex.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanelKardex.Name = "GroupPanelKardex"
        Me.GroupPanelKardex.Size = New System.Drawing.Size(1355, 286)
        '
        '
        '
        Me.GroupPanelKardex.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelKardex.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelKardex.Style.BackColorGradientAngle = 90
        Me.GroupPanelKardex.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelKardex.Style.BorderBottomWidth = 1
        Me.GroupPanelKardex.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelKardex.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelKardex.Style.BorderLeftWidth = 1
        Me.GroupPanelKardex.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelKardex.Style.BorderRightWidth = 1
        Me.GroupPanelKardex.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelKardex.Style.BorderTopWidth = 1
        Me.GroupPanelKardex.Style.CornerDiameter = 4
        Me.GroupPanelKardex.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelKardex.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanelKardex.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelKardex.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanelKardex.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelKardex.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelKardex.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanelKardex.TabIndex = 1
        Me.GroupPanelKardex.Text = "KARDEX"
        '
        'Dgj1Datos
        '
        Me.Dgj1Datos.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.Dgj1Datos.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Dgj1Datos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgj1Datos.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle
        Me.Dgj1Datos.Font = New System.Drawing.Font("Open Sans Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dgj1Datos.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dgj1Datos.Location = New System.Drawing.Point(0, 0)
        Me.Dgj1Datos.Margin = New System.Windows.Forms.Padding(4)
        Me.Dgj1Datos.Name = "Dgj1Datos"
        Me.Dgj1Datos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.Dgj1Datos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.Dgj1Datos.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.Dgj1Datos.SelectedFormatStyle.Font = New System.Drawing.Font("Open Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dgj1Datos.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.Dgj1Datos.Size = New System.Drawing.Size(1349, 259)
        Me.Dgj1Datos.TabIndex = 0
        Me.Dgj1Datos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(501, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Panel3.Size = New System.Drawing.Size(133, 89)
        Me.Panel3.TabIndex = 10
        '
        'PictureBox2
        '
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox2.Image = Global.Presentacion.My.Resources.Resources.packing_2
        Me.PictureBox2.Location = New System.Drawing.Point(7, 6)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(119, 77)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'F0_KardexMovimiento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1392, 690)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "F0_KardexMovimiento"
        Me.Text = "F0_KardexMovimiento"
        Me.Controls.SetChildIndex(Me.PanelPrincipal, 0)
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
        Me.PanelContent.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.MSuperTabControlPanel1.ResumeLayout(False)
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MSuperTabControl.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanelDatos.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.Dti2FechaFin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dti1FechaIni, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanelKardex.ResumeLayout(False)
        CType(Me.Dgj1Datos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupPanelDatos As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Tb1CodEquipo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Tb2DescEquipo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Dti2FechaFin As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Dti1FechaIni As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents Tb3Saldo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btActualizar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btImprimir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents BtGenerar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupPanelKardex As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Dgj1Datos As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents tbFechaVenc As DevComponents.DotNetBar.Controls.TextBoxX
End Class
