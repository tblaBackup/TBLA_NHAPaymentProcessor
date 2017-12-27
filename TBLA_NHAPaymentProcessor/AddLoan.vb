Public Class AddLoan
    Public Event CreateLoan()
    Private CustomerID As Integer
    Private TBLAAcctNuber As String
    Private LoanPrincipal As Double
    Private LoanInterestRate As Double
    Private LoanTerm As Integer
    Private LoanStartDate As Date
    Private LoanMRC As Double
    Public Property DLoanPrincipal As Double
        Set(value As Double)
            LoanPrincipal = value
        End Set
        Get
            Return LoanPrincipal
        End Get
    End Property
    Public Property DLoanTerm As Integer
        Set(value As Integer)
            LoanTerm = value
        End Set
        Get
            Return LoanTerm
        End Get
    End Property
    Public Property DLoanStartDate As Date
        Set(value As Date)
            LoanStartDate = value
        End Set
        Get
            Return LoanStartDate
        End Get
    End Property
    Public Property DLoanInterestRate As Double
        Set(value As Double)
            LoanInterestRate = value
        End Set
        Get
            Return LoanInterestRate
        End Get
    End Property
    Public Property DcustomerID As Integer
        Set(value As Integer)
            CustomerID = value
        End Set
        Get
            Return CustomerID
        End Get
    End Property
    Public Property DTBLAAcctNum As String
        Set(value As String)
            TBLAAcctNuber = value
        End Set
        Get
            Return TBLAAcctNuber
        End Get
    End Property
    Public Property DLoanMRC As Double
        Set(value As Double)
            LoanMRC = value
        End Set
        Get
            Return LoanMRC
        End Get
    End Property
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DcustomerID = ComboBox1.SelectedValue
        DTBLAAcctNum = TextBox2.Text
        DLoanInterestRate = NumericUpDown2.Value
        DLoanPrincipal = NumericUpDown1.Value
        DLoanTerm = NumericUpDown3.Value
        DLoanStartDate = DateTimePicker1.Value
        DLoanMRC = NumericUpDown4.Value
        RaiseEvent CreateLoan()
    End Sub
    Public Sub Clearer()
        TextBox2.Text = Nothing
        NumericUpDown1.Value = 0
        NumericUpDown2.Value = 0
        NumericUpDown3.Value = 0
        NumericUpDown4.Value = 0
        DateTimePicker1.Value = Now()
    End Sub
End Class
