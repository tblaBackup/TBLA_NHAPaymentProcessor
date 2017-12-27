Public Class Form1
    Private Sub HomeBtn_Click(sender As Object, e As EventArgs) Handles HomeBtn.Click
        GohomeBTN()
    End Sub

    Public Sub loadLoansByCustomerID(ByVal CustomerID As Integer, ByRef myDisplay As ActiveLoan)
        Dim myContext As New DataClasses1DataContext
        Dim myTble As DataTable = myGlobalz.ToDataTable(Of CustomersLoansResult)(myContext.CustomersLoans1(CustomerID).ToList)
        myGlobalz.loadGrid(myTble, myDisplay.DataGridView2)
        Panel1.Controls.Clear()
        Panel1.Controls.Add(myDisplay)
        myContext = Nothing
    End Sub
    Public Sub loadCustomers()
        Dim myControl1 As New SearchDialog
        Dim myControl2 As New ActiveLoan
        myControl2.Name = "DActiveLoan"
        Dim SearchResponse As DialogResult = myControl1.ShowDialog()
        Select Case SearchResponse
            Case vbOK
                Select Case myControl1.SearchParameter
                    Case "Account"
                        loadCustBYaccountNumber(myControl1.SearchString, myControl2)
                    Case "First Name"
                        loadCustByFirstName(myControl1.SearchString, myControl2)
                    Case "Last Name"
                        loadCustByLastName(myControl1.SearchString, myControl2)
                End Select
            Case vbCancel
                GohomeBTN()
            Case Else
                GohomeBTN()
        End Select

    End Sub
    Public Sub loadCustByFirstName(ByVal SearchString As String, ByRef myDisplay As ActiveLoan)
        Dim myContext As New DataClasses1DataContext
        Dim myTble As DataTable = myGlobalz.ToDataTable(Of SearchCustomerByFirstNameResult)(myContext.SearchCustomerByFirstName(SearchString).ToList)
        myGlobalz.loadGrid(myTble, myDisplay.DataGridView1)
        Panel1.Controls.Clear()
        Panel1.Controls.Add(myDisplay)
        myDisplay.DataGridView1.Rows(0).Selected = True
        myContext = Nothing
    End Sub
    Public Sub loadCustByLastName(ByVal SearchString As String, ByRef myDisplay As ActiveLoan)
        Dim myContext As New DataClasses1DataContext
        Dim myTble As DataTable = myGlobalz.ToDataTable(Of SearchCustomerByLastNameResult)(myContext.SearchCustomerByLastName(SearchString).ToList)
        myGlobalz.loadGrid(myTble, myDisplay.DataGridView1)
        Panel1.Controls.Clear()
        Panel1.Controls.Add(myDisplay)
        myDisplay.DataGridView1.Rows(0).Selected = True
        myContext = Nothing
    End Sub
    Public Sub loadCustBYaccountNumber(ByVal SearchString As String, ByRef myDisplay As ActiveLoan)
        Dim myContext As New DataClasses1DataContext
        Dim myTble As DataTable = myGlobalz.ToDataTable(Of SearchCustomerByAccNumResult)(myContext.SearchCustomerByAccNum(SearchString).ToList)
        myGlobalz.loadGrid(myTble, myDisplay.DataGridView1)
        Panel1.Controls.Clear()
        Panel1.Controls.Add(myDisplay)
        myDisplay.DataGridView1.Rows(0).Selected = True
        myContext = Nothing
    End Sub
    Public Sub LoadActiveCustLoans(ByVal CustID As Integer, ByRef MyDisplay As ActiveLoan)
        Dim myContext As New DataClasses1DataContext
        Dim myTble As DataTable = myGlobalz.ToDataTable(Of CustomersLoansResult)(myContext.CustomersLoans1(CustID).ToList)
        myGlobalz.loadGrid(myTble, MyDisplay.DataGridView2)
        myContext = Nothing
    End Sub
    Public Sub LoadActiveLoanPayments(ByVal LoanID As Integer, ByRef MyDisplay As ActiveLoan)
        Dim myContext As New DataClasses1DataContext
        Dim myTble As DataTable = myGlobalz.ToDataTable(Of LoansPaymentsResult)(myContext.LoansPayments(LoanID).ToList)
        myGlobalz.loadGrid(myTble, MyDisplay.DataGridView3)
        myContext = Nothing
    End Sub

    Private Sub PaymentBtn_Click(sender As Object, e As EventArgs) Handles PaymentBtn.Click
        loadCustomers()
        Dim avf As Control() = Me.Controls.Find("DActiveLoan", True)
        Dim activer As ActiveLoan = avf(0)
        Dim activeCustID As Integer = -1
        Dim activeLoanID As Integer = -1
        Dim MyContext As New DataClasses1DataContext
        Try
            activeCustID = activer.DataGridView1.SelectedRows(0).Cells(0).Value
            LoadActiveCustLoans(activeCustID, activer)
            AddHandler activer.PaymentProcessor1.PaymentProcessed, Sub()
                                                                       MyContext.InsertIntoCash(activer.PaymentProcessor1.NumericUpDown2.Value, activer.PaymentProcessor1.DateTimePicker1.Value, activeLoanID)
                                                                       Dim DCashID As Integer = MyContext.ReturnCashID(activer.PaymentProcessor1.NumericUpDown2.Value, activer.PaymentProcessor1.DateTimePicker1.Value, activeLoanID)
                                                                       Dim monthlyCost = MyContext.LoansMRC(activeLoanID)
                                                                       Dim Payment As Double = activer.PaymentProcessor1.NumericUpDown2.Value
                                                                       Dim sumOnHold As Double = 0
                                                                       Try
                                                                           SumonHold = MyContext.SumOnHold(activeLoanID)
                                                                       Catch ex As Exception
                                                                           sumOnHold = 0
                                                                       End Try
                                                                       Dim PaymentID As Integer = -1
                                                                       Dim PaymentDate As New Date
                                                                       Dim AmortID As Integer = 0
                                                                       Dim InterestAmount, PricipalAmount As Double
                                                                       Payment += sumOnHold
                                                                       While Payment > monthlyCost
                                                                           PaymentID = MyContext.ReturnNextPayment(activeLoanID)
                                                                           AmortID = MyContext.ReturnNextAmortID(activeLoanID)
                                                                           InterestAmount = MyContext.Re2rnInterest(activeLoanID, PaymentID)
                                                                           PricipalAmount = MyContext.Re2rnPrincipal(activeLoanID, PaymentID)
                                                                           PaymentDate = activer.PaymentProcessor1.DateTimePicker1.Value
                                                                           MyContext.InsertDataIntoTable(PaymentDate, monthlyCost, activeLoanID, InterestAmount, PricipalAmount, DCashID)
                                                                           MyContext.MarkAmortPaid(AmortID)
                                                                           Payment -= monthlyCost
                                                                       End While
                                                                       MyContext.InsertIntoHolding(Payment, activeLoanID)
                                                                       MyContext = New DataClasses1DataContext
                                                                       LoadActiveLoanPayments(activeLoanID, activer)
                                                                       'Print Receipt
                                                                       Dim Saver As String = activer.PaymentProcessor1.TextBox1.Text + Now.Day.ToString + "-" + Now.Month.ToString + "-" + Now.Year.ToString
                                                                       myGlobalz.TBLPrinter(Saver, activer.PaymentProcessor1.TextBox1.Text, Now, DCashID, activer.PaymentProcessor1.NumericUpDown2.Value)
                                                                       myGlobalz.PrintPdf(Saver + ".pdf")
                                                                       myGlobalz.loadGrid(myGlobalz.ToDataTable(Of LoansPaymentsResult)(MyContext.LoansPayments(activeLoanID).ToList), activer.DataGridView3)
                                                                       activer.PaymentProcessor1.Clearer()
                                                                   End Sub
            If activer.DataGridView2.Rows.Count > 0 Then
                With activer.DataGridView2.Rows.Item(0)
                    .Selected = True
                    activeLoanID = .Cells(0).Value
                End With
                LoadActiveLoanPayments(activeLoanID, activer)
            End If
            activer.PaymentProcessor1.TextBox1.Text = MyContext.DCustFullname(activeCustID).ToString
        Catch ex As Exception
            MsgBox("Your search did not return a result")
            GohomeBTN()
        End Try



    End Sub
    Public Sub AddCustomer()
        Panel1.Controls.Clear()
        Dim myControl As New AddCustomer
        myControl.Name = "AddCuztomer"
        Dim myContext As New DataClasses1DataContext
        myGlobalz.loadGrid(myGlobalz.ToDataTable(Of tblCustomer)(myContext.tblCustomers.ToList), myControl.DataGridView1)
        AddHandler myControl.SubmitCustomer, Sub()
                                                 myContext.CreeightCustomer(myControl.TextBox1.Text, myControl.TextBox2.Text, myControl.TextBox3.Text, myControl.TextBox4.Text, myControl.TextBox5.Text)
                                                 myControl.Clearer()
                                                 myContext = New DataClasses1DataContext
                                                 myGlobalz.loadGrid(myGlobalz.ToDataTable(Of tblCustomer)(myContext.tblCustomers.ToList), myControl.DataGridView1)
                                                 MsgBox("Customer Created Successfully")
                                             End Sub
        Panel1.Controls.Add(myControl)
    End Sub
    Public Sub GohomeBTN()
        Panel1.Controls.Clear()
        Dim myControl As New DataGridX
        Dim myContext As New DataClasses1DataContext
        myGlobalz.loadGrid(myGlobalz.ToDataTable(Of tblCustomer)(myContext.tblCustomers.ToList), myControl.DataGridView1)
        If myControl.DataGridView1.Rows.Count > 1 Then
            myControl.Selected = myControl.DataGridView1.Rows(0).Cells(0).Value
        End If
        myContext = Nothing
        Panel1.Controls.Add(myControl)
    End Sub
    Private Sub AddCustBtn_Click(sender As Object, e As EventArgs) Handles AddCustBtn.Click
        AddCustomer()
    End Sub
    Public Function ManualAmortSetup() As ManualAmort
        'Dim opener2 As New DrillDownDialog
        'opener2.ComboBox1.DataSource = myGlobalz.ToDataTable(Of FullNameCust)(MyContext.FullNameCusts.ToList)
        'opener2.ComboBox1.DisplayMember = "FullName"
        'opener2.ComboBox1.ValueMember = "CustomerID"
        'Dim myResult As DialogResult = opener2.ShowDialog()
        'If myResult = vbOK Then
        Dim Opener As New ManualAmort
        Dim MyContext As New DataClasses1DataContext
        Dim MyControl As New ManalAmortDialog
            Dim myResponse As DialogResult = MyControl.ShowDialog()
            If myResponse = vbOK Then
                Dim myTble As DataTable = myGlobalz.ToDataTable(Of ReturnLoanAmortsResult)(MyContext.ReturnLoanAmorts(MyControl.SearchParameter).ToList)
                myGlobalz.loadGrid(myTble, Opener.DataGridView1)
                Try
                Opener.NumericUpDown1.Value = myTble.Rows(0).Item("PaymentNumber") + 1
            Catch ex As Exception
                    Opener.NumericUpDown1.Value = 1
                End Try
            ' Dim assist As IQueryable(Of CustomersLoan) = From theLoans In MyContext.CustomersLoans Where theLoans.CustomerID = opener2.selectedCustomer Select theLoans
            Dim mySearchValue As String = MyControl.SearchParameter
            Dim myTble2 = (MyContext.SearchLoansByAccountNum(mySearchValue)).ToList
            Opener.NumericUpDown2.Value = myTble2(0).MonthlyInstallment
            Opener.ActiveLoan = myTble2(0).loanID
        End If
            MyContext = Nothing
            Return Opener
        'End If
        'Return Nothing
    End Function
    Private Sub ManualAmortToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManualAmortToolStripMenuItem.Click
        Dim actrl As ManualAmort = ManualAmortSetup()


        AddHandler actrl.Submitted, Sub()
                                        Dim myContext As New DataClasses1DataContext
                                        myContext.CreateAmort(actrl.ActiveLoan, actrl.NumericUpDown1.Value, actrl.NumericUpDown2.Value, actrl.NumericUpDown3.Value, actrl.NumericUpDown4.Value)
                                        actrl.NumericUpDown1.Value += 1
                                        actrl.NumericUpDown4.Value = 0
                                        actrl.NumericUpDown3.Value = 0
                                        myContext = New DataClasses1DataContext
                                        Dim myTble As DataTable = myGlobalz.ToDataTable(Of ReturnLoanAmortsByIDResult)(myContext.ReturnLoanAmortsByID(actrl.ActiveLoan).ToList)
                                        myGlobalz.loadGrid(myTble, actrl.DataGridView1)
                                    End Sub
        Panel1.Controls.Clear()
        Panel1.Controls.Add(actrl)
    End Sub
    Public Function AddALoan() As AddLoan
        Dim opener As New AddLoan
        Dim myContext As New DataClasses1DataContext
        Dim myTble As DataTable = myGlobalz.ToDataTable(Of FullNameCust)(myContext.FullNameCusts.ToList)
        Dim myTble2 As DataTable = myGlobalz.ToDataTable(Of tblLoan)(myContext.tblLoans.ToList)
        With opener.ComboBox1
            .DataSource = myTble
            .DisplayMember = "FullName"
            .ValueMember = "CustomerID"
        End With
        With opener.DataGridView1
            .DataSource = myTble2
        End With
        myContext = Nothing
        Return opener
    End Function
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Panel1.Controls.Clear()
        Dim Myopener As AddLoan = AddALoan()
        Myopener.Name = "TheFrame"
        AddHandler Myopener.CreateLoan, Sub()
                                            Dim myContext As New DataClasses1DataContext
                                            myContext.CreateLoanAccountNumber(Myopener.ComboBox1.SelectedValue, Myopener.TextBox2.Text)
                                            Dim custAccNo As Integer = CInt(myContext.ReturnLastLoanACCNum)
                                            myContext.CreateLoan(Myopener.DLoanPrincipal, Myopener.DLoanInterestRate, Myopener.DLoanTerm, Myopener.DLoanStartDate, Myopener.DLoanMRC, custAccNo, True)
                                            Myopener.Clearer()
                                        End Sub
        Panel1.Controls.Add(Myopener)
    End Sub

    Private Sub SearchBtn_Click(sender As Object, e As EventArgs) Handles SearchBtn.Click
        myGlobalz.TBLPrinter("Tester1", "That Guy", Now, 3, 50)
        myGlobalz.PrintPdf("tester1.pdf")
    End Sub
End Class
