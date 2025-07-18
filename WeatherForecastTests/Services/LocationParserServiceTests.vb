Imports System.IO
Imports System.Web
Imports Moq
Imports WeatherForecast

<TestClass>
Public Class LocationParserServiceTests
    <TestMethod>
    Public Sub ParseCsvFile_ReturnLocationsSuccessfully()
        Dim csvContent As String = "Latitude,Longitude,LocationName" & vbCrLf &
                           "51.507351,-0.127758,London,UK" & vbCrLf &
                           "55.864239,-4.251806,Glasgow,UK"
        Dim byteArray As Byte() = Text.Encoding.UTF8.GetBytes(csvContent)
        Dim stream As New MemoryStream(byteArray)

        Dim mockFile As New Mock(Of HttpPostedFileBase)()
        mockFile.Setup(Function(f) f.InputStream).Returns(stream)

        Dim locationParserService As New LocationParserService

        Dim parsedResults = locationParserService.ParseCsvFile(mockFile.Object)

        Assert.AreEqual("London", parsedResults(0).LocationName)
        Assert.AreEqual("51.507351", parsedResults(0).Latitude)
        Assert.AreEqual("-0.127758", parsedResults(0).Longitude)

    End Sub

    <TestMethod>
    Public Sub ParseCsvFile_ThrowsCsvParsingException_WhenLocationIsNullOrEmpty()
        Dim csvContent As String = "Latitude,Longitude,LocationName" & vbCrLf &
                           "51.507351,-0.127758," & vbCrLf
        Dim byteArray As Byte() = Text.Encoding.UTF8.GetBytes(csvContent)
        Dim stream As New MemoryStream(byteArray)

        Dim mockFile As New Mock(Of HttpPostedFileBase)()
        mockFile.Setup(Function(f) f.InputStream).Returns(stream)

        Dim locationParserService As New LocationParserService

        Try
            Dim result = locationParserService.ParseCsvFile(mockFile.Object)
            Assert.Fail("Expected CsvParsingException was not thrown.")
        Catch ex As CsvParsingException
            Assert.IsTrue(ex.Message.Contains("Location Name is Empty"))
        End Try

    End Sub

    <TestMethod>
    Public Sub ParseCsvFile_ThrowsCsvParsingException_WhenLatitudeIsNull()
        Dim csvContent As String = "Latitude,Longitude,LocationName" & vbCrLf &
                           ",-0.127758,Glassgow,UK" & vbCrLf
        Dim byteArray As Byte() = Text.Encoding.UTF8.GetBytes(csvContent)
        Dim stream As New MemoryStream(byteArray)

        Dim mockFile As New Mock(Of HttpPostedFileBase)()
        mockFile.Setup(Function(f) f.InputStream).Returns(stream)

        Dim locationParserService As New LocationParserService

        Try
            Dim result = locationParserService.ParseCsvFile(mockFile.Object)
            Assert.Fail("Expected CsvParsingException was not thrown.")
        Catch ex As CsvParsingException
            Assert.IsTrue(ex.Message.Contains("Invalid Latitude"))
        End Try

    End Sub

    <TestMethod>
    Public Sub ParseCsvFile_ThrowsCsvParsingException_WhenLongitudeIsNull()
        Dim csvContent As String = "Latitude,Longitude,LocationName" & vbCrLf &
                           "51.507351,,""Glasgow,UK""" & vbCrLf
        Dim byteArray As Byte() = Text.Encoding.UTF8.GetBytes(csvContent)
        Dim stream As New MemoryStream(byteArray)

        Dim mockFile As New Mock(Of HttpPostedFileBase)()
        mockFile.Setup(Function(f) f.InputStream).Returns(stream)

        Dim locationParserService As New LocationParserService

        Try
            Dim result = locationParserService.ParseCsvFile(mockFile.Object)
            Assert.Fail("Expected CsvParsingException was not thrown.")
        Catch ex As CsvParsingException
            Assert.IsTrue(ex.Message.Contains("Invalid Longitude"))
        End Try

    End Sub
End Class
