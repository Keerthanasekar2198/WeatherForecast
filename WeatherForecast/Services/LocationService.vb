Imports System.IO

Public Class LocationService
    Implements ILocationService
    Public Function ParseCsvFile(file As HttpPostedFileBase) As List(Of LocationViewModel) Implements ILocationService.ParseCsvfile
        Dim Locations As New List(Of LocationViewModel)()

        Using reader As New StreamReader(file.InputStream)
            Dim lineNumber As Integer = 1

            reader.ReadLine()

            While Not reader.EndOfStream
                Dim line = reader.ReadLine()
                lineNumber += 1

                Dim location = CsvValidator.ValidateRecord(line, lineNumber)

                If location IsNot Nothing Then
                    Locations.Add(location)
                End If
            End While
        End Using

        Return Locations
    End Function
End Class
