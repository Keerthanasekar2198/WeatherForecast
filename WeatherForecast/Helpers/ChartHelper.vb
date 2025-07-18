Public Class ChartHelper
    Public Shared Function RenderChart(LocationForecastData As LocationViewModel) As Tuple(Of String, List(Of Integer), List(Of String))
        Dim city = LocationForecastData.LocationName
        Dim temperatures As New List(Of Integer)
        Dim days As New List(Of String)

        For Each forecastData In LocationForecastData.DailyForecasts
            temperatures.Add(CInt(forecastData.TemperatureMax))
            days.Add(forecastData.DateValue.ToString("ddd"))
        Next

        Return Tuple.Create(city, temperatures, days)
    End Function
End Class
