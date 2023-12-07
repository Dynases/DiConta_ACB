Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Modelos
Imports DevComponents.DotNetBar.Controls
Imports System.Math
Imports System.IO

Public Class F0_Comprobante

#Region "ATRIBUTOS"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _existTipoCambio As Boolean

    Private _numiCuentaAjuste As String
    Private _difMaximaAjuste As Double

    Public _modulo As SideNavItem

    Private _detalleDetalle As DataTable
    Private _detalleDetalleCompras As DataTable

    Private _ultimaFecha As DateTime

    Private _numiAuxMod As Integer = 0
    Private _numiAuxSuc As Integer = 0
#End Region

#Region "VARIABLES LOCALES"
    Public _MPos As Integer
    Public _MNuevo As Boolean
    Public _MModificar As Boolean

    Public _MListEstBuscador As List(Of Celda)

    Public _MTipoInserccionNuevo As Boolean = True

#End Region


#Region "METODOS PRIVADOS MODELO"

    Public Sub _PMIniciarTodo()

        Me.Text = "c o m p r o b a n t e s".ToUpper
        TxtNombreUsu.Text = MGlobal.gs_usuario
        TxtNombreUsu.ReadOnly = True

        Me.WindowState = FormWindowState.Maximized
        Me.SupTabItemBusqueda.Visible = True

        _MListEstBuscador = _PMOGetListEstructuraBuscador()

        _PMInhabilitar()

        _PMOHabilitarFocus()

        _PMFiltrar()
        '_PMInhabilitar()

        '_PMOHabilitarFocus()

        AddHandler JGrM_Buscador.SelectionChanged, AddressOf JGrM_Buscador_SelectionChanged


        ''probando el control de errores
        '' Add the event handler for handling non-UI thread exceptions to the event. 
        'AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException

        '' Runs the application.
        ''Application.Run(New ErrorHandlerForm())

    End Sub

#Region "Control de errores"
    Private Shared Sub CurrentDomain_UnhandledException(ByVal sender As Object,
     ByVal e As UnhandledExceptionEventArgs)
        Try
            Dim ex As Exception = CType(e.ExceptionObject, Exception)
            Dim errorMsg As String = "An application error occurred. Please contact the adminstrator " &
                 "with the following information:" & ControlChars.Lf & ControlChars.Lf

            ' Since we can't prevent the app from terminating, log this to the event log.
            If (Not EventLog.SourceExists("ThreadException")) Then
                EventLog.CreateEventSource("ThreadException", "Application")
            End If

            ' Create an EventLog instance and assign its source.
            Dim myLog As New EventLog()
            myLog.Source = "ThreadException"
            myLog.WriteEntry((errorMsg + ex.Message & ControlChars.Lf & ControlChars.Lf &
                 "Stack Trace:" & ControlChars.Lf & ex.StackTrace))
        Catch exc As Exception
            Try
                MessageBox.Show("Fatal Non-UI Error", "Fatal Non-UI Error. Could not write the error to the event log. " &
                     "Reason: " & exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Finally
                Application.Exit()
            End Try
        End Try
    End Sub


    ' Creates the error message and displays it.
    Private Shared Function ShowThreadExceptionDialog(ByVal title As String, ByVal e As Exception) As DialogResult
        Dim errorMsg As String = "An application error occurred. Please contact the adminstrator " &
"with the following information:" & ControlChars.Lf & ControlChars.Lf
        errorMsg = errorMsg & e.Message & ControlChars.Lf &
ControlChars.Lf & "Stack Trace:" & ControlChars.Lf & e.StackTrace

        Return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop)
    End Function
#End Region

    Private Sub _PMCargarBuscador()

        Dim dtBuscador As DataTable = _PMOGetTablaBuscador()

        JGrM_Buscador.DataSource = dtBuscador
        JGrM_Buscador.RetrieveStructure()

        For i = 0 To _MListEstBuscador.Count - 1
            Dim campo As String = _MListEstBuscador.Item(i).campo
            With JGrM_Buscador.RootTable.Columns(campo)
                If _MListEstBuscador.Item(i).visible = True Then
                    .Caption = _MListEstBuscador.Item(i).titulo
                    .Width = _MListEstBuscador.Item(i).tamano
                    .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                    Dim col As DataColumn = dtBuscador.Columns(campo)
                    Dim tipo As Type = col.DataType
                    If tipo.ToString = "System.Int32" Or tipo.ToString = "System.Decimal" Or tipo.ToString = "System.Double" Then
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End If
                    If _MListEstBuscador.Item(i).formato <> String.Empty Then
                        .FormatString = _MListEstBuscador.Item(i).formato
                    End If
                Else
                    .Visible = False
                End If
            End With
        Next

        'Habilitar Filtradores
        With JGrM_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        'metodo para hacer la actualizacion de algo cuando cambia el datasource del buscador
        '_PMOLuegoDeCargarBuscador()

    End Sub
    Private Sub _PMCargarBuscador1()

        Dim dtBuscador As DataTable = _PMOGetTablaBuscador()

        JGrM_Buscador.DataSource = dtBuscador
        JGrM_Buscador.RetrieveStructure()

        For i = 0 To dtBuscador.Columns.Count - 1
            With JGrM_Buscador.RootTable.Columns(i)
                If _MListEstBuscador.Item(i).visible = True Then
                    .Caption = _MListEstBuscador.Item(i).titulo
                    .Width = _MListEstBuscador.Item(i).tamano
                    .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                    Dim col As DataColumn = dtBuscador.Columns(i)
                    Dim tipo As Type = col.DataType
                    If tipo.ToString = "System.Int32" Or tipo.ToString = "System.Decimal" Then
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End If
                    If _MListEstBuscador.Item(i).formato = String.Empty Then
                        .FormatString = _MListEstBuscador.Item(i).formato
                    End If
                Else
                    .Visible = False
                End If
            End With
        Next

        'Habilitar Filtradores
        With JGrM_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

    End Sub


    Public Sub _PMInhabilitar()
        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False
        PanelNavegacion.Enabled = True
        JGrM_Buscador.Enabled = True
        MRlAccion.Text = ""

        '_PMOLimpiarErrores()

        _PMOInhabilitar()

        _PMOLimpiarErrores()
    End Sub

    Private Sub _PMHabilitar()
        JGrM_Buscador.Enabled = False
        _PMOHabilitar()
    End Sub
    Public Sub _PMFiltrar()
        'cargo el buscador
        _PMCargarBuscador()
        If JGrM_Buscador.RowCount > 0 Then
            _MPos = 0
            _PMOMostrarRegistro(_MPos)
        Else
            _PMOLimpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub

    Public Sub _PMPrimerRegistro()
        If JGrM_Buscador.RowCount > 0 Then
            _MPos = 0
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub
    Private Sub _PMAnteriorRegistro()
        If _MPos > 0 And JGrM_Buscador.RowCount > 0 Then
            _MPos = _MPos - 1
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub
    Private Sub _PMSiguienteRegistro()
        If _MPos < JGrM_Buscador.RowCount - 1 Then
            _MPos = _MPos + 1
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub
    Private Sub _PMUltimoRegistro()
        If JGrM_Buscador.RowCount > 0 Then
            _MPos = JGrM_Buscador.RowCount - 1
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub

    Private Sub _PMNuevo()
        _MNuevo = True
        _MModificar = False

        'aca estaban

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False


        _PMHabilitar()
        _PMOLimpiar()
        '_PMHabilitar()

        MRlAccion.Text = "NUEVO"

        '_PMOLimpiar()

        If (CType(tbTipo.DataSource, DataTable).Rows.Count > 0) Then
            tbTipo.Value = 1

        End If
    End Sub

    Private Sub _PMModificar()
        If JGrM_Buscador.Row >= 0 Then
            _MNuevo = False
            _MModificar = True

            _PMHabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelNavegacion.Enabled = False

            MRlAccion.Text = "MODIFICAR"
        End If
    End Sub

    Private Sub _PMEliminar()

        _PMOEliminarRegistro()
    End Sub

    Private Sub _PMGuardar()

        If _PMOValidarCampos() = False Then
            Exit Sub
        End If

        If _MNuevo Then
            If btnGrabar.Enabled = True Then
                If tbTipo.SelectedIndex = 0 Then
                    tbNombre.ReadOnly = False
                    tbBanco.ReadOnly = False
                ElseIf tbTipo.SelectedIndex = 1 Then
                    tbNombre.ReadOnly = False
                    tbBanco.ReadOnly = False
                    tbChque.ReadOnly = False

                End If
                If tbTipo.SelectedIndex >= 0 Then
                    _prCargarOpcionesDeCuentasAutomaticas()

                    'cargar el numero de comprobante
                    Dim dt As DataTable = L_prObtenerNumFacturaGeneral(tbTipo.Value, tbFecha.Value.Year, tbFecha.Value.Month, gi_empresaNumi)
                    If dt.Rows.Count > 0 Then
                        tbNum.Text = dt.Rows(0).Item("oanum")
                        tbNroDoc.Text = dt.Rows(0).Item("oanumdoc")

                    End If


                Else
                    tbNum.Text = ""
                    tbNroDoc.Text = ""

                    Me.cmOpcionesDetalle.Items.Clear()

                    Dim subItem1 As ToolStripMenuItem
                    subItem1 = New ToolStripMenuItem()
                    subItem1.Image = Global.Presentacion.My.Resources.Resources.elim_fila2
                    subItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
                    subItem1.Name = "subItemEliminar"
                    subItem1.Size = New System.Drawing.Size(274, 36)
                    subItem1.Text = "ELIMINAR"
                    AddHandler subItem1.Click, AddressOf ELIMINAR_Click
                    Me.cmOpcionesDetalle.Items.Add(subItem1)

                    Dim subItem2 As ToolStripMenuItem
                    subItem2 = New ToolStripMenuItem()
                    subItem2.Name = "subItemNuevaFila"
                    subItem2.Size = New System.Drawing.Size(274, 36)
                    subItem2.Text = "INSERTAR FILA"
                    AddHandler subItem2.Click, AddressOf INSERTARFILAToolStripMenuItem_Click
                    Me.cmOpcionesDetalle.Items.Add(subItem2)
                End If
            End If
            If _PMOGrabarRegistro() = True Then
                'actualizar el grid de buscador
                '_PMCargarBuscador()

                If _MTipoInserccionNuevo Then
                    _PMSalir()
                    _PMUltimoRegistro()
                Else
                    _PMSalir()
                End If
            Else
                Exit Sub
            End If

        Else

            _PMOModificarRegistro()

            'actualizar el grid de buscador
            '_PMCargarBuscador()

            _PMSalir()
        End If
    End Sub

    Private Sub _PMSalir()
        _PSalirRegistro()
    End Sub
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        Me.Text = "c o m p r o b a n t e s".ToUpper

        'cargar datos globales
        Dim dtGlobal As DataTable = L_prConfigGeneralEmpresa(gi_empresaNumi)
        If dtGlobal.Rows.Count > 0 Then
            _numiCuentaAjuste = dtGlobal.Rows(0).Item("cfnumitc11")
            _difMaximaAjuste = dtGlobal.Rows(0).Item("cfdifmax")
        End If

        _detalleDetalle = L_prComprobanteDetalleDetalleGeneral(-1)
        _detalleDetalleCompras = L_prCompraComprobanteGeneralPorNumi(-1)

        _prCargarComboLibreria(tbTipo, gi_LibComprobante, gi_LibCOMPROBANTETipo)
        _prCargarComboLibreria(comboanio, 10, 1)
        _prCargarComboLibreria(comboMeses, 10, 2)
        tbChque.ReadOnly = True
        tbBanco.ReadOnly = True
        tbNombre.ReadOnly = True
        tbEmpresa.ReadOnly = True
        tbEmpresa.Text = gs_empresaDescSistema

        _prCargarGridAyudaCuenta()

        _PMIniciarTodo()

        _prAsignarPermisos()

        SuperTabPrincipal.SelectedTabIndex = 0
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaDetalleGeneral(cod1, cod2)

        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cnnum").Width = 70
            .DropDownList.Columns("cnnum").Caption = "COD"

            .DropDownList.Columns.Add("cndesc1").Width = 200
            .DropDownList.Columns("cndesc1").Caption = "DESCRIPCION"

            .ValueMember = "cnnum"
            .DisplayMember = "cndesc1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If

    End Sub

    Private Sub _prCargarGridDetalle(numi As String, Optional vacio As Integer = -1)
        Dim dt As New DataTable
        dt = L_prComprobanteDetalleGeneral(numi)

        If vacio = 1 Then
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "Diconta".ToUpper
            ef.Header = "¿Desea Importar los montos del comprobante?".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                For Each fila As DataRow In dt.Rows
                    'fila.Item("obdebebs") = 0
                    'fila.Item("obhaberbs") = 0
                    'fila.Item("obdebeus") = 0
                    'fila.Item("obhaberus") = 0
                    fila.Item("estado") = 0
                    fila.Item("obtc") = tbTipoCambio.Value

                Next
            Else
                For Each fila As DataRow In dt.Rows
                    fila.Item("obdebebs") = 0
                    fila.Item("obhaberbs") = 0
                    fila.Item("obdebeus") = 0
                    fila.Item("obhaberus") = 0
                    fila.Item("estado") = 0
                    fila.Item("obtc") = tbTipoCambio.Value

                Next
            End If


        End If
        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("obnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("obnumito1")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("oblin")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obcuenta")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cadesc2")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cacta")
            .Caption = "CUENTA"
            .HeaderAlignment = TextAlignment.Center
            .Width = 60
            .AllowSort = False
            .Visible = False
            '.EditType = EditType.NoEdit
        End With
        With grDetalle.RootTable.Columns("cactaucg")
            .Caption = "CUENTA"
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .AllowSort = False
            '.EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("cadesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "CUENTA"
            .Width = 200
            .EditType = EditType.NoEdit
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("camon")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("numAux")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obaux1")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("desc1")
            .Caption = "AUX 1"
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = Color.DodgerBlue
            .AllowSort = False
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obaux2")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("desc2")
            .Caption = "AUX 2"
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = Color.DodgerBlue
            .AllowSort = False
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obaux3")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("desc3")
            .Caption = "AUX 3"
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = Color.DodgerBlue
            .AllowSort = False
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obobs")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "REFERENCIA"
            .Width = 300
            .MaxLength = 200
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("obobs2")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "DETALLE"
            .Width = 150
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obcheque")
            .Caption = "CHEQUE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .AllowSort = False
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obtc")
            .Caption = "TC"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = tbTipoCambio.Value
            .AllowSort = False
            .EditType = EditType.NoEdit
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obdebebs")
            .Caption = "DEBE BS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightBlue
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("obhaberbs")
            .Caption = "HABER BS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightBlue
            .AggregateFunction = AggregateFunction.Sum
            .AllowSort = False
            .TotalFormatString = "0.00"
        End With

        With grDetalle.RootTable.Columns("obdebeus")
            .Caption = "DEBE SUS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .AllowSort = False
            .TotalFormatString = "0.00"

        End With

        With grDetalle.RootTable.Columns("obhaberus")
            .Caption = "HABER SUS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .AllowSort = False
            .TotalFormatString = "0.00"

        End With

        With grDetalle.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle.RootTable.Columns("numiCobrar")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle.RootTable.Columns("descCobrar")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("numiCompra")
            .Visible = False
            .DefaultValue = 0
        End With
        With grDetalle.RootTable.Columns("imgCompra")
            .HeaderAlignment = TextAlignment.Center
            .CellStyle.TextAlignment = TextAlignment.Center
            .CellStyle.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Center
            .CellStyle.ImageVerticalAlignment = Janus.Windows.GridEX.ImageVerticalAlignment.Center
            .Caption = "COMPRA"
            .Width = 70
            .Visible = False
        End With

        With grDetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

            .NewRowPosition = NewRowPosition.BottomRow

            'tratando de ocultar las cabeceras
            '.ColumnHeaders = InheritableBoolean.False

            'poner estilo a la celda seleccionada
            .FocusCellFormatStyle.BackColor = Color.Pink
        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalle.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grDetalle.RootTable.FormatConditions.Add(fc)

        'cargar la grilla donde se va a poner la diferencia
        '_prCargarGridDetalle2()

    End Sub

    Private Sub _prCargarGridDetalle2()
        Dim dt As New DataTable
        dt = L_prComprobanteDetalleGeneral(-1)
        dt.Rows.Add()
        dt.Rows(0).Item("obcheque") = "DIFERENCIA: "

        grDetalle2.DataSource = dt
        grDetalle2.RetrieveStructure()

        Dim _color As Color = Color.LightGray

        'dar formato a las columnas
        With grDetalle2.RootTable.Columns("obnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle2.RootTable.Columns("obnumito1")
            .Width = 50
            .Visible = False
        End With

        With grDetalle2.RootTable.Columns("oblin")
            .Width = 50
            .Visible = False
        End With

        With grDetalle2.RootTable.Columns("obcuenta")
            .Width = 50
            .Visible = False
        End With

        With grDetalle2.RootTable.Columns("cadesc2")
            .Width = 50
            .Visible = False
        End With

        With grDetalle2.RootTable.Columns("cacta")
            .Caption = "CUENTA"
            .HeaderAlignment = TextAlignment.Center
            .Width = 60
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = _color
        End With

        With grDetalle2.RootTable.Columns("cadesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "CUENTA"
            .Width = 200
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("camon")
            .Width = 50
            .Visible = False
        End With

        With grDetalle2.RootTable.Columns("numAux")
            .Visible = False
        End With

        With grDetalle2.RootTable.Columns("obaux1")
            .Visible = False
        End With
        With grDetalle2.RootTable.Columns("desc1")
            .Caption = "AUX 1"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obaux2")
            .Visible = False
        End With
        With grDetalle2.RootTable.Columns("desc2")
            .Caption = "AUX 2"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obaux3")
            .Visible = False
        End With
        With grDetalle2.RootTable.Columns("desc3")
            .Caption = "AUX 3"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obobs")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "REFERENCIA"
            .Width = 300
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obobs2")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "DETALLE"
            .Width = 150
            .Visible = False
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obcheque")
            .Caption = "CHEQUE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100 + 70
            .CellStyle.FontBold = TriState.True
            .CellStyle.BackColor = _color
            .Visible = False

        End With

        With grDetalle2.RootTable.Columns("obtc")
            .Caption = "TIPO CAMBIO"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70 - 70
            .FormatString = "0.00"
            .DefaultValue = tbTipoCambio.Value
            .Visible = False
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obdebebs")
            .Caption = "DEBE BS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightBlue
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obhaberbs")
            .Caption = "HABER BS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightBlue
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obdebeus")
            .Caption = "DEBE SUS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightGreen
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("obhaberus")
            .Caption = "HABER SUS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightGreen
            .CellStyle.BackColor = _color

        End With

        With grDetalle2.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle2.RootTable.Columns("numiCobrar")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle.RootTable.Columns("descCobrar")
            .Visible = False
        End With

        With grDetalle2
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales


            'tratando de ocultar las cabeceras
            .ColumnHeaders = InheritableBoolean.False
        End With





    End Sub

    Private Sub _prCargarGridDetalleRecuperado(dt As DataTable)

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("obnumi")
            .Width = 50
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("obnumito1")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("oblin")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obcuenta")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cadesc2")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cacta")
            .Caption = "CUENTA"
            .HeaderAlignment = TextAlignment.Center
            .Width = 60
            .AllowSort = False
            '.EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("cadesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "CUENTA"
            .Width = 200
            .EditType = EditType.NoEdit
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("camon")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("numAux")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obaux1")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("desc1")
            .Caption = "AUX 1"
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = Color.DodgerBlue
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("obaux2")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("desc2")
            .Caption = "AUX 2"
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = Color.DodgerBlue
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("obaux3")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("desc3")
            .Caption = "AUX 3"
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit
            .CellStyle.BackColor = Color.DodgerBlue
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("obobs")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "REFERENCIA"
            .Width = 300
            .MaxLength = 200
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("obobs2")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "DETALLE"
            .Width = 150
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obcheque")
            .Caption = "CHEQUE"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 100
            .AllowSort = False
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("obtc")
            .Caption = "TC"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = tbTipoCambio.Value
            .AllowSort = False
            .EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("obdebebs")
            .Caption = "DEBE BS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightBlue
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
            .AllowSort = False

        End With

        With grDetalle.RootTable.Columns("obhaberbs")
            .Caption = "HABER BS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightBlue
            .AggregateFunction = AggregateFunction.Sum
            .AllowSort = False
            .TotalFormatString = "0.00"
        End With

        With grDetalle.RootTable.Columns("obdebeus")
            .Caption = "DEBE SUS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .AllowSort = False
            .TotalFormatString = "0.00"

        End With

        With grDetalle.RootTable.Columns("obhaberus")
            .Caption = "HABER SUS."
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
            .DefaultValue = 0
            .CellStyle.BackColor = Color.LightGreen
            .AggregateFunction = AggregateFunction.Sum
            .AllowSort = False
            .TotalFormatString = "0.00"

        End With

        With grDetalle.RootTable.Columns("estado")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle.RootTable.Columns("numiCobrar")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle.RootTable.Columns("descCobrar")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("numiCompra")
            .Visible = False
            .DefaultValue = 0
        End With

        With grDetalle.RootTable.Columns("imgCompra")
            .HeaderAlignment = TextAlignment.Center
            .CellStyle.TextAlignment = TextAlignment.Center
            .CellStyle.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Center
            .CellStyle.ImageVerticalAlignment = Janus.Windows.GridEX.ImageVerticalAlignment.Center
            .Caption = "COMPRA"
            .Width = 70
            .Visible = False
        End With

        With grDetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'poner fila de totales
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

            .NewRowPosition = NewRowPosition.BottomRow

            'tratando de ocultar las cabeceras
            '.ColumnHeaders = InheritableBoolean.False

            'poner estilo a la celda seleccionada
            .FocusCellFormatStyle.BackColor = Color.Pink
        End With

        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grDetalle.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.BackColor = Color.LightGreen
        'grDetalle.RootTable.FormatConditions.Add(fc)

        'cargar la grilla donde se va a poner la diferencia
        '_prCargarGridDetalle2()

    End Sub


    Private Sub _prCargarGridAyudaCuenta()


        Dim dt As New DataTable
        dt = L_prCuentaGeneralBasicoParaComprobante(gi_empresaNumi)

        grAyudaCuenta.DataSource = dt
        grAyudaCuenta.RetrieveStructure()

        'dar formato a las columnas
        With grAyudaCuenta.RootTable.Columns("canumi")
            .Width = 50
            .Visible = False
        End With


        With grAyudaCuenta.RootTable.Columns("cacta")
            .Caption = "CODIGO"
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("cactaucg")
            .Caption = "CODIGO"
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
        End With

        With grAyudaCuenta.RootTable.Columns("cadesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "CUENTA"
            .Width = 400
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
        End With

        With grAyudaCuenta.RootTable.Columns("camon")
            .Caption = "MONEDA"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 80
            .EditType = EditType.NoEdit

        End With

        With grAyudaCuenta.RootTable.Columns("catipo")
            .Visible = False
        End With



        With grAyudaCuenta.RootTable.Columns("cndesc1")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "TIPO"
            .Width = 150

        End With

        With grAyudaCuenta.RootTable.Columns("cadesc2")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("numAux")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("catipo1")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("caniv")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("isPagar")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("isCobrar")
            .Visible = False
        End With
        With grAyudaCuenta.RootTable.Columns("isCompra")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("modulo")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("sucursal")
            .Visible = False
        End With

        With grAyudaCuenta
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'habilitar buscador
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            .ContextMenuStrip = Nothing

        End With

        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("catipo"), ConditionOperator.Equal, 1)
        fc.FormatStyle.BackColor = Color.LightGreen
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("catipo"), ConditionOperator.Equal, 2)
        fc.FormatStyle.BackColor = Color.LightYellow
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("catipo"), ConditionOperator.Equal, 3)
        fc.FormatStyle.BackColor = Color.LightBlue
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("catipo"), ConditionOperator.Equal, 4)
        fc.FormatStyle.BackColor = Color.LightCoral
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("catipo"), ConditionOperator.Equal, 5)
        fc.FormatStyle.BackColor = Color.LightSlateGray
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("catipo"), ConditionOperator.Equal, 6)
        fc.FormatStyle.BackColor = Color.LightGreen
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        'setear todas las que son mayores
        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("caniv"), ConditionOperator.LessThanOrEqualTo, 4)
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.Red
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grAyudaCuenta.RootTable.Columns("caniv"), ConditionOperator.LessThanOrEqualTo, 5)
        fc.FormatStyle.FontBold = TriState.True
        grAyudaCuenta.RootTable.FormatConditions.Add(fc)

        'poner el focus en la grilla
        grAyudaCuenta.Focus()
        grAyudaCuenta.MoveTo(grAyudaCuenta.FilterRow)
        grAyudaCuenta.Col = 1

        'poner en el tag a cual corresponde
        grAyudaCuenta.Tag = 0
    End Sub

    Private Sub _prCargarGridAyudaClienteCobrar(numiCuenta As String)


        Dim dt As New DataTable
        dt = L_prClientesComprobante(1, numiCuenta)

        grAyudaCuenta.DataSource = dt
        grAyudaCuenta.RetrieveStructure()

        'dar formato a las columnas
        With grAyudaCuenta.RootTable.Columns("cjnumi")
            .Width = 50
            .Visible = False
        End With


        With grAyudaCuenta.RootTable.Columns("cjci")
            .Caption = "CI"
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .Visible = False
            .DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
        End With

        With grAyudaCuenta.RootTable.Columns("cjnombre")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "NOMBRE"
            .Width = 400
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
        End With

        With grAyudaCuenta.RootTable.Columns("cjtipo")
            .Visible = False
        End With


        With grAyudaCuenta
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'habilitar buscador
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            .ContextMenuStrip = cmOpcionesAyuda

        End With

        'poner el focus en la grilla
        grAyudaCuenta.Focus()
        grAyudaCuenta.MoveTo(grAyudaCuenta.FilterRow)
        grAyudaCuenta.Col = 1

        'poner en el tag a cual corresponde
        grAyudaCuenta.Tag = 0
    End Sub

    Private Sub _prCargarGridAyudaClientePagar(numiCuenta As String)


        Dim dt As New DataTable
        dt = L_prClientesComprobante(2, numiCuenta)

        grAyudaCuenta.DataSource = dt
        grAyudaCuenta.RetrieveStructure()

        'dar formato a las columnas
        With grAyudaCuenta.RootTable.Columns("cjnumi")
            .Width = 50
            .Visible = False
        End With


        With grAyudaCuenta.RootTable.Columns("cjci")
            .Caption = "CI"
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .Visible = False
            .DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
        End With

        With grAyudaCuenta.RootTable.Columns("cjnombre")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "NOMBRE"
            .Width = 400
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
        End With

        With grAyudaCuenta.RootTable.Columns("cjtipo")
            .Visible = False
        End With


        With grAyudaCuenta
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'habilitar buscador
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            .ContextMenuStrip = cmOpcionesAyuda
        End With

        'poner el focus en la grilla
        grAyudaCuenta.Focus()
        grAyudaCuenta.MoveTo(grAyudaCuenta.FilterRow)
        grAyudaCuenta.Col = 1

        'poner en el tag a cual corresponde
        grAyudaCuenta.Tag = 0
    End Sub
    Private Sub _prCargarGridAyudaAuxiliar(pos As Integer, numiAux As Integer)

        Dim dt, dtAuxiliaresCuenta As DataTable
        '' here
        dtAuxiliaresCuenta = L_prCuentaDetalleGeneral(grDetalle.GetValue("obcuenta"))

        dt = L_prAuxiliarDetalleGeneral(dtAuxiliaresCuenta.Rows(pos - 1).Item("cenumitc3"))
        Dim descAuxiliar As String = dtAuxiliaresCuenta.Rows(pos - 1).Item("ccdesc")

        grAyudaCuenta.DataSource = dt
        grAyudaCuenta.RetrieveStructure()

        'dar formato a las columnas
        Dim _color As Color = Color.DodgerBlue
        With grAyudaCuenta.RootTable.Columns("cdnumi")
            .Caption = "CODIGO"
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .CellStyle.BackColor = _color
        End With

        With grAyudaCuenta.RootTable.Columns("cddesc")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "auxliar".ToUpper
            .Width = 400
            .CellStyle.BackColor = _color

        End With

        With grAyudaCuenta.RootTable.Columns("cdnumitc3")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("cdest")
            .Visible = False
        End With

        With grAyudaCuenta.RootTable.Columns("cdest2")
            .Visible = False
        End With
        With grAyudaCuenta.RootTable.Columns("estado")
            .Visible = False
        End With

        With grAyudaCuenta
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            'habilitar buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            '.BackColor = _color

        End With

        'poner el focus en la grilla
        If numiAux > 0 Then
            Dim j As Integer
            For j = 0 To dt.Rows.Count - 1
                If dt.Rows(j).Item("cdnumi") = numiAux Then
                    Exit For
                End If
            Next

            grAyudaCuenta.Focus()
            grAyudaCuenta.MoveTo(j)
            grAyudaCuenta.Col = 1


        Else
            grAyudaCuenta.Focus()
            grAyudaCuenta.MoveTo(grAyudaCuenta.FilterRow)
            grAyudaCuenta.Col = 1
        End If

        'poner en el tag a cual corresponde
        grAyudaCuenta.Tag = pos
    End Sub

    Private Sub _prEliminarFilaDetalle()
        If grDetalle.Row >= 0 Then

            Dim estado As Integer = grDetalle.GetValue("estado")

            If estado = 1 Or estado = 2 Then
                grDetalle.GetRow(grDetalle.Row).BeginEdit()
                grDetalle.CurrentRow.Cells.Item("estado").Value = -1
            Else
                grDetalle.GetRow(grDetalle.Row).BeginEdit()
                grDetalle.CurrentRow.Cells.Item("estado").Value = -2
            End If


            grDetalle.RemoveFilters()
            grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThan, -1))

            _prIsBalanceado()
        End If
    End Sub

    Private Sub _prAñadirTipoCambio()
        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, "btConfTipoCambio")

        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")

        If add = True Then
            Dim frm As New F0_TipoCambio_Nuevo
            frm.tbFecha.Value = tbFecha.Value
            frm.ShowDialog()
            tbFecha.Value = DateAdd(DateInterval.Day, -1, tbFecha.Value)
            tbFecha.Value = DateAdd(DateInterval.Day, 1, tbFecha.Value)
        Else
            ToastNotification.Show(Me, "el usario no cuenta con los permisos para adicionar tipo de cambio".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If

    End Sub

    Private Sub _prIsBalanceado()
        'grDetalle.Refetch()

        'consulto si esta descuadrado el total
        Dim debeBs As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obdebebs"), AggregateFunction.Sum)
        Dim haberBs As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obhaberbs"), AggregateFunction.Sum)
        Dim debeSus As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obdebeus"), AggregateFunction.Sum)
        Dim haberSus As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obhaberus"), AggregateFunction.Sum)

        '--------------------colocar la diferencia
        tbDiferenciaBs.Text = (debeBs - haberBs).ToString("0.00")
        tbDiferenciaSus.Text = (debeSus - haberSus).ToString("0.00")

        '---------------------------------------------

        'colocar la diferencia
        ''If debeBs = haberBs Then
        ''    CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obdebebs") = 0
        ''    CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obhaberbs") = 0
        ''Else
        ''    If debeBs > haberBs Then
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obdebebs") = 0
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obhaberbs") = debeBs - haberBs
        ''    Else
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obdebebs") = haberBs - debeBs
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obhaberbs") = 0
        ''    End If

        ''End If


        ''If debeSus = haberSus Then
        ''    CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obdebeus") = 0
        ''    CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obhaberus") = 0
        ''Else
        ''    If debeSus > haberSus Then
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obdebeus") = 0
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obhaberus") = debeSus - haberSus
        ''    Else
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obdebeus") = haberSus - debeSus
        ''        CType(grDetalle2.DataSource, DataTable).Rows(0).Item("obhaberus") = 0
        ''    End If
        ''End If

        'tbBalanceBs.Tag = False
        'If debeBs = haberBs Then
        '    tbBalanceBs.Value = True
        'Else
        '    tbBalanceBs.Value = False
        'End If

        'tbBalanceSus.Tag = False
        'If debeSus = haberSus Then
        '    tbBalanceSus.Value = True
        'Else
        '    tbBalanceSus.Value = False
        'End If

        'tbBalanceBs.Tag = True
        'tbBalanceSus.Tag = True
    End Sub

    Private Sub _prPonerLine(ByRef dt As DataTable)
        _detalleDetalle.Rows.Clear()

        Dim i As Integer = 1
        For Each fila As DataRow In dt.Rows
            If fila.Item("estado") = 0 Or fila.Item("estado") = 1 Or fila.Item("estado") = 2 Then
                fila.Item("oblin") = i
                If fila.Item("estado") = 0 Or fila.Item("estado") = 2 Then
                    If fila.Item("numiCobrar") > 0 Then
                        _detalleDetalle.Rows.Add(-1, i, fila.Item("numiCobrar"))
                    End If
                    If fila.Item("numiCompra") > 0 Then
                        _detalleDetalleCompras.Rows(fila.Item("numiCompra") - 1).Item("fcanumito11") = i
                    End If
                End If

                i = i + 1
            End If
        Next
    End Sub

    Private Sub _prImportar()
        If btnGrabar.Enabled = True Then
            If _existTipoCambio = True Then
                Dim frm As New F0_ComprobanteImportar
                frm.ShowDialog()
                If frm.seleccionado = True Then
                    _prCargarComrobanteImportado(frm.filaSelect)
                End If
            Else
                ToastNotification.Show(Me, "primero ingrese tipo de cambio".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)

            End If

        End If
    End Sub

    Private Sub _prOrdenarDetalle()
        If btnGrabar.Enabled = True Then
            Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)

            Dim filasFiltradas As DataRow() = dt.Select("1=1", "obdebebs desc")
            If filasFiltradas.Count > 0 Then
                dt = filasFiltradas.CopyToDataTable
                grDetalle.DataSource = dt
            End If
        End If
    End Sub
    Public Sub _prCargarComrobanteImportado(filaSelect As Janus.Windows.GridEX.GridEXRow)


        With filaSelect
            'tbNumi.Text = .Cells("oanumi").ToString
            'tbNroDoc.Text = .Cells("oanumdoc").ToString
            tbTipo.Value = .Cells("oatip").Value
            'tbAnio.Text = .Cells("oaano").ToString
            'tbMes.Text = .Cells("oames").ToString
            'tbNum.Text = .Cells("oanum").ToString
            'tbFecha.Value = .Cells("oafdoc")
            'tbTipoCambio.Value = .Cells("oatc")
            tbGlosa.Text = .Cells("oaglosa").Value.ToString
            tbObs.Text = .Cells("oaobs").Value.ToString

            Dim numi As String = .Cells("oanumi").Value.ToString

            'CARGAR DETALLE
            _prCargarGridDetalle(numi, 1)
        End With


    End Sub

    Private Sub _prImprimir()
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim objrep As New R_Comprobante2
        Dim dt As New DataTable
        dt = L_prComprobanteReporteComprobante(tbNumi.Text)
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        _Ds = L_prComprobanteReporteComprobante1(tbNumi.Text)
        'Literal 

        _TotalLi = _Ds.Tables(0).Rows(0).Item("obhaberus")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 DOLARES AMERICANOS"
        Dim presidente = "", tesorero = "", conforme = ""
        Dim nombre As String = tbNombre.Text
        Dim banco As String = tbBanco.Text
        Dim cheque As String = tbChque.Text
        Dim impresion As String = DateTime.Now
        'ahora lo mando al visualizador
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("fechaDesde", "")
        objrep.SetParameterValue("fechaHasta", "")
        objrep.SetParameterValue("titulo", "" + gs_empresaDesc.ToUpper)
        objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)
        objrep.SetParameterValue("ultimoRegistro", 0)
        objrep.SetParameterValue("Autor", gs_user)
        objrep.SetParameterValue("nombre", nombre)
        objrep.SetParameterValue("banco", banco)
        objrep.SetParameterValue("cheque", cheque)
        objrep.SetParameterValue("fechaImpresion", "Fecha de Impresion: " + impresion)
        objrep.SetParameterValue("literal", li.ToUpper)
        If tbTipo.Value = 1 Then
            conforme = "Entregué Conforme"
        ElseIf tbTipo.Value = 2 Then
            presidente = "Presidente"
            tesorero = "Tesorero"
            conforme = "Recibí Conforme"
        End If
        objrep.SetParameterValue("presidente", presidente)
        objrep.SetParameterValue("tesorero", tesorero)
        objrep.SetParameterValue("conforme", conforme)


        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

    End Sub

    Private Sub _prCargarOpcionesDeCuentasAutomaticas()

        If tbTipo.SelectedIndex < 0 Then
            ToastNotification.Show(Me, "seleccione el tipo de comprobante".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
            Return
        End If

        Dim numiTipo As Integer = tbTipo.Value
        Dim dt As DataTable = New DataTable
        Select Case numiTipo
            Case 1
                dt = L_prCuentasAutomaticaObtenerIngresos()
            Case 2
                dt = L_prCuentasAutomaticaObtenerEgresos()
            Case 3
                dt = L_prCuentasAutomaticaObtenerTraspaso()
        End Select

        Me.cmOpcionesDetalle.Items.Clear()

        For Each fila As DataRow In dt.Rows
            Dim numi As String = fila.Item("cgnumi")
            Dim desc As String = fila.Item("cgdesc")

            Dim subItem As ToolStripMenuItem
            subItem = New ToolStripMenuItem()

            subItem.Image = Global.Presentacion.My.Resources.Resources.add2
            subItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            subItem.Name = numi
            subItem.Size = New System.Drawing.Size(274, 36)
            subItem.Text = desc

            AddHandler subItem.Click, AddressOf subItem_Click

            'Me.msTiposReforzamiento.Items.AddRange(New ToolStripItem() {Me.ToolStripMenuItem1, Me.VEROBSERVACIONESDECLASESToolStripMenuItem, Me.ASIGNARTODASLASASISTENCIASToolStripMenuItem, Me.FINALIZARCURSOToolStripMenuItem})
            Me.cmOpcionesDetalle.Items.Add(subItem)

        Next

        Dim subItem1 As ToolStripMenuItem
        subItem1 = New ToolStripMenuItem()
        subItem1.Image = Global.Presentacion.My.Resources.Resources.elim_fila2
        subItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        subItem1.Name = "subItemEliminar"
        subItem1.Size = New System.Drawing.Size(274, 36)
        subItem1.Text = "ELIMINAR"
        AddHandler subItem1.Click, AddressOf ELIMINAR_Click
        Me.cmOpcionesDetalle.Items.Add(subItem1)

        Dim subItem2 As ToolStripMenuItem
        subItem2 = New ToolStripMenuItem()
        subItem2.Name = "subItemNuevaFila"
        subItem2.Size = New System.Drawing.Size(274, 36)
        subItem2.Text = "INSERTAR FILA"
        AddHandler subItem2.Click, AddressOf INSERTARFILAToolStripMenuItem_Click
        Me.cmOpcionesDetalle.Items.Add(subItem2)

    End Sub

    Private Function to3Decimales(num As Double) As Double
        Dim res As Double = 0
        'If num > 0 Then
        '    Dim parteEntera As Double = Int(num)
        '    Dim decimalStrin As String = Int(num * 1000)
        '    Dim parteDecimal As Double = Convert.ToDouble("0." & decimalStrin.Substring(decimalStrin.Count - 3))
        '    res = parteEntera + parteDecimal
        'End If

        Dim numeroString As String = num.ToString()
        Dim posicionPuntoDecimal As Integer = numeroString.IndexOf(".")

        If posicionPuntoDecimal > 0 Then
            Dim cantidadDecimales As Integer = numeroString.Substring(posicionPuntoDecimal).Count - 1
            If cantidadDecimales >= 3 Then
                numeroString = numeroString.Substring(0, posicionPuntoDecimal + 4)
                res = Convert.ToDouble(numeroString)
            Else
                res = num
            End If

        Else
            res = num
        End If
        Return Res
    End Function

    Private Sub ELIMINAR_Click(sender As Object, e As EventArgs)
        _prEliminarFilaDetalle()
    End Sub

    Private Sub subItem_Click(sender As Object, e As EventArgs)
        Dim f, c As Integer
        f = grDetalle.Row
        c = grDetalle.Col
        Dim subItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

        If f >= 0 And c >= 0 Then
            Dim numiCuentaAutomatica As String = subItem.Name.ToString.Trim
            Dim dtDetalle As DataTable = L_prCuentasAutomaticaDetalleGeneralCompleto(numiCuentaAutomatica)

            Dim monto As Double = 0
            monto = grDetalle.GetValue("obdebebs")
            If monto = 0 Then
                monto = grDetalle.GetValue("obhaberbs")
                If monto = 0 Then


                End If

            End If
            Dim numiAux1 As Integer = grDetalle.GetValue("obaux1")
            Dim Aux1 As String = grDetalle.GetValue("desc1")

            For Each fila As DataRow In dtDetalle.Rows
                Dim numiCuenta As String = fila.Item("chnumitc1")
                Dim codCuenta As String = fila.Item("cacta")
                Dim cuenta As String = fila.Item("cadesc")
                Dim moneda As String = fila.Item("camon")
                Dim porcentaje As Integer = fila.Item("chporcen")
                Dim cuentaPadre As String = fila.Item("cadesc2")
                Dim numAux As String = fila.Item("numAux")

                Dim total As Double = monto * (porcentaje / 100)

                Dim isDebe As Boolean = fila.Item("chdebe")
                Dim debeBs As Double = 0
                Dim haberBs As Double = 0
                Dim debeSus As Double = 0
                Dim haberSus As Double = 0

                If isDebe = True Then 'es debe
                    debeBs = total
                    debeSus = Round(to3Decimales(total / tbTipoCambio.Value), 2)
                Else 'es haber
                    haberBs = total
                    haberSus = Round(to3Decimales(total / tbTipoCambio.Value), 2)
                End If

                Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
                'a.obnumi,a.obnumito1,a.oblin,a.obcuenta,b.cacta,b.cadesc,c.cadesc as cadesc2,b.camon,numAux,
                'obaux1,desc1,obaux2,desc2,obaux3,desc3,a.obobs,a.obobs2,a.obcheque,a.obtc,
                'a.obdebebs,a.obhaberbs,a.obdebeus, a.obhaberus, estado
                dt.Rows.Add(0, 0, 0, numiCuenta, codCuenta, cuenta, cuentaPadre, moneda, numAux,
                            numiAux1, Aux1, 0, "", 0, "", "", "", "", tbTipoCambio.Value,
                            debeBs, haberBs, debeSus, haberSus, 0)


            Next

            _prIsBalanceado()
        End If

    End Sub

    Private Sub _prInsertarFila()
        If grDetalle.Row >= 0 Then

            Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
            Dim fila As DataRow = dt.NewRow
            fila.Item("estado") = 0
            fila.Item("obtc") = tbTipoCambio.Value
            fila.Item("obdebebs") = 0
            fila.Item("obhaberbs") = 0
            fila.Item("obdebeus") = 0
            fila.Item("obhaberus") = 0
            fila.Item("numiCobrar") = 0
            fila.Item("numiCompra") = 0

            dt.Rows.InsertAt(fila, grDetalle.Row)


            grDetalle.MovePrevious()
            _prCargarGridAyudaCuenta()

        End If
    End Sub

    Private Sub _prInsertarImagen()
        Dim img As Bitmap
        Dim img2 As Bitmap

        img = New Bitmap(Global.Presentacion.My.Resources.Resources.edit2)
        img2 = New Bitmap(img, 15, 15)
        'grDetalle.SetValue("imgCompra", _fnImageToByteArray2(img2))
        grDetalle.SetValue("imgCompra", img2)

    End Sub
    Public Function _fnImageToByteArray(ByVal bitmap As Bitmap) As Byte()

        Dim img As Bitmap = New Bitmap(bitmap)
        Dim Bin As New MemoryStream
        img.Save(Bin, Imaging.ImageFormat.Jpeg)

        Return Bin.GetBuffer
    End Function
    Public Function _fnImageToByteArray2(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function


    Private Sub _prRecuperar()
        If True Then 'btnGrabar.Enabled = True And _MNuevo = True
            If _existTipoCambio = True Then
                Dim frm As New F0_ComprobanteRecuperar
                frm.ShowDialog()
                If frm.seleccionado = True Then
                    _prCargarGridDetalleRecuperado(frm._dtSeleccionado)

                End If
            Else
                ToastNotification.Show(Me, "primero ingrese tipo de cambio".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)

            End If

        End If
    End Sub
#End Region

#Region "METODOS PARA LLENAR"

    Public Sub _PMOHabilitar()

        'tbAnio.ReadOnly = False
        'tbMes.ReadOnly = False
        If tbTipo.SelectedIndex = 0 Then
            tbNombre.ReadOnly = False
            tbBanco.ReadOnly = False
        ElseIf tbTipo.SelectedIndex = 1 Then
            tbNombre.ReadOnly = False
            tbBanco.ReadOnly = False
            tbChque.ReadOnly = False
        End If
        tbGlosa.ReadOnly = False
        'tbNroDoc.ReadOnly = False
        'tbTipoCambio.IsInputReadOnly = False
        tbFecha.Enabled = True
        tbObs.ReadOnly = False

        PanelInferior.Visible = False

        If _MNuevo = True Then
            tbTipo.ReadOnly = False

        End If

        grDetalle.ContextMenuStrip = cmOpcionesDetalle
        grDetalle.AllowAddNew = InheritableBoolean.True
        grDetalle.AllowEdit = InheritableBoolean.True

        'tbBalanceBs.IsReadOnly = False
        'tbBalanceSus.IsReadOnly = False
    End Sub

    Public Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbMes.ReadOnly = True
        tbAnio.ReadOnly = True
        tbNum.ReadOnly = True

        tbGlosa.ReadOnly = True
        tbNroDoc.ReadOnly = True
        tbTipo.ReadOnly = True
        tbTipoCambio.IsInputReadOnly = True
        tbFecha.Enabled = False
        tbObs.ReadOnly = True

        tbReferencia.ReadOnly = True
        tbCuentaSuperior.ReadOnly = True

        grDetalle.ContextMenuStrip = Nothing
        grDetalle.AllowAddNew = InheritableBoolean.False
        grDetalle.AllowEdit = InheritableBoolean.False

        btnNuevoTipoCambio.Visible = False

        panelAyudaCuenta.Visible = False

        PanelInferior.Visible = True

        'tbBalanceBs.IsReadOnly = True
        'tbBalanceSus.IsReadOnly = True
    End Sub

    Public Sub _PMOLimpiar()
        'VACIO EL DETALLE
        _prCargarGridDetalle(-1)

        tbNumi.Text = ""
        'tbAnio.Text = ""
        tbFecha.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        tbFecha.Value = Now.Date
        tbGlosa.Text = ""
        'tbMes.Text = ""
        tbNroDoc.Text = ""
        tbNum.Text = ""
        tbTipo.Text = ""
        'tbTipoCambio.Value = 0
        tbObs.Text = ""
        tbNombre.Text = ""
        tbChque.Text = ""
        tbBanco.Text = ""
        tbCuentaSuperior.Text = ""
        tbReferencia.Text = ""

        tbMes.Text = tbFecha.Value.Month
        tbAnio.Text = tbFecha.Value.Year

        tbDiferenciaBs.Text = ""
        tbDiferenciaSus.Text = ""

        _detalleDetalleCompras.Rows.Clear()
    End Sub

    Public Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbAnio.BackColor = Color.White
        tbGlosa.BackColor = Color.White
        tbMes.BackColor = Color.White
        tbNroDoc.BackColor = Color.White
        tbNum.BackColor = Color.White
        tbTipo.BackColor = Color.White
        tbTipoCambio.BackgroundStyle.BackColor = Color.White
    End Sub

    Public Function _PMOGrabarRegistro() As Boolean
        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
        _prPonerLine(dtDetalle)
        dtDetalle = dtDetalle.DefaultView.ToTable(True, "obnumi", "obnumito1", "oblin", "obcuenta", "obaux1", "obaux2", "obaux3", "obobs", "obobs2", "obcheque", "obtc", "obdebebs", "obhaberbs", "obdebeus", "obhaberus", "estado")


        If tbMes.Text.Trim.Count = 1 Then
            tbMes.Text = "0" + tbMes.Text
        End If
        Dim fecha As DateTime = New Date(tbFecha.Value.Year, tbFecha.Value.Month, tbFecha.Value.Day, Now.Hour, Now.Minute, Now.Second)

        Dim res As Boolean = L_prComprobanteGrabar(tbNumi.Text, tbNroDoc.Text, tbTipo.Value, tbAnio.Text, tbMes.Text, tbNum.Text, fecha.ToString("yyyy-MM-dd"), tbTipoCambio.Value, tbGlosa.Text, tbObs.Text, gi_empresaNumi, dtDetalle, _detalleDetalle, _detalleDetalleCompras, gs_user, tbNombre.Text, tbBanco.Text, tbChque.Text)
        If res Then

            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _prImprimir()

            L_prComprobanteEliminarRespaldo(gi_userNumi)
        End If
        Return res

    End Function

    Public Function _PMOModificarRegistro() As Boolean
        tbEmpresa.Focus()

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
        _prPonerLine(dtDetalle)
        dtDetalle = dtDetalle.DefaultView.ToTable(True, "obnumi", "obnumito1", "oblin", "obcuenta", "obaux1", "obaux2", "obaux3", "obobs", "obobs2", "obcheque", "obtc", "obdebebs", "obhaberbs", "obdebeus", "obhaberus", "estado")

        Dim fecha As DateTime = New Date(tbFecha.Value.Year, tbFecha.Value.Month, tbFecha.Value.Day, Now.Hour, Now.Minute, Now.Second)

        Dim res As Boolean = L_prComprobanteModificar(tbNumi.Text, tbNroDoc.Text, tbTipo.Value, tbAnio.Text, tbMes.Text, tbNum.Text, fecha.ToString("yyyy/MM/dd hh:mm:ss"), tbTipoCambio.Value, tbGlosa.Text, tbObs.Text, gi_empresaNumi, dtDetalle, _detalleDetalle, tbNombre.Text, tbBanco.Text, tbChque.Text)
        If res Then

            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            '_PSalirRegistro()
        End If
        Return res
    End Function

    Public Sub _PMOEliminarRegistro()
        Dim dtUltimoCompr As DataTable = L_prObtenerUltimoComprobantePorTipoAnioMesEmpresa(tbTipo.Value, tbAnio.Text, tbMes.Text, gi_empresaNumi)
        If dtUltimoCompr.Rows.Count > 0 Then
            'If dtUltimoCompr.Rows(0).Item("oanumi") = tbNumi.Text Then
            Dim info As New TaskDialogInfo("anulacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de anular el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_prComprobanteBorrar(tbNumi.Text, tbNroDoc.Text, tbTipo.Value, tbAnio.Text, tbMes.Text, tbNum.Text, tbFecha.Value.ToString("yyyy/MM/dd"), tbTipoCambio.Value, tbGlosa.Text, tbObs.Text, gi_empresaNumi, mensajeError)
                If res Then
                    ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Anulado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PMFiltrar()
                Else
                    ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If
            End If
            'Else
            'ToastNotification.Show(Me, "solo se puede eliminar el ultimo comprobante".ToUpper, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            'End If
        End If


    End Sub
    Public Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        Dim _okBs As Boolean = True
        MEP.Clear()

        If tbTipoCambio.Value = 0 Then
            tbTipoCambio.BackgroundStyle.BackColor = Color.Red
            MEP.SetError(tbTipoCambio, "no existe tipo de cambio para la fecha seleccionada!".ToUpper)
            _ok = False
        Else
            tbTipoCambio.BackgroundStyle.BackColor = Color.White
            MEP.SetError(tbTipoCambio, "")
        End If

        If tbTipo.SelectedIndex < 0 Then
            tbTipo.BackColor = Color.Red
            MEP.SetError(tbTipo, "seleccione el tipo de comprobante!".ToUpper)
            _ok = False
        Else
            tbTipo.BackColor = Color.White
            MEP.SetError(tbTipo, "")
        End If

        'verificar si esta cuadrado
        Dim debeBs As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obdebebs"), AggregateFunction.Sum)
        Dim haberBs As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obhaberbs"), AggregateFunction.Sum)
        Dim debeSus As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obdebeus"), AggregateFunction.Sum)
        Dim haberSus As Double = grDetalle.GetTotal(grDetalle.RootTable.Columns("obhaberus"), AggregateFunction.Sum)

        If debeSus <> haberSus Then
            _ok = True
            ToastNotification.Show(Me, "No se puede grabar el comprobante porque esta desbalanceado".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If


        If _ok = True Then

            If debeSus <> haberSus Then
                Dim esDebe As Boolean = True
                Dim diferencia As Double = debeSus - haberSus
                'verifico si la diferencia es para el debe
                If diferencia < 0 Then 'si es negativo la diferencia es para el haber
                    esDebe = False
                    diferencia = diferencia * -1
                End If

                If diferencia <= _difMaximaAjuste Then 'si es verdadero,pregunto sin desea hacer el ajuste automatico
                    Dim info As New TaskDialogInfo("ajuste".ToUpper, eTaskDialogIcon.Help, "¿Desea realizar el ajuste automatico ".ToUpper + " ?", "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.No, eTaskDialogBackgroundColor.Blue)
                    Dim result As eTaskDialogResult = TaskDialog.Show(info)
                    If result = eTaskDialogResult.Yes Then
                        Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
                        'a.obnumi,a.obnumito1,a.oblin,a.obcuenta,b.cacta,b.cadesc,c.cadesc as cadesc2,b.camon,numAux,
                        'obaux1,desc1,obaux2,desc2,obaux3,desc3,a.obobs,a.obobs2,a.obcheque,a.obtc,
                        'a.obdebebs,a.obhaberbs,a.obdebeus, a.obhaberus, estado
                        dt.Rows.Add(0, 0, 0, _numiCuentaAjuste, 6010302001, 0, "Diferencia de Cambio", "Diferencia de Cambio", "SU",
                                    0, 0, 0, 0, 0, 0, "", "DIFERENCIA", 0,
                                    0, 6.96, IIf(esDebe = False, diferencia, 0), IIf(esDebe = True, diferencia, 0),
                                    0, 0, 0, 0, 0, 0, 0)

                    Else
                        _ok = False
                        ToastNotification.Show(Me, "No se puede grabar el comprobante porque esta desbalanceado".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                    End If
                Else
                    _ok = False
                    ToastNotification.Show(Me, "No se puede grabar el comprobante porque esta desbalanceado".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                End If

            End If
        End If

        If _MModificar = True Then
            If tbFecha.Value.Year <> _ultimaFecha.Year Or tbFecha.Value.Month <> _ultimaFecha.Month Then
                _ok = False
                ToastNotification.Show(Me, "la fecha no puede ser en un año y mes distinto al ya registrado".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
            End If
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok

    End Function

    Public Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable
        If CheckBox1.Checked = True Then
            dtBuscador = L_prComprobanteGeneral(gi_empresaNumi, -1, -1)
        Else
            dtBuscador = L_prComprobanteGeneral(gi_empresaNumi, comboanio.Text, comboMeses.Value)
        End If

        Return dtBuscador
    End Function

    Public Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("oanumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("oatip", False))
        listEstCeldas.Add(New Modelos.Celda("oanumdoc1", True, "NRO. DOCUMENTO", 100))
        listEstCeldas.Add(New Modelos.Celda("oanumfac", True, "NRO. FACTURA", 100))
        listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "TIPO", 100))
        listEstCeldas.Add(New Modelos.Celda("oaano", False))
        listEstCeldas.Add(New Modelos.Celda("oames", False))
        listEstCeldas.Add(New Modelos.Celda("oanum", True, "NUMERO", 100))
        listEstCeldas.Add(New Modelos.Celda("oafdoc", True, "FECHA", 100))
        listEstCeldas.Add(New Modelos.Celda("oatc", True, "TIPO DE CAMBIO", 50, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("oaglosa", True, "GLOSA", 200))
        listEstCeldas.Add(New Modelos.Celda("oaobs", True, "OBSERVACION", 200))
        listEstCeldas.Add(New Modelos.Celda("oaemp", False))
        listEstCeldas.Add(New Modelos.Celda("oanom", False))
        listEstCeldas.Add(New Modelos.Celda("oaban", False))
        listEstCeldas.Add(New Modelos.Celda("oache", False))
        listEstCeldas.Add(New Modelos.Celda("oaest", False))
        Return listEstCeldas
    End Function

    Public Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("oanumi").ToString
            tbNroDoc.Text = .GetValue("oanumdoc").ToString
            tbTipo.Value = .GetValue("oatip")
            tbAnio.Text = .GetValue("oaano").ToString
            tbMes.Text = .GetValue("oames").ToString
            tbNum.Text = .GetValue("oanum").ToString
            tbFecha.Value = .GetValue("oafdoc")
            tbTipoCambio.Value = .GetValue("oatc")
            swEstado.Value = .GetValue("oaest")
            tbGlosa.Text = .GetValue("oaglosa").ToString
            tbObs.Text = .GetValue("oaobs").ToString
            tbNombre.Text = .GetValue("oanom").ToString
            tbBanco.Text = .GetValue("oaban").ToString
            tbChque.Text = .GetValue("oache").ToString
            'lbFecha.Text = CType(.GetValue("ybfact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("ybhact").ToString
            'lbUsuario.Text = .GetValue("ybuact").ToString

            'CARGAR DETALLE
            _prCargarGridDetalle(tbNumi.Text)
        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Sub _PMOHabilitarFocus()


    End Sub

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            _modulo.Select()
            _tab.Close()
        End If
    End Sub
#End Region

#Region "EVENTOS DEL MODELO"
    Private Sub JGrM_Buscador_SelectionChanged(sender As Object, e As EventArgs)
        If JGrM_Buscador.Row >= 0 And btnGrabar.Enabled = False Then
            _MPos = JGrM_Buscador.Row
            _PMOMostrarRegistro(_MPos)
        End If
    End Sub

    Private Sub MFlyoutUsuario_PrepareContent(sender As Object, e As EventArgs) Handles MFlyoutUsuario.PrepareContent
        If sender Is BubbleBarUsuario And btnGrabar.Enabled = False Then
            MFlyoutUsuario.BorderColor = Color.FromArgb(&HC0, 0, 0)
            MFlyoutUsuario.Content = PanelUsuario
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbTipo.Focus()
        Dim dtTipoCambio As DataTable = L_prTipoCambioGeneralPorFecha(Now.ToString("yyyy/MM/dd"))
        If dtTipoCambio.Rows.Count = 0 Then
            _existTipoCambio = False
            tbTipoCambio.Value = 0
            tbTipoCambio.BackgroundStyle.BackColor = Color.Red
        Else
            _existTipoCambio = True
            tbTipoCambio.Value = dtTipoCambio.Rows(0).Item("cbdol")
            tbTipoCambio.BackgroundStyle.BackColor = Color.White
            MEP.SetError(tbTipoCambio, "")

            'pongo como default el tipo de cambio
            With grDetalle.RootTable.Columns("obtc")
                .DefaultValue = tbTipoCambio.Value
            End With
        End If



        _PMNuevo()

        'preguntar si esta guardado un recuperar detalle
        Dim dtRecuperar As New DataTable
        dtRecuperar = L_prComprobanteDetalleGeneralRecuperado(gi_userNumi)
        If dtRecuperar.Rows.Count > 0 Then
            Dim info As New TaskDialogInfo("recuperacion de detalle".ToUpper, eTaskDialogIcon.Help, "¿Desea restaurar el ultimo comprobante?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.No, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                _prRecuperar()
            End If
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbTipo.Focus()
        _ultimaFecha = tbFecha.Value
        _PMModificar()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        _PMEliminar()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        _PMGuardar()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PMSalir()
    End Sub

    Private Sub JGrM_Buscador_EditingCell(sender As Object, e As EditingCellEventArgs) Handles JGrM_Buscador.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PMPrimerRegistro()
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        _PMAnteriorRegistro()
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        _PMSiguienteRegistro()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        _PMUltimoRegistro()
    End Sub

    Private Sub F0_Comprobante_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub


#End Region

    Private Sub ELIMINARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARToolStripMenuItem.Click
        _prEliminarFilaDetalle()
    End Sub

    Private Sub tbFecha_ValueChanged(sender As Object, e As EventArgs) Handles tbFecha.ValueChanged
        If btnGrabar.Enabled = False Then
            Exit Sub
        End If


        tbMes.Text = tbFecha.Value.Month
        tbAnio.Text = tbFecha.Value.Year

        If tbTipo.SelectedIndex >= 0 Then
            _prCargarOpcionesDeCuentasAutomaticas()

            If _MNuevo = True Then
                'cargar el numero de comprobante
                Dim dt As DataTable = L_prObtenerNumFacturaGeneral(tbTipo.Value, tbFecha.Value.Year, tbFecha.Value.Month, gi_empresaNumi)
                If dt.Rows.Count > 0 Then
                    tbNum.Text = dt.Rows(0).Item("oanum")
                    tbNroDoc.Text = dt.Rows(0).Item("oanumdoc")
                End If
            End If


        Else
            tbNum.Text = ""
            tbNroDoc.Text = ""
        End If


        'verifico el tipo de cambio de la fecha elegida
        Dim dtTipoCambio As DataTable = L_prTipoCambioGeneralPorFecha(tbFecha.Value.ToString("yyyy/MM/dd"))
        If dtTipoCambio.Rows.Count = 0 Then
            _existTipoCambio = False
            tbTipoCambio.Value = 0
            tbTipoCambio.BackgroundStyle.BackColor = Color.Red
            btnNuevoTipoCambio.Visible = True
        Else
            _existTipoCambio = True
            tbTipoCambio.Value = dtTipoCambio.Rows(0).Item("cbdol")
            tbTipoCambio.BackgroundStyle.BackColor = Color.White
            btnNuevoTipoCambio.Visible = False
            MEP.SetError(tbTipoCambio, "")

            'pongo como default el tipo de cambio
            With grDetalle.RootTable.Columns("obtc")
                .DefaultValue = tbTipoCambio.Value
            End With

            'verifico si es que hay detalle ya insertado
            If grDetalle.RowCount > 1 And btnGrabar.Enabled = True And (_MNuevo Or _MModificar) Then
                Dim info As New TaskDialogInfo("actualizacion".ToUpper, eTaskDialogIcon.Help, "¿Desea actualizar el tipo de cambio de todos los registros del detalle?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.No, eTaskDialogBackgroundColor.Blue)
                Dim result As eTaskDialogResult = TaskDialog.Show(info)
                If result = eTaskDialogResult.Yes Then
                    Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
                    For Each fila As DataRow In dt.Rows
                        fila.Item("obtc") = tbTipoCambio.Value
                    Next
                End If
            End If


        End If

    End Sub

    Private Sub grDetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grDetalle.KeyDown
        Dim f As Integer = grDetalle.Row
        Dim c As Integer = grDetalle.Col
        If e.KeyData = Keys.Control + Keys.Enter And c >= 0 And btnGrabar.Enabled = True Then
            If grDetalle.RootTable.Columns(c).Key = "cacta" Or grDetalle.RootTable.Columns(c).Key = "cadesc" Then

                Dim frmAyuda As Modelos.ModeloAyuda
                Dim dt As DataTable

                dt = L_prCuentaGeneralBasicoParaComprobante(gi_empresaNumi)

                Dim listEstCeldas As New List(Of Modelos.Celda)
                listEstCeldas.Add(New Modelos.Celda("canumi", False))
                listEstCeldas.Add(New Modelos.Celda("cacta", True, "codigo".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("cadesc", True, "cuenta".ToUpper, 200))
                listEstCeldas.Add(New Modelos.Celda("camon", True, "moneda".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("catipo", False))
                listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "tipo".ToUpper, 150))
                listEstCeldas.Add(New Modelos.Celda("cadesc2", False))
                listEstCeldas.Add(New Modelos.Celda("numAux", False))
                listEstCeldas.Add(New Modelos.Celda("catipo", False))

                frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione cuenta".ToUpper, listEstCeldas)
                frmAyuda.grJBuscador.DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
                Dim fc As GridEXFormatCondition
                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 1)
                fc.FormatStyle.BackColor = Color.LightGreen
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 2)
                fc.FormatStyle.BackColor = Color.LightYellow
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 3)
                fc.FormatStyle.BackColor = Color.LightBlue
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 4)
                fc.FormatStyle.BackColor = Color.LightCoral
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 5)
                fc.FormatStyle.BackColor = Color.LightSlateGray
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                fc = New GridEXFormatCondition(frmAyuda.grJBuscador.RootTable.Columns("catipo"), ConditionOperator.Equal, 6)
                fc.FormatStyle.BackColor = Color.LightGreen
                frmAyuda.grJBuscador.RootTable.FormatConditions.Add(fc)

                frmAyuda.ShowDialog()

                If frmAyuda.seleccionado = True Then
                    Dim numiCuenta As String = frmAyuda.filaSelect.Cells("canumi").Value
                    Dim cod As String = frmAyuda.filaSelect.Cells("cacta").Value
                    Dim desc As String = frmAyuda.filaSelect.Cells("cadesc").Value
                    Dim numAux As Integer = frmAyuda.filaSelect.Cells("numAux").Value
                    Dim cuentaPadre As String = frmAyuda.filaSelect.Cells("cadesc2").Value

                    grDetalle.SetValue("obcuenta", numiCuenta)
                    grDetalle.SetValue("cacta", cod)
                    grDetalle.SetValue("cadesc", desc)
                    grDetalle.SetValue("numAux", numAux)
                    grDetalle.SetValue("cadesc2", cuentaPadre)
                    'limpio los auxiliares
                    grDetalle.SetValue("obaux1", 0)
                    grDetalle.SetValue("desc1", "")
                    grDetalle.SetValue("obaux2", 0)
                    grDetalle.SetValue("desc2", "")
                    grDetalle.SetValue("obaux3", 0)
                    grDetalle.SetValue("desc3", "")

                    'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                    'If grDetalle.GetValue("numAux") >= 1 Then
                    '    _prCargarGridAyudaAuxiliar(1)
                    'Else
                    '    ToastNotification.Show(Me, "auxiliar no asociado con cuenta".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                    'End If

                    Dim estado As Integer = grDetalle.GetValue("estado")
                    If estado = 1 Then
                        grDetalle.SetValue("estado", 2)

                    End If
                End If
            End If

            If grDetalle.RootTable.Columns(c).Key = "desc1" Then
                If grDetalle.GetValue("numAux") >= 1 Then
                    Dim frmAyuda As Modelos.ModeloAyuda
                    Dim dt, dtAuxiliaresCuenta As DataTable

                    dtAuxiliaresCuenta = L_prCuentaDetalleGeneral(grDetalle.GetValue("obcuenta"))

                    dt = L_prAuxiliarDetalleGeneral(dtAuxiliaresCuenta.Rows(0).Item("cenumitc3"))
                    Dim descAuxiliar As String = dtAuxiliaresCuenta.Rows(0).Item("ccdesc")

                    Dim listEstCeldas As New List(Of Modelos.Celda)
                    listEstCeldas.Add(New Modelos.Celda("cdnumi", True, "codigo".ToUpper, 80))
                    listEstCeldas.Add(New Modelos.Celda("cdnumitc3", False))
                    listEstCeldas.Add(New Modelos.Celda("cddesc", True, "auxliar".ToUpper, 200))
                    listEstCeldas.Add(New Modelos.Celda("cdest", False))
                    listEstCeldas.Add(New Modelos.Celda("cdest2", False))
                    listEstCeldas.Add(New Modelos.Celda("estado", False))

                    frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione el auxiliar --> ".ToUpper + descAuxiliar.ToUpper, listEstCeldas)
                    frmAyuda.ShowDialog()

                    If frmAyuda.seleccionado = True Then
                        Dim numiAuxiliar As String = frmAyuda.filaSelect.Cells("cdnumi").Value
                        Dim desc As String = frmAyuda.filaSelect.Cells("cddesc").Value

                        grDetalle.SetValue("obaux1", numiAuxiliar)
                        grDetalle.SetValue("desc1", desc)

                        Dim estado As Integer = grDetalle.GetValue("estado")
                        If estado = 1 Then
                            grDetalle.SetValue("estado", 2)

                        End If
                    End If
                Else
                    ToastNotification.Show(Me, "auxiliar no asociado con cuenta".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                End If

            End If

            If grDetalle.RootTable.Columns(c).Key = "desc2" Then
                If grDetalle.GetValue("numAux") >= 2 Then
                    Dim frmAyuda As Modelos.ModeloAyuda
                    Dim dt, dtAuxiliaresCuenta As DataTable

                    dtAuxiliaresCuenta = L_prCuentaDetalleGeneral(grDetalle.GetValue("obcuenta"))

                    dt = L_prAuxiliarDetalleGeneral(dtAuxiliaresCuenta.Rows(1).Item("cenumitc3"))
                    Dim descAuxiliar As String = dtAuxiliaresCuenta.Rows(1).Item("ccdesc")

                    Dim listEstCeldas As New List(Of Modelos.Celda)
                    listEstCeldas.Add(New Modelos.Celda("cdnumi", True, "codigo".ToUpper, 80))
                    listEstCeldas.Add(New Modelos.Celda("cdnumitc3", False))
                    listEstCeldas.Add(New Modelos.Celda("cddesc", True, "auxliar".ToUpper, 200))
                    listEstCeldas.Add(New Modelos.Celda("cdest", False))
                    listEstCeldas.Add(New Modelos.Celda("cdest2", False))
                    listEstCeldas.Add(New Modelos.Celda("estado", False))

                    frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione el auxiliar --> ".ToUpper + descAuxiliar.ToUpper, listEstCeldas)
                    frmAyuda.ShowDialog()

                    If frmAyuda.seleccionado = True Then
                        Dim numiAuxiliar As String = frmAyuda.filaSelect.Cells("cdnumi").Value
                        Dim desc As String = frmAyuda.filaSelect.Cells("cddesc").Value

                        grDetalle.SetValue("obaux2", numiAuxiliar)
                        grDetalle.SetValue("desc2", desc)

                        Dim estado As Integer = grDetalle.GetValue("estado")
                        If estado = 1 Then
                            grDetalle.SetValue("estado", 2)

                        End If
                    End If
                Else
                    ToastNotification.Show(Me, "auxiliar no asociado con cuenta".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                End If

            End If

            If grDetalle.RootTable.Columns(c).Key = "desc3" Then
                If grDetalle.GetValue("numAux") >= 3 Then
                    Dim frmAyuda As Modelos.ModeloAyuda
                    Dim dt, dtAuxiliaresCuenta As DataTable

                    dtAuxiliaresCuenta = L_prCuentaDetalleGeneral(grDetalle.GetValue("obcuenta"))

                    dt = L_prAuxiliarDetalleGeneral(dtAuxiliaresCuenta.Rows(2).Item("cenumitc3"))
                    Dim descAuxiliar As String = dtAuxiliaresCuenta.Rows(2).Item("ccdesc")

                    Dim listEstCeldas As New List(Of Modelos.Celda)
                    listEstCeldas.Add(New Modelos.Celda("cdnumi", True, "codigo".ToUpper, 80))
                    listEstCeldas.Add(New Modelos.Celda("cdnumitc3", False))
                    listEstCeldas.Add(New Modelos.Celda("cddesc", True, "auxliar".ToUpper, 200))
                    listEstCeldas.Add(New Modelos.Celda("cdest", False))
                    listEstCeldas.Add(New Modelos.Celda("cdest2", False))
                    listEstCeldas.Add(New Modelos.Celda("estado", False))

                    frmAyuda = New Modelos.ModeloAyuda(0, 0, dt, "seleccione el auxiliar --> ".ToUpper + descAuxiliar.ToUpper, listEstCeldas)
                    frmAyuda.ShowDialog()

                    If frmAyuda.seleccionado = True Then
                        Dim numiAuxiliar As String = frmAyuda.filaSelect.Cells("cdnumi").Value
                        Dim desc As String = frmAyuda.filaSelect.Cells("cddesc").Value

                        grDetalle.SetValue("obaux3", numiAuxiliar)
                        grDetalle.SetValue("desc3", desc)

                        Dim estado As Integer = grDetalle.GetValue("estado")
                        If estado = 1 Then
                            grDetalle.SetValue("estado", 2)

                        End If
                    End If
                Else
                    ToastNotification.Show(Me, "auxiliar no asociado con cuenta".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                End If
            End If


        End If

        '--------------------------------------------------------------------------------------------
        'EN EL CASO QUE SOLO HALLA APRETADO ENTER EN LA CASILLA DE MONTOS
        If e.KeyData = Keys.Enter And c >= 0 And btnGrabar.Enabled = True Then
            'e.Handled = False
            If grDetalle.RootTable.Columns(c).Key = "obobs" Then
                e.Handled = True

                grDetalle.Col = grDetalle.RootTable.Columns("obcheque").Index

            End If

            If grDetalle.RootTable.Columns(c).Key = "obcheque" Then
                e.Handled = True

                If grDetalle.GetValue("camon") = "BO" Then
                    grDetalle.Col = grDetalle.RootTable.Columns("obdebebs").Index
                Else
                    grDetalle.Col = grDetalle.RootTable.Columns("obdebeus").Index
                End If
            End If

            If grDetalle.RootTable.Columns(c).Key = "obdebebs" Then
                e.Handled = True

                If IsNothing(grDetalle.GetValue("obdebebs")) = False Then
                    If grDetalle.GetValue("obdebebs").ToString <> String.Empty Then
                        Dim conversion As Double = grDetalle.GetValue("obdebebs") / grDetalle.GetValue("obtc")

                        conversion = to3Decimales(conversion)

                        grDetalle.SetValue("obdebeus", Round(conversion, 2))
                    Else
                        grDetalle.SetValue("obdebeus", 0)
                    End If
                Else
                    grDetalle.SetValue("obdebeus", 0)

                End If


                grDetalle.Col = grDetalle.RootTable.Columns("obhaberbs").Index
            End If
            If grDetalle.RootTable.Columns(c).Key = "obdebeus" Then
                e.Handled = True
                If IsNothing(grDetalle.GetValue("obdebeus")) = False Then
                    If grDetalle.GetValue("obdebeus").ToString <> String.Empty Then
                        Dim conversion As Double = grDetalle.GetValue("obdebeus") * grDetalle.GetValue("obtc")
                        conversion = to3Decimales(conversion)
                        grDetalle.SetValue("obdebebs", Round(conversion, 2))
                    Else
                        grDetalle.SetValue("obdebebs", 0)

                    End If
                Else
                    grDetalle.SetValue("obdebebs", 0)

                End If

                grDetalle.Col = grDetalle.RootTable.Columns("obhaberus").Index
            End If
            If grDetalle.RootTable.Columns(c).Key = "obhaberbs" Or grDetalle.RootTable.Columns(c).Key = "obhaberus" Then
                tbFecha.Focus()
                panelAyudaCuenta.Visible = True
                _prIsBalanceado()
                grDetalle.Row = grDetalle.RowCount - 1
                _prCargarGridAyudaCuenta()
            End If

        End If

        'copiar la observacion de arriba de la fila seleccionada
        If e.KeyData = Keys.F2 And c >= 0 And f >= 1 And btnGrabar.Enabled = True Then
            grDetalle.SetValue("obobs", grDetalle.GetRows(grDetalle.Row - 1).Cells("obobs").Value.ToString)
        End If
    End Sub

    Private Sub grDetalle_RecordUpdated(sender As Object, e As EventArgs) Handles grDetalle.RecordUpdated
        _prIsBalanceado()
    End Sub

    Private Sub tbBalanceBs_ValueChanged(sender As Object, e As EventArgs)
        'If btnGrabar.Enabled = True Then
        '    If tbBalanceBs.Value Then
        '        If tbBalanceBs.Tag = True Then ' por aca se supone que quiere balancear automaticamente tbBalanceBs.IsReadOnly = False 
        '            'pregunto si desea balancear automaticamente
        '            ToastNotification.Show(Me, "Configurar primero las cuentas de ajuste".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        '            tbBalanceBs.Tag = False
        '            tbBalanceBs.Value = False
        '            tbBalanceBs.Tag = True
        '        End If
        '        tbBalanceBs.IsReadOnly = True
        '    Else
        '        tbBalanceBs.IsReadOnly = False
        '    End If
        'End If


    End Sub

    Private Sub tbBalanceSus_ValueChanged(sender As Object, e As EventArgs)
        'If btnGrabar.Enabled = True Then
        '    If tbBalanceSus.Value Then
        '        If tbBalanceSus.Tag = True Then ' por aca se supone que quiere balancear automaticamente tbBalanceSus.IsReadOnly = False
        '            'pregunto si desea balancear automaticamente
        '            ToastNotification.Show(Me, "Configurar primero las cuentas de ajuste".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        '            tbBalanceSus.Tag = False
        '            tbBalanceSus.Value = False
        '            tbBalanceSus.Tag = True
        '        End If
        '        tbBalanceSus.IsReadOnly = True
        '    Else
        '        tbBalanceSus.IsReadOnly = False
        '    End If
        'End If
    End Sub

    Private Sub grDetalle_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles grDetalle.UpdatingRecord
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obdebebs" And IsNothing(grDetalle.GetValue("obdebebs")) = False Then 'And grDetalle.GetValue("obdebebs") <> 0
            Dim conversion As Double = grDetalle.GetValue("obdebebs") / grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obdebeus", Round(conversion, 2))
            'grDetalle.SetValue("obhaberus", 0)
            'grDetalle.SetValue("obhaberbs", 0)
        End If
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obhaberbs" And IsNothing(grDetalle.GetValue("obhaberbs")) = False Then 'And grDetalle.GetValue("obhaberbs") <> 0
            Dim conversion As Double = grDetalle.GetValue("obhaberbs") / grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obhaberus", Round(conversion, 2))
            'grDetalle.SetValue("obdebeus", 0)
            'grDetalle.SetValue("obdebebs", 0)
        End If
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obdebeus" And IsNothing(grDetalle.GetValue("obdebeus")) = False Then 'And grDetalle.GetValue("obdebeus") <> 0
            Dim conversion As Double = grDetalle.GetValue("obdebeus") * grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obdebebs", Round(conversion, 2))
            'grDetalle.SetValue("obhaberbs", 0)
            'grDetalle.SetValue("obhaberus", 0)
        End If
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obhaberus" And IsNothing(grDetalle.GetValue("obhaberus")) = False Then 'And grDetalle.GetValue("obhaberus") <> 0
            Dim conversion As Double = grDetalle.GetValue("obhaberus") * grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obhaberbs", Round(conversion, 2))
            'grDetalle.SetValue("obdebebs", 0)
            'grDetalle.SetValue("obdebeus", 0)
        End If

        '_prIsBalanceado()
    End Sub

    Private Sub btnNuevoTipoCambio_Click(sender As Object, e As EventArgs) Handles btnNuevoTipoCambio.Click
        _prAñadirTipoCambio()
    End Sub

    Private Sub grDetalle_SelectionChanged(sender As Object, e As EventArgs) Handles grDetalle.SelectionChanged
        If grDetalle.Row >= 0 And CType(grDetalle.DataSource, DataTable).Rows.Count > 0 Then
            If IsDBNull(grDetalle.GetValue("obobs")) = False And IsNothing(grDetalle.GetValue("obobs")) = False Then
                tbReferencia.Text = grDetalle.GetValue("obobs").ToString
            Else
                tbReferencia.Text = ""
            End If
            If IsDBNull(grDetalle.GetValue("cadesc2")) = False And IsNothing(grDetalle.GetValue("cadesc2")) = False Then
                tbCuentaSuperior.Text = grDetalle.GetValue("cadesc2").ToString
            Else
                tbCuentaSuperior.Text = ""
            End If

        End If

    End Sub

    Private Sub grDetalle_RecordAdded(sender As Object, e As EventArgs) Handles grDetalle.RecordAdded
        'mando a la casilla de nuevo
        'grDetalle.MoveNext()
        'SendKeys.Send("{ENTER}")
        'SendKeys.Send("{ENTER}")
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obdebebs" Then
            Dim conversion As Double = grDetalle.GetValue("obdebebs") / grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obdebeus", Round(conversion, 2))
            'grDetalle.SetValue("obhaberus", 0)
            'grDetalle.SetValue("obhaberbs", 0)
        End If
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obhaberbs" Then
            Dim conversion As Double = grDetalle.GetValue("obhaberbs") / grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obhaberus", Round(conversion, 2))
            'grDetalle.SetValue("obdebeus", 0)
            'grDetalle.SetValue("obdebebs", 0)
        End If
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obdebeus" Then
            Dim conversion As Double = grDetalle.GetValue("obdebeus") * grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obdebebs", Round(conversion, 2))
            'grDetalle.SetValue("obhaberbs", 0)
            'grDetalle.SetValue("obhaberus", 0)
        End If
        If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obhaberus" Then
            Dim conversion As Double = grDetalle.GetValue("obhaberus") * grDetalle.GetValue("obtc")
            conversion = to3Decimales(conversion)
            grDetalle.SetValue("obhaberbs", Round(conversion, 2))
            'grDetalle.SetValue("obdebebs", 0)
            'grDetalle.SetValue("obdebeus", 0)
        End If

        _prIsBalanceado()
    End Sub

    Private Sub grDetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellValueChanged
        'Dim f As Integer = grDetalle.Row
        'Dim c As Integer = grDetalle.Col

        'If grDetalle.RootTable.Columns(c).Key = "cacta" Then
        '    panelAyudaCuenta.Visible = True

        '    Dim codCuenta As String = grDetalle.GetValue("cacta")
        '    grAyudaCuenta.RemoveFilters()
        '    grAyudaCuenta.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grAyudaCuenta.RootTable.Columns("cacta"), Janus.Windows.GridEX.ConditionOperator.BeginsWith, codCuenta))
        'End If

        If True Then
            If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obdebebs" Then 'And grDetalle.GetValue("obdebebs") <> 0
                If IsNumeric(grDetalle.GetValue("obdebebs")) Or grDetalle.GetValue("obdebebs") = "." Then
                    Dim conversion As Double = grDetalle.GetValue("obdebebs") / grDetalle.GetValue("obtc")
                    conversion = to3Decimales(conversion)
                    grDetalle.SetValue("obdebeus", Round(conversion, 2))
                    grDetalle.SetValue("obhaberus", 0)
                    grDetalle.SetValue("obhaberbs", 0)

                End If

            End If
            If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obhaberbs" Then 'And grDetalle.GetValue("obhaberbs") <> 0
                If IsNumeric(grDetalle.GetValue("obhaberbs")) Or grDetalle.GetValue("obhaberbs") = "." Then
                    Dim conversion As Double = grDetalle.GetValue("obhaberbs") / grDetalle.GetValue("obtc")
                    conversion = to3Decimales(conversion)
                    grDetalle.SetValue("obhaberus", Round(conversion, 2))
                    grDetalle.SetValue("obdebeus", 0)
                    grDetalle.SetValue("obdebebs", 0)

                End If
            End If
            If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obdebeus" Then 'And grDetalle.GetValue("obdebeus") <> 0
                If IsNumeric(grDetalle.GetValue("obdebeus")) Or grDetalle.GetValue("obdebeus") = "." Then
                    Dim conversion As Double = grDetalle.GetValue("obdebeus") * grDetalle.GetValue("obtc")
                    conversion = to3Decimales(conversion)
                    grDetalle.SetValue("obdebebs", Round(conversion, 2))
                    grDetalle.SetValue("obhaberbs", 0)
                    grDetalle.SetValue("obhaberus", 0)

                End If


            End If
            If grDetalle.RootTable.Columns(grDetalle.Col).Key = "obhaberus" Then 'And grDetalle.GetValue("obhaberus") <> 0
                If IsNumeric(grDetalle.GetValue("obhaberus")) Or grDetalle.GetValue("obhaberus") = "." Then
                    Dim conversion As Double = grDetalle.GetValue("obhaberus") * grDetalle.GetValue("obtc")
                    conversion = to3Decimales(conversion)
                    grDetalle.SetValue("obhaberbs", Round(conversion, 2))
                    grDetalle.SetValue("obdebebs", 0)
                    grDetalle.SetValue("obdebeus", 0)

                End If

            End If
        End If
    End Sub

    Private Sub grDetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalle.EditingCell
        If tbTipoCambio.Value = 0 Then
            e.Cancel = True
            ToastNotification.Show(Me, "el tipo de cambio debe ser distinto de cero".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
            Return
        End If

        If grDetalle.RootTable.Columns(e.Column.Index).Key = "cactaucg" Then
            panelAyudaCuenta.Visible = True
            _prCargarGridAyudaCuenta()
        End If


    End Sub

    Private Sub grAyudaCuenta_KeyDown(sender As Object, e As KeyEventArgs) Handles grAyudaCuenta.KeyDown




        If e.KeyData = Keys.Control + Keys.A And btnGrabar.Enabled = True And grAyudaCuenta.Tag = -1 And grAyudaCuenta.Row = -2 Then
            'desea agregar a un nuevo cliente

            Dim ci As String = "" 'grAyudaCuenta.GetValue("cjci").ToString
            Dim nombre As String = grAyudaCuenta.GetValue("cjnombre").ToString
            Dim numiNuevo As String = ""
            L_prClientesComprobanteGrabar(numiNuevo, ci, nombre, "1", grDetalle.GetValue("obcuenta"))
            _prCargarGridAyudaClienteCobrar(grDetalle.GetValue("obcuenta"))
            grAyudaCuenta.Tag = -1

            grAyudaCuenta.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grAyudaCuenta.RootTable.Columns("cjnombre"), Janus.Windows.GridEX.ConditionOperator.Equal, nombre))

        End If

        If e.KeyData = Keys.Control + Keys.A And btnGrabar.Enabled = True And grAyudaCuenta.Tag = -2 And grAyudaCuenta.Row = -2 Then
            'desea agregar a un nuevo cliente
            Dim ci As String = "" 'grAyudaCuenta.GetValue("cjci").ToString
            Dim nombre As String = grAyudaCuenta.GetValue("cjnombre").ToString
            Dim numiNuevo As String = ""
            L_prClientesComprobanteGrabar(numiNuevo, ci, nombre, "2", grDetalle.GetValue("obcuenta"))
            _prCargarGridAyudaClientePagar(grDetalle.GetValue("obcuenta"))
            grAyudaCuenta.Tag = -2

            grAyudaCuenta.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grAyudaCuenta.RootTable.Columns("cjnombre"), Janus.Windows.GridEX.ConditionOperator.Equal, nombre))

        End If

        Dim f As Integer = grAyudaCuenta.Row
        Dim c As Integer = grAyudaCuenta.Col
        If e.KeyData = Keys.Enter And c >= 0 And f >= 0 And btnGrabar.Enabled = True Then


            If grAyudaCuenta.Tag = 0 Then 'significa que esta poniendo una cuenta
                If (grAyudaCuenta.GetValue("caniv") <= 4) Then
                    ToastNotification.Show(Me, "Error: La cuenta seleccionada no corresponde a una cuenta operacional".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)

                    Return

                End If
                If True Then 'grAyudaCuenta.GetValue("caniv") = 5
                    Dim numiCuenta As String = grAyudaCuenta.GetValue("canumi")
                    Dim cod As String = grAyudaCuenta.GetValue("cactaucg")
                    Dim desc As String = grAyudaCuenta.GetValue("cadesc")
                    Dim numAux As Integer = grAyudaCuenta.GetValue("numAux")
                    Dim cuentaPadre As String = grAyudaCuenta.GetValue("cadesc2")
                    Dim cuentaMoneda As String = grAyudaCuenta.GetValue("camon")

                    _numiAuxMod = grAyudaCuenta.GetValue("modulo")
                    _numiAuxSuc = grAyudaCuenta.GetValue("sucursal")

                    grDetalle.SetValue("obcuenta", numiCuenta)
                    grDetalle.SetValue("cactaucg", cod)
                    grDetalle.SetValue("cadesc", desc)
                    grDetalle.SetValue("numAux", numAux)
                    grDetalle.SetValue("cadesc2", cuentaPadre)
                    grDetalle.SetValue("camon", cuentaMoneda)
                    If (grDetalle.RowCount = 2) Then
                        'Dim ref As String = grDetalle.GetRow(0).Cells(16).Value.ToString
                        grDetalle.SetValue("obobs", tbGlosa.Text)

                    Else
                        Dim ref As String = grDetalle.GetRow(grDetalle.RowCount - 3).Cells(16).Value.ToString
                        grDetalle.SetValue("obobs", ref)
                    End If

                    'limpio los auxiliares
                    grDetalle.SetValue("obaux1", 0)
                    grDetalle.SetValue("desc1", "")
                    grDetalle.SetValue("obaux2", 0)
                    grDetalle.SetValue("desc2", "")
                    grDetalle.SetValue("obaux3", 0)
                    grDetalle.SetValue("desc3", "")




                    'If grDetalle.Row >= 0 Then
                    '    Dim estado As Integer = grDetalle.GetValue("estado")
                    '    If estado = 1 Then
                    '        'grDetalle.SetValue("estado", 2)
                    '        grDetalle.CurrentRow.Cells("estado").Value = 2
                    '    End If
                    'End If

                    'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                    'If grDetalle.GetValue("numAux") >= 1 Then
                    '    _prCargarGridAyudaAuxiliar(1)
                    'Else
                    '    grDetalle.Focus()
                    '    grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index

                    '    panelAyudaCuenta.Visible = False
                    'End If
                    'Return

                    'verifico si tiene cuenta por cobrar para mandarlo a clientes
                    If grAyudaCuenta.GetValue("isCobrar") = 1 Then
                        _prCargarGridAyudaClienteCobrar(numiCuenta)
                        grAyudaCuenta.Tag = -1
                    Else
                        If grAyudaCuenta.GetValue("isPagar") = 1 Then
                            _prCargarGridAyudaClientePagar(numiCuenta)
                            grAyudaCuenta.Tag = -2
                        Else
                            If grAyudaCuenta.GetValue("isCompra") = 1 Then
                                Dim frm As New F0_ComprobanteCompra
                                frm._detalleCompras = _detalleDetalleCompras
                                frm.ShowDialog()
                                If frm.seleccionado = True Then
                                    grDetalle.SetValue("numiCompra", _detalleDetalleCompras.Rows.Count)
                                    grDetalle.SetValue("obobs", "F:" + frm.tbinrofactura.Text)
                                    'inserto la imagen para que puedan editar el comprobante
                                    _prInsertarImagen()
                                End If


                                'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                                If grDetalle.GetValue("numAux") >= 1 Then
                                    _prCargarGridAyudaAuxiliar(1, _numiAuxMod)
                                Else
                                    grDetalle.Focus()
                                    Dim pos As Integer = grDetalle.RowCount - 2
                                    grDetalle.Row = pos
                                    grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index

                                    panelAyudaCuenta.Visible = False
                                End If
                            Else
                                'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                                If grDetalle.GetValue("numAux") >= 1 Then
                                    _prCargarGridAyudaAuxiliar(1, _numiAuxMod)
                                Else
                                    grDetalle.Focus()
                                    Dim pos As Integer = grDetalle.RowCount - 2
                                    grDetalle.Row = pos
                                    grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index

                                    panelAyudaCuenta.Visible = False
                                End If
                            End If
                        End If
                    End If


                End If


                Return
            End If

            If grAyudaCuenta.Tag = -1 Then 'significa que esta ingresando cliente de cuenta por cobrar
                Dim numiCliente As String = grAyudaCuenta.GetValue("cjnumi").ToString
                Dim desc As String = grAyudaCuenta.GetValue(("cjnombre")).ToString

                grDetalle.SetValue("numiCobrar", numiCliente)
                grDetalle.SetValue("descCobrar", desc)
                grDetalle.SetValue("obobs", desc)

                'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                If grDetalle.GetValue("numAux") >= 1 Then
                    _prCargarGridAyudaAuxiliar(1, _numiAuxMod)
                Else
                    grDetalle.Focus()
                    Dim pos As Integer = grDetalle.RowCount - 2
                    grDetalle.Row = pos
                    grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index

                    panelAyudaCuenta.Visible = False
                End If

                Return
            End If

            If grAyudaCuenta.Tag = -2 Then 'significa que esta ingresando cliente de cuenta por cobrar
                Dim numiCliente As String = grAyudaCuenta.GetValue("cjnumi").ToString
                Dim desc As String = grAyudaCuenta.GetValue(("cjnombre")).ToString

                grDetalle.SetValue("numiCobrar", numiCliente)
                grDetalle.SetValue("descCobrar", desc)
                grDetalle.SetValue("obobs", desc)

                'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                If grDetalle.GetValue("numAux") >= 1 Then
                    _prCargarGridAyudaAuxiliar(1, _numiAuxMod)
                Else
                    grDetalle.Focus()
                    Dim pos As Integer = grDetalle.RowCount - 2
                    grDetalle.Row = pos
                    grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index

                    panelAyudaCuenta.Visible = False
                End If

                Return
            End If

            If grAyudaCuenta.Tag = 1 Then 'significa que esta el auxiliar 1
                Dim numiAuxiliar As String = grAyudaCuenta.GetValue("cdnumi")
                Dim desc As String = grAyudaCuenta.GetValue(("cddesc"))

                grDetalle.SetValue("obaux1", numiAuxiliar)
                grDetalle.SetValue("desc1", desc)

                'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                If grDetalle.GetValue("numAux") >= 2 Then
                    _prCargarGridAyudaAuxiliar(2, _numiAuxSuc)
                Else
                    grDetalle.Focus()
                    Dim pos As Integer = grDetalle.RowCount - 2
                    grDetalle.Row = pos
                    grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index
                    'If grDetalle.GetValue("camon") = "BO" Then
                    '    grDetalle.Col = grDetalle.RootTable.Columns("obdebebs").Index
                    'Else
                    '    grDetalle.Col = grDetalle.RootTable.Columns("obdebeus").Index
                    'End If

                    panelAyudaCuenta.Visible = False
                End If
                Return
            End If

            If grAyudaCuenta.Tag = 2 Then 'significa que esta el auxiliar 1
                Dim numiAuxiliar As String = grAyudaCuenta.GetValue("cdnumi")
                Dim desc As String = grAyudaCuenta.GetValue(("cddesc"))

                grDetalle.SetValue("obaux2", numiAuxiliar)
                grDetalle.SetValue("desc2", desc)

                'verificar si tiene aux1 para mandarlo a buscar el auxiliar 1
                If grDetalle.GetValue("numAux") >= 3 Then
                    _prCargarGridAyudaAuxiliar(3, 0)
                Else
                    grDetalle.Focus()
                    Dim pos As Integer = grDetalle.RowCount - 2
                    grDetalle.Row = pos
                    grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index

                    'If grDetalle.GetValue("camon") = "BO" Then
                    '    grDetalle.Col = grDetalle.RootTable.Columns("obdebebs").Index
                    'Else
                    '    grDetalle.Col = grDetalle.RootTable.Columns("obdebeus").Index
                    'End If
                    panelAyudaCuenta.Visible = False
                End If
                Return
            End If

            If grAyudaCuenta.Tag = 3 Then 'significa que esta el auxiliar 1
                Dim numiAuxiliar As String = grAyudaCuenta.GetValue("cdnumi")
                Dim desc As String = grAyudaCuenta.GetValue(("cddesc"))

                grDetalle.SetValue("obaux3", numiAuxiliar)
                grDetalle.SetValue("desc3", desc)

                grDetalle.Focus()
                Dim pos As Integer = grDetalle.RowCount - 2
                grDetalle.Row = pos
                grDetalle.Col = grDetalle.RootTable.Columns("obobs").Index

                'If grDetalle.GetValue("camon") = "BO" Then
                '    grDetalle.Col = grDetalle.RootTable.Columns("obdebebs").Index
                'Else
                '    grDetalle.Col = grDetalle.RootTable.Columns("obdebeus").Index
                'End If
                panelAyudaCuenta.Visible = False
                Return
            End If
        End If
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        _prImportar()
    End Sub

    Private Sub tbTipo_ValueChanged(sender As Object, e As EventArgs) Handles tbTipo.ValueChanged

        If btnGrabar.Enabled = True Then
            If tbTipo.SelectedIndex = 0 Then
                tbNombre.ReadOnly = False
                tbBanco.ReadOnly = False
            ElseIf tbTipo.SelectedIndex = 1 Then
                tbNombre.ReadOnly = False
                tbBanco.ReadOnly = False
                tbChque.ReadOnly = False

            End If
            If tbTipo.SelectedIndex >= 0 Then
                _prCargarOpcionesDeCuentasAutomaticas()

                'cargar el numero de comprobante
                Dim dt As DataTable = L_prObtenerNumFacturaGeneral(tbTipo.Value, tbFecha.Value.Year, tbFecha.Value.Month, gi_empresaNumi)
                If dt.Rows.Count > 0 Then
                    tbNum.Text = dt.Rows(0).Item("oanum")
                    tbNroDoc.Text = dt.Rows(0).Item("oanumdoc")

                End If


            Else
                tbNum.Text = ""
                tbNroDoc.Text = ""

                Me.cmOpcionesDetalle.Items.Clear()

                Dim subItem1 As ToolStripMenuItem
                subItem1 = New ToolStripMenuItem()
                subItem1.Image = Global.Presentacion.My.Resources.Resources.elim_fila2
                subItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
                subItem1.Name = "subItemEliminar"
                subItem1.Size = New System.Drawing.Size(274, 36)
                subItem1.Text = "ELIMINAR"
                AddHandler subItem1.Click, AddressOf ELIMINAR_Click
                Me.cmOpcionesDetalle.Items.Add(subItem1)

                Dim subItem2 As ToolStripMenuItem
                subItem2 = New ToolStripMenuItem()
                subItem2.Name = "subItemNuevaFila"
                subItem2.Size = New System.Drawing.Size(274, 36)
                subItem2.Text = "INSERTAR FILA"
                AddHandler subItem2.Click, AddressOf INSERTARFILAToolStripMenuItem_Click
                Me.cmOpcionesDetalle.Items.Add(subItem2)
            End If
        End If

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If btnGrabar.Enabled = False Then
            _prImprimir()
        End If
    End Sub

    Private Sub ButtonX4_Click(sender As Object, e As EventArgs) Handles ButtonX4.Click
        Dim Proceso As New Process()
        Proceso.StartInfo.FileName = "calc.exe"
        Proceso.StartInfo.Arguments = ""
        Proceso.Start()
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        Dim frm As New PR_LibroMayor
        frm._modo = 1
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        _prOrdenarDetalle()
    End Sub

    Private Sub F0_Comprobante_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim dtGlobal As DataTable = L_prConfigGeneralEmpresa(gi_empresaNumi)
        If dtGlobal.Rows.Count = 0 Then
            Dim info As New TaskDialogInfo("configuracion".ToUpper, eTaskDialogIcon.Exclamation, "no esta configurado las las cuentas de redondeo, ingrese al programa configuracion primeramente".ToUpper, "".ToUpper, eTaskDialogButton.Ok, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            _modulo.Select()
            _tab.Close()
            Return
        End If
    End Sub



    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If grAyudaCuenta.Row >= 0 Then
            If grAyudaCuenta.Tag = -1 Or grAyudaCuenta.Tag = -2 Then
                Dim nombre As String = InputBox("INGRESE NOMBRE", "CLIENTE", grAyudaCuenta.GetValue("cjnombre")).ToUpper
                If nombre <> "" Then
                    Dim numi As String = grAyudaCuenta.GetValue("cjnumi")
                    L_prClientesComprobanteModificarNombre(numi, nombre)
                    If grAyudaCuenta.Tag = -1 Then
                        _prCargarGridAyudaClienteCobrar(grDetalle.GetValue("obcuenta"))
                        grAyudaCuenta.Tag = -1
                    Else
                        _prCargarGridAyudaClientePagar(grDetalle.GetValue("obcuenta"))
                        grAyudaCuenta.Tag = -2
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub INSERTARFILAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INSERTARFILAToolStripMenuItem.Click
        _prInsertarFila()
    End Sub

    Private Sub grDetalle_Click(sender As Object, e As EventArgs) Handles grDetalle.Click
        If grDetalle.Row >= 0 And 1 = 0 Then
            If grDetalle.RootTable.Columns(grDetalle.Col).Key = "imgCompra" Then

                Dim frm As New F0_ComprobanteCompra
                frm._detalleCompras = _detalleDetalleCompras
                frm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub grDetalle_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles grDetalle.PreviewKeyDown

    End Sub

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited
        Dim estado As Integer = grDetalle.GetValue("estado")
        If estado = 1 Then
            grDetalle.SetValue("estado", 2)

        End If
    End Sub

    Private Sub F0_Comprobante_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
        '_prPonerLine(dtDetalle)
        'dtDetalle = dtDetalle.DefaultView.ToTable(True, "obnumi", "obnumito1", "oblin", "obcuenta", "obaux1", "obaux2", "obaux3", "obobs", "obobs2", "obcheque", "obtc", "obdebebs", "obhaberbs", "obdebeus", "obhaberus", "estado")


        'L_prComprobanteGrabarRespaldo(dtDetalle)
    End Sub

    Private Sub btRecuperar_Click(sender As Object, e As EventArgs) Handles btRecuperar.Click
        _prRecuperar()
    End Sub

    Private Sub timerRecuperacion_Tick(sender As Object, e As EventArgs) Handles timerRecuperacion.Tick
        If btnGrabar.Enabled = True Then
            Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
            '_prPonerLine(dtDetalle)
            dtDetalle = dtDetalle.DefaultView.ToTable(True, "obnumi", "obnumito1", "oblin", "obcuenta", "obaux1", "obaux2", "obaux3", "obobs", "obobs2", "obcheque", "obtc", "obdebebs", "obhaberbs", "obdebeus", "obhaberus", "estado")
            If dtDetalle.Rows.Count > 0 Then
                L_prComprobanteGrabarRespaldo(gi_userNumi, dtDetalle)
            End If
        End If
    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grDetalle.Enter
        grDetalle.Focus()
        grDetalle.Select()
        grDetalle.Col = 2
        grDetalle.Row = 0

    End Sub

    Private Sub tbTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles tbTipo.KeyDown
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab Then
            tbFecha.Focus()
        End If
    End Sub

    Private Sub tbFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles tbFecha.KeyDown
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab Then
            ' tbGlosa.Focus()
            tbBanco.Focus()
        End If
    End Sub

    Private Sub tbGlosa_KeyDown(sender As Object, e As KeyEventArgs) Handles tbGlosa.KeyDown
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab Then
            tbObs.Focus()
        End If
    End Sub

    Private Sub tbObs_KeyDown(sender As Object, e As KeyEventArgs) Handles tbObs.KeyDown
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab Then
            grDetalle.Focus()

            If (grDetalle.RowCount = 1) Then
                panelAyudaCuenta.Visible = True
                _prIsBalanceado()
                grDetalle.Row = grDetalle.RowCount - 1
                _prCargarGridAyudaCuenta()
            End If
        End If
    End Sub

    Private Sub F0_Comprobante_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.I Then
            ButtonX2.Focus()
            ButtonX2.PerformClick()

            Return

        End If
        If btnGrabar.Enabled = True Then
            If e.KeyData = Keys.Alt + Keys.S Then
                btnGrabar.Focus()
                btnGrabar.PerformClick()

                Return

            End If

        End If

        If e.KeyData = Keys.Alt + Keys.C Then
            ButtonX4.Focus()
            ButtonX4.PerformClick()

            Return

        End If
        If e.KeyData = Keys.Alt + Keys.L Then
            ButtonX3.Focus()
            ButtonX3.PerformClick()

            Return

        End If

    End Sub

    Private Sub btnBitacora_Click(sender As Object, e As EventArgs) Handles btnBitacora.Click
        Dim dtb As DataTable
        dtb = L_prBitacoraComprobantes(tbNumi.Text)
        If dtb.Rows.Count > 0 Then
            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("accion", True, "ACCIÓN", 120))
            listEstCeldas.Add(New Modelos.Celda("oafact", True, "FECHA", 80))
            listEstCeldas.Add(New Modelos.Celda("oahact", True, "HORA", 70))
            listEstCeldas.Add(New Modelos.Celda("oauact", True, "USUARIO", 120))
            listEstCeldas.Add(New Modelos.Celda("oanumi", False, "ID", 50))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dtb
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.AutoScrollPosition = AutoScrollPosition
            'ef.alto = 450
            'ef.ancho = 180
            ef.Context = "BITÁCORA DEL COMPROBANTE"
            ef.ShowDialog()
        Else
            ToastNotification.Show(Me, "No existe bitácora para este registro".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If

    End Sub

    Private Sub JGrM_Buscador_DoubleClick(sender As Object, e As EventArgs) Handles JGrM_Buscador.DoubleClick
        SuperTabPrincipal.SelectedTabIndex = 0
        grDetalle2.Focus()
    End Sub

    Private Sub GroupPanel6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupPanel1_Click(sender As Object, e As EventArgs) Handles GroupPanel1.Click

    End Sub

    Private Sub grDetalle2_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grDetalle2.FormattingRow

    End Sub

    Private Sub grAyudaCuenta_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grAyudaCuenta.FormattingRow

    End Sub

    Private Sub tbBanco_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbBanco.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper

    End Sub

    Private Sub tbNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNombre.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub tbChque_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbChque.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub tbBanco_KeyDown(sender As Object, e As KeyEventArgs) Handles tbBanco.KeyDown
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab Then
            tbChque.Focus()
        End If
    End Sub

    Private Sub tbChque_KeyDown(sender As Object, e As KeyEventArgs) Handles tbChque.KeyDown
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab Then
            tbNombre.Focus()
        End If
    End Sub

    Private Sub tbNombre_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNombre.KeyDown
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Tab Then
            tbGlosa.Focus()
        End If
    End Sub

    Private Sub SupTabItemBusqueda_Click(sender As Object, e As EventArgs) Handles SupTabItemBusqueda.Click

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        _PMIniciarTodo()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then

            _PMIniciarTodo()
        Else
            comboanio.SelectedItem = 1
            comboMeses.SelectedItem = 1
        End If
    End Sub
End Class