Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX

Public Class P_TableView
    Inherits DevComponents.DotNetBar.Office2007Form

    Private moDataTableTipoDato As New DataTable
    Private strNombreTabla As String


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Tb_Estado As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents Btn_SelecFoto As System.Windows.Forms.Button
    Friend WithEvents ListB_Empleados As DevComponents.DotNetBar.ListBoxAdv
    Friend WithEvents cboTipoOrigen As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Wizard1 As DevComponents.DotNetBar.Wizard
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdTarea As Janus.Windows.GridEX.GridEX
    Friend WithEvents WizardPage1 As DevComponents.DotNetBar.WizardPage
    Friend WithEvents WizardPage2 As DevComponents.DotNetBar.WizardPage
    Friend WithEvents WizardPage3 As DevComponents.DotNetBar.WizardPage
    Friend WithEvents WizardPage4 As DevComponents.DotNetBar.WizardPage
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBaseDatos As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtObservacionTarea As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtNombrePrograma As Janus.Windows.GridEX.EditControls.EditBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(P_TableView))
        Dim cboTipoOrigen_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.txtObservacionTarea = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Wizard1 = New DevComponents.DotNetBar.Wizard()
        Me.WizardPage1 = New DevComponents.DotNetBar.WizardPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.WizardPage2 = New DevComponents.DotNetBar.WizardPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNombrePrograma = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.WizardPage3 = New DevComponents.DotNetBar.WizardPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBaseDatos = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.WizardPage4 = New DevComponents.DotNetBar.WizardPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cboTipoOrigen = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.grdTarea = New Janus.Windows.GridEX.GridEX()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Tb_Estado = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.Btn_SelecFoto = New System.Windows.Forms.Button()
        Me.ListB_Empleados = New DevComponents.DotNetBar.ListBoxAdv()
        Me.Wizard1.SuspendLayout()
        Me.WizardPage1.SuspendLayout()
        Me.WizardPage2.SuspendLayout()
        Me.WizardPage3.SuspendLayout()
        Me.WizardPage4.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.cboTipoOrigen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTarea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtObservacionTarea
        '
        Me.txtObservacionTarea.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtObservacionTarea.Location = New System.Drawing.Point(203, 3)
        Me.txtObservacionTarea.MaxLength = 255
        Me.txtObservacionTarea.Multiline = True
        Me.txtObservacionTarea.Name = "txtObservacionTarea"
        Me.txtObservacionTarea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionTarea.Size = New System.Drawing.Size(308, 20)
        Me.txtObservacionTarea.TabIndex = 327
        Me.txtObservacionTarea.TabStop = False
        Me.txtObservacionTarea.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtObservacionTarea.Visible = False
        Me.txtObservacionTarea.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Wizard1
        '
        Me.Wizard1.BackButtonText = "< Atrás"
        Me.Wizard1.BackButtonWidth = 89
        Me.Wizard1.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.Wizard1.BackgroundImage = CType(resources.GetObject("Wizard1.BackgroundImage"), System.Drawing.Image)
        Me.Wizard1.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007
        Me.Wizard1.CancelButtonText = "Cancelar"
        Me.Wizard1.CancelButtonWidth = 88
        Me.Wizard1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Wizard1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Wizard1.FinishButtonTabIndex = 3
        Me.Wizard1.FinishButtonWidth = 89
        Me.Wizard1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.FooterHeight = 53
        '
        '
        '
        Me.Wizard1.FooterStyle.BackColor = System.Drawing.Color.Transparent
        Me.Wizard1.FooterStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Wizard1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Wizard1.HeaderCaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.HeaderDescriptionFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.HeaderDescriptionIndent = 72
        Me.Wizard1.HeaderDescriptionVisible = False
        Me.Wizard1.HeaderHeight = 69
        Me.Wizard1.HeaderImageAlignment = DevComponents.DotNetBar.eWizardTitleImageAlignment.Left
        Me.Wizard1.HeaderImageSize = New System.Drawing.Size(58, 55)
        '
        '
        '
        Me.Wizard1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Wizard1.HeaderStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(219, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Wizard1.HeaderStyle.BackColorGradientAngle = 90
        Me.Wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.Wizard1.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(182, Byte), Integer))
        Me.Wizard1.HeaderStyle.BorderBottomWidth = 1
        Me.Wizard1.HeaderStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Wizard1.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.Wizard1.HeaderTitleIndent = 72
        Me.Wizard1.HelpButtonText = "Ayuda"
        Me.Wizard1.HelpButtonWidth = 89
        Me.Wizard1.Location = New System.Drawing.Point(0, 0)
        Me.Wizard1.Name = "Wizard1"
        Me.Wizard1.NextButtonText = "Siguiente >"
        Me.Wizard1.NextButtonWidth = 89
        Me.Wizard1.Size = New System.Drawing.Size(818, 519)
        Me.Wizard1.TabIndex = 0
        Me.Wizard1.WizardPages.AddRange(New DevComponents.DotNetBar.WizardPage() {Me.WizardPage1, Me.WizardPage2, Me.WizardPage3, Me.WizardPage4})
        '
        'WizardPage1
        '
        Me.WizardPage1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WizardPage1.AntiAlias = False
        Me.WizardPage1.BackButtonEnabled = DevComponents.DotNetBar.eWizardButtonState.[False]
        Me.WizardPage1.BackColor = System.Drawing.Color.Transparent
        Me.WizardPage1.Controls.Add(Me.Label1)
        Me.WizardPage1.Controls.Add(Me.Label2)
        Me.WizardPage1.Controls.Add(Me.Label3)
        Me.WizardPage1.InteriorPage = False
        Me.WizardPage1.Location = New System.Drawing.Point(0, 0)
        Me.WizardPage1.Name = "WizardPage1"
        Me.WizardPage1.Size = New System.Drawing.Size(818, 466)
        '
        '
        '
        Me.WizardPage1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.WizardPage1.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.Label1.Location = New System.Drawing.Point(252, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(549, 76)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bienvenido al Asistente de Proyecto Dinases"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(252, 115)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(548, 314)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Esta página de bienvenida se ha creado para guiar a traves de los pasos necesario" & _
    "s"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(252, 438)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(250, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Para continuar, haga clic en Siguiente."
        '
        'WizardPage2
        '
        Me.WizardPage2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WizardPage2.AntiAlias = False
        Me.WizardPage2.BackButtonEnabled = DevComponents.DotNetBar.eWizardButtonState.[True]
        Me.WizardPage2.BackColor = System.Drawing.Color.Transparent
        Me.WizardPage2.CancelButtonEnabled = DevComponents.DotNetBar.eWizardButtonState.[True]
        Me.WizardPage2.Controls.Add(Me.Label10)
        Me.WizardPage2.Controls.Add(Me.txtNombrePrograma)
        Me.WizardPage2.Controls.Add(Me.Label4)
        Me.WizardPage2.Location = New System.Drawing.Point(7, 81)
        Me.WizardPage2.Name = "WizardPage2"
        Me.WizardPage2.NextButtonEnabled = DevComponents.DotNetBar.eWizardButtonState.[True]
        Me.WizardPage2.PageDescription = "Asistente"
        Me.WizardPage2.PageTitle = "Asistente"
        Me.WizardPage2.Size = New System.Drawing.Size(804, 373)
        '
        '
        '
        Me.WizardPage2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.WizardPage2.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label10.Location = New System.Drawing.Point(116, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 40)
        Me.Label10.TabIndex = 188
        Me.Label10.Text = "Nombre del Programa"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNombrePrograma
        '
        Me.txtNombrePrograma.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombrePrograma.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtNombrePrograma.Location = New System.Drawing.Point(217, 100)
        Me.txtNombrePrograma.Name = "txtNombrePrograma"
        Me.txtNombrePrograma.Size = New System.Drawing.Size(343, 23)
        Me.txtNombrePrograma.TabIndex = 14
        Me.txtNombrePrograma.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtNombrePrograma.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(100, 150)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(460, 18)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Ingrese Nombre de Programa..."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label4.Visible = False
        '
        'WizardPage3
        '
        Me.WizardPage3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WizardPage3.AntiAlias = False
        Me.WizardPage3.BackColor = System.Drawing.Color.Transparent
        Me.WizardPage3.Controls.Add(Me.Label5)
        Me.WizardPage3.Controls.Add(Me.txtBaseDatos)
        Me.WizardPage3.Controls.Add(Me.Label11)
        Me.WizardPage3.Location = New System.Drawing.Point(7, 81)
        Me.WizardPage3.Name = "WizardPage3"
        Me.WizardPage3.PageDescription = "Asistente2"
        Me.WizardPage3.PageTitle = "Asistente 2"
        Me.WizardPage3.Size = New System.Drawing.Size(804, 373)
        '
        '
        '
        Me.WizardPage3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.WizardPage3.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(140, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 40)
        Me.Label5.TabIndex = 191
        Me.Label5.Text = "Base de Datos"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBaseDatos
        '
        Me.txtBaseDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBaseDatos.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtBaseDatos.Location = New System.Drawing.Point(217, 100)
        Me.txtBaseDatos.Name = "txtBaseDatos"
        Me.txtBaseDatos.Size = New System.Drawing.Size(343, 23)
        Me.txtBaseDatos.TabIndex = 190
        Me.txtBaseDatos.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtBaseDatos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(100, 140)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(460, 18)
        Me.Label11.TabIndex = 189
        Me.Label11.Text = "Ingrese Nombre de Base de Datos..."
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label11.Visible = False
        '
        'WizardPage4
        '
        Me.WizardPage4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WizardPage4.AntiAlias = False
        Me.WizardPage4.BackColor = System.Drawing.Color.Transparent
        Me.WizardPage4.Controls.Add(Me.TableLayoutPanel1)
        Me.WizardPage4.FinishButtonEnabled = DevComponents.DotNetBar.eWizardButtonState.[True]
        Me.WizardPage4.Location = New System.Drawing.Point(7, 81)
        Me.WizardPage4.Name = "WizardPage4"
        Me.WizardPage4.PageDescription = "Asistente4"
        Me.WizardPage4.PageTitle = "Asistente 4"
        Me.WizardPage4.Size = New System.Drawing.Size(804, 373)
        '
        '
        '
        Me.WizardPage4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.WizardPage4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.WizardPage4.TabIndex = 10
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cboTipoOrigen, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.grdTarea, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 373.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(804, 373)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'cboTipoOrigen
        '
        Me.cboTipoOrigen.BackColor = System.Drawing.SystemColors.Info
        cboTipoOrigen_DesignTimeLayout.LayoutString = resources.GetString("cboTipoOrigen_DesignTimeLayout.LayoutString")
        Me.cboTipoOrigen.DesignTimeLayout = cboTipoOrigen_DesignTimeLayout
        Me.cboTipoOrigen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoOrigen.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.cboTipoOrigen.Location = New System.Drawing.Point(3, 396)
        Me.cboTipoOrigen.Name = "cboTipoOrigen"
        Me.cboTipoOrigen.SelectedIndex = -1
        Me.cboTipoOrigen.SelectedItem = Nothing
        Me.cboTipoOrigen.Size = New System.Drawing.Size(114, 23)
        Me.cboTipoOrigen.TabIndex = 4
        Me.cboTipoOrigen.TabStop = False
        Me.cboTipoOrigen.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cboTipoOrigen.Visible = False
        Me.cboTipoOrigen.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdTarea
        '
        Me.grdTarea.AllowCardSizing = False
        Me.grdTarea.AlternatingColors = True
        Me.grdTarea.BackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdTarea.BorderStyle = Janus.Windows.GridEX.BorderStyle.None
        Me.grdTarea.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.grdTarea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTarea.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdTarea.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Info
        Me.grdTarea.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdTarea.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.grdTarea.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grdTarea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdTarea.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdTarea.GroupByBoxVisible = False
        Me.grdTarea.HeaderFormatStyle.BackColorGradient = System.Drawing.Color.Empty
        Me.grdTarea.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.grdTarea.Location = New System.Drawing.Point(123, 3)
        Me.grdTarea.Name = "grdTarea"
        Me.grdTarea.RecordNavigator = True
        Me.grdTarea.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTarea.SelectedInactiveFormatStyle.Blend = 0.0!
        Me.grdTarea.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.grdTarea.Size = New System.Drawing.Size(678, 367)
        Me.grdTarea.TabIndex = 326
        Me.grdTarea.TableHeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.grdTarea.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdTarea.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdTarea.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Tb_Estado, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_SelecFoto, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ListB_Empleados, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(114, 367)
        Me.TableLayoutPanel2.TabIndex = 327
        '
        'Tb_Estado
        '
        '
        '
        '
        Me.Tb_Estado.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb_Estado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_Estado.Location = New System.Drawing.Point(4, 337)
        Me.Tb_Estado.Margin = New System.Windows.Forms.Padding(4)
        Me.Tb_Estado.Name = "Tb_Estado"
        Me.Tb_Estado.OffText = "TABLA"
        Me.Tb_Estado.OnText = "VISTA"
        Me.Tb_Estado.Size = New System.Drawing.Size(106, 26)
        Me.Tb_Estado.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Tb_Estado.TabIndex = 23
        Me.Tb_Estado.ValueFalse = "0"
        Me.Tb_Estado.ValueTrue = "1"
        '
        'Btn_SelecFoto
        '
        Me.Btn_SelecFoto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_SelecFoto.Location = New System.Drawing.Point(4, 4)
        Me.Btn_SelecFoto.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_SelecFoto.Name = "Btn_SelecFoto"
        Me.Btn_SelecFoto.Size = New System.Drawing.Size(106, 25)
        Me.Btn_SelecFoto.TabIndex = 25
        Me.Btn_SelecFoto.Text = "Actualizar"
        Me.Btn_SelecFoto.UseVisualStyleBackColor = True
        Me.Btn_SelecFoto.Visible = False
        '
        'ListB_Empleados
        '
        Me.ListB_Empleados.AutoScroll = True
        '
        '
        '
        Me.ListB_Empleados.BackgroundStyle.BackColor2 = System.Drawing.Color.Turquoise
        Me.ListB_Empleados.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor
        Me.ListB_Empleados.BackgroundStyle.Class = "ListBoxAdv"
        Me.ListB_Empleados.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ListB_Empleados.CheckStateMember = Nothing
        Me.ListB_Empleados.ContainerControlProcessDialogKey = True
        Me.ListB_Empleados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListB_Empleados.DragDropSupport = True
        Me.ListB_Empleados.Location = New System.Drawing.Point(4, 37)
        Me.ListB_Empleados.Margin = New System.Windows.Forms.Padding(4)
        Me.ListB_Empleados.Name = "ListB_Empleados"
        Me.ListB_Empleados.Size = New System.Drawing.Size(106, 292)
        Me.ListB_Empleados.TabIndex = 116
        Me.ListB_Empleados.Text = "ListBoxAdv1"
        '
        'P_TableView
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(818, 519)
        Me.Controls.Add(Me.Wizard1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "P_TableView"
        Me.Text = "Sample Wizard"
        Me.Wizard1.ResumeLayout(False)
        Me.WizardPage1.ResumeLayout(False)
        Me.WizardPage2.ResumeLayout(False)
        Me.WizardPage2.PerformLayout()
        Me.WizardPage3.ResumeLayout(False)
        Me.WizardPage3.PerformLayout()
        Me.WizardPage4.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.cboTipoOrigen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTarea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Metodos Wizzard "
    Private Sub Wizard1_CancelButtonClick(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Wizard1.CancelButtonClick
        If DevComponents.DotNetBar.MessageBoxEx.Show("Cerrar Asistente?", "Asistente", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub Wizard1_FinishButtonClick(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Wizard1.FinishButtonClick
        Me.Close()
    End Sub

    Private Sub Wizard1_WizardPageChanging(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.WizardCancelPageChangeEventArgs) Handles Wizard1.WizardPageChanging
        If e.OldPage Is WizardPage2 AndAlso e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.NextButton Then
            If txtNombrePrograma.Text.Trim = String.Empty Then
                Label4.Visible = True
                e.NewPage = WizardPage2
            End If
        ElseIf e.OldPage Is WizardPage2 AndAlso e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.BackButton Then
            Label4.Visible = False
            e.NewPage = WizardPage1
        End If

        If e.OldPage Is WizardPage3 AndAlso e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.NextButton Then
            If txtBaseDatos.Text.Trim = String.Empty Then
                Label11.Text = "Ingrese Nombre de Base de Datos..."
                Label11.Visible = True
                e.NewPage = WizardPage3
            Else
                L_abrirConexion()
                If L_Existe_BD(txtBaseDatos.Text.Trim).Tables(0).Rows.Count > 0 Then
                    Call Cargar_Tablas()
                    Btn_SelecFoto.Visible = True
                Else
                    Label11.Text = "No Existe Base de Datos..."
                    Label11.Visible = True
                    e.NewPage = WizardPage3
                End If
            End If
        ElseIf e.OldPage Is WizardPage3 AndAlso e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.BackButton Then
            Label11.Visible = False
            Label4.Visible = False
            e.NewPage = WizardPage2
        End If

        If e.OldPage Is WizardPage4 AndAlso e.PageChangeSource = DevComponents.DotNetBar.eWizardPageChangeSource.BackButton Then
            grdTarea.ClearStructure()
            moDataTableTipoDato.Rows.Clear()
            Btn_SelecFoto.Visible = False
            Label11.Visible = False
            grdTarea.Update()
        End If
    End Sub
#End Region

    Private Sub Cargar_Tablas()
        L_abrirConexion(, txtBaseDatos.Text.Trim)
        Dim dataTable As DataTable

        If Not Tb_Estado.Value Then
            dataTable = L_Table_General(0).Tables(0)
        Else
            dataTable = L_Vista_General(0).Tables(0)
        End If

        strNombreTabla = String.Empty
        ListB_Empleados.ItemHeight = 30
        ListB_Empleados.SelectionMode = eSelectionMode.One
        ListB_Empleados.BackColor = Color.AliceBlue
        ListB_Empleados.DataSource = dataTable
        ListB_Empleados.DisplayMember = "TABLE_NAME"
        ListB_Empleados.ValueMember = "TABLE_NAME"

        If dataTable.Rows.Count > 0 Then
            ListB_Empleados.SetSelected(0, False)
        End If

        Call cboLibreriaLoad()

        grdTarea.ClearStructure()
        moDataTableTipoDato.Rows.Clear()
        grdTarea.Update()
    End Sub

#Region " TABLA "
#Region " Metodo-Tabla "
    Private Function ExisteNewOrEdit() As Boolean
        For Each campo As DataRow In moDataTableTipoDato.Rows
            If campo("Estado") = "New" Or campo("Estado") = "Edit" Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub cboLibreriaLoad()
        Dim data As DataTable = L_General_LibreriaCabecera(0).Tables(0)

        With cboTipoOrigen
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cacon")
            .DropDownList.Columns("cacon").DataMember = "cacon"
            .DropDownList.Columns("cacon").Visible = False

            .DropDownList.Columns.Add("cadesc")
            .DropDownList.Columns("cadesc").Caption = "Descripción"
            .DropDownList.Columns("cadesc").DataMember = "cadesc"
            .DropDownList.Columns("cadesc").Width = 205

            .ValueMember = "cacon"
            .DisplayMember = "cadesc"
            .DataSource = data ' L_General_LibreriaCabecera(0) ' data
            .Value = Nothing
        End With
    End Sub

    Public Function ListFindItem(ByVal lstCtrl As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal lngValue As Long) As Janus.Windows.GridEX.GridEXRow
        For Each oRow As Janus.Windows.GridEX.GridEXRow In lstCtrl.DropDownList.GetRows
            If CInt(oRow.Cells(0).Value) = lngValue Then
                Return oRow '.Cells(1).Value
            End If
        Next
        ListFindItem = Nothing
    End Function

    Private Sub DeleteDetalle()
        For Each oRow As DataRow In moDataTableTipoDato.Rows
            If oRow("Estado") <> "New" Then
                L_Table_Borrar(oRow("numi").ToString.Trim)
            End If
        Next
    End Sub
#End Region

#Region " DataSet-Tabla "
    Private Sub DataSetTable()
        '  _Dsencabezado = New DataSet("Tablas")
        moDataTableTipoDato = New DataTable("Prueba")
        '   moDataTableTipoDato = _Dsencabezado.Tables.Add("PRUEBA")
        moDataTableTipoDato.Columns.Add("numi", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("Columna", Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("TipoDato", Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("Tamanio", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("Sel", Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Caption", Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("Size", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("LibreriaId", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("Libreria", Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("Estado", Type.GetType("System.String"))
    End Sub

    Private Function RowNewTable(ByVal oDataRow As DataRow, ByVal boolSw As Boolean) As DataRow
        Dim oRow As DataRow
        oRow = moDataTableTipoDato.NewRow

        If boolSw Then
            oRow("numi") = 0
            oRow("Columna") = oDataRow(0).ToString().Trim
            oRow("TipoDato") = oDataRow("DataType").ToString.Trim
            oRow("Tamanio") = Integer.Parse(oDataRow("ColumnSize").ToString.Trim)
            oRow("Sel") = False ' oDataRow("IsKey")
            oRow("Caption") = ""
            oRow("Size") = 0
            oRow("LibreriaId") = 0
            oRow("Libreria") = String.Empty
            oRow("Estado") = "New"
        Else
            oRow("numi") = CInt(oDataRow("ybnumi").ToString().Trim)
            oRow("Columna") = oDataRow("ybcampo").ToString().Trim
            oRow("TipoDato") = oDataRow("ybtipodato").ToString.Trim
            oRow("Tamanio") = Integer.Parse(oDataRow("ybtamanio").ToString.Trim)
            oRow("Sel") = CBool(oDataRow("ybvisible")) 'False ' oDataRow("IsKey")
            oRow("Caption") = oDataRow("ybcaption").ToString.Trim
            oRow("Size") = Integer.Parse(oDataRow("ybsize").ToString.Trim) ' oDataRow("ybtamanio")
            oRow("LibreriaId") = Integer.Parse(oDataRow("yblibreria").ToString.Trim)
            oRow("Libreria") = oDataRow("yblibreriaDes").ToString.Trim
            oRow("Estado") = "Show"
        End If
        Return oRow
    End Function

    Private Sub EditarRow(ByRef oRow As DataRow)
        If CBool(grdTarea.GetValue("Sel")) <> CBool(oRow("Sel")) Then
            If CStr(oRow("Estado")) <> "New" Then
                grdTarea.SetValue("Estado", "Edit")
            End If
        End If

        If Trim(grdTarea.GetValue("Caption").ToString) <> Trim(oRow("Caption").ToString) Then
            grdTarea.SetValue("Caption", grdTarea.GetValue("Caption").ToString.Trim)
            If CStr(oRow("Estado")) <> "New" Then
                grdTarea.SetValue("Estado", "Edit")
            End If
        Else
            grdTarea.SetValue("Caption", oRow("Caption").ToString.Trim)
        End If

        If IsNumeric(grdTarea.GetValue("Size").ToString) Then
            If CInt(grdTarea.GetValue("Size").ToString) <> CInt(oRow("Size").ToString) Then
                grdTarea.SetValue("Size", CInt(grdTarea.GetValue("Size")))
                If CStr(oRow("Estado")) <> "New" Then
                    grdTarea.SetValue("Estado", "Edit")
                End If
            End If
        Else
            grdTarea.SetValue("Size", CInt(oRow("Size").ToString.Trim))
        End If

    End Sub
#End Region

#Region " Grid-Tabla "
    Private Sub grdTableSave()
        Dim numi, size, tamanio, libreria As Integer
        Dim campo, caption, dato, formato, tipoDato, libreriDes As String
        Dim visible As Boolean

        For Each oRow As DataRow In moDataTableTipoDato.Rows
            numi = oRow("numi")
            campo = oRow("Columna")
            caption = oRow("Caption")
            size = oRow("Size")
            visible = oRow("Sel")
            tipoDato = oRow("TipoDato")
            tamanio = oRow("Tamanio")
            dato = ""
            formato = ""
            libreria = oRow("LibreriaId")
            libreriDes = oRow("Libreria")
            If oRow("Estado") = "New" Then
                L_Tabla_Grabar(numi, campo, caption, size, visible, dato, formato, tipoDato, tamanio, strNombreTabla, libreria, libreriDes)
            ElseIf oRow("Estado") = "Edit" Then
                L_Tabla_Modificar(numi, campo, caption, size, visible, dato, formato, tipoDato, tamanio, strNombreTabla, libreria, libreriDes)
            End If
        Next
    End Sub

    Private Sub grdIntTable()
        With grdTarea
            .RootTable.Columns("numi").Visible = False

            .RootTable.Columns("Columna").Caption = "Columna"
            .RootTable.Columns("Columna").Width = 70
            .RootTable.Columns("Columna").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("Columna").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Columna").EditType = Janus.Windows.GridEX.EditType.NoEdit

            .RootTable.Columns("TipoDato").Caption = "Tipo de Dato"
            .RootTable.Columns("TipoDato").Width = 100
            .RootTable.Columns("TipoDato").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("TipoDato").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("TipoDato").EditType = Janus.Windows.GridEX.EditType.NoEdit

            .RootTable.Columns("Tamanio").Caption = "Tamaño"
            .RootTable.Columns("Tamanio").Width = 60
            .RootTable.Columns("Tamanio").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Tamanio").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Tamanio").EditType = Janus.Windows.GridEX.EditType.NoEdit

            .RootTable.Columns("Sel").Caption = "Visible"
            .RootTable.Columns("Sel").Width = 50
            .RootTable.Columns("Sel").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Sel").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Caption").Caption = "Título"
            .RootTable.Columns("Caption").Width = 100
            .RootTable.Columns("Caption").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("Caption").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Size").Caption = "Dimensión"
            .RootTable.Columns("Size").Width = 100
            .RootTable.Columns("Size").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .RootTable.Columns("Size").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Libreria").Caption = "Libreria"
            .RootTable.Columns("Libreria").Width = 100
            .RootTable.Columns("Libreria").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("Libreria").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Libreria").EditType = Janus.Windows.GridEX.EditType.Custom
            .RootTable.Columns("Libreria").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

            .RootTable.Columns("LibreriaId").Visible = False
            .RootTable.Columns("Estado").Visible = False
        End With

        Dim fc As Janus.Windows.GridEX.GridEXFormatCondition

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(grdTarea.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "Edit")
        fc.FormatStyle.BackColor = Color.LightYellow 'Color.LightGreen ' Color.DarkRed
        grdTarea.RootTable.FormatConditions.Add(fc)

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(grdTarea.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "New")
        fc.FormatStyle.BackColor = Color.LightGreen ' Color.DarkRed
        grdTarea.RootTable.FormatConditions.Add(fc)

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(grdTarea.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "Show")
        fc.FormatStyle.BackColor = Color.White 'Color.LightGreen ' Color.DarkRed
        grdTarea.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub grdTarea_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTarea.CellUpdated
        EditarRow(moDataTableTipoDato.Rows(grdTarea.GetRow.Position))
        Call grdIntTable()
    End Sub

    Private Sub grdTarea_InitCustomEdit(sender As Object, e As InitCustomEditEventArgs) Handles grdTarea.InitCustomEdit
        If grdTarea.RowCount > 0 Then
            If e.Column.DataMember = "Libreria" Then
                Dim row As Janus.Windows.GridEX.GridEXRow = ListFindItem(cboTipoOrigen, CInt(grdTarea.GetValue("LibreriaId").ToString))
                If row IsNot Nothing Then
                    cboTipoOrigen.Value = row.Cells(0).Value
                Else
                    cboTipoOrigen.Value = Nothing
                End If

                e.EditControl = cboTipoOrigen
                cboTipoOrigen.Visible = True
            End If
        End If
    End Sub

    Private Sub grdTarea_EndCustomEdit(sender As Object, e As EndCustomEditEventArgs) Handles grdTarea.EndCustomEdit
        If Not e.CancelUpdate Then
            If e.Column.DataMember = "Libreria" Then
                If cboTipoOrigen.Text.Trim <> String.Empty And cboTipoOrigen.SelectedIndex >= 0 Then
                    Dim row As Janus.Windows.GridEX.GridEXRow = cboTipoOrigen.DropDownList.GetRows.ElementAt(cboTipoOrigen.SelectedIndex) 'ListFindItem1(cboTipoOrigen, cboTipoOrigen.SelectedIndex)

                    If CInt(grdTarea.GetValue("LibreriaId").ToString) <> CInt(row.Cells(0).Value) Then
                        If CStr(grdTarea.GetValue("Estado").ToString) <> "New" Then
                            grdTarea.SetValue("Estado", "Edit")
                        End If

                        grdTarea.SetValue("LibreriaId", row.Cells(0).Value)
                        grdTarea.SetValue("Libreria", row.Cells(1).Value)
                    End If
                Else
                    If CStr(grdTarea.GetValue("Estado").ToString) = "Show" And CInt(grdTarea.GetValue("LibreriaId").ToString) > 0 Then
                        grdTarea.SetValue("Estado", "Edit")
                    End If

                    grdTarea.SetValue("LibreriaId", 0)
                    grdTarea.SetValue("Libreria", String.Empty)
                End If

                grdTarea.UpdateData()
                Call grdIntTable()
                cboTipoOrigen.Visible = False
            End If
        End If
    End Sub
#End Region
#End Region

#Region " Eventos-ListBox "
    Private Sub ListB_Empleados_ItemClick(sender As Object, e As EventArgs) Handles ListB_Empleados.ItemClick
        Try
            If ListB_Empleados.SelectedIndex >= 0 And strNombreTabla <> ListB_Empleados.SelectedValue Then 'selecciono el item
                If ExisteNewOrEdit() Then
                    If MessageBox.Show("¿Existen Cambios, desea guardarlos?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Call grdTableSave()
                    End If
                End If

                strNombreTabla = ListB_Empleados.SelectedValue
                Call DataSetTable()
                Dim dtInsu As DataTable
                dtInsu = L_Propiedades_General(0, strNombreTabla).Tables(0)
                If dtInsu.Rows.Count = 0 Then
                    dtInsu = L_Propiedades_Table(0, strNombreTabla).Tables(0)
                    For Each campo As DataRow In dtInsu.Rows
                        moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
                    Next
                Else
                    For Each campo As DataRow In dtInsu.Rows
                        moDataTableTipoDato.Rows.Add(RowNewTable(campo, False))
                    Next
                End If

                grdTarea.DataSource = moDataTableTipoDato
                grdTarea.RetrieveStructure()

                Call grdIntTable()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub Tb_Estado_ValueChanged(sender As Object, e As EventArgs) Handles Tb_Estado.ValueChanged
        Call Cargar_Tablas()
    End Sub

    Private Sub Btn_SelecFoto_Click(sender As Object, e As EventArgs) Handles Btn_SelecFoto.Click
        If ListB_Empleados.SelectedIndex >= 0 Then 'selecciono el item
            Call DeleteDetalle()
            Call DataSetTable()
            Dim dtInsu As DataTable

            dtInsu = L_Propiedades_Table(0, strNombreTabla).Tables(0)
            For Each campo As DataRow In dtInsu.Rows
                moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
            Next

            grdTarea.DataSource = moDataTableTipoDato
            grdTarea.RetrieveStructure()

            Call grdIntTable()
        End If
    End Sub

End Class
