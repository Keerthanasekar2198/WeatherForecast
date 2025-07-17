Public Interface IForecastService
    Function GetLocationForecast(lat As Double, lon As Double, locationName As String) As LocationViewModel
    Function GetDailyForecast(lat As Double, lon As Double) As List(Of ForecastData)
End Interface
