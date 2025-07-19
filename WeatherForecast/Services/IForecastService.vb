Namespace WeatherForecast.Services
    Public Interface IForecastService
        Function ProcessForecastCSVFile(forecastCsvFile As HttpPostedFileBase) As Tuple(Of List(Of LocationViewModel), String, Tuple(Of String, List(Of Integer), List(Of Integer), List(Of String)))
        Function GetLocationForecast(lat As Double, lon As Double, locationName As String) As LocationViewModel
    End Interface
End Namespace
