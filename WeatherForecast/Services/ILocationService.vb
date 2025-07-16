Public Interface ILocationService
    Function ParseCsvFile(file As HttpPostedFileBase) As List(Of LocationViewModel)
End Interface
