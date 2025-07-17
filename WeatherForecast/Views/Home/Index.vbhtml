@ModelType List(Of LocationViewModel)

@Code
    ViewData("Title") = "Home Page"
End Code

<main>
    <section class="row" aria-labelledby="aspnetTitle">
        
        @Html.Partial("~/Views/Forecast/_UploadForm.vbhtml")

        @* Render partial view with the locations if Model exists *@
        @If Model IsNot Nothing Then
            @Html.Partial("~/Views/Forecast/_LocationsList.vbhtml", Model)
        End If

    </section>
</main>