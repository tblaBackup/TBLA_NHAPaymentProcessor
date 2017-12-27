Imports System.Reflection
Imports System.ComponentModel
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO.FileStream
Imports System.IO
Public Class myGlobalz
    Public Shared Sub loadGrid(ByVal input As DataTable, ByRef Datagridview1 As DataGridView)
        Datagridview1.DataSource = input
    End Sub
    Public Shared Sub TBLPrinter(ByVal FileName As String, ByVal CustomerName As String, ByVal datePaid As Date, ByVal CashID As Integer, ByVal amtpaid As Double)
        Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 10, 10)
        Dim DFilename As String = FileName + ".pdf"
        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, New System.IO.FileStream(DFilename, FileMode.Create))
        doc.Open()
        Dim MyTble As New PdfPTable(2)
        Dim MyTble2 As New PdfPTable(2)
        Dim imageURL As String = Application.StartupPath + "\tbla_logo.jpg"
        Dim Myjpg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageURL)
        Myjpg.ScaleToFit(140.0F, 120.0F)
        Myjpg.SpacingBefore = 10.0F
        Myjpg.SpacingAfter = 1.0F
        Myjpg.Alignment = Element.ALIGN_LEFT
        doc.Add(Myjpg)
        Dim imgCell As New PdfPCell()
        imgCell.Border = Nothing
        MyTble2.AddCell(imgCell)
        Dim Header As Phrase = New Phrase("Receipt #" + CashID.ToString)
        Dim SecondndHeader As PdfPCell = New PdfPCell(Header)
        SecondndHeader.HorizontalAlignment = Element.ALIGN_RIGHT
        SecondndHeader.Border = Nothing
        MyTble2.AddCell(SecondndHeader)
        doc.Add(MyTble2)
        Dim spacer As Paragraph = New Paragraph("")
        doc.Add(spacer)
        MyTble.AddCell("Customer Name")
        Dim cell As PdfPCell = New PdfPCell(New Phrase(CustomerName))
        'cell.BorderColor = BaseColor.BLACK
        'cell.Border = Rectangle.BOTTOM_BORDER
        'cell.Border = Rectangle.TOP_BORDER
        'cell.BorderWidthBottom = 1.0F
        'cell.BorderWidthTop = 1.0F

        'cell.PaddingBottom = 10.0F
        'cell.PaddingLeft = 20.0F
        'cell.PaddingTop = 4.0F
        MyTble.AddCell(cell)
        MyTble.AddCell("Date Paid")
        MyTble.AddCell(datePaid)
        MyTble.AddCell("Amt Paid")
        MyTble.AddCell(amtpaid)
        doc.Add(MyTble)
        doc.Close()
    End Sub
    Public Shared Sub PrintPdf(ByVal sPdfToPrint As String)
        If Not File.Exists(sPdfToPrint) Then Exit Sub
        If File.Exists(sPdfToPrint) Then
            Dim oProcess As New System.Diagnostics.Process
            With oProcess.StartInfo
                .CreateNoWindow = True
                .WindowStyle = ProcessWindowStyle.Hidden
                .Verb = "print"
                .UseShellExecute = True
                .FileName = sPdfToPrint
            End With
            oProcess.Start()
            System.Threading.Thread.Sleep(120000)
            oProcess.CloseMainWindow()
            oProcess.Close()
            oProcess.Dispose()
            oProcess = Nothing

        End If

    End Sub
    Public Shared Sub PrintReceipt(ByVal CashID)
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
    Public Shared Function ToDataTable(Of t)(
                                                  ByVal list As IList(Of t)
                                               ) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim columns As PropertyInfo() = Nothing
        If list Is Nothing Then Return dt
        For Each Record As t In list
            If columns Is Nothing Then
                columns = (CType(Record.[GetType](), Type)).GetProperties()
                For Each GetProperty As PropertyInfo In columns
                    Dim colType As Type = GetProperty.PropertyType
                    If (colType.IsGenericType) AndAlso (colType.GetGenericTypeDefinition() = GetType(Nullable(Of ))) Then
                        colType = colType.GetGenericArguments()(0)
                    End If

                    dt.Columns.Add(New DataColumn(GetProperty.Name, colType))
                Next
            End If

            Dim dr As DataRow = dt.NewRow()
            For Each pinfo As PropertyInfo In columns
                dr(pinfo.Name) = If(pinfo.GetValue(Record, Nothing) Is Nothing, DBNull.Value, pinfo.GetValue(Record, Nothing))
            Next

            dt.Rows.Add(dr)
        Next

        Return dt

    End Function

End Class
