@ModelType List(Of LocationViewModel)

@Code
    ViewData("Title") = "Home Page"
End Code

<main>
    <section class="row" aria-labelledby="aspnetTitle">
        <h1 id="title">GeoForecast</h1>
        <p class="lead">Please upload a CSV file containing the latitude, longitude, and area names of the locations you’d like to receive weather forecasts.</p>

        @* This is to allow users to Upload CSV files *@
        <form id="uploadForm" action="@Url.Action("UploadCsv", "Forecast")" method="post" enctype="multipart/form-data">
            <div class="upload-container">
                <input type="file" id="forecastCsvInput" name="forecastCsvFile" accept=".csv" />
                <label for="forecastCsvInput" class="upload-label">Upload CSV</label>

                <div id="fileBox" class="file-box">
                    <span id="fileName" class="file-name">
                        @If ViewBag.UploadedFileName IsNot Nothing Then
                            @<text>@ViewBag.UploadedFileName</text>
                        Else
                            @<text>No file chosen</text>
                        End If
                    </span>
                    <span id="clearFile" class="clear-btn">&times;</span>
                </div>

            </div>
        </form>

        @* Render partial view with the locations if Model exists *@
        @If Model IsNot Nothing Then
            Html.RenderPartial("~/Views/Forecast/_LocationsList.vbhtml", Model)
        End If

    </section>
</main>
