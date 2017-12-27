Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO.FileStream
Imports System.IO

Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 10, 10)
        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, New System.IO.FileStream("test.pdf", FileMode.Create))
        doc.Open()
        Dim MyTble As New PdfPTable(3)
        MyTble.AddCell("Cell1")
        Dim cell As PdfPCell = New PdfPCell(New Phrase("Cell 2"))
        Dim backgroundCol As BaseColor
        backgroundCol = BaseColor.GREEN
        cell.BackgroundColor = backgroundCol

        cell.BorderColor = BaseColor.BLACK

        cell.Border = Rectangle.BOTTOM_BORDER
        cell.Border = Rectangle.TOP_BORDER

        cell.BorderWidthBottom = 3.0F

        cell.BorderWidthTop = 3.0F

        cell.PaddingBottom = 10.0F

        cell.PaddingLeft = 20.0F

        cell.PaddingTop = 4.0F

        MyTble.AddCell(cell)

        MyTble.AddCell("Cell 3")

        doc.Add(MyTble)
        doc.Close()
    End Sub
End Class