@ModelType List(Of LocationViewModel)

@Code
    ViewData("Title") = "Forecast Results"
    Dim dateHeaders As New List(Of Date)
    If Model IsNot Nothing AndAlso Model.Any() AndAlso Model(0).DailyForecasts IsNot Nothing Then
        For Each forecast In Model(0).DailyForecasts
            dateHeaders.Add(forecast.DateValue)
        Next
    End If
End Code

<main>
    @If Model Is Nothing OrElse Not Model.Any() Then
        @<text>
            <div class="alert alert-warning">No forecast data available.</div>
        </text>
    Else
        @<text>
            <table class="table table-bordered top-margin">
                <thead>
                    <tr>
                        <th> Location(Lat, Long)</th>
                        @For Each d In dateHeaders
                            @<th>@d.ToString("MMM dd")<br /><small>Min/Max(°C)</small></th>
                        Next
                    </tr>
                </thead>
                <tbody>
                    @For Each locationForecast In Model
                        @<text>
                            <tr>
                                <td><b>@locationForecast.LocationName (@locationForecast.Latitude, @locationForecast.Longitude)</b></td>
                                @For Each forecast In locationForecast.DailyForecasts
                                    @<td>
                                        @forecast.TemperatureMin/@forecast.TemperatureMax
                                    </td>
                                Next
                            </tr>
                        </text>
                    Next
                </tbody>
            </table>
        </text>
    End If
</main>