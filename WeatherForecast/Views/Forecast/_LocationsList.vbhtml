@ModelType List(Of LocationViewModel)

@Code
    ViewBag.Title = "Parsed Locations"
End Code

@If Model Is Nothing OrElse Not Model.Any() Then
    @<text>
        <div class="alert alert-warning">No locations found in the uploaded file.</div>
    </text>
Else
    @<text>
        <table class="table table-striped table-bordered top-margin">
            <thead class="thead-dark">
                <tr>
                    <th>Latitude</th>
                    <th>Longitude</th>
                    <th>Area Name</th>
                </tr>
            </thead>
            <tbody>
                @For Each location In Model
                    @<text>
                        <tr>
                            <td>@location.Latitude</td>
                            <td>@location.Longitude</td>
                            <td>@location.LocationName</td>
                        </tr>
                    </text>
                Next
            </tbody>
        </table>
    </text>
End If
