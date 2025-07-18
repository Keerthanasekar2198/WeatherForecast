Public Class ChartHelper
    Public Shared Function RenderChart(LocationForecastData As LocationViewModel) As Tuple(Of String, List(Of Integer), List(Of Integer), List(Of String))
        Dim city = LocationForecastData.LocationName
        Dim minTemperatures As New List(Of Integer)
        Dim maxTemperatures As New List(Of Integer)
        Dim days As New List(Of String)

        For Each forecastData In LocationForecastData.DailyForecasts
            minTemperatures.Add(CInt(forecastData.TemperatureMin))
            maxTemperatures.Add(CInt(forecastData.TemperatureMax))
            days.Add(forecastData.DateValue.ToString("ddd"))
        Next

        Return Tuple.Create(city, minTemperatures, maxTemperatures, days)
    End Function
End Class
