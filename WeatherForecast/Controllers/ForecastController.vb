Imports System.IO

Public Class ForecastController
    Inherits Controller

    Private ReadOnly _locationParserService As ILocationParserService

    Public Sub New()
        _locationParserService = New LocationParserService()
    End Sub

    Public Function Index() As ActionResult
        Return View()
    End Function

    <HttpPost>
    Public Function UploadCsv(forecastCsvFile As HttpPostedFileBase) As ActionResult
        If forecastCsvFile Is Nothing OrElse forecastCsvFile.ContentLength = 0 Then
            ModelState.AddModelError("", "Please upload a valid CSV file.")
            Return View("~/Views/Home/Index.vbhtml")
        End If

        Try
            Dim locations = _locationParserService.ParseCsvFile(forecastCsvFile)

            Dim forecastService As New ForeCastService()

            Dim locationsForecastData As New List(Of LocationViewModel)()


            For Each location In locations
                Dim forcastResult = forecastService.GetLocationForecast(Double.Parse(location.Latitude), Double.Parse(location.Longitude), location.LocationName)
                locationsForecastData.Add(forcastResult)

            Next

            ViewBag.UploadedFileName = Path.GetFileName(forecastCsvFile.FileName)
            Return View("~/Views/Home/Index.vbhtml", locationsForecastData)
        Catch ex As CsvParsingException
            ModelState.AddModelError("", ex.Message)
            Return View("~/Views/Home/Index.vbhtml")
        Catch ex As Exception
            ModelState.AddModelError("", "An unexpected error occured.")
            Return View("~/Views/Home/Index.vbhtml")
        End Try
    End Function
End Class
