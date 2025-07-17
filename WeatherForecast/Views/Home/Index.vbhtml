@ModelType List(Of LocationViewModel)

@Code
    ViewData("Title") = "Home Page"
End Code

<main>
    <section class="row" aria-labelledby="aspnetTitle">
        <div id="uploadSection" style="display:@(If(ViewBag.UploadedFileName Is Nothing, "block", "none"));">
            @Html.Partial("~/Views/Forecast/_UploadForm.vbhtml")
        </div>


        @* Render partial view with the locations if Model exists *@
        @If Model IsNot Nothing Then
            @<text>
                <div id="forecastSection">
                    @Html.Partial("~/Views/Forecast/_LocationsList.vbhtml", Model)
                </div>
            </text>
        End If

    </section>
</main>
