Imports WeatherForecast.WeatherForecast.Exceptions
Imports WeatherForecast.WeatherForecast.Helpers
Imports WeatherForecast.WeatherForecast.Services

Public Class ForecastController
    Inherits Controller

    Private ReadOnly _forecastService As IForecastService

    Public Sub New()
        _forecastService = New ForeCastService()
    End Sub

    <HttpPost>
    Public Function UploadCsv(forecastCsvFile As HttpPostedFileBase) As ActionResult
        If forecastCsvFile Is Nothing OrElse forecastCsvFile.ContentLength = 0 Then
            ModelState.AddModelError("", "CSV File is empty.")
            Return View("~/Views/Home/Index.vbhtml")
        End If

        Try
            Dim forecastData = _forecastService.ProcessForecastCSVFile(forecastCsvFile)

            ViewBag.UploadedFileName = forecastData.Item2
            ViewBag.ForecastChartData = forecastData.Item3
            ViewBag.ForecastTabularData = forecastData.Item1
            ViewBag.ShowChart = False
            Session("ForecastTabularData") = forecastData.Item1

            Return View("~/Views/Home/Index.vbhtml", forecastData.Item1)

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

        Dim forecastChartData = _forecastService.GetLocationForecast(Double.Parse(Latitude), Double.Parse(Longitude), SelectedCity)

        Dim forecastData = CType(Session("ForecastTabularData"), List(Of LocationViewModel))

        ViewBag.ForecastChartData = ChartHelper.RenderChart(forecastChartData)
        ViewBag.ForecastTabularData = forecastData
        ViewBag.ShowChart = True

        Return View("~/Views/Home/Index.vbhtml", forecastData)
    End Function
End Class
