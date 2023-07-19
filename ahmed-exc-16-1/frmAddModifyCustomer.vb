Public Class frmAddModifyCustomer

    Public addCustomer As Boolean
    Public customer As Customer

    Private Sub frmAddModifyCustomer_Load(ByVal sender As System.Object,
            ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LoadStateComboBox()
        If addCustomer Then
            Me.Text = "Add Customer"
            cboStates.SelectedIndex = -1
        Else
            Me.Text = "Modify Customer"
            Me.DisplayCustomerData()
        End If
    End Sub

    Private Sub LoadStateComboBox()
        Dim stateList As List(Of State)
        Try
            stateList = StateDB.GetStateList
            cboStates.DataSource = stateList
            cboStates.DisplayMember = "StateName"
            cboStates.ValueMember = "StateCode"
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        End Try
    End Sub

    Private Sub DisplayCustomerData()
        txtName.Text = customer.Name
        txtAddress.Text = customer.Address
        txtCity.Text = customer.City
        cboStates.SelectedValue = customer.State
        txtZipCode.Text = customer.ZipCode
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object,
            ByVal e As System.EventArgs) Handles btnAccept.Click
        If IsValidData() Then
            If addCustomer Then
                customer = New Customer
                Me.PutCustomerData(customer)
                Try
                    customer.CustomerID = CustomerDB.AddCustomer(customer)
                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    MessageBox.Show(ex.Message, ex.GetType.ToString)
                End Try
            Else
                Dim newCustomer As New Customer
                newCustomer.CustomerID = customer.CustomerID
                Me.PutCustomerData(newCustomer)
                Try
                    If Not CustomerDB.UpdateCustomer(customer, newCustomer) Then
                        MessageBox.Show("Another user has updated or deleted that customer.",
                        "Database Error")
                        Me.DialogResult = DialogResult.Retry
                    Else
                        customer = newCustomer
                        Me.DialogResult = DialogResult.OK
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, ex.GetType.ToString)
                End Try
            End If
        End If
    End Sub

    Private Function IsValidData() As Boolean
        Return _
            Validator.IsPresent(txtName) AndAlso
            Validator.IsPresent(txtAddress) AndAlso
            Validator.IsPresent(txtCity) AndAlso
            Validator.IsPresent(cboStates) AndAlso
            Validator.IsPresent(txtZipCode)
    End Function

    Private Sub PutCustomerData(ByVal customer As Customer)
        customer.Name = txtName.Text
        customer.Address = txtAddress.Text
        customer.City = txtCity.Text
        customer.State = cboStates.SelectedValue.ToString
        customer.ZipCode = txtZipCode.Text
    End Sub

End Class
