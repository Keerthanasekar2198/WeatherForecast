Imports System.Data.SqlClient

Namespace WeatherForecast.Repositories
    Public Class ForecastRepository
        Implements IForecastRepository

        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("ForecastDb").ConnectionString
        Private ReadOnly _cacheHours As Integer = ConfigurationManager.AppSettings("ForecastCacheHours")

        Public Sub SaveForecastsData(locationForecasts As List(Of LocationViewModel)) Implements IForecastRepository.SaveForecastsData
            Using connection As New SqlConnection(_connectionString)
                connection.Open()

                For Each location In locationForecasts
                    For Each forecastData In location.DailyForecasts
                        Dim cmd As New SqlCommand("INSERT INTO ForecastsData (LocationName, Latitude, Longitude, ForecastDate, TemperatureMax, TemperatureMin, FetchedAt) 
                                               VALUES (@LocationName, @Latitude, @Longitude, @ForecastDate, @MaxTemp, @MinTemp, @FetchedAt)", connection)

                        cmd.Parameters.AddWithValue("@LocationName", location.LocationName)
                        cmd.Parameters.AddWithValue("@Latitude", location.Latitude)
                        cmd.Parameters.AddWithValue("@Longitude", location.Longitude)
                        cmd.Parameters.AddWithValue("@ForecastDate", forecastData.DateValue)
                        cmd.Parameters.AddWithValue("@MaxTemp", forecastData.TemperatureMax)
                        cmd.Parameters.AddWithValue("@MinTemp", forecastData.TemperatureMin)
                        cmd.Parameters.AddWithValue("@FetchedAt", DateTime.UtcNow)
                        cmd.ExecuteNonQuery()
                    Next
                Next
            End Using
        End Sub


        Public Function FetchRecentForecastsData(lat As Double, lon As Double) As LocationViewModel Implements IForecastRepository.FetchRecentForecastsData
            Dim recentForecasts As New List(Of ForecastData)
            Dim locationForecasts As New LocationViewModel()
            Dim locationName As String = String.Empty

            Using connection As New SqlConnection(_connectionString)
                connection.Open()

                Dim cmd As New SqlCommand("SELECT LocationName, Latitude, Longitude, ForecastDate, TemperatureMax, TemperatureMin FROM ForecastsData WHERE Latitude=@Latitude AND Longitude=@Longitude AND FetchedAt >= DATEADD(hour, -" & _cacheHours & ", GETDATE()) ORDER BY FetchedAt", connection)

                cmd.Parameters.AddWithValue("@Latitude", lat)
                cmd.Parameters.AddWithValue("@Longitude", lon)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        locationName = reader("LocationName").ToString()

                        recentForecasts.Add(New ForecastData With {
                        .DateValue = Convert.ToDateTime(reader("ForecastDate")),
                        .TemperatureMax = Convert.ToDouble(reader("TemperatureMax")),
                        .TemperatureMin = Convert.ToDouble(reader("TemperatureMin"))
                    })
                    End While
                End Using
            End Using

            If recentForecasts.Any() Then
                locationForecasts = New LocationViewModel With {
                .LocationName = locationName,
                .Latitude = lat.ToString(),
                .Longitude = lon.ToString(),
                .DailyForecasts = recentForecasts
             }

            End If

            Return locationForecasts
        End Function

    End Class

End Namespace
