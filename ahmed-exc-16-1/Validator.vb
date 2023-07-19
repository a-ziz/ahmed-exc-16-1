Public Class Validator

    Public Shared Property Title() As String = "Entry Error"

    Public Shared Function IsPresent(ByVal control As Control) _
            As Boolean
        If control.GetType.ToString = "System.Windows.Forms.TextBox" Then
            Dim textBox As TextBox = CType(control, TextBox)
            If textBox.Text = "" Then
                MessageBox.Show(textBox.Tag.ToString & " is a required field.", Title)
                control.Select()
                Return False
            Else
                Return True
            End If
        ElseIf control.GetType.ToString = "System.Windows.Forms.ComboBox" Then
            Dim comboBox As ComboBox = CType(control, ComboBox)
            If comboBox.SelectedIndex = -1 Then
                MessageBox.Show(comboBox.Tag.ToString & " is a required field.", Title)
                comboBox.Select()
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Public Shared Function IsDecimal(ByVal textBox As TextBox) _
            As Boolean
        Try
            Convert.ToDecimal(textBox.Text)
            Return True
        Catch ex As FormatException
            MessageBox.Show(textBox.Tag.ToString & " must be a decimal value.", Title)
            textBox.Select()
            textBox.SelectAll()
            Return False
        End Try

    End Function

    Public Shared Function IsInt32(ByVal textBox As TextBox) _
            As Boolean
        Try
            Convert.ToInt32(textBox.Text)
            Return True
        Catch ex As FormatException
            MessageBox.Show(textBox.Tag.ToString & " must be an integer value.", Title)
            textBox.Select()
            textBox.SelectAll()
            Return False
        End Try
    End Function

    Public Shared Function IsWithinRange(ByVal textBox As TextBox,
            ByVal min As Decimal, ByVal max As Decimal) As Boolean
        Dim number As Decimal = CDec(textBox.Text)
        If number < min OrElse number > max Then
            MessageBox.Show(textBox.Tag.ToString & " must be between " &
                min & " and " & max & ".", Title)
            textBox.Select()
            textBox.SelectAll()
            Return False
        Else
            Return True
        End If
    End Function
End Class
