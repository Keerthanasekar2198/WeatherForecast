@ModelType Tuple(Of String, List(Of Integer), List(Of String))

<div class="d-flex align-items-center justify-content-between mt-3">

    <button id="reuploadBtn" type="button" class="btn btn-secondary mt-3" onclick="ReuploadCSV()">Re-upload CSV</button>
    <div class="d-flex align-items-center gap-3">
        @Using Html.BeginForm("GetForecastForCity", "Forecast", FormMethod.Get, New With {.id = "cityForm"})
            @<text> <select id="SelectedCity" name="SelectedCity" Class="form-select mt-3" onchange="OnLocationChange(this)">
                    <option value="">Select a City</option>
                    @For Each location In CType(ViewBag.LocationsList, List(Of LocationViewModel))
                        @<text>
                            <option value="@location.LocationName" data-lat="@location.Latitude" data-lon="@location.Longitude">@location.LocationName</option>
                        </text>
                    Next
                </select>


                <input type="hidden" id="Latitude" name="Latitude" />
                <input type="hidden" id="Longitude" name="Longitude" />  </text>
        End Using
        <i id="tableIcon" class="bi bi-table fs-3" onclick="ShowTable()"></i>
    </div>
</div>

<div class="container text-center mt-4">
    <h4>Temperature Forecast - @Model.Item1</h4>
    <canvas id="forecastChart" width="600" height="400"></canvas>
</div>

<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script type="text/javascript">
    const ctx = document.getElementById('forecastChart').getContext('2d');
    const forecastChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Item3)),
            datasets: [{
                label: 'Temperature (°C)',
                data: @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Item2)),
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                tension: 0.3,
                fill: true,
                pointRadius: 5,
                pointHoverRadius: 7
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top'
                }
            },
            scales: {
                y: {
                    beginAtZero: false
                }
            }
        }
    });
</script>
