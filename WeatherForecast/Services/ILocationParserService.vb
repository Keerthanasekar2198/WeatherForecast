Public Interface ILocationParserService
    Function ParseCsvFile(file As HttpPostedFileBase) As List(Of LocationViewModel)
End Interface
