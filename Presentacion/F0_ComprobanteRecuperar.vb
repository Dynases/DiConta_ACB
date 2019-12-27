Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Public Class F0_ComprobanteRecuperar
    Public filaSelect As Janus.Windows.GridEX.GridEXRow
    Public seleccionado As Boolean
    Private _dtReg As DataTable
    Public _dtSeleccionado As DataTable
    Private _i As Integer = 0

#Region "Metodos privados"

    Private Sub _prCargarGridDetalle()
        Dim numi As String = _dtReg.Rows(_i).Item("obnumito1")
        Dim user As String = _dtReg.Rows(_i).Item("yduser")

        Dim dt As New DataTable
        dt = L_prComprobanteDetalleGeneralRecuperado(numi)

        tbUsuarios.text = user

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
            .Width = 70
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cadesc2")
            .Width = 50
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("cacta")
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

        End With

        With grDetalle.RootTable.Columns("obtc")
            .Caption = "TC"
            .CellStyle.TextAlignment = TextAlignment.Far
            .HeaderAlignment = TextAlignment.Center
            .Width = 70
            .FormatString = "0.00"
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

#End Region

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If grDetalle.Row >= 0 Then
            _dtSeleccionado = CType(grDetalle.DataSource, DataTable)
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

        _dtReg = L_prComprobanteDetalleObtenerRecuperados()
        If _dtReg.Rows.Count > 0 Then
            _prCargarGridDetalle()
        End If
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


    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        If _i < _dtReg.Rows.Count - 1 Then
            _i = _i + 1
            _prCargarGridDetalle()
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        If _i >= 1 Then
            _i = _i - 1
            _prCargarGridDetalle()
        End If
    End Sub
End Class