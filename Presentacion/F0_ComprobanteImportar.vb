Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Public Class F0_ComprobanteImportar
    Public filaSelect As Janus.Windows.GridEX.GridEXRow
    Public seleccionado As Boolean

#Region "Metodos privados"
    Private Sub _PMCargarBuscador()
        Dim dtBuscador As DataTable
        If tbFiltrarFecha.Value = True Then
            dtBuscador = L_prComprobanteGeneral2(gi_empresaNumi, tbFechaDel.Value.ToString("yyyy/MM/dd"), tbFechaAl.Value.ToString("yyyy/MM/dd"))

        Else
            dtBuscador = L_prComprobanteGeneral(gi_empresaNumi)
        End If

        Dim _MListEstBuscador As New List(Of Modelos.Celda)
        _MListEstBuscador.Add(New Modelos.Celda("oanumi", True, "ID", 50))
        _MListEstBuscador.Add(New Modelos.Celda("oatip", False))
        _MListEstBuscador.Add(New Modelos.Celda("oanumdoc", True, "NRO. DOCUMENTO", 100))
        _MListEstBuscador.Add(New Modelos.Celda("cndesc1", True, "TIPO", 80))
        _MListEstBuscador.Add(New Modelos.Celda("oaano", False))
        _MListEstBuscador.Add(New Modelos.Celda("oames", False))
        _MListEstBuscador.Add(New Modelos.Celda("oanum", True, "NUMERO", 80))
        _MListEstBuscador.Add(New Modelos.Celda("oafdoc", True, "FECHA", 80))
        _MListEstBuscador.Add(New Modelos.Celda("oatc", True, "TIPO DE CAMBIO", 120, "0.00"))
        _MListEstBuscador.Add(New Modelos.Celda("oaglosa", True, "GLOSA", 400))
        _MListEstBuscador.Add(New Modelos.Celda("oaobs", True, "OBSERVACION", 200))
        _MListEstBuscador.Add(New Modelos.Celda("oaemp", False))

        grDetalle.DataSource = dtBuscador
        grDetalle.RetrieveStructure()

        For i = 0 To _MListEstBuscador.Count - 1
            Dim campo As String = _MListEstBuscador.Item(i).campo
            With grDetalle.RootTable.Columns(campo)
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
        With grDetalle
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
#End Region

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If grDetalle.Row >= 0 Then
            filaSelect = grDetalle.GetRow()
            seleccionado = True
            Me.Close()
        Else
            ToastNotification.Show(Me, "seleccione alguna fila".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)

        End If
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        seleccionado = False
        Me.Close()
    End Sub

    Private Sub F0_TipoCambio_Nuevo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbFechaAl.Value = Now.Date
        tbFechaDel.Value = Now.Date
        _PMCargarBuscador()
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

    Private Sub tbFiltrarFecha_ValueChanged(sender As Object, e As EventArgs) Handles tbFiltrarFecha.ValueChanged
        If tbFiltrarFecha.Value = True Then
            tbFechaDel.Enabled = True
            tbFechaAl.Enabled = True
            _PMCargarBuscador()

            'grDetalle.RemoveFilters()
            'grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("oafdoc"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, tbFechaDel.Value.ToString("dd/MM/yyyy")))
            'grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("oafdoc2"), Janus.Windows.GridEX.ConditionOperator.LessThanOrEqualTo, tbFechaAl.Value.ToString("dd/MM/yyyy")))
        Else
            'grDetalle.RemoveFilters()
            _PMCargarBuscador()
        End If

    End Sub

    Private Sub tbFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles tbFechaDel.ValueChanged, tbFechaAl.ValueChanged
        If tbFiltrarFecha.Value = True Then
            'grDetalle.RemoveFilters()
            'grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("oafdoc"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, tbFechaDel.Value.ToString("dd/MM/yyyy")))
            'grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("oafdoc2"), Janus.Windows.GridEX.ConditionOperator.LessThanOrEqualTo, tbFechaAl.Value.ToString("dd/MM/yyyy")))
            _PMCargarBuscador()
        End If
    End Sub
End Class