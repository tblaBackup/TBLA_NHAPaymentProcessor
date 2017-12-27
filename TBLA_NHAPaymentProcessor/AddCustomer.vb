Public Class AddCustomer
    Private CustFName As String
    Private CustLName As String
    Private Addr1 As String
    Private Addr2 As String
    Private Addr3 As String
    Public Event SubmitCustomer()
    Public Property DCustFName As String
        Get
            Return CustFName
        End Get
        Set(value As String)
            CustFName = value
        End Set
    End Property
    Public Property DCustLName As String
        Set(value As String)
            CustLName = value
        End Set
        Get
            Return CustLName
        End Get
    End Property
    Public Property DAddr1 As String
        Set(value As String)
            Addr1 = value
        End Set
        Get
            Return Addr1
        End Get
    End Property
    Public Property DAddr2 As String
        Set(value As String)
            Addr2 = value
        End Set
        Get
            Return Addr2
        End Get
    End Property
    Public Property DAddr3 As String
        Set(value As String)
            Addr3 = value
        End Set
        Get
            Return Addr3
        End Get
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DCustFName = TextBox1.Text
        DCustLName = TextBox3.Text
        DAddr1 = TextBox3.Text
        DAddr2 = TextBox4.Text
        dAddr3 = TextBox5.Text
        RaiseEvent SubmitCustomer()
    End Sub
    Public Sub Clearer()
        TextBox1.Text = Nothing
        TextBox2.Text = Nothing
        TextBox3.Text = Nothing
        TextBox4.Text = Nothing
        TextBox5.Text = Nothing
    End Sub
End Class
