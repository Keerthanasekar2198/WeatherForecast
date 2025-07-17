Imports System.Net
Imports WeatherForecast

<TestClass>
Public Class ForeCastServiceTests
    <TestMethod>
    Public Sub GetForeCastdata_FromExternalApi_Successfully()
        Dim testBaseUrl As String = "https://api.open-meteo.com/v1/forecast"
        Dim latitude As Double = 51.507351
        Dim longitude As Double = -0.127758

        Dim forecastService As New ForeCastService(testBaseUrl)

        Dim forecastData As List(Of ForecastData) = forecastService.GetDailyForecast(latitude, longitude)

        Assert.IsNotNull(forecastData)
        Assert.IsTrue(forecastData.Count > 0, "No forecast data returned.")
    End Sub

    <TestMethod>
    Public Sub GetForeCastdata_FromExternalApi_FailsWithInvalidInput()
        Dim testBaseUrl As String = "https://api.open-meteo.com/v1/forecast"
        Dim latitude As Double = 9999
        Dim longitude As Double = 9999

        Dim forecastService As New ForeCastService(testBaseUrl)

        Try
            Dim forecastData As List(Of ForecastData) = forecastService.GetDailyForecast(latitude, longitude)
            Assert.Fail("Expected WebException was not thrown.")
        Catch ex As WebException
            Assert.IsTrue(ex.Message.Contains("The remote server returned an error: (400) Bad Request."))
        End Try
    End Sub
End Class
