Imports System.Data.SqlClient

Public Class CustomerDB
    Public Shared Function GetCustomer(customerID As Integer) As Customer

        ' Sql Connection to database from MMABooksDB class method 
        Dim connection As SqlConnection = MMABooksDB.GetConnection()

        Try
            connection.Open()
            ' Select Statement string, customer data based on Customer ID
            Dim selectStatement As String = "Select * from Customers where CustomerID=@CustomerID"

            ' Select command using select statement from connected sql server
            Dim selectCommand As SqlCommand = New SqlCommand(selectStatement, connection)

            ' Replace @CustomerID with CustomerID variable
            selectCommand.Parameters.AddWithValue("@CustomerID", customerID)

            ' result reads only a single row 
            Dim results As SqlDataReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow)

            ' Create a new customer
            Dim thisCustomer As New Customer()

            If results.Read Then
                ' add the data from database into thisCustomer
                thisCustomer.CustomerID = CInt(results("CustomerID"))
                thisCustomer.Name = results("Name").ToString
                thisCustomer.Address = results("Address").ToString
                thisCustomer.City = results("City").ToString
                thisCustomer.State = results("State").ToString
                thisCustomer.ZipCode = results("ZipCode").ToString

                ' return thisCustomer
                Return thisCustomer
            Else
                thisCustomer = Nothing
            End If
            results.Close()
        Catch ex As SqlException
            Throw ex
        Finally
            connection.Close()
        End Try

    End Function

    Public Shared Function UpdateCustomer(oldCustomer As Customer, newCustomer As Customer) As Boolean

        ' connection from MMABooksDB class method
        Dim connection As SqlConnection = MMABooksDB.GetConnection

        ' string to use sql update statement where the customer's data is set from old values to new values 
        Dim updateStatement As String =
            "UPDATE Customers SET " &
            "Name = @NewName, " &
            "Address = @NewAddress, " &
            "City = @NewCity, " &
            "State = @NewState, " &
            "ZipCode = @NewZipCode " &
            "WHERE CustomerID = @OldCustomerID " &
            "AND Name = @OldName " &
            "AND Address = @OldAddress " &
            "AND City = @OldCity " &
            "AND State = @OldState " &
            "AND ZipCode = @OldZipCode "

        ' UPDATE Command using UPDATE statement from connected sql server
        Dim updateCommand As New SqlCommand(updateStatement, connection)

        ' update both new and old values of the customer
        updateCommand.Parameters.AddWithValue("@NewName", newCustomer.Name)
        updateCommand.Parameters.AddWithValue("@NewAddress", newCustomer.Address)
        updateCommand.Parameters.AddWithValue("@NewCity", newCustomer.City)
        updateCommand.Parameters.AddWithValue("@NewState", newCustomer.State)
        updateCommand.Parameters.AddWithValue("@NewZipCode", newCustomer.ZipCode)
        updateCommand.Parameters.AddWithValue("@OldCustomerID", oldCustomer.CustomerID)
        updateCommand.Parameters.AddWithValue("@OldName", oldCustomer.Name)
        updateCommand.Parameters.AddWithValue("@OldAddress", oldCustomer.Address)
        updateCommand.Parameters.AddWithValue("@OldCity", oldCustomer.City)
        updateCommand.Parameters.AddWithValue("@OldState", oldCustomer.State)
        updateCommand.Parameters.AddWithValue("@OldZipCode", oldCustomer.ZipCode)

        Try
            connection.Open()
            Dim count As Integer = updateCommand.ExecuteNonQuery
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            connection.Close()
        End Try

    End Function

    Public Shared Function AddCustomer(customer As Customer) As Integer

        ' connect to sql server using MMABooksDB class methods
        Dim connection As SqlConnection = MMABooksDB.GetConnection

        ' string to store sql insert statement
        Dim insertStatement As String =
            "INSERT Customers " &
            "(Name, Address, City, State, ZipCode) " &
            "VALUES (@Name, @Address, @City, @State, @ZipCode)"

        ' INSERT Command using INSERT statement from connected sql server
        Dim insertCommand As New SqlCommand(insertStatement, connection)

        ' inserts new value from customer string
        insertCommand.Parameters.AddWithValue("@Name", customer.Name)
        insertCommand.Parameters.AddWithValue("@Address", customer.Address)
        insertCommand.Parameters.AddWithValue("@City", customer.City)
        insertCommand.Parameters.AddWithValue("@State", customer.State)
        insertCommand.Parameters.AddWithValue("@ZipCode", customer.ZipCode)

        Try
            connection.Open()
            insertCommand.ExecuteNonQuery()

            ' string to store sql select statement to retrieve customer ID for newly created customer
            Dim selectStatement As String =
                "SELECT IDENT_CURRENT('Customers') FROM Customers"

            ' SELECT command using SELECT statement from connected sql server
            Dim selectCommand As New SqlCommand(selectStatement, connection)
            Dim customerID As Integer = CInt(selectCommand.ExecuteScalar)

            ' returning customerID
            Return customerID

        Catch ex As Exception
            Throw ex
        Finally
            connection.Close()
        End Try
    End Function
    Public Shared Function DeleteCustomer(customer As Customer) As Boolean

        ' connection from MMABooksDB class method
        Dim connection As SqlConnection = MMABooksDB.GetConnection

        ' string for SQL DELETE Statement
        Dim deleteStatement As String =
            "DELETE FROM Customers " &
            "WHERE CustomerID = @CustomerID " &
            "AND Name = @Name " &
            "AND Address = @Address " &
            "AND City = @City " &
            "AND State = @State " &
            "AND ZipCode = @ZipCode "

        ' DELETE command using DELETE statement from connected sql server
        Dim deleteCommand As New SqlCommand(deleteStatement, connection)

        ' delete the values from customer
        deleteCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID)
        deleteCommand.Parameters.AddWithValue("@Name", customer.Name)
        deleteCommand.Parameters.AddWithValue("@Address", customer.Address)
        deleteCommand.Parameters.AddWithValue("@City", customer.City)
        deleteCommand.Parameters.AddWithValue("@State", customer.State)
        deleteCommand.Parameters.AddWithValue("@ZipCode", customer.ZipCode)

        Try
            connection.Open()
            Dim count As Integer = deleteCommand.ExecuteNonQuery
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            connection.Close()
        End Try

    End Function
End Class
