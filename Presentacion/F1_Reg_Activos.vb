Option Strict Off
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX
Public Class F1_Reg_Activos

#Region "Variables locales"

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Dim Cod_TipoActivo As Integer = 0
    Dim Cod_Encargado As Integer = 0
    'Dim Cod_Sector As Integer = 0
    Public TablaImagenes As DataTable
    Dim Modificar As Boolean = False
    Dim posImg As Integer = 0
    Public Limpiar As Boolean = False  'Bandera para indicar si limpiar todos los datos o mantener datos ya registrados

    Private _numiAuxiliarDetalleModulo As Integer = 1
    Private _numiAuxiliarDetalleSucursal As Integer = 11
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        Me.Text = "REGISTRO DE ACTIVO FIJO"
        ''L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prMaxLength()
        _prAsignarPermisos()
        _PMIniciarTodo()

        _prCargarComboAuxiliaresSucursales(_numiAuxiliarDetalleSucursal)
        _prCargarComboAuxiliaresModulos(_numiAuxiliarDetalleModulo)

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
    Private Sub _prCargarComboAuxiliaresSucursales(numi As String)
        Dim dt As New DataTable
        dt = L_prAuxiliarDetalleGeneral(numi)

        With tbSucursal
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cdnumi").Width = 70
            .DropDownList.Columns("cdnumi").Caption = "COD"

            .DropDownList.Columns.Add("cddesc").Width = 200
            .DropDownList.Columns("cddesc").Caption = "DESCRIPCION"

            .ValueMember = "cdnumi"
            .DisplayMember = "cddesc"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prCargarComboAuxiliaresModulos(numi As String)
        Dim dt As New DataTable
        dt = L_prAuxiliarDetalleGeneral(numi)

        With tbModulo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cdnumi").Width = 70
            .DropDownList.Columns("cdnumi").Caption = "COD"

            .DropDownList.Columns.Add("cddesc").Width = 200
            .DropDownList.Columns("cddesc").Caption = "DESCRIPCION"

            .ValueMember = "cdnumi"
            .DisplayMember = "cddesc"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Public Sub _prMaxLength()
        tbGlosa.MaxLength = 200

    End Sub
    Private Sub _prCrearCarpetaTemporal()

        If System.IO.Directory.Exists(RutaTemporal) = False Then
            System.IO.Directory.CreateDirectory(RutaTemporal)
        Else
            Try
                My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.CreateDirectory(RutaTemporal)
                'My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
                'System.IO.Directory.CreateDirectory(RutaTemporal)

            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Sub _prEliminarCarpeta(Ruta As String)

        If System.IO.Directory.Exists(Ruta) = True Then


            Try

                My.Computer.FileSystem.DeleteDirectory(Ruta, FileIO.DeleteDirectoryOption.DeleteAllContents)

            Catch ex As Exception

            End Try

        End If

    End Sub
    Private Sub _prCrearCarpetaImagenes()
        Dim rutaDestino As String = RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\"

        If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Imagenes") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes")
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo")

                End If
            End If
        End If
    End Sub
    Private Sub _prCrearCarpetaImagenes(carpetaFinal As String)
        Dim rutaDestino As String = RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + carpetaFinal + "\"

        If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + carpetaFinal) = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Imagenes") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes")
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo")
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + carpetaFinal + "\")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo")
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + carpetaFinal + "\")
                Else
                    If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + carpetaFinal) = False Then
                        System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + carpetaFinal + "\")
                    End If

                End If
            End If
        End If
    End Sub

    Public Function _fnActionNuevo() As Boolean
        Return tbCodigo.Text = String.Empty
    End Function

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            _modulo.Select()
            _tab.Close()
        End If
    End Sub
    Private Sub pbImg_MouseEnter(sender As Object, e As EventArgs)
        Dim pb As PictureBox = CType(sender, PictureBox)
        pbImgProdu.Image = pb.Image
        pbImgProdu.Tag = pb.Tag

    End Sub
    Private Sub pbImgProdu_MouseClick(sender As Object, e As MouseEventArgs) Handles pbImgProdu.MouseClick
        pbImgProdu.Size = New System.Drawing.Size(pbImgProdu.Width + e.Delta / 1, pbImgProdu.Height + e.Delta / 1)
        pbImgProdu.Location = New Point(Control.MousePosition.X - pbImgProdu.Width / 2, Control.MousePosition.Y - pbImgProdu.Height / 2)

    End Sub
    Private Sub btDeleteImg_Click(sender As Object, e As EventArgs) Handles btDeleteImg.Click
        Dim pos As Integer = CType(pbImgProdu.Tag, Integer)

        If (pos >= 0) Then
            TablaImagenes.Rows(pos).Item("estado") = -1
            _prCargarImagen()
        End If

    End Sub
    Public Sub _prCargarImagen()
        panelA.Controls.Clear()

        pbImgProdu.Image = Nothing
        posImg = -1

        Dim i As Integer = 0
        For Each fila As DataRow In TablaImagenes.Rows
            Dim elemImg As UCItemImg = New UCItemImg
            Dim rutImg = fila.Item("ieimg").ToString
            Dim estado As Integer = fila.Item("estado")

            If (estado = 0) Then
                elemImg.pbJpg.SizeMode = PictureBoxSizeMode.StretchImage
                Dim bm As Bitmap = Nothing
                Dim by As Byte() = fila.Item("img")
                Dim ms As New MemoryStream(by)
                bm = New Bitmap(ms)


                elemImg.pbJpg.Image = bm

                pbImgProdu.SizeMode = PictureBoxSizeMode.StretchImage
                pbImgProdu.Image = bm
                elemImg.pbJpg.Tag = i
                elemImg.Dock = DockStyle.Top
                pbImgProdu.Tag = i
                AddHandler elemImg.pbJpg.MouseEnter, AddressOf pbImg_MouseEnter
                elemImg.Height = panelA.Height / 3
                panelA.Controls.Add(elemImg)
            Else
                If (estado = 1) Then
                    If (File.Exists(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + "Activo_" + tbCodigo.Text + rutImg)) Then
                        Dim bm As Bitmap = New Bitmap(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + "Activo_" + tbCodigo.Text + rutImg)
                        elemImg.pbJpg.SizeMode = PictureBoxSizeMode.StretchImage
                        elemImg.pbJpg.Image = bm
                        pbImgProdu.SizeMode = PictureBoxSizeMode.StretchImage
                        pbImgProdu.Image = bm
                        elemImg.pbJpg.Tag = i
                        elemImg.Dock = DockStyle.Top
                        pbImgProdu.Tag = i
                        AddHandler elemImg.pbJpg.MouseEnter, AddressOf pbImg_MouseEnter
                        elemImg.Height = panelA.Height / 3
                        panelA.Controls.Add(elemImg)
                    End If

                End If
            End If




            i += 1
        Next
        If (Modificar = True) Then
            Dim elemImgAdd As UCItemImg = New UCItemImg

            Dim imgadd As Bitmap = New Bitmap(My.Resources.addimage)
            elemImgAdd.pbJpg.SizeMode = PictureBoxSizeMode.StretchImage
            elemImgAdd.pbJpg.Image = imgadd
            elemImgAdd.Dock = DockStyle.Top
            AddHandler elemImgAdd.pbJpg.Click, AddressOf pbJpg_MouseClick
            elemImgAdd.Height = panelA.Height / 3
            panelA.Controls.Add(elemImgAdd)
        End If
        If (Modificar = True And _fnObtenerNumeroFilasActivas() < 0) Then
            btDeleteImg.Visible = False
        Else
            If (Modificar = True) Then
                btDeleteImg.Visible = True
            End If


        End If
    End Sub
    Public Function _fnObtenerNumeroFilasActivas() As Integer
        Dim n As Integer = -1
        For i As Integer = 0 To TablaImagenes.Rows.Count - 1 Step 1
            Dim estado As Integer = TablaImagenes.Rows(i).Item("estado")
            If (estado = 0 Or estado = 1) Then
                n += 1

            End If
        Next
        Return n
    End Function

    Private Sub pbJpg_MouseClick(sender As Object, e As EventArgs)

        _fnCopiarImagenRutaDefinida()
        _prCargarImagen()

    End Sub
    Private Function _fnCopiarImagenRutaDefinida() As String
        'copio la imagen en la carpeta del sistema

        Dim file As New OpenFileDialog()
        file.Filter = "Ficheros JPG o JPEG o PNG|*.jpg;*.jpeg;*.png" &
                      "|Ficheros GIF|*.gif" &
                      "|Ficheros BMP|*.bmp" &
                      "|Ficheros PNG|*.png" &
                      "|Ficheros TIFF|*.tif"
        If file.ShowDialog() = DialogResult.OK Then
            Dim ruta As String = file.FileName
            Dim nombre As String = ""

            If file.CheckFileExists = True Then
                Dim img As New Bitmap(New Bitmap(ruta))
                Dim a As Object = file.GetType.ToString

                Dim da As String = Str(Now.Day).Trim + Str(Now.Month).Trim + Str(Now.Year).Trim + Str(Now.Hour) + Str(Now.Minute) + Str(Now.Second)

                nombre = "\Imagen_" + da + ".jpg".Trim


                If (_fnActionNuevo()) Then
                    Dim mstream = New MemoryStream()

                    img.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

                    TablaImagenes.Rows.Add(0, 0, nombre, mstream.ToArray(), 0)
                    mstream.Dispose()

                Else
                    Dim mstream = New MemoryStream()

                    img.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

                    'a.ienumi , a.ienumiti4, a.ieimg, Cast(null As image) As img, 1 as estado
                    TablaImagenes.Rows.Add(0, tbCodigo.Text, nombre, mstream.ToArray(), 0)
                    mstream.Dispose()

                End If

                'img.Save(RutaTemporal + nombre, System.Drawing.Imaging.ImageFormat.Jpeg)




            End If
            Return nombre
        End If

        Return "default.jpg"
    End Function

    Public Sub _prGuardarImagenes(_ruta As String)
        panelA.Controls.Clear()


        For i As Integer = 0 To TablaImagenes.Rows.Count - 1 Step 1
            Dim estado As Integer = TablaImagenes.Rows(i).Item("estado")
            If (estado = 0) Then

                Dim bm As Bitmap = Nothing
                Dim by As Byte() = TablaImagenes.Rows(i).Item("img")
                Dim ms As New MemoryStream(by)
                bm = New Bitmap(ms)
                Try
                    bm.Save(_ruta + TablaImagenes.Rows(i).Item("ieimg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                Catch ex As Exception

                End Try




            End If
            If (estado = -1) Then
                Try
                    Me.pbImgProdu.Image.Dispose()
                    Me.pbImgProdu.Image = Nothing
                    Application.DoEvents()
                    TablaImagenes.Rows(i).Item("img") = Nothing



                    If (File.Exists(_ruta + TablaImagenes.Rows(i).Item("ieimg"))) Then
                        My.Computer.FileSystem.DeleteFile(_ruta + TablaImagenes.Rows(i).Item("ieimg"))
                    End If

                Catch ex As Exception

                End Try
            End If
        Next
    End Sub
#End Region
#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()
        swDepreciable.IsReadOnly = False
        tbDepreciasion.IsInputReadOnly = False
        tbValorI.IsInputReadOnly = False
        tbGlosa.ReadOnly = False
        _prCrearCarpetaImagenes()
        _prCrearCarpetaTemporal()
        btnImprimir.Visible = False
        Modificar = True
        btnActivo.Visible = True
        btnEncargado.Visible = True
        'btnSector.Visible = True

        tbModulo.ReadOnly = False
        tbDeducible.IsReadOnly = False
        tbDepreAcum.IsInputReadOnly = False
        tbSucursal.ReadOnly = False
        tbVidaUtilActual.IsInputReadOnly = False


        _prCargarImagen()
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        swDepreciable.IsReadOnly = True
        tbDepreciasion.IsInputReadOnly = True
        tbValorI.IsInputReadOnly = True
        tbGlosa.ReadOnly = True
        tbTipoActivo.ReadOnly = True
        tbEncargado.ReadOnly = True
        tbModulo.ReadOnly = True
        tbVidaUtil.IsInputReadOnly = True
        JGrM_Buscador.Focus()
        Limpiar = False
        btnImprimir.Visible = True
        Modificar = False
        btnActivo.Visible = False
        btnEncargado.Visible = False

        tbDeducible.IsReadOnly = True
        tbDepreAcum.IsInputReadOnly = True
        tbSucursal.ReadOnly = True
        tbVidaUtilActual.IsInputReadOnly = True
        'btnSector.Visible = False
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbCodigo.Clear()
        tbDepreciasion.Value = 0
        tbValorI.Value = 0
        tbGlosa.Clear()
        tbTipoActivo.Clear()
        tbEncargado.Clear()
        tbModulo.Clear()
        Cod_Encargado = 0
        'Cod_Sector = 0
        Cod_TipoActivo = 0
        Modificar = True
        tbVidaUtil.Value = 0
        swDepreciable.Value = True
        TablaImagenes = L_prCargarImagenes(-1)
        _prCargarImagen()
        tbTipoActivo.Focus()

        tbDeducible.Value = True
        tbDepreAcum.Value = 0
        tbSucursal.SelectedIndex = -1
        tbVidaUtilActual.Value = 0
    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbDepreciasion.BackColor = Color.White
        tbValorI.BackColor = Color.White
        tbGlosa.BackColor = Color.White
        tbTipoActivo.BackColor = Color.White
        tbEncargado.BackColor = Color.White
        tbModulo.BackColor = Color.White



    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        'ByRef _idnumi As String, _idnumiti3 As Integer, _idglosa As String, _idvalori As Double, _idfechac As String,
        '                                                 _idfechau As String,
        '                                                 _idfactdepmes As Double,
        '                                                 _idfactdepanual As Double,
        '                                                 _idsector As Integer, _idencargado As Integer, _idvalact As Double, _detalle As DataTable

        Dim res As Boolean = L_prRegistroActivoFijoGrabar(tbCodigo.Text, Cod_TipoActivo, tbGlosa.Text, tbValorI.Value, tbFechaCompra.Value.ToString("yyyy/MM/dd"), tbFechaUso.Value.ToString("yyyy/MM/dd"), 0, tbDepreciasion.Value, tbModulo.Value, Cod_Encargado, 0, TablaImagenes, IIf(swDepreciable.Value = True, 1, 0), tbDepreAcum.Value, tbVidaUtilActual.Value, tbSucursal.Value, IIf(tbDeducible.Value = True, 1, 0), gi_empresaNumi)


        If res Then
            Modificado = False
            _prCrearCarpetaImagenes("Activo_" + tbCodigo.Text.Trim)
            nameImg = "Default.jpg"
            _prGuardarImagenes(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + "Activo_" + tbCodigo.Text.Trim + "\")
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de ACTIVO FIJO ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            tbCodigo.Focus()
            Limpiar = True
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El ACTIVO FIJO no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean

        ''Dim nameImage As String = JGrM_Buscador.GetValue("idimg")

        res = L_prRegistroActivoFijoModificar(tbCodigo.Text, Cod_TipoActivo, tbGlosa.Text, tbValorI.Value, tbFechaCompra.Value.ToString("yyyy/MM/dd"), tbFechaUso.Value.ToString("yyyy/MM/dd"), 0, tbDepreciasion.Value, tbModulo.Value, Cod_Encargado, 0, TablaImagenes, IIf(swDepreciable.Value = True, 1, 0), tbDepreAcum.Value, tbVidaUtilActual.Value, tbSucursal.Value, IIf(tbDeducible.Value = True, 1, 0), gi_empresaNumi)

        If res Then


            _prCrearCarpetaImagenes("Activo_" + tbCodigo.Text.Trim)

            _prGuardarImagenes(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + "Activo_" + tbCodigo.Text.Trim + "\")

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de ACTIVO FIJO ".ToUpper + tbCodigo.Text + " modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "EL Activo Fijo no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        _PMInhabilitar()
        _PMPrimerRegistro()
        Return res
    End Function




    Public Overrides Sub _PMOEliminarRegistro()

        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prRegistroActivoFijoBorrar(tbCodigo.Text, Cod_TipoActivo, tbGlosa.Text, tbValorI.Value, tbFechaCompra.Value.ToString("yyyy/MM/dd"), tbFechaUso.Value.ToString("yyyy/MM/dd"), 0, tbDepreciasion.Value, tbModulo.Value, Cod_Encargado, 0, mensajeError)
            If res Then

                _prEliminarCarpeta(RutaGlobal + "\Imagenes\Imagenes RegistroActivoFijo\" + "Activo_" + tbCodigo.Text.Trim)
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Activo Fijo ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _PMFiltrar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If


    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbGlosa.Text = String.Empty Then
            tbGlosa.BackColor = Color.Red

            MEP.SetError(tbGlosa, "ingrese la glosa por favor!".ToUpper)
            _ok = False
        Else
            tbGlosa.BackColor = Color.White
            MEP.SetError(tbGlosa, "")
        End If
        If tbTipoActivo.Text = String.Empty Then
            tbTipoActivo.BackColor = Color.Red
            MEP.SetError(tbTipoActivo, "Seleccione un tipo de activo!".ToUpper)

            _ok = False
        Else
            tbTipoActivo.BackColor = Color.White
            MEP.SetError(tbTipoActivo, "")
        End If
        If tbEncargado.Text = String.Empty Then
            tbEncargado.BackColor = Color.Red
            MEP.SetError(tbEncargado, "Seleccione encargado del activo!".ToUpper)

            _ok = False
        Else
            tbEncargado.BackColor = Color.White
            MEP.SetError(tbEncargado, "")
        End If
        If tbModulo.SelectedIndex < 0 Then
            tbModulo.BackColor = Color.Red
            MEP.SetError(tbModulo, "Seleccione un sector por favor!".ToUpper)

            _ok = False
        Else
            tbModulo.BackColor = Color.White
            MEP.SetError(tbModulo, "")
        End If

        If tbSucursal.SelectedIndex < 0 Then
            tbSucursal.BackColor = Color.Red
            MEP.SetError(tbSucursal, "Seleccione sucursal por favor!".ToUpper)

            _ok = False
        Else
            tbSucursal.BackColor = Color.White
            MEP.SetError(tbSucursal, "")
        End If

        If tbValorI.Text = String.Empty Then
            tbValorI.BackColor = Color.Red
            MEP.SetError(tbValorI, "Ingrese Valor Inicial del Activo Pro favor!".ToUpper)

            _ok = False
        Else
            tbValorI.BackColor = Color.White
            MEP.SetError(tbValorI, "")
        End If
        If tbDepreciasion.Text = String.Empty Then
            tbDepreciasion.BackColor = Color.Red
            MEP.SetError(tbDepreciasion, "Ingrese Valor de depreciacion del Activo Pro favor!".ToUpper)

            _ok = False
        Else
            tbDepreciasion.BackColor = Color.White
            MEP.SetError(tbDepreciasion, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prRegistroActivoFijoGeneral(gi_empresaNumi)
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)

        Dim listEstCeldas As New List(Of Modelos.Celda)
        '    a.idnumi , a.idnumiti3, a.idglosa, a.idvalori, a.idfechac, a.idfechau, a.idfactdepmes, a.idfactdepanual,
        'a.idsector, sector, a.idencargado,  personal , a.idvalact  ,TipoActivo,
        'TipoActivo.icvidautil as vidaUtil
        listEstCeldas.Add(New Modelos.Celda("idnumi", True, "Código".ToUpper, 100))
        listEstCeldas.Add(New Modelos.Celda("idnumiti3", False))
        listEstCeldas.Add(New Modelos.Celda("idglosa", True, "GLOSA".ToUpper, 250))
        listEstCeldas.Add(New Modelos.Celda("idvalori", True, "VALOR INICIAL".ToUpper, 120, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("idfechac", True, "Fecha Compra".ToUpper, 120, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelos.Celda("idfechau", True, "Fecha Uso".ToUpper, 120, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelos.Celda("idfactdepmes", False, "Depreciacion Mensual".ToUpper, 180, "0.0000"))

        listEstCeldas.Add(New Modelos.Celda("idfactdepanual", False, "Depreciacion Anual".ToUpper, 180, "0.0000"))
        listEstCeldas.Add(New Modelos.Celda("idsector", False))
        listEstCeldas.Add(New Modelos.Celda("sector", True, "Sector".ToUpper, 120))
        listEstCeldas.Add(New Modelos.Celda("idencargado", False))
        listEstCeldas.Add(New Modelos.Celda("personal", True, "PERSONAL E.", 120))
        listEstCeldas.Add(New Modelos.Celda("TipoActivo", True, "Tipo Activo", 120))
        listEstCeldas.Add(New Modelos.Celda("idvalact", False))
        listEstCeldas.Add(New Modelos.Celda("vidaUtil", False))
        listEstCeldas.Add(New Modelos.Celda("estado", False))

        listEstCeldas.Add(New Modelos.Celda("iddepacum", True, "DEPRECIACION ACUM".ToUpper, 120, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("idvidutact", True, "VIDA UTIL ACTUAL".ToUpper, 120, "0.00"))
        listEstCeldas.Add(New Modelos.Celda("idsuc", False))
        listEstCeldas.Add(New Modelos.Celda("idsuc2", True, "SUCURSAL".ToUpper, 150))
        listEstCeldas.Add(New Modelos.Celda("iddedu", True, "DEDUCIBLE".ToUpper, 70))
        listEstCeldas.Add(New Modelos.Celda("idemp", False))

        Return listEstCeldas

    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos
        '    a.idnumi , a.idnumiti3, a.idglosa, a.idvalori, a.idfechac, a.idfechau, a.idfactdepmes, a.idfactdepanual,
        'a.idsector, sector, a.idencargado,  personal , a.idvalact ,TipoActivo,
        'TipoActivo.icvidautil as vidaUtil
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodigo.Text = JGrM_Buscador.GetValue("idnumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            tbCodigo.Text = .GetValue("idnumi").ToString
            Cod_TipoActivo = .GetValue("idnumiti3")
            tbGlosa.Text = .GetValue("idglosa").ToString
            tbFechaCompra.Value = .GetValue("idfechac")
            tbFechaUso.Value = .GetValue("idfechau")
            tbDepreciasion.Value = .GetValue("idfactdepanual")
            tbModulo.Value = .GetValue("idsector")
            'tbSector.Text = .GetValue("sector")
            Cod_Encargado = .GetValue("idencargado")
            tbEncargado.Text = .GetValue("personal")
            tbTipoActivo.Text = .GetValue("TipoActivo")
            tbValorI.Value = .GetValue("idvalori")
            tbVidaUtil.Value = .GetValue("vidaUtil")
            TablaImagenes = L_prCargarImagenes(.GetValue("idnumi"))
            swDepreciable.Value = .GetValue("estado")

            tbDepreAcum.Value = IIf(IsDBNull(.GetValue("iddepacum")) = True, 0, .GetValue("iddepacum"))
            tbVidaUtilActual.Value = IIf(IsDBNull(.GetValue("idvidutact")) = True, 0, .GetValue("idvidutact"))
            tbSucursal.Value = IIf(IsDBNull(.GetValue("idsuc")) = True, -1, .GetValue("idsuc"))
            tbDeducible.Value = IIf(IsDBNull(.GetValue("iddedu")) = True, True, .GetValue("iddedu"))


            _prCargarImagen()
        End With
        
        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()

        'With MHighlighterFocus
        '    .SetHighlightOnFocus(tbCodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodProd, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbStockMinimo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodBarra, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        '    .SetHighlightOnFocus(tbDescPro, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbDescCort, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo2, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo3, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo4, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUMed, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(swEstado, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUniVenta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUnidMaxima, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbConversion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)


        'End With
    End Sub

    Private Sub F1_Activos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs)
        Dim pos As Integer = CType(pbImgProdu.Tag, Integer)

        If (pos >= 0) Then
            TablaImagenes.Rows(pos).Item("estado") = -1
            _prCargarImagen()
        End If
    End Sub

    Private Sub tbTipoActivo_KeyDown(sender As Object, e As KeyEventArgs) Handles tbTipoActivo.KeyDown

        If (tbGlosa.ReadOnly = False) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                _prListarTipoActivo()

            End If

        End If
    End Sub
    Public Sub _prListarTipoActivo()

        Dim dt As DataTable

        dt = L_prListarTipoActivos()
        'a.icnumi , a.icnom, a.icvidautil 

        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("icnumi", True, "CODIGO", 50))
        listEstCeldas.Add(New Modelos.Celda("icnom", True, "NOMBRE", 200))
        listEstCeldas.Add(New Modelos.Celda("icvidautil", True, "VIDA UTIL", 120, "0.00"))

        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione TIPO DE ACTIVOS".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            'a.icnumi , a.icnom, a.icvidautil 
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            Cod_TipoActivo = Row.Cells("icnumi").Value
            tbTipoActivo.Text = Row.Cells("icnom").Value
            tbVidaUtil.Value = Row.Cells("icvidautil").Value

        End If
    End Sub

    'Public Sub _prListarSectoActivos()

    '    Dim dt As DataTable

    '    dt = L_prListarSectorActivos()
    '    'B.cdnumi ,b.cddesc 

    '    Dim listEstCeldas As New List(Of Modelos.Celda)
    '    listEstCeldas.Add(New Modelos.Celda("cdnumi", True, "CODIGO", 50))
    '    listEstCeldas.Add(New Modelos.Celda("cddesc", True, "NOMBRE", 200))

    '    Dim ef = New Efecto
    '    ef.tipo = 3
    '    ef.dt = dt
    '    ef.SeleclCol = 2
    '    ef.listEstCeldas = listEstCeldas
    '    ef.alto = 50
    '    ef.ancho = 350
    '    ef.Context = "Seleccione un sector".ToUpper
    '    ef.ShowDialog()
    '    Dim bandera As Boolean = False
    '    bandera = ef.band
    '    If (bandera = True) Then
    '        'a.icnumi , a.icnom, a.icvidautil 
    '        Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
    '        Cod_Sector = Row.Cells("cdnumi").Value
    '        tbSector.Text = Row.Cells("cddesc").Value

    '    End If
    'End Sub

    Public Sub _prListarPersonalActivos()

        Dim dt As DataTable

        dt = L_prListarPersonalActivos()
        'a.panumi ,a.panom 

        Dim listEstCeldas As New List(Of Modelos.Celda)
        listEstCeldas.Add(New Modelos.Celda("panumi", True, "CODIGO", 50))
        listEstCeldas.Add(New Modelos.Celda("panom", True, "NOMBRE", 200))

        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione un personal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            'a.icnumi , a.icnom, a.icvidautil 
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            Cod_Encargado = Row.Cells("panumi").Value
            tbEncargado.Text = Row.Cells("panom").Value

        End If
    End Sub

    Private Sub btnAnadir_Click(sender As Object, e As EventArgs) Handles btnActivo.Click
        If (tbGlosa.ReadOnly = False) Then

            _prListarTipoActivo()



        End If
    End Sub

    'Private Sub tbSector_KeyDown(sender As Object, e As KeyEventArgs)
    '    If (tbGlosa.ReadOnly = False) Then
    '        If e.KeyData = Keys.Control + Keys.Enter Then

    '            _prListarSectoActivos()

    '        End If

    '    End If
    'End Sub

    'Private Sub ButtonX1_Click(sender As Object, e As EventArgs)


    '    If (tbGlosa.ReadOnly = False) Then


    '        _prListarSectoActivos()


    '    End If
    'End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles btnEncargado.Click

        If (tbGlosa.ReadOnly = False) Then


            _prListarPersonalActivos()



        End If
    End Sub

    Private Sub tbEncargado_KeyDown(sender As Object, e As KeyEventArgs) Handles tbEncargado.KeyDown
        If (tbGlosa.ReadOnly = False) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                _prListarPersonalActivos()

            End If

        End If
    End Sub

    Private Sub LabelX12_Click(sender As Object, e As EventArgs) Handles LabelX12.Click

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub



#End Region
End Class