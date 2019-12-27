Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX.EditControls

Module P_Global
    Public gs_RutaImagenes As String = "C:\Imagenes"
    Public gs_RutaArchivos As String = "C:\Doc"

    'Variables del archivo de configuración
    Public gs_Ip As String = "127.0.0.1"
    Public gs_UsuarioSql As String = "sa"
    Public gs_ClaveSql As String = "123"
    Public gs_NombreBD As String = "BDDicon"
    Public gs_CarpetaRaiz As String = "C:\BD"

#Region "Variables"

    Public gc_SeparadorDecimal As Char = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
    Public Visualizador As Visualizador

#End Region

#Region "Librerias"



    Public gi_LibSistema As Integer = 1
    Public gi_LibSISModulo As Integer = 1

    Public gi_LibCuenta As Integer = 2
    Public gi_LibCuentaTipo As Integer = 1

    Public gi_LibComprobante As Integer = 3
    Public gi_LibCOMPROBANTETipo As Integer = 1

    Public gi_LibArqueo As Integer = 4
    Public gi_LibARQUEOTurno As Integer = 1

    Public gi_LibMaquina As Integer = 6
    Public gi_LibMAQUINACombustible As Integer = 1
    Public gi_LibMAQUINAManguera As Integer = 2

    Public gi_LibFactura As Integer = 9
    Public gi_LibFACTURATipo As Integer = 1

#End Region

#Region "Metodos"

    'Tipos de Modos
    '1 Valida que sea solo Numeros
    '2 Valida que sea solo Letras
    '3 Valida que sea Numeros y el Separador de Decimales
    '4 Valida que sea Numeros y el guion (-)
    Public Sub G_ValidarTextBox(ByVal _Modo As Byte, ByRef ee As KeyPressEventArgs)
        Select Case _Modo
            Case 1
                If (Char.IsNumber(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (Char.IsControl(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (Char.IsPunctuation(ee.KeyChar)) Then
                    ee.Handled = False
                Else
                    ee.Handled = True
                End If
            Case 2
                If (Char.IsLetter(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (Char.IsControl(ee.KeyChar)) Then
                    ee.Handled = False
                Else
                    ee.Handled = True
                End If
            Case 3
                If (Char.IsNumber(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (ee.KeyChar.Equals(gc_SeparadorDecimal)) Then
                    ee.Handled = False
                ElseIf (Char.IsControl(ee.KeyChar)) Then
                    ee.Handled = False
                Else
                    ee.Handled = True
                End If
            Case 4
                If (Char.IsNumber(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (ee.KeyChar.Equals(Convert.ToChar("-"))) Then
                    ee.Handled = False
                ElseIf (Char.IsControl(ee.KeyChar)) Then
                    ee.Handled = False
                Else
                    ee.Handled = True
                End If
            Case 5
                If (Char.IsNumber(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (ee.KeyChar.Equals(gc_SeparadorDecimal)) Then
                    ee.Handled = False
                ElseIf (ee.KeyChar.Equals(Convert.ToChar("-"))) Then
                    ee.Handled = False
                ElseIf (Char.IsControl(ee.KeyChar)) Then
                    ee.Handled = False
                Else
                    ee.Handled = True
                End If
        End Select
    End Sub

   

    Public Sub g_prArmarCombo(cbj As MultiColumnCombo, dtCombo As DataTable,
                              Optional anchoCodigo As Integer = 60, Optional anchoDesc As Integer = 200,
                              Optional nombreCodigo As String = "Código", Optional nombreDescripción As String = "Nombre")
        With cbj.DropDownList
            .Columns.Clear()

            .Columns.Add(dtCombo.Columns("cod").ToString).Width = anchoCodigo
            .Columns(0).Caption = nombreCodigo
            .Columns(0).Visible = True

            .Columns.Add(dtCombo.Columns("desc").ToString).Width = anchoDesc
            .Columns(1).Caption = nombreDescripción
            .Columns(1).Visible = True

            .ValueMember = dtCombo.Columns("cod").ToString
            .DisplayMember = dtCombo.Columns("desc").ToString
            .DataSource = dtCombo
            .Refresh()
        End With
    End Sub

   

#End Region

#Region "Configuracion del sistema"
    Public gs_llaveDinases As String = "carlosdinases123"
    Public gb_mostrarMapa As Boolean = True
    Public gi_userFuente As Integer = 8
    Public gs_user As String = "DEFAULT"
    Public gi_userNumi As Integer = 0
    Public gi_userRol As Integer = 0
    Public gi_userNumiEmpresa As Integer = 0
    Public gi_userNumiSucursal As Integer = 0
    Public gb_userTodasSuc As Boolean = False

    Public gd_tipoCambioCarburantes As Double = 0

    'configuracion del sistema tabla TCG011
    Public gs_empresaDireccion As String
    Public gs_empresaNit As String
    Public gs_empresaDescSistema As String
    Public gs_empresaDesc As String
    Public gi_empresaNumi As String
#End Region

#Region "Imagenes Reportes Categorias"

    Public Function G_getImgCategoria(cat As String) As String
        Select Case cat
            Case "A" : Return gs_CarpetaRaiz + "\Imagenes Categorias\" + "categoria_A.png"
            Case "B" : Return gs_CarpetaRaiz + "\Imagenes Categorias\" + "categoria_B.png"
            Case "C" : Return gs_CarpetaRaiz + "\Imagenes Categorias\" + "categoria_C.png"
            Case "P" : Return gs_CarpetaRaiz + "\Imagenes Categorias\" + "categoria_P.png"
            Case "M" : Return gs_CarpetaRaiz + "\Imagenes Categorias\" + "categoria_M.png"
        End Select
        Return ""
    End Function
#End Region

#Region "TOAST"
    Public Function getMensaje(mensaje As String, Optional tam As String = "6") As String
        Dim menFinal As String = "<b><font size=" + Chr(34) + "+" + tam + Chr(34) + "><font color=" + Chr(34) + "#FF0000" + Chr(34) + "></font>" + mensaje + "</font></b>"
        Return menFinal
    End Function
#End Region

#Region "Ventanas"


    Public Function _fnCrearPanelVentanas(frm As Form) As Panel
        Dim panel As New Panel()
        panel.Name = "panelA"
        panel.Dock = DockStyle.Fill
        panel.BackColor = Color.White
        frm.TopLevel = False
        frm.Location = New Point(0, 0)
        frm.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frm.Dock = panel.Dock
        panel.Controls.Add(frm)
        Return panel

    End Function
#End Region

#Region "Factura"

    Public gi_IVA As Decimal = 13 'Valor por defecto 13 = IVA actual Bolivia 2016
    Public gi_ICE As Decimal = 55 'Valor por defecto 55 = ICE actual Bolivia 2016

    'Parematros de la tabla TC0001
    Public gb_FacturaEmite As Boolean = True 'Emite factura? true=Sistema factura; false=Sistema no factura 
    Public gi_FacturaTipo As Byte = 2 'Tipo de Factura, 1=Ticket, 2=Hoja Carta
    Public gi_FacturaCantidadItems As Byte = 20 'Cantidad de items para la factura, 0 es sin limite
    Public gb_FacturaIncluirICE As Boolean = False 'Incluir en Importe ICE / IEHD / TASAS?, true=Si se incluye, false=No se incluye

#End Region
End Module
