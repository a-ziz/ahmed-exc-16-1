Public Class frmCustomerMaintenance
    Dim customer As Customer

    Private Sub btnGetCustomer_Click(ByVal sender As System.Object,
            ByVal e As System.EventArgs) Handles btnGetCustomer.Click
        If Validator.IsPresent(txtCustomerID) AndAlso
           Validator.IsInt32(txtCustomerID) Then
            Dim customerID As Integer = CInt(txtCustomerID.Text)
            Me.GetCustomer(customerID)
            If customer Is Nothing Then
                MessageBox.Show("No customer found with this ID. " &
                    "Please try again.", "Customer Not Found")
                Me.ClearControls()
            Else
                Me.DisplayCustomer()
            End If
        End If
    End Sub

    Private Sub GetCustomer(ByVal customerID As Integer)
        Try
            customer = CustomerDB.GetCustomer(customerID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub

    Private Sub ClearControls()
        txtCustomerID.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZipCode.Text = ""
        btnModify.Enabled = False
        btnDelete.Enabled = False
        txtCustomerID.Select()
    End Sub

    Private Sub DisplayCustomer()
        txtName.Text = customer.Name
        txtAddress.Text = customer.Address
        txtCity.Text = customer.City
        txtState.Text = customer.State
        txtZipCode.Text = customer.ZipCode
        btnModify.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object,
            ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim addCustomerForm As New frmAddModifyCustomer
        addCustomerForm.addCustomer = True
        Dim result As DialogResult = addCustomerForm.ShowDialog
        If result = DialogResult.OK Then
            customer = addCustomerForm.customer
            txtCustomerID.Text = customer.CustomerID.ToString
            Me.DisplayCustomer()
        End If
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object,
            ByVal e As System.EventArgs) Handles btnModify.Click
        Dim modifyCustomerForm As New frmAddModifyCustomer
        modifyCustomerForm.addCustomer = False
        modifyCustomerForm.customer = customer
        Dim result As DialogResult = modifyCustomerForm.ShowDialog
        If result = DialogResult.OK Then
            customer = modifyCustomerForm.customer
            Me.DisplayCustomer()
        ElseIf result = DialogResult.Retry Then
            Me.GetCustomer(customer.CustomerID)
            If customer IsNot Nothing Then
                Me.DisplayCustomer()
            Else
                Me.ClearControls()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object,
            ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim result As DialogResult =
              MessageBox.Show("Delete " & customer.Name & "?",
              "Confirm Delete", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Try
                If Not CustomerDB.DeleteCustomer(customer) Then
                    MessageBox.Show("Another user has updated or deleted " &
                    "that customer.", "Database Error")
                    Me.GetCustomer(customer.CustomerID)
                    If customer IsNot Nothing Then
                        Me.DisplayCustomer()
                    Else
                        Me.ClearControls()
                    End If
                Else
                    Me.ClearControls()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.GetType.ToString)
            End Try
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object,
            ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class