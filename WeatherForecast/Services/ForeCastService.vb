Imports System.Net
Imports System.Web.Script.Serialization

Public Class ForeCastService
    Implements IForecastService

    Public Function GetDailyForecast(lat As Double, lon As Double) As List(Of ForecastData) Implements IForecastService.GetDailyForecast
        Dim baseUrl As String = ConfigurationManager.AppSettings("OpenMeteoApiBaseUrl")
        Dim url As String = $"{baseUrl}?latitude={lat}&longitude={lon}&daily=temperature_2m_max,temperature_2m_min&timezone=auto"

        Using client As New WebClient()
            Dim response = client.DownloadString(url)
            Dim serializer As New JavaScriptSerializer()
            Dim serializedData = serializer.Deserialize(Of Dictionary(Of String, Object))(response)

            Dim forecastList As New List(Of ForecastData)

            Console.WriteLine(serializedData)

            If serializedData.ContainsKey("daily") Then
                Dim daily = CType(serializedData("daily"), Dictionary(Of String, Object))
                Dim dates = CType(daily("time"), ArrayList)
                Dim maxTemperature = CType(daily("temperature_2m_max"), ArrayList)
                Dim minTemperature = CType(daily("temperature_2m_min"), ArrayList)

                For i As Integer = 0 To dates.Count - 1
                    forecastList.Add(New ForecastData With {
                        .DateValue = DateTime.Parse(dates(i).ToString()),
                        .TemperatureMax = Convert.ToDouble(maxTemperature(i)),
                        .TemperatureMin = Convert.ToDouble(minTemperature(i))
                    })
                Next

            End If

            Return forecastList
        End Using
    End Function
End Class
