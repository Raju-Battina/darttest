# Weather Data Application

## Overview

The Weather Data Application is a three-component solution designed to retrieve weather information from a public weather API, store the retrieved data as JSON files, expose the stored weather data through an internal REST API, and display the weather information through an ASP.NET Core MVC web application.

The solution consists of the following applications:

* **WeatherImportApp** – .NET 10 Console/Batch application responsible for retrieving and storing weather data.
* **WeatherAPI** – ASP.NET Core 10 Web API responsible for reading and exposing the stored weather data.
* **WeatherApp** – ASP.NET Core 10 MVC web application responsible for consuming the Weather API and displaying the weather information through Razor views.

---

# Scope

The solution covers the following requirements:

1. Capture and store weather data using a public weather API.
2. Provide an internal API to expose the stored weather data.
3. Provide a web application to display the weather data.

---

# Solution Architecture

```text
                    Public Weather API
                           |
                           v
                 +---------------------+
                 |   WeatherImportApp  |
                 |       .NET 10       |
                 |    Console/Batch    |
                 +----------+----------+
                            |
                            | Store JSON
                            v
                     +-------------+
                     |   JSONData  |
                     |    Folder   |
                     +------+------+
                            |
                            | Read JSON
                            v
                 +---------------------+
                 |     WeatherAPI      |
                 |       .NET 10       |
                 |      Web API        |
                 +----------+----------+
                            |
                            | REST API
                            v
                 +---------------------+
                 |      WeatherApp     |
                 |       .NET 10       |
                 |    ASP.NET Core MVC |
                 |    Razor Views      |
                 +----------+----------+
                            |
                            v
                       Web Browser
```

---

# Projects

## 1. WeatherImportApp

### Purpose

`WeatherImportApp` is a .NET 10 Console/Batch application responsible for retrieving weather information from a public weather service and storing the response as JSON files.

### Responsibilities

The application:

1. Calls the public weather API.
2. Passes the required parameters:

   * Date
   * Place
3. Reads the list of dates from a `dates.txt` file.
4. Creates a `JSONData` folder if it does not already exist.
5. Stores the weather API responses as JSON files in the `JSONData` folder.

### Technology

* .NET 10
* C#
* HTTP Client
* JSON serialization/deserialization
* Console/Batch application

### Input

The application reads dates from:

```text
dates.txt
```

Example:

```text
2026-09-20
2026-09-21
2026-09-22
2026-09-23
```

The application uses each date together with the configured place to request weather information from the public weather API.

### JSON Data Storage

The application creates the following folder when required:

```text
JSONData
```

Example:

```text
WeatherImportApp
│
├── dates.txt
├── JSONData
│   ├── 2026-09-20.json
│   ├── 2026-09-21.json
│   ├── 2026-09-22.json
│   └── 2026-09-23.json
└── ...
```

---

# 2. WeatherAPI

## Purpose

`WeatherAPI` is an ASP.NET Core 10 Web API that reads the weather information stored by `WeatherImportApp` and exposes it through REST endpoints.

### Responsibilities

The API:

1. Reads weather data from JSON files.
2. Deserializes JSON data into application models.
3. Exposes weather information through REST endpoints.
4. Supports API versioning.
5. Supports CORS for approved web application origins.

### Technology

* .NET 10
* ASP.NET Core Web API
* C#
* REST API
* JSON
* API Versioning
* CORS

---

## API Versioning

The API uses URL-based versioning.

Example:

```text
GET /api/v1/weather
```

Future versions can be introduced without breaking existing consumers:

```text
GET /api/v2/weather
```

### Example Request

```http
GET https://localhost:7001/api/v1/weather
```

### Example Response

```json
[
  {
    "date": "2026-09-23",
    "minTemp": 72,
    "maxTemp": 91,
    "precipitation": 10
  }
]
```

> The actual response structure depends on the implementation of the `WeatherData` model.

---

# 3. WeatherApp

## Purpose

`WeatherApp` is an ASP.NET Core 10 MVC web application that consumes the internal `WeatherAPI` and displays the weather information through Razor views.

### Responsibilities

The application:

1. Calls the internal Weather API.
2. Retrieves weather data.
3. Displays weather information in a table.
4. Provides sorting functionality.
5. Provides filtering functionality for each column.
6. Presents the weather data through Razor/MVC views.

### Technology

* .NET 10
* ASP.NET Core MVC
* Razor Views
* C#
* HTML
* CSS
* JavaScript/jQuery
* REST API

---

# Weather Data Table

The web application displays the following columns:

| Column        | Description               |
| ------------- | ------------------------- |
| Date          | Weather forecast date     |
| Min Temp      | Minimum temperature       |
| Max Temp      | Maximum temperature       |
| Precipitation | Precipitation information |

Example:

| Date       | Min Temp | Max Temp | Precipitation |
| ---------- | -------: | -------: | ------------: |
| 2026-09-20 |     70°F |     89°F |           10% |
| 2026-09-21 |     71°F |     90°F |           15% |
| 2026-09-22 |     73°F |     92°F |            5% |
| 2026-09-23 |     72°F |     91°F |           10% |

### Sorting

Users can sort the weather data by clicking the table column headers.

Supported sorting includes:

* Date
* Minimum Temperature
* Maximum Temperature
* Precipitation

### Filtering

Users can filter the weather data using filters provided for each column.

---

# WeatherApp Project Structure

```text
WeatherApp
│
├── Controllers
│   └── WeatherController.cs
│
├── Models
│   └── WeatherData.cs
│
├── Views
│   ├── Weather
│   │   └── Index.cshtml
│   └── Shared
│       └── _Layout.cshtml
│
├── wwwroot
│   ├── css
│   ├── js
│   └── lib
│
├── appsettings.json
├── Program.cs
└── WeatherApp.csproj
```

---

# WeatherApp API Communication

The MVC application communicates with `WeatherAPI` using HTTP.

The general flow is:

```text
Browser
   |
   v
WeatherApp
ASP.NET Core 10 MVC
   |
   | HTTP Request
   v
WeatherAPI
ASP.NET Core 10 Web API
   |
   | Read JSON
   v
JSONData
```

The MVC controller/service calls the API and passes the returned weather data to the Razor view.

Example endpoint:

```text
GET /api/v1/weather
```

The Razor view then displays the returned data in the weather table.

---

# Prerequisites

The following software is required to build and run the solution:

* .NET 10 SDK
* Visual Studio 2022/2026 or Visual Studio Code
* Git
* Modern web browser

Verify the .NET installation:

```bash
dotnet --version
```

The installed SDK should support .NET 10.

---

# Configuration

## WeatherImportApp

Configure the public weather API details and place according to the application's configuration.

Example:

```json
{
  "WeatherApi": {
    "BaseUrl": "https://example-weather-api.com",
    "Place": "Austin"
  }
}
```

> Do not commit API keys, secrets, passwords, or other sensitive credentials to source control. Use environment variables, user secrets, Azure Key Vault, or another secure configuration mechanism as appropriate.

---

## WeatherAPI

Configure the location of the JSON data folder.

Example:

```json
{
  "WeatherData": {
    "JsonDataPath": "JSONData"
  }
}
```

The API reads the generated JSON files from the configured location.

---

## WeatherApp

Configure the Weather API base URL.

Example:

```text
https://localhost:7001/api/v1/weather
```

For production, configure the URL for the deployed WeatherAPI endpoint.

---

# How to Run

## Step 1 – Run WeatherImportApp

Navigate to the project directory:

```bash
cd WeatherImportApp
```

Run the application:

```bash
dotnet run
```

The application will:

1. Read dates from `dates.txt`.
2. Call the public weather API for each date and configured place.
3. Create the `JSONData` folder if necessary.
4. Save the returned weather data as JSON files.

---

## Step 2 – Run WeatherAPI

Navigate to the API project:

```bash
cd WeatherAPI
```

Run the API:

```bash
dotnet run
```

Test the API using a browser, Swagger, Postman, or another API client.

Example:

```text
GET /api/v1/weather
```

---

## Step 3 – Run WeatherApp

Navigate to the MVC application:

```bash
cd WeatherApp
```

Run the application:

```bash
dotnet run
```

Open the URL displayed by the ASP.NET Core application in a web browser.

---

# End-to-End Process

```text
1. dates.txt
      |
      v
2. WeatherImportApp
      |
      v
3. Public Weather API
      |
      v
4. Weather JSON Response
      |
      v
5. JSONData Folder
      |
      v
6. WeatherAPI
      |
      v
7. REST Endpoint
   /api/v1/weather
      |
      v
8. WeatherApp
   ASP.NET Core MVC
      |
      v
9. Razor View
      |
      v
10. Weather Data Table
      |
      v
11. Sort / Filter
```

---

# Error Handling

The applications should handle common failure scenarios, including:

* Public weather API unavailable
* Invalid date
* Invalid place
* HTTP/API errors
* Invalid JSON response
* Missing `dates.txt`
* Missing `JSONData` directory
* Invalid or corrupted JSON files
* WeatherAPI unavailable to WeatherApp

Appropriate logging and error messages should be used to simplify troubleshooting.

---

# Security Considerations(! Partly implemented) 
The solution follows these general security practices:

* HTTPS should be used for API communication.
* CORS should be restricted to trusted application origins.
* API keys and secrets should not be stored in source control.
* Configuration should be environment-specific.
* Input values should be validated.
* External API failures should be handled gracefully.
* Production secrets should be stored using a secure secret-management mechanism.
* Added health-check endpoints for monitoring.
---



# Technologies Used

| Component            | Technology               |
| -------------------- | ------------------------ |
| Weather Import       | .NET 10 Console/Batch    |
| Weather API          | ASP.NET Core 10 Web API  |
| Web Application      | ASP.NET Core 10 MVC      |
| UI                   | Razor Views              |
| Programming Language | C#                       |
| Client-side          | JavaScript / jQuery      |
| Data Format          | JSON                     |
| API Communication    | REST / HTTP              |
| API Versioning       | URL-based API versioning |
| Cross-Origin Access  | CORS                     |
| Source Control       | Git                      |

---

# Summary

The Weather Data Application provides an end-to-end solution for collecting, storing, exposing, and displaying weather information.

The architecture separates responsibilities into three independent applications:

* **WeatherImportApp** handles weather data collection and JSON storage.
* **WeatherAPI** provides controlled access to the stored weather data.
* **WeatherApp** provides the web interface for viewing, sorting, and filtering weather information.

The solution is implemented using **.NET 10, ASP.NET Core Web API, ASP.NET Core MVC/Razor Views, C#, REST, JSON, and CORS**.
