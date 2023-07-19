Imports System.Data.SqlClient

Public Class StateDB

    Public Shared Function GetStateList() As List(Of State)

        ' variable to get list of state
        Dim stateList As New List(Of State)

        ' connect to sql server
        Dim connection As SqlConnection = MMABooksDB.GetConnection

        ' select statement string to use for sql command
        Dim selectStatement As String =
            "SELECT StateCode, StateName " &
            "FROM States " &
            "ORDER BY StateName"

        ' sql command to execute sql statement string 
        Dim selectCommand As New SqlCommand(selectStatement, connection)

        Try
            connection.Open()
            Dim reader As SqlDataReader = selectCommand.ExecuteReader()
            Dim state As State
            Do While reader.Read
                state = New State
                state.StateCode = reader("StateCode").ToString
                state.StateName = reader("StateName").ToString
                stateList.Add(state)
            Loop
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            connection.Close()
        End Try
        Return stateList

    End Function
End Class
