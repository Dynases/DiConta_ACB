Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class F1_Personal

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

#Region "Marco"
    'Public Sub _prConectar()
    '    Dim ta As DataTable = L_prObtenerIpReloj(1)
    '    If (ta.Rows.Count > 0) Then
    '        Ip = ta.Rows(0).Item("caip")
    '    Else
    '        MsgBox("No se Pudo Conectar Error en Obtener Ip Sucursal", MsgBoxStyle.Exclamation, "Error")
    '        Return

    '    End If

    '    Cursor = Cursors.WaitCursor

    '    Dim idwErrorCode As Integer
    '    If btConectar.Text = "DESCONECTAR" Then
    '        axCZKEM1.Disconnect()
    '        bIsConnected = False
    '        btConectar.Text = "CONECTAR"

    '        btConectar.Image = My.Resources.switch_2
    '        Cursor = Cursors.Default
    '        Return
    '    End If
    '    bIsConnected = axCZKEM1.Connect_Net(Ip, Convert.ToInt32("4370"))
    '    If bIsConnected = True Then
    '        iMachineNumber = 1 'In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
    '        axCZKEM1.RegEvent(iMachineNumber, 65535) 'Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)

    '        btConectar.Text = "DESCONECTAR"
    '        btConectar.Image = My.Resources.switch_3

    '        btConectar.Refresh()
    '    Else
    '        axCZKEM1.GetLastError(idwErrorCode)
    '        MsgBox("No se Pudo Conectar=" & idwErrorCode, MsgBoxStyle.Exclamation, "Error")

    '    End If
    '    Cursor = Cursors.Default
    'End Sub
    'Public Sub _prRegistrarEmpleadoAlReloj()
    '    If bIsConnected = False Then
    '        MsgBox("Conecte el Reloj al sistema por favor".ToUpper, MsgBoxStyle.Exclamation, "Error")
    '        Return
    '    End If
    '    Dim idwErrorCode As Integer
    '    Dim bEnabled As Boolean = True
    '    If (TableEmpleado.Rows.Count > 0) Then
    '        For i As Integer = 0 To TableEmpleado.Rows.Count - 1 Step 1
    '            Dim sdwEnrollNumber As Integer = TableEmpleado.Rows(i).Item("panumi")
    '            Dim sName As String = TableEmpleado.Rows(i).Item("nombre")
    '            Dim sPassword As String = ""
    '            Dim iPrivilege As Integer = 0
    '            Dim sCardnumber As String = ""

    '            Cursor = Cursors.WaitCursor
    '            axCZKEM1.EnableDevice(iMachineNumber, False)
    '            axCZKEM1.SetStrCardNumber(sCardnumber) 'Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
    '            If axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled) = True Then 'upload the user's information(card number included)
    '                'MsgBox("SetUserInfo,UserID:" + sdwEnrollNumber.ToString() + " Privilege:" + iPrivilege.ToString() + " Cardnumber:" + sCardnumber + " Enabled:" + bEnabled.ToString(), MsgBoxStyle.Information, "Success")
    '                TableEmpleado.Rows(i).Item("pareloj") = 1
    '            Else
    '                axCZKEM1.GetLastError(idwErrorCode)
    '                MsgBox("No se Pudo Registrar,ErrorCode=" & idwErrorCode.ToString(), MsgBoxStyle.Exclamation, "Error")
    '            End If
    '            axCZKEM1.RefreshData(iMachineNumber) 'the data in the device should be refreshed
    '            axCZKEM1.EnableDevice(iMachineNumber, True)
    '            Cursor = Cursors.Default
    '        Next
    '    Else
    '        axCZKEM1.GetLastError(idwErrorCode)
    '        MsgBox("NO EXISTE USUARIO SIN REGISTRAR EN EL RELOJ DE MARCACION", MsgBoxStyle.Exclamation, "AVISO")
    '    End If

    'End Sub
#End Region
    Private Sub _prIniciarTodo()
        Me.Text = "P E R S O N A L"

        '_prCargarComboLibreria(tbTipo, gi_LibPERSONAL, gi_LibPERSTipo)
        '_prCargarComboLibreria(tbEstCivil, gi_LibPERSONAL, gi_LibPERSEstCivil)
        '_prCargarComboSucursal()

        '_prInicarMapa()

        _PMIniciarTodo()

        _prAsignarPermisos()
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

    'Public Sub _prInicarMapa()
    '    _Punto = 0
    '    '_ListPuntos = New List(Of PointLatLng)
    '    _Overlay = New GMapOverlay("points")
    '    mpUbicacion.Overlays.Add(_Overlay)
    '    P_IniciarMap()
    'End Sub
    'Private Sub P_IniciarMap()
    '    mpUbicacion.DragButton = MouseButtons.Left
    '    mpUbicacion.CanDragMap = True
    '    mpUbicacion.MapProvider = GMapProviders.GoogleMap
    '    If (_latitud <> 0 And _longitud <> 0) Then

    '        mpUbicacion.Position = New PointLatLng(_latitud, _longitud)
    '    Else

    '        _Overlay.Markers.Clear()
    '        mpUbicacion.Position = New PointLatLng(-17.3931784, -66.1738852)
    '    End If

    '    mpUbicacion.MinZoom = 0
    '    mpUbicacion.MaxZoom = 24
    '    mpUbicacion.Zoom = 15.5
    '    mpUbicacion.AutoScroll = True

    '    GMapProvider.Language = LanguageType.Spanish
    'End Sub

    'Public Sub _dibujarUbicacion(_nombre As String)
    '    If (_latitud <> 0 And _longitud <> 0) Then
    '        Dim plg As PointLatLng = New PointLatLng(_latitud, _longitud)
    '        _Overlay.Markers.Clear()
    '        P_AgregarPunto(plg, _nombre)
    '    Else


    '        _Overlay.Markers.Clear()
    '        mpUbicacion.Position = New PointLatLng(-17.3931784, -66.1738852)
    '    End If
    'End Sub

    'Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
    '    Dim dt As New DataTable
    '    dt = L_prLibreriaDetalleGeneral(cod1, cod2)

    '    With mCombo
    '        .DropDownList.Columns.Clear()

    '        .DropDownList.Columns.Add("cenum").Width = 70
    '        .DropDownList.Columns("cenum").Caption = "COD"

    '        .DropDownList.Columns.Add("cedesc1").Width = 200
    '        .DropDownList.Columns("cedesc1").Caption = "DESCRIPCION"

    '        .ValueMember = "cenum"
    '        .DisplayMember = "cedesc1"
    '        .DataSource = dt
    '        .Refresh()
    '    End With
    'End Sub

    'Private Sub P_AgregarPunto(pointLatLng As PointLatLng, _nombre As String)
    '    If (Not IsNothing(_Overlay)) Then
    '        'añadir puntos
    '        'Dim markersOverlay As New GMapOverlay("markers")
    '        Dim marker As New GMarkerGoogle(pointLatLng, My.Resources.markerIcono)
    '        'añadir tooltip
    '        Dim mode As MarkerTooltipMode = MarkerTooltipMode.OnMouseOver
    '        marker.ToolTip = New GMapBaloonToolTip(marker)
    '        marker.ToolTipMode = mode
    '        Dim ToolTipBackColor As New SolidBrush(Color.Blue)
    '        marker.ToolTip.Fill = ToolTipBackColor
    '        marker.ToolTip.Foreground = Brushes.White
    '        If (Not _nombre.ToString = String.Empty) Then
    '            marker.ToolTipText = "PERSONAL: " + _nombre
    '        End If
    '        _Overlay.Markers.Add(marker)
    '        'mapa.Overlays.Add(markersOverlay)
    '        mpUbicacion.Position = pointLatLng
    '    End If
    'End Sub

    'Private Sub _prCargarImagen()
    '    OfdFoto.InitialDirectory = "C:\Users\" + Environment.UserName + "\Pictures"
    '    OfdFoto.Filter = "Imagen|*.jpg;*.jpeg;*.png;*.bmp"
    '    OfdFoto.FilterIndex = 1
    '    If (OfdFoto.ShowDialog() = DialogResult.OK) Then
    '        vlImagen = New CImagen(OfdFoto.SafeFileName, OfdFoto.FileName, 0)
    '        pbImg.SizeMode = PictureBoxSizeMode.StretchImage
    '        pbImg.Load(vlImagen.getImagen())
    '    End If
    'End Sub

    'Private Sub _prEliminarImagen()
    '    vlImagen = Nothing
    '    pbImg.Image = Nothing
    'End Sub

    'Private Sub _prGuardarImagen()
    '    Dim rutaDestino As String = vlRutaBase + "\Imagenes\Imagenes Personal\"
    '    If System.IO.Directory.Exists(rutaDestino) = False Then
    '        If System.IO.Directory.Exists(vlRutaBase + "\Imagenes\") = False Then
    '            System.IO.Directory.CreateDirectory(vlRutaBase + "\Imagenes")
    '            If System.IO.Directory.Exists(vlRutaBase + "\Imagenes\Imagenes Personal\") = False Then
    '                System.IO.Directory.CreateDirectory(vlRutaBase + "\Imagenes\Imagenes Personal")
    '            End If
    '        Else
    '            If System.IO.Directory.Exists(vlRutaBase + "\Imagenes\Imagenes Personal") = False Then
    '                System.IO.Directory.CreateDirectory(vlRutaBase + "\Imagenes\Imagenes Personal")
    '            End If
    '        End If
    '    End If

    '    Dim rutaOrigen As String
    '    rutaOrigen = vlImagen.getImagen()
    '    FileCopy(rutaOrigen, rutaDestino + vlImagen.nombre + ".jpg")

    'End Sub

    'Private Sub _prCargarGridDetalle(idCabecera As String)
    '    Dim dt As New DataTable
    '    dt = L_prPersonaDetalleGeneral(idCabecera)

    '    grDetalle.DataSource = dt

    '    'dar formato a las columnas
    '    With grDetalle.Columns("pblinea")
    '        .Width = 50
    '        .Visible = False
    '    End With

    '    With grDetalle.Columns("pbnumi")
    '        .Width = 60
    '        .Visible = False
    '    End With

    '    With grDetalle.Columns("pbano")
    '        .HeaderText = "AÑO"
    '        .Width = 50
    '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '    End With

    '    With grDetalle.Columns("pbmes")
    '        .HeaderText = "MES"
    '        .Width = 50
    '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '    End With

    '    With grDetalle.Columns("pbsueldo")
    '        .HeaderText = "SUELDO"
    '        .Width = 100
    '        .DefaultCellStyle.Format = "0.00"
    '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '    End With

    '    With grDetalle.Columns("estado")
    '        .ReadOnly = True
    '        .Visible = False
    '    End With

    '    With grDetalle
    '        .AllowUserToAddRows = False
    '        .ContextMenuStrip = cmOpciones
    '    End With
    'End Sub

    'Private Sub _prCargarComboSucursal()
    '    Dim dt As New DataTable
    '    dt = L_prSucursalAyuda()

    '    With tbSuc
    '        .DropDownList.Columns.Clear()

    '        .DropDownList.Columns.Add("canumi").Width = 70
    '        .DropDownList.Columns("canumi").Caption = "COD"

    '        .DropDownList.Columns.Add("cadesc").Width = 200
    '        .DropDownList.Columns("cadesc").Caption = "descripcion".ToUpper

    '        .ValueMember = "canumi"
    '        .DisplayMember = "cadesc"
    '        .DataSource = dt
    '        .Refresh()
    '    End With


    '    If gb_userTodasSuc = False Then
    '        tbSuc.Enabled = False
    '    End If
    'End Sub

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
        tbApellido.ReadOnly = False
        tbTelef2.ReadOnly = False
        tbCi.ReadOnly = False
        tbDireccion.ReadOnly = False
        tbEmail.ReadOnly = False
        tbNombre.ReadOnly = False
        tbTelef1.ReadOnly = False
        tbTipo.IsReadOnly = False
        'tbEstado.IsReadOnly = False
        'tbEmpr.IsReadOnly = False
        'tbFIng.Enabled = True
        'tbFNac.Enabled = True
        'tbFRet.Enabled = True
        'tbObs.ReadOnly = False
        'tbTipo.ReadOnly = False
        'tbEstCivil.ReadOnly = False
        'tbSuc.ReadOnly = False
        'tbFijo.IsReadOnly = False
        'tbFSalida.Enabled = True
        'PanelAgrImagen.Enabled = True
        'grDetalle.Enabled = True

        'grDetalle.AllowUserToAddRows = True

        'tbPareja.ReadOnly = False
        'tbHijos.ReadOnly = False
        'tbMatrSeguro.ReadOnly = False
        'tbProbSalud.ReadOnly = False
        'tbTipoSangre.ReadOnly = False

    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbCi.ReadOnly = True
        tbApellido.ReadOnly = True
        tbTelef2.ReadOnly = True
        tbDireccion.ReadOnly = True
        tbEmail.ReadOnly = True
        tbNombre.ReadOnly = True
        tbNumi.ReadOnly = True
        tbTelef1.ReadOnly = True
        tbTipo.IsReadOnly = True

        'tbEstado.IsReadOnly = True
        'tbEmpr.IsReadOnly = True
        'tbReloj.IsReadOnly = True
        'tbFIng.Enabled = False
        'tbFNac.Enabled = False
        'tbFRet.Enabled = False
        'tbObs.ReadOnly = True
        'tbTipo.ReadOnly = True
        'tbSalario.Enabled = False
        'tbEstCivil.ReadOnly = True
        'tbSuc.ReadOnly = True
        'tbFijo.IsReadOnly = True
        'tbFSalida.Enabled = False
        'PanelAgrImagen.Enabled = False
        'grDetalle.Enabled = False

        'tbPareja.ReadOnly = True
        'tbHijos.ReadOnly = True
        'tbMatrSeguro.ReadOnly = True
        'tbProbSalud.ReadOnly = True
        'tbTipoSangre.ReadOnly = True
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbApellido.Text = ""
        tbTelef2.Text = ""
        tbCi.Text = ""
        tbDireccion.Text = ""
        tbEmail.Text = ""
        tbNombre.Text = ""
        tbNumi.Text = ""
        tbTelef1.Text = ""
        tbTipo.Value = True

        'tbEstado.Value = True
        'tbEmpr.Value = True
        'tbReloj.Value = False
        'tbFIng.Value = Now.Date
        'tbFNac.Value = Now.Date
        'tbFRet.Value = Now.Date
        'tbObs.Text = ""
        'tbSalario.Value = 0
        'tbTipo.Text = ""
        'tbEstCivil.Text = ""
        'tbFijo.Value = True
        'tbFSalida.Value = DateAdd(DateInterval.Day, 89, Now.Date)
        'tbSuc.Value = gi_userSuc
        '_prEliminarImagen()

        'tbPareja.Text = ""
        'tbHijos.Text = ""
        'tbMatrSeguro.Text = ""
        'tbProbSalud.Text = ""
        'tbTipoSangre.Text = ""

        '_latitud = 0
        '_longitud = 0
        '_Overlay.Markers.Clear()

        ''VACIO EL DETALLE
        '_prCargarGridDetalle(-1)
        'grDetalle.AllowUserToAddRows = True
        'btConectar.Visible = False
        'btuser.Visible = False
    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbApellido.BackColor = Color.White
        tbCi.BackColor = Color.White
        tbDireccion.BackColor = Color.White
        tbEmail.BackColor = Color.White
        tbNombre.BackColor = Color.White
        tbTelef2.BackColor = Color.White
        tbTelef1.BackColor = Color.White
        'tbEstado.BackColor = Color.White
        'tbEmpr.BackColor = Color.White
        'tbFIng.BackColor = Color.White
        'tbFNac.BackColor = Color.White
        'tbFRet.BackColor = Color.White
        'tbObs.BackColor = Color.White
        'tbSalario.BackColor = Color.White
        'tbEstCivil.BackColor = Color.White
        'tbTipo.BackColor = Color.White
        'tbSuc.BackColor = Color.White

        'tbPareja.BackColor = Color.White
        'tbHijos.BackColor = Color.White
        'tbMatrSeguro.BackColor = Color.White
        'tbProbSalud.BackColor = Color.White
        'tbTipoSangre.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean
        Dim nomImg As String
        'If IsNothing(vlImagen) = True Then
        '    nomImg = ""
        'Else
        '    nomImg = vlImagen.nombre
        'End If
        'If grDetalle.RowCount >= 2 Then
        '    Dim pos As Integer = grDetalle.RowCount - 2
        '    tbSalario.Value = grDetalle.Rows(pos).Cells("pbsueldo").Value
        'End If

        Dim res As Boolean = L_prPersonaGrabar(tbNumi.Text, tbCi.Text, tbNombre.Text, tbApellido.Text, tbDireccion.Text, tbTelef1.Text, tbTelef2.Text, tbEmail.Text, IIf(tbTipo.Value = True, 1, 0))
        If res Then
            'If IsNothing(vlImagen) = False Then
            '    vlImagen.nombre = nomImg
            '    _prGuardarImagen()
            'End If

            ToastNotification.Show(Me, "Codigo de Personal ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim nomImg As String
        'If IsNothing(vlImagen) = True Then
        '    nomImg = ""
        'Else
        '    nomImg = vlImagen.nombre
        'End If
        'If grDetalle.RowCount >= 2 Then
        '    Dim pos As Integer = grDetalle.RowCount - 2
        '    tbSalario.Value = grDetalle.Rows(pos).Cells("pbsueldo").Value
        'End If

        Dim res As Boolean = L_prPersonaModificar(tbNumi.Text, tbCi.Text, tbNombre.Text, tbApellido.Text, tbDireccion.Text, tbTelef1.Text, tbTelef2.Text, tbEmail.Text, IIf(tbTipo.Value = True, 1, 0))
        If res Then
            'If IsNothing(vlImagen) = False Then
            '    If vlImagen.tipo = 0 Then
            '        vlImagen.nombre = nomImg
            '        _prGuardarImagen()
            '    End If
            'End If
            ToastNotification.Show(Me, "Codigo de Personal ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _PSalirRegistro()

        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prPersonaBorrar(tbNumi.Text, mensajeError)
            If res Then
                ToastNotification.Show(Me, "Codigo de vehiculo ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        'If tbApellido.Text = String.Empty Then
        '    tbApellido.BackColor = Color.Red
        '    MEP.SetError(tbApellido, "ingrese apellido del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbApellido.BackColor = Color.White
        '    MEP.SetError(tbApellido, "")
        'End If

        'If tbCi.Text = String.Empty Then
        '    tbCi.BackColor = Color.Red
        '    MEP.SetError(tbCi, "ingrese ci del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbCi.BackColor = Color.White
        '    MEP.SetError(tbCi, "")
        'End If

        'If tbDireccion.Text = String.Empty Then
        '    tbDireccion.BackColor = Color.Red
        '    MEP.SetError(tbDireccion, "ingrese direccion del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbDireccion.BackColor = Color.White
        '    MEP.SetError(tbDireccion, "")
        'End If

        'If tbEmail.Text = String.Empty Then
        '    tbEmail.BackColor = Color.Red
        '    MEP.SetError(tbEmail, "ingrese e-mail del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbEmail.BackColor = Color.White
        '    MEP.SetError(tbEmail, "")
        'End If

        If tbNombre.Text = String.Empty Then
            tbNombre.BackColor = Color.Red
            MEP.SetError(tbNombre, "ingrese nombre del personal!".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If

        'If tbTelef1.Text = String.Empty Then
        '    tbTelef1.BackColor = Color.Red
        '    MEP.SetError(tbTelef1, "ingrese telefono del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbTelef1.BackColor = Color.White
        '    MEP.SetError(tbTelef1, "")
        'End If

        'If tbTelef2.Text = String.Empty Then
        '    tbTelef2.BackColor = Color.Red
        '    MEP.SetError(tbTelef2, "ingrese celular del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbTelef2.BackColor = Color.White
        '    MEP.SetError(tbTelef2, "")
        'End If

        'If tbObs.Text = String.Empty Then
        '    tbObs.BackColor = Color.Red
        '    MEP.SetError(tbObs, "ingrese la observacion del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbObs.BackColor = Color.White
        '    MEP.SetError(tbObs, "")
        'End If

        'If tbTipo.SelectedIndex < 0 Then
        '    tbTipo.BackColor = Color.Red
        '    MEP.SetError(tbTipo, "seleccione el tipo del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbTipo.BackColor = Color.White
        '    MEP.SetError(tbTipo, "")
        'End If

        'If tbEstCivil.SelectedIndex < 0 Then
        '    tbEstCivil.BackColor = Color.Red
        '    MEP.SetError(tbEstCivil, "seleccione el estado civil del personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbEstCivil.BackColor = Color.White
        '    MEP.SetError(tbEstCivil, "")
        'End If

        'If tbSuc.SelectedIndex < 0 Then
        '    tbSuc.BackColor = Color.Red
        '    MEP.SetError(tbSuc, "seleccione la sucursal a la que pertenece el personal!".ToUpper)
        '    _ok = False
        'Else
        '    tbSuc.BackColor = Color.White
        '    MEP.SetError(tbSuc, "")
        'End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        'Dim numiSuc As String = IIf(gb_userTodasSuc = True, "-1", gi_userSuc)
        Dim dtBuscador As DataTable = L_prPersonaGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("panumi", True, "ID", 70))
        listEstCeldas.Add(New Modelos.Celda("paci", True, "CI", 70))
        listEstCeldas.Add(New Modelos.Celda("panom", True, "NOMBRE", 200))
        listEstCeldas.Add(New Modelos.Celda("paape", True, "APELLIDO", 200))
        listEstCeldas.Add(New Modelos.Celda("padirec", True, "DIRECCION", 200))
        listEstCeldas.Add(New Modelos.Celda("patelef1", True, "TELEFONO", 100))
        listEstCeldas.Add(New Modelos.Celda("patelef2", True, "CELULAR", 100))
        listEstCeldas.Add(New Modelos.Celda("paemail", True, "E-MAIL", 100))
        listEstCeldas.Add(New Modelos.Celda("patipo", False))
        'listEstCeldas.Add(New Modelos.Celda("tipodesc", True, "TIPO", 70))
        'listEstCeldas.Add(New Modelos.Celda("paobs", True, "OBSERVACION", 70))
        'listEstCeldas.Add(New Modelos.Celda("pafnac", True, "FEC. NACIMIENTO", 80))
        'listEstCeldas.Add(New Modelos.Celda("pafing", True, "FEC. INGRESO", 80))
        'listEstCeldas.Add(New Modelos.Celda("pafret", True, "FEC. RETIRO", 80))
        'listEstCeldas.Add(New Modelos.Celda("paest", False))
        'listEstCeldas.Add(New Modelos.Celda("paest2", True, "ESTADO", 80))
        'listEstCeldas.Add(New Modelos.Celda("pasal", True, "SALARIO", 80, "0.00"))
        'listEstCeldas.Add(New Modelos.Celda("paeciv", False))
        'listEstCeldas.Add(New Modelos.Celda("pafot", False))
        'listEstCeldas.Add(New Modelos.Celda("civildesc", True, "EST. CIVIL", 120))
        'listEstCeldas.Add(New Modelos.Celda("pasuc", False))
        'listEstCeldas.Add(New Modelos.Celda("cadesc", True, "SUCURSAL", 120))
        'listEstCeldas.Add(New Modelos.Celda("pafijo", False))
        'listEstCeldas.Add(New Modelos.Celda("pafijo2", True, "FIJO", 80))
        'listEstCeldas.Add(New Modelos.Celda("pafsal", True, "SALIDA", 80))
        'listEstCeldas.Add(New Modelos.Celda("pareloj", False))
        'listEstCeldas.Add(New Modelos.Celda("pareloj2", True, "RELOJ", 80))
        'listEstCeldas.Add(New Modelos.Celda("paemp", False))
        'listEstCeldas.Add(New Modelos.Celda("paemp2", True, "EMPRESA", 120))
        'listEstCeldas.Add(New Modelos.Celda("palat", False))
        'listEstCeldas.Add(New Modelos.Celda("palon", False))
        'listEstCeldas.Add(New Modelos.Celda("paesp", True, "PAREJA", 120))
        'listEstCeldas.Add(New Modelos.Celda("pahijos", True, "HIJOS", 120))
        'listEstCeldas.Add(New Modelos.Celda("pamseg", True, "MATRI. SEGURO", 120))
        'listEstCeldas.Add(New Modelos.Celda("patsan", True, "TIPO SANGRE", 120))
        'listEstCeldas.Add(New Modelos.Celda("papsalud", True, "PROBLEM. SALUD", 120))
        listEstCeldas.Add(New Modelos.Celda("pafact", False))
        listEstCeldas.Add(New Modelos.Celda("pahact", False))
        listEstCeldas.Add(New Modelos.Celda("pauact", False))
        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("panumi").ToString
            tbCi.Text = .GetValue("paci").ToString
            tbNombre.Text = .GetValue("panom").ToString
            tbApellido.Text = .GetValue("paape").ToString
            tbDireccion.Text = .GetValue("padirec").ToString
            tbTelef1.Text = .GetValue("patelef1").ToString
            tbTelef2.Text = .GetValue("patelef2").ToString
            tbEmail.Text = .GetValue("paemail").ToString
            tbTipo.Value = IIf(IsDBNull(.GetValue("patipo")) = True, True, .GetValue("patipo"))
            'tbEstado.Value = .GetValue("paest")
            'tbEstCivil.Text = .GetValue("civildesc")
            'tbTipo.Text = .GetValue("tipodesc")
            'tbFIng.Value = .GetValue("pafing")
            'tbFNac.Value = .GetValue("pafnac")
            'tbFRet.Value = .GetValue("pafret")
            'tbSalario.Text = .GetValue("pasal")
            'tbObs.Text = .GetValue("paobs").ToString
            'tbSuc.Text = .GetValue("cadesc").ToString
            'tbFijo.Value = IIf(.GetValue("pafijo").ToString = "1", True, False)
            'tbFSalida.Value = IIf(IsDBNull(.GetValue("pafsal")) = True, Now.Date, .GetValue("pafsal"))
            'tbReloj.Value = IIf(IsDBNull(.GetValue("pareloj")) = True, False, .GetValue("pareloj"))
            'tbEmpr.Value = IIf(IsDBNull(.GetValue("paemp")) = True, True, IIf(.GetValue("paemp") = 1, True, False))

            '_latitud = IIf(IsDBNull(.GetValue("palat")) = True, 0, .GetValue("palat"))
            '_longitud = IIf(IsDBNull(.GetValue("palon")) = True, 0, .GetValue("palon"))
            '_dibujarUbicacion(tbNombre.Text + " " + tbApellido.Text)
            'tbPareja.Text = .GetValue("paesp").ToString
            'tbHijos.Text = .GetValue("pahijos").ToString
            'tbMatrSeguro.Text = .GetValue("pamseg").ToString
            'tbTipoSangre.Text = .GetValue("patsan").ToString
            'tbProbSalud.Text = .GetValue("papsalud").ToString

            'If (tbReloj.Value = True) Then
            '    btConectar.Visible = False
            '    btuser.Visible = False
            'Else
            '    btConectar.Visible = True
            '    btuser.Visible = True

            'End If

            'Dim nomImg = .GetValue("pafot").ToString
            'If nomImg = String.Empty Then
            '    pbImg.Image = Nothing
            'Else
            '    Dim rutaOrigen As String = vlRutaBase + "\Imagenes\Imagenes Personal\"
            '    vlImagen = New CImagen(nomImg, rutaOrigen + nomImg, 1)
            '    pbImg.SizeMode = PictureBoxSizeMode.StretchImage
            '    Try
            '        pbImg.Load(vlImagen.getImagen())
            '    Catch ex As Exception
            '        pbImg.Image = Nothing
            '        vlImagen = Nothing
            '    End Try

            'End If



            lbFecha.Text = CType(.GetValue("pafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("pahact").ToString
            lbUsuario.Text = .GetValue("pauact").ToString

            'CARGAR DETALLE
            '_prCargarGridDetalle(tbNumi.Text)
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
        tbCi.Focus()
    End Sub
    Private Sub tbEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbEmail.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToLower
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        tbCi.Focus()
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub
#End Region

End Class