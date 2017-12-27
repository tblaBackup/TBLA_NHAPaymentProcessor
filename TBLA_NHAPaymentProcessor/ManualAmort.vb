Public Class ManualAmort
    Public Event Submitted()
    Public ActiveLoan As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ActiveLoan = -1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        NumericUpDown4.Value = NumericUpDown2.Value - NumericUpDown3.Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent submitted()
    End Sub
End Class
