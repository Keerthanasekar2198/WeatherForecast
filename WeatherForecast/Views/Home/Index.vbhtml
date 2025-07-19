@ModelType List(Of LocationViewModel)

@Code
    ViewData("Title") = "Home Page"
End Code

<main>
    <section class="row" aria-labelledby="aspnetTitle">
        @If Not ViewData.ModelState.IsValid Then
            @<div class="d-flex justify-content-center mt-4">
                <div class="card border-danger shadow-sm" style="max-width: 600px; width: 100%;">
                    <div class="card-header bg-danger text-white d-flex align-items-center">
                        <i class="bi bi-exclamation-triangle-fill me-2"></i>
                        <span>Error</span>
                    </div>
                    <div class="card-body text-danger">
                        @For Each e In ViewData.ModelState.Values.SelectMany(Function(v) v.Errors)
                            @<text><div> <i class="bi"></i> @e.ErrorMessage  Please re-upload the CSV with proper values!</div></text>
                        Next
                    </div>
                </div>
            </div>
        End If


        <div id="uploadSection" style="display:@(If(ViewBag.UploadedFileName Is Nothing AndAlso (ViewBag.ShowChart Is Nothing OrElse Not ViewBag.ShowChart), "block", "none"));">
            @Html.Partial("~/Views/Forecast/_UploadForm.vbhtml")
        </div>


        @* Render partial view with the locations if Model exists *@
        @If Model IsNot Nothing Then
        @<text>
            <div id="forecastSection" style="@(If(ViewBag.ShowChart, "display:none", "display:block"))">
                @Html.Partial("~/Views/Forecast/_LocationsList.vbhtml", Model)
            </div>
        </text>
                End If

        @If Model IsNot Nothing Then
    @<text>
        <div id="chartSection" style="@(If(ViewBag.ShowChart, "display:block", "display:none"))">
            @Html.Partial("~/Views/Forecast/_ForecastChart.vbhtml", ViewBag.ForecastData)
        </div>
    </text>
            End If

    </section>
</main>
