@ModelType List(Of LocationViewModel)

@Code
    ViewData("Title") = "Home Page"
End Code

<main>
    <section class="row" aria-labelledby="aspnetTitle">
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
