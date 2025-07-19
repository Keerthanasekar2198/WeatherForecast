Imports System.Net
Imports WeatherForecast
Imports WeatherForecast.WeatherForecast.Services.ExternalApi

<TestClass>
Public Class OpenMeteoClientTests
    <TestMethod>
    Public Sub GetForeCastdata_FromExternalApi_Successfully()
        Dim testBaseUrl As String = "https://api.open-meteo.com/v1/forecast"
        Dim latitude As Double = 51.507351
        Dim longitude As Double = -0.127758

        Dim openMeteoClient As New OpenMeteoClient(testBaseUrl)

        Dim forecastData As List(Of ForecastData) = openMeteoClient.GetDailyForecastData(latitude, longitude)

        Assert.IsNotNull(forecastData)
        Assert.IsTrue(forecastData.Count > 0, "No forecast data returned.")
    End Sub

    <TestMethod>
    Public Sub GetForeCastdata_FromExternalApi_FailsWithInvalidInput()
        Dim testBaseUrl As String = "https://api.open-meteo.com/v1/forecast"
        Dim latitude As Double = 9999
        Dim longitude As Double = 9999

        Dim openMeteoClient As New OpenMeteoClient(testBaseUrl)

        Try
            Dim forecastData As List(Of ForecastData) = openMeteoClient.GetDailyForecastData(latitude, longitude)
            Assert.Fail("Expected WebException was not thrown.")
        Catch ex As WebException
            Assert.IsTrue(ex.Message.Contains("The remote server returned an error: (400) Bad Request."))
        End Try
    End Sub
End Class
