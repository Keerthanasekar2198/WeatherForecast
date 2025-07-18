@ModelType Tuple(Of String, List(Of Integer), List(Of Integer), List(Of String))

<div class="d-flex align-items-center justify-content-between mt-3">

    <button id="reuploadBtn" type="button" class="btn btn-secondary mt-3" onclick="ReuploadCSV()">Re-upload CSV</button>
    <div class="d-flex align-items-center gap-3">
        @Using Html.BeginForm("GetForecastForCity", "Forecast", FormMethod.Get, New With {.id = "cityForm"})
            @<text> <select id="SelectedCity" name="SelectedCity" Class="form-select mt-3" onchange="OnLocationChange(this)">
                     <option value="@Model.Item1" selected>@Model.Item1</option>
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
    <div class="card mt-4 w-100" style="height: 700px; overflow-y: auto;">
        <div style="display: flex; justify-content: center; align-items: center; height: 90%;">
            <canvas id="forecastChart"></canvas>
        </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script type="text/javascript">
    const ctx = document.getElementById('forecastChart').getContext('2d');
    const forecastChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Item4)),
            datasets: [{
                    label: 'Max Temperature (°C)',
                    data: @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Item3)),
                    borderColor: 'rgba(255, 99, 132, 1)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    tension: 0.3,
                    fill: false,
                    pointRadius: 5,
                    pointHoverRadius: 7
                },
                {
                    label: 'Min Temperature (°C)',
                    data: @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Item2)),
                    borderColor: 'rgba(54, 162, 235, 1)',
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    tension: 0.3,
                    fill: false,
                    pointRadius: 5,
                    pointHoverRadius: 7
                }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                     labels: {
                        usePointStyle: true,
                        pointStyle: 'line',
                        boxWidth: 45
                    }
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
