Namespace WeatherForecast.Repositories

    Public Interface IForecastRepository
        Sub SaveForecastsData(locationForecasts As List(Of LocationViewModel))
        Function FetchRecentForecastsData(lat As Double, lon As Double) As LocationViewModel
    End Interface

End Namespace
