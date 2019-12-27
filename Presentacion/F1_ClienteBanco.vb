
Imports Janus.Windows.GridEX
Imports Modelos
Imports Logica.AccesoLogica
Public Class F1_ClienteBanco

#Region "ATRIBUTOS"
    Public dtBuscador As DataTable
    Public nombreVista As String
    Public posX As Integer
    Public posY As Integer
    Public seleccionado As Boolean

    Public filaSelect As Janus.Windows.GridEX.GridEXRow
    Dim Cuenta As Integer = 0
    Public listEstrucGrilla As List(Of Celda)
#End Region
#Region "METODOS PRIVADOS"
    Public Sub New(ByVal x As Integer, y As Integer, titulo As String)

        posX = x
        posY = y
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.StartPosition = FormStartPosition.Manual
        If posX = 0 And posY = 0 Then
            Me.StartPosition = FormStartPosition.CenterScreen
        Else
            Me.Location = New Point(posX, posY)
        End If

        GPPanelP.Text = titulo


        seleccionado = False

    End Sub

    Private Sub _PMCargarBuscador()

        Dim dt As New DataTable
        dt = L_prClientesBanco()

        grAyudaCuenta.DataSource = dt
        grAyudaCuenta.RetrieveStructure()
        't.canumi , t.canombre, t.cacuenta, t.caobs 
        'dar formato a las columnas
        With grAyudaCuenta.RootTable.Columns("canumi")
            .Width = 60
            .Visible = True
            .Caption = "CODIGO"
        End With



        With grAyudaCuenta.RootTable.Columns("canombre")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "NOMBRE"
            .Width = 250
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
        End With

        With grAyudaCuenta.RootTable.Columns("cacuenta")
            .Visible = True
            .Caption = "CUENTA"
            .Width = 200
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
        End With
        With grAyudaCuenta.RootTable.Columns("caobs")
            .Visible = True
            .Caption = "OBSERVACION"
            .Width = 200
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
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




    End Sub
#End Region

    Private Sub ModeloAyuda_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Escape)) Then
            e.Handled = True
            Me.Close()
        End If
    End Sub

    Private Sub grJBuscador_KeyDown(sender As Object, e As KeyEventArgs) Handles grAyudaCuenta.KeyDown

        If e.KeyData = Keys.Escape Then
            Me.Close()
        End If

        If e.KeyData = Keys.Enter Then
            If (grAyudaCuenta.Row >= 0) Then
                filaSelect = grAyudaCuenta.GetRow()
                seleccionado = True
                Me.Close()
            End If

        End If

    End Sub

    Private Sub grAyudaCuenta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grAyudaCuenta.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub



    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        'If grAyudaCuenta.Row >= 0 Then

        '    Dim nombre As String = InputBox("INGRESE NOMBRE", "BANCO", grAyudaCuenta.GetValue("cjnombre")).ToUpper
        '    If nombre <> "" Then
        '        't.canumi , t.canombre, t.cacuenta, t.caobs
        '        Dim numi As String = grAyudaCuenta.GetValue("cjnumi")
        '        L_prClientesComprobanteModificarNombre(numi, nombre)

        '        _PMCargarBuscador()


        '    End If

        'End If
    End Sub

    Private Sub F1_ClientePorCobrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PMCargarBuscador()
        grAyudaCuenta.Focus()
        grAyudaCuenta.MoveTo(grAyudaCuenta.FilterRow)
        grAyudaCuenta.Col = 2
    End Sub
End Class