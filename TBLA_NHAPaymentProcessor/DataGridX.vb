Public Class DataGridX
    Private selectedID As Integer
    Public Property Selected As Integer
        Set(value As Integer)
            selectedID = value
        End Set
        Get
            Return selectedID
        End Get
    End Property
    Private Sub DataGridX_Click(sender As Object, e As EventArgs) Handles Me.Click
        If DataGridView1.Rows.Count > 0 Then
            Selected = DataGridView1.SelectedRows(0).Cells(0).Value
            MsgBox(Selected)
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Rows.Count > 0 Then
            Selected = DataGridView1.SelectedRows(0).Cells(0).Value
            MsgBox(Selected)
        End If
    End Sub
End Class
