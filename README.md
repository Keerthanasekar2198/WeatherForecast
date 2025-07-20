# WeatherForecast

Weather Forecast Application using VB.NET MVC

Approach:

1. User is shown with the interface to either upload/drag & drop CSV file and the CSV contains list of latitude, longitude and location name
2. The CSV file is passed to the Forecast controller to parse the CSV to get the values of latitude, longitude, locationname
3. The CSV is also validated against various different cases example: passing empty values to latitude, locationname, etc so it throws CSVParsing Exception
4. If the parsing is successful the latitude and longitude values is passed to the external API Open-Meteo to get the Weather Forecast data for 7 days
5. The data is now passed to the view(table view that displays list of locations and their maximum and minimum temperatures for next 7 days)
6. User is also provided a option to switch to chart view (visual representation of weather data for particular location)
7. Once chart view is switched there will a dropdown preloaded with list of locations that's been passed in the CSV
8. By default chart view for first location in the CSV will be displayed (Line chart displaying min and max temperatures for first location - 7 days)
9. User can switch the dropdown to different location so that chart data will be loaded for the selected location
10. User can re-upload CSV from the same page where they are currently and reuploading CSV will be by default load the table view

Points to be noted:
1. Used local SQL Server DB to store the recent forecasts data
2. The application uses local database storage as a cache with a 6-hour expiration. When a user requests forecast data, the system checks if the most recent data for that location was fetched within the last 6 hours. If so, it retrieves the data from the database instead of making an external API call, thereby reducing latency and improving performance. (The configuration 6 hrs can be updated easily as it's included in the config)
3. Implemented modular approach and tried to reused most functionalities(Example: Resued GetLocationForecast service for rendering both chart and table view)
4. Implemented Object Oriented Programming concepts Encapsulation, Inheritance, Abstraction, Polymorphism
5. Implemented unit tests MS-Tests for both LocationCSVParser (covers all happy and negative cases for CSV Validation), ForecastService(covers happy and negative cases for returning forecast data)
6. Created responsive UI and implemented bootstrap styling as well used javascript libraries like chart.js to render Line chart for rendering forecast data
7. Improved code readability by organizing files into separate folders for Controllers, Services, Exceptions, Models, and Helpers, enhancing maintainability


Project Structure:

WeatherForecasts

/Controllers          →  Handles HTTP requests (ForecastController)

/Models               →  Classes for data ( LocationViewModel, ForecastData)

/Views                →  UI templates (Index.vbhtml, PartialViews to upload, render chart/table views)

/Services             →  Business logic (LocationCSVParserService, ForeCastService)

/Service/ExternalApi  → External Api call(OpenMeteoApiClient)

/Helpers              →  Utility classes (CSVValidator, ChartHelper)

/Repository           →  Handles all database-related operations(ForecastRepository)

/Exceptions           →  Handles all custom exceptions(CsvParsingException)

/Web.config           →  Configuration settings (API keys, DB connection)

WeatherForecastsTests (For unit tests)

Flow Diagram: https://github.com/Keerthanasekar2198/WeatherForecast/blob/master/Forecast-Application-Flow.png

Documentation Link: https://github.com/Keerthanasekar2198/WeatherForecast/blob/master/Documentation-Weather%20Forecast%20Application.pdf
