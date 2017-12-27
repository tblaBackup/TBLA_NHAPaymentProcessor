Imports System.Windows.Forms

Public Class DrillDownDialog
    Public selectedCustomer As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        selectedCustomer = -1
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        selectedCustomer = ComboBox1.SelectedValue
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DrillDownDialog_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        selectedCustomer = ComboBox1.SelectedValue
        Me.Close()
    End Sub
End Class
