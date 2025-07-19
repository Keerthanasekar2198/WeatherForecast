Imports System.IO
Imports WeatherForecast.WeatherForecast.Helpers

Namespace WeatherForecast.Services
    Public Class LocationParserService
        Implements ILocationParserService
        Public Function ParseCsvFile(file As HttpPostedFileBase) As List(Of LocationViewModel) Implements ILocationParserService.ParseCsvFile
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
End Namespace
