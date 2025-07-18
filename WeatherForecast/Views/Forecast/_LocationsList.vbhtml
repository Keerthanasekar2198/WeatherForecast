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
    <button id="reuploadBtn" type="button" class="btn btn-secondary mt-3" onclick="ReuploadCSV()">Re-upload CSV</button>
    <i id="graphIcon" class="bi bi-graph-up" onclick="RenderChart()"></i>
    @If Model Is Nothing OrElse Not Model.Any() Then
        @<text>
            <div class="alert alert-warning">No forecast data available.</div>
        </text>
    Else
        @<text>

            <div class="card mt-4 w-100" style="height: 600px; overflow-y: auto;">
                <table class="table table-bordered top-margin">
                    <thead>
                        <tr>
                            <th> Location</th>
                            @For Each d In dateHeaders
                                @<th>@d.ToString("MMM dd")<br /><small>Min/Max(°C)</small></th>
                            Next
                        </tr>
                    </thead>
                    <tbody>
                        @For Each locationForecast In Model
                            @<text>
                                <tr>
                                    <td><b>@locationForecast.LocationName</b></td>
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
            </div>
        </text>
    End If
</main>