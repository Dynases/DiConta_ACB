Imports Logica.AccesoLogica
Imports Modelos.MGlobal
Imports DevComponents.DotNetBar.Controls
Imports DevComponents.DotNetBar.Metro
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public Class P_Principal

#Region "Atributos"
    Private _version As String

#End Region

#Region "Metodos Privados"

    Public Sub New()
        _prCambiarStyle()
        ' This call is required by the designer.
        InitializeComponent()
        FP_Configuracion.Select ()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub _prIniciarTodo()
        'poner numiModulos a los modulos y tambien la version
        FP_Configuracion.Tag = New Version(1, "2.4.6")
        FP_Transacciones.Tag = New Version(2, "2.5.1")
        FP_Carburantes.Tag = New Version(3, "2.5.2")
        FP_Ventas.Tag = New Version(4, "2.5.8")
        FP_Inventario.Tag = New Version(5, "2.4.2")
        FP_Gerencia.Tag = New Version(6, "2.4.6")

        'le pongo la version a cada modulo


        'obtenerVersion
        Dim acerca As New P_Acerca
        _version = acerca.lbVersion.Text


        tbDecimal.Value = 3.14
        Dim simboloDecimal As String = tbDecimal.Value.ToString

        If simboloDecimal.Contains(".") = False Then 'And s <> "."
            Dim info As New TaskDialogInfo("CONFIGURACION ERRONEA".ToUpper, eTaskDialogIcon.Exclamation, "separador decimal incorrecto, no se puede iniciar el sistema".ToUpper, "cambiar el separador decimal a punto '.'".ToUpper, eTaskDialogButton.Ok, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            Close()
            Return
        End If

        'Leer Archivo de Configuración
        _prLeerArchivoConfig()

        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)

        L_prAbrirConexionBitacora(gs_Ip, gs_UsuarioSql, gs_ClaveSql, "BHDicon")

        Me.WindowState = FormWindowState.Maximized
        Me.Text = "DICONTA"

        'inicio la empresa
        Dim frmEmpresa As New P_LoginEmpresa
        frmEmpresa.ShowDialog()
        If frmEmpresa.ok = False Then
            Me.Close()
            Exit Sub
        End If

        'iniciar login de usuario
        _prLogin()

    End Sub
    Private Sub _prCambiarStyle()
        'tratar de cambiar estilo
        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.VistaGlass)
        'RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, eStyle.VisualStudio2012Dark)
        'RibbonPredefinedColorSchemes.ChangeStyle(eStyle.VisualStudio2012Dark)

        'cambio de otros colores
        Dim table As Office2007ColorTable = CType(GlobalManager.Renderer, Office2007Renderer).ColorTable
        Dim ct As SideNavColorTable = table.SideNav
        ct.TitleBackColor = Color.Black
        'ct.SideNavItem.MouseOver.BackColors = New Color() {Color.Red, Color.Yellow}
        ct.SideNavItem.MouseOver.BorderColors = New Color() {Color.Black} ' No border
        ct.SideNavItem.Selected.BackColors = New Color() {Color.Yellow}
        ct.BorderColors = New Color() {Color.Black} ' Control border color

        ct.PanelBackColor = Color.Black
    End Sub

    Private Sub _prLeerArchivoConfig()
        Dim Archivo() As String = IO.File.ReadAllLines(Application.StartupPath + "\CONFIG.TXT")
        gs_Ip = Archivo(0).Split("=")(1).Trim
        gs_UsuarioSql = Archivo(1).Split("=")(1).Trim
        gs_ClaveSql = Archivo(2).Split("=")(1).Trim
        gs_NombreBD = Archivo(3).Split("=")(1).Trim
        gs_CarpetaRaiz = Archivo(4).Split("=")(1).Trim
    End Sub

    Private Sub _prLogin()
        Dim Frm As New Login
        Frm.lbEmpresa.Text = gs_empresaDescSistema
        Frm.ShowDialog()

        If Frm._ok = True Then
            L_Usuario = gs_user
            Modelos.MGlobal.gs_usuario = gs_user

            lbUsuario.Text = gs_user
            lbUsuario.Font = New Font("Tahoma", 12, FontStyle.Bold)

            _PCargarPrivilegios()
            _prCargarConfiguracionSistema()
            SideNav1.Enabled = True

            Me.Text = "DICONTA -->" + gs_empresaDescSistema

        Else
            SideNav1.Enabled = False

            'inicio la empresa
            Dim frmEmpresa As New P_LoginEmpresa
            frmEmpresa.ShowDialog()
            If frmEmpresa.ok = False Then
                Me.Close()
                Exit Sub
            End If

            'iniciar login de usuario
            _prLogin()
        End If
    End Sub

    Private Sub _prCargarConfiguracionSistema()
        Dim dtConf As DataTable = L_prConGlobalGeneral()
        gd_tipoCambioCarburantes = dtConf.Rows(0).Item("gbtcambio")

    End Sub

    Private Sub _PCargarPrivilegios()
        Dim listaTabs As New List(Of DevComponents.DotNetBar.Metro.MetroTilePanel)
        listaTabs.Add(MetroTilePanel1)
        listaTabs.Add(MetroTilePanel2)
        listaTabs.Add(MetroTilePanel6)
        listaTabs.Add(MetroTilePanel7)
        listaTabs.Add(MetroTilePanel8)
        listaTabs.Add(MetroTilePanel9)

        Dim idRolUsu As String = gi_userRol

        Dim dtModulos As DataTable = L_prLibreriaDetalleGeneral(gi_LibSistema, gi_LibSISModulo)
        Dim listFormsModulo As New List(Of String)

        For i = 0 To dtModulos.Rows.Count - 1
            Dim dtDetRol As DataTable = L_RolDetalle_General(-1, idRolUsu, dtModulos.Rows(i).Item("cnnum"))
            listFormsModulo = New List(Of String)

            If dtDetRol.Rows.Count > 0 Then
                'cargo los nombres de los programas(botones) del modulo
                For Each fila As DataRow In dtDetRol.Rows
                    listFormsModulo.Add(fila.Item("yaprog").ToString.ToUpper)
                Next
                'recorro el modulo(tab) que corresponde
                For Each _item As DevComponents.DotNetBar.BaseItem In listaTabs.Item(i).Items
                    If TypeOf (_item) Is DevComponents.DotNetBar.Metro.MetroTileItem Then 'es un boton del modulo
                        Dim btn As DevComponents.DotNetBar.Metro.MetroTileItem = CType(_item, DevComponents.DotNetBar.Metro.MetroTileItem)
                        If listFormsModulo.Contains(btn.Name.ToUpper) Then 'si el nombre del boton pertenece a la lista de formularios del modulo
                            Dim Texto As String = btn.Text
                            Dim TTexto As String = btn.TitleText
                            Dim f As Integer = listFormsModulo.IndexOf(btn.Name.ToUpper)
                            If Texto = "" Then 'esta usando el Title Text
                                btn.TitleText = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                            Else 'esta usando el Text
                                btn.Text = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                            End If

                            If dtDetRol.Rows(f).Item("ycshow") = True Or dtDetRol.Rows(f).Item("ycadd") = True Or dtDetRol.Rows(f).Item("ycmod") = True Or dtDetRol.Rows(f).Item("ycdel") = True Then
                                btn.Visible = True
                            Else
                                btn.Visible = False
                            End If
                        Else 'si no pertenece lo oculto
                            btn.Visible = False
                        End If
                    Else 'seria un sub grupo en el modulo
                        'recorro todos los elementos del sub grupo
                        If TypeOf _item Is ItemContainer Then
                            For Each _subItem In _item.SubItems
                                Dim _subBtn As MetroTileItem = CType(_subItem, MetroTileItem)
                                If listFormsModulo.Contains(_subBtn.Name.ToUpper) Then
                                    Dim Texto As String = _subBtn.Text
                                    Dim TTexto As String = _subBtn.TitleText
                                    Dim f As Integer = listFormsModulo.IndexOf(_subBtn.Name.ToUpper)
                                    If Texto = "" Then 'esta usando el Title Text
                                        _subBtn.TitleText = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                                    Else 'esta usando el Text
                                        _subBtn.Text = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                                    End If

                                    If dtDetRol.Rows(f).Item("ycshow") = True Or dtDetRol.Rows(f).Item("ycadd") = True Or dtDetRol.Rows(f).Item("ycmod") = True Or dtDetRol.Rows(f).Item("ycdel") = True Then
                                        _subBtn.Visible = True
                                    Else
                                        _subBtn.Visible = False
                                    End If
                                Else
                                    _subBtn.Visible = False
                                End If
                            Next
                        End If

                    End If
                Next
            Else ' no exiten formulario registrados en el modulo pero igual hay que ocultar los botones y los subbotones que tenga
                For Each _item As DevComponents.DotNetBar.BaseItem In listaTabs.Item(i).Items
                    If TypeOf (_item) Is DevComponents.DotNetBar.Metro.MetroTileItem Then 'es un boton del modulo
                        Dim btn As DevComponents.DotNetBar.Metro.MetroTileItem = CType(_item, DevComponents.DotNetBar.Metro.MetroTileItem)
                        btn.Visible = False
                    Else 'seria un sub grupo en el modulo
                        'recorro todos los elementos del sub grupo
                        If TypeOf _item Is ItemContainer Then
                            For Each _subItem In _item.SubItems
                                Dim _subBtn As MetroTileItem = CType(_subItem, MetroTileItem)
                                _subBtn.Visible = False
                            Next
                        End If

                    End If
                Next

            End If

        Next

        'refrescar el formulario
        Me.Refresh()
    End Sub
#End Region

    Private Sub P_Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Private Sub P_Principal_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        _prLogin()
    End Sub

    Private Sub P_Principal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        _prLogin()
    End Sub

    Private Sub rmSesion_ItemClick(sender As Object, e As EventArgs) Handles rmSesion.ItemClick
        Dim item As RadialMenuItem = TryCast(sender, RadialMenuItem)
        If item IsNot Nothing AndAlso (Not String.IsNullOrEmpty(item.Text)) Then
            Select Case item.Name
                Case "btCerrarSesion"
                    _prLogin()
                Case "btSalir"
                    Me.Close()
                Case "btAbout"
                    Dim frm As New P_Acerca
                    frm.ShowDialog()
                Case "tbEmpresa"
                    'inicio la empresa
                    Dim frmEmpresa As New P_LoginEmpresa
                    frmEmpresa.ShowDialog()
                    If frmEmpresa.ok = False Then
                        Me.Close()
                        Exit Sub
                    End If
                    'iniciar login de usuario
                    _prLogin()
            End Select
        End If
    End Sub


    
    Private Sub btConfRoles_Click(sender As Object, e As EventArgs) Handles btConfRoles.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Rol
        frm._nameButton = btConfRoles.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)

        tab3.RecalcSize()
        tab3.ThemeAware = True
        tab3.ShowSubItems = True
        tab3.UpdateBindings()


        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text

    End Sub

    Private Sub btConfUsuarios_Click(sender As Object, e As EventArgs) Handles btConfUsuarios.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Usuarios
        frm._nameButton = btConfUsuarios.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text


    End Sub



    Private Sub Ventana_Click(sender As Object, e As EventArgs) Handles Ventana.Click
        SideNav1.IsMenuExpanded = False
    End Sub

    Private Sub SideNav1_IsMenuExpandedChanged(sender As Object, e As EventArgs) Handles SideNav1.IsMenuExpandedChanged
        If (SideNav1.IsMenuExpanded = True) Then
            FP_Configuracion.Select()

        End If
    End Sub

    Private Sub SideNavItem3_Click(sender As Object, e As EventArgs) Handles SideNavItem3.Click
        rmSesion.IsOpen = True
        rmSesion.MenuLocation = New Point(Me.Width / 2, Me.Height / 3)
        SideNav_Conf.Select()

    End Sub

    Private Sub rmSesion_MenuClosed(sender As Object, e As EventArgs) Handles rmSesion.MenuClosed
        FP_Configuracion.Select()

    End Sub

    Private Sub btConfCuenta_Click(sender As Object, e As EventArgs) Handles btConfCuenta.Click


        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Cuentas
        frm._nameButton = btConfCuenta.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text


    End Sub

    Private Sub btConfTipoCambio_Click(sender As Object, e As EventArgs) Handles btConfTipoCambio.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_TipoCambio
        frm._nameButton = btConfTipoCambio.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btConfAuxiliares_Click(sender As Object, e As EventArgs) Handles btConfAuxiliares.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Auxiliares
        frm._nameButton = btConfAuxiliares.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub superTabControl3_TabItemClose(sender As Object, e As SuperTabStripTabItemCloseEventArgs) Handles superTabControl3.TabItemClose
        'Dim cantidad As Integer = superTabControl3.Tabs.Count
        'If cantidad = 1 Then
        '    FP_Configuracion.Select()
        'End If

    End Sub

    Private Sub btTranComprobante_Click(sender As Object, e As EventArgs) Handles btTranComprobante.Click

        Try
            SideNav1.IsMenuExpanded = False
            Ventana.Select()
            Dim frm As New F0_Comprobante
            frm._nameButton = btTranComprobante.Name
            frm._modulo = FP_Transacciones
            Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
            frm._tab = tab3
            Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
            superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
            tab3.AttachedControl.Controls.Add(panel)
            frm.Show()
            tab3.Text = frm.Text
        Catch ex As Exception

        End Try



    End Sub

    Private Sub btTranRepEstadoCuentas_Click(sender As Object, e As EventArgs) Handles btTranRepEstadoCuentas.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_EstadoCuentasActivoPasivo
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepBalanceComproSumSaldos_Click(sender As Object, e As EventArgs) Handles btTranRepBalanceComproSumSaldos.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_BalanceComprobacionSumasSaldos
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepBalanceGeneral_Click(sender As Object, e As EventArgs) Handles btTranRepBalanceGeneral.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_BalanceGeneral
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepLibroDiario_Click(sender As Object, e As EventArgs) Handles btTranRepLibroDiario.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_LibroDiario
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepEstadoResultados_Click(sender As Object, e As EventArgs) Handles btTranRepEstadoResultados.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_EstadoResultados
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepEstadoCuentasResultados_Click(sender As Object, e As EventArgs) Handles btTranRepEstadoCuentasResultados.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_EstadoCuentasResultados
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btCarbArqueo_Click(sender As Object, e As EventArgs) Handles btCarbArqueo.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Arqueo
        frm._nameButton = btCarbArqueo.Name
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btCarbCliente_Click(sender As Object, e As EventArgs) Handles btCarbCliente.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_ClienteCarburantes
        frm._nameButton = btCarbCliente.Name
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text

    End Sub

    Private Sub btCarbMaquina_Click(sender As Object, e As EventArgs) Handles btCarbMaquina.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Maquina
        frm._nameButton = btCarbMaquina.Name
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btCarbRepResumen_Click(sender As Object, e As EventArgs) Handles btCarbRepResumen.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_ArqueoResumen
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btConfConfiguracion_Click(sender As Object, e As EventArgs) Handles btConfConfiguracion.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Parametros
        frm._nameButton = btConfConfiguracion.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btCarbPersonal_Click(sender As Object, e As EventArgs) Handles btCarbPersonal.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Personal
        frm._nameButton = btCarbPersonal.Name
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btConfConfCuentaAuto_Click(sender As Object, e As EventArgs) Handles btConfConfCuentaAuto.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_CuemtaAutomatica
        frm._nameButton = btConfConfCuentaAuto.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub MetroTileItem13_Click(sender As Object, e As EventArgs) Handles btConfProducto.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Productos
        frm._nameButton = btConfProducto.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btCarbCombustible_Click(sender As Object, e As EventArgs) Handles btCarbCombustible.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Combustible
        frm._nameButton = btCarbCombustible.Name
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btConfMovimiento_Click(sender As Object, e As EventArgs)
       
    End Sub

    Private Sub btConfDosificacion_Click(sender As Object, e As EventArgs) Handles btConfDosificacion.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Dosificacion
        frm._nameButton = btConfDosificacion.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentVentas_Click(sender As Object, e As EventArgs) Handles btVentVentas.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_ServicioVenta
        frm._nameButton = btVentVentas.Name
        frm._modulo = FP_Ventas
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub FP_Carburantes_Click(sender As Object, e As EventArgs) Handles FP_Carburantes.Click

    End Sub

    Private Sub btVentAnulfact_Click(sender As Object, e As EventArgs) Handles btVentAnulfact.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_AnularFactura
        frm._nameButton = btVentAnulfact.Name

        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentLibroVenta_Click(sender As Object, e As EventArgs) Handles btVentLibroVenta.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_LibroVenta
        frm._nameButton = btVentLibroVenta.Name
        frm._modulo = FP_Ventas
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btInvMovimientos_Click(sender As Object, e As EventArgs) Handles btInvMovimientos.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F00_Movimientos
        frm._nameButton = btInvMovimientos.Name
        frm._modulo = FP_Inventario
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btInvKardex_Click(sender As Object, e As EventArgs) Handles btInvKardex.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_KardexMovimiento
        frm._nameButton = btInvKardex.Name
        frm._modulo = FP_Inventario
        frm.pTitulo = "K A R D E X   D E   P R O D U C T O S"
        frm.pTipo = 2
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btInvSaldo_Click(sender As Object, e As EventArgs) Handles btInvSaldo.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_StockActualEquipoProducto
        frm.pTitulo = "S A L D O   A C T U A L   D E   P R O D U C T O S"
        frm.pTipo = 2
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = "S A L D O   A C T U A L   D E   P R O D U C T O S"
    End Sub

    Private Sub MetroTileItem15_Click(sender As Object, e As EventArgs) Handles btConfServicio.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Servicios
        frm._nameButton = btConfServicio.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub MetroTileItem15_Click_1(sender As Object, e As EventArgs) Handles btInvActivoFijo.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Activos
        frm._nameButton = btInvActivoFijo.Name
        frm._modulo = FP_Inventario
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btInvRegActivoFijo_Click(sender As Object, e As EventArgs) Handles btInvRegActivoFijo.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Reg_Activos
        frm._nameButton = btInvRegActivoFijo.Name
        frm._modulo = FP_Inventario
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranFacturas_Click(sender As Object, e As EventArgs) Handles btTranFacturas.Click

        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Facturas
        frm._nameButton = btTranFacturas.Name
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepLibroCompras_Click(sender As Object, e As EventArgs) Handles btTranRepLibroCompras.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_LibroCompra1
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentCierreCaja_Click(sender As Object, e As EventArgs) Handles btVentCierreCaja.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_AsientosContables
        frm._nameButton = btVentCierreCaja.Name
        frm._modulo = FP_Ventas
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btCuentaServicio_Click(sender As Object, e As EventArgs) Handles btCuentaServicio.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_ServicioCuentas
        frm._nameButton = btCuentaServicio.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentIntegArqueo_Click(sender As Object, e As EventArgs) Handles btVentIntegArqueo2.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        'Dim frm As New F1_ComprobanteArqueo
        'frm._nameButton = btVentIntegArqueo2.Name
        'frm._modulo = FP_Ventas
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        'frm.Show()
        'tab3.Text = frm.Text
    End Sub

    Private Sub btConfPlanCuentas_Click(sender As Object, e As EventArgs) Handles btConfPlanCuentas.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_PlanCuentas
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentRepVentas_Click(sender As Object, e As EventArgs) Handles btVentRepVentas.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_Ventas
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub


    Private Sub btConfEmpresa_Click(sender As Object, e As EventArgs) Handles btConfEmpresa.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Empresas
        frm._nameButton = btConfEmpresa.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btConfVarReport_Click(sender As Object, e As EventArgs) Handles btConfVarReport.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_VariablesReportes
        frm._nameButton = btConfVarReport.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepLibroMayor_Click(sender As Object, e As EventArgs) Handles btTranRepLibroMayor.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_LibroMayor
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub FP_Configuracion_Click(sender As Object, e As EventArgs) Handles FP_Configuracion.Click, FP_Transacciones.Click, FP_Carburantes.Click, FP_Ventas.Click, FP_Inventario.Click
        Dim moduloItem As SideNavItem = CType(sender, SideNavItem)
        Dim myVersion As Version = CType(moduloItem.Tag, Version)

        Dim dtModulo As DataTable = L_prModulosAll(myVersion.numiModulo)
        If dtModulo.Rows.Count > 0 Then
            If dtModulo.Rows(0).Item("yfver").trim.ToString.ToUpper <> myVersion.version.ToUpper Then '_version.Trim.ToUpper
                Dim info As New TaskDialogInfo("advertencia".ToUpper, eTaskDialogIcon.Exclamation, "modulo de sistema desactualizado".ToUpper, "no cuenta con la ultima version del sistema para este modulo.".ToUpper + vbCrLf + "ultima version: ".ToUpper + dtModulo.Rows(0).Item("yfver").trim.ToString.ToUpper + vbCrLf + "Por favor contactar al encargado de sistemas.".ToUpper, eTaskDialogButton.Yes, eTaskDialogBackgroundColor.Blue)
                Dim result As eTaskDialogResult = TaskDialog.Show(info)
            End If
        End If
    End Sub

    Private Sub btConfRepPresu_Click(sender As Object, e As EventArgs)
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_Presupuesto
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btTranRepEstadoCuentasResultadosV2_Click(sender As Object, e As EventArgs) Handles btTranRepEstadoCuentasResultadosV2.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_EstadoCuentasResultadosV2
        frm._modulo = FP_Transacciones
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentRepCierre_Click(sender As Object, e As EventArgs) Handles btVentRepCierre.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_CierreGeneral
        frm._modulo = FP_Ventas
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btDepreActivoFijo_Click(sender As Object, e As EventArgs) Handles btInvDepreActivoFijo.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_DepreActivoFijo
        frm._nameButton = btInvDepreActivoFijo.Name
        frm._modulo = FP_Inventario
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentIntegArqueo_Click_1(sender As Object, e As EventArgs) Handles btVentIntegArqueo.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_ComprobanteArqueo
        frm._nameButton = btVentIntegArqueo.Name
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentRepResumVent_Click(sender As Object, e As EventArgs) Handles btVentRepResumVent.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_ResumenVentas
        frm._modulo = FP_Ventas
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub MetroTileItem15_Click_2(sender As Object, e As EventArgs) Handles btConfPresupuesto.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Presupuesto
        frm._nameButton = btConfPresupuesto.Name
        frm._modulo = FP_Gerencia
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btConfRepPresu_Click_1(sender As Object, e As EventArgs) Handles btConfRepPresu.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_Presupuesto
        frm._modulo = FP_Gerencia
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btVentRepVentasDetallado_Click(sender As Object, e As EventArgs) Handles btVentRepVentasDetallado.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_ResumenVentasDetallado
        frm._modulo = FP_Ventas
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btCarbArqueoDiario_Click(sender As Object, e As EventArgs) Handles btCarbArqueoDiario.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_ArqueoDiario
        frm._modulo = FP_Carburantes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub

    Private Sub btInvDepreResumen_Click(sender As Object, e As EventArgs) Handles btInvDepreResumen.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New PR_DepresiacionResumen
        frm._modulo = FP_Inventario
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
    End Sub
End Class