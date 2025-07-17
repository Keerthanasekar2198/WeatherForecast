Imports System.Net
Imports System.Web.Script.Serialization

Public Class ForeCastService
    Implements IForecastService

    Private ReadOnly _baseUrl As String

    Public Sub New(Optional baseUrl As String = Nothing)
        If String.IsNullOrEmpty(baseUrl) Then
            _baseUrl = ConfigurationManager.AppSettings("OpenMeteoApiBaseUrl")
        Else
            _baseUrl = baseUrl
        End If
    End Sub

    Public Function GetLocationForecast(lat As Double, lon As Double, locationName As String) As LocationViewModel Implements IForecastService.GetLocationForecast
        Dim forecasts = GetDailyForecast(lat, lon)

        Dim locationViewModel As New LocationViewModel() With {
        .Latitude = lat.ToString(),
        .Longitude = lon.ToString(),
        .LocationName = locationName,
        .DailyForecasts = forecasts
    }

        Dim forecastRepositoryService As New ForecastRepository()


        forecastRepositoryService.SaveForecastsData(New List(Of LocationViewModel) From {locationViewModel})

        Return locationViewModel
    End Function

    Public Function GetDailyForecast(lat As Double, lon As Double) As List(Of ForecastData) Implements IForecastService.GetDailyForecast
        Dim url As String = $"{_baseUrl}?latitude={lat}&longitude={lon}&daily=temperature_2m_max,temperature_2m_min&timezone=auto"

        Try
            Using client As New WebClient()
                Dim response = client.DownloadString(url)
                Dim serializer As New JavaScriptSerializer()
                Dim serializedData = serializer.Deserialize(Of Dictionary(Of String, Object))(response)

                Dim forecastList As New List(Of ForecastData)

                If serializedData.ContainsKey("daily") Then
                    Dim daily = CType(serializedData("daily"), Dictionary(Of String, Object))
                    Dim dates = CType(daily("time"), ArrayList)
                    Dim maxTemperature = CType(daily("temperature_2m_max"), ArrayList)
                    Dim minTemperature = CType(daily("temperature_2m_min"), ArrayList)

                    For i As Integer = 0 To dates.Count - 1
                        forecastList.Add(New ForecastData With {
                            .DateValue = DateTime.Parse(dates(i).ToString()),
                            .TemperatureMax = Convert.ToDouble(maxTemperature(i)),
                            .TemperatureMin = Convert.ToDouble(minTemperature(i))
                        })
                    Next

                End If

                Return forecastList
            End Using
        Catch ex As WebException
            Throw
        Catch ex As Exception
            Throw
        End Try

    End Function
End Class
