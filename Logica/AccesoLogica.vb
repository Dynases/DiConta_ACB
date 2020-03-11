Option Strict Off
Imports System.Data
Imports System.Data.SqlClient
Imports Datos.AccesoDatos

Public Class AccesoLogica

    Public Shared L_Usuario As String = "DEFAULT"


#Region "METODOS PRIVADOS"
    Public Shared Sub L_prAbrirConexion(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")
        D_abrirConexion(Ip, UsuarioSql, ClaveSql, NombreBD)
    End Sub
    Public Shared Sub L_prAbrirConexionBitacora(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")
        D_abrirConexionHistorial(Ip, UsuarioSql, ClaveSql, NombreBD)
    End Sub

    Public Shared Function _fnsAuditoria() As String
        Return "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString("00") + ":" + Now.Minute.ToString("00") + "' ,'" + L_Usuario + "'"
    End Function
#End Region

#Region "LIBRERIAS"



    Public Shared Function L_prLibreriaDetalleGeneral(_cod1 As String, _cod2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@cncod1", _cod1))
        _listParam.Add(New Datos.DParametro("@cncod2", _cod2))
        _listParam.Add(New Datos.DParametro("@cnuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC0051", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prLibreriaGrabar(ByRef _numi As String, _cod1 As String, _cod2 As String, _desc1 As String, _desc2 As String) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cncod1", _cod1))
        _listParam.Add(New Datos.DParametro("@cncod2", _cod2))
        _listParam.Add(New Datos.DParametro("@cndesc1", _desc1))
        _listParam.Add(New Datos.DParametro("@cndesc2", _desc2))
        _listParam.Add(New Datos.DParametro("@cnuact", 1))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC0051", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function
#End Region

#Region "VALIDAR ELIMINACION"
    Public Shared Function L_fnbValidarEliminacion(_numi As String, _tablaOri As String, _campoOri As String, ByRef _respuesta As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        _Where = "bbtori='" + _tablaOri + "' and bbtran=1"
        _campos = "bbnumi,bbtran,bbtori,bbcori,bbtdes,bbcdes,bbprog"
        _Tabla = D_Datos_Tabla(_campos, "TB002", _Where)
        _respuesta = "no se puede eliminar el registro: ".ToUpper + _numi + " por que esta siendo usado en los siguientes programas: ".ToUpper + vbCrLf

        Dim result As Boolean = True
        For Each fila As DataRow In _Tabla.Rows
            If L_fnbExisteRegEnTabla(_numi, fila.Item("bbtdes").ToString, fila.Item("bbcdes").ToString) = True Then
                _respuesta = _respuesta + fila.Item("bbprog").ToString + vbCrLf
                result = False
            End If
        Next
        Return result
    End Function

    Private Shared Function L_fnbExisteRegEnTabla(_numiOri As String, _tablaDest As String, _campoDest As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        _Where = _campoDest + "=" + _numiOri
        _campos = _campoDest
        _Tabla = D_Datos_Tabla(_campos, _tablaDest, _Where)
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "METODOS PARA EL CONTROL DE USUARIOS Y PRIVILEGIOS"

#Region "Formularios"
    Public Shared Function L_Formulario_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY001.yanumi=ZY001.yanumi and ZY001.yamod=TC0051.cenum and cecod1=4 AND cecod2=1 "
        Else
            _Where = "ZY001.yanumi=ZY001.yanumi and ZY001.yamod=TC0051.cenum and cecod1=4 AND cecod2=1 " + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY001.yanumi,ZY001.yaprog,ZY001.yatit,ZY001.yamod,TC0051.cedesc1", "ZY001,TC0051", _Where + " order by yanumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Sub L_Formulario_Grabar(ByRef _numi As String, _desc As String, _direc As String, _categ As String)
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY001", "yanumi", "yanumi=yanumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        Dim Sql As String
        Sql = _numi + ",'" + _desc + "','" + _direc + "'," + _categ
        _Err = D_Insertar_Datos("ZY001", Sql)
    End Sub

    Public Shared Sub L_Formulario_Modificar(_numi As String, _desc As String, _direc As String, _categ As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "yaprog = '" + _desc + "' , " +
        "yatit = '" + _direc + "' , " +
        "yamod = " + _categ

        _where = "yanumi = " + _numi
        _Err = D_Modificar_Datos("ZY001", Sql, _where)
    End Sub

    Public Shared Sub L_Formulario_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "yanumi = " + _Id
        _Err = D_Eliminar_Datos("ZY001", _Where)
    End Sub
#End Region

#Region "Roles"
    Public Shared Function L_Rol_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY002.ybnumi=ZY002.ybnumi "
        Else
            _Where = "ZY002.ybnumi=ZY002.ybnumi " + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY002.ybnumi,ZY002.ybrol", "ZY002", _Where + " order by ybnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_RolDetalle_General(_Modo As Integer, Optional _idCabecera As String = "", Optional _idModulo As String = "") As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        If _Modo = 0 Then
            _Where = " ycnumi = ycnumi"
        Else
            _Where = " ycnumi=" + _idCabecera + " and ZY001.yamod=" + _idModulo + " and ZY0021.ycyanumi=ZY001.yanumi"
        End If
        _Tabla = D_Datos_Tabla("ZY0021.ycnumi,ZY0021.ycyanumi,ZY0021.ycshow,ZY0021.ycadd,ZY0021.ycmod,ZY0021.ycdel,ZY001.yaprog,ZY001.yatit", "ZY0021,ZY001", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_RolDetalle_General2(_Modo As Integer, Optional _idCabecera As String = "", Optional _where1 As String = "") As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        If _Modo = 0 Then
            _Where = " ycnumi = ycnumi"
        Else
            _Where = " ycnumi=" + _idCabecera + " and " + _where1
        End If
        _Tabla = D_Datos_Tabla("ycnumi,ycyanumi,ycshow,ycadd,ycmod,ycdel", "ZY0021", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_prRolDetalleGeneral(_numiRol As String, _idNombreButton As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String

        _Where = "ZY0021.ycnumi=" + _numiRol + " and ZY0021.ycyanumi=ZY001.yanumi and ZY001.yaprog='" + _idNombreButton + "'"

        _Tabla = D_Datos_Tabla("ycnumi,ycyanumi,ycshow,ycadd,ycmod,ycdel", "ZY0021,ZY001", _Where)
        Return _Tabla
    End Function

    Public Shared Sub L_Rol_Grabar(ByRef _numi As String, _rol As String)
        Dim _Actualizacion As String
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY002", "ybnumi", "ybnumi=ybnumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString + ":" + Now.Minute.ToString + "' ,'" + L_Usuario + "'"

        Dim Sql As String
        Sql = _numi + ",'" + _rol + "'," + _Actualizacion
        _Err = D_Insertar_Datos("ZY002", Sql)
    End Sub
    Public Shared Sub L_RolDetalle_Grabar(_idCabecera As String, _numi1 As Integer, _show As Boolean, _add As Boolean, _mod As Boolean, _del As Boolean)
        Dim _Err As Boolean
        Dim Sql As String
        Sql = _idCabecera & "," & _numi1 & ",'" & _show & "','" & _add & "','" & _mod & "','" & _del & "'"
        _Err = D_Insertar_Datos("ZY0021", Sql)
    End Sub
    Public Shared Sub L_Rol_Modificar(_numi As String, _desc As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "ybrol = '" + _desc + "' "

        _where = "ybnumi = " + _numi
        _Err = D_Modificar_Datos("ZY002", Sql, _where)
    End Sub
    Public Shared Sub L_Rol_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "ybnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY002", _Where)
    End Sub
    Public Shared Sub L_RolDetalle_Modificar(_idCabecera As String, _numi1 As Integer, _show As Boolean, _add As Boolean, _mod As Boolean, _del As Boolean)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "ycshow = '" & _show & "' , " & "ycadd = '" & _add & "' , " & "ycmod = '" & _mod & "' , " & "ycdel = '" & _del & "' "

        _where = "ycnumi = " & _idCabecera & " and ycyanumi = " & _numi1
        _Err = D_Modificar_Datos("ZY0021", Sql, _where)
    End Sub
    Public Shared Sub L_RolDetalle_Borrar(_Id As String, _Id1 As String)
        Dim _Where As String
        Dim _Err As Boolean

        _Where = "ycnumi = " + _Id + " and ycyanumi = " + _Id1
        _Err = D_Eliminar_Datos("ZY0021", _Where)
    End Sub
    Public Shared Sub L_RolDetalle_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean

        _Where = "ycnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY0021", _Where)
    End Sub
#End Region

#Region "Usuarios"
    Public Shared Function L_Usuario_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol "
        Else
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol " + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser,ZY003.ydpass,ZY003.ydest,ZY003.ydcant,ZY002.ybnumi,ZY002.ybrol", "ZY003,ZY002", _Where + " order by ydnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function


    Public Shared Function L_Usuario_General2(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TC004.cenumi=ZY003.ydemp  and 
DBDies .dbo.TC001 .canumi =ZY003.ydsuc"
        Else
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TC004.cenumi=ZY003.ydemp and 
DBDies .dbo.TC001 .canumi =ZY003.ydsuc" + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser,ZY003.ydpass,ZY003.ydest,ZY003.ydcant,ZY003.ydfontsize,ZY002.ybnumi,ZY002.ybrol,ZY003.ydsuc,ZY003.ydall,ZY003.ydemp,TC004.cedesc,DBDies .dbo.TC001.cadesc", "ZY003,ZY002,TC004,DBDies .dbo.TC001 ", _Where + " order by ydnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_Usuario_General3(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "1=1"
        Else
            _Where = _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser", "ZY003", _Where + " order by yduser")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Sub L_Usuario_Grabar(ByRef _numi As String, _user As String, _pass As String, _rol As String, _estado As String, _cantDias As String, _tamFuente As String, _suc As String, _allSuc As String, _emp As String)
        Dim _Actualizacion As String
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY003", "ydnumi", "ydnumi=ydnumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString + ":" + Now.Minute.ToString + "' ,'" + L_Usuario + "'"

        Dim Sql As String
        Sql = _numi + ",'" + _user + "'," + _rol + ",'" + _pass + "','" + _estado + "'," + _cantDias + "," + _tamFuente + "," + _suc + "," + _allSuc + "," + _emp + "," + _Actualizacion
        _Err = D_Insertar_Datos("ZY003", Sql)
    End Sub
    Public Shared Sub L_Usuario_Modificar(_numi As String, _user As String, _pass As String, _rol As String, _estado As String, _cantDias As String, _tamFuente As String, _suc As String, _allSuc As String, _emp As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "yduser = '" + _user + "' , " +
        "ydpass = '" + _pass + "' , " +
        "ydrol = " + _rol + " , " +
        "ydest = '" + _estado + "' , " +
        "ydcant = " + _cantDias + " , " +
        "ydfontsize = " + _tamFuente + " , " +
        "ydsuc = " + _suc + " , " +
        "ydall = " + _allSuc + " , " +
        "ydemp = " + _emp

        _where = "ydnumi = " + _numi
        _Err = D_Modificar_Datos("ZY003", Sql, _where)
    End Sub
    Public Shared Sub L_Usuario_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "ydnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY003", _Where)
    End Sub

    Public Shared Function L_Validar_Usuario2(_Nom As String, _Pass As String) As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("*", "ZY003", "yduser = '" + _Nom + "' AND ydpass = '" + _Pass + "'")
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function L_Validar_Usuario(_Nom As String, _Pass As String, _numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("ydnumi,yduser,ydrol,ydpass,ydest,ydcant,ydfontsize,ydsuc,ydemp,ydall", "ZY003", "yduser = '" + _Nom + "' AND ydpass = '" + _Pass + "' and ydemp=" + _numiEmpresa)
        Return _Tabla
    End Function
#End Region

#End Region

#Region "CONFIGURACION SISTEMA TCG01"

    Public Shared Function L_prConGlobalGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TCG01", _listParam)

        Return _Tabla
    End Function


#End Region

#Region "CUENTAS"

    Public Shared Function L_prCuentaReportePlanCuentas(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayor(_numiCuenta As String, _fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayorAuxiliar01(_numiCuenta As String, _fechaDel As String, _fechaAl As String, auxiliar01 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 500))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@numiAux", auxiliar01))
        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayorAuxiliar01AndAuxiliar02(_numiCuenta As String, _fechaDel As String, _fechaAl As String, auxiliar01 As Integer, auxiliar02 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 501))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@numiAux", auxiliar01))
        _listParam.Add(New Datos.DParametro("@numiAux2", auxiliar02))
        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayorAuxiliar02(_numiCuenta As String, _fechaDel As String, _fechaAl As String, auxiliar02 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 502))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@numiAux2", auxiliar02))
        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayorPorCliente(_numiCuenta As String, _fechaDel As String, _fechaAl As String, _numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 142))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiCliente))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayorPorClienteAuxiliar01(_numiCuenta As String, _fechaDel As String, _fechaAl As String, _numiCliente As String, Auxiliar01 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 503))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiCliente))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@auxiliar01", Auxiliar01))
        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayorPorClienteAuxiliar01Auxiliar02(_numiCuenta As String, _fechaDel As String, _fechaAl As String, _numiCliente As String, Auxiliar01 As Integer, Auxiliar02 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 504))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiCliente))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@auxiliar01", Auxiliar01))
        _listParam.Add(New Datos.DParametro("@auxiliar02", Auxiliar02))
        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteLibroMayorPorClienteAuxiliar02(_numiCuenta As String, _fechaDel As String, _fechaAl As String, _numiCliente As String, Auxiliar02 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 505))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiCliente))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@auxiliar02", Auxiliar02))
        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaGeneralBasicoParaParametros(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@canumi", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaGeneralBasicoParaParametrosMayores(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1301))
        _listParam.Add(New Datos.DParametro("@canumi", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaGeneralBasicoParaLibroMayor(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@canumi", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaReporteEstadoCuentasResultadosDebeHaber(_numiCuenta As String, _fechaDel As String, _fechaAl As String, _numiEmp As String, _numiAux1 As String, _numiAux2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@canumi", _numiCuenta))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiAux1))
        _listParam.Add(New Datos.DParametro("@numiAux2", _numiAux2))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaReporteEstadoCuentasResultadosEstructura(_numiEmp As String, _fechaDel As String, _fechaAl As String, _numiAux1 As String, _numiAux2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiAux1))
        _listParam.Add(New Datos.DParametro("@numiAux2", _numiAux2))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaReporteEstadoResultados(_numiEmp As String, _fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteEstadoResultadosPorAuxiliar(_numiEmp As String, _fechaDel As String, _fechaAl As String, _numiAux As String, _numiAux2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 101))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiAux))
        _listParam.Add(New Datos.DParametro("@numiAux2", _numiAux2))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteEstadoCuentasResultadosV2(_numiEmp As String, _fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteEstadoCuentasResultadosV2PorAuxiliar(_numiEmp As String, _fechaDel As String, _fechaAl As String, _numiAux As String, _numiAux2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 171))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiAux))
        _listParam.Add(New Datos.DParametro("@numiAux2", _numiAux2))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaReporteBalanceGeneral(_numiEmp As String, _fechaDel As String, _fechaAl As String, _numiAux1 As String, _numiAux2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiAux1))
        _listParam.Add(New Datos.DParametro("@numiAux2", _numiAux2))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaReporteBalanceComprabacionSumasSaldos(_numiEmp As String, _fechaDel As String, _fechaAl As String, _numiAux1 As String, _numiAux2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiAux1))
        _listParam.Add(New Datos.DParametro("@numiAux2", _numiAux2))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaReporteEstadoCuentasActivoPasivo(_numiEmp As String, _fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaReporteEstadoCuentasActivoPasivoSinGestion(_numiEmp As String, _fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7777))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaReporteEstadoCuentasActivoPasivoPorAuxiliar(_numiEmp As String, _fechaDel As String, _fechaAl As String, _numiAuxiliar As String, _numiAuxiliar2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 71))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@numiAux", _numiAuxiliar))
        _listParam.Add(New Datos.DParametro("@numiAux2", _numiAuxiliar2))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentaGeneral(_numiEmp As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaGeneralBasico(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@canumi", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaGeneralBasicoParaComprobante(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 66))
        _listParam.Add(New Datos.DParametro("@canumi", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_prCuentaGetByNroCuenta(_cuenta As String, _numiEmp As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@cacta", _cuenta))
        _listParam.Add(New Datos.DParametro("@caemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaGrabar(ByRef _numi As String, _empresa As String, _cuenta As String, _desc As String, _nivel As String, _moneda As String, _tipo As String, _numiPadre As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@caemp", _empresa))
        _listParam.Add(New Datos.DParametro("@cacta", _cuenta))
        _listParam.Add(New Datos.DParametro("@cadesc", _desc))
        _listParam.Add(New Datos.DParametro("@caniv", _nivel))
        _listParam.Add(New Datos.DParametro("@camon", _moneda))
        _listParam.Add(New Datos.DParametro("@catipo", _tipo))
        _listParam.Add(New Datos.DParametro("@capadre", _numiPadre))
        _listParam.Add(New Datos.DParametro("@TC0011", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prCuentaGrabarHistorial(_numi, _empresa, _cuenta, _desc, _nivel, _moneda, _tipo, _numiPadre, "CUENTAS", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCuentaModificar(_numi As String, _empresa As String, _cuenta As String, _desc As String, _nivel As String, _moneda As String, _tipo As String, _numiPadre As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@caemp", _empresa))
        _listParam.Add(New Datos.DParametro("@cacta", _cuenta))
        _listParam.Add(New Datos.DParametro("@cadesc", _desc))
        _listParam.Add(New Datos.DParametro("@caniv", _nivel))
        _listParam.Add(New Datos.DParametro("@camon", _moneda))
        _listParam.Add(New Datos.DParametro("@TC0011", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True

            L_prCuentaGrabarHistorial(_numi, _empresa, _cuenta, _desc, _nivel, _moneda, _tipo, _numiPadre, "CUENTAS", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCuentaBorrar(_numi As String, _empresa As String, _cuenta As String, _desc As String, _nivel As String, _moneda As String, _tipo As String, _numiPadre As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TC001", "canumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@canumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TC001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prCuentaGrabarHistorial(_numi, _empresa, _cuenta, _desc, _nivel, _moneda, _tipo, _numiPadre, "CUENTAS", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCuentaGrabarHistorial(_numi As String, _empresa As String, _cuenta As String, _desc As String, _nivel As String, _moneda As String, _tipo As String, _numiPadre As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@caemp", _empresa))
        _listParam.Add(New Datos.DParametro("@cacta", _cuenta))
        _listParam.Add(New Datos.DParametro("@cadesc", _desc))
        _listParam.Add(New Datos.DParametro("@caniv", _nivel))
        _listParam.Add(New Datos.DParametro("@camon", _moneda))
        _listParam.Add(New Datos.DParametro("@catipo", _tipo))
        _listParam.Add(New Datos.DParametro("@capadre", _numiPadre))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "TIPO DE CAMBIO"

    Public Shared Function L_prTipoCambioGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prTipoCambioGeneralPorFecha(_fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@cbfecha", _fecha))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prTipoCambioGrabar(ByRef _numi As String, _fecha As String, _dolar As String, _ufv As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cbfecha", _fecha))
        _listParam.Add(New Datos.DParametro("@cbdol", _dolar))
        _listParam.Add(New Datos.DParametro("@cbufv", _ufv))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prTipoCambioModificar(_numi As String, _fecha As String, _dolar As String, _ufv As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@cbnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cbfecha", _fecha))
        _listParam.Add(New Datos.DParametro("@cbdol", _dolar))
        _listParam.Add(New Datos.DParametro("@cbufv", _ufv))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prTipoCambioModificarTodo(detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@TC002", "", detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prTipoCambioGrabarHistorialVarios(detalle, "TIPO DE CAMBIO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prTipoCambioBorrar(_numi As String, _fecha As String, _dolar As String, _ufv As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TC002", "cbnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@cbnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TC002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prTipoCambioGrabarHistorial(_numi As String, _fecha As String, _dolar As String, _ufv As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cbnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cbfecha", _fecha))
        _listParam.Add(New Datos.DParametro("@cbdol", _dolar))
        _listParam.Add(New Datos.DParametro("@cbufv", _ufv))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HC002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prTipoCambioGrabarHistorialVarios(_tablaDetalle As DataTable, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@TC002", "", _tablaDetalle))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HC002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region


#Region "ROLES CORRECTO"

    Public Shared Function L_prRolGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prRolDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prRolGrabar(ByRef _numi As String, _rol As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ybrol", _rol))
        _listParam.Add(New Datos.DParametro("@ZY0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prRolModificar(_numi As String, _rol As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ybrol", _rol))
        _listParam.Add(New Datos.DParametro("@ZY0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



    Public Shared Function L_prRolBorrar(_numi As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "ZY002", "ybnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



#End Region


#Region "AUXILIAR"

    Public Shared Function L_prAuxiliarGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC003", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prAuxiliarDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ccnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC003", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prAuxiliarDetalleGeneralAuxiliar(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ccnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC003", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prAuxiliarGrabar(ByRef _numi As String, _desc As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ccnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ccdesc", _desc))
        _listParam.Add(New Datos.DParametro("@TC0031", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prAuxiliarGrabarHistorial(_numi, _desc, "AUXILIAR", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prAuxiliarModificar(_numi As String, _desc As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ccnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ccdesc", _desc))
        _listParam.Add(New Datos.DParametro("@TC0031", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prAuxiliarGrabarHistorial(_numi, _desc, "AUXILIAR", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_prAuxiliarBorrar(_numi As String, _desc As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TC003", "ccnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ccnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TC003", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prAuxiliarGrabarHistorial(_numi, _desc, "AUXILIAR", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prAuxiliarGrabarHistorial(_numi As String, _desc As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ccnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ccdesc", _desc))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HC003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "COMPROBANTE"
    Public Shared Function L_prComprobanteObtenerUsuariosSistema() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prComprobanteDetalleDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@oanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prComprobanteReporteComprobante(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@oanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prComprobanteReporteLibroDiario(_numiEmp As String, _fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmp))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prComprobanteGeneral(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prComprobanteGeneral2(_numiEmpresa As String, _fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prComprobanteDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@oanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prComprobanteDetalleGeneralRecuperado(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 401))
        _listParam.Add(New Datos.DParametro("@oanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prComprobanteDetalleObtenerRecuperados() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 402))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prObtenerNumFacturaGeneral(_tipo As String, _anio As String, _mes As String, _numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@oatip", _tipo))
        _listParam.Add(New Datos.DParametro("@oaano", _anio))
        _listParam.Add(New Datos.DParametro("@oames", _mes))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prObtenerUltimoComprobantePorTipoAnioMesEmpresa(_tipo As String, _anio As String, _mes As String, _numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@oatip", _tipo))
        _listParam.Add(New Datos.DParametro("@oaano", _anio))
        _listParam.Add(New Datos.DParametro("@oames", _mes))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prComprobanteGrabar(ByRef _numi As String, _numDoc As String, _tipo As String, _anio As String, _mes As String, _num As String, _fecha As String, _tipoCambio As String, _glosa As String, _obs As String, _numiEmpresa As String, _detalle As DataTable, _detalleDetalle As DataTable, _detalleDetalleCompras As DataTable, _user As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@oanumdoc", _numDoc))
        _listParam.Add(New Datos.DParametro("@oatip", _tipo))
        _listParam.Add(New Datos.DParametro("@oaano", _anio))
        _listParam.Add(New Datos.DParametro("@oames", _mes))
        '_listParam.Add(New Datos.DParametro("@oanum", _num))
        _listParam.Add(New Datos.DParametro("@oafdoc", _fecha))
        _listParam.Add(New Datos.DParametro("@oatc", _tipoCambio))
        _listParam.Add(New Datos.DParametro("@oaglosa", _glosa))
        _listParam.Add(New Datos.DParametro("@oaobs", _obs))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@TO0011", "", _detalle))
        _listParam.Add(New Datos.DParametro("@TO00111", "", _detalleDetalle))
        _listParam.Add(New Datos.DParametro("@TFC001", "", _detalleDetalleCompras))
        _listParam.Add(New Datos.DParametro("@uact", _user))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prComprobanteGrabarHistorial(_numi, _numDoc, _tipo, _anio, _mes, _num, _fecha, _tipoCambio, _glosa, _obs, _numiEmpresa, "COMPROBANTE", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prComprobanteGrabarRespaldo(numiUsuario As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 101))
        _listParam.Add(New Datos.DParametro("@oanumi", numiUsuario))
        _listParam.Add(New Datos.DParametro("@TO0011", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prComprobanteEliminarRespaldo(numiUsuario As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 102))
        _listParam.Add(New Datos.DParametro("@oanumi", numiUsuario))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



    Public Shared Function L_prComprobanteGrabarIntegracion(ByRef _numi As String, _numDoc As String, _tipo As String, _anio As String, _mes As String, _num As String, _fecha As String, _tipoCambio As String, _glosa As String, _obs As String, _numiEmpresa As String, _detalle As DataTable, _detalle2 As DataTable, _ifnumi As String, _ifto001numi As Integer, _iftc As Double,
                                                            _iffechai As String, _iffechaf As String, _ifest As Integer, _dtestado As DataTable, _sucursal As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))

        _listParam.Add(New Datos.DParametro("@oanumdoc", _numDoc))
        _listParam.Add(New Datos.DParametro("@oatip", 1))
        _listParam.Add(New Datos.DParametro("@oaano", _anio))
        _listParam.Add(New Datos.DParametro("@oames", _mes))
        '_listParam.Add(New Datos.DParametro("@oanum", _num))
        _listParam.Add(New Datos.DParametro("@oafdoc", _fecha))
        _listParam.Add(New Datos.DParametro("@oatc", _tipoCambio))
        _listParam.Add(New Datos.DParametro("@oaglosa", ""))
        _listParam.Add(New Datos.DParametro("@oaobs", ""))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@TI005", "", _detalle))
        _listParam.Add(New Datos.DParametro("@Banco", "", _detalle2))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        '@ifnumi int =-1,@ifto001numi int=-1,@iftc decimal(18,2)=null,
        '					 @iffechai date=null,@iffechaf date=null,@ifest int=-1
        _listParam.Add(New Datos.DParametro("@ifnumi", _ifnumi))
        _listParam.Add(New Datos.DParametro("@ifto001numi", _ifto001numi))
        _listParam.Add(New Datos.DParametro("@iftc", _iftc))
        _listParam.Add(New Datos.DParametro("@iffechai", _iffechai))
        _listParam.Add(New Datos.DParametro("@iffechaf", _iffechaf))
        _listParam.Add(New Datos.DParametro("@ifest", _ifest))
        _listParam.Add(New Datos.DParametro("@ifsuc", _sucursal))
        _listParam.Add(New Datos.DParametro("@Estado", "", _dtestado))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True

        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnComprobanteIntegracionArqueoEliminar(_vcnumi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(_vcnumi, "TA005", "ahnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ahnumi", _vcnumi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TA005", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                'L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
                '                                 autoriz, nfac, key, fdel,
                '                                 fal, nota, nota2, est, "DOSIFICACION", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_prComprobanteGrabarIntegracionArqueo(ByRef _numi As String, _numDoc As String, _tipo As String, _anio As String, _mes As String, _num As String, _fecha As String, _tipoCambio As String, _glosa As String, _obs As String, _numiEmpresa As String, _detalle As DataTable, _ifnumi As String, _ifto001numi As Integer, _iftc As Double,
                                                            _iffechai As String, _iffechaf As String, _ifest As Integer, _dtestado As DataTable, _sucursal As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))

        _listParam.Add(New Datos.DParametro("@oanumdoc", _numDoc))
        _listParam.Add(New Datos.DParametro("@oatip", 1))
        _listParam.Add(New Datos.DParametro("@oaano", _anio))
        _listParam.Add(New Datos.DParametro("@oames", _mes))
        '_listParam.Add(New Datos.DParametro("@oanum", _num))
        _listParam.Add(New Datos.DParametro("@oafdoc", _fecha))
        _listParam.Add(New Datos.DParametro("@oatc", _tipoCambio))
        _listParam.Add(New Datos.DParametro("@oaglosa", ""))
        _listParam.Add(New Datos.DParametro("@oaobs", ""))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@TI005", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        '@ifnumi int =-1,@ifto001numi int=-1,@iftc decimal(18,2)=null,
        '					 @iffechai date=null,@iffechaf date=null,@ifest int=-1
        _listParam.Add(New Datos.DParametro("@ahnumi", _ifnumi))
        _listParam.Add(New Datos.DParametro("@ahto001numi", _ifto001numi))
        _listParam.Add(New Datos.DParametro("@ahtc", _iftc))
        _listParam.Add(New Datos.DParametro("@ahfechai", _iffechai))
        _listParam.Add(New Datos.DParametro("@ahfechaf", _iffechaf))
        _listParam.Add(New Datos.DParametro("@ahest", _ifest))
        _listParam.Add(New Datos.DParametro("@Estado", "", _dtestado))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True

        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prComprobanteModificar(_numi As String, _numDoc As String, _tipo As String, _anio As String, _mes As String, _num As String, _fecha As String, _tipoCambio As String, _glosa As String, _obs As String, _numiEmpresa As String, _detalle As DataTable, _detalleDetalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@oanumi", _numi))
        _listParam.Add(New Datos.DParametro("@oanumdoc", _numDoc))
        _listParam.Add(New Datos.DParametro("@oatip", _tipo))
        _listParam.Add(New Datos.DParametro("@oaano", _anio))
        _listParam.Add(New Datos.DParametro("@oames", _mes))
        _listParam.Add(New Datos.DParametro("@oanum", _num))
        _listParam.Add(New Datos.DParametro("@oafdoc", _fecha))
        _listParam.Add(New Datos.DParametro("@oatc", _tipoCambio))
        _listParam.Add(New Datos.DParametro("@oaglosa", _glosa))
        _listParam.Add(New Datos.DParametro("@oaobs", _obs))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@TO0011", "", _detalle))
        _listParam.Add(New Datos.DParametro("@TO00111", "", _detalleDetalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prComprobanteGrabarHistorial(_numi, _numDoc, _tipo, _anio, _mes, _num, _fecha, _tipoCambio, _glosa, _obs, _numiEmpresa, "COMPROBANTE", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_prComprobanteBorrar(_numi As String, _numDoc As String, _tipo As String, _anio As String, _mes As String, _num As String, _fecha As String, _tipoCambio As String, _glosa As String, _obs As String, _numiEmpresa As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TO001", "oanumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@oanumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TO001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prComprobanteGrabarHistorial(_numi, _numDoc, _tipo, _anio, _mes, _num, _fecha, _tipoCambio, _glosa, _obs, _numiEmpresa, "COMPROBANTE", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prComprobanteGrabarHistorial(_numi As String, _numDoc As String, _tipo As String, _anio As String, _mes As String, _num As String, _fecha As String, _tipoCambio As String, _glosa As String, _obs As String, _numiEmpresa As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@oanumi", _numi))
        _listParam.Add(New Datos.DParametro("@oanumdoc", _numDoc))
        _listParam.Add(New Datos.DParametro("@oatip", _tipo))
        _listParam.Add(New Datos.DParametro("@oaano", _anio))
        _listParam.Add(New Datos.DParametro("@oames", _mes))
        _listParam.Add(New Datos.DParametro("@oanum", _num))
        _listParam.Add(New Datos.DParametro("@oafdoc", _fecha))
        _listParam.Add(New Datos.DParametro("@oatc", _tipoCambio))
        _listParam.Add(New Datos.DParametro("@oaglosa", _glosa))
        _listParam.Add(New Datos.DParametro("@oaobs", _obs))
        _listParam.Add(New Datos.DParametro("@oaemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HO001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "EMPRESA"

    Public Shared Function L_prEmpresaGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prEmpresaAyuda() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC004", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prEmpresaGrabar(ByRef _numi As String, _desc As String, _con1 As String, _con2 As String, _con3 As String, _con4 As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cenumi", _numi))
        _listParam.Add(New Datos.DParametro("@cedesc", _desc))
        _listParam.Add(New Datos.DParametro("@cecon1", _con1))
        _listParam.Add(New Datos.DParametro("@cecon2", _con2))
        _listParam.Add(New Datos.DParametro("@cecon3", _con3))
        _listParam.Add(New Datos.DParametro("@cecon4", _con4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prEmpresaGrabarHistorial(_numi, _desc, _con1, _con2, _con3, _con4, "EMPRESA", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prEmpresaModificar(_numi As String, _desc As String, _con1 As String, _con2 As String, _con3 As String, _con4 As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@cenumi", _numi))
        _listParam.Add(New Datos.DParametro("@cedesc", _desc))
        _listParam.Add(New Datos.DParametro("@cecon1", _con1))
        _listParam.Add(New Datos.DParametro("@cecon2", _con2))
        _listParam.Add(New Datos.DParametro("@cecon3", _con3))
        _listParam.Add(New Datos.DParametro("@cecon4", _con4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prEmpresaGrabarHistorial(_numi, _desc, _con1, _con2, _con3, _con4, "EMPRESA", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_prEmpresaBorrar(_numi As String, _desc As String, _con1 As String, _con2 As String, _con3 As String, _con4 As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TC004", "cenumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@cenumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TC004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prEmpresaGrabarHistorial(_numi, _desc, _con1, _con2, _con3, _con4, "EMPRESA", 2)

            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prEmpresaGrabarHistorial(_numi As String, _desc As String, _con1 As String, _con2 As String, _con3 As String, _con4 As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cenumi", _numi))
        _listParam.Add(New Datos.DParametro("@cedesc", _desc))
        _listParam.Add(New Datos.DParametro("@cecon1", _con1))
        _listParam.Add(New Datos.DParametro("@cecon2", _con2))
        _listParam.Add(New Datos.DParametro("@cecon3", _con3))
        _listParam.Add(New Datos.DParametro("@cecon4", _con4))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HC004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region


#Region "ARQUEO TA001"
    Public Shared Function L_prArqueoObtenerDatosMaquina(_numiMaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@aanumi", _numiMaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prArqueoDetalleMaquinasGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoObtenerUltimoRegistroManguera(_numiMaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@aamaq", _numiMaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoReporteResumenObtenerTotales(_fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoReporteResumen(_fechaDel As String, _fechaAl As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoReporte(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prArqueoVendedorAyuda(tipo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@tipoEmpleado", tipo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoMaquinaAyuda() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoMaquinaAyudaTodos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 71))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prArqueoClienteAyuda(tipoCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@aanumi", tipoCliente))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_prArqueoDetalle1General(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoDetalle3PagosAnticipadosGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 41))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prArqueoDetalle4ProductosGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prArqueoDetalleDetalle1General(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 42))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prArqueoDetalle2General(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prArqueoDetalle3General(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prArqueoObtenerProductosGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        Return _Tabla
    End Function
    'Public Shared Function L_prArqueoMaquinaActualizarMit(_numi As String, _mitFin As String) As Boolean
    '    Dim _resultado As Boolean

    '    Dim _Tabla As DataTable
    '    Dim _listParam As New List(Of Datos.DParametro)

    '    _listParam.Add(New Datos.DParametro("@tipo", 9))

    '    _listParam.Add(New Datos.DParametro("@aamaq1", _numi))
    '    _listParam.Add(New Datos.DParametro("@aamitini1", _))

    '    _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

    '    _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

    '    If _Tabla.Rows.Count > 0 Then
    '        _resultado = True

    '    Else
    '        _resultado = False
    '    End If

    '    Return _resultado
    'End Function

    Public Shared Function L_prArqueoGrabar(ByRef _numi As String, _fecha As String, _turno As String, _vendedor As String, _numiMaquina As String, _manguera1 As String, _mitIni1 As String, _mitFin1 As String, _total1 As String, _manguera2 As String, _mitIni2 As String, _mitFin2 As String, _total2 As String, _manguera3 As String, _mitIni3 As String, _mitFin3 As String, _total3 As String, _manguera4 As String, _mitIni4 As String, _mitFin4 As String, _total4 As String, _totalEfectivo As String, _totalTarjeta As String, _totalDolares As String, _ticoCambio As String, _obs As String, _numiCaja As String, _totalProd As Double, cali1 As String, cali2 As String, cali3 As String, cali4 As String, _detalle1 As DataTable, _detalle2 As DataTable, _detalle3 As DataTable, _detalleDetalle As DataTable, _detalleDetalleAnticipo As DataTable, _detalle4 As DataTable, _detalleMangueras As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@aafec", _fecha))
        _listParam.Add(New Datos.DParametro("@aatur", _turno))
        _listParam.Add(New Datos.DParametro("@aaven", _vendedor))
        _listParam.Add(New Datos.DParametro("@aamaq", _numiMaquina))
        _listParam.Add(New Datos.DParametro("@aaman1", _manguera1))
        _listParam.Add(New Datos.DParametro("@aamitini1", _mitIni1))
        _listParam.Add(New Datos.DParametro("@aamitfin1", _mitFin1))
        _listParam.Add(New Datos.DParametro("@aatotal1", _total1))
        _listParam.Add(New Datos.DParametro("@aaman2", _manguera2))
        _listParam.Add(New Datos.DParametro("@aamitini2", _mitIni2))
        _listParam.Add(New Datos.DParametro("@aamitfin2", _mitFin2))
        _listParam.Add(New Datos.DParametro("@aatotal2", _total2))
        _listParam.Add(New Datos.DParametro("@aaman3", _manguera3))
        _listParam.Add(New Datos.DParametro("@aamitini3", _mitIni3))
        _listParam.Add(New Datos.DParametro("@aamitfin3", _mitFin3))
        _listParam.Add(New Datos.DParametro("@aatotal3", _total3))
        _listParam.Add(New Datos.DParametro("@aaman4", _manguera4))
        _listParam.Add(New Datos.DParametro("@aamitini4", _mitIni4))
        _listParam.Add(New Datos.DParametro("@aamitfin4", _mitFin4))
        _listParam.Add(New Datos.DParametro("@aatotal4", _total4))
        _listParam.Add(New Datos.DParametro("@aatotefe", _totalEfectivo))
        _listParam.Add(New Datos.DParametro("@aatottar", _totalTarjeta))
        _listParam.Add(New Datos.DParametro("@aatotdol", _totalDolares))
        _listParam.Add(New Datos.DParametro("@aatc", _ticoCambio))
        _listParam.Add(New Datos.DParametro("@aaobs", _obs))
        _listParam.Add(New Datos.DParametro("@aacaj", _numiCaja))
        _listParam.Add(New Datos.DParametro("@aaprod", _totalProd))
        _listParam.Add(New Datos.DParametro("@aamitcali1", cali1))
        _listParam.Add(New Datos.DParametro("@aamitcali2", cali2))
        _listParam.Add(New Datos.DParametro("@aamitcali3", cali3))
        _listParam.Add(New Datos.DParametro("@aamitcali4", cali4))
        _listParam.Add(New Datos.DParametro("@TA0011", "", _detalle1))
        _listParam.Add(New Datos.DParametro("@TA0012", "", _detalle2))
        _listParam.Add(New Datos.DParametro("@TA0013", "", _detalle3))
        _listParam.Add(New Datos.DParametro("@TA00111", "", _detalleDetalle))
        _listParam.Add(New Datos.DParametro("@TA00111Anticipo", "", _detalleDetalleAnticipo))
        _listParam.Add(New Datos.DParametro("@TA0014", "", _detalle4))
        _listParam.Add(New Datos.DParametro("@TA0015", "", _detalleMangueras))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prArqueoGrabarHistorial(_numi, _fecha, _turno, _vendedor, _numiMaquina, _manguera1, _mitIni1, _mitFin1, _total1, _manguera2, _mitIni2, _mitFin2, _total2, _manguera3, _mitIni3, _mitFin3, _total3, _manguera4, _mitIni4, _mitFin4, _total4, _totalEfectivo, _totalTarjeta, _totalDolares, _ticoCambio, _obs, _numiCaja, _totalProd, cali1, cali2, cali3, cali4, "ARQUEO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prArqueoModificar(_numi As String, _fecha As String, _turno As String, _vendedor As String, _numiMaquina As String, _manguera1 As String, _mitIni1 As String, _mitFin1 As String, _total1 As String, _manguera2 As String, _mitIni2 As String, _mitFin2 As String, _total2 As String, _manguera3 As String, _mitIni3 As String, _mitFin3 As String, _total3 As String, _manguera4 As String, _mitIni4 As String, _mitFin4 As String, _total4 As String, _totalEfectivo As String, _totalTarjeta As String, _totalDolares As String, _ticoCambio As String, _obs As String, _numiCaja As String, _totalProd As Double, cali1 As String, cali2 As String, cali3 As String, cali4 As String, _detalle1 As DataTable, _detalle2 As DataTable, _detalle3 As DataTable, _detalleDetalle As DataTable, _detalleDetalleAnticipo As DataTable, _detalle4 As DataTable, _detalleMangueras As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@aafec", _fecha))
        _listParam.Add(New Datos.DParametro("@aatur", _turno))
        _listParam.Add(New Datos.DParametro("@aaven", _vendedor))
        _listParam.Add(New Datos.DParametro("@aamaq", _numiMaquina))
        _listParam.Add(New Datos.DParametro("@aaman1", _manguera1))
        _listParam.Add(New Datos.DParametro("@aamitini1", _mitIni1))
        _listParam.Add(New Datos.DParametro("@aamitfin1", _mitFin1))
        _listParam.Add(New Datos.DParametro("@aatotal1", _total1))
        _listParam.Add(New Datos.DParametro("@aaman2", _manguera2))
        _listParam.Add(New Datos.DParametro("@aamitini2", _mitIni2))
        _listParam.Add(New Datos.DParametro("@aamitfin2", _mitFin2))
        _listParam.Add(New Datos.DParametro("@aatotal2", _total2))
        _listParam.Add(New Datos.DParametro("@aaman3", _manguera3))
        _listParam.Add(New Datos.DParametro("@aamitini3", _mitIni3))
        _listParam.Add(New Datos.DParametro("@aamitfin3", _mitFin3))
        _listParam.Add(New Datos.DParametro("@aatotal3", _total3))
        _listParam.Add(New Datos.DParametro("@aaman4", _manguera4))
        _listParam.Add(New Datos.DParametro("@aamitini4", _mitIni4))
        _listParam.Add(New Datos.DParametro("@aamitfin4", _mitFin4))
        _listParam.Add(New Datos.DParametro("@aatotal4", _total4))
        _listParam.Add(New Datos.DParametro("@aatotefe", _totalEfectivo))
        _listParam.Add(New Datos.DParametro("@aatottar", _totalTarjeta))
        _listParam.Add(New Datos.DParametro("@aatotdol", _totalDolares))
        _listParam.Add(New Datos.DParametro("@aatc", _ticoCambio))
        _listParam.Add(New Datos.DParametro("@aaobs", _obs))
        _listParam.Add(New Datos.DParametro("@aacaj", _numiCaja))
        _listParam.Add(New Datos.DParametro("@aaprod", _totalProd))
        _listParam.Add(New Datos.DParametro("@aamitcali1", cali1))
        _listParam.Add(New Datos.DParametro("@aamitcali2", cali2))
        _listParam.Add(New Datos.DParametro("@aamitcali3", cali3))
        _listParam.Add(New Datos.DParametro("@aamitcali4", cali4))
        _listParam.Add(New Datos.DParametro("@TA0011", "", _detalle1))
        _listParam.Add(New Datos.DParametro("@TA0012", "", _detalle2))
        _listParam.Add(New Datos.DParametro("@TA0013", "", _detalle3))
        _listParam.Add(New Datos.DParametro("@TA00111", "", _detalleDetalle))
        _listParam.Add(New Datos.DParametro("@TA00111Anticipo", "", _detalleDetalleAnticipo))
        _listParam.Add(New Datos.DParametro("@TA0014", "", _detalle4))
        _listParam.Add(New Datos.DParametro("@TA0015", "", _detalleMangueras))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True

            L_prArqueoGrabarHistorial(_numi, _fecha, _turno, _vendedor, _numiMaquina, _manguera1, _mitIni1, _mitFin1, _total1, _manguera2, _mitIni2, _mitFin2, _total2, _manguera3, _mitIni3, _mitFin3, _total3, _manguera4, _mitIni4, _mitFin4, _total4, _totalEfectivo, _totalTarjeta, _totalDolares, _ticoCambio, _obs, _numiCaja, _totalProd, cali1, cali2, cali3, cali4, "ARQUEO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prArqueoBorrar(_numi As String, _fecha As String, _turno As String, _vendedor As String, _numiMaquina As String, _manguera1 As String, _mitIni1 As String, _mitFin1 As String, _total1 As String, _manguera2 As String, _mitIni2 As String, _mitFin2 As String, _total2 As String, _manguera3 As String, _mitIni3 As String, _mitFin3 As String, _total3 As String, _manguera4 As String, _mitIni4 As String, _mitFin4 As String, _total4 As String, _totalEfectivo As String, _totalTarjeta As String, _totalDolares As String, _ticoCambio As String, _obs As String, _numiCaja As String, _totalProd As String, cali1 As String, cali2 As String, cali3 As String, cali4 As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TA001", "aanumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@aanumi", _numi))
            _listParam.Add(New Datos.DParametro("@aaman1", _manguera1))
            _listParam.Add(New Datos.DParametro("@aamitini1", _mitIni1))
            _listParam.Add(New Datos.DParametro("@aamitfin1", _mitFin1))
            _listParam.Add(New Datos.DParametro("@aatotal1", _total1))
            _listParam.Add(New Datos.DParametro("@aaman2", _manguera2))
            _listParam.Add(New Datos.DParametro("@aamitini2", _mitIni2))
            _listParam.Add(New Datos.DParametro("@aamitfin2", _mitFin2))
            _listParam.Add(New Datos.DParametro("@aatotal2", _total2))
            _listParam.Add(New Datos.DParametro("@aaman3", _manguera3))
            _listParam.Add(New Datos.DParametro("@aamitini3", _mitIni3))
            _listParam.Add(New Datos.DParametro("@aamitfin3", _mitFin3))
            _listParam.Add(New Datos.DParametro("@aatotal3", _total3))
            _listParam.Add(New Datos.DParametro("@aaman4", _manguera4))
            _listParam.Add(New Datos.DParametro("@aamitini4", _mitIni4))
            _listParam.Add(New Datos.DParametro("@aamitfin4", _mitFin4))
            _listParam.Add(New Datos.DParametro("@aatotal4", _total4))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TA001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prArqueoGrabarHistorial(_numi, _fecha, _turno, _vendedor, _numiMaquina, _manguera1, _mitIni1, _mitFin1, _total1, _manguera2, _mitIni2, _mitFin2, _total2, _manguera3, _mitIni3, _mitFin3, _total3, _manguera4, _mitIni4, _mitFin4, _total4, _totalEfectivo, _totalTarjeta, _totalDolares, _ticoCambio, _obs, _numiCaja, _totalProd, cali1, cali2, cali3, cali4, "ARQUEO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prArqueoGrabarHistorial(_numi As String, _fecha As String, _turno As String, _vendedor As String, _numiMaquina As String, _manguera1 As String, _mitIni1 As String, _mitFin1 As String, _total1 As String, _manguera2 As String, _mitIni2 As String, _mitFin2 As String, _total2 As String, _manguera3 As String, _mitIni3 As String, _mitFin3 As String, _total3 As String, _manguera4 As String, _mitIni4 As String, _mitFin4 As String, _total4 As String, _totalEfectivo As String, _totalTarjeta As String, _totalDolares As String, _ticoCambio As String, _obs As String, _numiCaja As String, _totalProd As String, cali1 As String, cali2 As String, cali3 As String, cali4 As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@aanumi", _numi))
        _listParam.Add(New Datos.DParametro("@aafec", _fecha))
        _listParam.Add(New Datos.DParametro("@aatur", _turno))
        _listParam.Add(New Datos.DParametro("@aaven", _vendedor))
        _listParam.Add(New Datos.DParametro("@aamaq", _numiMaquina))
        _listParam.Add(New Datos.DParametro("@aaman1", _manguera1))
        _listParam.Add(New Datos.DParametro("@aamitini1", _mitIni1))
        _listParam.Add(New Datos.DParametro("@aamitfin1", _mitFin1))
        _listParam.Add(New Datos.DParametro("@aatotal1", _total1))
        _listParam.Add(New Datos.DParametro("@aaman2", _manguera2))
        _listParam.Add(New Datos.DParametro("@aamitini2", _mitIni2))
        _listParam.Add(New Datos.DParametro("@aamitfin2", _mitFin2))
        _listParam.Add(New Datos.DParametro("@aatotal2", _total2))
        _listParam.Add(New Datos.DParametro("@aaman3", _manguera3))
        _listParam.Add(New Datos.DParametro("@aamitini3", _mitIni3))
        _listParam.Add(New Datos.DParametro("@aamitfin3", _mitFin3))
        _listParam.Add(New Datos.DParametro("@aatotal3", _total3))
        _listParam.Add(New Datos.DParametro("@aaman4", _manguera4))
        _listParam.Add(New Datos.DParametro("@aamitini4", _mitIni4))
        _listParam.Add(New Datos.DParametro("@aamitfin4", _mitFin4))
        _listParam.Add(New Datos.DParametro("@aatotal4", _total4))
        _listParam.Add(New Datos.DParametro("@aatotefe", _totalEfectivo))
        _listParam.Add(New Datos.DParametro("@aatottar", _totalTarjeta))
        _listParam.Add(New Datos.DParametro("@aatotdol", _totalDolares))
        _listParam.Add(New Datos.DParametro("@aatc", _ticoCambio))
        _listParam.Add(New Datos.DParametro("@aaobs", _obs))
        _listParam.Add(New Datos.DParametro("@aacaj", _numiCaja))
        _listParam.Add(New Datos.DParametro("@aaprod", _totalProd))
        _listParam.Add(New Datos.DParametro("@aamitcali1", cali1))
        _listParam.Add(New Datos.DParametro("@aamitcali2", cali2))
        _listParam.Add(New Datos.DParametro("@aamitcali3", cali3))
        _listParam.Add(New Datos.DParametro("@aamitcali4", cali4))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HA001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "CLIENTE CARBURANTES"

    Public Shared Function L_prClienteCarburadorGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prClienteCarburadorGrabar(ByRef _numi As String, _nombre As String, _direccion As String, _telef As String, _tipo As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@adnom", _nombre))
        _listParam.Add(New Datos.DParametro("@addirec", _direccion))
        _listParam.Add(New Datos.DParametro("@adtelef", _telef))
        _listParam.Add(New Datos.DParametro("@adtipo", _tipo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prClienteCarburadorGrabarHistorial(_numi, _nombre, _direccion, _telef, "CLIENTE CARBURANTES", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prClienteCarburadorModificar(_numi As String, _nombre As String, _direccion As String, _telef As String, _tipo As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@adnumi", _numi))
        _listParam.Add(New Datos.DParametro("@adnom", _nombre))
        _listParam.Add(New Datos.DParametro("@addirec", _direccion))
        _listParam.Add(New Datos.DParametro("@adtelef", _telef))
        _listParam.Add(New Datos.DParametro("@adtipo", _tipo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prClienteCarburadorGrabarHistorial(_numi, _nombre, _direccion, _telef, "CLIENTE CARBURANTES", 2)

        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prClienteCarburadorBorrar(_numi As String, _nombre As String, _direccion As String, _telef As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TA002", "adnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@adnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TA002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prClienteCarburadorGrabarHistorial(_numi, _nombre, _direccion, _telef, "CLIENTE CARBURANTES", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prClienteCarburadorGrabarHistorial(_numi As String, _nombre As String, _direccion As String, _telef As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@adnumi", _numi))
        _listParam.Add(New Datos.DParametro("@adnom", _nombre))
        _listParam.Add(New Datos.DParametro("@addirec", _direccion))
        _listParam.Add(New Datos.DParametro("@adtelef", _telef))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HA002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "MAQUINAS"
    Public Shared Function L_prMaquinaObtenerCombustible() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMaquinaDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@aenumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA003", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMaquinaGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA003", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prMaquinaGrabar(ByRef _numi As String, _desc As String, _estado As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@aedesc", _desc))
        '_listParam.Add(New Datos.DParametro("@aenumita2", _maquinaPareja))
        _listParam.Add(New Datos.DParametro("@aeest", _estado))
        '_listParam.Add(New Datos.DParametro("@aeconbus", _combustible))
        '_listParam.Add(New Datos.DParametro("@aeprecio", _precio))
        '_listParam.Add(New Datos.DParametro("@aemitfin", _mitFin))
        _listParam.Add(New Datos.DParametro("@TA0031", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prMaquinaGrabarHistorial(_numi, _desc, _estado, "MAQUINA", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prMaquinaModificar(_numi As String, _desc As String, _estado As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@aenumi", _numi))
        _listParam.Add(New Datos.DParametro("@aedesc", _desc))
        '_listParam.Add(New Datos.DParametro("@aenumita2", _maquinaPareja))
        _listParam.Add(New Datos.DParametro("@aeest", _estado))
        '_listParam.Add(New Datos.DParametro("@aeconbus", _combustible))
        '_listParam.Add(New Datos.DParametro("@aeprecio", _precio))
        '_listParam.Add(New Datos.DParametro("@aemitfin", _mitFin))
        _listParam.Add(New Datos.DParametro("@TA0031", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prMaquinaGrabarHistorial(_numi, _desc, _estado, "MAQUINA", 2)

        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prMaquinaBorrar(_numi As String, _desc As String, _estado As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TA003", "aenumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@aenumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TA003", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prMaquinaGrabarHistorial(_numi, _desc, _estado, "MAQUINA", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prMaquinaGrabarHistorial(_numi As String, _desc As String, _estado As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@aenumi", _numi))
        _listParam.Add(New Datos.DParametro("@aedesc", _desc))
        '_listParam.Add(New Datos.DParametro("@aenumita2", _maquinaPareja))
        _listParam.Add(New Datos.DParametro("@aeest", _estado))
        '_listParam.Add(New Datos.DParametro("@aeconbus", _combustible))
        '_listParam.Add(New Datos.DParametro("@aeprecio", _precio))
        '_listParam.Add(New Datos.DParametro("@aemitfin", _mitFin))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HA003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "DIES"

#Region "PERSONAL"
    Public Shared Function L_prPersonaAyudaGeneralPorSucursal(_suc As String, _tipo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@pasuc", _suc))
        _listParam.Add(New Datos.DParametro("@patipo", _tipo))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPersonaAyudaGeneral(_tipo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 41))
        _listParam.Add(New Datos.DParametro("@patipo", _tipo))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPersonaAyudaGeneral2(_tipo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 43))
        _listParam.Add(New Datos.DParametro("@patipo", _tipo))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPersonaAyudaTodosGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 42))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prPersonaAyudaTodosGeneralCorrMarcado() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPersonaAyudaInstructoresHorasTrabajadas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPersonaGeneral(Optional _Cadena As String = "", Optional _order As String = "") As DataTable 'modelo 1 con condificion
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        '_listParam.Add(New Datos.DParametro("@pasuc", _suc))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPersonaBuscarNumiGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prPersonaBuscarNumiGeneral2(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPersonaGrabar(ByRef _numi As String, _ci As String, _nom As String, _apellido As String, _direc As String, _telef1 As String, _telef2 As String, _email As String, _tipo As String) As Boolean
        Dim _res As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@paci", _ci))
        _listParam.Add(New Datos.DParametro("@panom", _nom))
        _listParam.Add(New Datos.DParametro("@paape", _apellido))
        _listParam.Add(New Datos.DParametro("@padirec", _direc))
        _listParam.Add(New Datos.DParametro("@patelef1", _telef1))
        _listParam.Add(New Datos.DParametro("@patelef2", _telef2))
        _listParam.Add(New Datos.DParametro("@paemail", _email))
        _listParam.Add(New Datos.DParametro("@patipo", _tipo))
        '_listParam.Add(New Datos.DParametro("@pasal", _salario))
        '_listParam.Add(New Datos.DParametro("@paobs", _obs))
        '_listParam.Add(New Datos.DParametro("@pafnac", _fNac))
        '_listParam.Add(New Datos.DParametro("@pafing", _fIng))
        '_listParam.Add(New Datos.DParametro("@pafret", _fRet))
        '_listParam.Add(New Datos.DParametro("@pafot", _foto))
        '_listParam.Add(New Datos.DParametro("@paest", _estado))
        '_listParam.Add(New Datos.DParametro("@paeciv", _estCivil))
        '_listParam.Add(New Datos.DParametro("@pasuc", _suc))
        '_listParam.Add(New Datos.DParametro("@pafijo", _fijo))
        '_listParam.Add(New Datos.DParametro("@pafsal", _fecSalida))
        '_listParam.Add(New Datos.DParametro("@pareloj", _reloj))
        '_listParam.Add(New Datos.DParametro("@paemp", _empresa))
        '_listParam.Add(New Datos.DParametro("@palat", _lat))
        '_listParam.Add(New Datos.DParametro("@palon", _longi))
        '_listParam.Add(New Datos.DParametro("@paesp", _pareja))
        '_listParam.Add(New Datos.DParametro("@pahijos", _hijos))
        '_listParam.Add(New Datos.DParametro("@pamseg", _matriSegu))
        '_listParam.Add(New Datos.DParametro("@patsan", _tipoSangre))
        '_listParam.Add(New Datos.DParametro("@papsalud", _problemSalud))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        '_listParam.Add(New Datos.DParametro("@TP0011", "", _TP0011))



        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            'If _foto <> "" Then
            '    _foto = "personal_" + _numi
            'End If
            _res = True
        Else
            _res = False
        End If

        Return _res
    End Function

    Public Shared Function L_prPersonaModificar(_numi As String, _ci As String, _nom As String, _apellido As String, _direc As String, _telef1 As String, _telef2 As String, _email As String, _tipo As String) As Boolean
        Dim _res As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@paci", _ci))
        _listParam.Add(New Datos.DParametro("@panom", _nom))
        _listParam.Add(New Datos.DParametro("@paape", _apellido))
        _listParam.Add(New Datos.DParametro("@padirec", _direc))
        _listParam.Add(New Datos.DParametro("@patelef1", _telef1))
        _listParam.Add(New Datos.DParametro("@patelef2", _telef2))
        _listParam.Add(New Datos.DParametro("@paemail", _email))
        _listParam.Add(New Datos.DParametro("@patipo", _tipo))
        '_listParam.Add(New Datos.DParametro("@pasal", _salario))
        '_listParam.Add(New Datos.DParametro("@paobs", _obs))
        '_listParam.Add(New Datos.DParametro("@pafnac", _fNac))
        '_listParam.Add(New Datos.DParametro("@pafing", _fIng))
        '_listParam.Add(New Datos.DParametro("@pafret", _fRet))
        '_listParam.Add(New Datos.DParametro("@pafot", _foto))
        '_listParam.Add(New Datos.DParametro("@paest", _estado))
        '_listParam.Add(New Datos.DParametro("@paeciv", _estCivil))
        '_listParam.Add(New Datos.DParametro("@pasuc", _suc))
        '_listParam.Add(New Datos.DParametro("@pafijo", _fijo))
        '_listParam.Add(New Datos.DParametro("@pafsal", _fecSalida))
        '_listParam.Add(New Datos.DParametro("@pareloj", _reloj))
        '_listParam.Add(New Datos.DParametro("@paemp", _empresa))
        '_listParam.Add(New Datos.DParametro("@palat", _lat))
        '_listParam.Add(New Datos.DParametro("@palon", _longi))
        '_listParam.Add(New Datos.DParametro("@paesp", _pareja))
        '_listParam.Add(New Datos.DParametro("@pahijos", _hijos))
        '_listParam.Add(New Datos.DParametro("@pamseg", _matriSegu))
        '_listParam.Add(New Datos.DParametro("@patsan", _tipoSangre))
        '_listParam.Add(New Datos.DParametro("@papsalud", _problemSalud))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        '_listParam.Add(New Datos.DParametro("@TP0011", "", _TP0011))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            'If _foto <> "" Then
            '    _foto = "personal_" + _numi
            'End If
            _res = True
        Else
            _res = False
        End If

        Return _res
    End Function

    Public Shared Function L_prPersonaBorrar(_numi As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TP001", "panumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@panumi", _numi))
            _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _numi = _Tabla.Rows(0).Item(0)
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    '-------------------------------- DETALLE PERSONAL TP0011-----------------------------------------
    Public Shared Function L_prPersonaDetalleGeneral(_numiCab As String, Optional _Cadena As String = "", Optional _order As String = "") As DataTable 'modelo 1 con condificion
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@panumi", _numiCab))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TP001", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "CONFIGURACION"
    Public Shared Function L_prConfigDetalleGeneral(numi As String, tipo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@cfnumi", numi))
        _listParam.Add(New Datos.DParametro("@tipoCobrarPagar", tipo))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prConfigGeneralEmpresa(numiEmpr As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@cfnumitc14", numiEmpr))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prConfigGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC006", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prConfigGrabar(ByRef _numi As String, _empresa As String, _cuenta1 As String, _cuenta2 As String, _cuenta3 As String, _cuenta4 As String, _difMaximaAjuste As String, _cobrarDebe As String, _cobrarHaber As String, _pagarDebe As String, _pagarHaber As String, detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cfnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cftnumitc4", _empresa))
        _listParam.Add(New Datos.DParametro("@cfnumitc11", _cuenta1))
        '_listParam.Add(New Datos.DParametro("@cfnumitc12", _cuenta2))
        '_listParam.Add(New Datos.DParametro("@cfnumitc13", _cuenta3))
        '_listParam.Add(New Datos.DParametro("@cfnumitc14", _cuenta4))
        _listParam.Add(New Datos.DParametro("@cfdifmax", _difMaximaAjuste))
        _listParam.Add(New Datos.DParametro("@cfcobdebe", _cobrarDebe))
        '_listParam.Add(New Datos.DParametro("@cfcobhaber", _cobrarHaber))
        '_listParam.Add(New Datos.DParametro("@cfpagdebe", _pagarDebe))
        '_listParam.Add(New Datos.DParametro("@cfpaghaber", _pagarHaber))
        _listParam.Add(New Datos.DParametro("@TC0061", "", detalle))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC006", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prConfigGrabarHistorial(_numi, _empresa, _cuenta1, _cuenta2, _cuenta3, _cuenta4, _difMaximaAjuste, _cobrarDebe, _cobrarHaber, _pagarDebe, _cobrarHaber, "CONFIGURACION", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prConfigModificar(_numi As String, _empresa As String, _cuenta1 As String, _cuenta2 As String, _cuenta3 As String, _cuenta4 As String, _difMaximaAjuste As String, _cobrarDebe As String, _cobrarHaber As String, _pagarDebe As String, _pagarHaber As String, detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@cfnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cftnumitc4", _empresa))
        _listParam.Add(New Datos.DParametro("@cfnumitc11", _cuenta1))
        _listParam.Add(New Datos.DParametro("@cfnumitc12", _cuenta2))
        _listParam.Add(New Datos.DParametro("@cfnumitc13", _cuenta3))
        _listParam.Add(New Datos.DParametro("@cfnumitc14", _cuenta4))
        _listParam.Add(New Datos.DParametro("@cfdifmax", _difMaximaAjuste))
        _listParam.Add(New Datos.DParametro("@cfcobdebe", _cobrarDebe))
        _listParam.Add(New Datos.DParametro("@cfcobhaber", _cobrarHaber))
        _listParam.Add(New Datos.DParametro("@cfpagdebe", _pagarDebe))
        _listParam.Add(New Datos.DParametro("@cfpaghaber", _pagarHaber))
        _listParam.Add(New Datos.DParametro("@TC0061", "", detalle))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC006", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prConfigGrabarHistorial(_numi, _empresa, _cuenta1, _cuenta2, _cuenta3, _cuenta4, _difMaximaAjuste, _cobrarDebe, _cobrarHaber, _pagarDebe, _cobrarHaber, "CONFIGURACION", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_prConfigBorrar(_numi As String, _empresa As String, _cuenta1 As String, _cuenta2 As String, _cuenta3 As String, _cuenta4 As String, _difMaximaAjuste As String, _cobrarDebe As String, _cobrarHaber As String, _pagarDebe As String, _pagarHaber As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TC006", "cfnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@cfnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TC006", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prConfigGrabarHistorial(_numi, _empresa, _cuenta1, _cuenta2, _cuenta3, _cuenta4, _difMaximaAjuste, _cobrarDebe, _cobrarHaber, _pagarDebe, _cobrarHaber, "CONFIGURACION", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prConfigGrabarHistorial(_numi As String, _empresa As String, _cuenta1 As String, _cuenta2 As String, _cuenta3 As String, _cuenta4 As String, _difMaximaAjuste As String, _cobrarDebe As String, _cobrarHaber As String, _pagarDebe As String, _pagarHaber As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cfnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cftnumitc4", _empresa))
        _listParam.Add(New Datos.DParametro("@cfnumitc11", _cuenta1))
        _listParam.Add(New Datos.DParametro("@cfnumitc12", _cuenta2))
        _listParam.Add(New Datos.DParametro("@cfnumitc13", _cuenta3))
        _listParam.Add(New Datos.DParametro("@cfnumitc14", _cuenta4))
        _listParam.Add(New Datos.DParametro("@cfdifmax", _difMaximaAjuste))
        _listParam.Add(New Datos.DParametro("@cfcobdebe", _cobrarDebe))
        _listParam.Add(New Datos.DParametro("@cfcobhaber", _cobrarHaber))
        _listParam.Add(New Datos.DParametro("@cfpagdebe", _pagarDebe))
        _listParam.Add(New Datos.DParametro("@cfpaghaber", _pagarHaber))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HC006", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region
#End Region


#Region "CUENTAS AUTOMATICAS"

    Public Shared Function L_prCuentasAutomaticaObtenerIngresos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentasAutomaticaObtenerEgresos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCuentasAutomaticaObtenerTraspaso() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentasAutomaticaGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentasAutomaticaDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@cgnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentasAutomaticaDetalleGeneralCompleto(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 41))
        _listParam.Add(New Datos.DParametro("@cgnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prCuentasAutomaticaGrabar(ByRef _numi As String, _desc As String, _estado As String, _ingreso As String, _egreso As String, _trapaso As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cgnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cgdesc", _desc))
        _listParam.Add(New Datos.DParametro("@cgest", _estado))
        _listParam.Add(New Datos.DParametro("@cging", _ingreso))
        _listParam.Add(New Datos.DParametro("@cgegre", _egreso))
        _listParam.Add(New Datos.DParametro("@cgtras", _trapaso))
        _listParam.Add(New Datos.DParametro("@TC0071", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prCuentasAutomaticaGrabarHistorial(_numi, _desc, _estado, _ingreso, _egreso, _trapaso, "CUENTA AUTO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCuentasAutomaticaModificar(_numi As String, _desc As String, _estado As String, _ingreso As String, _egreso As String, _trapaso As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@cgnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cgdesc", _desc))
        _listParam.Add(New Datos.DParametro("@cgest", _estado))
        _listParam.Add(New Datos.DParametro("@cging", _ingreso))
        _listParam.Add(New Datos.DParametro("@cgegre", _egreso))
        _listParam.Add(New Datos.DParametro("@cgtras", _trapaso))
        _listParam.Add(New Datos.DParametro("@TC0071", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prCuentasAutomaticaGrabarHistorial(_numi, _desc, _estado, _ingreso, _egreso, _trapaso, "CUENTA AUTO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_prCuentasAutomaticaBorrar(_numi As String, _desc As String, _estado As String, _ingreso As String, _egreso As String, _trapaso As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TC007", "cgnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@cgnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TC007", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prCuentasAutomaticaGrabarHistorial(_numi, _desc, _estado, _ingreso, _egreso, _trapaso, "CUENTA AUTO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCuentasAutomaticaGrabarHistorial(_numi As String, _desc As String, _estado As String, _ingreso As String, _egreso As String, _trapaso As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cgnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cgdesc", _desc))
        _listParam.Add(New Datos.DParametro("@cgest", _estado))
        _listParam.Add(New Datos.DParametro("@cging", _ingreso))
        _listParam.Add(New Datos.DParametro("@cgegre", _egreso))
        _listParam.Add(New Datos.DParametro("@cgtras", _trapaso))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HC007", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "PRODUCTOS"

    Public Shared Function L_prProductoGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC008", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnProductoNameLabel() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC008", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prProductoLibreriaGeneral(cod1 As String, cod2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ylcod1", cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", cod2))
        _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC008", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prProductosGrabar(ByRef _cinumi As String, _cicprod As String,
                                              _cicbarra As String, _cicdprod1 As String,
                                              _cicdprod2 As String, _cigr1 As Integer, _cigr2 As Integer,
                                              _cigr3 As Integer, _cigr4 As Integer, _ciMed As Integer, _ciumin As Integer, _ciusup As Integer, _civsup As Double, _cismin As Integer, _ciap As Integer, _ciimg As String, _ciprecio As Double) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '     @cinumi ,@cicprod ,@cicbarra ,@cicdprod1 ,@cicdprod2 ,
        '@cigr1 ,@cigr2 ,@cigr3 ,@cigr4 ,@ciMed ,@ciumin ,@ciusup ,@civsup ,
        '@cimstk ,@ciclot ,@cismin ,@ciap ,@ciimg 
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cinumi", _cinumi))
        _listParam.Add(New Datos.DParametro("@cicprod", _cicprod))

        _listParam.Add(New Datos.DParametro("@cicbarra", _cicbarra))
        _listParam.Add(New Datos.DParametro("@cicdprod1", _cicdprod1))
        _listParam.Add(New Datos.DParametro("@cicdprod2", _cicdprod2))
        _listParam.Add(New Datos.DParametro("@cigr1", _cigr1))
        _listParam.Add(New Datos.DParametro("@cigr2", _cigr2))
        _listParam.Add(New Datos.DParametro("@cigr3", _cigr3))
        _listParam.Add(New Datos.DParametro("@cigr4", _cigr4))
        _listParam.Add(New Datos.DParametro("@ciMed", _ciMed))
        _listParam.Add(New Datos.DParametro("@ciumin", _ciumin))
        _listParam.Add(New Datos.DParametro("@ciusup", _ciusup))
        _listParam.Add(New Datos.DParametro("@civsup", _civsup))
        _listParam.Add(New Datos.DParametro("@cimstk", 0))
        _listParam.Add(New Datos.DParametro("@ciclot", 0))

        _listParam.Add(New Datos.DParametro("@cismin", _cismin))
        _listParam.Add(New Datos.DParametro("@ciap", _ciap))
        _listParam.Add(New Datos.DParametro("@ciimg", _ciimg))
        _listParam.Add(New Datos.DParametro("@ciprecio", _ciprecio))
        _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC008", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _cinumi = _Tabla.Rows(0).Item(0)
            _resultado = True

            L_prProductoGrabarHistorial(_cinumi, _cicprod, _cicbarra, _cicdprod1, _cicdprod2, _cigr1, _cigr2, _cigr3, _cigr4, _ciMed, _ciumin, _ciusup, _civsup, _cismin, _ciap, _ciimg, "PRODUCTOS INVENTARIO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prProductosModificar(ByRef _cinumi As String, _cicprod As String,
                                              _cicbarra As String, _cicdprod1 As String,
                                              _cicdprod2 As String, _cigr1 As Integer, _cigr2 As Integer,
                                              _cigr3 As Integer, _cigr4 As Integer, _ciMed As Integer, _ciumin As Integer, _ciusup As Integer, _civsup As Double, _cismin As Integer, _ciap As Integer, _ciimg As String, _ciprecio As Double) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@cinumi", _cinumi))
        _listParam.Add(New Datos.DParametro("@cicprod", _cicprod))

        _listParam.Add(New Datos.DParametro("@cicbarra", _cicbarra))
        _listParam.Add(New Datos.DParametro("@cicdprod1", _cicdprod1))
        _listParam.Add(New Datos.DParametro("@cicdprod2", _cicdprod2))
        _listParam.Add(New Datos.DParametro("@cigr1", _cigr1))
        _listParam.Add(New Datos.DParametro("@cigr2", _cigr2))
        _listParam.Add(New Datos.DParametro("@cigr3", _cigr3))
        _listParam.Add(New Datos.DParametro("@cigr4", _cigr4))
        _listParam.Add(New Datos.DParametro("@ciMed", _ciMed))
        _listParam.Add(New Datos.DParametro("@ciumin", _ciumin))
        _listParam.Add(New Datos.DParametro("@ciusup", _ciusup))
        _listParam.Add(New Datos.DParametro("@civsup", _civsup))
        _listParam.Add(New Datos.DParametro("@cimstk", 0))
        _listParam.Add(New Datos.DParametro("@ciclot", 0))

        _listParam.Add(New Datos.DParametro("@cismin", _cismin))
        _listParam.Add(New Datos.DParametro("@ciap", _ciap))
        _listParam.Add(New Datos.DParametro("@ciimg", _ciimg))
        _listParam.Add(New Datos.DParametro("@ciprecio", _ciprecio))
        _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC008", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prProductoGrabarHistorial(_cinumi, _cicprod, _cicbarra, _cicdprod1, _cicdprod2, _cigr1, _cigr2, _cigr3, _cigr4, _ciMed, _ciumin, _ciusup, _civsup, _cismin, _ciap, _ciimg, "PRODUCTOS INVENTARIO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prProductosBorrar(_numi As String, _cicprod As String,
                                              _cicbarra As String, _cicdprod1 As String,
                                              _cicdprod2 As String, _cigr1 As Integer, _cigr2 As Integer,
                                              _cigr3 As Integer, _cigr4 As Integer, _ciMed As Integer, _ciumin As Integer, _ciusup As Integer, _civsup As Double, _cismin As Integer, _ciap As Integer, _ciimg As String, _ciprecio As Double, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TC008", "cinumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@cinumi", _numi))
            _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TC008", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prProductoGrabarHistorial(_numi, _cicprod, _cicbarra, _cicdprod1, _cicdprod2, _cigr1, _cigr2, _cigr3, _cigr4, _ciMed, _ciumin, _ciusup, _civsup, _cismin, _ciap, _ciimg, "PRODUCTOS INVENTARIO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prProductoGrabarHistorial(ByRef _cgnumi As String, _cgcprod As String,
                                              _cgcbarra As String, _cgcdprod1 As String,
                                              _cgcdprod2 As String, _cggr1 As Integer, _cggr2 As Integer,
                                              _cggr3 As Integer, _cggr4 As Integer, _cgMed As Integer, _cgumin As Integer, _cgusup As Integer, _cgvsup As Double, _cgsmin As Integer, _cgap As Integer, _cgimg As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '     @cgnumi,@cgcprod,@cgcbarra,@cgcdprod1,@cgcdprod2,@cggr1,@cggr2,
        '@cggr3,@cggr4,@cgMed,@cgumin,@cgusup,@cgvsup,@cgmstk,@cgclot,@cgsmin,
        '@cgap,@cgimg,@nprog,@tran,@newFecha,@newHora,@uact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cgnumi", _cgnumi))
        _listParam.Add(New Datos.DParametro("@cgcprod", _cgcprod))

        _listParam.Add(New Datos.DParametro("@cgcbarra", _cgcbarra))
        _listParam.Add(New Datos.DParametro("@cgcdprod1", _cgcdprod1))
        _listParam.Add(New Datos.DParametro("@cgcdprod2", _cgcdprod2))
        _listParam.Add(New Datos.DParametro("@cggr1", _cggr1))
        _listParam.Add(New Datos.DParametro("@cggr2", _cggr2))
        _listParam.Add(New Datos.DParametro("@cggr3", _cggr3))
        _listParam.Add(New Datos.DParametro("@cggr4", _cggr4))
        _listParam.Add(New Datos.DParametro("@cgMed", _cgMed))
        _listParam.Add(New Datos.DParametro("@cgumin", _cgumin))
        _listParam.Add(New Datos.DParametro("@cgusup", _cgusup))
        _listParam.Add(New Datos.DParametro("@cgvsup", _cgvsup))
        _listParam.Add(New Datos.DParametro("@cgmstk", 0))
        _listParam.Add(New Datos.DParametro("@cgclot", 0))

        _listParam.Add(New Datos.DParametro("@cgsmin", _cgsmin))
        _listParam.Add(New Datos.DParametro("@cgap", _cgap))
        _listParam.Add(New Datos.DParametro("@cgimg", _cgimg))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _Tabla = D_ProcedimientoConParamHistorial("sp_Mam_HC008", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region



#Region "COMBUSTIBLE"

    Public Shared Function L_prCombustibleGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA004", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prCombustibleGrabar(ByRef _numi As String, _desc As String, _precio As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@agdesc", _desc))
        _listParam.Add(New Datos.DParametro("@agprecio", _precio))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prCombustibleGrabarHistorial(_numi, _desc, _precio, "COMBUSTIBLE", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCombustibleModificar(_numi As String, _desc As String, _precio As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@agnumi", _numi))
        _listParam.Add(New Datos.DParametro("@agdesc", _desc))
        _listParam.Add(New Datos.DParametro("@agprecio", _precio))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TA004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prCombustibleGrabarHistorial(_numi, _desc, _precio, "COMBUSTIBLE", 2)

        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCombustibleBorrar(_numi As String, _desc As String, _precio As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TA004", "agnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@agnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TA004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prCombustibleGrabarHistorial(_numi, _desc, _precio, "COMBUSTIBLE", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCombustibleGrabarHistorial(_numi As String, _desc As String, _precio As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@agnumi", _numi))
        _listParam.Add(New Datos.DParametro("@agdesc", _desc))
        _listParam.Add(New Datos.DParametro("@agprecio", _precio))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParamHistorial("sp_dg_HA004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True

        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "MOVIMIENTOS"

    Public Shared Function L_prMovimientoGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoListarProductos(_dt As DataTable, _codAlmacen As Integer, _codSector As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TI0021", "", _dt))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@sector", _codSector))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoListarProductoKardex() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prListarConceptos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleMovimiento(numi As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoChoferABMDetalle(numi As String, Type As Integer, detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", Type))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TI0021", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoChoferGrabar(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String, _almacen As Integer, _depositoDestino As Integer, _ibidOrigen As Integer) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibdepdest", _depositoDestino))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))
        _listParam.Add(New Datos.DParametro("@ibidOrigen", _ibidOrigen))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prMovimientoModificar(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String, _almacen As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))

        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prMovimientoEliminar(numi As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region
#Region "TS002 Dosificacion"
    Public Shared Function L_prDosificacionGrabarHistorial(ByRef numi As String, cia As Integer, alm As String, sfc As String,
                                                  autoriz As String, nfac As Double, key As String, fdel As String,
                                                  fal As String, nota As String, nota2 As String, est As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@cia", cia))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@sfc", sfc))
        _listParam.Add(New Datos.DParametro("@autoriz", autoriz))
        _listParam.Add(New Datos.DParametro("@nfac", nfac))
        _listParam.Add(New Datos.DParametro("@key", key))
        _listParam.Add(New Datos.DParametro("@fdel", fdel))
        _listParam.Add(New Datos.DParametro("@fal", fal))
        _listParam.Add(New Datos.DParametro("@nota", nota))
        _listParam.Add(New Datos.DParametro("@nota2", nota2))
        _listParam.Add(New Datos.DParametro("@est", est))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _Tabla = D_ProcedimientoConParamHistorial("sp_Mam_HC009", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnEliminarDosificacion(numi As String, ByRef mensaje As String, cia As Integer, alm As String, sfc As String,
                                                     autoriz As String, nfac As Double, key As String, fdel As String,
                                                     fal As String, nota As String, nota2 As String, est As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TS002", "sbnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@numi", numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
                                                 autoriz, nfac, key, fdel,
                                                 fal, nota, nota2, est, "DOSIFICACION", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnGrabarDosificacion(ByRef numi As String, cia As Integer, alm As String, sfc As String,
                                                  autoriz As Double, nfac As Double, key As String, fdel As String,
                                                  fal As String, nota As String, nota2 As String, est As String, tipo As Integer, inicio As Integer, final As Integer, modulo As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@cia", cia))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@sfc", sfc))
        If (tipo = 1) Then
            _listParam.Add(New Datos.DParametro("@autoriz", autoriz))
        Else
            _listParam.Add(New Datos.DParametro("@autoriz", 0))
        End If


        _listParam.Add(New Datos.DParametro("@nfac", nfac))
        _listParam.Add(New Datos.DParametro("@key", key))
        _listParam.Add(New Datos.DParametro("@fdel", fdel))
        _listParam.Add(New Datos.DParametro("@fal", fal))
        _listParam.Add(New Datos.DParametro("@nota", nota))
        _listParam.Add(New Datos.DParametro("@nota2", nota2))
        _listParam.Add(New Datos.DParametro("@est", est))
        _listParam.Add(New Datos.DParametro("@sbtipo", tipo))
        _listParam.Add(New Datos.DParametro("@inicio", inicio))
        _listParam.Add(New Datos.DParametro("@fin", final))
        _listParam.Add(New Datos.DParametro("@modulo", modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
                                                  autoriz, nfac, key, fdel,
                                                  fal, nota, nota2, est, "DOSIFICACION", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarDosificacion(ByRef numi As String, cia As Integer, alm As String, sfc As String,
                                                     autoriz As String, nfac As Double, key As String, fdel As String,
                                                     fal As String, nota As String, nota2 As String, est As String, tipo As Integer, inicio As Integer, final As Integer, modulo As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@cia", cia))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@sfc", sfc))
        _listParam.Add(New Datos.DParametro("@autoriz", autoriz))
        _listParam.Add(New Datos.DParametro("@nfac", nfac))
        _listParam.Add(New Datos.DParametro("@key", key))
        _listParam.Add(New Datos.DParametro("@fdel", fdel))
        _listParam.Add(New Datos.DParametro("@fal", fal))
        _listParam.Add(New Datos.DParametro("@nota", nota))
        _listParam.Add(New Datos.DParametro("@nota2", nota2))
        _listParam.Add(New Datos.DParametro("@est", est))
        _listParam.Add(New Datos.DParametro("@sbtipo", tipo))
        _listParam.Add(New Datos.DParametro("@inicio", inicio))
        _listParam.Add(New Datos.DParametro("@fin", final))
        _listParam.Add(New Datos.DParametro("@modulo", modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
                                                 autoriz, nfac, key, fdel,
                                                 fal, nota, nota2, est, "DOSIFICACION", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDosificacionObtenerDatosSucursal(numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarModulos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarCompaniaDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnListarAlmacenDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarAlmacenQueTenganDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarAlmacenQueTenganDosificacionprueba(ByRef banderas As Boolean) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)
        If (_Tabla.Columns.Count <= 0) Then
            banderas = False
        Else
            banderas = True
        End If
        Return _Tabla
    End Function

#End Region


#Region "VENTA LAVADERO"


    '*****codigo danny
    Public Shared Function L_fnVentaLavaderoObtenerEstadoServ(numiServ As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@numiServ", numiServ))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    '********************
    Public Shared Function L_fnEliminarAsientoContable(_vcnumi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(_vcnumi, "TI005", "ifnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ifnumi", _vcnumi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                'L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
                '                                 autoriz, nfac, key, fdel,
                '                                 fal, nota, nota2, est, "DOSIFICACION", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnEliminarVentaLavadero(_vcnumi As String, ByRef mensaje As String, _vcsector As Integer, _vcSecNumi As Integer, _vcnumivehic As Integer, _vcalm As Integer, _vcfdoc As String, _vcclie As Integer, _vcfvcr As String, _vctipo As Integer, _vcest As Integer, _vcobs As String, _vcdesc As Double, _vctotal As Decimal, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(_vcnumi, "TV002", "vcnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@vcnumi", _vcnumi))
            _listParam.Add(New Datos.DParametro("@vcsector", _vcsector))
            _listParam.Add(New Datos.DParametro("@vcfdoc", _vcfdoc))
            _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                'L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
                '                                 autoriz, nfac, key, fdel,
                '                                 fal, nota, nota2, est, "DOSIFICACION", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_AnularVenta(_vcnumi As String, _vcfdoc As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(_vcnumi, "TV002", "vcnumi", "") = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -2))
            _listParam.Add(New Datos.DParametro("@vcnumi", _vcnumi))
            _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
            _listParam.Add(New Datos.DParametro("@vcfdoc", _vcfdoc))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                'L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
                '                                 autoriz, nfac, key, fdel,
                '                                 fal, nota, nota2, est, "DOSIFICACION", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnGrabarVentaLavadero(ByRef _vcnumi As String, _vcsector As Integer, _vcSecNumi As String, _vcnumivehic As Integer, _vcalm As Integer, _vcfdoc As String, _vcclie As Integer, _vcfvcr As String, _vctipo As Integer, _vcest As Integer, _vcobs As String, _vcdesc As Double, _vctotal As Decimal, _detalle As DataTable, _CodClienteCredito As Integer,
                                                   _dtDetalleVentas As DataTable, factura As Integer, factanul As Integer, _vcmoneda As Integer, _vcbanco As Integer) As Boolean
        Dim _resultado As Boolean
        ' vcnumi ,@vcidcore ,@vcsector ,@vcSecNumi ,@vcnumivehic ,@vcalm ,@vcfdoc ,@vcclie ,@vcfvcr ,@vctipo ,
        '@vcest ,@vcobs ,@vcdesc ,@vctotal 

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        If (_vcsector = -10 And _vcclie > 0) Then
            _listParam.Add(New Datos.DParametro("@tipo", 100))
        Else
            _listParam.Add(New Datos.DParametro("@tipo", 1))
        End If
        _listParam.Add(New Datos.DParametro("@vcnumi", _vcnumi))
        _listParam.Add(New Datos.DParametro("@vcidcore", 0))
        _listParam.Add(New Datos.DParametro("@vcsector", _vcsector))
        _listParam.Add(New Datos.DParametro("@vcSecNumi", _vcSecNumi))
        _listParam.Add(New Datos.DParametro("@vcnumivehic", _vcnumivehic))
        _listParam.Add(New Datos.DParametro("@vcalm", _vcalm))
        _listParam.Add(New Datos.DParametro("@vcfdoc", _vcfdoc))
        _listParam.Add(New Datos.DParametro("@vcclie", _vcclie))
        _listParam.Add(New Datos.DParametro("@vcfvcr", _vcfvcr))
        _listParam.Add(New Datos.DParametro("@vctipo", _vctipo))
        _listParam.Add(New Datos.DParametro("@vcest", _vcest))
        _listParam.Add(New Datos.DParametro("@vcobs", _vcobs))
        _listParam.Add(New Datos.DParametro("@vcdesc", _vcdesc))
        _listParam.Add(New Datos.DParametro("@vctotal", _vctotal))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ClienteCredito", _CodClienteCredito))
        _listParam.Add(New Datos.DParametro("@TV0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@vcfactura", factura))
        _listParam.Add(New Datos.DParametro("@vcfactanul", factanul))
        _listParam.Add(New Datos.DParametro("@vcmoneda", _vcmoneda))
        _listParam.Add(New Datos.DParametro("@vcbanco", _vcbanco))
        If (_vcsector = -10 And _vcclie > 0) Then
            _listParam.Add(New Datos.DParametro("@TV0023", "", _dtDetalleVentas))
        Else
            If (_vcsector <> -10) Then
                _listParam.Add(New Datos.DParametro("@TV0022", "", _dtDetalleVentas))
            End If

        End If
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        If _Tabla.Rows.Count > 0 And _Tabla.Rows(0).Item(0) <> -100 Then
            _vcnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
            'L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
            '                                      autoriz, nfac, key, fdel,
            '                                      fal, nota, nota2, est, "DOSIFICACION", 1)
        Else
            _vcnumi = _Tabla.Rows(0).Item(0)
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarVentaLavadero(ByRef _vcnumi As String, _vcsector As Integer, _vcSecNumi As Integer, _vcnumivehic As Integer, _vcalm As Integer, _vcfdoc As String, _vcclie As Integer, _vcfvcr As String, _vctipo As Integer, _vcest As Integer, _vcobs As String, _vcdesc As Double, _vctotal As Decimal, _detalle As DataTable, _CodClienteCredito As Integer, factura As Integer, factanul As Integer, _vcmoneda As Integer, _vcbanco As Integer) As Boolean
        Dim _resultado As Boolean
        ' vcnumi ,@vcidcore ,@vcsector ,@vcSecNumi ,@vcnumivehic ,@vcalm ,@vcfdoc ,@vcclie ,@vcfvcr ,@vctipo ,
        '@vcest ,@vcobs ,@vcdesc ,@vctotal 
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@vcnumi", _vcnumi))
        _listParam.Add(New Datos.DParametro("@vcidcore", 0))
        _listParam.Add(New Datos.DParametro("@vcsector", _vcsector))
        _listParam.Add(New Datos.DParametro("@vcSecNumi", _vcSecNumi))
        _listParam.Add(New Datos.DParametro("@vcnumivehic", _vcnumivehic))
        _listParam.Add(New Datos.DParametro("@vcalm", _vcalm))
        _listParam.Add(New Datos.DParametro("@vcfdoc", _vcfdoc))
        _listParam.Add(New Datos.DParametro("@vcclie", _vcclie))
        _listParam.Add(New Datos.DParametro("@vcfvcr", _vcfvcr))
        _listParam.Add(New Datos.DParametro("@vctipo", _vctipo))
        _listParam.Add(New Datos.DParametro("@vcest", _vcest))
        _listParam.Add(New Datos.DParametro("@vcobs", _vcobs))
        _listParam.Add(New Datos.DParametro("@vcdesc", _vcdesc))
        _listParam.Add(New Datos.DParametro("@vctotal", _vctotal))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ClienteCredito", _CodClienteCredito))
        _listParam.Add(New Datos.DParametro("@TV0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@vcfactura", factura))
        _listParam.Add(New Datos.DParametro("@vcfactanul", factanul))
        _listParam.Add(New Datos.DParametro("@vcmoneda", _vcmoneda))
        _listParam.Add(New Datos.DParametro("@vcbanco", _vcbanco))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _vcnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
            'L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
            '                                      autoriz, nfac, key, fdel,
            '                                      fal, nota, nota2, est, "DOSIFICACION", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralVentaLavadero() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerNumiServicioAnulado() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerExisteFactura(sucursal As Integer, nroFactura As Integer, FechaI As String, FechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@sucursal", sucursal))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@numerofactura", nroFactura))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerMaximoNroFacturaManual(sucursal As Integer, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@sucursal", sucursal))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEsDosificacionManual(sucursal As Integer, fechaFactura As String, modulo As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@sucursal", sucursal))
        _listParam.Add(New Datos.DParametro("@vcfdoc", fechaFactura))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@modulo", modulo))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_DosificacionAutomatica(sucursal As Integer, fechaFactura As String, modulo As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@sucursal", sucursal))
        _listParam.Add(New Datos.DParametro("@vcfdoc", fechaFactura))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@modulo", modulo))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_ObtenerActividadEconomica(modulo As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@modulo", modulo))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralLibreriaLavadero() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarVentaLavadero(dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0022", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarVentaLavaderoProductos(dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0022", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarVentaRemolque(dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0022", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarVentaCabañas(dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0023", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleAyudaLavadero(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@numiVenta", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleAyudaRemolque(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@numiVenta", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleLavadero(numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@vcnumi", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarServicios(tipo As String, dt As DataTable, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tipoC", tipo))
        _listParam.Add(New Datos.DParametro("@TV0021", "", dt))
        _listParam.Add(New Datos.DParametro("@sucursalC", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnVerificarSiExisteDosificacionFactura(Sucursal As Integer, TipoFactura As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@sucursal", Sucursal))
        _listParam.Add(New Datos.DParametro("@tipoFactura", TipoFactura))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarNroOrdenesLavadero(_numi As String, tipo As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", tipo))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@vcnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnFacturaLavadero(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@vcnumi", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnConfSistemaGeneral() As DataTable
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        Dim _dtConfSist As DataTable = D_Datos_Tabla("cnumi", "TC000", "1=1")

        _Where = "ccctc0=" + _dtConfSist.Rows(0).Item("cnumi").ToString
        _campos = "*"
        _Tabla = D_Datos_Tabla(_campos, "TC0001", _Where)
        Return _Tabla
    End Function

    'ventas danny
    Public Shared Function L_prVentaReporteGeneral(_fechaDel As String, _fechaAl As String, _numiSector As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fecha1", _fechaDel))
        _listParam.Add(New Datos.DParametro("@fecha2", _fechaAl))
        _listParam.Add(New Datos.DParametro("@vcsector", _numiSector))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_dg_TV002", _listParam)

        Return _Tabla
    End Function

#End Region


#Region "Facturar"

    Public Shared Sub L_Grabar_Factura(_Numi As String, _Fecha As String, _Nfac As String, _NAutoriz As String, _Est As String,
                                       _NitCli As String, _CodCli As String, _DesCli1 As String, _DesCli2 As String,
                                       _A As String, _B As String, _C As String, _D As String, _E As String, _F As String,
                                       _G As String, _H As String, _CodCon As String, _FecLim As String,
                                       _Imgqr As String, _Alm As String, _Numi2 As String)
        Dim Sql As String
        Try
            Sql = "" + _Numi + ", " _
                + "'" + _Fecha + "', " _
                + "" + _Nfac + ", " _
                + "" + _NAutoriz + ", " _
                + "" + _Est + ", " _
                + "'" + _NitCli + "', " _
                + "" + _CodCli + ", " _
                + "'" + _DesCli1 + "', " _
                + "'" + _DesCli2 + "', " _
                + "" + _A + ", " _
                + "" + _B + ", " _
                + "" + _C + ", " _
                + "" + _D + ", " _
                + "" + _E + ", " _
                + "" + _F + ", " _
                + "" + _G + ", " _
                + "" + _H + ", " _
                + "'" + _CodCon + "', " _
                + "'" + _FecLim + "', " _
                + "" + _Imgqr + ", " _
                + "" + _Alm + ", " _
                + "" + _Numi2 + ""

            D_Insertar_Datos("TFV001", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub L_Modificar_Factura(Where As String, Optional _Fecha As String = "",
                                          Optional _Nfact As String = "", Optional _NAutoriz As String = "",
                                          Optional _Est As String = "", Optional _NitCli As String = "",
                                          Optional _CodCli As String = "", Optional _DesCli1 As String = "",
                                          Optional _DesCli2 As String = "", Optional _A As String = "",
                                          Optional _B As String = "", Optional _C As String = "",
                                          Optional _D As String = "", Optional _E As String = "",
                                          Optional _F As String = "", Optional _G As String = "",
                                          Optional _H As String = "", Optional _CodCon As String = "",
                                          Optional _FecLim As String = "", Optional _Imgqr As String = "",
                                          Optional _Alm As String = "", Optional _Numi2 As String = "")
        Dim Sql As String
        Try
            Sql = IIf(_Fecha.Equals(""), "", "fvafec = '" + _Fecha + "', ") +
              IIf(_Nfact.Equals(""), "", "fvanfac = " + _Nfact + ", ") +
              IIf(_NAutoriz.Equals(""), "", "fvaautoriz = " + _NAutoriz + ", ") +
              IIf(_Est.Equals(""), "", "fvaest = " + _Est) +
              IIf(_NitCli.Equals(""), "", "fvanitcli = '" + _NitCli + "', ") +
              IIf(_CodCli.Equals(""), "", "fvacodcli = " + _CodCli + ", ") +
              IIf(_DesCli1.Equals(""), "", "fvadescli1 = '" + _DesCli1 + "', ") +
              IIf(_DesCli2.Equals(""), "", "fvadescli2 = '" + _DesCli2 + "', ") +
              IIf(_A.Equals(""), "", "fvastot = " + _A + ", ") +
              IIf(_B.Equals(""), "", "fvaimpsi = " + _B + ", ") +
              IIf(_C.Equals(""), "", "fvaimpeo = " + _C + ", ") +
              IIf(_D.Equals(""), "", "fvaimptc = " + _D + ", ") +
              IIf(_E.Equals(""), "", "fvasubtotal = " + _E + ", ") +
              IIf(_F.Equals(""), "", "fvadesc = " + _F + ", ") +
              IIf(_G.Equals(""), "", "fvatotal = " + _G + ", ") +
              IIf(_H.Equals(""), "", "fvadebfis = " + _H + ", ") +
              IIf(_CodCon.Equals(""), "", "fvaccont = '" + _CodCon + "', ") +
              IIf(_FecLim.Equals(""), "", "fvaflim = '" + _FecLim + "', ") +
              IIf(_Imgqr.Equals(""), "", "fvaimgqr = '" + _Imgqr + "', ") +
              IIf(_Alm.Equals(""), "", "fvaalm = " + _Alm + ", ") +
              IIf(_Numi2.Equals(""), "", "fvanumi2 = " + _Numi2 + ", ")
            Sql = Sql.Trim
            If (Sql.Substring(Sql.Length - 1, 1).Equals(",")) Then
                Sql = Sql.Substring(0, Sql.Length - 1)
            End If

            D_Modificar_Datos("TFV001", Sql, Where)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Shared Sub L_Grabar_Factura_Detalle(_Numi As String, _CodProd As String, _DescProd As String, _Cant As String, _Precio As String, _Numi2 As String)
        Dim Sql As String
        Try
            Sql = _Numi + ", '" + _CodProd + "', '" + _DescProd + "', " + _Cant + ", " + _Precio + ", " + _Numi2

            D_Insertar_Datos("TFV0011", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Function L_Reporte_Factura(_Numi As String, _Numi2 As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvanumi = " + _Numi + " and fvanumi2 = " + _Numi2

        _Tabla = D_Datos_Tabla("*", "VR_GO_Factura", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_Reporte_Factura_Cia(_Cia As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " scnumi = " + _Cia

        _Tabla = D_Datos_Tabla("*", "TS003", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_ObtenerRutaImpresora(_NroImp As String, Optional tImp As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If (Not _NroImp.Trim.Equals("")) Then
            _Where = " cbnumi = " + _NroImp + " and cbest = 1 order by cbnumi"
        Else
            _Where = " cbtimp = " + tImp + " and cbest = 1 order by cbnumi"
        End If
        _Tabla = D_Datos_Tabla("*", "TS004", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_fnGetIVA() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1 = 1"
        _Tabla = D_Datos_Tabla("scdebfis", "TS003", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_fnGetICE() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1 = 1"
        _Tabla = D_Datos_Tabla("scice", "TS003", _Where)
        Return _Tabla
    End Function

    Public Shared Sub L_Grabar_Nit(_Nit As String, _Nom1 As String, _Nom2 As String)
        Dim _Err As Boolean
        Dim _Nom01, _Nom02 As String
        Dim Sql As String
        _Nom01 = ""
        _Nom02 = ""
        L_Validar_Nit(_Nit, _Nom01, _Nom02)

        If _Nom01 = "" Then
            Sql = _Nit + ", '" + _Nom1 + "', '" + _Nom2 + "'"
            _Err = D_Insertar_Datos("TS001", Sql)
        Else
            If (_Nom1 <> _Nom01) Or (_Nom2 <> _Nom02) Then
                Sql = "sanom1 = '" + _Nom1 + "' " +
                      IIf(_Nom02.ToString.Trim.Equals(""), "", ", sanom2 = '" + _Nom2 + "', ")
                _Err = D_Modificar_Datos("TS001", Sql, "sanit = " + "'" + _Nit + "'")
            End If
        End If
    End Sub

    Public Shared Function L_Validar_Nit(_Nit As String, ByRef _Nom1 As String, ByRef _Nom2 As String) As Boolean
        Dim _Tabla As DataTable

        _Tabla = D_Datos_Tabla("*", "TS001", "sanit = '" + _Nit + "'")

        If _Tabla.Rows.Count > 0 Then
            _Nom1 = _Tabla.Rows(0).Item(2)
            _Nom2 = IIf(_Tabla.Rows(0).Item(3).ToString.Trim.Equals(""), "", _Tabla.Rows(0).Item(3))
            Return True
        End If
        Return False
    End Function

    Public Shared Function L_Eliminar_Nit(_Nit As String) As Boolean
        Dim res As Boolean = False
        Try
            res = D_Eliminar_Datos("TS001", "sanit = " + _Nit)
        Catch ex As Exception
            res = False
        End Try
        Return res
    End Function

    Public Shared Function L_Dosificacion(_cia As String, _alm As String, _fecha As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        '_fecha = Now.Date.ToString("yyyy/MM/dd")
        _Where = "sbcia = " + _cia + " AND sbalm = " + _alm + " AND sbfdel <= '" + _fecha + "' AND sbfal >= '" + _fecha + "' AND sbest = 1 and sbtipo=1"

        _Tabla = D_Datos_Tabla("*", "TS002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Sub L_Actualiza_Dosificacion(_Numi As String, _NumFac As String, _Numi2 As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "sbnfac = " + _NumFac
        _where = "sbnumi = " + _Numi

        _Err = D_Modificar_Datos("TS002", Sql, _where)
    End Sub

    Public Shared Function L_fnObtenerMaxIdTabla(tabla As String, campo As String, where As String) As Long
        Dim Dt As DataTable = New DataTable
        Dt = D_Maximo(tabla, campo, where)

        If (Dt.Rows.Count > 0) Then
            If (Dt.Rows(0).Item(0).ToString.Equals("")) Then
                Return 0
            Else
                Return CLng(Dt.Rows(0).Item(0).ToString)
            End If
        Else
            Return 0
        End If
    End Function

    Public Shared Function L_fnObtenerTabla(tabla As String, campo As String, where As String) As DataTable
        Dim Dt As DataTable = D_Datos_Tabla(campo, tabla, where)
        Return Dt
    End Function

    Public Shared Function L_fnObtenerDatoTabla(tabla As String, campo As String, where As String) As String
        Dim Dt As DataTable = D_Datos_Tabla(campo, tabla, where)
        If (Dt.Rows.Count > 0) Then
            Return Dt.Rows(0).Item(campo).ToString
        Else
            Return ""
        End If
    End Function

    Public Shared Function L_fnEliminarDatos(Tabla As String, Where As String) As Boolean
        Return D_Eliminar_Datos(Tabla, Where)
    End Function

#End Region
#Region "Anular Factura"

    Public Shared Function L_Obtener_Facturas(almacen As String, factura As Integer) As DataSet
        Dim _Tabla1 As New DataTable
        Dim _Tabla2 As New DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvaalm =" + almacen + " and fvanumi=vcnumi and vcfactura=" + Str(factura)
        'Cambiar la logica para visualizar las facturas esto en el programa de facturas
        _Tabla1 = D_Datos_Tabla("concat(fvanfac, '_', fvaautoriz) as Archivo, fvanumi as Codigo, fvanfac as [Nro Factura], " _
                                + "fvafec as Fecha, fvacodcli as [Cod Cliente], " _
                                + " fvadescli1 as [Nombre 1], fvadescli2 as [Nombre 2], fvanitcli as Nit, " _
                                + " fvastot as Subtotal, fvadesc as Descuento, fvatotal as Total, " _
                                + " fvaccont as [Cod Control], fvaflim as [Fec Limite], fvaest as Estado",
                                "TFV001,TV002", _Where)
        '_Tabla1.Columns(0).ColumnMapping = MappingType.Hidden
        _Ds.Tables.Add(_Tabla1)

        _Tabla2 = D_Datos_Tabla("concat(fvanfac, '_', fvaautoriz) as Archivo, fvbnumi as Codigo, fvbcprod as [Cod Producto], fvbdesprod as Descripcion, " _
                                + " fvbcant as Cantidad, fvbprecio as [Precio Unitario], (fvbcant * fvbprecio) as Precio",
                                "TFV001, TFV0011,TV002", "fvanumi = fvbnumi and fvanumi2 = fvbnumi2 and fvanumi=vcnumi and vcfactura=" + Str(factura))
        _Ds.Tables.Add(_Tabla2)
        _Ds.Relations.Add("1", _Tabla1.Columns("Archivo"), _Tabla2.Columns("Archivo"), False)
        Return _Ds
    End Function

    Public Shared Function L_ObtenerDetalleFactura(_CodFact As String) As DataSet 'Modifcar para que solo Traiga los productos Con Stock
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = "fvbnumi = " + _CodFact
        _Tabla = D_Datos_Tabla("fvbcprod as codP, fvbcant as can, '1' as sto", "TFV0011", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

#End Region

#Region "Libro de ventas"

    Public Shared Function L_fnObtenerLibroVenta(_CodAlm As String, _fechai As String, _FechaF As String, factura As Integer, modulo As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = ""
        If _CodAlm > 0 Then
            _Where = "modulo = " + modulo + " and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"
        End If
        If _CodAlm = 0 Then 'todas las sucursales
            _Where = "modulo = " + modulo + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"
        End If
        If _CodAlm = -1 Then 'todas las sucursales menos la principal
            _Where = "modulo = " + modulo + " and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"
        End If
        Dim _select As String = "fvanumi, FORMAT(fvafec,'dd/MM/yyyy') as fvafec, fvanfac, fvaautoriz,fvaest, fvanitcli, fvadescli, fvastot, fvaimpsi, fvaimpeo, fvaimptc, fvasubtotal, fvadesc, fvatotal, fvadebfis, fvaccont,fvaflim,fvaalm,scneg, factura"

        _Tabla = D_Datos_Tabla(_select,
                               "VR_GO_LibroVenta", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_Comprobante(_numi As String)
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@Numi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_dg_validaciones", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_hijoscuenta(_numi As String)
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@Numi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_dg_validaciones", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLibroVentaAmbosTipoFactura(_CodAlm As String, _fechai As String, _FechaF As String, modulo As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = ""

        'If _CodAlm > 0 Then
        '    _Where = " fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanumi desc"
        'End If
        'If _CodAlm = 0 Then 'todas las sucursales
        '    _Where = " fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanumi desc"
        'End If
        'If _CodAlm = -1 Then 'todas las sucursales menos la principal
        '    _Where = " fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanumi desc"
        'End If  CFO 27-02-20

        If _CodAlm > 0 Then
            _Where = "modulo = " + modulo + " and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanumi desc"
        End If
        If _CodAlm = 0 Then 'todas las sucursales
            _Where = "modulo = " + modulo + " and  fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanumi desc"
        End If
        If _CodAlm = -1 Then 'todas las sucursales menos la principal
            _Where = "modulo = " + modulo + " and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanumi"
        End If  'CFO 13-02-20,  CFO 28-02-20 cambiar a modulo

        Dim _select As String = "fvanumi, FORMAT(fvafec,'dd/MM/yyyy') as fvafec, fvanfac, fvaautoriz,fvaest, fvanitcli, fvadescli, fvastot, fvaimpsi, fvaimpeo, fvaimptc, fvasubtotal, fvadesc, fvatotal, fvadebfis, fvaccont,fvaflim,fvaalm,scneg, factura"

        _Tabla = D_Datos_Tabla(_select,
                               "VR_GO_LibroVenta", _Where)
        Return _Tabla


    End Function
    Public Shared Function L_SumarSucursalTotal(modulo As String, codalm As String, fechai As String, fechaf As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@modulo", modulo))
        _listParam.Add(New Datos.DParametro("@alm", codalm))
        _listParam.Add(New Datos.DParametro("@fdel", fechai))
        _listParam.Add(New Datos.DParametro("@fal", fechaf))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_SumarModuloTotal(modulo As String, codalm As String, fechai As String, fechaf As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@modulo", modulo))
        _listParam.Add(New Datos.DParametro("@alm", codalm))
        _listParam.Add(New Datos.DParametro("@fdel", fechai))
        _listParam.Add(New Datos.DParametro("@fal", fechaf))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_ObtenerAnhoFactura() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "1 = 1 ORDER BY year(fvafec)"
        _Tabla = D_Datos_Tabla("Distinct(year(fvafec)) AS anho",
                               "VR_GO_LibroVenta", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_ObtenerSucursalesFactura() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "1 = 1 ORDER BY a.scneg"
        _Tabla = D_Datos_Tabla("a.scnumi, a.scneg, a.scnit",
                               "TS003 a", _Where)
        Return _Tabla
    End Function

#End Region


#Region "Kardex de Inventario"

    Public Shared Sub L_Actualizar_SaldoInventario(cod As String, cant As String, alm As String)
        'verificamos si es que ya existe el codigo del producto en la tabla TI001
        Dim _Err As Boolean
        Dim _where As String = "iccprod =" + cod + " and icalm = " + alm
        _Err = D_Modificar_Datos("TI001", "iccven = " + cant, _where)
    End Sub

    Shared Function L_VistaKardexInventario(cod As String, fIni As String, fFin As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String

        If (cod.Equals("-1")) Then
            _Where = "cprod = " + cod + " And fdoc >= '" + fIni + "' And fdoc <= '" + fFin + "'"
        Else
            _Where = "cprod = " + cod + " And fdoc >= '" + fIni + "' And fdoc <= '" + fFin + "'"
        End If

        Dim campos As String = "*"
        _Tabla = D_Datos_Tabla(campos, "VR_KardexInventario", _Where + " order by fdoc")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Shared Function L_VistaKardexInventarioTodo(cod As String, Optional fFin As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String = "cprod = " + cod + IIf(fFin.Equals(""), "", " And fdoc < '" + fFin + "'")
        Dim campos As String = "*"
        _Tabla = D_Datos_Tabla(campos, "VR_KardexInventario", _Where + " order by fdoc")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Shared Function L_VistaStockActual(Optional _where1 As String = "") As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        If _where1 = String.Empty Then
            _Where = "1=1"
        Else
            _Where = _where1
        End If

        Dim campos As String = "*"
        _Tabla = D_Datos_Tabla(campos, "VR_stockActual", _Where + " order by ldcdprod1")
        Return _Tabla
    End Function

#End Region


#Region "SERVICIOSSS"

    Public Shared Function L_prServicioGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@sduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TS005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prServiciosGrabar(ByRef _sdnumi As String, _sdcprod As String, _sddesc As String, _sdprec As Double, _sdtipo As Integer, _sdsuc As Integer, _sdest As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@sdnumi ,@sdcod ,@sddesc ,@sdprec ,@sdtipo ,@sdsuc,@sdest
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@sdnumi", _sdnumi))
        _listParam.Add(New Datos.DParametro("@sdcod", _sdcprod))
        _listParam.Add(New Datos.DParametro("@sddesc", _sddesc))
        _listParam.Add(New Datos.DParametro("@sdprec", _sdprec))
        _listParam.Add(New Datos.DParametro("@sdtipo", _sdtipo))
        _listParam.Add(New Datos.DParametro("@sdsuc", _sdsuc))
        _listParam.Add(New Datos.DParametro("@sdest", _sdest))
        _listParam.Add(New Datos.DParametro("@sduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TS005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _sdnumi = _Tabla.Rows(0).Item(0)
            _resultado = True

            L_prServiciosGrabarHistorial(_sdnumi, _sdcprod, _sddesc, _sdprec, _sdtipo, _sdsuc, _sdest, "SERVICIOS", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prServicioModificar(ByRef _sdnumi As String, _sdcprod As String, _sddesc As String, _sdprec As Double, _sdtipo As Integer, _sdsuc As Integer, _sdest As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@sdnumi", _sdnumi))
        _listParam.Add(New Datos.DParametro("@sdcod", _sdcprod))
        _listParam.Add(New Datos.DParametro("@sddesc", _sddesc))
        _listParam.Add(New Datos.DParametro("@sdprec", _sdprec))
        _listParam.Add(New Datos.DParametro("@sdtipo", _sdtipo))
        _listParam.Add(New Datos.DParametro("@sdsuc", _sdsuc))
        _listParam.Add(New Datos.DParametro("@sdest", _sdest))
        _listParam.Add(New Datos.DParametro("@sduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TS005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prServiciosGrabarHistorial(_sdnumi, _sdcprod, _sddesc, _sdprec, _sdtipo, _sdsuc, _sdest, "PRODUCTOS INVENTARIO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prServiciosBorrar(ByRef _sdnumi As String, _sdcprod As String, _sddesc As String, _sdprec As Double, _sdtipo As Integer, _sdsuc As Integer, _sdest As Integer, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_sdnumi, "TS005", "sdnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@sdnumi", _sdnumi))


            _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TS005", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prServiciosGrabarHistorial(_sdnumi, _sdcprod, _sddesc, _sdprec, _sdtipo, _sdsuc, _sdest, "SERVICIO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prServiciosGrabarHistorial(ByRef _sdnumi As String, _sdcprod As String, _sddesc As String, _sdprec As Double, _sdtipo As Integer, _sdsuc As Integer, _sdest As Integer, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '     @cgnumi,@cgcprod,@cgcbarra,@cgcdprod1,@cgcdprod2,@cggr1,@cggr2,
        '@cggr3,@cggr4,@cgMed,@cgumin,@cgusup,@cgvsup,@cgmstk,@cgclot,@cgsmin,
        '@cgap,@cgimg,@nprog,@tran,@newFecha,@newHora,@uact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@sdnumi", _sdnumi))
        _listParam.Add(New Datos.DParametro("@sdcod", _sdcprod))
        _listParam.Add(New Datos.DParametro("@sddesc", _sddesc))
        _listParam.Add(New Datos.DParametro("@sdprec", _sdprec))
        _listParam.Add(New Datos.DParametro("@sdtipo", _sdtipo))
        _listParam.Add(New Datos.DParametro("@sdsuc", _sdsuc))
        _listParam.Add(New Datos.DParametro("@sdest", _sdest))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _Tabla = D_ProcedimientoConParamHistorial("sp_Mam_HC010", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region


#Region "CLIENTES TC009"
    Public Shared Function L_prClientesComprobante(_tipo As String, numiCuenta As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@cjtipo", _tipo))
        _listParam.Add(New Datos.DParametro("@cjnumitc1", numiCuenta))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC009", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prClientesBanco() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC009", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prClientesPorCobrarCaja(_tipo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@cjtipo", _tipo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC009", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prNumiCuentaCobrarSY000() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC009", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prClientesComprobanteGrabar(ByRef _numi As String, _ci As String, _nombre As String, _tipo As String, numiCuenta As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@cjci", _ci))
        _listParam.Add(New Datos.DParametro("@cjnombre", _nombre))
        _listParam.Add(New Datos.DParametro("@cjtipo", _tipo))
        _listParam.Add(New Datos.DParametro("@cjnumitc1", numiCuenta))


        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC009", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            'L_prMaquinaGrabarHistorial(_numi, _desc, _estado, "MAQUINA", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prClientesComprobanteModificarNombre(_numi As String, _nombre As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@cjnumi", _numi))
        _listParam.Add(New Datos.DParametro("@cjnombre", _nombre))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC009", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            'L_prMaquinaGrabarHistorial(_numi, _desc, _estado, "MAQUINA", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region



#Region "MARCO REGISTRO TIPO ACTIVO FIJO "

    Public Shared Function L_prActivoFijoGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@icuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI003", _listParam)

        Return _Tabla
    End Function




    Public Shared Function L_prActivoFijoGrabar(ByRef _icnumi As String, _icnom As String, _icnomc As String, _icvidautil As Double, _icimg As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@icnumi ,@icnom ,@icnomc ,@icvidautil ,@icimg 
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@icnumi", _icnumi))
        _listParam.Add(New Datos.DParametro("@icnom", _icnom))

        _listParam.Add(New Datos.DParametro("@icnomc", _icnomc))
        _listParam.Add(New Datos.DParametro("@icvidautil", _icvidautil))
        _listParam.Add(New Datos.DParametro("@icimg", _icimg))
        _listParam.Add(New Datos.DParametro("@icuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _icnumi = _Tabla.Rows(0).Item(0)
            _resultado = True

            L_prActivoFijoGrabarHistorial(_icnumi, _icnom, _icnomc, _icvidautil, _icimg, "TIPO ACTIVO FIJO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prActivoFijoModificar(ByRef _icnumi As String, _icnom As String, _icnomc As String, _icvidautil As Double, _icimg As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@icnumi", _icnumi))
        _listParam.Add(New Datos.DParametro("@icnom", _icnom))

        _listParam.Add(New Datos.DParametro("@icnomc", _icnomc))
        _listParam.Add(New Datos.DParametro("@icvidautil", _icvidautil))
        _listParam.Add(New Datos.DParametro("@icimg", _icimg))
        _listParam.Add(New Datos.DParametro("@icuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prActivoFijoGrabarHistorial(_icnumi, _icnom, _icnomc, _icvidautil, _icimg, " TIPO ACTIVO FIJO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prActivoFijoBorrar(_icnumi As String, _icnom As String, _icnomc As String, _icvidautil As Double, _icimg As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_icnumi, "TI003", "icnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@icnumi", _icnumi))
            _listParam.Add(New Datos.DParametro("@icuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TI003", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prActivoFijoGrabarHistorial(_icnumi, _icnom, _icnomc, _icvidautil, _icimg, "TIPO ACTIVO FIJO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prActivoFijoGrabarHistorial(ByRef _icnumi As String, _icnom As String, _icnomc As String, _icvidautil As Double, _icimg As String, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '     @cgnumi,@cgcprod,@cgcbarra,@cgcdprod1,@cgcdprod2,@cggr1,@cggr2,
        '@cggr3,@cggr4,@cgMed,@cgumin,@cgusup,@cgvsup,@cgmstk,@cgclot,@cgsmin,
        '@cgap,@cgimg,@nprog,@tran,@newFecha,@newHora,@uact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@icnumi", _icnumi))
        _listParam.Add(New Datos.DParametro("@icnom", _icnom))

        _listParam.Add(New Datos.DParametro("@icnomc", _icnomc))
        _listParam.Add(New Datos.DParametro("@icvidautil", _icvidautil))
        _listParam.Add(New Datos.DParametro("@icimg", _icimg))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _Tabla = D_ProcedimientoConParamHistorial("sp_Mam_HI001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region



#Region "REGISTRO  ACTIVO FIJO "

    Public Shared Function L_prRegistroActivoFijoGeneral(numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@idemp", numiEmpresa))
        _listParam.Add(New Datos.DParametro("@iduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prListarTipoActivos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@iduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarSectorActivos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@iduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarPersonalActivos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@iduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCargarImagenes(numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@iduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@idnumi", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_prRegistroActivoFijoGrabar(ByRef _idnumi As String, _idnumiti3 As Integer, _idglosa As String, _idvalori As Double, _idfechac As String,
                                                        _idfechau As String,
                                                        _idfactdepmes As Double,
                                                        _idfactdepanual As Double,
                                                        _idsector As Integer, _idencargado As Integer, _idvalact As Double, _detalle As DataTable, _depreciable As Integer,
                                                        _deprecionAcum As String, _vidaUtilActual As String, _numiSuc As String, _deducible As String, _numiEmpr As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        ' @idnumi ,@idnumiti3  ,@idglosa  ,@idvalori  ,@idfechac ,@idfechau ,@idfactdepmes ,@idfactdepanual ,
        '@idsector ,@idencargado ,@idvalact 
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@idnumi", _idnumi))
        _listParam.Add(New Datos.DParametro("@idnumiti3", _idnumiti3))
        _listParam.Add(New Datos.DParametro("@idglosa", _idglosa))
        _listParam.Add(New Datos.DParametro("@idvalori", _idvalori))
        _listParam.Add(New Datos.DParametro("@idfechac", _idfechac))
        _listParam.Add(New Datos.DParametro("@idfechau", _idfechau))
        _listParam.Add(New Datos.DParametro("@idfactdepmes", _idfactdepmes))
        _listParam.Add(New Datos.DParametro("@idfactdepanual", _idfactdepanual))
        _listParam.Add(New Datos.DParametro("@idsector", _idsector))
        _listParam.Add(New Datos.DParametro("@idencargado", _idencargado))
        _listParam.Add(New Datos.DParametro("@idest", _depreciable))
        _listParam.Add(New Datos.DParametro("@iddepacum", _deprecionAcum))
        _listParam.Add(New Datos.DParametro("@idvidutact", _vidaUtilActual))
        _listParam.Add(New Datos.DParametro("@idsuc", _numiSuc))
        _listParam.Add(New Datos.DParametro("@iddedu", _deducible))
        _listParam.Add(New Datos.DParametro("@idemp", _numiEmpr))



        _listParam.Add(New Datos.DParametro("@TI0041", "", _detalle))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _idnumi = _Tabla.Rows(0).Item(0)
            _resultado = True

            L_prRegistroActivoFijoGrabarHistorial(_idnumi, _idnumiti3, _idglosa, _idvalori, _idfechac, _idfechau, _idfactdepmes, _idfactdepanual, _idsector, _idencargado, _idvalact, "REGISTRO ACTIVO FIJO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prRegistroActivoFijoModificar(ByRef _idnumi As String, _idnumiti3 As Integer, _idglosa As String, _idvalori As Double, _idfechac As String,
                                                        _idfechau As String,
                                                        _idfactdepmes As Double,
                                                        _idfactdepanual As Double,
                                                        _idsector As Integer, _idencargado As Integer, _idvalact As Double, _detalle As DataTable, _depreciable As Integer,
                                                        _deprecionAcum As String, _vidaUtilActual As String, _numiSuc As String, _deducible As String, _numiEmpr As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@idnumi", _idnumi))
        _listParam.Add(New Datos.DParametro("@idnumiti3", _idnumiti3))
        _listParam.Add(New Datos.DParametro("@idglosa", _idglosa))
        _listParam.Add(New Datos.DParametro("@idvalori", _idvalori))
        _listParam.Add(New Datos.DParametro("@idfechac", _idfechac))
        _listParam.Add(New Datos.DParametro("@idfechau", _idfechau))
        _listParam.Add(New Datos.DParametro("@idfactdepmes", _idfactdepmes))
        _listParam.Add(New Datos.DParametro("@idfactdepanual", _idfactdepanual))
        _listParam.Add(New Datos.DParametro("@idsector", _idsector))
        _listParam.Add(New Datos.DParametro("@idencargado", _idencargado))
        _listParam.Add(New Datos.DParametro("@idest", _depreciable))
        _listParam.Add(New Datos.DParametro("@iddepacum", _deprecionAcum))
        _listParam.Add(New Datos.DParametro("@idvidutact", _vidaUtilActual))
        _listParam.Add(New Datos.DParametro("@idsuc", _numiSuc))
        _listParam.Add(New Datos.DParametro("@iddedu", _deducible))
        _listParam.Add(New Datos.DParametro("@idemp", _numiEmpr))


        _listParam.Add(New Datos.DParametro("@TI0041", "", _detalle))


        _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            L_prRegistroActivoFijoGrabarHistorial(_idnumi, _idnumiti3, _idglosa, _idvalori, _idfechac, _idfechau, _idfactdepmes, _idfactdepanual, _idsector, _idencargado, _idvalact, "REGISTRO ACTIVO FIJO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prRegistroActivoFijoBorrar(_idnumi As String, _idnumiti3 As Integer, _idglosa As String, _idvalori As Double, _idfechac As String,
                                                        _idfechau As String,
                                                        _idfactdepmes As Double,
                                                        _idfactdepanual As Double,
                                                        _idsector As Integer, _idencargado As Integer, _idvalact As Double, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_idnumi, "TI004", "idnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@idnumi", _idnumi))
            _listParam.Add(New Datos.DParametro("@iduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TI004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                L_prRegistroActivoFijoGrabarHistorial(_idnumi, _idnumiti3, _idglosa, _idvalori, _idfechac, _idfechau, _idfactdepmes, _idfactdepanual, _idsector, _idencargado, _idvalact, "REGISTRO ACTIVO FIJO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prRegistroActivoFijoGrabarHistorial(_idnumi As String, _idnumiti3 As Integer, _idglosa As String, _idvalori As Double, _idfechac As String,
                                                        _idfechau As String,
                                                        _idfactdepmes As Double,
                                                        _idfactdepanual As Double,
                                                        _idsector As Integer, _idencargado As Integer, _idvalact As Double, _programa As String, _transaccion As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '     @cgnumi,@cgcprod,@cgcbarra,@cgcdprod1,@cgcdprod2,@cggr1,@cggr2,
        '@cggr3,@cggr4,@cgMed,@cgumin,@cgusup,@cgvsup,@cgmstk,@cgclot,@cgsmin,
        '@cgap,@cgimg,@nprog,@tran,@newFecha,@newHora,@uact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@idnumi", _idnumi))
        _listParam.Add(New Datos.DParametro("@idnumiti3", _idnumiti3))
        _listParam.Add(New Datos.DParametro("@idglosa", _idglosa))
        _listParam.Add(New Datos.DParametro("@idvalori", _idvalori))
        _listParam.Add(New Datos.DParametro("@idfechac", _idfechac))
        _listParam.Add(New Datos.DParametro("@idfechau", _idfechau))
        _listParam.Add(New Datos.DParametro("@idfactdepmes", _idfactdepmes))
        _listParam.Add(New Datos.DParametro("@idfactdepanual", _idfactdepanual))
        _listParam.Add(New Datos.DParametro("@idsector", _idsector))
        _listParam.Add(New Datos.DParametro("@idencargado", _idencargado))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@nprog", _programa))
        _listParam.Add(New Datos.DParametro("@tran", _transaccion))
        _Tabla = D_ProcedimientoConParamHistorial("sp_Mam_HI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "COMPRAS EN COMPROBANTES"
    Public Shared Function L_prCompraComprobanteGrabar(ByRef numi As String,
                                                    ffec As String, fnit As String, frsocial As String,
                                                    fnro As String, dui As String, fautoriz As String, fmonto As String,
                                                    fccont As String, fmcfiscal As String, fdesc As String, tipoCompra As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))

        _listParam.Add(New Datos.DParametro("@ffec", ffec))
        _listParam.Add(New Datos.DParametro("@fnit", fnit))
        _listParam.Add(New Datos.DParametro("@frsocial", frsocial))
        _listParam.Add(New Datos.DParametro("@fnro", fnro))
        _listParam.Add(New Datos.DParametro("@fautoriz", fautoriz))
        _listParam.Add(New Datos.DParametro("@fmonto", fmonto))
        _listParam.Add(New Datos.DParametro("@fccont", fccont))
        _listParam.Add(New Datos.DParametro("@fmcfiscal", fmcfiscal))
        _listParam.Add(New Datos.DParametro("@fdesc", fdesc))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TFC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnCompraComprobanteModificar(ByRef numi As String,
                                                 ffec As String, fnit As String, frsocial As String,
                                                    fnro As String, dui As String, fautoriz As String, fmonto As String,
                                                          sujetoCreditoFiscal As String, subTotal As String, fdesc As String,
                                                           importeBaseCreditoFiscal As String, creditoFiscal As String,
                                                    fccont As String, tipoCompra As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@ffec", ffec))
        _listParam.Add(New Datos.DParametro("@fnit", fnit))
        _listParam.Add(New Datos.DParametro("@frsocial", frsocial))
        _listParam.Add(New Datos.DParametro("@fnro", fnro))
        _listParam.Add(New Datos.DParametro("@fcandui", dui))
        _listParam.Add(New Datos.DParametro("@fautoriz", fautoriz))
        _listParam.Add(New Datos.DParametro("@fmonto", fmonto))
        _listParam.Add(New Datos.DParametro("@fcanscf", sujetoCreditoFiscal))
        _listParam.Add(New Datos.DParametro("@fcasubtotal", subTotal))
        _listParam.Add(New Datos.DParametro("@fdesc", fdesc))
        _listParam.Add(New Datos.DParametro("@fcaibcf", importeBaseCreditoFiscal))
        _listParam.Add(New Datos.DParametro("@fcacfiscal", creditoFiscal))
        _listParam.Add(New Datos.DParametro("@fccont", fccont))
        _listParam.Add(New Datos.DParametro("@fcatcom", tipoCompra))


        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TFC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCompraComprobanteGeneralPorNumi(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@numi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TFC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCompraComprobanteGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TFC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCompraComprobanteBorrar(_numi As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TFC001", "fcanumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@fcanumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TFC001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _numi = _Tabla.Rows(0).Item(0)
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCompraComprobanteGeneralAnios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TFC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prCompraComprobanteGeneralLibroCompra(anio As String, mes As String, _emp As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@anio", anio))
        _listParam.Add(New Datos.DParametro("@mes", mes))
        _listParam.Add(New Datos.DParametro("@emp", _emp))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TFC001", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "NUMERO DE CUENTAS PRECIOS"


    Public Shared Function L_prlistarCategoriasActivos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TS006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prlistarCategoriasTipoVenta() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TS006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prlistarMostrarServiciosnumeroCuenta(categoria As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@categoria", categoria))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TS006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioCuentaModificar(ByRef _sdnumi As String, _dt As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@TS006", "", _dt))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TS006", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True

        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "COMPROBANTES MARCOS"

    'ARREGLOS DANNY********************
    Public Shared Function L_prIntegracionObtenerCuentaLicenciaInternacional() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 34))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    '*************************************************

    Public Shared Function L_prIntegracionGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prIntegracionBancos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prIntegracionBancosRegistrados(ifnumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ifnumi", ifnumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prIntegracionGeneralArqueo() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prIntegracionDetalleArqueo(_ahnumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ahnumi", _ahnumi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prIntegracionDetalle(_ifnumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ifnumi", _ifnumi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prIntegracionDetalleBanco(_ifnumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@ifnumi", _ifnumi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prIntegracionBanco(_ifnumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@ifnumi", _ifnumi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioListarCuentas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioListarCuentasArqueo() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_ComprobanteArqueo", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioListarClientesCreditoArqueo(_fechai As String, _fechaf As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechai))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaf))
        _Tabla = D_ProcedimientoConParam("sp_Mam_ComprobanteArqueo", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioListarVentasProductos(_fechai As String, _fechaf As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechai))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaf))
        _Tabla = D_ProcedimientoConParam("sp_Mam_ComprobanteArqueo", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioListarClientesAnticipos(_fechai As String, _fechaf As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechai))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaf))
        _Tabla = D_ProcedimientoConParam("sp_Mam_ComprobanteArqueo", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prServicioListarTotalPorTipoCombustible(_fechai As String, _fechaf As String, _Combustible As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechai))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaf))
        _listParam.Add(New Datos.DParametro("@combustible", _Combustible))
        _Tabla = D_ProcedimientoConParam("sp_Mam_ComprobanteArqueo", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioListarVentasArqueo(_fechai As String, _fechaf As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechai))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaf))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_ComprobanteArqueo", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prNumiCuentaCobrar() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prServicioBuscarPadreCuenta(_canumi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prServicioObtenerTotalPorCategoria(_categoria As Integer, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@categoria", _categoria))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioObtenerTotalPorCategoriaTodosConRecibo(_categoria As Integer, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 110101))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@categoria", _categoria))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioObtenerTotalPorCategoriaCuentasCobrar(_Tventa As Integer, _categoria As Integer, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 32))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@tventa", _Tventa))
        _listParam.Add(New Datos.DParametro("@categoria", _categoria))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioObtenerTotalPorCategoriaCuentasCobrartra(_Tventa As Integer, _categoria As Integer, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 322))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@tventa", _Tventa))
        _listParam.Add(New Datos.DParametro("@categoria", _categoria))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioObtenerTotalPorCategoriaClientePorCobrar(_categoria As Integer, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@categoria", _categoria))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarClientePorCobrarPorSector(_categoria As Integer, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@categoria", _categoria))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prServicioObtenerNumiPorCategoria(_categoria As Integer, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@categoria", _categoria))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioObtenerTotalServiciosLavadero(_sector As String, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prCuentaDiferencia(_cuenta As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@cuenta", _cuenta))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prServicioListarCuentasServicioLavadero(cuenta As String, Empresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cuenta", cuenta))
        _listParam.Add(New Datos.DParametro("@empresa", Empresa))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prServicioListarCuentasServicioGeneral(cuenta As String, Empresa As String, _sector As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cuenta", cuenta))
        _listParam.Add(New Datos.DParametro("@empresa", Empresa))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prServicioListarCuentasServicioGeneralSocio(cuenta As String, Empresa As String, _sector As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cuenta", cuenta))
        _listParam.Add(New Datos.DParametro("@empresa", Empresa))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prObtenerTotalPorCuenta(cuenta As String, descripcion As String, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@cuenta", cuenta))
        _listParam.Add(New Datos.DParametro("@descripcion", descripcion))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prObtenerTotalPorCuentaAdministracion(cuenta As String, descripcion As String, _fechaI As String, _fechaF As String, _sucursal As Integer, _sector As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@cuenta", cuenta))
        _listParam.Add(New Datos.DParametro("@descripcion", descripcion))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prObtenerTotalPorCuentaProductos(cuenta As String, descripcion As String, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cuenta", cuenta))
        _listParam.Add(New Datos.DParametro("@descripcion", descripcion))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prObtenerServiciosCabanas(_fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prObtenerNombreCuenta(cuenta As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cuenta", cuenta))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prListarCuentasServicioLavadero(_empresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@empresa", _empresa))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prListarCuentasServicioGeneral(_empresa As String, _sector As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@empresa", _empresa))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarSociosCuotas(_servicio As String, _fechaI As String, _fechaF As String,
                                                  _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@cuenta", _servicio))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarPagosSocioss(_servicio As String, _fechaI As String, _fechaF As String,
                                                  _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@vcnumi", _servicio))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prListarPagosSociossCuotaMourtotia(_servicio As String, _fechaI As String, _fechaF As String, _sucursal As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@seuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@vcnumi", _servicio))
        _listParam.Add(New Datos.DParametro("@sucursal", _sucursal))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Asiento", _listParam)

        Return _Tabla
    End Function
#End Region


#Region "ZY004"
    Public Shared Function L_prTitulosAll(_cod As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@yecod", _cod))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prTitulosAll2() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY004", _listParam)

        Return _Tabla
    End Function

    'Public Shared Function L_prTitulosGrabar(ByRef _numi As String, _desc As String, _con1 As String, _con2 As String, _con3 As String, _con4 As String) As Boolean
    '    Dim _resultado As Boolean

    '    Dim _Tabla As DataTable
    '    Dim _listParam As New List(Of Datos.DParametro)

    '    _listParam.Add(New Datos.DParametro("@tipo", 1))
    '    _listParam.Add(New Datos.DParametro("@yenumi", _numi))
    '    _listParam.Add(New Datos.DParametro("@cedesc", _desc))
    '    _listParam.Add(New Datos.DParametro("@cecon1", _con1))
    '    _listParam.Add(New Datos.DParametro("@cecon2", _con2))
    '    _listParam.Add(New Datos.DParametro("@cecon3", _con3))
    '    _listParam.Add(New Datos.DParametro("@cecon4", _con4))
    '    _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

    '    _Tabla = D_ProcedimientoConParam("sp_dg_ZY004", _listParam)

    '    If _Tabla.Rows.Count > 0 Then
    '        _numi = _Tabla.Rows(0).Item(0)
    '        _resultado = True
    '    Else
    '        _resultado = False
    '    End If

    '    Return _resultado
    'End Function

    Public Shared Function L_prTitulosModificar(_numi As String, _desc As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@yenumi", _numi))
        _listParam.Add(New Datos.DParametro("@yedesc", _desc))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    'Public Shared Function L_prTitulosBorrar(_numi As String, ByRef _mensaje As String) As Boolean

    '    Dim _resultado As Boolean

    '    If L_fnbValidarEliminacion(_numi, "ZY004", "yenumi", _mensaje) = True Then
    '        Dim _Tabla As DataTable

    '        Dim _listParam As New List(Of Datos.DParametro)

    '        _listParam.Add(New Datos.DParametro("@tipo", -1))
    '        _listParam.Add(New Datos.DParametro("@yenumi", _numi))
    '        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

    '        _Tabla = D_ProcedimientoConParam("sp_dg_ZY004", _listParam)

    '        If _Tabla.Rows.Count > 0 Then
    '            _resultado = True

    '        Else
    '            _resultado = False
    '        End If
    '    Else
    '        _resultado = False
    '    End If

    '    Return _resultado
    'End Function
#End Region

#Region "PRESUPUESTO"
    Public Shared Function L_prPresupuestoReporteResumen(gestion As String, mes As String, _numiModulo As String, _numiSuc As String, _numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@chanio", gestion))
        _listParam.Add(New Datos.DParametro("@chmes", mes))
        _listParam.Add(New Datos.DParametro("@numiModulo", _numiModulo))
        _listParam.Add(New Datos.DParametro("@numiSucursal", _numiSuc))
        _listParam.Add(New Datos.DParametro("@numiEmpresa", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prPresupuestoObtenerValoresPorServicio(gestion As String, numiServ As String, _numiModulo As String, _numiSuc As String, _numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 51))
        _listParam.Add(New Datos.DParametro("@chanio", gestion))
        _listParam.Add(New Datos.DParametro("@edtipo", numiServ))
        _listParam.Add(New Datos.DParametro("@numiModulo", _numiModulo))
        _listParam.Add(New Datos.DParametro("@numiSucursal", _numiSuc))
        _listParam.Add(New Datos.DParametro("@numiEmpresa", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPresupuestoObtenenerCuentasEgresoPadres(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@edtipo", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prPresupuestoGeneralGestiones(_numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@numiEmpresa", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPresupuestoGeneralPorAnio(_anio As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@chanio", _anio))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPresupuestoObtenenerServiciosLavadero() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prPresupuestoObtenenerServiciosPorTipo(_tipo As String, _tipoInEgre As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@edtipo", _tipo))
        _listParam.Add(New Datos.DParametro("@chtipo", _tipoInEgre))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPresupuestoObtenenerCuentasIngresoPorEmpresaConMayores(_numiEmpresa As String, _numiModulo As String, _numiSuc As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@edtipo", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@numiModulo", _numiModulo))
        _listParam.Add(New Datos.DParametro("@numiSucursal", _numiSuc))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPresupuestoObtenenerCuentasEgresoPorEmpresaConMayores(_numiEmpresa As String, _numiModulo As String, _numiSuc As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@edtipo", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@numiModulo", _numiModulo))
        _listParam.Add(New Datos.DParametro("@numiSucursal", _numiSuc))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPresupuestoObtenenerCuentasPorPadre(_numiPadre As String, _tipoInEgre As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 41))
        _listParam.Add(New Datos.DParametro("@edtipo", _numiPadre))
        _listParam.Add(New Datos.DParametro("@chtipo", _tipoInEgre))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prPresupuestoGrabar(_detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@TC010", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prPresupuestoEliminar(_numiEmp As String, _anio As String, _numiMod As String, _numiSuc As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@numiEmpresa", _numiEmp))
        _listParam.Add(New Datos.DParametro("@chanio", _anio))
        _listParam.Add(New Datos.DParametro("@numiModulo", _numiMod))
        _listParam.Add(New Datos.DParametro("@numiSucursal", _numiSuc))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC010", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            'L_prDosificacionGrabarHistorial(numi, cia, alm, sfc,
            '                                 autoriz, nfac, key, fdel,
            '                                 fal, nota, nota2, est, "DOSIFICACION", 3)
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "ZY005"
    Public Shared Function L_prModulosAll(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@yfnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY005", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "DEPR. ACTIVO FIJO"

    '-----------------------------------------------------------------

    Public Shared Function L_prDepreGeneralDesc(numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@iiemp", numiEmpresa))
        _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prDepreRepResumenMes(_mes As String, _anio As String, numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@iimes", _mes))
        _listParam.Add(New Datos.DParametro("@iiano", _anio))
        _listParam.Add(New Datos.DParametro("@iiemp", numiEmpresa))

        _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prDepreRepResumenGestion(_anio As String, numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@iiano", _anio))
        _listParam.Add(New Datos.DParametro("@iiemp", numiEmpresa))

        _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

        Return _Tabla
    End Function

    '---------------------------------------------------------------

    'Public Shared Function L_prDepreGeneral() As DataTable
    '    Dim _Tabla As DataTable

    '    Dim _listParam As New List(Of Datos.DParametro)

    '    _listParam.Add(New Datos.DParametro("@tipo", 3))
    '    _listParam.Add(New Datos.DParametro("@iguact", L_Usuario))

    '    _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)

    '    Return _Tabla
    'End Function
    Public Shared Function L_prDepreGeneral2(numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@iiemp", numiEmpresa))

        _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

        Return _Tabla
    End Function
    'Public Shared Function L_prDepreDetalle(numi As String) As DataTable
    '    Dim _Tabla As DataTable

    '    Dim _listParam As New List(Of Datos.DParametro)

    '    _listParam.Add(New Datos.DParametro("@tipo", 5))
    '    _listParam.Add(New Datos.DParametro("@ignumi", numi))
    '    _listParam.Add(New Datos.DParametro("@iguact", L_Usuario))

    '    _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)

    '    Return _Tabla
    'End Function

    Public Shared Function L_prDepreDetalle2(numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@iinumi", numi))
        _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

        Return _Tabla
    End Function

    'Public Shared Function L_prDepreDetalle() As DataTable
    '    Dim _Tabla As DataTable

    '    Dim _listParam As New List(Of Datos.DParametro)

    '    _listParam.Add(New Datos.DParametro("@tipo", 8))
    '    _listParam.Add(New Datos.DParametro("@iguact", L_Usuario))

    '    _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)

    '    Return _Tabla
    'End Function
    Public Shared Function L_prDepreDetalleCalcularDepreciacion(numiEmpresa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@iiemp", numiEmpresa))
        _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

        Return _Tabla
    End Function

    'Public Shared Function L_prDepreGrabar(_fecha As String, _detalle As DataTable) As Boolean
    '    Dim _resultado As Boolean

    '    Dim _Tabla As DataTable
    '    Dim _listParam As New List(Of Datos.DParametro)
    '    '     @cinumi ,@cicprod ,@cicbarra ,@cicdprod1 ,@cicdprod2 ,
    '    '@cigr1 ,@cigr2 ,@cigr3 ,@cigr4 ,@ciMed ,@ciumin ,@ciusup ,@civsup ,
    '    '@cimstk ,@ciclot ,@cismin ,@ciap ,@ciimg 
    '    _listParam.Add(New Datos.DParametro("@tipo", 1))
    '    '_listParam.Add(New Datos.DParametro("@ignumi", _ignumi))
    '    _listParam.Add(New Datos.DParametro("@fecha", _fecha))
    '    _listParam.Add(New Datos.DParametro("@TI0061", "", _detalle))
    '    _listParam.Add(New Datos.DParametro("@iguact", L_Usuario))

    '    _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)

    '    If _Tabla.Rows.Count > 0 Then
    '        '_cinumi = _Tabla.Rows(0).Item(0)
    '        _resultado = True

    '        'L_prProductoGrabarHistorial(_cinumi, _cicprod, _cicbarra, _cicdprod1, _cicdprod2, _cigr1, _cigr2, _cigr3, _cigr4, _ciMed, _ciumin, _ciusup, _civsup, _cismin, _ciap, _ciimg, "PRODUCTOS INVENTARIO", 1)
    '    Else
    '        _resultado = False
    '    End If

    '    Return _resultado
    'End Function

    Public Shared Function L_prDepreGrabar2(_mes As String, _anio As String, _numiEmpresa As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@iimes", _mes))
        _listParam.Add(New Datos.DParametro("@iiano", _anio))
        _listParam.Add(New Datos.DParametro("@iiemp", _numiEmpresa))
        _listParam.Add(New Datos.DParametro("@TI0071", "", _detalle))
        _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_cinumi = _Tabla.Rows(0).Item(0)
            _resultado = True

        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    'Public Shared Function L_prDepreModificar(ByRef _ignumi As String, _fecha As String, _detalle As DataTable) As Boolean
    '    Dim _resultado As Boolean

    '    Dim _Tabla As DataTable
    '    Dim _listParam As New List(Of Datos.DParametro)

    '    _listParam.Add(New Datos.DParametro("@tipo", 2))
    '    _listParam.Add(New Datos.DParametro("@ignumi", _ignumi))
    '    _listParam.Add(New Datos.DParametro("@fecha", _fecha))
    '    _listParam.Add(New Datos.DParametro("@TI0061", "", _detalle))
    '    _listParam.Add(New Datos.DParametro("@ciuact", L_Usuario))

    '    _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)


    '    If _Tabla.Rows.Count > 0 Then
    '        _resultado = True
    '        'L_prProductoGrabarHistorial(_cinumi, _cicprod, _cicbarra, _cicdprod1, _cicdprod2, _cigr1, _cigr2, _cigr3, _cigr4, _ciMed, _ciumin, _ciusup, _civsup, _cismin, _ciap, _ciimg, "PRODUCTOS INVENTARIO", 2)
    '    Else
    '        _resultado = False
    '    End If

    '    Return _resultado
    'End Function

    'Public Shared Function L_prDepreELiminar(ByRef _numi As String, _mensaje As String) As Boolean

    '    Dim _resultado As Boolean

    '    If L_fnbValidarEliminacion(_numi, "TI006", "ignumi", _mensaje) = True Then
    '        Dim _Tabla As DataTable

    '        Dim _listParam As New List(Of Datos.DParametro)

    '        _listParam.Add(New Datos.DParametro("@tipo", -1))
    '        _listParam.Add(New Datos.DParametro("@ignumi", _numi))
    '        _listParam.Add(New Datos.DParametro("@iguact", L_Usuario))

    '        _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)

    '        If _Tabla.Rows.Count > 0 Then
    '            _resultado = True
    '        Else
    '            _resultado = False
    '        End If
    '    Else
    '        _resultado = False
    '    End If

    '    Return _resultado
    'End Function

    Public Shared Function L_prDepreELiminar2(ByRef _numi As String, _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TI007", "iinumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@iinumi", _numi))
            _listParam.Add(New Datos.DParametro("@iiuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_TI007", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prUfvfin(_fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fecha", _fecha))
        _listParam.Add(New Datos.DParametro("@iguact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prUfvini(_fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@fecha", _fecha))
        _listParam.Add(New Datos.DParametro("@iguact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TI006", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "Reporte CierreGeneral"
    Public Shared Function L_prTraerReporteCierre(_fechadel As String, _fechaal As String, _almacen As String, _numisec As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechadel", _fechadel))
        _listParam.Add(New Datos.DParametro("@fechaal", _fechaal))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@numisect", _numisec))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prTraerReporteCierreTipo(_fechadel As String, _fechaal As String, _almacen As String, _numisec As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechadel", _fechadel))
        _listParam.Add(New Datos.DParametro("@fechaal", _fechaal))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@numisect", _numisec))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prTraerReporteCierre(_fechadel As String, _fechaal As String, _almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechadel", _fechadel))
        _listParam.Add(New Datos.DParametro("@fechaal", _fechaal))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prTraerReporteCierreTipoVenta(_fechadel As String, _fechaal As String, _almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechadel", _fechadel))
        _listParam.Add(New Datos.DParametro("@fechaal", _fechaal))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV002", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "Reporte Resumen de ventas"
    Public Shared Function L_prTraerReporteResumenVentas(_fec1 As String, _fec2 As String, _almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fec1", _fec1))
        _listParam.Add(New Datos.DParametro("@fec2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV0021", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prTraerReporteResumenVentas(_fec1 As String, _fec2 As String, _almacen As String, _sector As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fec1", _fec1))
        _listParam.Add(New Datos.DParametro("@fec2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV0021", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prTraerReporteResumenVentas2(_fec1 As String, _fec2 As String, _almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fec1", _fec1))
        _listParam.Add(New Datos.DParametro("@fec2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV0021", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prTraerReporteResumenVentasServ(_fec1 As String, _fec2 As String, _sucursal As String, _servicio As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fec1", _fec1))
        _listParam.Add(New Datos.DParametro("@fec2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _sucursal))
        _listParam.Add(New Datos.DParametro("@servicio", _servicio))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV0021", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prTraerReporteResumenVentas(_fec1 As String, _fec2 As String, _almacen As String, _sector As String, _servicio As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fec1", _fec1))
        _listParam.Add(New Datos.DParametro("@fec2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@servicio", _servicio))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TV0021", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "Reporte Resumen de ventas detallado"
    Public Shared Function L_prTraerReporteVentasDetallado(_fec1 As String, _fec2 As String, _almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fecha1", _fec1))
        _listParam.Add(New Datos.DParametro("@fecha2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_VentasDetallado", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prTraerReporteVentasDetallado(_fec1 As String, _fec2 As String, _almacen As String, _sector As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fecha1", _fec1))
        _listParam.Add(New Datos.DParametro("@fecha2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_VentasDetallado", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prTraerReporteVentasDetallado(_fec1 As String, _fec2 As String, _almacen As String, _sector As String, _servicio As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fecha1", _fec1))
        _listParam.Add(New Datos.DParametro("@fecha2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@sector", _sector))
        _listParam.Add(New Datos.DParametro("@servicio", _servicio))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_VentasDetallado", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prTraerReporteVentasDetalladoServ(_fec1 As String, _fec2 As String, _almacen As String, servicio As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fecha1", _fec1))
        _listParam.Add(New Datos.DParametro("@fecha2", _fec2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@servicio", servicio))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_VentasDetallado", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prTraerServ(numiSector As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@sector", numiSector))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_VentasDetallado", _listParam)

        Return _Tabla
    End Function
#End Region


#Region "TC0031 traer los items de los auxiliares"
    Public Shared Function L_prItemPorModulo(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@numi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_TC0031", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "reporte arqueo diario"
    Public Shared Function L_prTraerReporteArqueoDiario(_fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fecha", _fecha))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_da_ArqueoDiario", _listParam)

        Return _Tabla
    End Function
#End Region
End Class
