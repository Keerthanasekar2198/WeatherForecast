@ModelType List(Of LocationViewModel)

@Code
    ViewData("Title") = "Forecast Results"
End Code

<main>
    <h2>Weather Forecast</h2>

    @If Model Is Nothing OrElse Not Model.Any() Then
        @<text>
            <div class="alert alert-warning">No forecast data available.</div>
        </text>
    Else
        For Each locationForecast In Model
            @<text>
                <div class="card mb-3">
                    <div class="card-header">
                        Forecast for @locationForecast.LocationName (@locationForecast.Latitude, @locationForecast.Longitude)
                    </div>
                    <div class="card-body">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Min Temp (°C)</th>
                                    <th>Max Temp (°C)</th>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each ForecastData In locationForecast.DailyForecasts
                                    @<text>
                                        <tr>
                                            <td>@ForecastData.DateValue.ToShortDateString()</td>
                                            <td>@ForecastData.TemperatureMin</td>
                                            <td>@ForecastData.TemperatureMax</td>
                                        </tr>
                                    </text>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>
            </text>
        Next
    End If
</main>
