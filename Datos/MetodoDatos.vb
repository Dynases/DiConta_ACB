Imports System.Data
Imports System.Data.SqlClient   'Lib para conexion con SQL Server
Imports System.Drawing

'Imports System.Data.OleDb       'Lib para conexion con Access
Public Class MetodoDatos
    Public Shared Function CrearComando(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "") As SqlCommand
        Dim _cadenaConexion = Configuracion.CadenaConexion(Ip, UsuarioSql, ClaveSql, NombreBD)
        Dim _conexion As New SqlConnection() 'SqlConnection()
        _conexion.ConnectionString = _cadenaConexion
        '+ ";Connection Timeout=15"
        Dim _comando As New SqlCommand() 'SqlCommand()  75900286 carmen
        _comando = _conexion.CreateCommand()
        _comando.CommandType = CommandType.Text
        'abrir
        _comando.Connection.Open()
        Return _comando
    End Function
    Public Shared Function CrearComandoProcedimiento(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "") As SqlCommand
        Dim _cadenaConexion = Configuracion.CadenaConexion(Ip, UsuarioSql, ClaveSql, NombreBD)

        Dim _conexion As New SqlConnection() 'SqlConnection()
        _conexion.ConnectionString = _cadenaConexion
        '+ ";Connection Timeout=5"

        Dim _comando As New SqlCommand() 'SqlCommand()
        _comando = _conexion.CreateCommand()
        _comando.CommandType = CommandType.StoredProcedure
        'abrir
        _comando.Connection.Open()
        Return _comando
    End Function








    Public Shared Function EjecutarComandoSelect(Comando As SqlCommand) As DataTable
        Dim _tabla As New DataTable()
        Try

            If (Comando.Connection.State <> ConnectionState.Open) Then
                Comando.Connection.Open()
            End If

            Dim _adaptador As New SqlDataAdapter 'SqlDataAdapter()
            _adaptador.SelectCommand = Comando
            _adaptador.Fill(_tabla)
            Comando.Connection.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
            'Finally
            Comando.Connection.Close()

        End Try
        Return _tabla
    End Function

    Public Shared Function EjecutarInsert(Comando As SqlCommand) As Boolean
        Dim _tabla As New DataTable()
        Dim _Err As Boolean = False

        Try
            If (Comando.Connection.State <> ConnectionState.Open) Then
                Comando.Connection.Open()
            End If


            Comando.ExecuteNonQuery()
            Comando.Connection.Close()
            'Comando.Connection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            _Err = True
            'Finally
            Comando.Connection.Close()

        End Try
        Return _Err
    End Function

    Public Shared Function EjecutarProcedimiento(Comando As SqlCommand) As DataTable
        Dim _tabla As New DataTable()
        Try
            If (Comando.Connection.State <> ConnectionState.Open) Then
                Comando.Connection.Open()
            End If

            Dim _adaptador As New SqlDataAdapter 'SqlDataAdapter()
            _adaptador.SelectCommand = Comando

            _adaptador.Fill(_tabla)

            'Dim n As Integer = Comando.Connection.ConnectionTimeout()
            'MsgBox("Tiempo de Espera :" + Str(n))
            'Comando.Connection.Close()

            Comando.CommandTimeout = 1200
            Comando.Connection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Return _tabla
    End Function
    Public Shared Function EjecutarProcedimiento1(Comando As SqlCommand) As DataSet
        Dim _tabla As New DataSet()
        Try
            If (Comando.Connection.State <> ConnectionState.Open) Then
                Comando.Connection.Open()
            End If

            Dim _adaptador As New SqlDataAdapter 'SqlDataAdapter()
            _adaptador.SelectCommand = Comando

            _adaptador.Fill(_tabla)

            'Dim n As Integer = Comando.Connection.ConnectionTimeout()
            'MsgBox("Tiempo de Espera :" + Str(n))
            'Comando.Connection.Close()

            Comando.CommandTimeout = 1200
            Comando.Connection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Return _tabla
    End Function

End Class