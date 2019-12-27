Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Modelos
Imports DevComponents.DotNetBar.Controls
Imports System.Math
Imports System.IO

Public Class F0_DepreActivoFijo

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

#End Region

#Region "VARIABLES LOCALES"
    Public _MPos As Integer
    Public _MNuevo As Boolean
    Public _MModificar As Boolean

    Public _MListEstBuscador As List(Of Celda)

    Public _MTipoInserccionNuevo As Boolean = False

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
        dtFecha.Enabled = False

        '_PMOLimpiarErrores()

        _PMOInhabilitar()

        _PMOLimpiarErrores()
    End Sub

    Private Sub _PMHabilitar()
        JGrM_Buscador.Enabled = False
        _PMOHabilitar()
        dtFecha.Enabled = True
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
        dtFecha.Enabled = True

        _PMHabilitar()
        _PMOLimpiar()
        '_PMHabilitar()

        MRlAccion.Text = "NUEVO"

        '_PMOLimpiar()

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
            If _PMOGrabarRegistro() = True Then
                'actualizar el grid de buscador
                _PMCargarBuscador()

                If _MTipoInserccionNuevo Then
                    _PMOLimpiar()
                Else
                    _PMSalir()
                End If
            Else
                Exit Sub
            End If

        Else

            _PMOModificarRegistro()

            'actualizar el grid de buscador
            _PMCargarBuscador()

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

        _PMIniciarTodo()

        _prAsignarPermisos()

        SuperTabPrincipal.SelectedTabIndex = 0
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

    'Private Sub _prCargarGridDetalle(numi As String)
    '    Dim dt As New DataTable
    '    dt = L_prDepreDetalle(numi)

    '    grDetalle.DataSource = dt
    '    grDetalle.RetrieveStructure()

    '    If dtFecha.Value = CDate("1/1/0001") Then
    '        tbUfvfin.Text = ""
    '        tbUfvfin.Text = ""
    '    Else
    '        Dim ufvini As DataTable = L_prUfvini(dtFecha.Value.ToString("yyyy/MM/dd"))
    '        Dim ufvfin As DataTable = L_prUfvfin(dtFecha.Value.ToString("yyyy/MM/dd"))
    '        For Each fila As DataRow In ufvini.Rows
    '            tbUfvIni.Text = fila.Item("cbufv")
    '        Next
    '        For Each fila As DataRow In ufvfin.Rows
    '            tbUfvfin.Text = fila.Item("cbufv")
    '        Next
    '    End If


    '    'For Each fila As DataRow In dt.Rows
    '    '    fila.Item("idnumiti3") = fila.Item("idnumiti3")
    '    '    fila.Item("idglosa") = fila.Item("idglosa")
    '    '    fila.Item("idfechau") = fila.Item("idfechau")
    '    '    fila.Item("idvalori") = fila.Item("idvalori")
    '    '    fila.Item("icvidautil") = fila.Item("icvidautil")
    '    '    fila.Item("ihvalorini") = fila.Item("idvalori") '/ fila.Item("cbufv")
    '    '    fila.Item("ihvalorfin") = fila.Item("ihvalorini") '/ (tbUfvfin.Text * tbUfvIni.Text)
    '    '    fila.Item("ihact") = fila.Item("ihvalorfin") - fila.Item("ihvalorini")
    '    '    fila.Item("ihvdepr") = fila.Item("idvalori") / fila.Item("icvidautil")
    '    '    fila.Item("ihdacum") = fila.Item("ihdacum") + fila.Item("ihvdepr")
    '    '    fila.Item("ihactdepr") = fila.Item("ihdacum") ' / (tbUfvIni.Text * tbUfvfin.Text)
    '    '    fila.Item("ihvacumdp") = fila.Item("ihactdepr") - fila.Item("ihdacum")
    '    '    fila.Item("ihvneto") = fila.Item("ihvalorfin") - fila.Item("ihactdepr")
    '    '    tbUfvfin.Text = fila.Item("cbufv").ToString
    '    '    'fila.Item("estado") = 0
    '    'Next

    '    'dar formato a las columnas
    '    With grDetalle.RootTable.Columns("ihnumi")
    '        .Caption = "Codigo al 31/01/2017"
    '        .Width = 50
    '        .Visible = True
    '    End With
    '    With grDetalle.RootTable.Columns("ihtc2numi")
    '        .Caption = "Descripcion </br> al 31/01/2017"
    '        .Width = 100
    '        .Visible = True
    '    End With

    '    With grDetalle.RootTable.Columns("ihti6numi")
    '        .Caption = "Fecha </br> al 31/01/2017"
    '        .Width = 70
    '        .Visible = True
    '    End With
    '    With grDetalle.RootTable.Columns("ihti3numi")
    '        .Caption = "Codigo </br> al 31/01/2017"
    '        .Width = 50
    '        .Visible = True
    '    End With
    '    With grDetalle.RootTable.Columns("ihti4numi")
    '        .Caption = "Codigo"
    '        .Width = 50
    '        .Visible = True
    '    End With
    '    With grDetalle.RootTable.Columns("ihvalorini")
    '        .Caption = "Valor"
    '        .Width = 70
    '        .FormatString = "0.00"
    '        .FormatString = "0.00"
    '        .Visible = True
    '    End With

    '    With grDetalle.RootTable.Columns("ihact")
    '        .Caption = "Vida Util"
    '        .Width = 50
    '        .FormatString = "0.00"
    '        .Visible = True
    '    End With

    '    With grDetalle.RootTable.Columns("ihvalorfin")
    '        .Caption = "Valor al" '+ vbCrLf + dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
    '        .Width = 100
    '        .FormatString = "0.00"
    '        .AllowSort = False
    '        '.EditType = EditType.NoEdit
    '    End With

    '    With grDetalle.RootTable.Columns("ihvdepr")
    '        .Caption = "Valor al" '+ vbCrLf & dtFecha.Value.ToString("dd/MM/yyyy")
    '        .Width = 100
    '        .FormatString = "0.00"
    '        .Visible = True
    '    End With

    '    With grDetalle.RootTable.Columns("ihdacum")
    '        .Caption = "Actualizado al " '+ vbCrLf & dtFecha.Value.ToString("dd/MM/yyyy")
    '        .Width = 100
    '        .FormatString = "0.00"
    '        .Visible = True
    '    End With

    '    With grDetalle.RootTable.Columns("ihactdepr")
    '        .Caption = "Depre al " '+ dtFecha.Value.ToString("dd/MM/yyyy")
    '        .Width = 100
    '        .FormatString = "0.00"
    '        .Visible = True
    '    End With

    '    With grDetalle.RootTable.Columns("ihvacumdp")
    '        .Caption = "Depr. Acum al " '& vbCrLf & dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
    '        .Width = 100
    '        .FormatString = "0.00"
    '        .Visible = True
    '    End With
    '    With grDetalle.RootTable.Columns("ihvneto")
    '        .Caption = "Act. depr. al " '+ dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
    '        .Width = 100
    '        .FormatString = "0.00"
    '        .Visible = True
    '    End With

    '    With grDetalle
    '        .GroupByBoxVisible = False
    '        'diseño de la grilla
    '        .VisualStyle = VisualStyle.Office2007

    '        .DefaultFilterRowComparison = FilterConditionOperator.Contains
    '        .FilterMode = FilterMode.Automatic
    '        .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

    '        .AllowEdit = InheritableBoolean.False
    '        'poner fila de totales
    '        '.TotalRow = InheritableBoolean.True
    '        '.TotalRowFormatStyle.BackColor = Color.Gold
    '        '.TotalRowPosition = TotalRowPosition.BottomFixed

    '        .NewRowPosition = NewRowPosition.BottomRow

    '        'tratando de ocultar las cabeceras
    '        '.ColumnHeaders = InheritableBoolean.False

    '        'poner estilo a la celda seleccionada
    '        .FocusCellFormatStyle.BackColor = Color.Pink
    '    End With
    '    ''Dim fc As GridEXFormatCondition
    '    ''fc = New GridEXFormatCondition(grDetalle.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
    '    ''fc.FormatStyle.BackColor = Color.LightGreen
    '    ''grDetalle.RootTable.FormatConditions.Add(fc)

    '    ''cargar la grilla donde se va a poner la diferencia
    '    ''_prCargarGridDetalle2()

    'End Sub

    Private Sub _prCargarGridDetalle2(numi As String)
        Dim dt As New DataTable
        dt = L_prDepreDetalle2(numi)

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        If dtFecha.Value = CDate("1/1/0001") Then
            tbUfvfin.Text = ""
            tbUfvfin.Text = ""
        Else
            Dim ufvini As DataTable = L_prUfvini(dtFecha.Value.ToString("yyyy/MM/dd"))
            Dim ufvfin As DataTable = L_prUfvfin(dtFecha.Value.ToString("yyyy/MM/dd"))
            For Each fila As DataRow In ufvini.Rows
                tbUfvIni.Text = fila.Item("cbufv")
            Next
            For Each fila As DataRow In ufvfin.Rows
                tbUfvfin.Text = fila.Item("cbufv")
            Next
        End If


        'dar formato a las columnas
        With grDetalle.RootTable.Columns("sucursal")
            .Caption = "Sucursal"
            .Width = 100
            .Visible = True
            .CellStyle.BackColor = Color.LightBlue
        End With

        With grDetalle.RootTable.Columns("sector")
            .Caption = "Sector"
            .Width = 100
            .Visible = True
            .CellStyle.BackColor = Color.LightGreen

        End With

        With grDetalle.RootTable.Columns("ijnumi")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("ijnumiti7")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("ijnumiti4")
            .Caption = "Codigo"
            .Width = 50
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("idglosa")
            .Caption = "Descripcion"
            .Width = 300
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("idfechac")
            .Caption = "Fecha Ingreso"
            .Width = 70
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ijvalant")
            .Caption = "Valor"
            .Width = 70
            .FormatString = "0.00"
            .Visible = True
            .CellStyle.TextAlignment = TextAlignment.Far
        End With

        With grDetalle.RootTable.Columns("ijvalantact")
            .Caption = "Actualizado"
            .Width = 50
            .FormatString = "0.00"
            .CellStyle.TextAlignment = TextAlignment.Far
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ijvalanttot")
            .Caption = "Importe Actual" '+ vbCrLf + dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
            .Width = 100
            .FormatString = "0.00"
            .CellStyle.TextAlignment = TextAlignment.Far
            .AllowSort = False
            '.EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("ijdeprec")
            .Caption = "Depresiacion" '+ vbCrLf & dtFecha.Value.ToString("dd/MM/yyyy")
            .Width = 100
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ijdepacum")
            .Caption = "Depre. Acumulada" '+ vbCrLf & dtFecha.Value.ToString("dd/MM/yyyy")
            .Width = 100
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ijdepacumact")
            .Caption = "Actualizado" '+ dtFecha.Value.ToString("dd/MM/yyyy")
            .Width = 100
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ijvaloracumtot")
            .Caption = "Valor Acumulado" '& vbCrLf & dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
            .Width = 100
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("ijvalorneto")
            .Caption = "Valor Neto" '+ dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
            .Width = 100
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ijvidutil")
            .Caption = "Vida Util" '+ dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
            .Width = 100
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            .AllowEdit = InheritableBoolean.False
            'poner fila de totales
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed

            .NewRowPosition = NewRowPosition.BottomRow

            'tratando de ocultar las cabeceras
            '.ColumnHeaders = InheritableBoolean.False

            'poner estilo a la celda seleccionada
            .FocusCellFormatStyle.BackColor = Color.Pink
            .AllowEdit = InheritableBoolean.False
        End With
        ''Dim fc As GridEXFormatCondition
        ''fc = New GridEXFormatCondition(grDetalle.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        ''fc.FormatStyle.BackColor = Color.LightGreen
        ''grDetalle.RootTable.FormatConditions.Add(fc)

        ''cargar la grilla donde se va a poner la diferencia
        ''_prCargarGridDetalle2()

    End Sub




    Private Sub _prPonerLine(ByRef dt As DataTable)
        _detalleDetalle.Rows.Clear()

        Dim i As Integer = 1
        For Each fila As DataRow In dt.Rows
            If fila.Item("estado") = 0 Or fila.Item("estado") = 1 Or fila.Item("estado") = 2 Then
                fila.Item("oblin") = i
                If fila.Item("numiCobrar") > 0 Then
                    _detalleDetalle.Rows.Add(-1, i, fila.Item("numiCobrar"))
                End If
                If fila.Item("numiCompra") > 0 Then
                    _detalleDetalleCompras.Rows(fila.Item("numiCompra") - 1).Item("fcanumito11") = i
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


    Public Sub _prCargarComrobanteImportado(filaSelect As Janus.Windows.GridEX.GridEXRow)


        With filaSelect
            'tbNumi.Text = .Cells("oanumi").ToString
            'tbNroDoc.Text = .Cells("oanumdoc").ToString
            'tbAnio.Text = .Cells("oaano").ToString
            'tbMes.Text = .Cells("oames").ToString
            'tbNum.Text = .Cells("oanum").ToString
            'tbFecha.Value = .Cells("oafdoc")
            'tbTipoCambio.Value = .Cells("oatc")

            Dim numi As String = .Cells("oanumi").Value.ToString

            'CARGAR DETALLE
            _prCargarGridDetalle2(numi)
        End With


    End Sub

    Private Sub _prImprimir()
        Dim objrep As New R_Comprobante
        Dim dt As New DataTable
        dt = L_prComprobanteReporteComprobante(tbNumi.Text)

        'ahora lo mando al visualizador
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("fechaDesde", "")
        objrep.SetParameterValue("fechaHasta", "")
        objrep.SetParameterValue("titulo", "AUTOMOVIL CLUB BOLIVIANO " + gs_empresaDescSistema.ToUpper)
        objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)
        objrep.SetParameterValue("ultimoRegistro", 0)

        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

    End Sub

    'Private Sub _prCargarOpcionesDeCuentasAutomaticas()

    '    Dim dt As DataTable = New DataTable
    '    Select Case numiTipo
    '        Case 1
    '            dt = L_prCuentasAutomaticaObtenerIngresos()
    '        Case 2
    '            dt = L_prCuentasAutomaticaObtenerEgresos()
    '        Case 3
    '            dt = L_prCuentasAutomaticaObtenerTraspaso()
    '    End Select

    '    Me.cmOpcionesDetalle.Items.Clear()

    '    For Each fila As DataRow In dt.Rows
    '        Dim numi As String = fila.Item("cgnumi")
    '        Dim desc As String = fila.Item("cgdesc")

    '        Dim subItem As ToolStripMenuItem
    '        subItem = New ToolStripMenuItem()

    '        subItem.Image = Global.Presentacion.My.Resources.Resources.add2
    '        subItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
    '        subItem.Name = numi
    '        subItem.Size = New System.Drawing.Size(274, 36)
    '        subItem.Text = desc

    '        AddHandler subItem.Click, AddressOf subItem_Click

    '        'Me.msTiposReforzamiento.Items.AddRange(New ToolStripItem() {Me.ToolStripMenuItem1, Me.VEROBSERVACIONESDECLASESToolStripMenuItem, Me.ASIGNARTODASLASASISTENCIASToolStripMenuItem, Me.FINALIZARCURSOToolStripMenuItem})
    '        Me.cmOpcionesDetalle.Items.Add(subItem)

    '    Next

    '    Dim subItem1 As ToolStripMenuItem
    '    subItem1 = New ToolStripMenuItem()
    '    subItem1.Image = Global.Presentacion.My.Resources.Resources.elim_fila2
    '    subItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
    '    subItem1.Name = "subItemEliminar"
    '    subItem1.Size = New System.Drawing.Size(274, 36)
    '    subItem1.Text = "ELIMINAR"
    '    AddHandler subItem1.Click, AddressOf ELIMINAR_Click
    '    Me.cmOpcionesDetalle.Items.Add(subItem1)

    '    Dim subItem2 As ToolStripMenuItem
    '    subItem2 = New ToolStripMenuItem()
    '    subItem2.Name = "subItemNuevaFila"
    '    subItem2.Size = New System.Drawing.Size(274, 36)
    '    subItem2.Text = "INSERTAR FILA"
    '    AddHandler subItem2.Click, AddressOf INSERTARFILAToolStripMenuItem_Click
    '    Me.cmOpcionesDetalle.Items.Add(subItem2)

    'End Sub

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
        Return res
    End Function

    Private Sub P_GenerarExcel()
        If (P_Exportar(".csv")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN EXITOSA..!!!",
                                       My.Resources.OK1, _DuracionSms * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomLeft)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN..!!!",
                                       My.Resources.WARNING, _DuracionSms * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub
    Private Function P_Exportar(ext As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = gs_CarpetaRaiz
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grDetalle.RowCount
                Dim _columna As Integer = grDetalle.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\DEPRECIACION_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ext
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                Dim ii As Integer = 0
                For ii = 0 To grDetalle.RootTable.Columns.Count - 1
                    If (grDetalle.RootTable.Columns(ii).Visible = True) Then
                        _linea = _linea & grDetalle.RootTable.Columns(ii).Caption & IIf(ext.Equals(".txt"), "|", ";")
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

                Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
                For Each _fil As DataRow In dtDetalle.Rows
                    For ii = 0 To grDetalle.RootTable.Columns.Count - 1
                        If (grDetalle.RootTable.Columns(ii).Visible = True) Then
                            _linea = _linea & CStr(_fil.Item(ii).ToString()) & IIf(ext.Equals(".txt"), "|", ";")
                        End If
                    Next

                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing
                    'Pbx_Precios.Value += 1
                Next
                _escritor.Close()
                'Pbx_Precios.Visible = False
                Try
                    If (MessageBox.Show("DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        Process.Start(_archivo)
                    End If
                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function
#End Region

#Region "METODOS PARA LLENAR"

    Public Sub _PMOHabilitar()
        Dim dt As DataTable = L_prDepreGeneralDesc(gi_empresaNumi)

        If dt.Rows.Count = 0 Then
            tbAnio.Enabled = True
            tbMes.Enabled = True
        End If




        'tbNroDoc.ReadOnly = False
        'tbTipoCambio.IsInputReadOnly = False

        PanelInferior.Visible = False

        If _MNuevo = True Then

        End If

        'tbBalanceBs.IsReadOnly = False
        'tbBalanceSus.IsReadOnly = False
    End Sub

    Public Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbUfvIni.ReadOnly = True
        tbUfvfin.ReadOnly = True


        PanelInferior.Visible = True

        tbAnio.Enabled = False
        tbMes.Enabled = False


        'tbBalanceBs.IsReadOnly = True
        'tbBalanceSus.IsReadOnly = True
    End Sub

    Public Sub _PMOLimpiar()
        'VACIO EL DETALLE
        _prCargarGridDetalle2(-1)

        tbNumi.Text = ""
        dtFecha.Value = CDate("1/1/0001")
        tbUfvIni.Clear()
        tbUfvfin.Clear()

        Dim dt As DataTable = L_prDepreGeneralDesc(gi_empresaNumi)

        If dt.Rows.Count = 0 Then
            tbAnio.Value = Now.Year
            tbMes.Value = Now.Month
        Else
            Dim fec As Date = New Date(dt.Rows(0).Item("iiano"), dt.Rows(0).Item("iimes"), 1)
            fec = DateAdd(DateInterval.Month, 1, fec)
            tbAnio.Value = fec.Year
            tbMes.Value = fec.Month
        End If

        Dim fecha As Date = New Date(tbAnio.Value, tbMes.Value, 1)
        Dim ultimoDia As Integer = ((fecha.AddMonths(1)).AddDays(-1)).Day
        dtFecha.Value = New Date(tbAnio.Value, tbMes.Value, ultimoDia)

        _detalleDetalleCompras.Rows.Clear()
    End Sub

    Public Sub _PMOLimpiarErrores()
        MEP.Clear()
        dtFecha.BackColor = Color.White
    End Sub

    Public Function _PMOGrabarRegistro() As Boolean
        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)

        'dtDetalle = dtDetalle.DefaultView.ToTable(True, "ihnumi", "cbnumi", "idnumi", "idnumiti3", "ignumi", "ihvalorini", "ihact", "ihvalorfin", "ihvdepr", "ihdacum", "ihactdepr",
        '                                           "ihvacumdp", "ihvneto", "icvidautil", "estado")
        dtDetalle = dtDetalle.DefaultView.ToTable(True, "ihnumi", "ignumi", "idnumi", "ihvalorini", "ihact", "ihvalorfin", "ihvdepr", "ihdacum", "ihactdepr",
                                                   "ihvacumdp", "ihvneto", "icvidautil", "estado")
        dtDetalle.Columns("ihnumi").ColumnName = "ijnumi"
        dtDetalle.Columns("ignumi").ColumnName = "ijnumiti7"
        dtDetalle.Columns("idnumi").ColumnName = "ijnumiti4"
        dtDetalle.Columns("ihvalorini").ColumnName = "ijvalant"
        dtDetalle.Columns("ihact").ColumnName = "ijvalantact"
        dtDetalle.Columns("ihvalorfin").ColumnName = "ijvalanttot"
        dtDetalle.Columns("ihvdepr").ColumnName = "ijdeprec"
        dtDetalle.Columns("ihdacum").ColumnName = "ijdepacum"
        dtDetalle.Columns("ihactdepr").ColumnName = "ijdepacumact"
        dtDetalle.Columns("ihvacumdp").ColumnName = "ijvaloracumtot"
        dtDetalle.Columns("ihvneto").ColumnName = "ijvalorneto"
        dtDetalle.Columns("icvidautil").ColumnName = "ijvidutil"

        Dim res As Boolean = L_prDepreGrabar2(tbMes.Value, tbAnio.Value, gi_empresaNumi, dtDetalle)
        If res Then

            ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Function _PMOModificarRegistro() As Boolean

        'Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
        'Dim res As Boolean = L_prDepreModificar(tbNumi.Text, dtFecha.Value.ToString("yyyy/MM/dd"), dtDetalle)
        'If res Then
        '    ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        '    '_PSalirRegistro()
        'End If
        'Return res
    End Function

    Public Sub _PMOEliminarRegistro()
        Dim dt As DataTable = L_prDepreGeneralDesc(gi_empresaNumi)
        Dim ultimoNumi As String = dt.Rows(0).Item("iinumi")

        If ultimoNumi = tbNumi.Text Then
            Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_prDepreELiminar2(tbNumi.Text, mensajeError)
                If res Then
                    ToastNotification.Show(Me, "Codigo ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PMFiltrar()
                Else
                    ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If
            End If
        Else
            Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "Solo puede eliminar la ultima depreciacion".ToUpper, "".ToUpper, eTaskDialogButton.Yes, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
        End If

    End Sub
    Public Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If dtFecha.Value = CDate("1/1/0001") Then
            dtFecha.BackgroundStyle.BackColor = Color.Red
            MEP.SetError(dtFecha, "no existe una fecha seleccionada!".ToUpper)
            _ok = False
        Else
            dtFecha.BackgroundStyle.BackColor = Color.White
            MEP.SetError(dtFecha, "")
        End If
        'Dim fechafin As DateTime = Now.AddMonths(1).AddDays(-1)
        'If dtFecha.Value <> fechafin Then
        '    dtFecha.BackColor = Color.Red
        '    MEP.SetError(dtFecha, "Seleccione el ultimo dia del mes!".ToUpper)
        '    _ok = False
        'Else
        '    dtFecha.BackColor = Color.White
        '    MEP.SetError(dtFecha, "")
        'End If

        Return _ok
    End Function

    Public Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prDepreGeneral2(gi_empresaNumi)
        Return dtBuscador

    End Function

    Public Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        'listEstCeldas.Add(New Modelos.Celda("oanumi", True, "ID", 70))
        'listEstCeldas.Add(New Modelos.Celda("oatip", False))
        'listEstCeldas.Add(New Modelos.Celda("oanumdoc", True, "NRO. DOCUMENTO", 150))
        'listEstCeldas.Add(New Modelos.Celda("cndesc1", True, "TIPO", 100))
        'listEstCeldas.Add(New Modelos.Celda("oaano", False))
        'listEstCeldas.Add(New Modelos.Celda("oames", False))
        'listEstCeldas.Add(New Modelos.Celda("oanum", True, "NUMERO", 100))
        'listEstCeldas.Add(New Modelos.Celda("oafdoc", True, "FECHA", 100))
        'listEstCeldas.Add(New Modelos.Celda("oatc", True, "TIPO DE CAMBIO", 120, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("oaglosa", True, "GLOSA", 200))
        'listEstCeldas.Add(New Modelos.Celda("oaobs", True, "OBSERVACION", 200))
        'listEstCeldas.Add(New Modelos.Celda("oaemp", False))

        Return listEstCeldas
    End Function

    Public Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            'tbNumi.Text = .GetValue("ignumi").ToString
            'dtFecha.Value = .GetValue("igfecha")
            tbNumi.Text = .GetValue("iinumi").ToString
            tbMes.Value = .GetValue("iimes")
            tbAnio.Value = .GetValue("iiano")

            'CARGAR DETALLE
            _prCargarGridDetalle2(tbNumi.Text)
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
        _PMNuevo()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
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

    Private Sub tbFecha_ValueChanged(sender As Object, e As EventArgs)
        If btnGrabar.Enabled = False Then
            Exit Sub
        End If


        'verifico el tipo de cambio de la fecha elegida
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

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs)
        _prImportar()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If btnGrabar.Enabled = False Then
            ''_prImprimir()
            P_GenerarExcel()
        End If
    End Sub

    Private Sub ButtonX4_Click(sender As Object, e As EventArgs)
        Dim Proceso As New Process()
        Proceso.StartInfo.FileName = "calc.exe"
        Proceso.StartInfo.Arguments = ""
        Proceso.Start()
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs)
        Dim frm As New PR_LibroMayor
        frm._modo = 1
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.ShowDialog()
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
    Private Sub F0_Comprobante_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable)
        '_prPonerLine(dtDetalle)
        'dtDetalle = dtDetalle.DefaultView.ToTable(True, "obnumi", "obnumito1", "oblin", "obcuenta", "obaux1", "obaux2", "obaux3", "obobs", "obobs2", "obcheque", "obtc", "obdebebs", "obhaberbs", "obdebeus", "obhaberus", "estado")


        'L_prComprobanteGrabarRespaldo(dtDetalle)
    End Sub
    Private Sub btGenerar_Click(sender As Object, e As EventArgs) Handles btGenerar.Click
        If dtFecha.Value <> CDate("1/1/0001") Then
            Dim ufvfin As DataTable = L_prUfvfin(dtFecha.Value.ToString("yyyy/MM/dd"))
            Dim ufvini As DataTable = L_prUfvini(dtFecha.Value.ToString("yyyy/MM/dd"))
            Dim valorufvini As Double
            Dim valorufvfin As Double
            For Each row As DataRow In ufvfin.Rows
                valorufvfin = row.Item("cbufv")
                tbUfvfin.Text = row.Item("cbufv").ToString
            Next
            For Each row As DataRow In ufvini.Rows
                valorufvini = row.Item("cbufv")
                tbUfvIni.Text = row.Item("cbufv").ToString
            Next
            If valorufvfin = 0.00 Or valorufvini = 0.00 Then
                Dim info As New TaskDialogInfo("valor de uvf incorrecto".ToUpper, eTaskDialogIcon.Delete, "debe ingresar valor de ufv".ToUpper, "".ToUpper, eTaskDialogButton.Yes, eTaskDialogBackgroundColor.Blue)
                Dim result As eTaskDialogResult = TaskDialog.Show(info)
            Else
                CalcularDepreciacion()
            End If
        Else
            Dim info As New TaskDialogInfo("Error de datos".ToUpper, eTaskDialogIcon.Delete, "debe ingresar una fecha".ToUpper, "".ToUpper, eTaskDialogButton.Yes, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
        End If

    End Sub

    Private Sub CalcularDepreciacion()
        Dim dt As New DataTable
        dt = L_prDepreDetalleCalcularDepreciacion(gi_empresaNumi)
        For Each fila As DataRow In dt.Rows
            fila.Item("idnumiti3") = fila.Item("idnumiti3")
            fila.Item("idglosa") = fila.Item("idglosa")
            fila.Item("idfechau") = fila.Item("idfechau")
            fila.Item("idvalori") = fila.Item("idvalori")
            fila.Item("icvidautil") = fila.Item("icvidautil")
            fila.Item("ihvalorini") = IIf(fila.Item("ihvalorfin") = 0, fila.Item("idvalori"), fila.Item("ihvalorfin"))
            fila.Item("ihvalorfin") = (fila.Item("ihvalorini") / tbUfvIni.Text) * tbUfvfin.Text

            'codigo q verifica el tipo de activo--------------------------------------------------
            Dim tipoActivo As Integer = fila.Item("idnumiti3")
            If tipoActivo = 5 Then 'es activo terreno
                fila.Item("ihvdepr") = 0
                fila.Item("ihdacum") = 0
                fila.Item("ihactdepr") = 0
                fila.Item("ihvacumdp") = 0
            Else 'no es activo terreno

                fila.Item("ihvdepr") = fila.Item("ihvalorfin") / (fila.Item("icvidautil") * 12)

                fila.Item("ihdacum") = fila.Item("ihdacum") ' + fila.Item("ihvdepr")
                fila.Item("ihactdepr") = (fila.Item("ihdacum") / tbUfvIni.Text) * tbUfvfin.Text
                fila.Item("ihvacumdp") = fila.Item("ihactdepr") - fila.Item("ihdacum")
            End If
            fila.Item("ihact") = fila.Item("ihvalorfin") - fila.Item("ihvalorini")

            'fila.Item("ihvdepr") = fila.Item("idvalori") / (fila.Item("icvidautil") * 12)

            fila.Item("ihvneto") = fila.Item("ihvalorfin") - fila.Item("ihactdepr")

            'codigo q verifica el tipo de activo--------------------------------------------------
            '-------------------------------------------------------------------------------------

            'fila.Item("estado") = 0
            grDetalle.DataSource = dt
            grDetalle.RetrieveStructure()
        Next

        With grDetalle.RootTable.Columns("sucursal")
            .Caption = "Sucursal"
            .Width = 100
            .Visible = True
            .CellStyle.BackColor = Color.LightBlue
        End With

        With grDetalle.RootTable.Columns("sector")
            .Caption = "Sector"
            .Width = 100
            .Visible = True
            .CellStyle.BackColor = Color.LightGreen

        End With

        With grDetalle.RootTable.Columns("ihnumi")
            .Visible = False
        End With
        'With grDetalle.RootTable.Columns("cbnumi")
        '    .Visible = False
        'End With
        With grDetalle.RootTable.Columns("ignumi")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("idnumi")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("idnumiti3")
            .Caption = "Codigo"
            .Width = 50
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("idglosa")
            .Caption = "Descripcion"
            .Width = 150
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("idfechau")
            .Caption = "Fecha Ingreso"
            .Width = 120
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("idvalori")
            .Caption = "Valor"
            .Width = 70
            .FormatString = "0.00"
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("icvidautil")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("icvidautil2")
            .Caption = "Vida Util"
            .Width = 70
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ihvalorini")
            .Caption = "Valor al <br>  31/01/2017"
            .Width = 150
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AllowSort = False
            '.EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("ihvalorfin")
            .Caption = "Valor al </br> 31/01/2017"
            .Width = 150
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ihact")
            .Caption = "Actualizado al " + dtFecha.Value.ToString("dd/MM/yyyy")
            .Width = 150
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ihvdepr")
            .Caption = "Depre al " + dtFecha.Value.ToString("dd/MM/yyyy")
            .Width = 150
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ihdacum")
            .Caption = "Depr. Acum al " + dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
            .Width = 150
            .FormatString = "0.00"
            .CellStyle.TextAlignment = TextAlignment.Far
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("ihactdepr")
            .Caption = "Act. depr. al " + dtFecha.Value.AddMonths(-1).ToString("dd/MM/yyyy")
            .Width = 150
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With

        With grDetalle.RootTable.Columns("ihvneto")
            .Caption = "Valor Neto al " + vbCrLf + dtFecha.Value.ToString("dd/MM/yyyy")
            .Width = 150
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("ihvacumdp")
            .Caption = "Valor Acum. depr. al " + dtFecha.Value.ToString("dd/MM/yyyy")
            .Width = 150
            .CellStyle.TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True

        End With

        With grDetalle.RootTable.Columns("estado")
            .Visible = False
        End With
        'With grDetalle.RootTable.Columns("depre")
        '    .Visible = False
        'End With
    End Sub

    Private Sub dtFecha_Click(sender As Object, e As EventArgs) Handles dtFecha.Click

    End Sub

    Private Sub tbGestion_ValueChanged(sender As Object, e As EventArgs) Handles tbAnio.ValueChanged
        If btnGrabar.Enabled = True Then
            Dim fecha As Date = New Date(tbAnio.Value, tbMes.Value, 1)
            Dim ultimoDia As Integer = ((fecha.AddMonths(1)).AddDays(-1)).Day
            dtFecha.Value = New Date(tbAnio.Value, tbMes.Value, ultimoDia)
        End If
    End Sub

    Private Sub tbMes_ValueChanged(sender As Object, e As EventArgs) Handles tbMes.ValueChanged
        If btnGrabar.Enabled = True Then
            Dim fecha As Date = New Date(tbAnio.Value, tbMes.Value, 1)
            Dim ultimoDia As Integer = ((fecha.AddMonths(1)).AddDays(-1)).Day
            dtFecha.Value = New Date(tbAnio.Value, tbMes.Value, ultimoDia)
        End If
    End Sub
End Class