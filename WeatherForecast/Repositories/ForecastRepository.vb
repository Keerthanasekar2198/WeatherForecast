Imports System.Data.SqlClient

Public Class ForecastRepository
    Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("ForecastDb").ConnectionString

    Public Sub SaveForecastsData(locationForecasts As List(Of LocationViewModel))
        Using connection As New SqlConnection(_connectionString)
            connection.Open()

            For Each location In locationForecasts
                For Each forecastData In location.DailyForecasts
                    Dim cmd As New SqlCommand("INSERT INTO ForecastsData (LocationName, Latitude, Longitude, ForecastDate, TemperatureMax, TemperatureMin) 
                                               VALUES (@LocationName, @Latitude, @Longitude, @ForecastDate, @MaxTemp, @MinTemp)", connection)

                    cmd.Parameters.AddWithValue("@LocationName", location.LocationName)
                    cmd.Parameters.AddWithValue("@Latitude", location.Latitude)
                    cmd.Parameters.AddWithValue("@Longitude", location.Longitude)
                    cmd.Parameters.AddWithValue("@ForecastDate", forecastData.DateValue)
                    cmd.Parameters.AddWithValue("@MaxTemp", forecastData.TemperatureMax)
                    cmd.Parameters.AddWithValue("@MinTemp", forecastData.TemperatureMin)

                    cmd.ExecuteNonQuery()
                Next
            Next
        End Using
    End Sub
End Class
