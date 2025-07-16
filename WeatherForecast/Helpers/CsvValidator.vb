Public Class CsvValidator
    Public Shared Function ValidateRecord(line As String, lineNumber As Integer)
        If String.IsNullOrWhiteSpace(line) Then
            Throw New CsvParsingException($"Line {lineNumber} is empty.")
        End If

        Dim values = line.Split(","c)

        If values.Length < 3 Then
            Throw New CsvParsingException($"Line {lineNumber} is invalid: expected 3 columns, got {values.Length}.")
        End If

        Dim latitude As Double
        Dim longitude As Double

        If Not Double.TryParse(values(0).Trim, latitude) Then
            Throw New CsvParsingException($"Invalid Latitude at Line {lineNumber}")
        End If

        If Not Double.TryParse(values(1).Trim, longitude) Then
            Throw New CsvParsingException($"Invalid Longitude at Line {lineNumber}")
        End If

        Dim LocationName = values(2).Trim()

        If String.IsNullOrEmpty(LocationName) Then
            Throw New CsvParsingException($"Location Name is Empty at Line {lineNumber}")
        End If

        Return New LocationViewModel With {
         .Latitude = latitude,
         .Longitude = longitude,
         .LocationName = LocationName
        }

    End Function

End Class
