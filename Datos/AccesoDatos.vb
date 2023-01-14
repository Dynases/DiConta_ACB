Imports System.Data
Imports System.Data.SqlClient   'Lib para conexion con SQL Server
'Imports System.Data.OleDb       'Lib para conexion con Access
Public Class AccesoDatos
    Shared _comando As SqlCommand
    Shared _comandoProcedimiento As SqlCommand
    Shared _comandoProcedimientoHistorial As SqlCommand

    'parametros de la conexion de la base de datos
    Shared _ip As String
    Shared _usuarioSql As String
    Shared _claveSql As String
    Shared _nombreBD As String

    '------------------------------------------------------

    Public Shared Sub D_abrirConexion(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")
        _comando = MetodoDatos.CrearComando(Ip, UsuarioSql, ClaveSql, NombreBD)
        _comandoProcedimiento = MetodoDatos.CrearComandoProcedimiento(Ip, UsuarioSql, ClaveSql, NombreBD)

        '-----------------------------
        _ip = Ip
        _usuarioSql = UsuarioSql
        _claveSql = ClaveSql
        _nombreBD = NombreBD
        '-----------------------------

    End Sub
    Public Shared Sub D_abrirConexionHistorial(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")

        _comandoProcedimientoHistorial = MetodoDatos.CrearComandoProcedimiento(Ip, UsuarioSql, ClaveSql, NombreBD)
    End Sub

    Public Shared Function D_Datos_Tabla(_Campos As String, _Tabla As String, _Where As String) As DataTable
        'Dim _comando As OleDbCommand = MetodoDatos.CrearComando()


        _comando.CommandText = "SELECT " + _Campos + " FROM " + _Tabla + " WHERE " + _Where
        Return MetodoDatos.EjecutarComandoSelect(_comando)
    End Function

    Public Shared Function D_Maximo(_Tabla As String, _Campo As String, _Where As String) As DataTable
        'Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "SELECT MAX(" + _Campo + ") FROM " + _Tabla + " WHERE " + _Where
        Return MetodoDatos.EjecutarComandoSelect(_comando)
    End Function

    Public Shared Function D_Sum(_Tabla As String, _Where As String, _Campo1 As String, Optional _Campo2 As String = "") As DataTable
        'Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        Dim Sql As String
        Sql = "SELECT SUM(" + _Campo1 + ")"
        If _Campo2 <> "" Then
            Sql = Sql + ", SUM(" + _Campo2 + ")"
        End If
        _comando.CommandText = Sql + " FROM " + _Tabla + " WHERE " + _Where
        Return MetodoDatos.EjecutarComandoSelect(_comando)
    End Function

    Public Shared Function D_Insertar_Datos(_Tabla As String, _Campos As String) As Boolean
        'Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "Insert Into " + _Tabla + " Values(" + _Campos + ")"

        Return MetodoDatos.EjecutarInsert(_comando)

    End Function

    Public Shared Function D_Modificar_Datos(_Tabla As String, _Campos As String, _Where As String) As Boolean
        'Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "Update " + _Tabla + " Set " + _Campos + " Where " + _Where

        Return MetodoDatos.EjecutarInsert(_comando)

    End Function

    Public Shared Function D_Eliminar_Datos(_Tabla As String, _Where As String) As Boolean
        'Dim _comando As OleDbCommand = MetodoDatos.CrearComando()
        _comando.CommandText = "Delete From " + _Tabla + " Where " + _Where

        Return MetodoDatos.EjecutarInsert(_comando)

    End Function

    Public Shared Function D_Procedimiento(_Nombre As String) As DataTable
        _comando.CommandText = _Nombre
        _comando.CommandType = CommandType.StoredProcedure
        _comando.ExecuteNonQuery()
        Return MetodoDatos.EjecutarProcedimiento(_comando)
    End Function

    Public Shared Function D_ProcedimientoConParam(_Nombre As String, _listaParam As List(Of DParametro)) As DataTable
        'codigo modificado para abrir la conexion a la base de datos cuando expiro el tiempo de conexion
        'If _comandoProcedimiento.Connection.State <> ConnectionState.Open Then
        '    D_abrirConexion(_ip, _usuarioSql, _claveSql, _nombreBD)
        '    D_abrirConexion(_ip, _usuarioSql, _claveSql, "BHDicon")
        'End If

        '-------------------------------------------------------------------------

        _comandoProcedimiento.Parameters.Clear()
        _comandoProcedimiento.CommandText = _Nombre
        For i = 0 To _listaParam.Count - 1
            Dim nombre As String = _listaParam.Item(i).nombre
            Dim valor As String = _listaParam.Item(i).valor
            Dim detalle As DataTable = _listaParam.Item(i).detalle

            If IsNothing(detalle) Then
                _comandoProcedimiento.Parameters.AddWithValue(nombre, valor)
            Else
                _comandoProcedimiento.Parameters.Add(nombre, SqlDbType.Structured)
                _comandoProcedimiento.Parameters(nombre).Value = detalle
            End If

        Next

        Return MetodoDatos.EjecutarProcedimiento(_comandoProcedimiento)
    End Function

    Public Shared Function D_ProcedimientoConParam1(_Nombre As String, _listaParam As List(Of DParametro)) As DataSet
        'codigo modificado para abrir la conexion a la base de datos cuando expiro el tiempo de conexion
        'If _comandoProcedimiento.Connection.State <> ConnectionState.Open Then
        '    D_abrirConexion(_ip, _usuarioSql, _claveSql, _nombreBD)
        '    D_abrirConexion(_ip, _usuarioSql, _claveSql, "BHDicon")
        'End If

        '-------------------------------------------------------------------------

        _comandoProcedimiento.Parameters.Clear()
        _comandoProcedimiento.CommandText = _Nombre
        For i = 0 To _listaParam.Count - 1
            Dim nombre As String = _listaParam.Item(i).nombre
            Dim valor As String = _listaParam.Item(i).valor
            Dim detalle As DataTable = _listaParam.Item(i).detalle

            If IsNothing(detalle) Then
                _comandoProcedimiento.Parameters.AddWithValue(nombre, valor)
            Else
                _comandoProcedimiento.Parameters.Add(nombre, SqlDbType.Structured)
                _comandoProcedimiento.Parameters(nombre).Value = detalle
            End If

        Next

        Return MetodoDatos.EjecutarProcedimiento1(_comandoProcedimiento)
    End Function

    Public Shared Function D_ProcedimientoConParamHistorial(_Nombre As String, _listaParam As List(Of DParametro)) As DataTable
        _comandoProcedimientoHistorial.Parameters.Clear()
        _comandoProcedimientoHistorial.CommandText = _Nombre
        For i = 0 To _listaParam.Count - 1
            Dim nombre As String = _listaParam.Item(i).nombre
            Dim valor As String = _listaParam.Item(i).valor
            Dim detalle As DataTable = _listaParam.Item(i).detalle

            If IsNothing(detalle) Then
                _comandoProcedimientoHistorial.Parameters.AddWithValue(nombre, valor)
            Else
                _comandoProcedimientoHistorial.Parameters.Add(nombre, SqlDbType.Structured)
                _comandoProcedimientoHistorial.Parameters(nombre).Value = detalle
            End If

        Next

        Return MetodoDatos.EjecutarProcedimiento(_comandoProcedimientoHistorial)
    End Function
End Class


