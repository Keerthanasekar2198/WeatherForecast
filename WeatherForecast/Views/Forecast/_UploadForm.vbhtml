<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">


<div class="card mt-4">
    <h1 id="title" class="text-center mt-5">
        GeoForecast<img src="https://flagcdn.com/w40/gb.png" alt="UK Flag" class="ms-1" style="vertical-align: super; font-size: 2.75em;">
    </h1>
    <p class="text-center mt-3">Upload your CSV and forecast weather.</p>

    <form id="uploadForm" action="@Url.Action("UploadCsv", "Forecast")" method="post" enctype="multipart/form-data">
            <div class="drop-zone text-center d-flex flex-column justify-content-center align-items-center border p-4 mt-3"
                 style="min-height: 140px; cursor: pointer; width: 100%; max-width: 60%; margin: 10% auto; border-color: #579fe8 !important;"
                 onclick="document.getElementById('forecastCsvInput').click();">

                <i id="fileUploadTrigger" class="bi bi-upload fs-2 mt-5" style="cursor: pointer;"></i>
                <div class="drop-zone-text h5 mt-5">Drag & Drop Your CSV File Here</div>
                <div class="drop-zone-link text-primary small mt-5">or click to select a file</div>


                @Html.TextBox("forecastCsvFile", "", New With {
                                         .type = "file",
                                         .id = "forecastCsvInput",
                                         .class = "d-none"
                                     })

                <div id="fileBox" class="file-box mt-5">
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

</div>