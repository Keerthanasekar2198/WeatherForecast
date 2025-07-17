Public Interface IForecastService
    Function GetDailyForecast(lat As Double, lon As Double) As List(Of ForecastData)
End Interface
