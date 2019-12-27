Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class F1_Facturas

#Region "Variables locales"
    'Private vlImagen As CImagen = Nothing
    'Private vlRutaBase As String = gs_CarpetaRaiz
    Private Ip As String = ""
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#Region "Variable MArco"
    Dim TableEmpleado As DataTable
    'Public axCZKEM1 As New zkemkeeper.CZKEM
    Private bIsConnected = False
    Private iMachineNumber As Integer
#End Region

#Region "MApas"
    'Dim _Punto As Integer
    'Dim _ListPuntos As List(Of PointLatLng)
    'Dim _Overlay As GMapOverlay
    'Dim _latitud As Double = 0
    'Dim _longitud As Double = 0
#End Region

#End Region



#Region "METODOS PRIVADOS"


    Private Sub _prIniciarTodo()
        Me.Text = "F A C T U R A S"
        _prCargarComboLibreria(tbTipo, gi_LibFactura, gi_LibFACTURATipo)


        _PMIniciarTodo()

        _prAsignarPermisos()

        btnNuevo.Visible = False
        btnEliminar.Visible = False
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaDetalleGeneral(cod1, cod2)

        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cnnum").Width = 70
            .DropDownList.Columns("cnnum").Caption = "COD"

            .DropDownList.Columns.Add("cndesc1").Width = 350
            .DropDownList.Columns("cndesc1").Caption = "DESCRIPCION"

            .ValueMember = "cnnum"
            .DisplayMember = "cndesc1"
            .DataSource = dt
            .Refresh()
        End With

        If dt.Rows.Count > 0 Then
            mCombo.SelectedIndex = 0
        End If
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

#Region "METODOS SOBRECARGADOS"
    Public Overrides Sub _PMOHabilitar()

        TbNit.ReadOnly = False
        TbRazonSocial.ReadOnly = False
        TbNroAutorizacion.ReadOnly = False
        TbCodigoControl.ReadOnly = False

        tbDui.IsInputReadOnly = False
        tbTipo.ReadOnly = False

        TbdMontoFactura.IsInputReadOnly = False
        TbSubTotal.IsInputReadOnly = False
        TbdDescuento.IsInputReadOnly = False

        'Componente TextBox Integer Input
        TbiNroFactura.IsInputReadOnly = False


    End Sub

    Public Overrides Sub _PMOInhabilitar()

        TbNit.ReadOnly = True
        TbRazonSocial.ReadOnly = True
        TbNroAutorizacion.ReadOnly = True
        TbCodigoControl.ReadOnly = True
        tbSujetoCreditoFiscal.IsInputReadOnly = True
        TbSubTotal.IsInputReadOnly = True
        tbTipo.ReadOnly = True


        TbdMontoFactura.IsInputReadOnly = True
        TbdDescuento.IsInputReadOnly = True

        'Componente TextBox Integer Input
        TbiNroFactura.IsInputReadOnly = True

    End Sub

    Public Overrides Sub _PMOLimpiar()

        DtiFechaFactura.Value = Now.Date

        TbNit.Clear()
        TbRazonSocial.Clear()
        TbNroAutorizacion.Clear()
        TbCodigoControl.Clear()

        TbiNroFactura.Value = 0

        tbDui.Text = ""
        tbTipo.SelectedIndex = -1

        TbdMontoFactura.Value = 0
        tbSujetoCreditoFiscal.Value = 0
        TbSubTotal.Value = 0
        TbdDescuento.Value = 0

    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean
        'Dim ffec As String
        'Dim fnit As String
        'Dim frsocial As String
        'Dim fnro As String
        'Dim fautoriz As String
        'Dim fmonto As String
        'Dim fccont As String
        'Dim fmcfiscal As String
        'Dim fdesc As String

        'ffec = DtiFechaFactura.Value.ToString("yyyy/MM/dd")
        'fnit = TbNit.Text
        'frsocial = TbRazonSocial.Text
        'fnro = TbiNroFactura.Value.ToString
        'fautoriz = TbNroAutorizacion.Text
        'fmonto = TbdMontoFactura.Value.ToString
        'fccont = TbCodigoControl.Text
        'fmcfiscal = TbdMontoCreditoFiscal.Value.ToString
        'fdesc = TbdDescuento.Value.ToString

        'Dim res As Boolean = L_prCompraComprobanteGrabar(tbNumi.Text, ffec, fnit, frsocial, fnro, tbDui.Text, fautoriz, fmonto, fccont, fmcfiscal, fdesc, tbTipo.Value)
        'If res Then


        '    ToastNotification.Show(Me, "Codigo de factura ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        'End If
        'Return res
        Return False
    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean

        Dim ffec As String
        Dim fnit As String
        Dim frsocial As String
        Dim fnro As String
        Dim fautoriz As String
        Dim fmonto As String
        Dim fccont As String
        Dim sujetoCreditoFiscal As String
        Dim subTotal As String
        Dim fdesc As String
        Dim importeBaseCreditoFiscal As String
        Dim creditoFiscal As String

        ffec = DtiFechaFactura.Value.ToString("yyyy/MM/dd")
        fnit = TbNit.Text
        frsocial = TbRazonSocial.Text
        fnro = TbiNroFactura.Value.ToString
        fautoriz = TbNroAutorizacion.Text
        fmonto = TbdMontoFactura.Value.ToString
        sujetoCreditoFiscal = tbSujetoCreditoFiscal.Value.ToString
        subTotal = TbSubTotal.Value.ToString
        fdesc = TbdDescuento.Value.ToString
        importeBaseCreditoFiscal = tbImporteBaseCreditoFiscal.Value.ToString
        creditoFiscal = tbCreditoFiscal.Value.ToString
        fccont = TbCodigoControl.Text


        Dim res As Boolean = L_fnCompraComprobanteModificar(tbNumi.Text, ffec, fnit, frsocial, fnro, tbDui.Value, fautoriz, fmonto, sujetoCreditoFiscal, subTotal, fdesc, importeBaseCreditoFiscal, creditoFiscal, fccont, tbTipo.Value)
        If res Then

            ToastNotification.Show(Me, "Codigo de factura ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _PSalirRegistro()

        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prCompraComprobanteBorrar(tbNumi.Text, mensajeError)
            If res Then
                ToastNotification.Show(Me, "Codigo de factura ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()
        Dim sms As String = ""
        If (Not IsDate(DtiFechaFactura.Value)) Then
            If (sms = String.Empty) Then
                sms = "debe elegir una fecha de factura valida.".ToUpper
            Else
                sms = sms + Chr(13) + "debe elegir una fecha factura valida.".ToUpper
            End If
            _ok = False
        End If

        If (TbNit.Text = String.Empty Or TbNit.Text.Equals("0")) Then
            If (sms = String.Empty) Then
                sms = "el nit de la factura no es valido.".ToUpper
            Else
                sms = sms + Chr(13) + "el nit de la factura no es valido.".ToUpper
            End If
            _ok = False

        End If

        If (TbRazonSocial.Text = String.Empty) Then
            If (sms = String.Empty) Then
                sms = "la razon social de la factura no puede quedar vacia.".ToUpper
            Else
                sms = sms + Chr(13) + "la razon social de la factura no puede quedar vacia.".ToUpper
            End If
            _ok = False

        End If

        If (Not TbiNroFactura.Value > 0) Then
            If (sms = String.Empty) Then
                sms = "nro de la factura no valido.".ToUpper
            Else
                sms = sms + Chr(13) + "nro de la factura no valido.".ToUpper
            End If
            _ok = False

        End If

        If (TbNroAutorizacion.Text = String.Empty) Then
            If (sms = String.Empty) Then
                sms = "nro autorización de la factura no valido.".ToUpper
            Else
                sms = sms + Chr(13) + "nro autorización de la factura no valido.".ToUpper
            End If
            _ok = False

        End If

        If (TbCodigoControl.Text = String.Empty) Then
            If (sms = String.Empty) Then
                sms = "el código de control no puede quedar vacio.".ToUpper
            Else
                sms = sms + Chr(13) + "el código de control no puede quedar vacio.".ToUpper
            End If
            _ok = False

        End If

        If (Not TbdMontoFactura.Value > 0) Then
            If (sms = String.Empty) Then
                sms = "debe poner un monto de factura mayor a cero.".ToUpper
            Else
                sms = sms + Chr(13) + "debe poner un monto de factura mayor a cero.".ToUpper
            End If
            _ok = False

        End If

        'If (TbdMontoCreditoFiscal.Value.ToString = String.Empty And TbdMontoCreditoFiscal.Value < 0) Then
        '    TbdMontoCreditoFiscal.Value = 0
        '    _ok = False

        'End If

        If (TbdDescuento.Value.ToString = String.Empty And TbdDescuento.Value < 0) Then
            TbdDescuento.Value = 0
            _ok = False

        End If



        If (Not sms = String.Empty) Then
            ToastNotification.Show(Me, sms.ToUpper,
                       My.Resources.WARNING,
                       4 * 1000,
                       eToastGlowColor.Red,
                       eToastPosition.MiddleCenter)
            Return False
            Exit Function
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        'Dim numiSuc As String = IIf(gb_userTodasSuc = True, "-1", gi_userSuc)
        Dim dtBuscador As DataTable = L_prCompraComprobanteGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("fcanumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("fcafdoc", True, "FECHA", 100))
        listEstCeldas.Add(New Modelos.Celda("fcanit", True, "NIT", 200))
        listEstCeldas.Add(New Modelos.Celda("fcarsocial", True, "RAZON SOCIAL", 200))
        listEstCeldas.Add(New Modelos.Celda("fcanfac", True, "NRO. FACTURA", 200))
        listEstCeldas.Add(New Modelos.Celda("fcandui", True, "DUI", 100))
        listEstCeldas.Add(New Modelos.Celda("fcaautoriz", True, "NRO. AUTORIZACION", 100))
        listEstCeldas.Add(New Modelos.Celda("fcaitc", True, "IMPORTE TOTAL COMPRA", 100, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("fcanscf", False))
        listEstCeldas.Add(New Modelos.Celda("fcasubtotal", True, "SUBTOTAL", 100, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("fcadesc", False, "OBSERVACION", 70))
        listEstCeldas.Add(New Modelos.Celda("fcaibcf", False, "FEC. NACIMIENTO", 80))
        listEstCeldas.Add(New Modelos.Celda("fcacfiscal", False, "FEC. INGRESO", 80))
        listEstCeldas.Add(New Modelos.Celda("fcaccont", True, "CODIGO DE CONTROL", 100))
        listEstCeldas.Add(New Modelos.Celda("fcatcom", False, "FEC. RETIRO", 80))
        listEstCeldas.Add(New Modelos.Celda("fcanumito11", False, "FEC. RETIRO", 80))
        listEstCeldas.Add(New Modelos.Celda("estado", False, "FEC. RETIRO", 80))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("fcanumi").ToString
            DtiFechaFactura.Value = .GetValue("fcafdoc")
            TbNit.Text = .GetValue("fcanit")
            TbRazonSocial.Text = .GetValue("fcarsocial")
            TbiNroFactura.Value = .GetValue("fcanfac")
            tbDui.Value = .GetValue("fcandui")
            TbNroAutorizacion.Text = .GetValue("fcaautoriz")
            TbdMontoFactura.Value = .GetValue("fcaitc")
            tbSujetoCreditoFiscal.Value = .GetValue("fcanscf")
            TbSubTotal.Value = .GetValue("fcasubtotal")
            TbdDescuento.Value = .GetValue("fcadesc")
            tbImporteBaseCreditoFiscal.Value = .GetValue("fcaibcf")
            tbCreditoFiscal.Value = .GetValue("fcacfiscal")

            TbCodigoControl.Text = .GetValue("fcaccont")

            tbTipo.Value = IIf(IsDBNull(.GetValue("fcatcom")), -1, .GetValue("fcatcom"))


        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            '.SetHighlightOnFocus(tbApellido, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbCi, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbDireccion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbEmail, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbNombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbTelef2, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbTelef1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbEstado, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbEmpr, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbReloj, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbEstCivil, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbFIng, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbFNac, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbFRet, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbObs, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbSalario, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbTipo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(tbSuc, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With

    End Sub

    Private Sub P_Instructores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        DtiFechaFactura.Focus()
    End Sub
    Private Sub tbEmail_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.KeyChar = e.KeyChar.ToString.ToLower
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        DtiFechaFactura.Focus()
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub TbdMontoCreditoFiscal_ValueChanged(sender As Object, e As EventArgs) Handles TbSubTotal.ValueChanged
        tbSujetoCreditoFiscal.Value = TbdMontoFactura.Value - TbSubTotal.Value
        tbImporteBaseCreditoFiscal.Value = TbSubTotal.Value - TbdDescuento.Value

    End Sub

    Private Sub tbSujetoCreditoFiscal_ValueChanged(sender As Object, e As EventArgs) Handles tbSujetoCreditoFiscal.ValueChanged
        TbSubTotal.Value = TbdMontoFactura.Value - tbSujetoCreditoFiscal.Value
    End Sub

    Private Sub TbdMontoFactura_ValueChanged(sender As Object, e As EventArgs) Handles TbdMontoFactura.ValueChanged
        TbSubTotal.Value = TbdMontoFactura.Value
        tbSujetoCreditoFiscal.Value = TbdMontoFactura.Value - TbSubTotal.Value
    End Sub

    Private Sub TbdDescuento_ValueChanged(sender As Object, e As EventArgs) Handles TbdDescuento.ValueChanged
        tbImporteBaseCreditoFiscal.Value = TbSubTotal.Value - TbdDescuento.Value
    End Sub

    Private Sub tbImporteBaseCreditoFiscal_ValueChanged(sender As Object, e As EventArgs) Handles tbImporteBaseCreditoFiscal.ValueChanged
        tbCreditoFiscal.Value = tbImporteBaseCreditoFiscal.Value * 0.13
    End Sub
#End Region

End Class