Imports System.Windows.Forms

Public Class SearchDialog
    Public SearchParameter As String
    Public SearchString As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        If CheckBox1.Checked Then
            SearchParameter = "First Name"
            SearchString = TextBox1.Text
        End If
        If CheckBox2.Checked Then
            SearchParameter = "Last Name"
            SearchString = TextBox2.Text
        End If
        If CheckBox3.Checked Then
            SearchParameter = "Account"
            SearchString = TextBox3.Text
        End If
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            CheckBox2.Checked = False
            CheckBox1.Checked = False
            TextBox3.Enabled = True
        Else
            TextBox3.Enabled = False
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            CheckBox3.Checked = False
            CheckBox1.Checked = False
            TextBox2.Enabled = True
        Else
            TextBox2.Enabled = False
        End If
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            TextBox1.Enabled = True
        Else
            TextBox1.Enabled = False
        End If
    End Sub
End Class
