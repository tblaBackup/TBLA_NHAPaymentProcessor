Public Class PaymentProcessor
    Private PaymentAmt As Double
    Private PaymentDate As Date
    Public Event PaymentProcessed()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent PaymentProcessed()
    End Sub
    Public Sub Clearer()
        NumericUpDown2.Value = 0.0
    End Sub
End Class
