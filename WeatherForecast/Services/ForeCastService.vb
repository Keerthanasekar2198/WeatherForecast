Imports System.IO
Imports WeatherForecast.WeatherForecast.Helpers
Imports WeatherForecast.WeatherForecast.Repositories
Imports WeatherForecast.WeatherForecast.Services.ExternalApi

Namespace WeatherForecast.Services
    Public Class ForeCastService
        Implements IForecastService

        Private ReadOnly _locationParserService As ILocationParserService
        Private ReadOnly _forecastRepositoryService As IForecastRepository
        Private ReadOnly _openMeteoClient As IOpenMeteoClient

        Public Sub New()
            _locationParserService = New LocationParserService()
            _forecastRepositoryService = New ForecastRepository()
            _openMeteoClient = New OpenMeteoClient()
        End Sub

        Public Function ProcessForecastCSVFile(forecastCsvFile As HttpPostedFileBase) As Tuple(Of List(Of LocationViewModel), String, Tuple(Of String, List(Of Integer), List(Of Integer), List(Of String))) Implements IForecastService.ProcessForecastCSVFile
            Dim forecastService As New ForeCastService()

            Dim parsedLocations = _locationParserService.ParseCsvFile(forecastCsvFile)

            Dim forecastDataList As New List(Of LocationViewModel)()


            For Each location In parsedLocations
                Dim data = forecastService.GetLocationForecast(Double.Parse(location.Latitude), Double.Parse(location.Longitude), location.LocationName)
                forecastDataList.Add(data)
            Next

            Dim chartData = ChartHelper.RenderChart(forecastDataList(0))
            Dim fileName = Path.GetFileName(forecastCsvFile.FileName)

            Return Tuple.Create(forecastDataList, fileName, chartData)

        End Function

        Public Function GetLocationForecast(lat As Double, lon As Double, locationName As String) As LocationViewModel Implements IForecastService.GetLocationForecast

            Dim forecastDataFromDb = _forecastRepositoryService.FetchRecentForecastsData(lat, lon)

            If forecastDataFromDb IsNot Nothing AndAlso forecastDataFromDb.DailyForecasts IsNot Nothing AndAlso forecastDataFromDb.DailyForecasts.Any() Then
                Return forecastDataFromDb
            Else
                Dim data = _openMeteoClient.GetDailyForecastData(lat, lon)

                Dim forecastData As New LocationViewModel() With {
            .Latitude = lat.ToString(),
            .Longitude = lon.ToString(),
            .LocationName = locationName,
            .DailyForecasts = data
            }

                _forecastRepositoryService.SaveForecastsData(New List(Of LocationViewModel) From {forecastData})

                Return forecastData
            End If
        End Function
    End Class
End Namespace
