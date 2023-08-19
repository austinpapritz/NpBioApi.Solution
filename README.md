# US National Park Biodiversity API

#### By _Austin Papritz_

## Technologies Used

* _C#_
* _ASP.NET Core_
* _MySQL_
* _Visual Studio Code_
* _Entity Framework Core_
* _CvsHelper library_

Welcome to the US National Park Biodiversity API! This API provides access to detailed information about US National Parks and the various species found within them. Parks and species dataset were downloaded from [kaggle.com](https://www.kaggle.com/datasets/nationalparkservice/park-biodiversity).

## Table of Contents

- [Getting Started](#getting-started)
  - [Project Setup](#project-setup)
  - [Database Setup](#database-setup)
  - [Making API Calls](#making-api-calls)
- [Endpoints](#endpoints)
  - [Parks](#parks)
  - [Species](#species)
- [Data Models](#data-models)
  - [Park Model](#park-model)
  - [Species Model](#species-model)
  - [Paginated Species List Model](#paginated-species-list-model)
- [Support and Feedback](#support-and-feedback)
- [License](#license)

## Getting Started

### Project Setup

* _Navigate to where you want to save the repo using your favorite terminal app (e.g., GitBash)._
* _Enter in terminal:_ 

    ```$ git clone https://github.com/austinpapritz/NpBioApi.Solution.git```
* _Navigate into the repo folder and then open it in your favorite IDE (e.g., VS Code, Xcode, Atom)._

### Database Setup

* _Search online to install MySQL on your computer. Remember your username and password._
* _Add `appsettings.Development.json` file to project folder. Paste the following code, inserting your own information where {indicated}._

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database={DATABASE-NAME};uid={USERNAME};pwd={PASSWORD};"
  }
}
```
* _Build the project by entering `$ dotnet build`._
* _Seed the database by entering `$ dotnet run`._
* _This will likely take several minutes._

### Making API Calls

* _Search online to install Postman on your computer._
* _In Postman, navigate to `Workspaces` > `My WorkSpaces`_
* _Start every call with `http://localhost:5000`, see specific endpoints below._
* _Make sure you have entered `$ dotnet run` in terminal before making calls in Postman._

## Endpoints

### Parks

#### **GET** `/api/Parks`

- Fetches a list of all national parks.

#### **GET** `/api/Parks/{id}`

- Fetches details of a specific park using its ID.
  
#### **GET** `/api/Parks/{id}/Species`

- Fetches a list of species found in a specific park using the park's ID.

#### **POST** `/api/Parks`

- Creates a new park entry.

#### **PUT** `/api/Parks/{id}`

- Updates details of a specific park using its ID.

#### **DELETE** `/api/Parks/{id}`

- Deletes a specific park using its ID.

### Species

#### **GET** `/api/Species`

- Fetches a paginated list of species.
- Parameters:
  - `page`: The page number (default is 1).
  - `pageSize`: Number of species per page (default is 100).
- Includes pagination metadata (see [Paginated Species List Model](#paginated-species-list-model)).

#### **GET** `/api/Species/{id}`

- Fetches details of a specific species using its ID.

#### **GET** `/api/Species/CommonName/{commonName}/Parks`

- Fetches parks where a specific species (by common name) is found.

#### **GET** `/api/Species/ScientificName/{scientificName}/Parks`

- Fetches parks where a specific species (by scientific name) is found.

#### **GET** `/api/Species/Search`

- Searches for species by their common name or scientific name.
- Not case sensitive, matches partial strings.
- Parameters:
  - `commonName`: The common name of the species.
  - `scientificName`: The scientific name of the species.
- Example: `/api/Species/Search?commonName=Moose` returns mammals called "Moose" and plants like "Moosewood" and "Moosebush".

#### **POST** `/api/Species`

- Creates a new species entry.

#### **PUT** `/api/Species/{id}`

- Updates details of a specific species using its ID.

#### **DELETE** `/api/Species/{id}`

- Deletes a specific species using its ID.

## Data Models

### Park Model

```json
{
  "id": int,
  "parkCode": "string",
  "parkName": "string",
  "state": "string",
  "acres": int,
  "latitude": double,
  "longitude": double
}
```

### Species Model

```json
{
  "id": integer,
  "speciesId": "string",
  "parkName": "string",
  "category": "string",
  "order": "string",
  "family": "string",
  "scientificName": "string",
  "commonNames": "string",
  "recordStatus": "string",
  "occurrence": "string",
  "nativeness": "string",
  "abundance": "string",
  "seasonality": "string",
  "conservationStatus": "string",
  "parkId": integer
}
```

### Paginated Species List Model

```json
{
    "metadata": {
        "totalCount": integer,
        "pageSize": integer,
        "currentPage": integer,
        "totalPages": integer,
        "hasPrevious": bool,
        "hasNext": bool
    },
    "data": [
        {
          "id": integer,
          "speciesId": "string",
          "parkName": "string",
          "category": "string",
          "order": "string",
          "family": "string",
          "scientificName": "string",
          "commonNames": "string",
          "recordStatus": "string",
          "occurrence": "string",
          "nativeness": "string",
          "abundance": "string",
          "seasonality": "string",
          "conservationStatus": "string",
          "parkId": integer
        },
        // remaining list of species
    ]
}
```

## Support and Feedback

For any support or feedback, please contact austin@papritz.dev or raise an issue on the repository.

## License

This API is not licensed and is free to use.
