
Imports Janus.Windows.GridEX

Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.SuperGrid
Imports Logica.AccesoLogica
Public Class F0_SolicitarDatosFactura

    Public nameFactura As String
    Public NroControl As String
    Public NroAutorizacion As String
    Public sucursal As Integer
    Public rangoInicial As Integer
    Public rangoFinal As Integer
    Public Bandera As Boolean = False
    Public FechaI As String
    Public FechaF As String


    Public Sub _prIniciar()
        tbnrofactura.Clear()
        tbnroautorizacion.Clear()
        tbcodigocontrol.Clear()
        tbnrofactura.MaxLength = 10
        tbnroautorizacion.MaxLength = 25
        tbcodigocontrol.MaxLength = 20
        tbnrofactura.CharacterCasing = CharacterCasing.Upper
        tbnroautorizacion.CharacterCasing = CharacterCasing.Upper
        tbcodigocontrol.CharacterCasing = CharacterCasing.Upper
        _prHabilitarFocus()

        tbnrofactura.Focus()
        Dim dt As DataTable = L_fnObtenerMaximoNroFacturaManual(sucursal, FechaI, FechaF)
        If (dt.Rows.Count > 0) Then
            tbnrofactura.Text = dt.Rows(0).Item("nro")
        End If
    End Sub

    Public Function _prValidar() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbnrofactura.Text = String.Empty Then
            tbnrofactura.BackColor = Color.Red
            MEP.SetError(tbnrofactura, "Ingrese numero de factura valido!".ToUpper)
            _ok = False
            Return _ok
        Else
            tbnrofactura.BackColor = Color.White
            MEP.SetError(tbnrofactura, "")
        End If
        ''If tbnroautorizacion.Text = String.Empty Then
        ''    tbnroautorizacion.BackColor = Color.Red
        ''    MEP.SetError(tbnroautorizacion, "Ingrese numero de autorizacion valido!".ToUpper)
        ''    _ok = False
        ''    Return _ok
        ''Else
        ''    tbnroautorizacion.BackColor = Color.White
        ''    MEP.SetError(tbnroautorizacion, "")
        ''End If
        ''If tbcodigocontrol.Text = String.Empty Then
        ''    tbcodigocontrol.BackColor = Color.Red
        ''    MEP.SetError(tbcodigocontrol, "Ingrese un codigo de control valido!".ToUpper)
        ''    _ok = False
        ''    Return _ok
        ''Else
        ''    tbcodigocontrol.BackColor = Color.White
        ''    MEP.SetError(tbcodigocontrol, "")
        ''End If
        If tbnrofactura.Text >= rangoInicial And tbnrofactura.Text <= rangoFinal Then
            tbnrofactura.BackColor = Color.White
            MEP.SetError(tbnrofactura, "")
        Else
            tbnrofactura.BackColor = Color.Red
            MEP.SetError(tbnrofactura, "Este Numero de factura no se encuentre en el rango configurado. Ingrese numero de factura valido!".ToUpper)
            _ok = False
            Return _ok
        End If
        MHighlighterFocusTT.UpdateHighlights()
        Return _ok

    End Function

    Public Sub _prHabilitarFocus()
        With MHighlighterFocusTT
            .SetHighlightOnFocus(tbnrofactura, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbnroautorizacion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbcodigocontrol, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

            .SetHighlightOnFocus(btnguardar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnsalir, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Private Sub F0_SolicitarDatosFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciar()
        _prHabilitarFocus()
    End Sub
    Public Function _fnValidarNroFactura() As Boolean
        Dim dt As DataTable = L_fnObtenerExisteFactura(sucursal, tbnrofactura.Text, FechaI, FechaF)
        If (dt.Rows.Count > 0) Then
            MEP.SetError(tbnrofactura, "Este Numero de factura ya esta siendo usado en otra venta. Ingrese numero de factura valido!".ToUpper)
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "Este Numero de factura ya esta siendo usado en otra venta. Ingrese numero de factura valido!".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        Else
            Return True

        End If
    End Function
    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If (_prValidar()) Then
            If (_fnValidarNroFactura()) Then
                NroAutorizacion = tbnroautorizacion.Text
                NroControl = tbcodigocontrol.Text
                nameFactura = tbnrofactura.Text
                Bandera = True
                Me.Close()
            End If


        End If
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Bandera = False
        Me.Close()

    End Sub

    Private Sub tbnrofactura_KeyDown(sender As Object, e As KeyEventArgs) Handles tbnrofactura.KeyDown
        If e.KeyData = Keys.Enter Then
            btnguardar.Focus()
        End If
    End Sub

    Private Sub tbnroautorizacion_KeyDown(sender As Object, e As KeyEventArgs) Handles tbnroautorizacion.KeyDown
        If e.KeyData = Keys.Enter Then
            tbcodigocontrol.Focus()
        End If
    End Sub

    Private Sub tbcodigocontrol_KeyDown(sender As Object, e As KeyEventArgs) Handles tbcodigocontrol.KeyDown
        If e.KeyData = Keys.Enter Then
            btnguardar.Focus()
        End If
    End Sub


    Private Sub F0_SolicitarDatosFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub


End Class