Namespace WeatherForecast.Services.ExternalApi
    Public Interface IOpenMeteoClient
        Function GetDailyForecastData(lat As Double, lon As Double) As List(Of ForecastData)
    End Interface
End Namespace

