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
            ViewBag.ForecastData = ChartHelper.RenderChart(locationsForecastData(0))
            ViewBag.LocationsList = locations
            Session("LocationsList") = locations
            ViewBag.ShowChart = False
            Session("LocationsForecastData") = locationsForecastData
            Return View("~/Views/Home/Index.vbhtml", locationsForecastData)
        Catch ex As CsvParsingException
            ModelState.AddModelError("", ex.Message)
            Return View("~/Views/Home/Index.vbhtml")
        Catch ex As Exception
            ModelState.AddModelError("", ex.Message)
            Return View("~/Views/Home/Index.vbhtml")
        End Try
    End Function

    <HttpGet>
    Public Function GetForecastForCity(SelectedCity As String, Latitude As String, Longitude As String) As ActionResult
        Dim forecastService As New ForeCastService()

        Dim forecastResult = forecastService.GetLocationForecast(Double.Parse(Latitude), Double.Parse(Longitude), SelectedCity)

        Dim locationsForecastData = CType(Session("LocationsForecastData"), List(Of LocationViewModel))

        ViewBag.ForecastData = ChartHelper.RenderChart(forecastResult)

        ViewBag.LocationsList = CType(Session("LocationsList"), List(Of LocationViewModel))

        ViewBag.ShowChart = True

        Return View("~/Views/Home/Index.vbhtml", locationsForecastData)
    End Function
End Class
