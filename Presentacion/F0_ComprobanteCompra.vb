Imports System.ComponentModel
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Public Class F0_ComprobanteCompra
    Public filaSelect As Janus.Windows.GridEX.GridEXRow
    Public seleccionado As Boolean = False
    Public _detalleCompras As DataTable

#Region "Metodos privados"
    'Private Sub _PMCargarBuscador()
    '    Dim dtBuscador As DataTable
    '    If tbFiltrarFecha.Value = True Then
    '        dtBuscador = L_prComprobanteGeneral2(gi_empresaNumi, tbFecha.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"))

    '    Else
    '        dtBuscador = L_prComprobanteGeneral(gi_empresaNumi)
    '    End If

    '    Dim _MListEstBuscador As New List(Of Modelos.Celda)
    '    _MListEstBuscador.Add(New Modelos.Celda("oanumi", True, "ID", 50))
    '    _MListEstBuscador.Add(New Modelos.Celda("oatip", False))
    '    _MListEstBuscador.Add(New Modelos.Celda("oanumdoc", True, "NRO. DOCUMENTO", 100))
    '    _MListEstBuscador.Add(New Modelos.Celda("cndesc1", True, "TIPO", 80))
    '    _MListEstBuscador.Add(New Modelos.Celda("oaano", False))
    '    _MListEstBuscador.Add(New Modelos.Celda("oames", False))
    '    _MListEstBuscador.Add(New Modelos.Celda("oanum", True, "NUMERO", 80))
    '    _MListEstBuscador.Add(New Modelos.Celda("oafdoc", True, "FECHA", 80))
    '    _MListEstBuscador.Add(New Modelos.Celda("oatc", True, "TIPO DE CAMBIO", 120, "0.00"))
    '    _MListEstBuscador.Add(New Modelos.Celda("oaglosa", True, "GLOSA", 400))
    '    _MListEstBuscador.Add(New Modelos.Celda("oaobs", True, "OBSERVACION", 200))
    '    _MListEstBuscador.Add(New Modelos.Celda("oaemp", False))

    '    grDetalle.DataSource = dtBuscador
    '    grDetalle.RetrieveStructure()

    '    For i = 0 To _MListEstBuscador.Count - 1
    '        Dim campo As String = _MListEstBuscador.Item(i).campo
    '        With grDetalle.RootTable.Columns(campo)
    '            If _MListEstBuscador.Item(i).visible = True Then
    '                .Caption = _MListEstBuscador.Item(i).titulo
    '                .Width = _MListEstBuscador.Item(i).tamano
    '                .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '                Dim col As DataColumn = dtBuscador.Columns(campo)
    '                Dim tipo As Type = col.DataType
    '                If tipo.ToString = "System.Int32" Or tipo.ToString = "System.Decimal" Or tipo.ToString = "System.Double" Then
    '                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '                End If
    '                If _MListEstBuscador.Item(i).formato <> String.Empty Then
    '                    .FormatString = _MListEstBuscador.Item(i).formato
    '                End If
    '            Else
    '                .Visible = False
    '            End If
    '        End With
    '    Next

    '    'Habilitar Filtradores
    '    With grDetalle
    '        .DefaultFilterRowComparison = FilterConditionOperator.Contains
    '        .FilterMode = FilterMode.Automatic
    '        .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
    '        .GroupByBoxVisible = False
    '        'diseño de la grilla
    '        .VisualStyle = VisualStyle.Office2007
    '    End With

    '    'metodo para hacer la actualizacion de algo cuando cambia el datasource del buscador
    '    '_PMOLuegoDeCargarBuscador()

    'End Sub
    Public Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()
        Dim sms As String = ""


        If (Not IsDate(DtiFechaFactura.Value)) Then
            If (sms = String.Empty) Then
                sms = "debe elegir una fecha de factura valida.".ToUpper
            Else
                sms = sms + Chr(13) + "debe elegir una fecha factura valida.".ToUpper
            End If
        End If

        If (TbNit.Text = String.Empty Or TbNit.Text.Equals("0")) Then
            If (sms = String.Empty) Then
                sms = "el nit de la factura no es valido.".ToUpper
            Else
                sms = sms + Chr(13) + "el nit de la factura no es valido.".ToUpper
            End If
        End If

        If (TbRazonSocial.Text = String.Empty) Then
            If (sms = String.Empty) Then
                sms = "la razon social de la factura no puede quedar vacia.".ToUpper
            Else
                sms = sms + Chr(13) + "la razon social de la factura no puede quedar vacia.".ToUpper
            End If
        End If

        If (Not tbinrofactura.Text <> "") Then
            If (sms = String.Empty) Then
                sms = "nro de la factura no valido.".ToUpper
            Else
                sms = sms + Chr(13) + "nro de la factura no valido.".ToUpper
            End If
        End If

        If (TbNroAutorizacion.Text = String.Empty) Then
            If (sms = String.Empty) Then
                sms = "nro autorización de la factura no valido.".ToUpper
            Else
                sms = sms + Chr(13) + "nro autorización de la factura no valido.".ToUpper
            End If
        End If

        If (TbCodigoControl.Text = String.Empty) Then
            If (sms = String.Empty) Then
                sms = "el código de control no puede quedar vacio.".ToUpper
            Else
                sms = sms + Chr(13) + "el código de control no puede quedar vacio.".ToUpper
            End If
        End If

        If (Not TbdMontoFactura.Value > 0) Then
            If (sms = String.Empty) Then
                sms = "debe poner un monto de factura mayor a cero.".ToUpper
            Else
                sms = sms + Chr(13) + "debe poner un monto de factura mayor a cero.".ToUpper
            End If
        End If

        If (TbSubTotal.Value.ToString = String.Empty And TbSubTotal.Value < 0) Then
            TbSubTotal.Value = 0
        End If

        If (TbdDescuento.Value.ToString = String.Empty And TbdDescuento.Value < 0) Then
            TbdDescuento.Value = 0
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

        Return True

        Return _ok
    End Function
    Private Sub _PMGuardar()

        If (Not TbNit.Text.Trim.Equals("0")) Then
            L_Grabar_Nit(TbNit.Text.Trim, TbRazonSocial.Text.Trim, "")
        Else
            L_Grabar_Nit(TbNit.Text, "S/N", "")
        End If

        If _PMOValidarCampos() = False Then
            Exit Sub
        End If

        If _PMOGrabarRegistro() = True Then

            Close()
            Exit Sub
        End If


    End Sub

    Public Function _PMOGrabarRegistro() As Boolean
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
        fnro = tbinrofactura.Text
        fautoriz = TbNroAutorizacion.Text
        fmonto = TbdMontoFactura.Value.ToString
        sujetoCreditoFiscal = tbSujetoCreditoFiscal.Value.ToString
        subTotal = TbSubTotal.Value.ToString
        fdesc = TbdDescuento.Value.ToString
        importeBaseCreditoFiscal = tbImporteBaseCreditoFiscal.Value.ToString
        creditoFiscal = tbCreditoFiscal.Value.ToString
        fccont = TbCodigoControl.Text

        Dim numi As String = ""

        _detalleCompras.Rows.Add(1, ffec, fnit, frsocial, fnro, tbDui.Value, fautoriz, fmonto, sujetoCreditoFiscal, subTotal, fdesc, importeBaseCreditoFiscal, creditoFiscal, fccont, tbTipo.Value, 0, 0)

        seleccionado = True

        Close()

        'Dim res As Boolean = L_prCompraComprobanteGrabar(numi, ffec, fnit, frsocial, fnro, fautoriz, fmonto, fccont, fmcfiscal, fdesc)
        'If res Then
        '    Close()
        'End If
        'Return res


    End Function

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
#End Region

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        _PMGuardar()
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        seleccionado = False
        If (Not TbNit.Text.Trim.Equals("0")) Then
            L_Grabar_Nit(TbNit.Text.Trim, TbRazonSocial.Text.Trim, "")
        Else
            L_Grabar_Nit(TbNit.Text, "S/N", "")
        End If
        Me.Close()
    End Sub


    Private Sub ModeloHor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            P_Moverenfoque()
        End If
    End Sub

    Private Sub P_Moverenfoque()
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub F0_ComprobanteCompra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prCargarComboLibreria(tbTipo, gi_LibFactura, gi_LibFACTURATipo)

        DtiFechaFactura.Value = Now.Date
        TbNit.Focus()
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

    Private Sub PanelEx1_Click(sender As Object, e As EventArgs) Handles PanelEx1.Click

    End Sub

    Private Sub TbNit_TextChanged(sender As Object, e As EventArgs) Handles TbNit.TextChanged

    End Sub

    Private Sub TbNit_KeyDown(sender As Object, e As KeyEventArgs) Handles TbNit.KeyDown

    End Sub

    Private Sub TbNit_Validating(sender As Object, e As CancelEventArgs) Handles TbNit.Validating
        Dim nom1, nom2 As String
        nom1 = ""
        nom2 = ""
        If (TbNit.Text.Trim = String.Empty) Then
            TbNit.Text = "0"
            TbRazonSocial.Text = "S/N"
        End If
        If (L_Validar_Nit(TbNit.Text.Trim, nom1, nom2)) Then
            TbRazonSocial.Text = nom1

        End If
    End Sub
End Class