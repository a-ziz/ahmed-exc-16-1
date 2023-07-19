Imports System.Data.SqlClient

Public Class MMABooksDB
    Public Shared Function GetConnection() As SqlConnection
        Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;" &
            "AttachDbFilename=|DataDirectory|\MMABooks.mdf;" &
            "Integrated Security=True"

        Return New SqlConnection(connectionString)
    End Function

End Class
