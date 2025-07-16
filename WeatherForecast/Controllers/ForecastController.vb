Imports System.IO

Public Class ForecastController
    Inherits Controller

    Private ReadOnly _locationService As ILocationService

    Public Sub New()
        _locationService = New LocationService()
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
            Dim locations = _locationService.ParseCsvFile(forecastCsvFile)
            ViewBag.UploadedFileName = Path.GetFileName(forecastCsvFile.FileName)
            Return View("~/Views/Home/Index.vbhtml", locations)
        Catch ex As CsvParsingException
            ModelState.AddModelError("", ex.Message)
            Return View("~/Views/Home/Index.vbhtml")
        Catch ex As Exception
            ModelState.AddModelError("", "An unexpected error occured.")
            Return View("~/Views/Home/Index.vbhtml")
        End Try
    End Function
End Class
