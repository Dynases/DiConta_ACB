Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX

Public Class F1_Productos
#Region "Variables locales"

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Public Limpiar As Boolean = False  'Bandera para indicar si limpiar todos los datos o mantener datos ya registrados
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        Me.Text = "PRODUCTOS"
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prMaxLength()
        _prCargarNameLabel()
        _prCargarComboLibreria(cbgrupo1, 7, 1)
        _prCargarComboLibreria(cbgrupo2, 7, 2)
        _prCargarComboLibreria(cbgrupo3, 7, 3)
        _prCargarComboLibreria(cbgrupo4, 7, 4)
        _prCargarComboLibreria(cbUMed, 7, 5)
        _prAsignarPermisos()
        _PMIniciarTodo()
        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
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

    Public Sub _prCargarNameLabel()
        Dim dt As DataTable = L_fnProductoNameLabel()
        If (dt.Rows.Count > 0) Then
            lbgrupo1.Text = dt.Rows(0).Item("Grupo 1").ToString + ":"
            lbgrupo2.Text = dt.Rows(0).Item("Grupo 2").ToString + ":"
            lbgrupo3.Text = dt.Rows(0).Item("Grupo 3").ToString + ":"
            lbgrupo4.Text = dt.Rows(0).Item("Grupo 4").ToString + ":"

        End If
    End Sub
    Public Sub _prMaxLength()
        tbCodProd.MaxLength = 10
        tbDescPro.MaxLength = 50
        tbDescCort.MaxLength = 15
        cbgrupo1.MaxLength = 40
        cbgrupo2.MaxLength = 40
        cbgrupo3.MaxLength = 40
        cbgrupo4.MaxLength = 40
        cbUMed.MaxLength = 10
    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prProductoLibreriaGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cnnum").Width = 70
            .DropDownList.Columns("cnnum").Caption = "COD"
            .DropDownList.Columns.Add("cndesc1").Width = 200
            .DropDownList.Columns("cndesc1").Caption = "DESCRIPCION"
            .ValueMember = "cnnum"
            .DisplayMember = "cndesc1"
            .DataSource = dt
            .Refresh()
        End With
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
    Private Sub _prCrearCarpetaImagenes()
        Dim rutaDestino As String = RutaGlobal + "\Imagenes\Imagenes ContaProducto\"

        If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ContaProducto\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Imagenes") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes")
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ContaProducto") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ContaProducto")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ContaProducto") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ContaProducto")

                End If
            End If
        End If
    End Sub

    Private Sub _fnMoverImagenRuta(Folder As String, name As String)
        'copio la imagen en la carpeta del sistema
        If (Not name.Equals("Default.jpg") And File.Exists(RutaTemporal + name)) Then

            Dim img As New Bitmap(New Bitmap(RutaTemporal + name), 500, 300)

            UsImg.pbImage.Image.Dispose()
            UsImg.pbImage.Image = Nothing
            Try
                My.Computer.FileSystem.CopyFile(RutaTemporal + name,
     Folder + name, overwrite:=True)

            Catch ex As System.IO.IOException


            End Try



        End If
    End Sub
    Public Function _fnActionNuevo() As Boolean
        Return tbCodigo.Text = String.Empty And tbDescPro.ReadOnly = False
    End Function
    Private Function _fnCopiarImagenRutaDefinida() As String
        'copio la imagen en la carpeta del sistema

        Dim file As New OpenFileDialog()
        file.Filter = "Ficheros JPG o JPEG o PNG|*.jpg;*.jpeg;*.png" & _
                      "|Ficheros GIF|*.gif" & _
                      "|Ficheros BMP|*.bmp" & _
                      "|Ficheros PNG|*.png" & _
                      "|Ficheros TIFF|*.tif"
        If file.ShowDialog() = DialogResult.OK Then
            Dim ruta As String = file.FileName


            If file.CheckFileExists = True Then
                Dim img As New Bitmap(New Bitmap(ruta))
                Dim imgM As New Bitmap(New Bitmap(ruta))
                Dim Bin As New MemoryStream
                imgM.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim a As Object = file.GetType.ToString
                Dim da As String = Str(Now.Day).Trim + Str(Now.Month).Trim + Str(Now.Year).Trim + Str(Now.Hour) + Str(Now.Minute) + Str(Now.Second)
                If (_fnActionNuevo()) Then

                    Dim mayor As Integer
                    mayor = JGrM_Buscador.GetTotal(JGrM_Buscador.RootTable.Columns("cinumi"), AggregateFunction.Max)
                    nameImg = "\Imagen_" + da + ".jpg"
                    UsImg.pbImage.SizeMode = PictureBoxSizeMode.StretchImage
                    UsImg.pbImage.Image = Image.FromStream(Bin)

                    img.Save(RutaTemporal + nameImg, System.Drawing.Imaging.ImageFormat.Jpeg)
                    img.Dispose()
                Else
                    nameImg = "\Imagen_" + da + ".jpg"
                    UsImg.pbImage.Image = Image.FromStream(Bin)
                    img.Save(RutaTemporal + nameImg, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Modificado = True
                    img.Dispose()

                End If
            End If

            Return nameImg
        End If

        Return "default.jpg"
    End Function


#End Region
#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()
        tbCodProd.ReadOnly = False
        tbDescPro.ReadOnly = False
        tbDescCort.ReadOnly = False
        cbgrupo1.ReadOnly = False
        cbgrupo2.ReadOnly = False
        cbgrupo3.ReadOnly = False
        cbgrupo4.ReadOnly = False
        cbUMed.ReadOnly = False
        swEstado.IsReadOnly = False
        _prCrearCarpetaImagenes()
        _prCrearCarpetaTemporal()
        BtAdicionar.Visible = True
        btnImprimir.Visible = False
        tbStockMinimo.IsInputReadOnly = False
        tbprecio.IsInputReadOnly = False
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbCodigo.ReadOnly = True
        tbCodProd.ReadOnly = True
        tbDescPro.ReadOnly = True
        tbDescCort.ReadOnly = True
        cbgrupo1.ReadOnly = True
        cbgrupo2.ReadOnly = True
        cbgrupo3.ReadOnly = True
        cbgrupo4.ReadOnly = True
        cbUMed.ReadOnly = True
        swEstado.IsReadOnly = True
        BtAdicionar.Visible = False
        JGrM_Buscador.Focus()
        Limpiar = False
        btnImprimir.Visible = True
        tbStockMinimo.IsInputReadOnly = True
        tbprecio.IsInputReadOnly = True
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbCodigo.Clear()
        tbCodProd.Clear()
        tbDescPro.Clear()
        tbDescCort.Clear()
        tbStockMinimo.Value = 0
        tbprecio.Value = 0
        If (Limpiar = False) Then
            _prSeleccionarCombo(cbgrupo1)
            _prSeleccionarCombo(cbgrupo2)
            _prSeleccionarCombo(cbgrupo3)
            _prSeleccionarCombo(cbgrupo4)
            _prSeleccionarCombo(cbUMed)
            swEstado.Value = True
        End If
        tbCodProd.Focus()
        UsImg.pbImage.Image = My.Resources.pantalla


    End Sub
    Public Sub _prSeleccionarCombo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        Else
            mCombo.SelectedIndex = -1
        End If
    End Sub


    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbDescPro.BackColor = Color.White
        tbDescCort.BackColor = Color.White
        cbgrupo1.BackColor = Color.White
        cbgrupo2.BackColor = Color.White
        cbgrupo3.BackColor = Color.White
        cbgrupo4.BackColor = Color.White
        tbprecio.BackColor = Color.White

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        'ByRef _yfnumi As String, _yfcprod As String,
        '                                      _yfcbarra As String, _yfcdprod1 As String,
        '                                      _yfcdprod2 As String, _yfgr1 As Integer, _yfgr2 As Integer,
        '                                      _yfgr3 As Integer, _yfgr4 As Integer, _yfMed As Integer, _yfumin As Integer,
        '_yfusup As Integer, _yfvsup As Double, _yfsmin As Integer, _yfap As Integer, _yfimg As String


        Dim res As Boolean = L_prProductosGrabar(tbCodigo.Text, tbCodProd.Text, 0, tbDescPro.Text, tbDescCort.Text, cbgrupo1.Value, cbgrupo2.Value, cbgrupo3.Value, cbgrupo4.Value, cbUMed.Value, 1, 1, 1, IIf(IsDBNull(tbStockMinimo.Value), 0, tbStockMinimo.Value), IIf(swEstado.Value = True, 1, 0), nameImg, tbprecio.Value)


        If res Then
            Modificado = False
            _fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes ContaProducto", nameImg)
            nameImg = "Default.jpg"

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Producto ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            tbCodigo.Focus()
            Limpiar = True
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El producto no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean

        Dim nameImage As String = JGrM_Buscador.GetValue("ciimg")
        If (Modificado = False) Then
            res = L_prProductosModificar(tbCodigo.Text, tbCodProd.Text, 0, tbDescPro.Text, tbDescCort.Text, cbgrupo1.Value, cbgrupo2.Value, cbgrupo3.Value, cbgrupo4.Value, cbUMed.Value, 1, 1, 1, IIf(IsDBNull(tbStockMinimo.Value), 0, tbStockMinimo.Value), IIf(swEstado.Value = True, 1, 0), nameImage, tbprecio.Value)
        Else
            res = L_prProductosModificar(tbCodigo.Text, tbCodProd.Text, 0, tbDescPro.Text, tbDescCort.Text, cbgrupo1.Value, cbgrupo2.Value, cbgrupo3.Value, cbgrupo4.Value, cbUMed.Value, 1, 1, 1, IIf(IsDBNull(tbStockMinimo.Value), 0, tbStockMinimo.Value), IIf(swEstado.Value = True, 1, 0), nameImg, tbprecio.Value)
        End If
        If res Then

            If (Modificado = True) Then
                _fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes ContaProducto", nameImg)
                Modificado = False
            End If
            nameImg = "Default.jpg"

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Producto ".ToUpper + tbCodigo.Text + " modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "EL producto no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        _PMInhabilitar()
        _PMPrimerRegistro()
        Return res
    End Function


    Public Sub _PrEliminarImage()

        If (Not (_fnActionNuevo()) And (File.Exists(RutaGlobal + "\Imagenes\Imagenes ContaProducto\Imagen_" + tbCodigo.Text + ".jpg"))) Then
            UsImg.pbImage.Image.Dispose()
            UsImg.pbImage.Image = Nothing
            Try
                My.Computer.FileSystem.DeleteFile(RutaGlobal + "\Imagenes\Imagenes ContaProducto\Imagen_" + tbCodigo.Text + ".jpg")
            Catch ex As Exception

            End Try


        End If
    End Sub


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
            Dim res As Boolean = L_prProductosBorrar(tbCodigo.Text, tbCodProd.Text, 0, tbDescPro.Text, tbDescCort.Text, cbgrupo1.Value, cbgrupo2.Value, cbgrupo3.Value, cbgrupo4.Value, cbUMed.Value, 1, 1, 1, IIf(IsDBNull(tbStockMinimo.Value), 0, tbStockMinimo.Value), IIf(swEstado.Value = True, 1, 0), nameImg, tbprecio.Value, mensajeError)
            If res Then
                _PrEliminarImage()

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Producto ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

        If tbDescPro.Text = String.Empty Then
            tbDescPro.BackColor = Color.Red

            MEP.SetError(tbDescPro, "ingrese el descripcion del producto!".ToUpper)
            _ok = False
        Else
            tbDescPro.BackColor = Color.White
            MEP.SetError(tbDescPro, "")
        End If
        If tbDescCort.Text = String.Empty Then
            tbDescCort.BackColor = Color.Red
            MEP.SetError(tbDescCort, "ingrese la Descripcion Corta del Producto!".ToUpper)

            _ok = False
        Else
            tbDescCort.BackColor = Color.White
            MEP.SetError(tbDescCort, "")
        End If

        If cbgrupo1.SelectedIndex < 0 Then
            cbgrupo1.BackColor = Color.Red
            MEP.SetError(cbgrupo1, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo1.BackColor = Color.White
            MEP.SetError(cbgrupo1, "")
        End If

        If cbgrupo2.SelectedIndex < 0 Then
            cbgrupo2.BackColor = Color.Red
            MEP.SetError(cbgrupo2, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo2.BackColor = Color.White
            MEP.SetError(cbgrupo2, "")
        End If
        If cbgrupo3.SelectedIndex < 0 Then
            cbgrupo3.BackColor = Color.Red
            MEP.SetError(cbgrupo3, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo3.BackColor = Color.White
            MEP.SetError(cbgrupo3, "")
        End If
        If cbgrupo4.SelectedIndex < 0 Then
            cbgrupo4.BackColor = Color.Red
            MEP.SetError(cbgrupo4, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo4.BackColor = Color.White
            MEP.SetError(cbgrupo4, "")
        End If
        If cbUMed.SelectedIndex < 0 Then
            cbUMed.BackColor = Color.Red
            MEP.SetError(cbUMed, "Selecciones Unidad De Medida Del Producto!".ToUpper)
            _ok = False
        Else
            cbUMed.BackColor = Color.White
            MEP.SetError(cbUMed, "")
        End If
        If tbprecio.ToString.Length <= 0 Then
            tbprecio.BackColor = Color.Red
            MEP.SetError(tbprecio, "INSERTE UN PRECIO VALIDO!".ToUpper)
            _ok = False
        Else
            tbprecio.BackColor = Color.White
            MEP.SetError(tbprecio, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prProductoGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)
        '     a.cinumi ,a.cicprod ,a.cicbarra ,a.cicdprod1,a.cicdprod2 ,a.cigr1,gr1.cndesc1  as grupo1,a.cigr2
        ',gr2.cndesc1 as grupo2 ,a.cigr3,gr3.cndesc1 as grupo3,a.cigr4 ,gr4 .cndesc1 as grupo4,
        'a.ciMed ,Umed .cndesc1 as Umedida,a.ciumin ,Umin .cndesc1 as UnidMin,a.ciusup ,Usup .cndesc1 as Umax
        ',a.civsup ,a.cimstk ,a.ciclot 
        ',a.cismin ,cast(a.ciap as bit) as ciap,a.ciimg 
        listEstCeldas.Add(New Modelos.Celda("cinumi", True, "Código".ToUpper, 80))
        listEstCeldas.Add(New Modelos.Celda("cicprod", False))
        listEstCeldas.Add(New Modelos.Celda("cicbarra", False))
        listEstCeldas.Add(New Modelos.Celda("cicdprod1", True, "Descripcion Producto".ToUpper, 250))
        listEstCeldas.Add(New Modelos.Celda("cicdprod2", True, "Descripcion Corta".ToUpper, 170))
        listEstCeldas.Add(New Modelos.Celda("cigr1", False))
        listEstCeldas.Add(New Modelos.Celda("cigr2", False))
        listEstCeldas.Add(New Modelos.Celda("cigr3", False))
        listEstCeldas.Add(New Modelos.Celda("cigr4", False))
        listEstCeldas.Add(New Modelos.Celda("ciMed", False))
        listEstCeldas.Add(New Modelos.Celda("ciumin", False))
        listEstCeldas.Add(New Modelos.Celda("ciusup", False))
        listEstCeldas.Add(New Modelos.Celda("civsup", False))
        listEstCeldas.Add(New Modelos.Celda("cimstk", False))
        listEstCeldas.Add(New Modelos.Celda("ciclot", False))
        listEstCeldas.Add(New Modelos.Celda("cismin", False))
        listEstCeldas.Add(New Modelos.Celda("ciap", False))
        listEstCeldas.Add(New Modelos.Celda("ciimg", False))
        listEstCeldas.Add(New Modelos.Celda("ciprecio", True, "PRECIO", 100))
        'listEstCeldas.Add(New Modelos.Celda("cifact", False))
        'listEstCeldas.Add(New Modelos.Celda("cihact", False))
        'listEstCeldas.Add(New Modelos.Celda("ciuact", False))
        listEstCeldas.Add(New Modelos.Celda("grupo1", True, lbgrupo1.Text.Substring(0, lbgrupo1.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelos.Celda("grupo2", False, lbgrupo2.Text.Substring(0, lbgrupo2.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelos.Celda("grupo3", False, lbgrupo3.Text.Substring(0, lbgrupo3.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelos.Celda("grupo4", False, lbgrupo4.Text.Substring(0, lbgrupo4.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelos.Celda("Umedida", True, "UMedida".ToUpper, 150))
        listEstCeldas.Add(New Modelos.Celda("UnidMin", False, "UniVenta".ToUpper, 150))
        listEstCeldas.Add(New Modelos.Celda("Umax", False, "UniMaxima".ToUpper, 150))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos
        '     a.cinumi ,a.cicprod ,a.cicbarra ,a.cicdprod1,a.cicdprod2 ,a.cigr1,gr1.cndesc1  as grupo1,a.cigr2
        ',gr2.cndesc1 as grupo2 ,a.cigr3,gr3.cndesc1 as grupo3,a.cigr4 ,gr4 .cndesc1 as grupo4,
        'a.ciMed ,Umed .cndesc1 as Umedida,a.ciumin ,Umin .cndesc1 as UnidMin,a.ciusup ,Usup .cndesc1 as Umax
        ',a.civsup ,a.cimstk ,a.ciclot 
        ',a.cismin ,cast(a.ciap as bit) as ciap,a.ciimg 
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodigo.Text = JGrM_Buscador.GetValue("cinumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            tbCodigo.Text = .GetValue("cinumi").ToString
            tbCodProd.Text = .GetValue("cicprod").ToString
            tbDescPro.Text = .GetValue("cicdprod1").ToString
            tbDescCort.Text = .GetValue("cicdprod2").ToString
            cbgrupo1.Value = .GetValue("cigr1")
            cbgrupo2.Value = .GetValue("cigr2")
            cbgrupo3.Value = .GetValue("cigr3")
            cbgrupo4.Value = .GetValue("cigr4")
            cbUMed.Value = .GetValue("ciMed")
            swEstado.Value = .GetValue("ciap")
            tbprecio.Value = IIf(IsDBNull(.GetValue("ciprecio")), 0, .GetValue("ciprecio"))
            '' tbStockMinimo .Value =
            'lbFecha.Text = CType(.GetValue("cifact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("cihact").ToString
            'lbUsuario.Text = .GetValue("ciuact").ToString

        End With
        Dim name As String = JGrM_Buscador.GetValue("ciimg")
        If name.Equals("Default.jpg") Or Not File.Exists(RutaGlobal + "\Imagenes\Imagenes ContaProducto" + name) Then

            Dim im As New Bitmap(My.Resources.pantalla)
            UsImg.pbImage.Image = im
        Else
            If (File.Exists(RutaGlobal + "\Imagenes\Imagenes ContaProducto" + name)) Then
                Dim Bin As New MemoryStream
                Dim im As New Bitmap(New Bitmap(RutaGlobal + "\Imagenes\Imagenes ContaProducto" + name))
                im.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg)
                UsImg.pbImage.SizeMode = PictureBoxSizeMode.StretchImage
                UsImg.pbImage.Image = Image.FromStream(Bin)
                Bin.Dispose()

            End If
        End If
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

#End Region

    Private Sub F1_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
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
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub BtAdicionar_Click(sender As Object, e As EventArgs) Handles BtAdicionar.Click
        _fnCopiarImagenRutaDefinida()
        btnGrabar.Focus()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub

    Private Sub cbgrupo1_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo1.ValueChanged
        If cbgrupo1.SelectedIndex < 0 And cbgrupo1.Text <> String.Empty Then
            btgrupo1.Visible = True
        Else
            btgrupo1.Visible = False
        End If
    End Sub

    Private Sub cbgrupo2_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo2.ValueChanged
        If cbgrupo2.SelectedIndex < 0 And cbgrupo2.Text <> String.Empty Then
            btgrupo2.Visible = True
        Else
            btgrupo2.Visible = False
        End If
    End Sub

    Private Sub cbgrupo3_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo3.ValueChanged
        If cbgrupo3.SelectedIndex < 0 And cbgrupo3.Text <> String.Empty Then
            btgrupo3.Visible = True
        Else
            btgrupo3.Visible = False
        End If
    End Sub

    Private Sub cbgrupo4_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo4.ValueChanged
        If cbgrupo4.SelectedIndex < 0 And cbgrupo4.Text <> String.Empty Then
            btgrupo4.Visible = True
        Else
            btgrupo4.Visible = False
        End If
    End Sub

    Private Sub cbUMed_ValueChanged(sender As Object, e As EventArgs) Handles cbUMed.ValueChanged
        If cbUMed.SelectedIndex < 0 And cbUMed.Text <> String.Empty Then
            btUMedida.Visible = True
        Else
            btUMedida.Visible = False
        End If
    End Sub

    Private Sub btgrupo1_Click(sender As Object, e As EventArgs) Handles btgrupo1.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "1", cbgrupo1.Text, "") Then
            _prCargarComboLibreria(cbgrupo1, "7", "1")
            cbgrupo1.SelectedIndex = CType(cbgrupo1.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btgrupo2_Click(sender As Object, e As EventArgs) Handles btgrupo2.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "2", cbgrupo2.Text, "") Then
            _prCargarComboLibreria(cbgrupo2, "7", "2")
            cbgrupo2.SelectedIndex = CType(cbgrupo2.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btgrupo3_Click(sender As Object, e As EventArgs) Handles btgrupo3.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "3", cbgrupo3.Text, "") Then
            _prCargarComboLibreria(cbgrupo3, "7", "3")
            cbgrupo3.SelectedIndex = CType(cbgrupo3.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btgrupo4_Click(sender As Object, e As EventArgs) Handles btgrupo4.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "4", cbgrupo4.Text, "") Then
            _prCargarComboLibreria(cbgrupo4, "7", "4")
            cbgrupo4.SelectedIndex = CType(cbgrupo4.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btUMedida_Click(sender As Object, e As EventArgs) Handles btUMedida.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "5", cbUMed.Text, "") Then
            _prCargarComboLibreria(cbUMed, "7", "5")
            cbUMed.SelectedIndex = CType(cbUMed.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub
End Class